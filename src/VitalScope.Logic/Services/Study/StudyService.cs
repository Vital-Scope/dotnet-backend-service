using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using VitalScope.Common.Enums;
using VitalScope.Common.Helpers;
using VitalScope.Common.Options;
using VitalScope.Insfrastructure.Models;
using VitalScope.Insfrastructure.Repositories.Base;
using VitalScope.Insfrastructure.Specifications.Study;
using VitalScope.Logic.Hubs;
using VitalScope.Logic.Mappings;
using VitalScope.Logic.Models.Business;
using VitalScope.Logic.Models.Input.MainSensor;
using VitalScope.Logic.Models.Input.MetaSensor;
using VitalScope.Logic.Models.Output.MainSensor;
using VitalScope.Logic.Models.Output.MetaSensor;

namespace VitalScope.Logic.Services.Study;

public sealed class StudyService : IStudyService
{
    private readonly IRepository<StudyMetaInformationEntity> _repository;
    private readonly IRepository<StudyMainInformationEntity> _repositoryMain;
    private readonly ILogger<StudyService> _logger;
    private readonly IHubContext<SensorHub> _chatHub;

    public StudyService(IRepository<StudyMetaInformationEntity> repository,
        IRepository<StudyMainInformationEntity> repositoryMain, ILogger<StudyService> logger,
        IHubContext<SensorHub> chatHub)
    {
        _repository = repository;
        _repositoryMain = repositoryMain;
        _logger = logger;
        _chatHub = chatHub;
    }

    public async Task<MetaSensorOutputModel> AddMetaAsync(MetaSensorInputModel model, CancellationToken cancellationToken = default)
    {
        try
        {
            var id = Guid.NewGuid();
            
            await _repository.AddAsync(new StudyMetaInformationEntity
            {
                Id = id,
                Be = model.MedicalTests?.Be,
                Ph = model.MedicalTests?.Ph,
                Glu = model.MedicalTests?.Glu,
                СarbonDioxide = model.MedicalTests?.СarbonDioxide,
                DateStart = model.DateStart.ToDateTime(),
                DateEnd = model.DateEnd.ToDateTime(),
                Status = model.Status,
                Result = model.Result,
                Diagnosis = model.Diagnosis,
                PatientId = model.PatientId,
                InfoMetas = null,
                CreatedAt = DateTime.Now.Date,
                UpdatedAt = DateTime.Now.Date,
                Notes = model.Notes
            }, cancellationToken);

            var result = await _repository.GetFirstOrDefaultAsync(new StudySpecification(id), cancellationToken);
            if (result is null)
            {
                return default;
            }

            return result.EntityToOutputModel();

        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }

    }

    public async Task<MetaSensorOutputModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetFirstOrDefaultAsync(new StudySpecification(id), cancellationToken);
        if (entity is null)
        {
            return default;
        }

        return entity.EntityToOutputModel();
    }

    public async Task<IEnumerable<MetaSensorOutputModel>> GetAllMetaAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _repository.GetAllAsync(new StudySpecification(), cancellationToken);
        if (!entities.Any())
        {
            return Enumerable.Empty<MetaSensorOutputModel>();
        }

        return entities.Select(x => x.EntityToOutputModel());
    }

    public async Task AddMain(MainSensorInputModel model, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetFirstOrDefaultAsync(new StudySpecification(model.MonitoringId), cancellationToken);
        if (entity is null)
        {
            return;
        }

        await _repositoryMain.AddRangeAsync(model.Informations.Select(x => new StudyMainInformationEntity
        {
            Id = Guid.NewGuid(),
            Time = x.Time,
            Channel = x.Channel,
            Value = x.Value,
            StudyMetaInformationId = entity.Id
        }), cancellationToken);
    }

    public async Task AddMainItem(SensorModel model, IEnumerable<SensorModel> models, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetFirstOrDefaultAsync(new StudySpecification(StatusType.Active), cancellationToken);
        if (entity is null)
        {
            return;
        }

        await _chatHub.Clients.All.SendAsync("ReceiveSensor", model, cancellationToken);

        if (models != null && models.Any())
        {
            await _repositoryMain.AddRangeAsync(models.Select(x => new StudyMainInformationEntity
            {
                Id = Guid.NewGuid(),
                Time = x.Time,
                Channel = (ChannelType)x.Type,
                Value = x.Value,
                StudyMetaInformationId = entity.Id
            }), cancellationToken);
        }
    }

    public async Task AddMainItems(IEnumerable<SensorModel> models, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetFirstOrDefaultAsync(new StudySpecification(StatusType.Active), cancellationToken);
        if (entity is null)
        {
            return;
        }
        
        await _repositoryMain.AddRangeAsync(models.Select(x => new StudyMainInformationEntity
        {
            Id = Guid.NewGuid(),
            Time = x.Time,
            Channel = (ChannelType)x.Type,
            Value = x.Value,
            StudyMetaInformationId = entity.Id
        }), cancellationToken);
    }

    public async Task<IEnumerable<MainSensorOutputModel>> GetByIdMainAsync(Guid metaId, CancellationToken cancellationToken = default)
    {
        var entities = await _repositoryMain.GetAllAsync(new StudyMainSpecification(metaId), cancellationToken);

        return entities.Select(x => new MainSensorOutputModel
        {
            Id = x.Id,
            Value = x.Value,
            Time = x.Time,
            Channel = x.Channel
        });

    }

    public async Task<MetaSensorOutputModel> EditMetaAsync(EditMetaSensorInputModel model, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetFirstOrDefaultAsync(new StudySpecification(model.Id), cancellationToken);
        if (entity is null)
        {
            return default;
        }

        entity.DateStart = model.DateStart.ToDateTime();
        entity.DateEnd = model.DateEnd.ToDateTime();
        entity.Status = model.Status;
        entity.Result = model.Result;
        entity.Diagnosis = model.Diagnosis;
        entity.Notes = model.Notes;
        entity.Ph = model.MedicalTests?.Ph;
        entity.Glu = model.MedicalTests?.Glu;
        entity.Lac = model.MedicalTests?.Lac;
        entity.СarbonDioxide = model.MedicalTests?.СarbonDioxide;
        entity.Be = model.MedicalTests?.Be;
        entity.PregnancyWeek = model.PregnancyWeek;
        entity.UpdatedAt = DateTime.Now;

        await _repository.UpdateAsync(entity, cancellationToken);

        return entity.EntityToOutputModel();
    }
}