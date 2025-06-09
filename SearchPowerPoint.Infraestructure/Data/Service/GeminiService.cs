using Mscc.GenerativeAI;
using SearchPowerPoint.Domain.DTOs.GeminiDTO;
using SearchPowerPoint.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SearchPowerPoint.Infraestructure.Data.Service
{
    public class GeminiService : IGeminiService
    {
        private readonly HttpClient _httpClient;

        public GeminiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> EnviarMensagem(GeminiRequestDTO request,string message)
        {
            var requestData = new
            {
                contents = new[]
               {
                    new {
                        parts = new[]
                        {
                            new { text = message }
                        }
                    }
                }
            };
            var jsonRequest = JsonSerializer.Serialize(requestData);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var requestUri = $"{request.URL}?key={request.Key}";

            var response = await _httpClient.PostAsync(requestUri, content);
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                try
                {
                    using JsonDocument doc = JsonDocument.Parse(jsonResponse);
                    JsonElement candidatesElement = doc.RootElement.GetProperty("candidates");
                    if (candidatesElement.GetArrayLength() > 0)
                    {
                        JsonElement contentElement = candidatesElement[0].GetProperty("content");
                        JsonElement partsElement = contentElement.GetProperty("parts");
                        if (partsElement.GetArrayLength() > 0)
                        {
                            string teste = partsElement[0].GetProperty("text").GetString();
                            return teste;
                        }
                    }
                    return "Nenhuma resposta de texto encontrada.";
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"Erro ao desserializar a resposta: {ex.Message}");
                    throw new JsonException(ex.Message);
                }
            }
            else
            {
                Console.WriteLine($"Erro na requisição: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
                return $@"Erro - {response.StatusCode}";
            }
        } 
    }
}
