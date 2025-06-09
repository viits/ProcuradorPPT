using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchPowerPoint.Domain.Interface
{
    public interface IPowerPointService
    {
        string RetornaTextoPPT();
        string RetornaArquivo(string mensagem);
    }
}
