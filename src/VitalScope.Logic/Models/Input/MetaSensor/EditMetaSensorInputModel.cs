using VitalScope.Common.Enums;

namespace VitalScope.Logic.Models.Input.MetaSensor;

public sealed class EditMetaSensorInputModel
{
    public Guid Id { get; set; }
    
    public long? DateStart { get; set; }
    
    
    public long? DateEnd { get; set; }
    
    
    public int? PregnancyWeek { get; set; }
    
    public StatusType? Status { get; set; }
    
    public ResultType? Result { get; set; }
    
    
    public MedicalTestInputModel? MedicalTests { get; set; }

    public string? Diagnosis { get; set; }
    
    public string? Notes { get; set; }
}