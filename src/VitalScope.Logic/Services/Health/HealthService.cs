namespace VitalScope.Logic.Services.Health;

public class HealthService : IHealthService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public HealthService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    public async Task<bool> CheckHealth(CancellationToken cancellationToken = default)
    {
        using var client = _httpClientFactory.CreateClient("NodeHttpClient");
        
        var result = await client.GetAsync("health-proxy", cancellationToken);
        if (result.IsSuccessStatusCode)
        {
            return true;
        }
        
        return false;
    }
}