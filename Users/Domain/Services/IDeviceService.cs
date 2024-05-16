using Users.Domain.Model;
using Users.Domain.Services.Communication;

namespace Users.Domain.Services;
public interface IDeviceService
{
    Task<IEnumerable<Device>> ListAsync();
    Task<DeviceResponse> GetByIdAsync(int id);
    Task<DeviceResponse> GetByUserIdAsync(int userId);
    Task<DeviceResponse> SaveAsync(Device device);
    Task<DeviceResponse> UpdateAsync(int id, Device device);
    Task<DeviceResponse> DeleteAsync(int id);
}