using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchPowerPoint.Domain.DTOs.HomeDTO
{
    public class HomeResponseDTO
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = String.Empty;
    }
}
