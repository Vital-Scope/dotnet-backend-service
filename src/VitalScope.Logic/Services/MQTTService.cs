using MQTTnet;

namespace VitalScope.Logic.Services;

public sealed class MQTTService : IMQTTService
{
    private readonly MqttClientFactory _mqttClientFactory;

    public MQTTService()
    {
        _mqttClientFactory = new MqttClientFactory();
    }

    public async Task SubscribeAsync(CancellationToken cancellationToken = default)
    {
        using var mqttClient  = _mqttClientFactory.CreateMqttClient();
        
        /*var options = new MqttClientOptionsBuilder()
            .WithTcpServer(broker, port) // MQTT broker address and port
            .WithCredentials(username, password) // Set username and password
            .WithClientId(clientId)
            .WithCleanSession()
            .Build();
        
        var connectResult = await mqttClient.ConnectAsync(mqttClientOptions, cancellationToken);*/
        
        var mqttSubscribeOptions = _mqttClientFactory.CreateSubscribeOptionsBuilder()
            .WithTopicFilter("dsds")
            .Build();

        await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);

        mqttClient.ApplicationMessageReceivedAsync += e =>
        {
            Console.WriteLine($"Received message: {e.ApplicationMessage.Topic}");
            
            return Task.CompletedTask;
        };
    }
}