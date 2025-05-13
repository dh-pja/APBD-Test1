using Microsoft.AspNetCore.Mvc;
using TemplateWebApi.Repositories.Interfaces;

namespace TemplateWebApi.Controllers;

[ApiController]
[Route("api/samples")]
public class SampleController : ControllerBase
{
    private readonly ISampleRepository _sampleRepository;
    
    public SampleController(ISampleRepository sampleRepository)
    {
        _sampleRepository = sampleRepository;
    }
    
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> TestConnection([FromRoute] int id, CancellationToken ct)
    {
        var result = await _sampleRepository.GetSample(id, ct);
        if (result)
        {
            return Ok("Connection successful");
        }
        return StatusCode(StatusCodes.Status500InternalServerError, "Connection failed");
    }
}