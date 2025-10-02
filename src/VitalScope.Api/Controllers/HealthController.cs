using Microsoft.AspNetCore.Mvc;
using VitalScope.Logic.Services.Health;

namespace VitalScope.Api.Controllers;

public sealed class HealthController : BaseController
{
    private readonly IHealthService _healthService;

    public HealthController(IHealthService healthService)
    {
        _healthService = healthService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetHealthAsync(CancellationToken cancellationToken = default)
    {
        var result = await _healthService.CheckHealth(cancellationToken);
        
        return Ok(result);
    }
}