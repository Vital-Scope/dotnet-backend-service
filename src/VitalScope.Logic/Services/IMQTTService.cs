using VitalScope.Logic.Models.Business;

namespace VitalScope.Logic.Services;

public interface IMQTTService
{
    Task SubscribeAsync(SensorModel model, CancellationToken cancellationToken = default);
}