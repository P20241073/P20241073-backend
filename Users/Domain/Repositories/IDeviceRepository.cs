using Users.Domain.Model;

namespace Users.Domain.Repositories;

public interface IDeviceRepository
{
    Task<IEnumerable<Device>> ListAsync();
    Task AddAsync(Device device);
    Task<Device> FindByIdAsync(int id);
    Task<Device> FindByUserIdAsync(int userId);
    void Update(Device device);
    void Remove(Device device);
}