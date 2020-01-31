using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer.Security
{
    public class User
    {
        //Como esta propriedade é estática, ela nunca morrerá em uma
        //aplicação Desktop.
        public static Funcionario FuncionarioLogado { get; set; }
    }
}
