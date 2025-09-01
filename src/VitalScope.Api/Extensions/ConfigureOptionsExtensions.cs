namespace VitalScope.Api.Extensions;

public static class ConfigureOptionsExtensions
{
    public static IServiceCollection ConfigureOptionsConfigs(this IServiceCollection services, IConfigurationManager config)
    { 
        return services;
    }
}