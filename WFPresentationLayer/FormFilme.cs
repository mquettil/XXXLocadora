using BusinessLogicalLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entities.Enums;
using Entities;
using Entities.ResultSets;

namespace WFPresentationLayer
{
    public partial class FormFilme : Form
    {
        public FormFilme()
        {
            InitializeComponent();
        }

        private GeneroBLL generoBLL = new GeneroBLL();
        private FilmeBLL filmeBLL = new FilmeBLL();
        private int idFilmeASerAtualizadoExcluido = 0;

        private void FormFilme_Load(object sender, EventArgs e)
        {
            cmbGeneros.DataSource = generoBLL.GetData().Data;//<- Este .Data retorna uma List<Genero>
            cmbGeneros.DisplayMember = "Nome";
            cmbGeneros.ValueMember = "ID";
            cmbClassificacao.DataSource = Enum.GetValues(typeof(Classificacao));

            dataGridView1.DataSource = filmeBLL.GetFilmes().Data;
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            //Criar uma instância do objeto que representa a interface gráfica.
            //Em aplicações WEB, geralmente aqui se criaria um objeto chamado
            //FilmeInsertViewModel e seria necessário o converter para "FIlme" antes
            //de jogá-lo ao BLL
            Filme filme = new Filme();
            filme.Duracao = txtDuracao.Text.ToInt();
            filme.Classificacao = (Classificacao)cmbClassificacao.SelectedItem;
            filme.Nome = txtNome.Text;
            filme.DataLancamento = dtpLancamento.Value;
            //O SelectedValue conversa com a propriedade ValueMember, preenchida
            //lá no evento Form_Load. Neste caso, setamos o ValueMember com o
            //valor da propriedade ID do Gênero. Enquanto o .Text da combobox
            //nos trás o Nome do Gênero, o .SelectedValue nos trás o ID!
            filme.GeneroID = (int)cmbGeneros.SelectedValue;

            //Após preencher todas as propriedades do objeto Filme, passaremos
            //ele ao bll!

            Response response = filmeBLL.Insert(filme);
            if (response.Sucesso)
            {
                MessageBox.Show("Filme cadastrado com sucesso!");
                dataGridView1.DataSource = filmeBLL.GetFilmes().Data;
            }
            else
            {
                MessageBox.Show(response.GetErrorMessage());
            }

        }

        private void cmbFIltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Se selecionarmos gênero ou classificação, queremos deixar visível a ComboBox,
            //caso contrário, a textbox para o nome

            if (cmbFIltro.Text == "Nome")
            {
                cmbPesquisa.Visible = false;
                txtPesquisa.Visible = true;
            }
            else
            {
                if (cmbFIltro.Text == "Gênero")
                {
                    cmbPesquisa.DataSource = null;
                    cmbPesquisa.DataSource = generoBLL.GetData().Data;//<- Este .Data retorna uma List<Genero>
                    cmbPesquisa.DisplayMember = "Nome";
                    cmbPesquisa.ValueMember = "ID";
                }
                else
                {
                    cmbPesquisa.DataSource = null;
                    cmbPesquisa.DataSource = Enum.GetValues(typeof(Classificacao));
                }
                cmbPesquisa.Visible = true;
                txtPesquisa.Visible = false;
            }

        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            DataResponse<FilmeResultSet> response = null;

            if (cmbFIltro.Text == "Nome")
            {
                response = filmeBLL.GetFilmesByName(txtPesquisa.Text);
            }
            else if (cmbFIltro.Text == "Gênero")
            {
                response = filmeBLL.GetFilmesByGenero(((Genero)cmbPesquisa.SelectedItem).ID);
            }
            else
            {
                response = filmeBLL.GetFilmesByClassificacao(((Classificacao)cmbPesquisa.SelectedItem));
            }
            if (response.Sucesso)
            {
                if (response.Data.Count == 0)
                {
                    MessageBox.Show("Não foram encontrados dados!");
                }
                else
                {
                    dataGridView1.DataSource = response.Data;
                }
            }
            else
            {
                MessageBox.Show(response.GetErrorMessage());
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            FilmeResultSet result = (FilmeResultSet)dataGridView1.SelectedRows[0].DataBoundItem;
            DataResponse<Filme> response = filmeBLL.GetByID(result.ID);
            if (response.Sucesso)
            {
                Filme filme = response.Data[0];
                idFilmeASerAtualizadoExcluido = filme.ID;
                txtDuracao.Text = filme.Duracao.ToString();
                txtNome.Text = filme.Nome;
                dtpLancamento.Value = filme.DataLancamento;

                cmbClassificacao.SelectedItem = filme.Classificacao;
                cmbGeneros.SelectedValue = filme.GeneroID;
            }   

        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            Filme filme = new Filme();
            filme.ID = idFilmeASerAtualizadoExcluido;
            filme.Duracao = txtDuracao.Text.ToInt();
            filme.Classificacao = (Classificacao)cmbClassificacao.SelectedItem;
            filme.Nome = txtNome.Text;
            filme.DataLancamento = dtpLancamento.Value;
            //O SelectedValue conversa com a propriedade ValueMember, preenchida
            //lá no evento Form_Load. Neste caso, setamos o ValueMember com o
            //valor da propriedade ID do Gênero. Enquanto o .Text da combobox
            //nos trás o Nome do Gênero, o .SelectedValue nos trás o ID!
            filme.GeneroID = (int)cmbGeneros.SelectedValue;

            //Após preencher todas as propriedades do objeto Filme, passaremos
            //ele ao bll!

            Response response = filmeBLL.Update(filme);
            if (response.Sucesso)
            {
                MessageBox.Show("Filme atualizado com sucesso!");
                dataGridView1.DataSource = filmeBLL.GetFilmes().Data;
            }
            else
            {
                MessageBox.Show(response.GetErrorMessage());
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            Response response = filmeBLL.Delete(idFilmeASerAtualizadoExcluido);
            if (response.Sucesso)
            {
                MessageBox.Show("Filme excluído com sucesso!");
                dataGridView1.DataSource = filmeBLL.GetFilmes().Data;
            }
            else
            {
                MessageBox.Show(response.GetErrorMessage());
            }
        }
    }
    //class Moeda
    //{

    //    public static explicit operator double(Moeda m)
    //    {
    //        return m.Valor;
    //    }

    //    public static explicit operator Moeda(double numero)
    //    {
    //        Moeda m = new Moeda()
    //        {
    //            Cifra = "R$",
    //            Valor = numero
    //        };
    //        return m;
    //    }

    //    public string Cifra { get; set; }
    //    public double Valor { get; set; }

    //}
}
