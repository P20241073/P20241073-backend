using Shared.Domain.Repositories;
using Users.Domain.Model;
using Users.Domain.Repositories;
using Users.Domain.Services;
using Users.Domain.Services.Communication;

namespace Users.Services;
public class DeviceService : IDeviceService
{
    private readonly IDeviceRepository _deviceRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeviceService(IDeviceRepository deviceRepository, IUnitOfWork unitOfWork)
    {
        _deviceRepository = deviceRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Device>> ListAsync()
    {
        return await _deviceRepository.ListAsync();
    }

    public async Task<DeviceResponse> SaveAsync(Device device)
    {
        try
        {
            await _deviceRepository.AddAsync(device);
            await _unitOfWork.CompleteAsync();

            return new DeviceResponse(device);
        }
        catch (Exception ex)
        {
            return new DeviceResponse($"An error occurred when saving the device: {ex.Message}");
        }
    }

    public async Task<DeviceResponse> UpdateAsync(int id, Device device)
    {
        var existingDevice = await _deviceRepository.FindByIdAsync(id);

        if (existingDevice == null)
        {
            return new DeviceResponse("Device not found.");
        }

        existingDevice.Name = device.Name;
        existingDevice.Info = device.Info;
        existingDevice.UserType = device.UserType;
        existingDevice.UserId = device.UserId;

        try
        {
            _deviceRepository.Update(existingDevice);
            await _unitOfWork.CompleteAsync();

            return new DeviceResponse(existingDevice);
        }
        catch (Exception ex)
        {
            return new DeviceResponse($"An error occurred when updating the device: {ex.Message}");
        }
    }

    public async Task<DeviceResponse> DeleteAsync(int id)
    {
        var existingDevice = await _deviceRepository.FindByIdAsync(id);

        if (existingDevice == null)
        {
            return new DeviceResponse("Device not found.");
        }

        try
        {
            _deviceRepository.Remove(existingDevice);
            await _unitOfWork.CompleteAsync();

            return new DeviceResponse(existingDevice);
        }
        catch (Exception ex)
        {
            return new DeviceResponse($"An error occurred when deleting the device: {ex.Message}");
        }
    }

    public async Task<DeviceResponse> GetByIdAsync(int id)
    {
        var existingDevice = await _deviceRepository.FindByIdAsync(id);

        if (existingDevice == null)
            return new DeviceResponse("Device not found.");

        return new DeviceResponse(existingDevice);
    }

    public async Task<DeviceResponse> GetByUserIdAsync(int userId)
    {
        var existingDevice = await _deviceRepository.FindByUserIdAsync(userId);

        if (existingDevice == null)
            return new DeviceResponse("Device not found.");

        return new DeviceResponse(existingDevice);
    }
}