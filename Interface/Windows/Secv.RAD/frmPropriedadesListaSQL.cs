using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.Common;
using Acontep.Ng.Manutencao.Rad;

namespace Acontep.RAD
{
    public partial class frmPropriedadesListaSQL : Form
    {
        public frmPropriedadesListaSQL()
        {
            InitializeComponent();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            ListaSQL.ProjetoRow projeto = ListaSQL.ObterProjetoAtual();
            projeto.Descricao = rtbDescricao.Text;
            projeto.Projeto = rtbProjeto.Text;            
            projeto.TimeOut = Convert.ToInt32(mtbTimeOut.Text);
            if (AppUtil.Conexao == null)
            {
                if (new frmConfiguracoes().ShowDialog() != DialogResult.OK) 
                {
                    MessageBox.Show("Uma string de conexão deve ser informada!");
                    return;
                }
            }
            projeto.StringConexao = AppUtil.Conexao.ConnectionString;

            projeto.Provider = AppUtil.FactoryName;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void frmPropriedadesListaSQL_Load(object sender, EventArgs e)
        {
            CarregarInformacoes();
        }

        private void CarregarInformacoes()
        {
            ListaSQL.ProjetoRow projeto = ListaSQL.ObterProjetoAtual();
            rtbDescricao.Text = projeto.Descricao;
            rtbProjeto.Text = projeto.Projeto;
            edtDirBase.Text = EstruturaSaveRadUtil<ListaSQL>.FilePath;
            edtQtdArquivos.Text = projeto.GetArquivosRows().Length.ToString();
            mtbTimeOut.Text = projeto.TimeOut.ToString();
            if ( AppUtil.Conexao == null && ! ( string.IsNullOrEmpty(projeto.StringConexao) && string.IsNullOrEmpty(projeto.Provider)))
            {
                AppUtil.TryConectar(projeto.StringConexao, projeto.Provider);
            }
        }
    }
}