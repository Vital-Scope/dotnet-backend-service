using VitalScope.Logic.Models.Output.MainSensor;

namespace VitalScope.Logic.Models.Output.MetaSensor;

public sealed class MetaSensorOutputModel
{
    public Guid Id { get; set; }
    
    public DateTime? Date { get; set; }
    
    public float? Ph { get; set; }
    
    public float? Glu { get; set; }
    
    public float? СarbonDioxide { get; set; }
    
    public float? Be { get; set; }
    
    public float? Lac { get; set; }

    public string? Diagnosis { get; set; }
    
    public Guid? PatientId { get; set; }
    
    public IEnumerable<MainSensorOutputModel>  Sensors { get; set; }
}