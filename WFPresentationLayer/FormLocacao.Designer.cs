namespace WFPresentationLayer
{
    partial class FormLocacao
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtClienteID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtClienteNome = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtClienteCPF = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnPesquisaCLiente = new System.Windows.Forms.Button();
            this.btnPesquisaFilme = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.chkFoiPago = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtClienteID
            // 
            this.txtClienteID.AcceptsReturn = true;
            this.txtClienteID.Location = new System.Drawing.Point(33, 82);
            this.txtClienteID.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtClienteID.Name = "txtClienteID";
            this.txtClienteID.Size = new System.Drawing.Size(484, 30);
            this.txtClienteID.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 36);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 138);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Nome";
            // 
            // txtClienteNome
            // 
            this.txtClienteNome.Location = new System.Drawing.Point(33, 184);
            this.txtClienteNome.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtClienteNome.Name = "txtClienteNome";
            this.txtClienteNome.Size = new System.Drawing.Size(484, 30);
            this.txtClienteNome.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 237);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 25);
            this.label3.TabIndex = 5;
            this.label3.Text = "CPF";
            // 
            // txtClienteCPF
            // 
            this.txtClienteCPF.Location = new System.Drawing.Point(33, 283);
            this.txtClienteCPF.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtClienteCPF.Name = "txtClienteCPF";
            this.txtClienteCPF.Size = new System.Drawing.Size(484, 30);
            this.txtClienteCPF.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtClienteID);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtClienteCPF);
            this.groupBox1.Controls.Add(this.txtClienteNome);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(26, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(564, 375);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Dados Cliente";
            // 
            // btnPesquisaCLiente
            // 
            this.btnPesquisaCLiente.Location = new System.Drawing.Point(537, 414);
            this.btnPesquisaCLiente.Name = "btnPesquisaCLiente";
            this.btnPesquisaCLiente.Size = new System.Drawing.Size(53, 52);
            this.btnPesquisaCLiente.TabIndex = 6;
            this.btnPesquisaCLiente.Text = "...";
            this.btnPesquisaCLiente.UseVisualStyleBackColor = true;
            this.btnPesquisaCLiente.Click += new System.EventHandler(this.btnPesquisaCLiente_Click);
            // 
            // btnPesquisaFilme
            // 
            this.btnPesquisaFilme.Location = new System.Drawing.Point(1298, 427);
            this.btnPesquisaFilme.Name = "btnPesquisaFilme";
            this.btnPesquisaFilme.Size = new System.Drawing.Size(53, 52);
            this.btnPesquisaFilme.TabIndex = 7;
            this.btnPesquisaFilme.Text = "...";
            this.btnPesquisaFilme.UseVisualStyleBackColor = true;
            this.btnPesquisaFilme.Click += new System.EventHandler(this.btnPesquisaFilme_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(635, 37);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(716, 371);
            this.dataGridView1.TabIndex = 8;
            // 
            // chkFoiPago
            // 
            this.chkFoiPago.AutoSize = true;
            this.chkFoiPago.Location = new System.Drawing.Point(50, 427);
            this.chkFoiPago.Name = "chkFoiPago";
            this.chkFoiPago.Size = new System.Drawing.Size(112, 29);
            this.chkFoiPago.TabIndex = 9;
            this.chkFoiPago.Text = "Foi Pago";
            this.chkFoiPago.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(129, 541);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(436, 99);
            this.button1.TabIndex = 10;
            this.button1.Text = "Efetuar Locação";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormLocacao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1392, 691);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.chkFoiPago);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnPesquisaFilme);
            this.Controls.Add(this.btnPesquisaCLiente);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormLocacao";
            this.Text = "Locação";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtClienteID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtClienteNome;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtClienteCPF;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnPesquisaCLiente;
        private System.Windows.Forms.Button btnPesquisaFilme;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.CheckBox chkFoiPago;
        private System.Windows.Forms.Button button1;
    }
}