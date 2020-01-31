using Entities.Enums;
using Entities.ResultSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public interface IFilmeService
    {
        DataResponse<FilmeResultSet> GetFilmes();
        DataResponse<FilmeResultSet> GetFilmesByName(string nome);
        DataResponse<FilmeResultSet> GetFilmesByGenero(int genero);
        DataResponse<FilmeResultSet> GetFilmesByClassificacao(Classificacao classificacao);
    }
}
