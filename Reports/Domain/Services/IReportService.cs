using Reports.Domain.Model;
using Reports.Domain.Services.Communication;

namespace Reports.Domain.Services;
public interface IReportService
{
    Task<IEnumerable<Report>> ListAsync();
    Task<ReportResponse> GetByIdAsync(int id);
    Task<IEnumerable<Report>> GetByDeviceIdAsync(int deviceId);
    Task<ReportResponse> SaveAsync(Report report);
    Task<ReportResponse> UpdateAsync(int id, Report report);
    Task<ReportResponse> DeleteAsync(int id);
}