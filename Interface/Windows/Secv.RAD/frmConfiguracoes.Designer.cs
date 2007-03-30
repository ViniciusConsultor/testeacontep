namespace Acontep.RAD
{
    partial class frmConfiguracoes
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
            this.grbConexao = new System.Windows.Forms.GroupBox();
            this.StringConexaoTextBox = new System.Windows.Forms.TextBox();
            this.ProviderComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.grbConexao.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbConexao
            // 
            this.grbConexao.AutoSize = true;
            this.grbConexao.Controls.Add(this.StringConexaoTextBox);
            this.grbConexao.Controls.Add(this.ProviderComboBox);
            this.grbConexao.Controls.Add(this.label1);
            this.grbConexao.Controls.Add(this.label3);
            this.grbConexao.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbConexao.Location = new System.Drawing.Point(0, 0);
            this.grbConexao.Name = "grbConexao";
            this.grbConexao.Size = new System.Drawing.Size(506, 74);
            this.grbConexao.TabIndex = 1;
            this.grbConexao.TabStop = false;
            this.grbConexao.Text = "Conexão";
            // 
            // StringConexaoTextBox
            // 
            this.StringConexaoTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.StringConexaoTextBox.AutoCompleteCustomSource.AddRange(new string[] {
            "Data Source=CentralDev;Initial Catalog=SisAcontepTeste;User ID = sa; Pwd =centraldev" +
                "sa;",
            "Data Source=Franciscopc;Initial Catalog=Teste; Trusted_Connection = yes;",
            "Data Source=Thiagopc;Initial Catalog=SisAcontepTeste; Trusted_Connection = yes;"});
            this.StringConexaoTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.StringConexaoTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.StringConexaoTextBox.Location = new System.Drawing.Point(62, 12);
            this.StringConexaoTextBox.Name = "StringConexaoTextBox";
            this.StringConexaoTextBox.Size = new System.Drawing.Size(437, 20);
            this.StringConexaoTextBox.TabIndex = 1;
            this.StringConexaoTextBox.Text = "Data Source=Centraldev;Initial Catalog=SisAcontepTeste; Trusted_Connection = yes;";
            // 
            // ProviderComboBox
            // 
            this.ProviderComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ProviderComboBox.FormattingEnabled = true;
            this.ProviderComboBox.Items.AddRange(new object[] {
            "System.Data.SqlClient"});
            this.ProviderComboBox.Location = new System.Drawing.Point(62, 34);
            this.ProviderComboBox.Name = "ProviderComboBox";
            this.ProviderComboBox.Size = new System.Drawing.Size(437, 21);
            this.ProviderComboBox.TabIndex = 2;
            this.ProviderComboBox.Text = "System.Data.SqlClient";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "String";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Provider";
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirmar.Location = new System.Drawing.Point(346, 80);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(75, 23);
            this.btnConfirmar.TabIndex = 2;
            this.btnConfirmar.Text = "Confirmar";
            this.btnConfirmar.UseVisualStyleBackColor = true;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Location = new System.Drawing.Point(428, 80);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 3;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // frmConfiguracoes
            // 
            this.AcceptButton = this.btnConfirmar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(506, 115);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.grbConexao);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConfiguracoes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configurações";
            this.Load += new System.EventHandler(this.frmConfiguracoes_Load);
            this.grbConexao.ResumeLayout(false);
            this.grbConexao.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grbConexao;
        private System.Windows.Forms.TextBox StringConexaoTextBox;
        private System.Windows.Forms.ComboBox ProviderComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnConfirmar;
        private System.Windows.Forms.Button btnCancelar;

    }
}