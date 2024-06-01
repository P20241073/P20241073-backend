using Reports.Domain.Model;

namespace Reports.Domain.Repositories;

public interface IReportRepository
{
    Task<IEnumerable<Report>> ListAsync();
    Task AddAsync(Report report);
    Task<Report> FindByIdAsync(int id);
    Task<IEnumerable<Report>> FindAllByDeviceIdAsync(int deviceId);
    void Update(Report report);
    void Remove(Report report);
}