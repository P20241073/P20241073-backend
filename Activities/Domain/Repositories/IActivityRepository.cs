using Activites.Domain.Model;

namespace Activites.Domain.Repositories;

public interface IActivityRepository
{
    Task<IEnumerable<Activity>> ListAsync();
    Task AddAsync(Activity activity);
    Task<Activity> FindByIdAsync(int id);
    Task<IEnumerable<Activity>> FindAllByDeviceIdAsync(int deviceId);
    void Update(Activity activity);
    void Remove(Activity activity);
}