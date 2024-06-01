using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Reports.Domain.Model;
using Reports.Domain.Services;
using Reports.Resources;
namespace Users.Controllers;

[Produces("application/json")]
[ApiController]
[Route("/api/v1/devices/{deviceId}/reports")]

public class DeviceReportController: ControllerBase
{
    private readonly IReportService _reportService;
    private readonly IMapper _mapper;

    public DeviceReportController(IReportService reportService, IMapper mapper)
    {
        _reportService = reportService;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ReportResource>), 200)]
    public async Task<ActionResult<IEnumerable<Report>>> GetAllAsyncByDeviceId(int deviceId)
    {
        var reports = await _reportService.GetByDeviceIdAsync(deviceId);

        var resources = _mapper.Map<IEnumerable<Report>, IEnumerable<ReportResource>>(reports);

        return Ok(resources);
    }

}