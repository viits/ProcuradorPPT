using SearchPowerPoint.Domain.DTOs.GeminiDTO;
using SearchPowerPoint.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchPowerPoint.Application.PowerPoint
{
    public class PowerPointApplication
    {
        private readonly IPowerPointService _powerPointService;
        private readonly IGeminiService _geminiService;
        public PowerPointApplication(IPowerPointService powerPointService, IGeminiService geminiService)
        {
            _powerPointService = powerPointService;
            _geminiService = geminiService;
        }

        public async Task<string> SearchPPT(GeminiRequestDTO request,string message)
        {
            string mensagemRetorno = "";
            var ppt = _powerPointService.RetornaTextoPPT();
            string mensagem = $@"Caso encontre no texto ou similar ou estaja falando sobre a mensagem {message} me traz o titulo ou o texto que esta sendo abordado, mas apenas isso. O texto é {ppt}  caso nao encontre retorna não encontrado.";
            
            var mensagemGemini = await _geminiService.EnviarMensagem(request, mensagem);

            int indiceQuebraDeLinha = mensagemGemini.IndexOf('\n');
            if (indiceQuebraDeLinha != -1)
            {
                mensagemRetorno = _powerPointService.RetornaArquivo(mensagemGemini.Substring(0, indiceQuebraDeLinha));
                if (mensagemRetorno == "")
                {
                    mensagemRetorno = "Não encontrado.";
                }
            }
            else
            {
                mensagemRetorno = _powerPointService.RetornaArquivo(mensagemGemini);
                if (mensagemRetorno == "")
                {
                    mensagemRetorno = "Não encontrado.";
                }
            }
            return mensagemRetorno;
        }

    }
}
