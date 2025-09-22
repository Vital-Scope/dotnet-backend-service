using VitalScope.Logic.Models.Input.Authentication;
using VitalScope.Logic.Services.Authentication;
using VitalScope.Logic.Services.Study;

namespace VitalScope.Api.Extensions;

public static class EndpointExtensions
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapGroup("api/auth")
            .WithTags("Auth")
            .MapAuthApi();
    }

    private static RouteGroupBuilder MapAuthApi(this RouteGroupBuilder group)
    {
        group.MapPost("auth/", async (LoginDto dto, IAuthenticationService service) =>
        {
            var result = await service.LoginAsync(dto);
            return Results.Ok(result);
        });
        
        group.MapPost("registration/", async (RegisterDto dto, IAuthenticationService service) =>
        {
            var result = await service.RegisterUserAsync(dto);
            return Results.Ok(result);
        });
        
        return group;
    }
}