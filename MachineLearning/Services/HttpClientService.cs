using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MachineLearning.Resources;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MachineLearning.Services
{
    public class HttpClientService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        private readonly ILogger<HttpClientService> _logger;

        public HttpClientService(IConfiguration configuration, ILogger<HttpClientService> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            string apiUrl = configuration["AzureMachineLearningModelService:API"];
            if (string.IsNullOrEmpty(apiUrl))
            {
                _logger.LogError("API URL is not configured");
                throw new ArgumentNullException("API URL is not configured");
            }

            _client = new HttpClient
            {
                BaseAddress = new Uri(apiUrl)
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
                _logger.LogError("API key is required");
                throw new Exception("API key is required");
            }
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        }

        public async Task<string> PostAsync(string requestUri, SaveMachineLearningResource resource)
        {
            var requestBody = JsonConvert.SerializeObject(resource);
            var content = new StringContent(requestBody);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response;
            try
            {
                response = await _client.PostAsync(requestUri, content).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while sending HTTP request");
                throw;
            }

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                _logger.LogError($"Request failed with status code: {response.StatusCode}");
                throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
            }
        }
    }
}