using VitalScope.Common.Options;

namespace VitalScope.Api.Extensions;

public static class ConfigureOptionsExtensions
{
    public static IServiceCollection ConfigureOptionsConfigs(this IServiceCollection services, IConfigurationManager config)
    { 
        services.Configure<ExternalServiceOptions>(config.GetSection(nameof(ExternalServiceOptions)));

        services.Configure<MqttOptions>(config.GetSection(nameof(MqttOptions)));
        
        return services;
    }
}