using VitalScope.Insfrastructure.Models;
using VitalScope.Insfrastructure.Specifications.Base;

namespace VitalScope.Insfrastructure.Specifications.Study;

public  sealed class StudyMainSpecification : BaseSpecification<StudyMainInformationEntity>
{
    public StudyMainSpecification(Guid metaId, bool trackable = false) : base(x => x.StudyMetaInformationId == metaId)
    {
        Trackable = trackable;
    }
}