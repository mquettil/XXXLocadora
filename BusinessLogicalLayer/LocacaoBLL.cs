using DataAccessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BusinessLogicalLayer
{
    public class LocacaoBLL : ILocacaoService
    {
        private LocacaoDAL locacaoDAL = new LocacaoDAL();

        public Response EfetuarLocacao(Locacao locacao)
        {
            Response response = new Response();

            if (locacao.Filmes.Count == 0)
            {
                response.Erros.Add("Não é possível realizar a locação sem filmes.");
                response.Sucesso = false;
                return response;
            }

            TimeSpan ts = DateTime.Now.Subtract(locacao.Cliente.DataNascimento);
            //Calcula idade do cliente
            int idade = (int)(ts.TotalDays / 365);

            //Percorre todos os filmes locados a fim de encontrar algum que o cliente não possa ver
            foreach (Filme filme in locacao.Filmes)
            {
                if ((int)filme.Classificacao > idade)
                {
                    response
                   .Erros
                   .Add("A idade do cliente não corresponde com a classificação indicativa do filme " + filme.Nome);
                }
            }
            //Seta a data da locação com a data atual
            locacao.DataLocacao = DateTime.Now;
            locacao.DataPrevistaDevolucao = DateTime.Now;
            foreach (Filme filme in locacao.Filmes)
            {
                //Adiciona tempo na devolução de acordo com a data de lançamento 
                locacao.DataPrevistaDevolucao =
                    locacao.DataPrevistaDevolucao.AddHours(filme.CalcularDevolucao());

                //Adiciona os preços dos filmes 
                locacao.Preco += filme.CalcularPreco();
            }

            if (response.Erros.Count > 0)
            {
                response.Sucesso = false;
                return response;
            }


            //Necessário (dependendo da equipe que você estiver alocado)
            //Vai ao banco de dados com a finalidade de descobrir se o ID
            //do cliente associado a locação existe no banco de dados.
            //Cliente cliente = clienteBLL.GetClientByID(locacao.Cliente.ID);
            //if(cliente == null)
            //{
            //   response.Erros.Add("Cliente inexistente");
            //}

            //Utilizaremos aqui, o objeto TransactionScope para garantir que, tudo que 
            //esta entre as chaves rodará em uma transação onde OU TUDO FUNCIONA, OU NADA FUNCIONA.
            using (TransactionScope scope = new TransactionScope())
            {
                //Cadastra a locacao no banco de dados e ainda escreve
                //no objeto locacao o ID gerado pelo base.
                response = locacaoDAL.EfetuarLocacao(locacao);
                if (response.Sucesso)
                {
                    //Efetuar as inserções na tabela N para N
                    response = locacaoDAL.EfetuarLocacaoFilmes(locacao);
                    if (response.Sucesso)
                    {
                        //Se der certo, "commita" a operação.
                        //Chamar o Complete significa que deu tudo certo
                        scope.Complete();
                    }
                }
            }//Ao chegar no final das chaves, caso o complete não seja chamado, o c# reverterá
             //todas as operações em banco de dados efetuada dentro deste using
            return response;
        }
    }
}
