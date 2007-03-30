namespace Deployment
{
    partial class DeploymentFileForm
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
            this.tbxAlteracao = new System.Windows.Forms.TextBox();
            this.fbdCaminho = new System.Windows.Forms.FolderBrowserDialog();
            this.lblALteracao = new System.Windows.Forms.Label();
            this.btnAlteracao = new System.Windows.Forms.Button();
            this.btnBase = new System.Windows.Forms.Button();
            this.lblBase = new System.Windows.Forms.Label();
            this.tbxBase = new System.Windows.Forms.TextBox();
            this.btnResultado = new System.Windows.Forms.Button();
            this.lblResultado = new System.Windows.Forms.Label();
            this.tbxResultado = new System.Windows.Forms.TextBox();
            this.tbxLog = new System.Windows.Forms.TextBox();
            this.btnComparar = new System.Windows.Forms.Button();
            this.btnImplantar = new System.Windows.Forms.Button();
            this.gbxAvisos = new System.Windows.Forms.GroupBox();
            this.gbxOperacoes = new System.Windows.Forms.GroupBox();
            this.cbxDatDifAviso = new System.Windows.Forms.CheckBox();
            this.cbxArqDifDatDifAviso = new System.Windows.Forms.CheckBox();
            this.cbxCriar = new System.Windows.Forms.CheckBox();
            this.cbxModificar = new System.Windows.Forms.CheckBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.cbxArqIgualDatMaior = new System.Windows.Forms.CheckBox();
            this.cbxArqIgualDatMenor = new System.Windows.Forms.CheckBox();
            this.gbxAvisos.SuspendLayout();
            this.gbxOperacoes.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbxAlteracao
            // 
            this.tbxAlteracao.Location = new System.Drawing.Point(15, 39);
            this.tbxAlteracao.Name = "tbxAlteracao";
            this.tbxAlteracao.Size = new System.Drawing.Size(167, 20);
            this.tbxAlteracao.TabIndex = 0;
            this.tbxAlteracao.Text = "C:\\Documents and Settings\\thiago\\Desktop\\Implantacao2";
            // 
            // lblALteracao
            // 
            this.lblALteracao.AutoSize = true;
            this.lblALteracao.Location = new System.Drawing.Point(12, 23);
            this.lblALteracao.Name = "lblALteracao";
            this.lblALteracao.Size = new System.Drawing.Size(55, 13);
            this.lblALteracao.TabIndex = 1;
            this.lblALteracao.Text = "Alteração:";
            // 
            // btnAlteracao
            // 
            this.btnAlteracao.AutoSize = true;
            this.btnAlteracao.Location = new System.Drawing.Point(188, 37);
            this.btnAlteracao.Name = "btnAlteracao";
            this.btnAlteracao.Size = new System.Drawing.Size(28, 23);
            this.btnAlteracao.TabIndex = 2;
            this.btnAlteracao.Text = "...";
            this.btnAlteracao.UseVisualStyleBackColor = true;
            this.btnAlteracao.Click += new System.EventHandler(this.btnAlteracao_Click);
            // 
            // btnBase
            // 
            this.btnBase.AutoSize = true;
            this.btnBase.Location = new System.Drawing.Point(398, 37);
            this.btnBase.Name = "btnBase";
            this.btnBase.Size = new System.Drawing.Size(28, 23);
            this.btnBase.TabIndex = 5;
            this.btnBase.Text = "...";
            this.btnBase.UseVisualStyleBackColor = true;
            this.btnBase.Click += new System.EventHandler(this.btnBase_Click);
            // 
            // lblBase
            // 
            this.lblBase.AutoSize = true;
            this.lblBase.Location = new System.Drawing.Point(222, 23);
            this.lblBase.Name = "lblBase";
            this.lblBase.Size = new System.Drawing.Size(34, 13);
            this.lblBase.TabIndex = 4;
            this.lblBase.Text = "Base:";
            // 
            // tbxBase
            // 
            this.tbxBase.Location = new System.Drawing.Point(225, 39);
            this.tbxBase.Name = "tbxBase";
            this.tbxBase.Size = new System.Drawing.Size(167, 20);
            this.tbxBase.TabIndex = 3;
            this.tbxBase.Text = "C:\\Documents and Settings\\thiago\\Desktop\\Official";
            // 
            // btnResultado
            // 
            this.btnResultado.AutoSize = true;
            this.btnResultado.Location = new System.Drawing.Point(608, 37);
            this.btnResultado.Name = "btnResultado";
            this.btnResultado.Size = new System.Drawing.Size(28, 23);
            this.btnResultado.TabIndex = 8;
            this.btnResultado.Text = "...";
            this.btnResultado.UseVisualStyleBackColor = true;
            this.btnResultado.Click += new System.EventHandler(this.btnResultado_Click);
            // 
            // lblResultado
            // 
            this.lblResultado.AutoSize = true;
            this.lblResultado.Location = new System.Drawing.Point(432, 23);
            this.lblResultado.Name = "lblResultado";
            this.lblResultado.Size = new System.Drawing.Size(58, 13);
            this.lblResultado.TabIndex = 7;
            this.lblResultado.Text = "Resultado:";
            // 
            // tbxResultado
            // 
            this.tbxResultado.Location = new System.Drawing.Point(435, 39);
            this.tbxResultado.Name = "tbxResultado";
            this.tbxResultado.Size = new System.Drawing.Size(167, 20);
            this.tbxResultado.TabIndex = 6;
            this.tbxResultado.Text = "C:\\Documents and Settings\\thiago\\Desktop\\Resultado";
            // 
            // tbxLog
            // 
            this.tbxLog.AcceptsReturn = true;
            this.tbxLog.Location = new System.Drawing.Point(15, 165);
            this.tbxLog.Multiline = true;
            this.tbxLog.Name = "tbxLog";
            this.tbxLog.ReadOnly = true;
            this.tbxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxLog.Size = new System.Drawing.Size(587, 151);
            this.tbxLog.TabIndex = 9;
            // 
            // btnComparar
            // 
            this.btnComparar.Location = new System.Drawing.Point(15, 136);
            this.btnComparar.Name = "btnComparar";
            this.btnComparar.Size = new System.Drawing.Size(75, 23);
            this.btnComparar.TabIndex = 10;
            this.btnComparar.Text = "Comparar";
            this.btnComparar.UseVisualStyleBackColor = true;
            this.btnComparar.Click += new System.EventHandler(this.btnComparar_Click);
            // 
            // btnImplantar
            // 
            this.btnImplantar.Location = new System.Drawing.Point(96, 136);
            this.btnImplantar.Name = "btnImplantar";
            this.btnImplantar.Size = new System.Drawing.Size(75, 23);
            this.btnImplantar.TabIndex = 11;
            this.btnImplantar.Text = "Implantar";
            this.btnImplantar.UseVisualStyleBackColor = true;
            this.btnImplantar.Visible = false;
            // 
            // gbxAvisos
            // 
            this.gbxAvisos.Controls.Add(this.cbxArqDifDatDifAviso);
            this.gbxAvisos.Controls.Add(this.cbxDatDifAviso);
            this.gbxAvisos.Location = new System.Drawing.Point(15, 66);
            this.gbxAvisos.Name = "gbxAvisos";
            this.gbxAvisos.Size = new System.Drawing.Size(227, 67);
            this.gbxAvisos.TabIndex = 12;
            this.gbxAvisos.TabStop = false;
            this.gbxAvisos.Text = "Avisos";
            // 
            // gbxOperacoes
            // 
            this.gbxOperacoes.Controls.Add(this.cbxArqIgualDatMenor);
            this.gbxOperacoes.Controls.Add(this.cbxArqIgualDatMaior);
            this.gbxOperacoes.Controls.Add(this.checkBox5);
            this.gbxOperacoes.Controls.Add(this.cbxModificar);
            this.gbxOperacoes.Controls.Add(this.cbxCriar);
            this.gbxOperacoes.Location = new System.Drawing.Point(248, 66);
            this.gbxOperacoes.Name = "gbxOperacoes";
            this.gbxOperacoes.Size = new System.Drawing.Size(354, 67);
            this.gbxOperacoes.TabIndex = 13;
            this.gbxOperacoes.TabStop = false;
            this.gbxOperacoes.Text = "Operações";
            // 
            // cbxDatDifAviso
            // 
            this.cbxDatDifAviso.AutoSize = true;
            this.cbxDatDifAviso.Location = new System.Drawing.Point(6, 19);
            this.cbxDatDifAviso.Name = "cbxDatDifAviso";
            this.cbxDatDifAviso.Size = new System.Drawing.Size(93, 17);
            this.cbxDatDifAviso.TabIndex = 0;
            this.cbxDatDifAviso.Text = "Data diferente";
            this.cbxDatDifAviso.UseVisualStyleBackColor = true;
            // 
            // cbxArqDifDatDifAviso
            // 
            this.cbxArqDifDatDifAviso.AutoSize = true;
            this.cbxArqDifDatDifAviso.Checked = true;
            this.cbxArqDifDatDifAviso.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxArqDifDatDifAviso.Location = new System.Drawing.Point(6, 42);
            this.cbxArqDifDatDifAviso.Name = "cbxArqDifDatDifAviso";
            this.cbxArqDifDatDifAviso.Size = new System.Drawing.Size(215, 17);
            this.cbxArqDifDatDifAviso.TabIndex = 1;
            this.cbxArqDifDatDifAviso.Text = "Arquivo diferente com data incompativel";
            this.cbxArqDifDatDifAviso.UseVisualStyleBackColor = true;
            // 
            // cbxCriar
            // 
            this.cbxCriar.AutoSize = true;
            this.cbxCriar.Checked = true;
            this.cbxCriar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxCriar.Location = new System.Drawing.Point(6, 19);
            this.cbxCriar.Name = "cbxCriar";
            this.cbxCriar.Size = new System.Drawing.Size(47, 17);
            this.cbxCriar.TabIndex = 1;
            this.cbxCriar.Text = "Criar";
            this.cbxCriar.UseVisualStyleBackColor = true;
            // 
            // cbxModificar
            // 
            this.cbxModificar.AutoSize = true;
            this.cbxModificar.Checked = true;
            this.cbxModificar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxModificar.Location = new System.Drawing.Point(6, 42);
            this.cbxModificar.Name = "cbxModificar";
            this.cbxModificar.Size = new System.Drawing.Size(69, 17);
            this.cbxModificar.TabIndex = 2;
            this.cbxModificar.Text = "Modificar";
            this.cbxModificar.UseVisualStyleBackColor = true;
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Location = new System.Drawing.Point(274, 42);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(57, 17);
            this.checkBox5.TabIndex = 3;
            this.checkBox5.Text = "Excluir";
            this.checkBox5.UseVisualStyleBackColor = true;
            this.checkBox5.Visible = false;
            // 
            // cbxArqIgualDatMaior
            // 
            this.cbxArqIgualDatMaior.AutoSize = true;
            this.cbxArqIgualDatMaior.Checked = true;
            this.cbxArqIgualDatMaior.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxArqIgualDatMaior.Location = new System.Drawing.Point(75, 42);
            this.cbxArqIgualDatMaior.Name = "cbxArqIgualDatMaior";
            this.cbxArqIgualDatMaior.Size = new System.Drawing.Size(162, 17);
            this.cbxArqIgualDatMaior.TabIndex = 4;
            this.cbxArqIgualDatMaior.Text = "Dados iguais com data maior";
            this.cbxArqIgualDatMaior.UseVisualStyleBackColor = true;
            // 
            // cbxArqIgualDatMenor
            // 
            this.cbxArqIgualDatMenor.AutoSize = true;
            this.cbxArqIgualDatMenor.Location = new System.Drawing.Point(75, 19);
            this.cbxArqIgualDatMenor.Name = "cbxArqIgualDatMenor";
            this.cbxArqIgualDatMenor.Size = new System.Drawing.Size(166, 17);
            this.cbxArqIgualDatMenor.TabIndex = 5;
            this.cbxArqIgualDatMenor.Text = "Dados iguais com data menor";
            this.cbxArqIgualDatMenor.UseVisualStyleBackColor = true;
            // 
            // DeploymentFileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 328);
            this.Controls.Add(this.gbxOperacoes);
            this.Controls.Add(this.gbxAvisos);
            this.Controls.Add(this.btnImplantar);
            this.Controls.Add(this.btnComparar);
            this.Controls.Add(this.tbxLog);
            this.Controls.Add(this.btnResultado);
            this.Controls.Add(this.lblResultado);
            this.Controls.Add(this.tbxResultado);
            this.Controls.Add(this.btnBase);
            this.Controls.Add(this.lblBase);
            this.Controls.Add(this.tbxBase);
            this.Controls.Add(this.btnAlteracao);
            this.Controls.Add(this.lblALteracao);
            this.Controls.Add(this.tbxAlteracao);
            this.Name = "DeploymentFileForm";
            this.Text = "DeplymentFile";
            this.Load += new System.EventHandler(this.DeplymentFileForm_Load);
            this.gbxAvisos.ResumeLayout(false);
            this.gbxAvisos.PerformLayout();
            this.gbxOperacoes.ResumeLayout(false);
            this.gbxOperacoes.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbxAlteracao;
        private System.Windows.Forms.FolderBrowserDialog fbdCaminho;
        private System.Windows.Forms.Label lblALteracao;
        private System.Windows.Forms.Button btnAlteracao;
        private System.Windows.Forms.Button btnBase;
        private System.Windows.Forms.Label lblBase;
        private System.Windows.Forms.TextBox tbxBase;
        private System.Windows.Forms.Button btnResultado;
        private System.Windows.Forms.Label lblResultado;
        private System.Windows.Forms.TextBox tbxResultado;
        private System.Windows.Forms.TextBox tbxLog;
        private System.Windows.Forms.Button btnComparar;
        private System.Windows.Forms.Button btnImplantar;
        private System.Windows.Forms.GroupBox gbxAvisos;
        private System.Windows.Forms.CheckBox cbxArqDifDatDifAviso;
        private System.Windows.Forms.CheckBox cbxDatDifAviso;
        private System.Windows.Forms.GroupBox gbxOperacoes;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.CheckBox cbxModificar;
        private System.Windows.Forms.CheckBox cbxCriar;
        private System.Windows.Forms.CheckBox cbxArqIgualDatMaior;
        private System.Windows.Forms.CheckBox cbxArqIgualDatMenor;
    }
}