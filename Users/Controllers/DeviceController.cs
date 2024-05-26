using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions;
using Users.Domain.Model;
using Users.Domain.Services;
using Users.Resources;

namespace Users.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DeviceController : ControllerBase
{
    private readonly IDeviceService _deviceService;
    private readonly IMapper _mapper;

    public DeviceController(IDeviceService deviceService, IMapper mapper)
    {
        _deviceService = deviceService;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<DeviceResource>), 200)]
    public async Task<IEnumerable<DeviceResource>> GetDevices()
    {
        var devices = await _deviceService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Device>, IEnumerable<DeviceResource>>(devices);
        
        return resources;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(DeviceResource), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    public async Task<ActionResult> GetByIdAsync(int id)
    {
        var result = await _deviceService.GetByIdAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var deviceResult = _mapper.Map<Device, DeviceResource>(result.Resource);

        return Ok(deviceResult);
    }

    [HttpPost]
    [ProducesResponseType(typeof(DeviceResource), 201)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> PostAsync([FromBody] SaveDeviceResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var device = _mapper.Map<SaveDeviceResource, Device>(resource);

        var result = await _deviceService.SaveAsync(device);

        if (!result.Success)
            return BadRequest(result.Message);

        var deviceResource = _mapper.Map<Device, DeviceResource>(result.Resource);

        return Ok(deviceResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveDeviceResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
        var device = _mapper.Map<SaveDeviceResource, Device>(resource);

        var result = await _deviceService.UpdateAsync(id, device);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var deviceResource = _mapper.Map<Device, DeviceResource>(result.Resource);

        return Ok(deviceResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _deviceService.DeleteAsync(id);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var deviceResource = _mapper.Map<Device, DeviceResource>(result.Resource);

        return Ok(deviceResource);
    }
}