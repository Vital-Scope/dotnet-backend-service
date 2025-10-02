using VitalScope.Common.Enums;

namespace VitalScope.Logic.Models.Output.MetaSensor;

public class MetaSensorWithoutSensorOutputModel
{
    public Guid Id { get; set; }
    
    public long? DateStart { get; set; }
    
    public long? DateEnd { get; set; }
    
    public int? PregnancyWeek { get; set; }
    
    public StatusType? Status { get; set; }
    
    public ResultType? Result { get; set; }
    
    public MedicalTestOutputModel? MedicalTests { get; set; }

    public string? Diagnosis { get; set; }
    
    public Guid? PatientId { get; set; }

    public string? Notes { get; set; }
    
    public string FullName { get; set; }
    
    public long? CreatedAt { get; set; }
    
    public long? UpdatedAt { get; set; }
    
    public float? Percent { get; set; }
}