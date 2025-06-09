using SearchPowerPoint.Domain.DTOs.GeminiDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchPowerPoint.Domain.Interface
{
    public interface IGeminiService
    {
        Task<string> EnviarMensagem(GeminiRequestDTO request, string message);
    }
}
