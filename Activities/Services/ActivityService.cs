using Activites.Domain.Model;
using Activites.Domain.Repositories;
using Activities.Domain.Services;
using Activities.Domain.Services.Communication;
using Shared.Domain.Repositories;

namespace Activites.Services;
public class ActivityService : IActivityService
{
    private readonly IActivityRepository _activityRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ActivityService(IActivityRepository activityRepository, IUnitOfWork unitOfWork)
    {
        _activityRepository = activityRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Activity>> ListAsync()
    {
        return await _activityRepository.ListAsync();
    }

    public async Task<ActivityResponse> GetByIdAsync(int id)
    {
        var existingActivity = await _activityRepository.FindByIdAsync(id);

        if (existingActivity == null)
        {
            return new ActivityResponse("Activity not found.");
        }

        return new ActivityResponse(existingActivity);
    }

    public async Task<IEnumerable<Activity>> GetByDeviceIdAsync(int deviceId)
    {
        return await _activityRepository.FindAllByDeviceIdAsync(deviceId);
    }

    public async Task<ActivityResponse> SaveAsync(Activity activity)
    {
        try
        {
            await _activityRepository.AddAsync(activity);
            await _unitOfWork.CompleteAsync();

            return new ActivityResponse(activity);
        }
        catch (Exception ex)
        {
            return new ActivityResponse($"An error occurred when saving the activity: {ex.Message}");
        }
    }

    public async Task<ActivityResponse> UpdateAsync(int id, Activity activity)
    {
        var existingActivity = await _activityRepository.FindByIdAsync(id);

        if (existingActivity == null)
        {
            return new ActivityResponse("Activity not found.");
        }

        existingActivity.AppName = activity.AppName;
        existingActivity.TotalTimeUsedPerDay = activity.TotalTimeUsedPerDay;
        existingActivity.DateReported = activity.DateReported;
        existingActivity.TotalNotifications = activity.TotalNotifications;
        existingActivity.DeviceId = activity.DeviceId;

        try
        {
            _activityRepository.Update(existingActivity);
            await _unitOfWork.CompleteAsync();

            return new ActivityResponse(existingActivity);
        }
        catch (Exception ex)
        {
            return new ActivityResponse($"An error occurred when updating the activity: {ex.Message}");
        }
    }

    public async Task<ActivityResponse> DeleteAsync(int id)
    {
        var existingActivity = await _activityRepository.FindByIdAsync(id);

        if (existingActivity == null)
        {
            return new ActivityResponse("Activity not found.");
        }

        try
        {
            _activityRepository.Remove(existingActivity);
            await _unitOfWork.CompleteAsync();

            return new ActivityResponse(existingActivity);
        }
        catch (Exception ex)
        {
            return new ActivityResponse($"An error occurred when deleting the activity: {ex.Message}");
        }
    }
}
