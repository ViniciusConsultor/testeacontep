namespace Acontep.RAD
{
    partial class GerarMetodoViaSQL
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GerarMetodoViaSQL));
            this.tabControlMetodos = new System.Windows.Forms.TabControl();
            this.tabPageConfiguracao = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lstSQL = new System.Windows.Forms.GroupBox();
            this.lstMetodos = new System.Windows.Forms.CheckedListBox();
            this.grpBusca = new System.Windows.Forms.GroupBox();
            this.edtFiltroSQL = new System.Windows.Forms.TextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAdicionar = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRemover = new System.Windows.Forms.ToolStripButton();
            this.grpSQL = new System.Windows.Forms.GroupBox();
            this.edtNomeConsulta = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPageResultado = new System.Windows.Forms.TabPage();
            this.edtResultado = new System.Windows.Forms.RichTextBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonAreaTransferencia = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonGerarArquivoCS = new System.Windows.Forms.ToolStripButton();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.edtSQL = new WPC.OpenSource.EDIEditControl.Editor();
            this.tabControlMetodos.SuspendLayout();
            this.tabPageConfiguracao.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.lstSQL.SuspendLayout();
            this.grpBusca.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.grpSQL.SuspendLayout();
            this.tabPageResultado.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlMetodos
            // 
            this.tabControlMetodos.Controls.Add(this.tabPageConfiguracao);
            this.tabControlMetodos.Controls.Add(this.tabPageResultado);
            this.tabControlMetodos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMetodos.Location = new System.Drawing.Point(0, 0);
            this.tabControlMetodos.Name = "tabControlMetodos";
            this.tabControlMetodos.SelectedIndex = 0;
            this.tabControlMetodos.Size = new System.Drawing.Size(710, 572);
            this.tabControlMetodos.TabIndex = 0;
            this.toolTip.SetToolTip(this.tabControlMetodos, "Configura a geração dos métodos");
            this.tabControlMetodos.SelectedIndexChanged += new System.EventHandler(this.tabControlMetodos_SelectedIndexChanged);
            // 
            // tabPageConfiguracao
            // 
            this.tabPageConfiguracao.Controls.Add(this.splitContainer1);
            this.tabPageConfiguracao.Location = new System.Drawing.Point(4, 22);
            this.tabPageConfiguracao.Name = "tabPageConfiguracao";
            this.tabPageConfiguracao.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConfiguracao.Size = new System.Drawing.Size(702, 546);
            this.tabPageConfiguracao.TabIndex = 0;
            this.tabPageConfiguracao.Text = "Configuração";
            this.toolTip.SetToolTip(this.tabPageConfiguracao, "Configura a geração dos métodos");
            this.tabPageConfiguracao.ToolTipText = "Configura a geração dos métodos";
            this.tabPageConfiguracao.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lstSQL);
            this.splitContainer1.Panel1.Controls.Add(this.grpBusca);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grpSQL);
            this.splitContainer1.Panel2.Controls.Add(this.edtNomeConsulta);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Size = new System.Drawing.Size(696, 540);
            this.splitContainer1.SplitterDistance = 249;
            this.splitContainer1.TabIndex = 3;
            // 
            // lstSQL
            // 
            this.lstSQL.Controls.Add(this.lstMetodos);
            this.lstSQL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstSQL.Location = new System.Drawing.Point(0, 74);
            this.lstSQL.Name = "lstSQL";
            this.lstSQL.Size = new System.Drawing.Size(249, 466);
            this.lstSQL.TabIndex = 2;
            this.lstSQL.TabStop = false;
            this.lstSQL.Text = "Lista de SQL";
            // 
            // lstMetodos
            // 
            this.lstMetodos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstMetodos.FormattingEnabled = true;
            this.lstMetodos.Location = new System.Drawing.Point(3, 16);
            this.lstMetodos.Name = "lstMetodos";
            this.lstMetodos.Size = new System.Drawing.Size(243, 439);
            this.lstMetodos.TabIndex = 0;
            this.lstMetodos.SelectedIndexChanged += new System.EventHandler(this.lstMetodos_SelectedIndexChanged);
            this.lstMetodos.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.lstMetodos_Format);
            // 
            // grpBusca
            // 
            this.grpBusca.Controls.Add(this.edtFiltroSQL);
            this.grpBusca.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpBusca.Location = new System.Drawing.Point(0, 25);
            this.grpBusca.Name = "grpBusca";
            this.grpBusca.Size = new System.Drawing.Size(249, 49);
            this.grpBusca.TabIndex = 4;
            this.grpBusca.TabStop = false;
            this.grpBusca.Text = "Filtrar";
            // 
            // edtFiltroSQL
            // 
            this.edtFiltroSQL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.edtFiltroSQL.Location = new System.Drawing.Point(3, 16);
            this.edtFiltroSQL.Name = "edtFiltroSQL";
            this.edtFiltroSQL.Size = new System.Drawing.Size(243, 20);
            this.edtFiltroSQL.TabIndex = 0;
            this.toolTip.SetToolTip(this.edtFiltroSQL, "Filtrar método. Utilize % para um melhor resultado.");
            this.edtFiltroSQL.TextChanged += new System.EventHandler(this.edtFiltroSQL_TextChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.toolStripButtonAdicionar,
            this.toolStripButtonRemover});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(249, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // newToolStripButton
            // 
            this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripButton.Image")));
            this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripButton.Name = "newToolStripButton";
            this.newToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.newToolStripButton.Text = "&New";
            this.newToolStripButton.ToolTipText = "Novo método";
            this.newToolStripButton.Click += new System.EventHandler(this.newToolStripButton_Click);
            // 
            // toolStripButtonAdicionar
            // 
            this.toolStripButtonAdicionar.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAdicionar.Image")));
            this.toolStripButtonAdicionar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAdicionar.Name = "toolStripButtonAdicionar";
            this.toolStripButtonAdicionar.Size = new System.Drawing.Size(71, 22);
            this.toolStripButtonAdicionar.Text = "Adicionar";
            this.toolStripButtonAdicionar.ToolTipText = "Adiciona ou atualiza os dados.";
            this.toolStripButtonAdicionar.Click += new System.EventHandler(this.toolStripButtonAdicionar_Click);
            // 
            // toolStripButtonRemover
            // 
            this.toolStripButtonRemover.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRemover.Image")));
            this.toolStripButtonRemover.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRemover.Name = "toolStripButtonRemover";
            this.toolStripButtonRemover.Size = new System.Drawing.Size(70, 22);
            this.toolStripButtonRemover.Text = "Remover";
            this.toolStripButtonRemover.ToolTipText = "Remove os métodos marcados";
            this.toolStripButtonRemover.Click += new System.EventHandler(this.toolStripButtonRemover_Click);
            // 
            // grpSQL
            // 
            this.grpSQL.Controls.Add(this.edtSQL);
            this.grpSQL.Location = new System.Drawing.Point(3, 36);
            this.grpSQL.Name = "grpSQL";
            this.grpSQL.Size = new System.Drawing.Size(437, 504);
            this.grpSQL.TabIndex = 2;
            this.grpSQL.TabStop = false;
            this.grpSQL.Text = "SQL";
            // 
            // edtNomeConsulta
            // 
            this.edtNomeConsulta.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.edtNomeConsulta.Location = new System.Drawing.Point(120, 10);
            this.edtNomeConsulta.Name = "edtNomeConsulta";
            this.edtNomeConsulta.Size = new System.Drawing.Size(311, 20);
            this.edtNomeConsulta.TabIndex = 1;
            this.toolTip.SetToolTip(this.edtNomeConsulta, "Nome do método que será gerado para esta consulta.");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nome da consulta:";
            // 
            // tabPageResultado
            // 
            this.tabPageResultado.Controls.Add(this.edtResultado);
            this.tabPageResultado.Controls.Add(this.toolStrip2);
            this.tabPageResultado.Location = new System.Drawing.Point(4, 22);
            this.tabPageResultado.Name = "tabPageResultado";
            this.tabPageResultado.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageResultado.Size = new System.Drawing.Size(702, 546);
            this.tabPageResultado.TabIndex = 1;
            this.tabPageResultado.Text = "Resultado";
            this.toolTip.SetToolTip(this.tabPageResultado, "Resultado da configuração");
            this.tabPageResultado.ToolTipText = "Metodos";
            this.tabPageResultado.UseVisualStyleBackColor = true;
            // 
            // edtResultado
            // 
            this.edtResultado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.edtResultado.Location = new System.Drawing.Point(3, 28);
            this.edtResultado.Name = "edtResultado";
            this.edtResultado.Size = new System.Drawing.Size(696, 515);
            this.edtResultado.TabIndex = 0;
            this.edtResultado.Text = "";
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonAreaTransferencia,
            this.toolStripButtonGerarArquivoCS});
            this.toolStrip2.Location = new System.Drawing.Point(3, 3);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(696, 25);
            this.toolStrip2.Stretch = true;
            this.toolStrip2.TabIndex = 2;
            // 
            // toolStripButtonAreaTransferencia
            // 
            this.toolStripButtonAreaTransferencia.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAreaTransferencia.Image")));
            this.toolStripButtonAreaTransferencia.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAreaTransferencia.Name = "toolStripButtonAreaTransferencia";
            this.toolStripButtonAreaTransferencia.Size = new System.Drawing.Size(132, 22);
            this.toolStripButtonAreaTransferencia.Text = "Área de transferência";
            this.toolStripButtonAreaTransferencia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripButtonAreaTransferencia.Click += new System.EventHandler(this.toolStripButtonAreaTransferencia_Click);
            // 
            // toolStripButtonGerarArquivoCS
            // 
            this.toolStripButtonGerarArquivoCS.Enabled = false;
            this.toolStripButtonGerarArquivoCS.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonGerarArquivoCS.Image")));
            this.toolStripButtonGerarArquivoCS.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonGerarArquivoCS.Name = "toolStripButtonGerarArquivoCS";
            this.toolStripButtonGerarArquivoCS.Size = new System.Drawing.Size(113, 22);
            this.toolStripButtonGerarArquivoCS.Text = "Gerar arquivo .CS";
            // 
            // toolTip
            // 
            this.toolTip.IsBalloon = true;
            this.toolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip.UseAnimation = false;
            this.toolTip.UseFading = false;
            // 
            // edtSQL
            // 
            this.edtSQL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.edtSQL.FileName = "";
            this.edtSQL.HideSelection = false;
            this.edtSQL.Location = new System.Drawing.Point(3, 16);
            this.edtSQL.Name = "edtSQL";
            this.edtSQL.NeedsSave = false;
            this.edtSQL.Size = new System.Drawing.Size(431, 485);
            this.edtSQL.SyntaxFile = "";
            this.edtSQL.TabIndex = 0;
            this.edtSQL.Text = "";
            this.edtSQL.WordWrap = false;
            this.edtSQL.WrapOnSegmentDelimiter = false;
            // 
            // GerarMetodoViaSQL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 572);
            this.Controls.Add(this.tabControlMetodos);
            this.Name = "GerarMetodoViaSQL";
            this.Text = "Gerar método via SQL";
            this.Load += new System.EventHandler(this.GerarMetodoViaSQL_Load);
            this.tabControlMetodos.ResumeLayout(false);
            this.tabPageConfiguracao.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.lstSQL.ResumeLayout(false);
            this.grpBusca.ResumeLayout(false);
            this.grpBusca.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.grpSQL.ResumeLayout(false);
            this.tabPageResultado.ResumeLayout(false);
            this.tabPageResultado.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlMetodos;
        private System.Windows.Forms.TabPage tabPageConfiguracao;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox lstSQL;
        private System.Windows.Forms.GroupBox grpBusca;
        private System.Windows.Forms.TextBox edtFiltroSQL;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton newToolStripButton;
        private System.Windows.Forms.ToolStripButton toolStripButtonAdicionar;
        private System.Windows.Forms.ToolStripButton toolStripButtonRemover;
        private System.Windows.Forms.GroupBox grpSQL;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TextBox edtNomeConsulta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPageResultado;
        private System.Windows.Forms.RichTextBox edtResultado;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripButtonAreaTransferencia;
        private System.Windows.Forms.ToolStripButton toolStripButtonGerarArquivoCS;
        private System.Windows.Forms.CheckedListBox lstMetodos;
        private WPC.OpenSource.EDIEditControl.Editor edtSQL;


    }
}