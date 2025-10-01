using System.Globalization;

namespace VitalScope.Logic.Services;

public sealed class MLService : IMLService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public MLService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    
    public async Task<decimal?> CalculateRiskAsync(Guid monitoringId, CancellationToken cancellationToken = default)
    {
        using var client = _httpClientFactory.CreateClient("MLHttpClient");
        
        var result = await client.GetAsync($"api/predict/?guid={monitoringId}", cancellationToken);
        if (result.IsSuccessStatusCode)
        {
            var resultResponse = await result.Content.ReadAsStringAsync(cancellationToken);
            
            var cleanStr = resultResponse.Replace("\"", "");
            
            var value = double.Parse(cleanStr, CultureInfo.InvariantCulture);

            return decimal.Round((decimal)value, 2);
        }
        
        return null;
    }
}