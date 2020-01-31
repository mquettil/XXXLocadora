using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class LocacaoDAL : ILocacaoService
    {
        public Response EfetuarLocacao(Locacao locacao)
        {
            string connectionString = SqlData.ConnectionString;
            //I/O -> Input/Output
            //Arquivos
            //Conexões com banco
            //Network (comunicação com qualquer hardware externo)
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = SqlData.ConnectionString;
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                command.CommandText = @"INSERT INTO LOCACOES VALUES 
                                               (@CLIENTE,@FUNCIONARIO,
                                                @PRECO,@DATALOCACAO,
                                                @DATAPREVISTADEVOLUCAO,
                                                @DATADEVOLUCAO,@MULTA,
                                                @FOIPAGO); 
                                         SELECT SCOPE_IDENTITY()";

                command.Parameters.AddWithValue("@CLIENTE", locacao.Cliente.ID);
                command.Parameters.AddWithValue("@FUNCIONARIO", locacao.Funcionario.ID);
                command.Parameters.AddWithValue("@PRECO", locacao.Preco);
                command.Parameters.AddWithValue("@DATALOCACAO", locacao.DataLocacao);
                command.Parameters.AddWithValue("@DATAPREVISTADEVOLUCAO", locacao.DataPrevistaDevolucao);
                command.Parameters.AddWithValue("@DATADEVOLUCAO", DBNull.Value);
                command.Parameters.AddWithValue("@MULTA", locacao.Multa);
                command.Parameters.AddWithValue("@FOIPAGO", locacao.FoiPago);
                try
                {
                    connection.Open();
                    int idGerado = Convert.ToInt32(command.ExecuteScalar());
                    locacao.ID = idGerado;
                    Response response = new Response();
                    response.Sucesso = true;
                    return response;
                }
                catch (Exception ex)
                {
                    Response response = new Response();
                    response.Sucesso = false;
                    if (ex.Message.Contains("FK_LOCACOES_CLIENTES"))
                    {
                        response.Erros.Add("Cliente inexistente.");
                    }
                    else if (ex.Message.Contains("FK_LOCACOES_FUNCIONARIOS"))
                    {
                        response.Erros.Add("Funcionario inexistente.");
                    }
                    else
                    {
                        response.Erros.Add("Erro no banco de dados, contate o adm.");
                        File.WriteAllText("log.txt", ex.Message + " - " + ex.StackTrace);
                    }
                    return response;
                }
            }//connection.Dispose(); //Conexão fechada automaticamente
        }

        public Response EfetuarLocacaoFilmes(Locacao locacao)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = SqlData.ConnectionString;

            using (connection)
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                command.CommandText = "INSERT INTO LOCACOES_FILMES VALUES (@LOCACAOID,@FILMEID)";
                try
                {
                    connection.Open();
                    foreach (Filme filme in locacao.Filmes)
                    {
                        command.Parameters.AddWithValue("@LOCACAOID", locacao.ID);
                        command.Parameters.AddWithValue("@FILMEID", filme.ID);
                        command.ExecuteScalar();
                        command.Parameters.Clear();
                    }
                    Response response = new Response();
                    response.Sucesso = true;
                    return response;
                }
                catch (Exception ex)
                {
                    Response response = new Response();
                    response.Sucesso = false;
                    response.Erros.Add("Erro no banco de dados contate o administrador.");
                    File.WriteAllText("log.txt", ex.Message + " " + ex.StackTrace);
                    return response;
                }
            }
        }
    }
}