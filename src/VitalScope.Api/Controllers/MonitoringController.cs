using System.Net;
using Microsoft.AspNetCore.Mvc;
using VitalScope.Logic.Models.Input.MainSensor;
using VitalScope.Logic.Models.Input.MetaSensor;
using VitalScope.Logic.Models.Output.MetaSensor;
using VitalScope.Logic.Services.Study;

namespace VitalScope.Api.Controllers;

public sealed class MonitoringController : BaseController
{
    private readonly IStudyService _studyService;

    public MonitoringController(IStudyService studyService)
    {
        _studyService = studyService;
    }
    
    [HttpPost]
    public async Task<IActionResult> AddAsync(MetaSensorInputModel model, CancellationToken cancellationToken = default)
    {
       var result = await _studyService.AddMetaAsync(model, cancellationToken);
       
        return Ok(result);
    }
    
    [HttpPost("sensor")]
    public async Task<IActionResult> AddMainAsync(MainSensorInputModel model, CancellationToken cancellationToken = default)
    {
        await _studyService.AddMain(model, cancellationToken);
       
        return Ok();
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _studyService.GetByIdAsync(id, cancellationToken);
        
        return Ok(result);
    }
    
    [HttpGet("all")]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await _studyService.GetAllMetaAsync(cancellationToken);
        
        return Ok(result);
    }
    
    [HttpGet("sensor/{monitoringId:guid}")]
    public async Task<IActionResult> GetMainByIdAsync(Guid monitoringId, CancellationToken cancellationToken = default)
    {
        var result = await _studyService.GetByIdMainAsync(monitoringId, cancellationToken);
        
        return Ok(result);
    }
    
    [HttpPut("update")]
    [ProducesResponseType(typeof(MetaSensorOutputModel), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateAsync(EditMetaSensorInputModel model, CancellationToken cancellationToken = default)
    {
        var shipDto = await _studyService.EditMetaAsync(model, cancellationToken);
        
        return Ok(shipDto);
    }
}