using Microsoft.Extensions.DependencyInjection;
using VitalScope.Logic.Services;
using VitalScope.Logic.Services.Study;

namespace VitalScope.Logic.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddLogic(this IServiceCollection services)
    {
        services.AddScoped<IInfrastructureService, InfrastructureService>();
        services.AddScoped<IStudyService, StudyService>();
        
        return services;
    }
}