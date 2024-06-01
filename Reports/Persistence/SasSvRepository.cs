using Microsoft.EntityFrameworkCore;
using Reports.Domain.Model;
using Reports.Domain.Repositories;
using Shared.Persistence.Context;
using Shared.Persistence.Repositories;

namespace Reports.Persistence;

public class SasSvRepository : BaseRepository, ISasSvRepository
{
    public SasSvRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<SasSv>> ListAsync()
    {
        return await _context.SasSvs.ToListAsync();
    }

    public async Task AddAsync(SasSv sasSv)
    {
        await _context.SasSvs.AddAsync(sasSv);
    }

    public async Task<SasSv> FindByIdAsync(int id)
    {
        return await _context.SasSvs.FindAsync(id);
    }

    public void Update(SasSv sasSv)
    {
        _context.SasSvs.Update(sasSv);
    }

    public void Remove(SasSv sasSv)
    {
        _context.SasSvs.Remove(sasSv);
    }

    public async Task<IEnumerable<SasSv>> FindAllByDeviceIdAsync(int deviceId)
    {
        return await _context.SasSvs.Where(s => s.DeviceId == deviceId).ToListAsync();
    }
}