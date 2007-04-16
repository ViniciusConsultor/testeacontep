namespace Acontep.RAD
{
    partial class frmPropriedadesListaSQL
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
            this.gbxPropriedades = new System.Windows.Forms.GroupBox();
            this.tlpPropriedades = new System.Windows.Forms.TableLayoutPanel();
            this.lblProjeto = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.edtQtdArquivos = new System.Windows.Forms.Label();
            this.lblQtdArquivos = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.mtbTimeOut = new System.Windows.Forms.MaskedTextBox();
            this.edtDirBase = new System.Windows.Forms.Label();
            this.rtbDescricao = new WPC.OpenSource.EDIEditControl.Editor();
            this.rtbProjeto = new WPC.OpenSource.EDIEditControl.Editor();
            this.gbxPropriedades.SuspendLayout();
            this.tlpPropriedades.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxPropriedades
            // 
            this.gbxPropriedades.Controls.Add(this.tlpPropriedades);
            this.gbxPropriedades.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxPropriedades.Location = new System.Drawing.Point(0, 0);
            this.gbxPropriedades.Name = "gbxPropriedades";
            this.gbxPropriedades.Size = new System.Drawing.Size(511, 358);
            this.gbxPropriedades.TabIndex = 0;
            this.gbxPropriedades.TabStop = false;
            this.gbxPropriedades.Text = "Propriedades:";
            // 
            // tlpPropriedades
            // 
            this.tlpPropriedades.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tlpPropriedades.ColumnCount = 2;
            this.tlpPropriedades.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.67213F));
            this.tlpPropriedades.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.32787F));
            this.tlpPropriedades.Controls.Add(this.rtbDescricao, 1, 3);
            this.tlpPropriedades.Controls.Add(this.lblProjeto, 0, 0);
            this.tlpPropriedades.Controls.Add(this.rtbProjeto, 1, 0);
            this.tlpPropriedades.Controls.Add(this.label1, 0, 3);
            this.tlpPropriedades.Controls.Add(this.edtQtdArquivos, 1, 2);
            this.tlpPropriedades.Controls.Add(this.lblQtdArquivos, 0, 2);
            this.tlpPropriedades.Controls.Add(this.label2, 0, 1);
            this.tlpPropriedades.Controls.Add(this.groupBox1, 1, 5);
            this.tlpPropriedades.Controls.Add(this.label3, 0, 4);
            this.tlpPropriedades.Controls.Add(this.mtbTimeOut, 1, 4);
            this.tlpPropriedades.Controls.Add(this.edtDirBase, 1, 1);
            this.tlpPropriedades.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpPropriedades.Location = new System.Drawing.Point(3, 16);
            this.tlpPropriedades.Name = "tlpPropriedades";
            this.tlpPropriedades.RowCount = 6;
            this.tlpPropriedades.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpPropriedades.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpPropriedades.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpPropriedades.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpPropriedades.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpPropriedades.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpPropriedades.Size = new System.Drawing.Size(505, 339);
            this.tlpPropriedades.TabIndex = 0;
            // 
            // lblProjeto
            // 
            this.lblProjeto.AutoSize = true;
            this.lblProjeto.Location = new System.Drawing.Point(4, 1);
            this.lblProjeto.Name = "lblProjeto";
            this.lblProjeto.Size = new System.Drawing.Size(43, 13);
            this.lblProjeto.TabIndex = 0;
            this.lblProjeto.Text = "Projeto:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Descrição:";
            // 
            // edtQtdArquivos
            // 
            this.edtQtdArquivos.AutoSize = true;
            this.edtQtdArquivos.Location = new System.Drawing.Point(103, 50);
            this.edtQtdArquivos.Name = "edtQtdArquivos";
            this.edtQtdArquivos.Size = new System.Drawing.Size(13, 13);
            this.edtQtdArquivos.TabIndex = 4;
            this.edtQtdArquivos.Text = "0";
            // 
            // lblQtdArquivos
            // 
            this.lblQtdArquivos.AutoSize = true;
            this.lblQtdArquivos.Location = new System.Drawing.Point(4, 50);
            this.lblQtdArquivos.Name = "lblQtdArquivos";
            this.lblQtdArquivos.Size = new System.Drawing.Size(71, 13);
            this.lblQtdArquivos.TabIndex = 3;
            this.lblQtdArquivos.Text = "Qtd Arquivos:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Diretório base:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnCancelar);
            this.groupBox1.Controls.Add(this.btnConfirmar);
            this.groupBox1.Location = new System.Drawing.Point(334, 285);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(167, 46);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Location = new System.Drawing.Point(87, 16);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Location = new System.Drawing.Point(6, 16);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(75, 23);
            this.btnConfirmar.TabIndex = 0;
            this.btnConfirmar.Text = "Confirmar";
            this.btnConfirmar.UseVisualStyleBackColor = true;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 255);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Time out:";
            // 
            // mtbTimeOut
            // 
            this.mtbTimeOut.Location = new System.Drawing.Point(103, 258);
            this.mtbTimeOut.Mask = "99999";
            this.mtbTimeOut.Name = "mtbTimeOut";
            this.mtbTimeOut.Size = new System.Drawing.Size(63, 20);
            this.mtbTimeOut.TabIndex = 12;
            this.mtbTimeOut.Text = "30";
            // 
            // edtDirBase
            // 
            this.edtDirBase.AutoSize = true;
            this.edtDirBase.Location = new System.Drawing.Point(103, 36);
            this.edtDirBase.Name = "edtDirBase";
            this.edtDirBase.Size = new System.Drawing.Size(0, 13);
            this.edtDirBase.TabIndex = 13;
            // 
            // rtbDescricao
            // 
            this.rtbDescricao.AcceptsTab = true;
            this.rtbDescricao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbDescricao.FileName = "";
            this.rtbDescricao.HideSelection = false;
            this.rtbDescricao.Location = new System.Drawing.Point(103, 67);
            this.rtbDescricao.Name = "rtbDescricao";
            this.rtbDescricao.NeedsSave = false;
            this.rtbDescricao.Size = new System.Drawing.Size(398, 184);
            this.rtbDescricao.SyntaxFile = "";
            this.rtbDescricao.TabIndex = 9;
            this.rtbDescricao.Text = "";
            this.rtbDescricao.WordWrap = false;
            this.rtbDescricao.WrapOnSegmentDelimiter = false;
            // 
            // rtbProjeto
            // 
            this.rtbProjeto.AcceptsTab = true;
            this.rtbProjeto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbProjeto.FileName = "";
            this.rtbProjeto.HideSelection = false;
            this.rtbProjeto.Location = new System.Drawing.Point(103, 4);
            this.rtbProjeto.Name = "rtbProjeto";
            this.rtbProjeto.NeedsSave = false;
            this.rtbProjeto.Size = new System.Drawing.Size(398, 28);
            this.rtbProjeto.SyntaxFile = "";
            this.rtbProjeto.TabIndex = 8;
            this.rtbProjeto.Text = "";
            this.rtbProjeto.WordWrap = false;
            this.rtbProjeto.WrapOnSegmentDelimiter = false;
            // 
            // frmPropriedadesListaSQL
            // 
            this.AcceptButton = this.btnConfirmar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(511, 358);
            this.Controls.Add(this.gbxPropriedades);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPropriedadesListaSQL";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Propriedades";
            this.Load += new System.EventHandler(this.frmPropriedadesListaSQL_Load);
            this.gbxPropriedades.ResumeLayout(false);
            this.tlpPropriedades.ResumeLayout(false);
            this.tlpPropriedades.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxPropriedades;
        private System.Windows.Forms.TableLayoutPanel tlpPropriedades;
        private System.Windows.Forms.Label lblProjeto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblQtdArquivos;
        private System.Windows.Forms.Label edtQtdArquivos;
        private WPC.OpenSource.EDIEditControl.Editor rtbDescricao;
        private WPC.OpenSource.EDIEditControl.Editor rtbProjeto;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnConfirmar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MaskedTextBox mtbTimeOut;
        private System.Windows.Forms.Label edtDirBase;
    }
}