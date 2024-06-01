using Microsoft.EntityFrameworkCore;
using Reports.Domain.Model;
using Reports.Domain.Repositories;
using Shared.Persistence.Context;
using Shared.Persistence.Repositories;

namespace Reports.Persistence;

public class ReportRepository : BaseRepository, IReportRepository
{
    public ReportRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Report>> ListAsync()
    {
        return await _context.Reports.ToListAsync();
    }

    public async Task AddAsync(Report report)
    {
        await _context.Reports.AddAsync(report);
    }

    public async Task<Report> FindByIdAsync(int id)
    {
        return await _context.Reports.FindAsync(id);
    }

    public void Update(Report report)
    {
        _context.Reports.Update(report);
    }

    public void Remove(Report report)
    {
        _context.Reports.Remove(report);
    }

    public async Task<IEnumerable<Report>> FindAllByDeviceIdAsync(int deviceId)
    {
        return await _context.Reports.Where(r => r.DeviceId == deviceId).ToListAsync();
    }
}