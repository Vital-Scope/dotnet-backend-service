using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using VitalScope.Common.Enums;
using VitalScope.Common.Options;
using VitalScope.Insfrastructure.Models;
using VitalScope.Insfrastructure.Repositories.Base;
using VitalScope.Insfrastructure.Specifications.Study;
using VitalScope.Logic.Models.Business;
using VitalScope.Logic.Models.Output;

namespace VitalScope.Logic.Services.Study;

public sealed class StudyService : IStudyService
{
    private readonly IRepository<StudyMetaInformationEntity> _repository;
    private readonly IOptions<ExternalServiceOptions> _externalServiceOptions;

    public StudyService(IRepository<StudyMetaInformationEntity> repository,
        IOptions<ExternalServiceOptions> externalServiceOptions)
    {
        _repository = repository;
        _externalServiceOptions = externalServiceOptions;
    }

    public async Task AddInformationsAsync(IFormFileCollection files, CancellationToken cancellationToken = default)
    {
        using var client = new HttpClient();

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
    }

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
    }
}