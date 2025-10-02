namespace VitalScope.Logic.Models.Input.MainSensor;

public class MainSensorInputModel
{
    public Guid MonitoringId { get; set; }
    
    public IEnumerable<MainSensorInputItemModel> Informations { get; set; }
}