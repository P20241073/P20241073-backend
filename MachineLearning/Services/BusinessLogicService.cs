using System.Threading.Tasks;
using MachineLearning.Domain.Services;

namespace MachineLearning.Services;

public class BusinessLogicService : IBusinessLogicService
{
    public async Task<ProcessResponseResult> ProcessResponseAsync(string responseContent)
    {
        var result = new ProcessResponseResult
        {
            Success = true,
            ProcessedContent = responseContent
        };

        return await Task.FromResult(result);
    }
}

public class ProcessResponseResult
{
    public bool Success { get; set; }
    public string ProcessedContent { get; set; }
}