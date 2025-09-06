using VitalScope.Insfrastructure.Common;

namespace VitalScope.Insfrastructure.Models;

public class UserSettingEntity : BaseEntity
{
    public double HeartRateMin { get; set; }
    
    public double HeartRateMax { get; set; }
    
    public double ContractionMin { get; set; }
    
    public double ContractionMax { get; set; }
    
    public Guid UserId { get; set; }
    
    public UserEntity User { get; set; }
}