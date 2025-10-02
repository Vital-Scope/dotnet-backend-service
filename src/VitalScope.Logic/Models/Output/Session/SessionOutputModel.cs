using VitalScope.Common.Enums;

namespace VitalScope.Logic.Models.Output.Session;

public class SessionOutputModel
{
    public StatusType? Status { get; set; }
    
    public ResultType? ResultType { get; set; }
    
    public Guid? MonitoringId { get; set; }
    
    public Guid? PatientId { get; set; }
    
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }
    
    public string? MiddleName { get; set; }
    
    public long? DateStart { get; set; }
    
    public long? DateEnd { get; set; }
    
    public float? Percent { get; set; }
}