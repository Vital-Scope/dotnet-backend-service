using VitalScope.Insfrastructure.Common;
using VitalScope.Insfrastructure.Identity.Authentication;

namespace VitalScope.Insfrastructure.Models;

public class UserEntity : BaseEntity
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string MiddleName { get; set; }
    
    public string Email { get; set; }
    
    public Guid ApplicationUserId { get; set; }
    
    public UserSettingEntity  UserSettings { get; set; }
}