using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Acontep.RAD
{
    public partial class frmConfiguracoes : Form
    {
        public frmConfiguracoes()
        {
            InitializeComponent();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            AppUtil.Conectar(this.StringConexaoTextBox.Text, this.ProviderComboBox.Text);
            
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void frmConfiguracoes_Load(object sender, EventArgs e)
        {
            if (AppUtil.Conexao != null)
            {
                this.StringConexaoTextBox.Text = AppUtil.Conexao.ConnectionString;
                this.ProviderComboBox.SelectedValue = AppUtil.FactoryName;
            }
        }
    }
}