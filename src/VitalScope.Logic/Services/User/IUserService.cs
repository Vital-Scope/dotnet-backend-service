using VitalScope.Logic.Models.Input.Authentication;
using VitalScope.Logic.Models.Output.Authentication;
using VitalScope.Logic.Models.Output.User;

namespace VitalScope.Logic.Services.User;

public interface IUserService
{
    Task<UserOutputModel> LoginAsync(LoginDto dto, CancellationToken cancellationToken = default);

    Task<RegisterResult> RegisterAsync(RegisterDto dto, CancellationToken cancellationToken = default);
}