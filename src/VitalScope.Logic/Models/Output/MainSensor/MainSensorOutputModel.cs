using VitalScope.Common.Enums;

namespace VitalScope.Logic.Models.Output.MainSensor;

public class MainSensorOutputModel
{
    public Guid Id { get; set; }
    
    public double Time { get; set; }
    
    public ChannelType Channel { get; set; }  // FHR / UC
    
    public double Value { get; set; }
}