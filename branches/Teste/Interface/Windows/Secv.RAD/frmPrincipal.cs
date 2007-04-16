using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using Acontep.Ng.Manutencao.Rad;
using System.Globalization;

namespace Acontep.RAD
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void geradorCRUDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GerarCRUD gerarCRUD = new GerarCRUD();
            gerarCRUD.ShowDialog();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EstruturaSaveRadUtil<EstruturaSaveRAD>.ConteudoClasse.Clear();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                EstruturaSaveRadUtil<EstruturaSaveRAD>.AbrirArquivo(true);              
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

       
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                EstruturaSaveRadUtil<EstruturaSaveRAD>.SalvarArquivo();
            }
            catch (Exception Erro)
            {
                AppUtil.ExibirErro(Erro);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gerarMetodoViaSqlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GerarMetodoViaSQL gerar = new GerarMetodoViaSQL();
            gerar.ShowDialog();
        }

        private void frmPrincipal_Activated(object sender, EventArgs e)
        {
            AdicionarInformacoesArquivo();
        }

        private void AdicionarInformacoesArquivo()
        {
            grpInformacoes.Controls.Clear();
            Label Label = new Label();
            Label.BorderStyle = BorderStyle.FixedSingle;
            Label.Dock = DockStyle.Top;
            Label.BackColor = EstruturaSaveRadUtil<EstruturaSaveRAD>.ConteudoClasse.HasChanges() ? Color.Yellow : Color.Green;
            Label.Text = string.Format("Existe atualizações pendentes: {0}", EstruturaSaveRadUtil<EstruturaSaveRAD>.ConteudoClasse.HasChanges() ? "SIM" : "NÃO");
            grpInformacoes.Controls.Add(Label);

            Label = new Label();
            Label.BorderStyle = BorderStyle.FixedSingle;
            Label.Dock = DockStyle.Top;
            Label.Text = string.Format("Quantidade de Classes que serão geradas: {0}", EstruturaSaveRadUtil<EstruturaSaveRAD>.ConteudoClasse.ClasseCRUD.Count);
            grpInformacoes.Controls.Add(Label);
            Label = new Label();
            Label.BorderStyle = BorderStyle.FixedSingle;
            Label.Dock = DockStyle.Top;
            Label.Text = string.Format("Quantidade de métods com SQL personalizados: {0}", EstruturaSaveRadUtil<EstruturaSaveRAD>.ConteudoClasse.MetodoConsulta.Count);
            grpInformacoes.Controls.Add(Label);
            Label = new Label();
            Label.BorderStyle = BorderStyle.FixedSingle;
            Label.Dock = DockStyle.Top;
            Label.Text = string.Format("Quantidade de métods personalizados: {0}", EstruturaSaveRadUtil<EstruturaSaveRAD>.ConteudoClasse.Metodo.Count);
            grpInformacoes.Controls.Add(Label);
        }

        private void frmPrincipal_Paint(object sender, PaintEventArgs e)
        {
            AdicionarInformacoesArquivo();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            System.Globalization.CultureInfo info = new CultureInfo("pt-BR");
            
            this.Text = string.Format("{1} [v.{0} - {2}]",
                Application.ProductVersion,                
                this.Text,
                DateTime.Parse("12-03-2007", info).ToShortDateString()
                );
        }

        private void sobreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmAbout().ShowDialog();

        }

        private void executarListaSQLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmExecutarSQL().ShowDialog();
        }
    }
}