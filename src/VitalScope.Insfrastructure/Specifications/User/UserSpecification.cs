using VitalScope.Insfrastructure.Models;
using VitalScope.Insfrastructure.Specifications.Base;

namespace VitalScope.Insfrastructure.Specifications.User;

public class UserSpecification : BaseSpecification<UserEntity>
{
    public UserSpecification(Guid applicationId, bool trackable = false) : base(x => x.ApplicationUserId == applicationId)
    {
        Trackable = trackable;
    }
}