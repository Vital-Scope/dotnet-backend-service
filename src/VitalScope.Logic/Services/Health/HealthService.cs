using Microsoft.Extensions.Logging;

namespace VitalScope.Logic.Services.Health;

public class HealthService : IHealthService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<HealthService> _logger;

    public HealthService(IHttpClientFactory httpClientFactory, ILogger<HealthService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public HealthService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    public async Task<bool> CheckHealth(CancellationToken cancellationToken = default)
    {
        try
        {
            using var client = _httpClientFactory.CreateClient("NodeHttpClient");
        
            var result = await client.GetAsync("proxy/health-proxy", cancellationToken);
            if (result.IsSuccessStatusCode)
            {
                return true;
            }
        
            return false;
        }
        catch (Exception e)
        {
            _logger.LogError(e,"Ошибка получения health-proxy");

            return false;
        }

    }
}