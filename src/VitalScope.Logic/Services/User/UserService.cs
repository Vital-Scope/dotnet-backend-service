using VitalScope.Insfrastructure.Models;
using VitalScope.Insfrastructure.Repositories.Base;
using VitalScope.Insfrastructure.Specifications.User;
using VitalScope.Logic.Models.Input.Authentication;
using VitalScope.Logic.Models.Output.Authentication;
using VitalScope.Logic.Models.Output.User;
using VitalScope.Logic.Services.Authentication;

namespace VitalScope.Logic.Services.User;

public sealed class UserService : IUserService
{
    private readonly IRepository<UserEntity> _userRepository;
    private readonly IAuthenticationService _authenticationService;

    public UserService(IRepository<UserEntity> userRepository, IAuthenticationService authenticationService)
    {
        _userRepository = userRepository;
        _authenticationService = authenticationService;
    }


    public async Task<UserOutputModel> LoginAsync(LoginDto dto, CancellationToken cancellationToken = default)
    {
       var result = await _authenticationService.LoginAsync(dto, cancellationToken);

       var user = await _userRepository.GetFirstOrDefaultAsync(new UserSpecification(result.Id), cancellationToken);
       
       return user is null ? default : new UserOutputModel(result.Token, result.Expiration, user.FirstName, user.LastName, user.Id);
    }

    public Task<RegisterResult> RegisterAsync(RegisterDto dto, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}