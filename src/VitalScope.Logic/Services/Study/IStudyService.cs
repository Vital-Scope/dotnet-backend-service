using VitalScope.Logic.Models.Output;

namespace VitalScope.Logic.Services.Study;

public interface IStudyService
{
    Task AddInformationsAsync(CancellationToken cancellationToken = default);

    Task<StudyModel> GetValuesByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<MetaInformatio>> GetMetaInformatiosAsync(CancellationToken cancellationToken = default);
}