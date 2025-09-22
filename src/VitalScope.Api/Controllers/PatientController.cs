using System.Net;
using Microsoft.AspNetCore.Mvc;
using VitalScope.Logic.Models.Input.Patient;
using VitalScope.Logic.Models.Output.Patient;
using VitalScope.Logic.Patient;

namespace VitalScope.Api.Controllers;

public class PatientController(IPatientService patientService) : BaseController
{
    [HttpPost("avatar/base64")]
    public async Task<IActionResult> AddFileAsync(IFormFile uploadedFile)
    {
        using var ms = new MemoryStream();
        await uploadedFile.CopyToAsync(ms);
        var bytes = ms.ToArray();

        return Ok(Convert.ToBase64String(bytes));
    }
    
    [HttpPost("create")]
    [ProducesResponseType(typeof(PatientResultModel), (int)HttpStatusCode.OK)]

    public async Task<IActionResult> AddAsync(PatientModel model, CancellationToken cancellationToken = default)
    {
        var result = await patientService.AddAsync(model, cancellationToken);
        
        return Ok(result);
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(PatientResultModel), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await patientService.GetAsync(id, cancellationToken);
        
        return Ok(result);
    }
    
    [HttpGet("all")]
    [ProducesResponseType(typeof(IEnumerable<PatientResultModel>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await patientService.GetAllAsync(cancellationToken);
        
        return Ok(result);
    }
    
    [HttpPut("update")]
    [ProducesResponseType(typeof(PatientResultModel), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateAsync(EditPatientModel model, CancellationToken cancellationToken = default)
    {
        var shipDto = await patientService.UpdateAsync(model, cancellationToken);
        
        return Ok(shipDto);
    }

    /// <summary>
    /// Delete ship by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("delete/{id:guid}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await patientService.DeleteAsync(id, cancellationToken);
        
        return Ok();
    }
}