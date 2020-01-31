using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using DataAccessLayer;
using Entities.ResultSets;
using Entities.Enums;

namespace BusinessLogicalLayer
{
    public class FilmeBLL : IEntityCRUD<Filme>, IFilmeService
    {
        private FilmeDAL filmeDAL = new FilmeDAL();

        public Response Delete(int id)
        {
            Response response = new Response();
            if (id<= 0)
            {
                response.Erros.Add("ID do filme não foi informado.");
            }
            if (response.Erros.Count != 0)
            {
                response.Sucesso = false;
                return response;
            }
            return filmeDAL.Delete(id);
        }

        public DataResponse<Filme> GetByID(int id)
        {
            return filmeDAL.GetByID(id);
        }

        public DataResponse<Filme> GetData()
        {
            return filmeDAL.GetData();
        }

        public DataResponse<FilmeResultSet> GetFilmes()
        {
            return filmeDAL.GetFilmes();
        }

        public DataResponse<FilmeResultSet> GetFilmesByClassificacao(Classificacao classificacao)
        {
            return filmeDAL.GetFilmesByClassificacao(classificacao);
        }

        public DataResponse<FilmeResultSet> GetFilmesByGenero(int genero)
        {
            if (genero <= 0)
            {
                DataResponse<FilmeResultSet> response = new DataResponse<FilmeResultSet>();
                response.Sucesso = false;
                response.Erros.Add("Gênero deve ser informado.");
                return response;
            }
            return filmeDAL.GetFilmesByGenero(genero);
        }

        public DataResponse<FilmeResultSet> GetFilmesByName(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                DataResponse<FilmeResultSet> response = new DataResponse<FilmeResultSet>();
                response.Sucesso = false;
                response.Erros.Add("Nome deve ser informado.");
                return response;
            }
            nome = nome.Trim();
            return filmeDAL.GetFilmesByName(nome);
        }

        public Response Insert(Filme item)
        {
            Response response = Validate(item);
            //TODO: Verificar a existência desse gênero na base de dados
            //generoBLL.LerID(item.GeneroID);

            //Verifica se tem erros!
            if (response.Erros.Count != 0)
            {
                response.Sucesso = false;
                return response;
            }
            return filmeDAL.Insert(item);
        }
        public Response Update(Filme item)
        {
           Response response = Validate(item);
            //TODO: Verificar a existência desse gênero na base de dados
            //generoBLL.LerID(item.GeneroID);
            //Verifica se tem erros!
            if (response.Erros.Count != 0)
            {
                response.Sucesso = false;
                return response;
            }
            return filmeDAL.Update(item);
        }

        private Response Validate(Filme item)
        {
            Response response = new Response();

            if (item.Duracao <= 10)
            {
                response.Erros.Add("Duração não pode ser menor que 10 minutos.");
            }

            if (item.DataLancamento == DateTime.MinValue
                                    ||
                item.DataLancamento > DateTime.Now)
            {
                response.Erros.Add("Data inválida.");
            }

            return response;
        }
    }
}
