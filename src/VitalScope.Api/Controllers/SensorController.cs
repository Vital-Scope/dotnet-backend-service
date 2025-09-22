using Microsoft.AspNetCore.Mvc;
using VitalScope.Logic.Models.Input.MainSensor;
using VitalScope.Logic.Models.Input.MetaSensor;
using VitalScope.Logic.Services.Study;

namespace VitalScope.Api.Controllers;

public sealed class SensorController : BaseController
{
    private readonly IStudyService _studyService;

    public SensorController(IStudyService studyService)
    {
        _studyService = studyService;
    }
    
    [HttpPost("add-meta")]
    public async Task<IActionResult> AddAsync(MetaSensorInputModel model, CancellationToken cancellationToken = default)
    {
       var result = await _studyService.AddMetaAsync(model, cancellationToken);
       
        return Ok(result);
    }
    
    [HttpPost("add-main")]
    public async Task<IActionResult> AddMainAsync(MainSensorInputModel model, CancellationToken cancellationToken = default)
    {
        await _studyService.AddMain(model, cancellationToken);
       
        return Ok();
    }
    
    [HttpGet("meta/{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _studyService.GetByIdAsync(id, cancellationToken);
        
        return Ok(result);
    }
    
    [HttpGet("meta-all")]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await _studyService.GetAllMetaAsync(cancellationToken);
        
        return Ok(result);
    }
    
    [HttpGet("main/{id:guid}")]
    public async Task<IActionResult> GetMainByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _studyService.GetByIdMainAsync(id, cancellationToken);
        
        return Ok(result);
    }
}