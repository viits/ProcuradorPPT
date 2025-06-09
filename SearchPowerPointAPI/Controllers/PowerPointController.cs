using DocumentFormat.OpenXml.Packaging;
using Microsoft.AspNetCore.Mvc;
using SearchPowerPoint.Application.PowerPoint;
using SearchPowerPoint.Domain.DTOs.GeminiDTO;
using SearchPowerPoint.Domain.DTOs.PowerPointDTO;
using SearchPowerPoint.Domain.Interface;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace SearchPowerPointAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PowerPointController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly PowerPointApplication _powerPointApplication;
        public PowerPointController(IConfiguration configuration, PowerPointApplication powerPointApplication)
        {
            _configuration = configuration;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _powerPointApplication = powerPointApplication;
        }


        [HttpPost]
        public async Task<IActionResult> SearchPowerPoint([FromBody] PowerPointRequestDTO request)
        {
            try
            {
                var key = _configuration.GetSection("GeminiKey")?.Value;
                var urlGemini = _configuration.GetSection("URLGemini")?.Value;
                if (key == null || urlGemini == null)
                {
                    return NotFound("Chave ou url não encontrado.");
                }
                var geminiDTO = new GeminiRequestDTO()
                {
                    Key = key,
                    URL = urlGemini,
                };
                var ppt = await _powerPointApplication.SearchPPT(geminiDTO, request.Message);
                return Ok(ppt);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
