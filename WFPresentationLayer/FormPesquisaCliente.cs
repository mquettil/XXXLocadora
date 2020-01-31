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
    public partial class FormPesquisaCliente : Form
    {
        public FormPesquisaCliente()
        {
            InitializeComponent();
            this.Load += FormPesquisaCliente_Load;
            this.dataGridView1.CellDoubleClick += DataGridView1_CellDoubleClick;
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        public Cliente ClienteSelecionado { get; private set; }

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.ClienteSelecionado = (Cliente)this.dataGridView1.SelectedRows[0].DataBoundItem;
            this.Close();
        }

        private void FormPesquisaCliente_Load(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = new ClienteBLL().GetData().Data;
        }
    }
}
