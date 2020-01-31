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
    public class FuncionarioDAL : IEntityCRUD<Funcionario>, IFuncionarioService
    {
        public DataResponse<Funcionario> Autenticar(string email, string senha)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = SqlData.ConnectionString;
            SqlCommand command = new SqlCommand();
            command.Connection = connection;

            command.CommandText = "SELECT * FROM FUNCIONARIOS WHERE EMAIL = @EMAIL AND SENHA = @SENHA";
            command.Parameters.AddWithValue("@EMAIL", email);
            command.Parameters.AddWithValue("@SENHA", senha);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                DataResponse<Funcionario> response = new DataResponse<Funcionario>();
                if (reader.Read())
                {
                    //Se entrou aqui, encontramos o usuario no banco!
                    Funcionario funcionario = new Funcionario();
                    funcionario.ID = (int)reader["ID"];
                    funcionario.Nome = (string)reader["NOME"];
                    funcionario.CPF = (string)reader["CPF"];
                    funcionario.Email = email;
                    funcionario.DataNascimento= (DateTime)reader["DATANASCIMENTO"];
                    funcionario.Telefone = (string)reader["TELEFONE"];
                    List<Funcionario> funcionarios = new List<Funcionario>();
                    funcionarios.Add(funcionario);
                    response.Data = funcionarios;
                    response.Sucesso = true;
                    return response;
                }
                //Se chegou aqui, usuário digitou email/senha inválidos
                response.Erros.Add("Email e/ou senha inválidos");
                response.Sucesso = false;
                return response;
            }
            catch (Exception ex)
            {
                DataResponse<Funcionario> response = new DataResponse<Funcionario>();
                response.Sucesso = false;
                response.Erros.Add("Erro no banco de dados contate o administrador");
                return response;
            }
            finally
            {
                connection.Dispose();
            }

        }

        public Response Delete(int id)
        {
            throw new NotImplementedException();
        }

        public DataResponse<Funcionario> GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public DataResponse<Funcionario> GetData()
        {
            throw new NotImplementedException();
        }

        public Response Insert(Funcionario item)
        {
            //Objeto que se conecta a bases SQLSERVER
            SqlConnection connection = new SqlConnection();

            //Vincula a connection string ao objeto que gerencia conexões 
            connection.ConnectionString = SqlData.ConnectionString;

            //Objeto que realiza comandos em bases SQLSERVR
            SqlCommand command = new SqlCommand();

            //Define a query a ser executada no banco utilizando Sql Parameters
            //Ainda faz retornar o ID gerado pelo GENRES
            command.CommandText = "INSERT INTO FUNCIONARIOS (NOME,CPF,TELEFONE,SENHA,EMAIL,DATANASCIMENTO,EHATIVO) VALUES (@NOME,@CPF,@TELEFONE,@SENHA,@EMAIL,@DATANASCIMENTO,@EHATIVO); select scope_identity()";

            //Define o valor do parâmetro @NAME, neste caso, vincula com o texto
            //da caixa de texto da categoria
            command.Parameters.AddWithValue(@"NOME", item.Nome);
            command.Parameters.AddWithValue(@"CPF", item.CPF);
            command.Parameters.AddWithValue(@"TELEFONE", item.Telefone);
            command.Parameters.AddWithValue(@"EMAIL", item.Email);
            command.Parameters.AddWithValue(@"SENHA", item.Senha);
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
                else if (ex.Message.Contains("UQ_CLI_EMAIL"))
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

        public Response Update(Funcionario item)
        {
            throw new NotImplementedException();
        }
    }
}
