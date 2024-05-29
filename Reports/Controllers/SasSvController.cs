using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Reports.Domain.Model;
using Reports.Domain.Services;
using Reports.Resources;
using Shared.Extensions;

namespace Reports.Controllers;
[Route("api/[controller]")]
[ApiController]
public class SasSvController : ControllerBase
{
    private readonly ISasSvService _sasSvService;
    private readonly IMapper _mapper;

    public SasSvController(ISasSvService sasSvService, IMapper mapper)
    {
        _sasSvService = sasSvService;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<SasSvResource>), 200)]
    public async Task<IEnumerable<SasSvResource>> GetSasSvs()
    {
        var sasSvs = await _sasSvService.ListAsync();
        var resources = _mapper.Map<IEnumerable<SasSv>, IEnumerable<SasSvResource>>(sasSvs);

        return resources;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(SasSvResource), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    public async Task<ActionResult> GetByIdAsync(int id)
    {
        var result = await _sasSvService.GetByIdAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var sasSvResult = _mapper.Map<SasSv, SasSvResource>(result.Resource);

        return Ok(sasSvResult);
    }

    [HttpPost]
    [ProducesResponseType(typeof(SasSvResource), 201)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> PostAsync([FromBody] SaveSasSvResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var sasSv = _mapper.Map<SaveSasSvResource, SasSv>(resource);

        var result = await _sasSvService.SaveAsync(sasSv);

        if (!result.Success)
            return StatusCode(500, result.Message);

        var sasSvResource = _mapper.Map<SasSv, SasSvResource>(result.Resource);

        return Ok(sasSvResource);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(SasSvResource), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveSasSvResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var sasSv = _mapper.Map<SaveSasSvResource, SasSv>(resource);
        var result = await _sasSvService.UpdateAsync(id, sasSv);

        if (!result.Success)
            return BadRequest(result.Message);

        var sasSvResource = _mapper.Map<SasSv, SasSvResource>(result.Resource);

        return Ok(sasSvResource);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(SasSvResource), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _sasSvService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var sasSvResource = _mapper.Map<SasSv, SasSvResource>(result.Resource);

        return Ok(sasSvResource);
    }
}