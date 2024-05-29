using Reports.Domain.Model;
using Reports.Domain.Repositories;
using Shared.Persistence.Context;
using Shared.Persistence.Repositories;

namespace Reports.Persistence;

public class SurveyRepository : BaseRepository, ISurveyRepository
{
    public SurveyRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Survey>> ListAsync()
    {
        return await _context.Surveys.ToListAsync();
    }

    public async Task AddAsync(Survey survey)
    {
        await _context.Surveys.AddAsync(survey);
    }

    public async Task<Survey> FindByIdAsync(int id)
    {
        return await _context.Surveys.FindAsync(id);
    }

    public void Update(Survey survey)
    {
        _context.Surveys.Update(survey);
    }

    public void Remove(Survey survey)
    {
        _context.Surveys.Remove(survey);
    }

    public async Task<IEnumerable<Survey>> FindAllByDeviceIdAsync(int userId)
    {
        return await _context.Surveys.Where(s => s.DeviceId == userId).ToListAsync();
    }
}