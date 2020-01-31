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
    public class GeneroDAL : IEntityCRUD<Genero>
    {
        /// <summary>
        /// Método que insere o gênero em uma base sql server.
        /// Este objeto gênero chamado de "item", é criado na 
        /// camada PresentationLayer (Windows Forms) e validado na
        /// camada BusinessLogicalLayer (BLL) até chegar a este ponto,
        /// prontinho para ser cadastrado no banco!
        /// </summary>
        /// <param name="item"></param>
        public Response Insert(Genero item)
        {
            //Objeto que se conecta a bases SQLSERVER
            SqlConnection connection = new SqlConnection();

            //Vincula a connection string ao objeto que gerencia conexões 
            connection.ConnectionString = SqlData.ConnectionString;

            //Objeto que realiza comandos em bases SQLSERVR
            SqlCommand command = new SqlCommand();

            //Define a query a ser executada no banco utilizando Sql Parameters
            //Ainda faz retornar o ID gerado pelo GENRES
            command.CommandText = "INSERT INTO GENEROS (NOME) VALUES (@NOME); select scope_identity()";

            //Define o valor do parâmetro @NAME, neste caso, vincula com o texto
            //da caixa de texto da categoria
            command.Parameters.AddWithValue(@"NOME", item.Nome);

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

                if (ex.Message.Contains("UQ_GENEROS_NOME"))
                {
                    response.Erros.Add("Gênero já cadastrado!");
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

        public Response Update(Genero item)
        {
            throw new NotImplementedException();
        }

        public Response Delete(int id)
        {
            throw new NotImplementedException();
        }

        public DataResponse<Genero> GetData()
        {
            //Objeto que se conecta a bases SQLSERVER
            SqlConnection connection = new SqlConnection();

            //Vincula a connection string ao objeto que gerencia conexões 
            connection.ConnectionString = SqlData.ConnectionString;

            //Objeto que realiza comandos em bases SQLSERVR
            SqlCommand command = new SqlCommand();

            command.CommandText = "SELECT * FROM GENEROS";
            command.Connection = connection;

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                
                List<Genero> generos = new List<Genero>();

                //Enquanto houver registros, leia!
                while (reader.Read())
                {
                    //Exemplo utilizando um cast, veloz, porém perigoso
                    //em caso de migração de base
                    //string nome = (string)reader["NAME"];
                    int id = Convert.ToInt32(reader["ID"]);
                    string nome = Convert.ToString(reader["NOME"]);
                    //Criando um gênero pra representar o registro no banco
                    Genero g = new Genero(id, nome);
                    //Adicionando o gênero na lista criada acima.
                    generos.Add(g);
                }
                DataResponse<Genero> response = new DataResponse<Genero>();
                response.Sucesso = true;
                response.Data = generos;
                return response;
            }
            catch (Exception ex)
            {
                //Logar o erro pro adm do sistema ter acesso.
                File.WriteAllText("log.txt", ex.Message);
                DataResponse<Genero> response = new DataResponse<Genero>();
                response.Sucesso = false;
                response.Erros.Add("Falha ao acessar o banco de dados, contate o suporte.");
                return response;
            }
            finally
            {
                connection.Dispose();
            }
        }

        public DataResponse<Genero> GetByID(int id)
        {
            throw new NotImplementedException();
        }
    }
}
