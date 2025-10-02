namespace VitalScope.Logic.Services;

public interface IMLService
{
    Task<decimal?> CalculateRiskAsync(Guid monitoringId, CancellationToken cancellationToken = default);
}