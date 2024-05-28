using Activities.Domain.Model;
using Activities.Domain.Services.Communication;

namespace Activities.Domain.Services;
public interface IActivityService
{
    Task<IEnumerable<Activity>> ListAsync();
    Task<ActivityResponse> GetByIdAsync(int id);
    Task<IEnumerable<Activity>> GetByDeviceIdAsync(int deviceId);
    Task<ActivityResponse> SaveAsync(Activity activity);
    Task<ActivityResponse> UpdateAsync(int id, Activity activity);
    Task<ActivityResponse> DeleteAsync(int id);
}