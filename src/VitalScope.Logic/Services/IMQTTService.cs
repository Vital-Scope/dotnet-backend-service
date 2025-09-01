namespace VitalScope.Logic.Services;

public interface IMQTTService
{
    Task SubscribeAsync(CancellationToken cancellationToken = default);
}