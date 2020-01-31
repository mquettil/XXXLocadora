using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Cliente
    {
        public int ID { get; set; }
        public string Nome{ get; set; }
        public string CPF  { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento{ get; set; }
        public bool EhAtivo { get; set; }

        public Cliente()
        {

        }

        public Cliente(int iD, string nome, string cPF, string email, DateTime dataNascimento, bool ehAtivo)
        {
            ID = iD;
            Nome = nome;
            CPF = cPF;
            Email = email;
            DataNascimento = dataNascimento;
            EhAtivo = ehAtivo;
        }
    }
}
