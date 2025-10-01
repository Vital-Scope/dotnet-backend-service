using Microsoft.AspNetCore.Mvc;
using VitalScope.Logic.Services;

namespace VitalScope.Api.Controllers;

public sealed class PredictController : BaseController
{
    private readonly IMLService _service;

    public PredictController(IMLService service)
    {
        _service = service;
    }

    [HttpGet("{monitoringId:guid}")]
    public async Task<IActionResult> GetHealthAsync(Guid monitoringId, CancellationToken cancellationToken = default)
    {
        var result = await _service.CalculateRiskAsync(monitoringId, cancellationToken);
        
        return Ok(result);
    }
}