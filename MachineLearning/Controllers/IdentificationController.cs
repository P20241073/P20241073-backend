using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Reports.Domain.Model;
using Reports.Domain.Services;
using Reports.Resources;
using Shared.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Controllers;
[Route("api/identify-addiction")]
[ApiController]
public class IdentificationController : ControllerBase
{
    private readonly IReportService _reportService;
    private readonly IMapper _mapper;

    public IdentificationController(IReportService reportService, IMapper mapper)
    {
        _reportService = reportService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] MachineLearningDataResource resource)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var handler = new HttpClientHandler()
        {
            ClientCertificateOptions = ClientCertificateOption.Manual,
            ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) => { return true; }
        };
        using (var client = new HttpClient(handler))
        {
            // Request data goes here
            // The example below assumes JSON formatting which may be updated
            // depending on the format your endpoint expects.
            // More information can be found here:
            // https://docs.microsoft.com/azure/machine-learning/how-to-deploy-advanced-entry-script
            var requestBody = JsonConvert.SerializeObject(resource);

            // Replace this with the primary/secondary key, AMLToken, or Microsoft Entra ID token for the endpoint
            const string apiKey = "";
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new Exception("A key should be provided to invoke the endpoint");
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            client.BaseAddress = new Uri("");

            var content = new StringContent(requestBody);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await client.PostAsync("", content).ConfigureAwait(false);;

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Result: {0}", result);
                return Content(result, "application/json");
            }
            else
            {
                Console.WriteLine(string.Format("The request failed with status code: {0}", response.StatusCode));

                // Print the headers - they include the requert ID and the timestamp,
                // which are useful for debugging the failure
                Console.WriteLine(response.Headers.ToString());

                string responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseContent);
                return StatusCode((int)response.StatusCode, responseContent);
            }
        }
    }
}