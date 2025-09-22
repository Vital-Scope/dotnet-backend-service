using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using VitalScope.Common.Enums;
using VitalScope.Common.Options;
using VitalScope.Insfrastructure.Models;
using VitalScope.Insfrastructure.Repositories.Base;
using VitalScope.Insfrastructure.Specifications.Study;
using VitalScope.Logic.Models.Business;
using VitalScope.Logic.Models.Input.MainSensor;
using VitalScope.Logic.Models.Input.MetaSensor;
using VitalScope.Logic.Models.Output;
using VitalScope.Logic.Models.Output.MainSensor;
using VitalScope.Logic.Models.Output.MetaSensor;

namespace VitalScope.Logic.Services.Study;

public sealed class StudyService : IStudyService
{
    private readonly IRepository<StudyMetaInformationEntity> _repository;
    private readonly IRepository<StudyMainInformationEntity> _repositoryMain;
    private readonly IOptions<ExternalServiceOptions> _externalServiceOptions;
    private readonly ILogger<StudyService> _logger;

    public StudyService(IRepository<StudyMetaInformationEntity> repository,
        IRepository<StudyMainInformationEntity> repositoryMain, IOptions<ExternalServiceOptions> externalServiceOptions,
        ILogger<StudyService> logger)
    {
        _repository = repository;
        _repositoryMain = repositoryMain;
        _externalServiceOptions = externalServiceOptions;
        _logger = logger;
    }

    /*
    public async Task AddInformationsAsync(IFormFileCollection files, CancellationToken cancellationToken = default)
    {
       /* using var client = new HttpClient();

        var url = $"{_externalServiceOptions.Value}/api/parse_wfdb/";

        using var form = new MultipartFormDataContent();

        foreach (var file in files)
        {
            var fileStream = file.OpenReadStream();
            var streamContent = new StreamContent(fileStream);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            form.Add(streamContent, "files", file.FileName);
        }

        var response = await client.PostAsync(url, form);

        // Читаем результат
        var responseContent = await response.Content.ReadAsStringAsync();

        var responseJson = JsonSerializer.Deserialize<ParseWfdbResponse>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        var dateNow = DateTime.UtcNow;

        await _repository.AddAsync(new StudyMetaInformationEntity()
        {
            Id = Guid.NewGuid(),
            Name = responseJson.Metadata.RecordName,
            SamplingFrequency = responseJson.Metadata?.SamplingFrequency,
            Date = responseJson.Metadata?.BaseDate,
            Age = responseJson.Metadata?.Comments?.MaternalFactors?.Age,
            Gravidity = responseJson.Metadata?.Comments?.MaternalFactors?.Gravidity,
            InfoMetas = responseJson.Data.Select(x => new StudyMainInformationEntity()
            {
                Id = Guid.NewGuid(),
                Value = x.Value.Value,
                Time = dateNow.AddSeconds(x.Time.Value),
                Channel = x.Channel.Equals("Fhr", StringComparison.OrdinalIgnoreCase) ? ChannelType.Fhr : ChannelType.Uc

            }).ToList()
        }, cancellationToken);
    }
*/
    /*
public async Task<StudyModel> GetValuesByIdAsync(Guid id, CancellationToken cancellationToken = default)
{

    var result = await _repository.GetFirstOrDefaultAsync(new StudySpecification(id), cancellationToken);

    return new StudyModel
    {
        MetaInformatio = new MetaInformatio()
        {
            Id = result.Id,
            Name = result.Name,
            SamplingFrequency = result.SamplingFrequency,
            Age = result.Age
        },
        Values = result.InfoMetas.Select(v => new Maininformation()
        {
          //  Id = v.Id,
            Date = v.Time,
            Value = v.Value,
            ChannelType = v.Channel
        }).OrderBy(b=>b.Date)
    };

    return default;
}
*/ /*
    public async Task<IEnumerable<MetaInformatio>> GetMetaInformatiosAsync(CancellationToken cancellationToken = default)
    {

        var result = await _repository.GetAllAsync(new StudySpecification(), cancellationToken);

        return result.Select(x => new MetaInformatio
        {
            Id = x.Id,
            Name = x.Name,
            SamplingFrequency = x.SamplingFrequency,
            Age = x.Age
        });

        return default;
    }
    */
    public async Task<MetaSensorOutputModel> AddMetaAsync(MetaSensorInputModel model, CancellationToken cancellationToken = default)
    {
        try
        {
            var id = Guid.NewGuid();
            
            await _repository.AddAsync(new StudyMetaInformationEntity
            {
                Id = id,
                Lac = model.Lac,
                Be = model.Be,
                Ph = model.Ph,
                Glu = model.Glu,
                Date = model.Date,
                Diagnosis = model.Diagnosis,
                PatientId = model.PatientId,
                СarbonDioxide = model.СarbonDioxide,
                InfoMetas = null
            }, cancellationToken);

            var result = await _repository.GetFirstOrDefaultAsync(new StudySpecification(id), cancellationToken);
            if (result is null)
            {
                return default;
            }

            return new MetaSensorOutputModel
            {
                Id = result.Id,
                Lac = result.Lac,
                Be = result.Be,
                Ph = result.Ph,
                Glu = result.Glu,
                Date = result.Date,
                Diagnosis = result.Diagnosis,
                PatientId = result.PatientId,
                СarbonDioxide = result.СarbonDioxide,
                Sensors = result.InfoMetas.Select(x => new MainSensorOutputModel
                {
                    Id = x.Id,
                    Time = x.Time,
                    Channel = x.Channel,
                    Value = x.Value
                })
            };

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
        
        return new MetaSensorOutputModel
        {
            Id = entity.Id,
            Lac = entity.Lac,
            Be = entity.Be,
            Ph = entity.Ph,
            Glu = entity.Glu,
            Date = entity.Date,
            Diagnosis = entity.Diagnosis,
            PatientId = entity.PatientId,
            СarbonDioxide = entity.СarbonDioxide,
            Sensors = entity.InfoMetas.Select(x => new MainSensorOutputModel
            {
                Id = x.Id,
                Time = x.Time,
                Channel = x.Channel,
                Value = x.Value
            })
        };
    }

    public async Task<IEnumerable<MetaSensorOutputModel>> GetAllMetaAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _repository.GetAllAsync(new StudySpecification(), cancellationToken);
        if (!entities.Any())
        {
            return Enumerable.Empty<MetaSensorOutputModel>();
        }

        return entities.Select(x => new MetaSensorOutputModel
        {
            Id = x.Id,
            Lac = x.Lac,
            Be = x.Be,
            Ph = x.Ph,
            Glu = x.Glu,
            Date = x.Date,
            Diagnosis = x.Diagnosis,
            PatientId = x.PatientId,
            СarbonDioxide = x.СarbonDioxide,
            Sensors = x.InfoMetas.Select(x => new MainSensorOutputModel
            {
                Id = x.Id,
                Time = x.Time,
                Channel = x.Channel,
                Value = x.Value
            })
        });
    }

    public async Task AddMain(MainSensorInputModel model, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetFirstOrDefaultAsync(new StudySpecification(model.MetaId), cancellationToken);
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

    public async Task<IEnumerable<MainSensorOutputModel>> GetByIdMainAsync(Guid metaId, CancellationToken cancellationToken = default)
    {
        var entities = await _repositoryMain.GetAllAsync(new StudyMainSpecification(metaId), cancellationToken);

        return entities.Select(x => new MainSensorOutputModel
        {
            Id = x.Id,
            Value = x.Value,
            Time = x.Time,
            Channel = x.Channel,
        });

    }
}