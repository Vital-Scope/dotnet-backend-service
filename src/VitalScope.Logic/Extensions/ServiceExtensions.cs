using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using VitalScope.Logic.Patient;
using VitalScope.Logic.Services;
using VitalScope.Logic.Services.Study;

namespace VitalScope.Logic.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddLogic(this IServiceCollection services)
    {
        services.AddScoped<IInfrastructureService, InfrastructureService>();
        services.AddScoped<IStudyService, StudyService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IPatientService, PatientService>();
        
        
        
        return services;
    }
}