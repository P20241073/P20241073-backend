using Activities.Domain.Services.Communication;
using Reports.Domain.Model;
using Reports.Domain.Repositories;
using Reports.Domain.Services;
using Reports.Domain.Services.Communication;
using Shared.Domain.Repositories;

namespace Reports.Services;
public class ReportService : IReportService
{
    private readonly IReportRepository _reportRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ReportService(IReportRepository reportRepository, IUnitOfWork unitOfWork)
    {
        _reportRepository = reportRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Report>> ListAsync()
    {
        return await _reportRepository.ListAsync();
    }

    public async Task<ReportResponse> GetByIdAsync(int id)
    {
        var existingReport = await _reportRepository.FindByIdAsync(id);

        if (existingReport == null)
            return new ReportResponse("Report not found");

        return new ReportResponse(existingReport);
    }

    public async Task<IEnumerable<Report>> GetByDeviceIdAsync(int deviceId)
    {
        return await _reportRepository.FindAllByDeviceIdAsync(deviceId);
    }

    public async Task<ReportResponse> SaveAsync(Report report)
    {
        try
        {
            await _reportRepository.AddAsync(report);
            await _unitOfWork.CompleteAsync();

            return new ReportResponse(report);
        }
        catch (Exception ex)
        {
            // Do some logging stuff
            return new ReportResponse($"An error occurred when saving the report: {ex.Message}");
        }
    }

    public async Task<ReportResponse> UpdateAsync(int id, Report report)
    {
        var existingReport = await _reportRepository.FindByIdAsync(id);

        if (existingReport == null)
            return new ReportResponse("Report not found");

        existingReport.AverageTimeUsedPerDay = report.AverageTimeUsedPerDay;
        existingReport.MostUsedApp = report.MostUsedApp;
        existingReport.UsesSocialMedia = report.UsesSocialMedia;

        try
        {
            _reportRepository.Update(existingReport);
            await _unitOfWork.CompleteAsync();

            return new ReportResponse(existingReport);
        }
        catch (Exception ex)
        {
            // Do some logging stuff
            return new ReportResponse($"An error occurred when updating the report: {ex.Message}");
        }
    }

    public async Task<ReportResponse> DeleteAsync(int id)
    {
        var existingReport = await _reportRepository.FindByIdAsync(id);

        if (existingReport == null)
            return new ReportResponse("Report not found");

        try
        {
            _reportRepository.Remove(existingReport);
            await _unitOfWork.CompleteAsync();

            return new ReportResponse(existingReport);
        }
        catch (Exception ex)
        {
            return new ReportResponse($"An error occurred when deleting the report: {ex.Message}");
        }
    }
}