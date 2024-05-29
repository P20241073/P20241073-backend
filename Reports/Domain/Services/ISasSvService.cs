using Reports.Domain.Model;
using Reports.Domain.Services.Communication;

namespace Reports.Domain.Services;
public interface ISasSvService
{
    Task<IEnumerable<SasSv>> ListAsync();
    Task<SasSvResponse> GetByIdAsync(int id);
    Task<IEnumerable<SasSv>> GetByDeviceIdAsync(int deviceId);
    Task<SasSvResponse> SaveAsync(SasSv SasSv);
    Task<SasSvResponse> UpdateAsync(int id, SasSv SasSv);
    Task<SasSvResponse> DeleteAsync(int id);
}