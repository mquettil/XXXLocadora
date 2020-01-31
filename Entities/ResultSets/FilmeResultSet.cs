using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ResultSets
{
    /// <summary>
    /// Representa o retorno de uma operação InnerJoin do Gênero com o Filme
    /// </summary>
    public class FilmeResultSet
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Genero { get; set; }
        public Classificacao Classificacao { get; set; }
    }
}
