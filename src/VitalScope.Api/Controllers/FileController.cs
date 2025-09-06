
using System.Net;
using Microsoft.AspNetCore.Mvc;
using VitalScope.Logic.Services.Study;

namespace VitalScope.Api.Controllers;

public class FileController : BaseController
{
    private readonly IStudyService _studyService;

    public FileController(IStudyService studyService)
    {
        _studyService = studyService;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> CreateAsync(IFormFileCollection uploads, CancellationToken cancellationToken = default)
    {
        await _studyService.AddInformationsAsync(uploads, cancellationToken);
        
        return Ok();
    }
}