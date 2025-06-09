using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchPowerPoint.Domain.DTOs.GeminiDTO
{
    public class GeminiRequestDTO
    {
        public string URL { get; set; } = String.Empty;
        public string Key { get; set; } = String.Empty;
    }
}
