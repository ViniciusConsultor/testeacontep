using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Deployment
{
    public partial class DeploymentForm : Form
    {
        public DeploymentForm()
        {
            InitializeComponent();
        }

        private void btnSql_Click(object sender, EventArgs e)
        {
            new DeploymentSqlForm().Show();
        }

        private void btnArquivo_Click(object sender, EventArgs e)
        {
            new DeploymentFileForm().Show();
        }
    }
}