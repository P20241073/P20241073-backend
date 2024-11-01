using MachineLearning.Services;

namespace MachineLearning.Domain.Services;
public interface IBusinessLogicService
{
    Task<ProcessResponseResult> ProcessResponseAsync(string responseContent);
}