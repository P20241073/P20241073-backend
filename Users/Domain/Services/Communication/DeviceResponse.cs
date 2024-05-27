using Shared.Domain.Services.Communication;
using Users.Domain.Model;

namespace Users.Domain.Services.Communication;

public class DeviceResponse : BaseResponse<Device>
{
    public DeviceResponse(Device resource) : base(resource) { }

    public DeviceResponse(string message) : base(message) { }
}