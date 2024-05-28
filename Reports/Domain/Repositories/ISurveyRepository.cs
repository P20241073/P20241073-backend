using Reports.Domain.Model;

namespace Reports.Domain.Repositories;

public interface ISurveyRepository
{
    Task<IEnumerable<Survey>> ListAsync();
    Task AddAsync(Survey device);
    Task<Survey> FindByIdAsync(int id);
    Task<IEnumerable<Survey>> FindAllByDeviceIdAsync(int userId);
    void Update(Survey device);
    void Remove(Survey device);
}