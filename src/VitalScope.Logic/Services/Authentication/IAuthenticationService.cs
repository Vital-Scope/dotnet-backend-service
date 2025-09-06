using VitalScope.Logic.Models.Input.Authentication;
using VitalScope.Logic.Models.Output.Authentication;

namespace VitalScope.Logic.Services.Authentication;

public interface IAuthenticationService
{
    Task<LoginResult> LoginAsync(LoginDto dto, CancellationToken cancellationToken = default);

    Task LogoutAsync(CancellationToken cancellationToken = default);

    Task<RegisterResult> RegisterUserAsync(RegisterDto dto, CancellationToken cancellationToken = default);

    string UserName { get; }

    Task<IEnumerable<string>> GetRolesAsync(string userName, CancellationToken cancellationToken = default);
}