using Microsoft.Extensions.Logging;
using VitalScope.Common.Enums;
using VitalScope.Common.Helpers;
using VitalScope.Insfrastructure.Models;
using VitalScope.Insfrastructure.Repositories.Base;
using VitalScope.Insfrastructure.Specifications.Study;
using VitalScope.Logic.Models.Input.Session;
using VitalScope.Logic.Models.Output.Session;

namespace VitalScope.Logic.Services.Session;

public sealed class SessionService : ISessionService
{
    private readonly IRepository<StudyMetaInformationEntity> _repository;
    private readonly ILogger<SessionService> _logger;

    public SessionService(IRepository<StudyMetaInformationEntity> repository, ILogger<SessionService> logger)
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task<SessionOutputModel> GetActiveSessionAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var enity = await _repository.GetFirstOrDefaultAsync(new StudySpecification(StatusType.Active),
                cancellationToken);
            if (enity == null)
            {
                _logger.LogWarning("No active study found for this session");
                
                return null;
            }

            return new SessionOutputModel
            {
                Status = enity.Status,
                PatientId = enity.PatientId,
                MonitoringId = enity.Id,
                FirstName = enity.Patient?.FirstName,
                LastName = enity.Patient?.LastName,
                MiddleName = enity.Patient?.MiddleName,
                DateStart = enity.DateStart.ToTime(),
                DateEnd = enity.DateEnd.ToTime()
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting active session");
            
            throw;
        }

    }

    public async Task<SessionOutputModel> SetStatus(EditSessionInputModel model, CancellationToken cancellationToken = default)
    {
        var enity = await _repository.GetFirstOrDefaultAsync(new StudySpecification(model.MonitoringId),
            cancellationToken);
        
        enity.Status = model.Status;
        enity.UpdatedAt = DateTime.Now;
        
        if (model.Status == StatusType.Active)
        {
            enity.DateStart = DateTime.Now;
        }
        else
        {
            enity.DateEnd = DateTime.Now;
        }
        
        await _repository.UpdateAsync(enity, cancellationToken);
        
        return new SessionOutputModel
        {
            Status = enity.Status,
            PatientId = enity.PatientId,
            MonitoringId = enity.Id,
            FirstName = enity.Patient?.FirstName,
            LastName = enity.Patient?.LastName,
            MiddleName = enity.Patient?.MiddleName,
            DateStart = enity.DateStart.ToTime(),
            DateEnd = enity.DateEnd.ToTime()
        };
    }
}