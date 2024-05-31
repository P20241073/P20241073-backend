using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Reports.Domain.Model;
using Reports.Domain.Services;
using Reports.Resources;
using Shared.Extensions;

namespace Reports.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ReportController : ControllerBase
{
    private readonly IReportService _reportService;
    private readonly IMapper _mapper;

    public ReportController(IReportService reportService, IMapper mapper)
    {
        _reportService = reportService;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ReportResource>), 200)]
    public async Task<IEnumerable<ReportResource>> GetReports()
    {
        var reports = await _reportService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Report>, IEnumerable<ReportResource>>(reports);

        return resources;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ReportResource), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    public async Task<ActionResult> GetByIdAsync(int id)
    {
        var result = await _reportService.GetByIdAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var reportResult = _mapper.Map<Report, ReportResource>(result.Resource);

        return Ok(reportResult);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ReportResource), 201)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> PostAsync([FromBody] SaveReportResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var report = _mapper.Map<SaveReportResource, Report>(resource);

        var result = await _reportService.SaveAsync(report);

        if (!result.Success)
            return StatusCode(500, result.Message);

        var reportResource = _mapper.Map<Report, ReportResource>(result.Resource);

        return Ok(reportResource);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ReportResource), 200)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveReportResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var report = _mapper.Map<SaveReportResource, Report>(resource);
        var result = await _reportService.UpdateAsync(id, report);

        if (!result.Success)
            return StatusCode(500, result.Message);

        var reportResource = _mapper.Map<Report, ReportResource>(result.Resource);

        return Ok(reportResource);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ReportResource), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _reportService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var reportResource = _mapper.Map<Report, ReportResource>(result.Resource);

        return Ok(reportResource);
    }
}