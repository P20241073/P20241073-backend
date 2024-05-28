using Reports.Domain.Model;
using Reports.Domain.Services.Communication;

namespace Reports.Domain.Services;
public interface ISurveyService
{
    Task<IEnumerable<Survey>> ListAsync();
    Task<SurveyResponse> GetByIdAsync(int id);
    Task<IEnumerable<Survey>> GetByDeviceIdAsync(int userId);
    Task<SurveyResponse> SaveAsync(Survey survey);
    Task<SurveyResponse> UpdateAsync(int id, Survey survey);
    Task<SurveyResponse> DeleteAsync(int id);
}