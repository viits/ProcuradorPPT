using Microsoft.AspNetCore.Mvc;
using SearchPowerPoint.Domain.DTOs.HomeDTO;
using SearchPowerPoint.Domain.DTOs.PowerPointDTO;
using SearchPowerPoint.Models;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace SearchPowerPoint.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
        }

        public async Task<IActionResult> Index()
        {
            var responseDTO = new HomeResponseDTO();
            return View("Index",responseDTO);
        }
        public async Task<IActionResult> EnviarMensagem(string mensagemGemini)
        {
            var responseDTO = new HomeResponseDTO();
            try
            {
                string mensagem = "Gostaria de saber sobre incoterms";
                var requestDTO = new PowerPointRequestDTO()
                {
                    Message = mensagemGemini,
                };

                var url = "https://localhost:7292/api/PowerPoint";
                var content = new StringContent(JsonSerializer.Serialize(requestDTO), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(url, content);
                if(response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    responseDTO = new HomeResponseDTO()
                    {
                        StatusCode = 200,
                        Message = jsonResponse
                    };
                }
                return View("Index", responseDTO);
            }
            catch (Exception ex) {
                responseDTO.StatusCode = 500;
                responseDTO.Message = ex.Message;
                return View("Index", responseDTO);
            }
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
