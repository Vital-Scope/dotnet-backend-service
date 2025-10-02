namespace VitalScope.Logic.Services.Health;

public interface IHealthService
{
    Task<bool> CheckHealth(CancellationToken cancellationToken = default);
}