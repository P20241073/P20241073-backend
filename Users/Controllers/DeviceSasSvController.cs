using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Reports.Domain.Model;
using Reports.Domain.Services;
using Reports.Resources;
namespace Users.Controllers;

[Produces("application/json")]
[ApiController]
[Route("/api/v1/devices/{deviceId}/sas-svs")]

public class DeviceSasSvController: ControllerBase
{
    private readonly ISasSvService _sasSvService;
    private readonly IMapper _mapper;

    public DeviceSasSvController(ISasSvService sasSvService, IMapper mapper)
    {
        _sasSvService = sasSvService;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<SasSvResource>), 200)]
    public async Task<ActionResult<IEnumerable<SasSv>>> GetAllAsyncByDeviceId(int deviceId)
    {
        var sasSvs = await _sasSvService.GetByDeviceIdAsync(deviceId);

        var resources = _mapper.Map<IEnumerable<SasSv>, IEnumerable<SasSvResource>>(sasSvs);

        return Ok(resources);
    }
}