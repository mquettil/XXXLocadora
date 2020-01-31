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
    public partial class FormCliente : Form
    {
        public FormCliente()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cliente cliente = new Cliente();
            ClienteBLL bll = new ClienteBLL();
            Response response = bll.Insert(cliente);
            if (response.Sucesso)
            {
                MessageBox.Show("Cliente cadastrado com sucesso!");
            }
            else
            {
                MessageBox.Show(response.GetErrorMessage());
            }
        }
    }
}
