using MachineLearning.Domain.Services;
using MachineLearning.Resources;
using MachineLearning.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MachineLearning.Controllers;

[ApiController]
[Route("[controller]")]
public class IdentificationController : ControllerBase
{
    private readonly HttpClientService _httpClientService;
    private readonly IBusinessLogicService _businessLogicService;

    public IdentificationController(HttpClientService httpClientService, IBusinessLogicService businessLogicService)
    {
        _httpClientService = httpClientService;
        _businessLogicService = businessLogicService;
    }

    [HttpPost]
    public async Task<IActionResult> Identify([FromBody] SaveMachineLearningResource resource)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            string responseContent = await _httpClientService.PostAsync("/score", resource);

            var processResult = await _businessLogicService.ProcessResponseAsync(responseContent);

            if (processResult.Success)
            {
                return Ok(processResult.ProcessedContent);
            }
            else
            {
                // Handle the case where processing was not successful.
                return StatusCode(500, "An error occurred while processing the response.");
            }
        }
        catch (Exception ex)
        {
            // Log the exception details.
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }
}