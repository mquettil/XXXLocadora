using BusinessLogicalLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFPresentationLayer
{
    public partial class FormGenero : Form
    {
        public FormGenero()
        {
            InitializeComponent();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            //É aqui que a entidade é criada!Ela será validada e 
            //formatada no BLL e inserida no DAL
            Genero genero = new Genero();
            genero.Nome = txtGenero.Text;

            GeneroBLL bll = new GeneroBLL();
            //Invoca a sequência de operações de inserção (bll depois dal)
            //e recebe a resposta destas operaçoes!
            Response response = bll.Insert(genero);
            if (response.Sucesso)
            {
                MessageBox.Show("Cadastrado com sucesso!");
            }
            else
            {
                MessageBox.Show(response.GetErrorMessage());
            }

        }
    }
}
