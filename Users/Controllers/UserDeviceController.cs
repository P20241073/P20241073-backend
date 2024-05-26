using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions;
using Users.Domain.Model;
using Users.Domain.Services;
using Users.Resources;

namespace Users.Controllers;

[Produces("application/json")]
[ApiController]
[Route("/api/v1/users/{userId}/devices")]

public class UserDeviceController: ControllerBase
{
    private readonly IDeviceService _deviceService;
    private readonly IMapper _mapper;

    public UserDeviceController(IDeviceService deviceService, IMapper mapper)
    {
        _deviceService = deviceService;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<DeviceResource>), 200)]
    public async Task<ActionResult<IEnumerable<Device>>> GetAllAsyncByUserId([FromQuery] int userId)
    {
        var devices = await _deviceService.GetByUserIdAsync(userId);

        var resources = _mapper.Map<IEnumerable<Device>, IEnumerable<DeviceResource>>(devices);

        return Ok(resources);
    }
}
