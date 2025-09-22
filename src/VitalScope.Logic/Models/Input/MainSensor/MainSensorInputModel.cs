namespace VitalScope.Logic.Models.Input.MainSensor;

public class MainSensorInputModel
{
    public Guid MetaId { get; set; }
    
    public IEnumerable<MainSensorInputItemModel> Informations { get; set; }
}