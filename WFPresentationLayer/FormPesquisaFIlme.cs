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
    public partial class FormPesquisaFIlme : Form
    {
        public FormPesquisaFIlme()
        {
            InitializeComponent();
            this.Load += FormPesquisaFIlme_Load;
            this.dataGridView1.CellDoubleClick += DataGridView1_CellDoubleClick;
        }

        public Filme FilmeSelecionado { get; private set; }

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.FilmeSelecionado = this.dataGridView1.SelectedRows[0].DataBoundItem as Filme;
            this.Close();
        }

        private void FormPesquisaFIlme_Load(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = new FilmeBLL().GetData().Data;
        }
    }
}
