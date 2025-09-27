using VitalScope.Common.Enums;

namespace VitalScope.Logic.Models.Input.Session;

public class EditSessionInputModel
{
    public Guid MonitoringId { get; set; }
    
    public StatusType Status { get; set; }
}