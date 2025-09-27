using VitalScope.Common.Enums;
using VitalScope.Insfrastructure.Common;

namespace VitalScope.Insfrastructure.Models;

public sealed class StudyMetaInformationEntity : BaseEntity
{
    
    public DateTime? DateStart { get; set; }
    
    public DateTime? DateEnd { get; set; }
    
    
    public int? PregnancyWeek { get; set; }
    
    public StatusType? Status { get; set; }
    
    public ResultType? Result { get; set; }
    
    public float? Ph { get; set; }
    
    public float? Glu { get; set; }
    
    public float? СarbonDioxide { get; set; }
    
    public float? Be { get; set; }
    
    public float? Lac { get; set; }

    public string? Diagnosis { get; set; }

    public ICollection<StudyMainInformationEntity>? InfoMetas { get; set; }
    
    public PatientEntity? Patient { get; set; }
    
    public Guid? PatientId { get; set; }
    
    public DateTime? CreatedAt { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
    
    public string? Notes { get; set; }
    
    public bool ValidateFields()
    {
        return AnyNotNull(Ph, Glu, СarbonDioxide, Be, Lac);
    }

    private static bool AnyNotNull(params object?[] fields)
    {
        return fields.Any(field => field != null);
    }
}