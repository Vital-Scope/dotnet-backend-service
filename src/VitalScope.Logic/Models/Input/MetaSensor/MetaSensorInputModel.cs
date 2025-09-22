namespace VitalScope.Logic.Models.Input.MetaSensor;

public sealed class MetaSensorInputModel
{
    public DateTime? Date { get; set; }
    
    public float? Ph { get; set; }
    
    public float? Glu { get; set; }
    
    public float? СarbonDioxide { get; set; }
    
    public float? Be { get; set; }
    
    public float? Lac { get; set; }

    public string? Diagnosis { get; set; }
    
    public Guid? PatientId { get; set; }
}