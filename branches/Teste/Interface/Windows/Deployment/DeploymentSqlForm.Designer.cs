namespace Deployment
{
    partial class DeploymentSqlForm
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
            this.tclOpcoes = new System.Windows.Forms.TabControl();
            this.tpeItens = new System.Windows.Forms.TabPage();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.Arquivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tpeDependecias = new System.Windows.Forms.TabPage();
            this.tbxItems = new System.Windows.Forms.TextBox();
            this.clbDependecias = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxItems = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.arquivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salvaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.saveFileDialogOrdem = new System.Windows.Forms.SaveFileDialog();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.rbnOpBasica = new System.Windows.Forms.RadioButton();
            this.rbnDados = new System.Windows.Forms.RadioButton();
            this.gbxOpcoes = new System.Windows.Forms.GroupBox();
            this.tclOpcoes.SuspendLayout();
            this.tpeItens.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.tpeDependecias.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.gbxOpcoes.SuspendLayout();
            this.SuspendLayout();
            // 
            // tclOpcoes
            // 
            this.tclOpcoes.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tclOpcoes.Controls.Add(this.tpeItens);
            this.tclOpcoes.Controls.Add(this.tpeDependecias);
            this.tclOpcoes.ItemSize = new System.Drawing.Size(52, 18);
            this.tclOpcoes.Location = new System.Drawing.Point(12, 27);
            this.tclOpcoes.Name = "tclOpcoes";
            this.tclOpcoes.SelectedIndex = 0;
            this.tclOpcoes.Size = new System.Drawing.Size(642, 292);
            this.tclOpcoes.TabIndex = 0;
            // 
            // tpeItens
            // 
            this.tpeItens.AllowDrop = true;
            this.tpeItens.Controls.Add(this.dgvItems);
            this.tpeItens.Location = new System.Drawing.Point(4, 22);
            this.tpeItens.Name = "tpeItens";
            this.tpeItens.Padding = new System.Windows.Forms.Padding(3);
            this.tpeItens.Size = new System.Drawing.Size(605, 266);
            this.tpeItens.TabIndex = 0;
            this.tpeItens.Text = "Itens";
            this.tpeItens.UseVisualStyleBackColor = true;
            this.tpeItens.DragOver += new System.Windows.Forms.DragEventHandler(this.tpeItems_DragOver);
            // 
            // dgvItems
            // 
            this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Arquivo});
            this.dgvItems.Location = new System.Drawing.Point(16, 18);
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.Size = new System.Drawing.Size(397, 150);
            this.dgvItems.TabIndex = 1;
            this.dgvItems.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgvItems_UserDeletingRow);
            // 
            // Arquivo
            // 
            this.Arquivo.HeaderText = "Arquivo";
            this.Arquivo.Name = "Arquivo";
            this.Arquivo.Width = 200;
            // 
            // tpeDependecias
            // 
            this.tpeDependecias.Controls.Add(this.gbxOpcoes);
            this.tpeDependecias.Controls.Add(this.tbxItems);
            this.tpeDependecias.Controls.Add(this.clbDependecias);
            this.tpeDependecias.Controls.Add(this.label1);
            this.tpeDependecias.Controls.Add(this.cbxItems);
            this.tpeDependecias.Location = new System.Drawing.Point(4, 22);
            this.tpeDependecias.Name = "tpeDependecias";
            this.tpeDependecias.Padding = new System.Windows.Forms.Padding(3);
            this.tpeDependecias.Size = new System.Drawing.Size(634, 266);
            this.tpeDependecias.TabIndex = 1;
            this.tpeDependecias.Text = "Dependecias";
            this.tpeDependecias.UseVisualStyleBackColor = true;
            // 
            // tbxItems
            // 
            this.tbxItems.Location = new System.Drawing.Point(9, 28);
            this.tbxItems.Name = "tbxItems";
            this.tbxItems.Size = new System.Drawing.Size(220, 20);
            this.tbxItems.TabIndex = 4;
            this.tbxItems.TextChanged += new System.EventHandler(this.tbxItems_TextChanged);
            // 
            // clbDependecias
            // 
            this.clbDependecias.CheckOnClick = true;
            this.clbDependecias.FormattingEnabled = true;
            this.clbDependecias.Location = new System.Drawing.Point(6, 55);
            this.clbDependecias.Name = "clbDependecias";
            this.clbDependecias.Size = new System.Drawing.Size(603, 199);
            this.clbDependecias.TabIndex = 2;
            this.clbDependecias.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbDependecias_ItemCheck);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Items";
            // 
            // cbxItems
            // 
            this.cbxItems.FormattingEnabled = true;
            this.cbxItems.Location = new System.Drawing.Point(235, 27);
            this.cbxItems.Name = "cbxItems";
            this.cbxItems.Size = new System.Drawing.Size(220, 21);
            this.cbxItems.TabIndex = 0;
            this.cbxItems.SelectedIndexChanged += new System.EventHandler(this.cbxItems_SelectedIndexChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.arquivoToolStripMenuItem,
            this.exportarToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(666, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // arquivoToolStripMenuItem
            // 
            this.arquivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.abrirToolStripMenuItem,
            this.salvaToolStripMenuItem});
            this.arquivoToolStripMenuItem.Name = "arquivoToolStripMenuItem";
            this.arquivoToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.arquivoToolStripMenuItem.Text = "Arquivo";
            // 
            // abrirToolStripMenuItem
            // 
            this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
            this.abrirToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.abrirToolStripMenuItem.Text = "Abrir";
            this.abrirToolStripMenuItem.Click += new System.EventHandler(this.abrirToolStripMenuItem_Click);
            // 
            // salvaToolStripMenuItem
            // 
            this.salvaToolStripMenuItem.Name = "salvaToolStripMenuItem";
            this.salvaToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.salvaToolStripMenuItem.Text = "Salvar";
            this.salvaToolStripMenuItem.Click += new System.EventHandler(this.salvaToolStripMenuItem_Click);
            // 
            // exportarToolStripMenuItem
            // 
            this.exportarToolStripMenuItem.Name = "exportarToolStripMenuItem";
            this.exportarToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.exportarToolStripMenuItem.Text = "Exportar";
            this.exportarToolStripMenuItem.Click += new System.EventHandler(this.exportarToolStripMenuItem_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "Deployment";
            this.openFileDialog.Filter = "Deployment|*.Dpmt";
            this.openFileDialog.InitialDirectory = "C:\\Secv\\SisSecv\\trunk\\IMPLEMENTACAO\\Deployment";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "Deployment";
            this.saveFileDialog.Filter = "Deployment|*.Dpmt";
            // 
            // saveFileDialogOrdem
            // 
            this.saveFileDialogOrdem.DefaultExt = "Ordem";
            this.saveFileDialogOrdem.Filter = "Ordem|*.Btx";
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // rbnOpBasica
            // 
            this.rbnOpBasica.AutoSize = true;
            this.rbnOpBasica.Location = new System.Drawing.Point(6, 19);
            this.rbnOpBasica.Name = "rbnOpBasica";
            this.rbnOpBasica.Size = new System.Drawing.Size(74, 17);
            this.rbnOpBasica.TabIndex = 6;
            this.rbnOpBasica.Text = "Op.Basica";
            this.rbnOpBasica.UseVisualStyleBackColor = true;
            this.rbnOpBasica.CheckedChanged += new System.EventHandler(this.rbnOpBasica_CheckedChanged);
            // 
            // rbnDados
            // 
            this.rbnDados.AutoSize = true;
            this.rbnDados.Location = new System.Drawing.Point(86, 19);
            this.rbnDados.Name = "rbnDados";
            this.rbnDados.Size = new System.Drawing.Size(56, 17);
            this.rbnDados.TabIndex = 7;
            this.rbnDados.Text = "Dados";
            this.rbnDados.UseVisualStyleBackColor = true;
            this.rbnDados.CheckedChanged += new System.EventHandler(this.rbnDados_CheckedChanged);
            // 
            // gbxOpcoes
            // 
            this.gbxOpcoes.Controls.Add(this.rbnOpBasica);
            this.gbxOpcoes.Controls.Add(this.rbnDados);
            this.gbxOpcoes.Location = new System.Drawing.Point(461, 6);
            this.gbxOpcoes.Name = "gbxOpcoes";
            this.gbxOpcoes.Size = new System.Drawing.Size(148, 44);
            this.gbxOpcoes.TabIndex = 8;
            this.gbxOpcoes.TabStop = false;
            this.gbxOpcoes.Text = "Opções";
            // 
            // DeplymentSqlForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 321);
            this.Controls.Add(this.tclOpcoes);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "DeplymentSqlForm";
            this.Text = "DeplymentSql";
            this.tclOpcoes.ResumeLayout(false);
            this.tpeItens.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.tpeDependecias.ResumeLayout(false);
            this.tpeDependecias.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.gbxOpcoes.ResumeLayout(false);
            this.gbxOpcoes.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tclOpcoes;
        private System.Windows.Forms.TabPage tpeItens;
        private System.Windows.Forms.TabPage tpeDependecias;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem arquivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salvaToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.CheckedListBox clbDependecias;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxItems;
        private System.Windows.Forms.ToolStripMenuItem exportarToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialogOrdem;
        private System.Windows.Forms.TextBox tbxItems;
        private System.Windows.Forms.DataGridViewTextBoxColumn Arquivo;
        private System.Windows.Forms.RadioButton rbnOpBasica;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.RadioButton rbnDados;
        private System.Windows.Forms.GroupBox gbxOpcoes;
    }
}

