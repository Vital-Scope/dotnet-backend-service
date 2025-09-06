using VitalScope.Insfrastructure.Common;

namespace VitalScope.Insfrastructure.Models;

public sealed class PatientEntity : BaseEntity
{
    public string? Name { get; set; }
    
    public int? Age { get; set; }

    public ICollection<StudyMetaInformationEntity>  StudyMetaInformations { get; set; }
}