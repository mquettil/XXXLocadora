using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Funcionario
    {
        public int ID  { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }
        public string Senha { get; set; }
        public bool EhAtivo { get; set; }

        public Funcionario()
        {

        }

        public Funcionario(int iD, string nome, string email, string cPF, DateTime dataNascimento, string telefone, string senha, bool ehAtivo)
        {
            ID = iD;
            Nome = nome ?? throw new ArgumentNullException(nameof(nome));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            CPF = cPF ?? throw new ArgumentNullException(nameof(cPF));
            DataNascimento = dataNascimento;
            Telefone = telefone ?? throw new ArgumentNullException(nameof(telefone));
            Senha = senha ?? throw new ArgumentNullException(nameof(senha));
            EhAtivo = ehAtivo;
        }
    }
}
