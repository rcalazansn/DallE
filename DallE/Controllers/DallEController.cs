using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace DallE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DallEController : ControllerBase
    {
        private readonly ILogger<DallEController> _logger;

        public DallEController(ILogger<DallEController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult> GenerateImg(string query)
        {
            try
            {
                string openaiApiKey = "";

                var requestBody = new RequestBody(query);

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var requestBodyJson = JsonSerializer.Serialize(requestBody, options);

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {openaiApiKey}");

                    var response = await httpClient.PostAsync("https://api.openai.com/v1/images/generations",
                        new StringContent(requestBodyJson, Encoding.UTF8, Application.Json));

                    string responseBody = await response.Content.ReadAsStringAsync();
                    
                    _logger.LogDebug(responseBody);
                    
                    if (response.IsSuccessStatusCode)
                    {
                        var responseObject = JsonSerializer.Deserialize<object>(responseBody);

                        return Ok(responseObject);
                    }
                    else
                    {
                        return BadRequest(response.StatusCode);
                    }
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao gerar a imagem: {ex.Message}");
            }
        }
    }
}
