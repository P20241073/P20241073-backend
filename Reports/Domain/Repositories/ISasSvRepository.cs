using Reports.Domain.Model;

namespace Reports.Domain.Repositories;

public interface ISasSvRepository
{
    Task<IEnumerable<SasSv>> ListAsync();
    Task AddAsync(SasSv sasSv);
    Task<SasSv> FindByIdAsync(int id);
    Task<IEnumerable<SasSv>> FindAllByDeviceIdAsync(int deviceId);
    void Update(SasSv sasSv);
    void Remove(SasSv sasSv);
}