using Microsoft.EntityFrameworkCore;
using Shared.Persistence.Context;
using Shared.Persistence.Repositories;
using Users.Domain.Model;
using Users.Domain.Repositories;

namespace Users.Persistence
{
    public class DeviceRepository : BaseRepository, IDeviceRepository
    {
        public DeviceRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Device>> ListAsync()
        {
            return await _context.Devices.ToListAsync();
        }

        public async Task AddAsync(Device device)
        {
            await _context.Devices.AddAsync(device);
        }

        public async Task<Device> FindByIdAsync(int id)
        {
            return await _context.Devices.FindAsync(id);
        }

        public async Task<Device> FindByUserIdAsync(int userId)
        {
            return await _context.Devices.FirstOrDefaultAsync(d => d.UserId == userId);
        }

        public void Update(Device device)
        {
            _context.Devices.Update(device);
        }

        public void Remove(Device device)
        {
            _context.Devices.Remove(device);
        }
    }
}