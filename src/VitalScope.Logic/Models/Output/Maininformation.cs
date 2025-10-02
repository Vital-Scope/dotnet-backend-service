using VitalScope.Common.Enums;

namespace VitalScope.Logic.Models.Output;

public class Maininformation
{
    public ChannelType  ChannelType { get; set; }
    
    public double? Value { get; set; }
    
    public DateTime? Date { get; set; }
    
   // public Guid Id { get; set; }
}