using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public interface IFuncionarioService
    {
        DataResponse<Funcionario> Autenticar(string email, string senha);
    }
}
