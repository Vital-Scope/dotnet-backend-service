using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Protocol;
using VitalScope.Common.Options;
using VitalScope.Logic.Models.Business;
using VitalScope.Logic.Services.Study;

namespace VitalScope.Api.HostedServices;

public class MqttHostedService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<MqttHostedService> _logger;
    private readonly IOptionsMonitor<MqttOptions> _options;
    
    IMqttClient mqttClient;
    private List<SensorModel> sensors;

    public MqttHostedService(IServiceProvider serviceProvider, ILogger<MqttHostedService> logger,
        IOptionsMonitor<MqttOptions> options)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        sensors = new List<SensorModel>();
        _options = options;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
       var factory = new MqttClientFactory();
       mqttClient = factory.CreateMqttClient();

       var options = new MqttClientOptionsBuilder()
           .WithClientId("aspnet-subscriber")
           .WithTcpServer(_options.CurrentValue.Host, _options.CurrentValue.Port)
           .WithCleanSession(false)
           .Build();

       mqttClient.ConnectedAsync += MqttClientOnConnectedAsync;
       mqttClient.DisconnectedAsync += MqttClientOnDisconnectedAsync;
       mqttClient.ApplicationMessageReceivedAsync += MqttClientOnApplicationMessageReceivedAsync;

       await mqttClient.ConnectAsync(options, stoppingToken);
    }

    private async Task MqttClientOnApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs arg)
    {
        try
        {
            await using var scope = _serviceProvider.CreateAsyncScope();
            var service = scope.ServiceProvider.GetRequiredService<IStudyService>();
            
            var jsonBytes = Encoding.UTF8.GetString(arg.ApplicationMessage.Payload);

            var sensor = JsonSerializer.Deserialize<SensorModel>(jsonBytes);
        
            if (sensor is not null)
            {
                sensors.Add(sensor);

                if (sensors.Count == _options.CurrentValue.Amount)
                {
                    await service.AddMainItem(sensor, sensors);
                
                    sensors.Clear();
                }
                else
                {
                    await service.AddMainItem(sensor, Enumerable.Empty<SensorModel>());
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }

    private Task MqttClientOnDisconnectedAsync(MqttClientDisconnectedEventArgs arg)
    {
        _logger.LogWarning("### Отключились от MQTT ###");

        return Task.CompletedTask;
    }

    private async Task MqttClientOnConnectedAsync(MqttClientConnectedEventArgs arg)
    {
        await mqttClient.SubscribeAsync(new MqttClientSubscribeOptionsBuilder()
            .WithTopicFilter(_options.CurrentValue.Topic, MqttQualityOfServiceLevel.AtLeastOnce)
            .Build());
    }
}