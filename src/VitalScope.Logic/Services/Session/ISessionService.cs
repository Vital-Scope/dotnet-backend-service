using VitalScope.Common.Enums;
using VitalScope.Logic.Models.Input.Session;
using VitalScope.Logic.Models.Output.Session;

namespace VitalScope.Logic.Services.Session;

public interface ISessionService
{
    Task<SessionOutputModel> GetActiveSessionAsync(CancellationToken cancellationToken = default);
    
    Task<SessionOutputModel> SetStatus(EditSessionInputModel model, CancellationToken cancellationToken = default);
}