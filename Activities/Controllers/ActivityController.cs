using Activities.Domain.Model;
using Activities.Domain.Services;
using Activities.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions;
using Users.Resources;

namespace Activities.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ActivityController : ControllerBase
{
    private readonly IActivityService _activityService;
    private readonly IMapper _mapper;

    public ActivityController(IActivityService activityService, IMapper mapper)
    {
        _activityService = activityService;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ActivityResource>), 200)]
    public async Task<IEnumerable<ActivityResource>> GetActivities()
    {
        var activities = await _activityService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Activity>, IEnumerable<ActivityResource>>(activities);
        
        return resources;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ActivityResource), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    public async Task<ActionResult> GetByIdAsync(int id)
    {
        var result = await _activityService.GetByIdAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var activityResult = _mapper.Map<Activity, ActivityResource>(result.Resource);

        return Ok(activityResult);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ActivityResource), 201)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> PostAsync([FromBody] SaveActivityResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var activity = _mapper.Map<SaveActivityResource, Activity>(resource);

        var result = await _activityService.SaveAsync(activity);

        if (!result.Success)
            return StatusCode(500, result.Message);

        var activityResource = _mapper.Map<Activity, ActivityResource>(result.Resource);

        return Ok(activityResource);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ActivityResource), 200)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveActivityResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var activity = _mapper.Map<SaveActivityResource, Activity>(resource);

        var result = await _activityService.UpdateAsync(id, activity);

        if (!result.Success)
            return BadRequest(result.Message);

        var activityResource = _mapper.Map<Activity, ActivityResource>(result.Resource);

        return Ok(activityResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _activityService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var activityResource = _mapper.Map<Activity, ActivityResource>(result.Resource);

        return Ok(activityResource);
    }
}