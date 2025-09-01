using VitalScope.Insfrastructure;
using VitalScope.Insfrastructure.Extensions;

namespace VitalScope.Api.HostedServices;

public sealed class MigrationHostedService(IServiceProvider provider, ILogger<MigrationHostedService> logger)
    : IHostedService

{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            await provider.ApplyMigrationAsync<ApplicationDbContext>(cancellationToken);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Не удалось создать миграцю");
            throw new Exception(exception.Message);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}