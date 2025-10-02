using Microsoft.AspNetCore.Mvc;
using VitalScope.Logic.Models.Input.Session;
using VitalScope.Logic.Services.Session;

namespace VitalScope.Api.Controllers;

public sealed class SessionController : BaseController
{
    private readonly ISessionService _sessionService;

    public SessionController(ISessionService sessionService)
    {
        _sessionService = sessionService;
    }
    
    [HttpGet("active")]
    public async Task<IActionResult> GetActiveAsync(CancellationToken cancellationToken = default)
    {
        var result = await _sessionService.GetActiveSessionAsync(cancellationToken);
        
        return result is null ? NotFound() : Ok(result);
    }
    
    [HttpPatch("status")]
    public async Task<IActionResult> UpdateAsync(EditSessionInputModel model, CancellationToken cancellationToken = default)
    {
        var shipDto = await _sessionService.SetStatus(model, cancellationToken);
        
        return Ok(shipDto);
    }
}