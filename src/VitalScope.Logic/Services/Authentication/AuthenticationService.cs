using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using VitalScope.Common.Options;
using VitalScope.Common.Roles;
using VitalScope.Insfrastructure.Identity.Authentication;
using VitalScope.Logic.Models.Input.Authentication;
using VitalScope.Logic.Models.Output.Authentication;

namespace VitalScope.Logic.Services.Authentication;

public sealed class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IOptions<JwtOptions> _options;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthenticationService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
        IOptions<JwtOptions> options, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _options = options;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<LoginResult> LoginAsync(LoginDto dto, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByNameAsync(dto.Username);
        if (user != null && await _userManager.CheckPasswordAsync(user, dto.Password))
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            };

            authClaims.AddRange(userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Secret));

            var token = new JwtSecurityToken(
                issuer: _options.Value.ValidIssuer,
                audience: _options.Value.ValidAudience,
                expires: DateTime.Now.AddHours(_options.Value.ExpiresHour),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new LoginResult()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            };
        }

        return default;
    }

    public async Task LogoutAsync(CancellationToken cancellationToken = default)
    {
        var userExists = await _userManager.FindByNameAsync(UserName);
        if (userExists != default)
        {
            await _userManager.UpdateSecurityStampAsync(userExists);
        }
    }

    public async Task<RegisterResult> RegisterUserAsync(RegisterDto dto, CancellationToken cancellationToken = default)
    {
        var userExists = await _userManager.FindByNameAsync(dto.Username);
        if (userExists != default)
        {
            return new RegisterResult()
            {
                IsSucceeded = false,
                Errors = new[] {"Пользователь уже существует"}
            };
        }

        var user = new ApplicationUser()
        {
            Email = dto.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = dto.Username
        };

        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
        {
            return new RegisterResult()
            {
                IsSucceeded = result.Succeeded,
                Errors = result.Errors.Select(x=>x.Description)
            };
        }
        if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
        {
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
        }

        if (!await _roleManager.RoleExistsAsync(UserRoles.Doctor))
        {
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.Doctor));
        }
        
        if (await _roleManager.RoleExistsAsync(dto.Role))
        {
            await _userManager.AddToRoleAsync(user, dto.Role);
        }
            
        return new RegisterResult()
        {
            Id = Guid.Parse(user.Id),
            IsSucceeded = true,
            Errors = Enumerable.Empty<string>()
        };
    }

    public string UserName => _httpContextAccessor.HttpContext?.User?.Identity.Name;
    
    public async Task<IEnumerable<string>> GetRolesAsync(string userName, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if(user == null)
        {
            return Enumerable.Empty<string>();
        }

        return await _userManager.GetRolesAsync(user);
    }
}