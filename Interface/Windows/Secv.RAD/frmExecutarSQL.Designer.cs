namespace Acontep.RAD
{
    partial class frmExecutarSQL
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExecutarSQL));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.conexaoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propriedadesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainerPrincipal = new System.Windows.Forms.SplitContainer();
            this.rtbArquivos = new WPC.OpenSource.EDIEditControl.Editor();
            this.rtbResultado = new WPC.OpenSource.EDIEditControl.Editor();
            this.gbxProgresso = new System.Windows.Forms.GroupBox();
            this.tlpProcesso = new System.Windows.Forms.TableLayoutPanel();
            this.pbrAtual = new System.Windows.Forms.ProgressBar();
            this.pbrTotal = new System.Windows.Forms.ProgressBar();
            this.lblProcessoAtual = new System.Windows.Forms.Label();
            this.lblProcessoTotal = new System.Windows.Forms.Label();
            this.lblConexao = new System.Windows.Forms.Label();
            this.edtConexao = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAdicionarArquivo = new System.Windows.Forms.ToolStripButton();
            this.tsbExecutar = new System.Windows.Forms.ToolStripButton();
            this.ofdSQL = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1.SuspendLayout();
            this.splitContainerPrincipal.Panel1.SuspendLayout();
            this.splitContainerPrincipal.Panel2.SuspendLayout();
            this.splitContainerPrincipal.SuspendLayout();
            this.gbxProgresso.SuspendLayout();
            this.tlpProcesso.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(732, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripSeparator,
            this.saveToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.fileToolStripMenuItem.Text = "&Arquivo";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem.Image")));
            this.newToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.newToolStripMenuItem.Text = "&Novo";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
            this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.openToolStripMenuItem.Text = "&Abrir";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(150, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
            this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.saveToolStripMenuItem.Text = "&Salvar";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(150, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.conexaoToolStripMenuItem,
            this.propriedadesToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.toolsToolStripMenuItem.Text = "&Ferramentas";
            // 
            // conexaoToolStripMenuItem
            // 
            this.conexaoToolStripMenuItem.Name = "conexaoToolStripMenuItem";
            this.conexaoToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.conexaoToolStripMenuItem.Text = "&Conexão";
            this.conexaoToolStripMenuItem.Click += new System.EventHandler(this.conexaoToolStripMenuItem_Click);
            // 
            // propriedadesToolStripMenuItem
            // 
            this.propriedadesToolStripMenuItem.Name = "propriedadesToolStripMenuItem";
            this.propriedadesToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.propriedadesToolStripMenuItem.Text = "Propriedades";
            this.propriedadesToolStripMenuItem.Click += new System.EventHandler(this.propriedadesToolStripMenuItem_Click);
            // 
            // splitContainerPrincipal
            // 
            this.splitContainerPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerPrincipal.Location = new System.Drawing.Point(0, 76);
            this.splitContainerPrincipal.Name = "splitContainerPrincipal";
            this.splitContainerPrincipal.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerPrincipal.Panel1
            // 
            this.splitContainerPrincipal.Panel1.Controls.Add(this.rtbArquivos);
            // 
            // splitContainerPrincipal.Panel2
            // 
            this.splitContainerPrincipal.Panel2.Controls.Add(this.rtbResultado);
            this.splitContainerPrincipal.Size = new System.Drawing.Size(732, 406);
            this.splitContainerPrincipal.SplitterDistance = 170;
            this.splitContainerPrincipal.TabIndex = 1;
            // 
            // rtbArquivos
            // 
            this.rtbArquivos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbArquivos.FileName = "";
            this.rtbArquivos.HideSelection = false;
            this.rtbArquivos.Location = new System.Drawing.Point(0, 0);
            this.rtbArquivos.Name = "rtbArquivos";
            this.rtbArquivos.NeedsSave = false;
            this.rtbArquivos.Size = new System.Drawing.Size(732, 170);
            this.rtbArquivos.SyntaxFile = "";
            this.rtbArquivos.TabIndex = 2;
            this.rtbArquivos.Text = "";
            this.rtbArquivos.WordWrap = false;
            this.rtbArquivos.WrapOnSegmentDelimiter = false;
            this.rtbArquivos.Leave += new System.EventHandler(this.rtbArquivos_Leave);
            // 
            // rtbResultado
            // 
            this.rtbResultado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbResultado.FileName = "";
            this.rtbResultado.HideSelection = false;
            this.rtbResultado.Location = new System.Drawing.Point(0, 0);
            this.rtbResultado.Name = "rtbResultado";
            this.rtbResultado.NeedsSave = false;
            this.rtbResultado.ReadOnly = true;
            this.rtbResultado.Size = new System.Drawing.Size(732, 232);
            this.rtbResultado.SyntaxFile = "";
            this.rtbResultado.TabIndex = 0;
            this.rtbResultado.Text = "";
            this.rtbResultado.WordWrap = false;
            this.rtbResultado.WrapOnSegmentDelimiter = false;
            // 
            // gbxProgresso
            // 
            this.gbxProgresso.Controls.Add(this.tlpProcesso);
            this.gbxProgresso.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gbxProgresso.Location = new System.Drawing.Point(0, 482);
            this.gbxProgresso.Name = "gbxProgresso";
            this.gbxProgresso.Size = new System.Drawing.Size(732, 92);
            this.gbxProgresso.TabIndex = 2;
            this.gbxProgresso.TabStop = false;
            this.gbxProgresso.Text = "Progresso:";
            // 
            // tlpProcesso
            // 
            this.tlpProcesso.AutoSize = true;
            this.tlpProcesso.ColumnCount = 2;
            this.tlpProcesso.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.12121F));
            this.tlpProcesso.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 87.87879F));
            this.tlpProcesso.Controls.Add(this.pbrAtual, 1, 0);
            this.tlpProcesso.Controls.Add(this.pbrTotal, 1, 1);
            this.tlpProcesso.Controls.Add(this.lblProcessoAtual, 0, 0);
            this.tlpProcesso.Controls.Add(this.lblProcessoTotal, 0, 1);
            this.tlpProcesso.Controls.Add(this.lblConexao, 0, 2);
            this.tlpProcesso.Controls.Add(this.edtConexao, 1, 2);
            this.tlpProcesso.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpProcesso.Location = new System.Drawing.Point(3, 16);
            this.tlpProcesso.Name = "tlpProcesso";
            this.tlpProcesso.RowCount = 3;
            this.tlpProcesso.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpProcesso.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpProcesso.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpProcesso.Size = new System.Drawing.Size(726, 73);
            this.tlpProcesso.TabIndex = 0;
            // 
            // pbrAtual
            // 
            this.pbrAtual.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbrAtual.Location = new System.Drawing.Point(90, 3);
            this.pbrAtual.Name = "pbrAtual";
            this.pbrAtual.Size = new System.Drawing.Size(633, 20);
            this.pbrAtual.TabIndex = 0;
            // 
            // pbrTotal
            // 
            this.pbrTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbrTotal.Location = new System.Drawing.Point(90, 29);
            this.pbrTotal.Name = "pbrTotal";
            this.pbrTotal.Size = new System.Drawing.Size(633, 20);
            this.pbrTotal.TabIndex = 1;
            // 
            // lblProcessoAtual
            // 
            this.lblProcessoAtual.AutoSize = true;
            this.lblProcessoAtual.Location = new System.Drawing.Point(3, 0);
            this.lblProcessoAtual.Name = "lblProcessoAtual";
            this.lblProcessoAtual.Size = new System.Drawing.Size(80, 13);
            this.lblProcessoAtual.TabIndex = 2;
            this.lblProcessoAtual.Text = "Processo atual:";
            // 
            // lblProcessoTotal
            // 
            this.lblProcessoTotal.AutoSize = true;
            this.lblProcessoTotal.Location = new System.Drawing.Point(3, 26);
            this.lblProcessoTotal.Name = "lblProcessoTotal";
            this.lblProcessoTotal.Size = new System.Drawing.Size(77, 13);
            this.lblProcessoTotal.TabIndex = 3;
            this.lblProcessoTotal.Text = "Processo total:";
            // 
            // lblConexao
            // 
            this.lblConexao.AutoSize = true;
            this.lblConexao.Location = new System.Drawing.Point(3, 52);
            this.lblConexao.Name = "lblConexao";
            this.lblConexao.Size = new System.Drawing.Size(52, 13);
            this.lblConexao.TabIndex = 4;
            this.lblConexao.Text = "Conexão:";
            // 
            // edtConexao
            // 
            this.edtConexao.AutoSize = true;
            this.edtConexao.Location = new System.Drawing.Point(90, 52);
            this.edtConexao.Name = "edtConexao";
            this.edtConexao.Size = new System.Drawing.Size(0, 13);
            this.edtConexao.TabIndex = 5;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAdicionarArquivo,
            this.tsbExecutar});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(732, 52);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbAdicionarArquivo
            // 
            this.tsbAdicionarArquivo.Image = global::Secv.RAD.Properties.Resources.Note1;
            this.tsbAdicionarArquivo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAdicionarArquivo.Name = "tsbAdicionarArquivo";
            this.tsbAdicionarArquivo.Size = new System.Drawing.Size(94, 49);
            this.tsbAdicionarArquivo.Text = "Adicionar arquivo";
            this.tsbAdicionarArquivo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbAdicionarArquivo.Click += new System.EventHandler(this.tsbAdicionarArquivo_Click);
            // 
            // tsbExecutar
            // 
            this.tsbExecutar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbExecutar.Image = global::Secv.RAD.Properties.Resources._12;
            this.tsbExecutar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExecutar.Name = "tsbExecutar";
            this.tsbExecutar.Size = new System.Drawing.Size(54, 49);
            this.tsbExecutar.Text = "Executar";
            this.tsbExecutar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbExecutar.Click += new System.EventHandler(this.tsbExecutar_Click);
            // 
            // ofdSQL
            // 
            this.ofdSQL.DefaultExt = "*.sql";
            this.ofdSQL.Filter = "*.sql|*.SQL|*.txt|*.txt|*.*|*.*";
            this.ofdSQL.Multiselect = true;
            this.ofdSQL.Title = "Adicionar arquivo";
            // 
            // frmExecutarSQL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 574);
            this.Controls.Add(this.splitContainerPrincipal);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.gbxProgresso);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmExecutarSQL";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Executar lista de arquivos SQL";
            this.Activated += new System.EventHandler(this.frmExecutarSQL_Activated);
            this.Load += new System.EventHandler(this.frmExecutarSQL_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainerPrincipal.Panel1.ResumeLayout(false);
            this.splitContainerPrincipal.Panel2.ResumeLayout(false);
            this.splitContainerPrincipal.ResumeLayout(false);
            this.gbxProgresso.ResumeLayout(false);
            this.gbxProgresso.PerformLayout();
            this.tlpProcesso.ResumeLayout(false);
            this.tlpProcesso.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem conexaoToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainerPrincipal;
        private WPC.OpenSource.EDIEditControl.Editor rtbResultado;
        private System.Windows.Forms.GroupBox gbxProgresso;
        private System.Windows.Forms.TableLayoutPanel tlpProcesso;
        private System.Windows.Forms.ProgressBar pbrAtual;
        private System.Windows.Forms.ProgressBar pbrTotal;
        private System.Windows.Forms.Label lblProcessoAtual;
        private System.Windows.Forms.Label lblProcessoTotal;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbAdicionarArquivo;
        private System.Windows.Forms.ToolStripButton tsbExecutar;
        private System.Windows.Forms.OpenFileDialog ofdSQL;
        private System.Windows.Forms.ToolStripMenuItem propriedadesToolStripMenuItem;
        private System.Windows.Forms.Label lblConexao;
        private System.Windows.Forms.Label edtConexao;
        private WPC.OpenSource.EDIEditControl.Editor rtbArquivos;
    }
}