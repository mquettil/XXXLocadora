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
    public partial class FormFuncionario : Form
    {
        public FormFuncionario()
        {
            InitializeComponent();
        }

        private FuncionarioBLL funcionarioBLL = new FuncionarioBLL();


        private void button1_Click(object sender, EventArgs e)
        {
            if (txtConfirmarSenha.Text != txtSenha.Text)
            {
                MessageBox.Show("Senha diferentes!");
                return;
            }

            Funcionario funcionario = new Funcionario();
            funcionario.CPF = txtCpf.Text;
            funcionario.DataNascimento = dtpDataNascimento.Value;
            funcionario.Email = txtEmail.Text;
            funcionario.Nome = txtNome.Text;
            funcionario.Senha = txtSenha.Text;
            funcionario.Telefone = txtTelefone.Text;

            Response response = funcionarioBLL.Insert(funcionario);
            if (response.Sucesso)
            {
                MessageBox.Show("Cadastrado com sucesso!");
            }
            else
            {
                MessageBox.Show(response.GetErrorMessage());
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtSenha.UseSystemPasswordChar = !txtSenha.UseSystemPasswordChar;
        }

        private void btnMostrarSenha2_Click(object sender, EventArgs e)
        {
            txtConfirmarSenha.UseSystemPasswordChar = !txtConfirmarSenha.UseSystemPasswordChar;
        }
    }
}
