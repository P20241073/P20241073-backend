using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MachineLearning.Resources;
using Newtonsoft.Json;
namespace MachineLearning.Services;
public class HttpClientService
{
    private readonly HttpClient _client;
    private readonly IConfiguration _configuration;
    public HttpClientService(IConfiguration configuration)
    {
        _configuration = configuration;
        _client = new HttpClient
        {
            BaseAddress = new Uri(configuration["AzureMachineLearningModelService:API"])
        };
        InitializeClient();
    }

    private void InitializeClient()
    {
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        
        string apiKey = _configuration["AzureMachineLearningModelService:Key"];
        if (string.IsNullOrEmpty(apiKey))
        {
            throw new Exception("API key is required");
        }
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
    }

    public async Task<string> PostAsync(string requestUri, SaveMachineLearningResource resource)
    {
        var requestBody = JsonConvert.SerializeObject(resource);
        var content = new StringContent(requestBody);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        HttpResponseMessage response = await _client.PostAsync(requestUri, content).ConfigureAwait(false);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }
        else
        {
            // Consider logging the error or throwing an exception
            throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
        }
    }
}