using Activities.Domain.Services.Communication;
using Reports.Domain.Model;
using Reports.Domain.Repositories;
using Reports.Domain.Services;
using Reports.Domain.Services.Communication;
using Shared.Domain.Repositories;

namespace Reports.Services;
public class SasSvService : ISasSvService
{
    private readonly ISasSvRepository _sasSvRepository;
    private readonly IUnitOfWork _unitOfWork;
    public SasSvService(ISasSvRepository sasSvRepository, IUnitOfWork unitOfWork)
    {
        _sasSvRepository = sasSvRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<SasSv>> ListAsync()
    {
        return await _sasSvRepository.ListAsync();
    }

    public async Task<SasSvResponse> GetByIdAsync(int id)
    {
        var existingSasSv = await _sasSvRepository.FindByIdAsync(id);

        if (existingSasSv == null)
        {
            return new SasSvResponse("Activity not found.");
        }

        return new SasSvResponse(existingSasSv);
    }

    public async Task<IEnumerable<SasSv>> GetByDeviceIdAsync(int deviceId)
    {
        return await _sasSvRepository.FindAllByDeviceIdAsync(deviceId);
    }

    public async Task<SasSvResponse> SaveAsync(SasSv sasSv)
    {
        try
        {
            await _sasSvRepository.AddAsync(sasSv);
            await _unitOfWork.CompleteAsync();

            return new SasSvResponse(sasSv);
        }
        catch (Exception ex)
        {
            return new SasSvResponse($"An error occurred when saving the activity: {ex.Message}");
        }
    }

    public async Task<SasSvResponse> UpdateAsync(int id, SasSv sasSv)
    {
        var existingSasSv = await _sasSvRepository.FindByIdAsync(id);

        if (existingSasSv == null)
        {
            return new SasSvResponse("Activity not found.");
        }
        try
        {
            _sasSvRepository.Update(existingSasSv);
            await _unitOfWork.CompleteAsync();

            return new SasSvResponse(existingSasSv);
        }
        catch (Exception ex)
        {
            return new SasSvResponse($"An error occurred when updating the activity: {ex.Message}");
        }
    }

    public async Task<SasSvResponse> DeleteAsync(int id)
    {
        var existingSasSv= await _sasSvRepository.FindByIdAsync(id);

        if (existingSasSv == null)
        {
            return new SasSvResponse("Activity not found.");
        }

        try
        {
            _sasSvRepository.Remove(existingSasSv);
            await _unitOfWork.CompleteAsync();

            return new SasSvResponse(existingSasSv);
        }
        catch (Exception ex)
        {
            return new SasSvResponse($"An error occurred when deleting the activity: {ex.Message}");
        }
    }

}