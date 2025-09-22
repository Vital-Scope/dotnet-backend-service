using Microsoft.Extensions.Logging;
using VitalScope.Insfrastructure.Models;
using VitalScope.Insfrastructure.Repositories.Base;
using VitalScope.Insfrastructure.Specifications.Patient;
using VitalScope.Logic.Models.Input.Patient;
using VitalScope.Logic.Models.Output.MainSensor;
using VitalScope.Logic.Models.Output.MetaSensor;
using VitalScope.Logic.Models.Output.Patient;

namespace VitalScope.Logic.Patient;

public sealed class PatientService : IPatientService
{
    private readonly IRepository<PatientEntity> _repository;
    private readonly ILogger<PatientService> _logger;

    public PatientService(IRepository<PatientEntity> repository, ILogger<PatientService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<PatientResultModel> AddAsync(PatientModel patient, CancellationToken cancellationToken = default)
    {
        try
        {
            var id = Guid.NewGuid();
        
            await _repository.AddAsync(new PatientEntity
            {
                Id = id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                MiddleName = patient.MiddleName,
                BirthDate = patient.BirthDate,
                ClientId = patient.ClientId,
                PregnancyNumber = patient.PregnancyNumber,
                PregnancyWeek = patient.PregnancyWeek,
                Anamnesis = patient.Anamnesis,
                DoctorNotes = patient.DoctorNotes,
                Avatar = patient.Avatar
            }, cancellationToken);

            var result = await _repository.GetFirstOrDefaultAsync(new PatientSpecification(id), cancellationToken);
            if (result is null)
            {
                _logger.LogWarning($"Сущность не найдена {id}");
                return default;
            }

            return new PatientResultModel
            {
                Id = id,
                FirstName = result.FirstName,
                LastName = result.LastName,
                MiddleName = result.MiddleName,
                BirthDate = result.BirthDate,
                ClientId = result.ClientId,
                PregnancyNumber = result.PregnancyNumber,
                PregnancyWeek = result.PregnancyWeek,
                Anamnesis = result.Anamnesis,
                DoctorNotes = result.DoctorNotes,
                Avatar = result.Avatar
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ошикбка добавления пациента");
            throw;
        }

    }

    public async Task<PatientResultModel> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _repository.GetFirstOrDefaultAsync(new PatientSpecification(id), cancellationToken);
            if (result is null)
            {
                _logger.LogWarning($"Сущность не найдена {id}");
                return default;
            }

            return new PatientResultModel
            {
                Id = id,
                FirstName = result.FirstName,
                LastName = result.LastName,
                MiddleName = result.MiddleName,
                BirthDate = result.BirthDate,
                ClientId = result.ClientId,
                PregnancyNumber = result.PregnancyNumber,
                PregnancyWeek = result.PregnancyWeek,
                Anamnesis = result.Anamnesis,
                DoctorNotes = result.DoctorNotes,
                Avatar = result.Avatar,
                Informations = result.StudyMetaInformations.Select(c =>
                {
                    return new MetaSensorOutputModel
                    {
                        Id = c.Id,
                        Ph = c.Ph,
                        Be = c.Be,
                        Diagnosis = c.Diagnosis,
                        СarbonDioxide = c.СarbonDioxide,
                        Glu = c.Glu,
                        Sensors = c.InfoMetas.Select(v => new MainSensorOutputModel
                        {
                            Id = v.Id,
                            Channel = v.Channel,
                            Time = v.Time,
                            Value = v.Value,
                        })
                    };
                })
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Ошикбка получения пациента {id}");
            throw;
        }

    }

    public async Task<IEnumerable<PatientResultModel>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var entities = await _repository.GetAllAsync(new PatientSpecification(), cancellationToken);

            return entities.Select(x => new PatientResultModel
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                MiddleName = x.MiddleName,
                BirthDate = x.BirthDate,
                ClientId = x.ClientId,
                PregnancyNumber = x.PregnancyNumber,
                PregnancyWeek = x.PregnancyWeek,
                Anamnesis = x.Anamnesis,
                DoctorNotes = x.DoctorNotes,
                Avatar = x.Avatar
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Не удалось получить ");
            throw;
        }
       
        
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _repository.GetFirstOrDefaultAsync(new PatientSpecification(id), cancellationToken);
            if (result is null)
            {
                _logger.LogWarning($"Сущность не найдена {id}");
                return;
            }

            result.IsDeleted = true;
            
            await _repository.UpdateAsync(result, cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Не удалось удалить пациента с {id}");
            throw;
        }

    }

    public async Task<PatientResultModel> UpdateAsync(EditPatientModel patient, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _repository.GetFirstOrDefaultAsync(new PatientSpecification(patient.Id), cancellationToken);
            if (result is null)
            {
                _logger.LogWarning($"Сущность не найдена {patient.Id}");
                return default;
            }
            
            result.FirstName = patient.FirstName;
            result.LastName = patient.LastName;
            result.MiddleName = patient.MiddleName;
            result.BirthDate = patient.BirthDate;
            result.ClientId = patient.ClientId;
            result.PregnancyNumber = patient.PregnancyNumber;
            result.PregnancyWeek = patient.PregnancyWeek;
            result.Anamnesis = patient.Anamnesis;
            result.DoctorNotes = patient.DoctorNotes;
            result.Avatar = patient.Avatar;
            
            await _repository.UpdateAsync(result, cancellationToken);
            
            return new PatientResultModel
            {
                Id = result.Id,
                FirstName = result.FirstName,
                LastName = result.LastName,
                MiddleName = result.MiddleName,
                BirthDate = result.BirthDate,
                ClientId = result.ClientId,
                PregnancyNumber = result.PregnancyNumber,
                PregnancyWeek = result.PregnancyWeek,
                Anamnesis = result.Anamnesis,
                DoctorNotes = result.DoctorNotes,
                Avatar = result.Avatar
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}