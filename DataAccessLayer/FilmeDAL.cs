using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Data.SqlClient;
using System.IO;
using Entities.Enums;
using Entities.ResultSets;

namespace DataAccessLayer
{
    public class FilmeDAL : IEntityCRUD<Filme>, IFilmeService
    {

        public DataResponse<Filme> GetData()
        {
            //Objeto que se conecta a bases SQLSERVER
            SqlConnection connection = new SqlConnection();

            //Vincula a connection string ao objeto que gerencia conexões 
            connection.ConnectionString = SqlData.ConnectionString;

            //Objeto que realiza comandos em bases SQLSERVR
            SqlCommand command = new SqlCommand();

            command.CommandText = "SELECT * FROM FILMES";
            command.Connection = connection;

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                List<Filme> filmes = new List<Filme>();

                //Enquanto houver registros, leia!
                while (reader.Read())
                {
                    //Exemplo utilizando um cast, veloz, porém perigoso
                    //em caso de migração de base
                    //string nome = (string)reader["NAME"];
                    //Criando um gênero pra representar o registro no banco
                    Filme f = new Filme(Convert.ToInt32(reader["ID"]),
                                       (string)reader["NOME"],
                                       (DateTime)reader["DATALANCAMENTO"],
                                       (Classificacao)reader["CLASSIFICACAO"],
                                       (int)reader["DURACAO"],
                                       (int)reader["GENEROID"]);

                    //Adicionando o gênero na lista criada acima.
                    filmes.Add(f);
                }
                DataResponse<Filme> response = new DataResponse<Filme>();
                response.Sucesso = true;
                response.Data = filmes;
                return response;
            }
            catch (Exception ex)
            {
                //Logar o erro pro adm do sistema ter acesso.
                File.WriteAllText("log.txt", ex.Message);
                DataResponse<Filme> response = new DataResponse<Filme>();
                response.Sucesso = false;
                response.Erros.Add("Falha ao acessar o banco de dados, contate o suporte.");
                return response;
            }
            finally
            {
                connection.Dispose();
            }
        }

