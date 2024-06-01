using System.Diagnostics;
using Activities.Domain.Services;
using Activities.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions;
using Users.Domain.Model;
using Users.Domain.Services;
using Users.Resources;

namespace Users.Controllers;

[Produces("application/json")]
[ApiController]
[Route("/api/v1/devices/{deviceId}/activities")]

public class DeviceActivityController: ControllerBase
{
    private readonly IActivityService _activityService;
    private readonly IMapper _mapper;

    public DeviceActivityController(IActivityService activityService, IMapper mapper)
    {
        _activityService = activityService;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ActivityResource>), 200)]
    public async Task<ActionResult<IEnumerable<Activity>>> GetAllAsyncByDeviceId(int deviceId)
    {
        var activities = await _activityService.GetByDeviceIdAsync(deviceId);

        var resources = _mapper.Map<IEnumerable<Activity>, IEnumerable<ActivityResource>>(activities);

        return Ok(resources);
    }
}