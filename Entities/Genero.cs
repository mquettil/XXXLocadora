using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{

    //CQRS
    public class Genero
    {
        public int ID { get; set; }
        public string Nome { get; set; }

        public Genero()
        {

        }

        public Genero(int iD, string nome)
        {
            ID = iD;
            Nome = nome;
        }
    }
}