        public DataResponse<FilmeResultSet> GetFilmes()
        {
            //Objeto que se conecta a bases SQLSERVER
            SqlConnection connection = new SqlConnection();

            //Vincula a connection string ao objeto que gerencia conexões 
            connection.ConnectionString = SqlData.ConnectionString;

            //Objeto que realiza comandos em bases SQLSERVR
            SqlCommand command = new SqlCommand();

            command.CommandText = @"SELECT F.ID,
                                           F.NOME,
                                           G.NOME AS 'GENERO',
                                           F.CLASSIFICACAO
                                    FROM FILMES F INNER JOIN 
                                    GENEROS G ON F.GENEROID = G.ID";

            command.Connection = connection;

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                List<FilmeResultSet> filmes = new List<FilmeResultSet>();

                //Enquanto houver registros, leia!
                while (reader.Read())
                {
                    //Exemplo utilizando um cast, veloz, porém perigoso
                    //em caso de migração de base
                    //string nome = (string)reader["NAME"];
                    //Criando um gênero pra representar o registro no banco
                    FilmeResultSet f = new FilmeResultSet();
                    f.ID = (int)reader[0];
                    f.Nome = (string)reader[1];
                    f.Genero = (string)reader[2];
                    f.Classificacao = (Classificacao)reader[3];

                    //Adicionando o gênero na lista criada acima.
                    filmes.Add(f);
                }
                DataResponse<FilmeResultSet> response = new DataResponse<FilmeResultSet>();
                response.Sucesso = true;
                response.Data = filmes;
                return response;
            }
            catch (Exception ex)
            {
                //Logar o erro pro adm do sistema ter acesso.
                File.WriteAllText("log.txt", ex.Message);
                DataResponse<FilmeResultSet> response = new DataResponse<FilmeResultSet>();
                response.Sucesso = false;
                response.Erros.Add("Falha ao acessar o banco de dados, contate o suporte.");
                return response;
            }
            finally
            {
                connection.Dispose();
            }
        }

        public DataResponse<FilmeResultSet> GetFilmesByClassificacao(Classificacao classificacao)
        {
            SqlConnection connection = new SqlConnection();

            //Vincula a connection string ao objeto que gerencia conexões 
            connection.ConnectionString = SqlData.ConnectionString;

            //Objeto que realiza comandos em bases SQLSERVR
            SqlCommand command = new SqlCommand();

            command.CommandText = @"SELECT F.ID,
                                           F.NOME,
                                           G.NOME AS 'GENERO',
                                           F.CLASSIFICACAO
                                    FROM FILMES F INNER JOIN 
                                    GENEROS G ON F.GENEROID = G.ID
                                    WHERE F.CLASSIFICACAO = @CLASSIFICACAO";
            command.Parameters.AddWithValue("@CLASSIFICACAO", classificacao);

            command.Connection = connection;

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                List<FilmeResultSet> filmes = new List<FilmeResultSet>();

                //Enquanto houver registros, leia!
                while (reader.Read())
                {
                    //Exemplo utilizando um cast, veloz, porém perigoso
                    //em caso de migração de base
                    //string nome = (string)reader["NAME"];
                    //Criando um gênero pra representar o registro no banco
                    FilmeResultSet f = new FilmeResultSet();
                    f.ID = (int)reader[0];
                    f.Nome = (string)reader[1];
                    f.Genero = (string)reader[2];
                    f.Classificacao = (Classificacao)reader[3];

                    //Adicionando o gênero na lista criada acima.
                    filmes.Add(f);
                }
                DataResponse<FilmeResultSet> response = new DataResponse<FilmeResultSet>();
                response.Sucesso = true;
                response.Data = filmes;
                return response;
            }
            catch (Exception ex)
            {
                //Logar o erro pro adm do sistema ter acesso.
                File.WriteAllText("log.txt", ex.Message);
                DataResponse<FilmeResultSet> response = new DataResponse<FilmeResultSet>();
                response.Sucesso = false;
                response.Erros.Add("Falha ao acessar o banco de dados, contate o suporte.");
                return response;
            }
            finally
            {
                connection.Dispose();
            }
        }

        public DataResponse<FilmeResultSet> GetFilmesByGenero(int genero)
        {
            SqlConnection connection = new SqlConnection();

            //Vincula a connection string ao objeto que gerencia conexões 
            connection.ConnectionString = SqlData.ConnectionString;

            //Objeto que realiza comandos em bases SQLSERVR
            SqlCommand command = new SqlCommand();

            command.CommandText = @"SELECT F.ID,
                                           F.NOME,
                                           G.NOME AS 'GENERO',
                                           F.CLASSIFICACAO
                                    FROM FILMES F INNER JOIN 
                                    GENEROS G ON F.GENEROID = G.ID
                                    WHERE G.ID = @ID";
            command.Parameters.AddWithValue("@ID", genero);

            command.Connection = connection;

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                List<FilmeResultSet> filmes = new List<FilmeResultSet>();

                //Enquanto houver registros, leia!
                while (reader.Read())
                {
                    //Exemplo utilizando um cast, veloz, porém perigoso
                    //em caso de migração de base
                    //string nome = (string)reader["NAME"];
                    //Criando um gênero pra representar o registro no banco
                    FilmeResultSet f = new FilmeResultSet();
                    f.ID = (int)reader[0];
                    f.Nome = (string)reader[1];
                    f.Genero = (string)reader[2];
                    f.Classificacao = (Classificacao)reader[3];

                    //Adicionando o gênero na lista criada acima.
                    filmes.Add(f);
                }
                DataResponse<FilmeResultSet> response = new DataResponse<FilmeResultSet>();
                response.Sucesso = true;
                response.Data = filmes;
                return response;
            }
            catch (Exception ex)
            {
                //Logar o erro pro adm do sistema ter acesso.
                File.WriteAllText("log.txt", ex.Message);
                DataResponse<FilmeResultSet> response = new DataResponse<FilmeResultSet>();
                response.Sucesso = false;
                response.Erros.Add("Falha ao acessar o banco de dados, contate o suporte.");
                return response;
            }
            finally
            {
                connection.Dispose();
            }
        }

        public DataResponse<FilmeResultSet> GetFilmesByName(string nome)
        {
            SqlConnection connection = new SqlConnection();

            //Vincula a connection string ao objeto que gerencia conexões 
            connection.ConnectionString = SqlData.ConnectionString;

            //Objeto que realiza comandos em bases SQLSERVR
            SqlCommand command = new SqlCommand();

            command.CommandText = @"SELECT F.ID,
                                           F.NOME,
                                           G.NOME AS 'GENERO',
                                           F.CLASSIFICACAO
                                    FROM FILMES F INNER JOIN 
                                    GENEROS G ON F.GENEROID = G.ID
                                    WHERE F.NOME LIKE @NOME";
            command.Parameters.AddWithValue("@NOME", "%" + nome + "%");

            command.Connection = connection;

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                List<FilmeResultSet> filmes = new List<FilmeResultSet>();

                //Enquanto houver registros, leia!
                while (reader.Read())
                {
                    //Exemplo utilizando um cast, veloz, porém perigoso
                    //em caso de migração de base
                    //string nome = (string)reader["NAME"];
                    //Criando um gênero pra representar o registro no banco
                    FilmeResultSet f = new FilmeResultSet();
                    f.ID = (int)reader[0];
                    f.Nome = (string)reader[1];
                    f.Genero = (string)reader[2];
                    f.Classificacao = (Classificacao)reader[3];

                    //Adicionando o gênero na lista criada acima.
                    filmes.Add(f);
                }
                DataResponse<FilmeResultSet> response = new DataResponse<FilmeResultSet>();
                response.Sucesso = true;
                response.Data = filmes;
                return response;
            }
            catch (Exception ex)
            {
                //Logar o erro pro adm do sistema ter acesso.
                File.WriteAllText("log.txt", ex.Message);
                DataResponse<FilmeResultSet> response = new DataResponse<FilmeResultSet>();
                response.Sucesso = false;
                response.Erros.Add("Falha ao acessar o banco de dados, contate o suporte.");
                return response;
            }
            finally
            {
                connection.Dispose();
            }
        }

        public Response Insert(Filme item)
        {
            //Objeto que se conecta a bases SQLSERVER
            SqlConnection connection = new SqlConnection();

            //Vincula a connection string ao objeto que gerencia conexões 
            connection.ConnectionString = SqlData.ConnectionString;

            //Objeto que realiza comandos em bases SQLSERVR
            SqlCommand command = new SqlCommand();

            //Define a query a ser executada no banco utilizando Sql Parameters
            //Ainda faz retornar o ID gerado pelo GENRES
            command.CommandText = "INSERT INTO FILMES VALUES (@NOME,@DATALANCAMENTO,@CLASSIFICACAO,@DURACAO,@GENEROID); select scope_identity()";

            //Define o valor do parâmetro @NAME, neste caso, vincula com o texto
            //da caixa de texto da categoria
            command.Parameters.AddWithValue(@"NOME", item.Nome);
            command.Parameters.AddWithValue(@"DATALANCAMENTO", item.DataLancamento);
            command.Parameters.AddWithValue(@"CLASSIFICACAO", item.Classificacao);
            command.Parameters.AddWithValue(@"DURACAO", item.Duracao);
            command.Parameters.AddWithValue(@"GENEROID", item.GeneroID);


            //Vincula o objeto que sabe ONDE realizar os comandos, com o objeto
            //que sabe O QUE fazer.
            command.Connection = connection;

            //Agora iremos ficar online no banco, ou seja, pode dar cagadinha :(
            //Por isso estamos envolvendo os comandos em um bloco try!
            //Caso erros aconteçam, o código será redirecionado ao bloco catch abaixo
            //para tratarmos estes erros não esperados!
            try
            {
                //A partir de agora, estamos conectados!
                //É nosso dever, executar o comando e fechar a conexão
                //o mais rápido possível!
                connection.Open();
                //Convert é DO CARALHO, pois ele converterá o que vier pela frente,
                //diferentemente do cast (int)command.ExecuteScalar();
                //O problema do cast é que ele deve ser preciso.
                //Neste caso, o valor da variável idGerado será o valor gerado
                //pelo banco na hora de cadastrar um novo gênero, definido pelo termo
                //'identity' que aprendemos na aula de banco.
                int idGerado = Convert.ToInt32(command.ExecuteScalar());

                //Criar o objeto que representa a resposta do banco!
                Response response = new Response();
                response.Sucesso = true;
                return response;

            }
            //Bloco de código para tratar erros!
            catch (Exception ex)
            {
                Response response = new Response();
                response.Sucesso = false;

                if (ex.Message.Contains("FK"))
                {
                    response.Erros.Add("Gênero não encontrado!");
                }
                else
                {
                    response.Erros.Add("Erro no banco de dados, contate o administrador!");
                    File.WriteAllText("log.txt", ex.Message);
                }
                return response;
            }
            //Executa sempre!
            finally
            {
                //Fecha a conexão com o banco de dados, apenas se ela está aberta.
                connection.Dispose();
            }
        }

        public Response Update(Filme item)
        {
            //Objeto que se conecta a bases SQLSERVER
            SqlConnection connection = new SqlConnection();

            //Vincula a connection string ao objeto que gerencia conexões 
            connection.ConnectionString = SqlData.ConnectionString;

            //Objeto que realiza comandos em bases SQLSERVR
            SqlCommand command = new SqlCommand();

            //Define a query a ser executada no banco utilizando Sql Parameters
            //Ainda faz retornar o ID gerado pelo GENRES
            command.CommandText = @"UPDATE FILMES SET NOME = @NOME,
                                                      DATALANCAMENTO = @DATALANCAMENTO, 
                                                      CLASSIFICACAO = @CLASSIFICACAO,
                                                      DURACAO = @DURACAO,
                                                      GENEROID = @GENEROID
                                    WHERE ID = @ID";

            //Define o valor do parâmetro @NAME, neste caso, vincula com o texto
            //da caixa de texto da categoria
            command.Parameters.AddWithValue(@"NOME", item.Nome);
            command.Parameters.AddWithValue(@"DATALANCAMENTO", item.DataLancamento);
            command.Parameters.AddWithValue(@"CLASSIFICACAO", item.Classificacao);
            command.Parameters.AddWithValue(@"DURACAO", item.Duracao);
            command.Parameters.AddWithValue(@"GENEROID", item.GeneroID);
            command.Parameters.AddWithValue(@"ID", item.ID);

            //Vincula o objeto que sabe ONDE realizar os comandos, com o objeto
            //que sabe O QUE fazer.
            command.Connection = connection;

            //Agora iremos ficar online no banco, ou seja, pode dar cagadinha :(
            //Por isso estamos envolvendo os comandos em um bloco try!
            //Caso erros aconteçam, o código será redirecionado ao bloco catch abaixo
            //para tratarmos estes erros não esperados!
            try
            {
                //A partir de agora, estamos conectados!
                //É nosso dever, executar o comando e fechar a conexão
                //o mais rápido possível!
                connection.Open();
                //Convert é DO CARALHO, pois ele converterá o que vier pela frente,
                //diferentemente do cast (int)command.ExecuteScalar();
                //O problema do cast é que ele deve ser preciso.
                //Neste caso, o valor da variável idGerado será o valor gerado
                //pelo banco na hora de cadastrar um novo gênero, definido pelo termo
                //'identity' que aprendemos na aula de banco.
                int nLinhasAfetadas = command.ExecuteNonQuery();
                //Criar o objeto que representa a resposta do banco!
                Response response = new Response();
                if (nLinhasAfetadas != 1)
                {
                    response.Sucesso = false;
                    response.Erros.Add("ID do cliente não informado.");
                    return response;
                }
                response.Sucesso = true;
                return response;

            }
            //Bloco de código para tratar erros!
            catch (Exception ex)
            {
                Response response = new Response();
                response.Sucesso = false;

                if (ex.Message.Contains("FK"))
                {
                    response.Erros.Add("Gênero não encontrado!");
                }
                else
                {
                    response.Erros.Add("Erro no banco de dados, contate o administrador!");
                    File.WriteAllText("log.txt", ex.Message);
                }
                return response;
            }
            //Executa sempre!
            finally
            {
                //Fecha a conexão com o banco de dados, apenas se ela está aberta.
                connection.Dispose();
            }
        }

        public Response Delete(int id)
        {
            //Objeto que se conecta a bases SQLSERVER
            SqlConnection connection = new SqlConnection();

            //Vincula a connection string ao objeto que gerencia conexões 
            connection.ConnectionString = SqlData.ConnectionString;

            //Objeto que realiza comandos em bases SQLSERVR
            SqlCommand command = new SqlCommand();

            //Define a query a ser executada no banco utilizando Sql Parameters
            //Ainda faz retornar o ID gerado pelo GENRES
            command.CommandText = @"DELETE FROM FILMES
                                    WHERE ID = @ID";

            //Define o valor do parâmetro @NAME, neste caso, vincula com o texto
            //da caixa de texto da categoria
            command.Parameters.AddWithValue(@"ID", id);

            //Vincula o objeto que sabe ONDE realizar os comandos, com o objeto
            //que sabe O QUE fazer.
            command.Connection = connection;

            //Agora iremos ficar online no banco, ou seja, pode dar cagadinha :(
            //Por isso estamos envolvendo os comandos em um bloco try!
            //Caso erros aconteçam, o código será redirecionado ao bloco catch abaixo
            //para tratarmos estes erros não esperados!
            try
            {
                //A partir de agora, estamos conectados!
                //É nosso dever, executar o comando e fechar a conexão
                //o mais rápido possível!
                connection.Open();
                //Convert é DO CARALHO, pois ele converterá o que vier pela frente,
                //diferentemente do cast (int)command.ExecuteScalar();
                //O problema do cast é que ele deve ser preciso.
                //Neste caso, o valor da variável idGerado será o valor gerado
                //pelo banco na hora de cadastrar um novo gênero, definido pelo termo
                //'identity' que aprendemos na aula de banco.
                int nLinhasAfetadas = command.ExecuteNonQuery();
                //Criar o objeto que representa a resposta do banco!
                Response response = new Response();
                if (nLinhasAfetadas != 1)
                {
                    response.Sucesso = false;
                    response.Erros.Add("ID do cliente não informado.");
                    return response;
                }
                response.Sucesso = true;
                return response;

            }
            //Bloco de código para tratar erros!
            catch (Exception ex)
            {
                Response response = new Response();
                response.Sucesso = false;

                if (ex.Message.Contains("LOCACOES_FILMES"))
                {
                    response.Erros.Add("Filme não pode ser excluído, pois há locações vinculadas a ele.");
                }
                else
                {
                    response.Erros.Add("Erro no banco de dados, contate o administrador!");
                    File.WriteAllText("log.txt", ex.Message);
                }
                return response;
            }
            //Executa sempre!
            finally
            {
                //Fecha a conexão com o banco de dados, apenas se ela está aberta.
                connection.Dispose();
            }

        }

        public DataResponse<Filme> GetByID(int id)
        {
            //Objeto que se conecta a bases SQLSERVER
            SqlConnection connection = new SqlConnection();

            //Vincula a connection string ao objeto que gerencia conexões 
            connection.ConnectionString = SqlData.ConnectionString;

            //Objeto que realiza comandos em bases SQLSERVR
            SqlCommand command = new SqlCommand();

            command.CommandText = "SELECT * FROM FILMES WHERE ID = @ID";
            command.Parameters.AddWithValue("@ID", id);
            command.Connection = connection;

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                List<Filme> filmes = new List<Filme>();

                //Se tiver registro, leia.
                if (reader.Read())
                {
                    //Exemplo utilizando um cast, veloz, porém perigoso
                    //em caso de migração de base
                    //string nome = (string)reader["NAME"];
                    //Criando um gênero pra representar o registro no banco
                    Filme f = new Filme(Convert.ToInt32(reader["ID"]),
                                       (string)reader["NOME"],
                                       (DateTime)reader["DATALANCAMENTO"],
                                       (Classificacao)reader["CLASSIFICACAO"],
                                       (int)reader["DURACAO"],
                                       (int)reader["GENEROID"]);

                    //Adicionando o gênero na lista criada acima.
                    filmes.Add(f);
                }
                DataResponse<Filme> response = new DataResponse<Filme>();
                response.Sucesso = true;
                response.Data = filmes;
                return response;
            }
            catch (Exception ex)
            {
                //Logar o erro pro adm do sistema ter acesso.
                File.WriteAllText("log.txt", ex.Message);
                DataResponse<Filme> response = new DataResponse<Filme>();
                response.Sucesso = false;
                response.Erros.Add("Falha ao acessar o banco de dados, contate o suporte.");
                return response;
            }
            finally
            {
                connection.Dispose();
            }
        }
    }
}
