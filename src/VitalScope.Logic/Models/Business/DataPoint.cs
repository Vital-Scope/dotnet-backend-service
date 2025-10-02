namespace VitalScope.Logic.Models.Business;

public class DataPoint
{
    public double? Time { get; set; }
    public string? Channel { get; set; }
    public double? Value { get; set; }
}