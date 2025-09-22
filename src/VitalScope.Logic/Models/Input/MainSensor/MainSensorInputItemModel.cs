using VitalScope.Common.Enums;

namespace VitalScope.Logic.Models.Input.MainSensor;

public sealed class MainSensorInputItemModel
{
    public double Time { get; set; }
    
    public ChannelType Channel { get; set; }  // FHR / UC
    
    public double Value { get; set; }
}