using Activites.Domain.Model;
using Activites.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Shared.Persistence.Context;
using Shared.Persistence.Repositories;

namespace Activities.Persistence;

public class ActivityRepository : BaseRepository, IActivityRepository
{
    public ActivityRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Activity>> ListAsync()
    {
        return await _context.Activities.ToListAsync();
    }

    public async Task AddAsync(Activity activity)
    {
        await _context.Activities.AddAsync(activity);
    }

    public async Task<Activity> FindByIdAsync(int id)
    {
        return await _context.Activities.FindAsync(id);
    }

    public async Task<IEnumerable<Activity>> FindAllByDeviceIdAsync(int deviceId)
    {
        return await _context.Activities.Where(a => a.DeviceId == deviceId).ToListAsync();
    }

    public void Update(Activity activity)
    {
        _context.Activities.Update(activity);
    }

    public void Remove(Activity activity)
    {
        _context.Activities.Remove(activity);
    }
}