using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Locacao 
    {
        public Locacao()
        {
            this.Filmes = new List<Filme>();
        }

        public int ID { get; set; }
        public Cliente Cliente { get; set; }
        public Funcionario Funcionario { get; set; }
        public double Preco { get; set; }
        public double Multa { get; set; }
        public DateTime DataLocacao { get; set; }
        public DateTime DataPrevistaDevolucao { get; set; }
        public DateTime? DataDevolucao { get; set; }
        public List<Filme> Filmes { get; set; }
        public bool FoiPago { get; set; }

       
    }
}
