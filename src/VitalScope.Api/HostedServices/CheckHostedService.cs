using Microsoft.AspNetCore.SignalR;
using VitalScope.Common.Helpers;
using VitalScope.Logic.Hubs;
using VitalScope.Logic.Models.Business;

namespace VitalScope.Api.HostedServices;

public sealed class CheckHostedService : BackgroundService
{
    private readonly IHubContext<CheckHub> _hubContext;

    public CheckHostedService(IHubContext<CheckHub> hubContext)
    {
        _hubContext = hubContext;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (true)
        {
            await _hubContext.Clients.All.SendAsync("HealthCheck", new CheckModel
            {
                Status = "Active",
                Time = DateTime.Now.ToTime()
            }, stoppingToken);

            await Task.Delay(TimeSpan.FromSeconds(1));
        }

    }
}