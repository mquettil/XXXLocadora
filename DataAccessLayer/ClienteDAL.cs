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
    public class ClienteDAL : IEntityCRUD<Cliente>
    {
        /// <summary>
        /// Método que insere o gênero em uma base sql server.
        /// Este objeto gênero chamado de "item", é criado na 
        /// camada PresentationLayer (Windows Forms) e validado na
        /// camada BusinessLogicalLayer (BLL) até chegar a este ponto,
        /// prontinho para ser cadastrado no banco!
        /// </summary>
        /// <param name="item"></param>
        public Response Insert(Cliente item)
        {

            //Objeto que se conecta a bases SQLSERVER
            SqlConnection connection = new SqlConnection();

            //Vincula a connection string ao objeto que gerencia conexões 
            connection.ConnectionString = SqlData.ConnectionString;

            //Objeto que realiza comandos em bases SQLSERVR
            SqlCommand command = new SqlCommand();

            //Define a query a ser executada no banco utilizando Sql Parameters
            //Ainda faz retornar o ID gerado pelo GENRES
            command.CommandText = "INSERT INTO CLIENTES (NOME,CPF,EMAIL,DATANASCIMENTO,EHATIVO) VALUES (@NOME,@CPF,@EMAIL,@DATANASCIMENTO,@EHATIVO); select scope_identity()";

            //Define o valor do parâmetro @NAME, neste caso, vincula com o texto
            //da caixa de texto da categoria
            command.Parameters.AddWithValue(@"NOME", item.Nome);
            command.Parameters.AddWithValue(@"CPF", item.CPF);
            command.Parameters.AddWithValue(@"EMAIL", item.Email);
            command.Parameters.AddWithValue(@"DATANASCIMENTO", item.DataNascimento);
            command.Parameters.AddWithValue(@"EHATIVO", item.EhAtivo);


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

                if (ex.Message.Contains("UQ_CLI_CPF"))
                {
                    response.Erros.Add("CPF já cadastrado!");
                }
                else if(ex.Message.Contains("UQ_CLI_EMAIL"))
                {
                    response.Erros.Add("Email já cadastrado!");
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

        public Response Update(Cliente item)
        {
            throw new NotImplementedException();
        }

        public Response Delete(int id)
        {
            throw new NotImplementedException();
        }

        public DataResponse<Cliente> GetData()
        {

            //Objeto que se conecta a bases SQLSERVER
            SqlConnection connection = new SqlConnection();

            //Vincula a connection string ao objeto que gerencia conexões 
            connection.ConnectionString = SqlData.ConnectionString;

            //Objeto que realiza comandos em bases SQLSERVR
            SqlCommand command = new SqlCommand();

            command.CommandText = "SELECT * FROM CLIENTES";
            command.Connection = connection;

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                List<Cliente> clientes = new List<Cliente>();

                //Enquanto houver registros, leia!
                while (reader.Read())
                {
                    //Exemplo utilizando um cast, veloz, porém perigoso
                    //em caso de migração de base
                    //string nome = (string)reader["NAME"];
                    //Criando um gênero pra representar o registro no banco
                    Cliente c = new Cliente(Convert.ToInt32(reader["ID"]),
                                       (string)reader["NOME"],
                                       (string)reader["CPF"],
                                       (string)reader["EMAIL"],
                                       (DateTime)reader["DATANASCIMENTO"],
                                       (bool)reader["EHATIVO"]);

                    //Adicionando o gênero na lista criada acima.
                    clientes.Add(c);
                }
                DataResponse<Cliente> response = new DataResponse<Cliente>();
                response.Sucesso = true;
                response.Data = clientes;
                return response;
            }
            catch (Exception ex)
            {
                //Logar o erro pro adm do sistema ter acesso.
                File.WriteAllText("log.txt", ex.Message);
                DataResponse<Cliente> response = new DataResponse<Cliente>();
                response.Sucesso = false;
                response.Erros.Add("Falha ao acessar o banco de dados, contate o suporte.");
                return response;
            }
            finally
            {
                connection.Dispose();
            }
        }

        public DataResponse<Cliente> GetByID(int id)
        {
            throw new NotImplementedException();
        }
    }
}
