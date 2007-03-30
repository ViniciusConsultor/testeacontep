namespace Acontep.RAD
{
    partial class OpcoesCRUD
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grbOpcoesCRUD = new System.Windows.Forms.GroupBox();
            this.chkOpcoesCrud = new System.Windows.Forms.CheckedListBox();
            this.grbOpcoesCRUD.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbOpcoesCRUD
            // 
            this.grbOpcoesCRUD.Controls.Add(this.chkOpcoesCrud);
            this.grbOpcoesCRUD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbOpcoesCRUD.Location = new System.Drawing.Point(0, 0);
            this.grbOpcoesCRUD.Name = "grbOpcoesCRUD";
            this.grbOpcoesCRUD.Size = new System.Drawing.Size(150, 92);
            this.grbOpcoesCRUD.TabIndex = 22;
            this.grbOpcoesCRUD.TabStop = false;
            this.grbOpcoesCRUD.Text = "Opções CRUD";
            // 
            // chkOpcoesCrud
            // 
            this.chkOpcoesCrud.CheckOnClick = true;
            this.chkOpcoesCrud.ColumnWidth = 100;
            this.chkOpcoesCrud.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkOpcoesCrud.FormattingEnabled = true;
            this.chkOpcoesCrud.Items.AddRange(new object[] {
            "SELECT",
            "INSERT",
            "UPDATE",
            "DELETE"});
            this.chkOpcoesCrud.Location = new System.Drawing.Point(3, 16);
            this.chkOpcoesCrud.MultiColumn = true;
            this.chkOpcoesCrud.Name = "chkOpcoesCrud";
            this.chkOpcoesCrud.Size = new System.Drawing.Size(144, 64);
            this.chkOpcoesCrud.TabIndex = 20;
            // 
            // OpcoesCRUD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grbOpcoesCRUD);
            this.Name = "OpcoesCRUD";
            this.Size = new System.Drawing.Size(150, 92);
            this.grbOpcoesCRUD.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbOpcoesCRUD;
        private System.Windows.Forms.CheckedListBox chkOpcoesCrud;
    }
}
