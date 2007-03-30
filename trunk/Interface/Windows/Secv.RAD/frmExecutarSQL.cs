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
    public partial class frmExecutarSQL : Form
    {
        ExecutarListaSQL executarLista = null;
        public frmExecutarSQL()
        {
            InitializeComponent();
            executarLista = new ExecutarListaSQL();
            executarLista.ProgressChanged += new ProgressChangedEventHandler(bwrExecutarSQLs_ProgressChanged);
            executarLista.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrExecutarSQLs_RunWorkerCompleted);
        }
        
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pComandoOriginal"></param>
        /// <returns></returns>
        public string[] RecuperarComando(String pComandoOriginal)
        {
            return TratrarComandoGO(pComandoOriginal, false).Split(new string[] { "§GO§" }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pComandoOriginal"></param>
        /// <returns></returns>
        public static string TratrarComandoGO(String pComandoOriginal, bool pRetirarGO)
        {
            string lstr = pComandoOriginal;
            int lQtd = lstr.IndexOf("GO", StringComparison.InvariantCultureIgnoreCase);
            while (lQtd > -1)
            {
                if (
                    lstr.Trim().Length == 2 || lstr.Trim().Length == lQtd + 2 ||
                    (lstr.Trim().Length >= lQtd + 2 && Char.IsWhiteSpace(lstr[lQtd - 1]) && Char.IsWhiteSpace(lstr[lQtd + 2]))
                    )
                {
                    lstr = lstr.Insert(lQtd + 2, "§");
                    lstr = lstr.Insert(lQtd, "§");
                }
                if (lstr.Length > lQtd + 3)
                    lQtd = lstr.IndexOf("GO", lQtd + 3);
                else
                {
                    lQtd = -1;
                }
            }
            if (pRetirarGO)
                lstr = lstr.Replace("§GO§", "");
            return lstr;

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void conexaoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmConfiguracoes().ShowDialog();
        }

        private void frmExecutarSQL_Activated(object sender, EventArgs e)
        {
            conexaoToolStripMenuItem.Checked = AppUtil.Conexao != null;
            if (conexaoToolStripMenuItem.Checked)
            {
                edtConexao.Text = AppUtil.Conexao.ConnectionString;
            }            
        }

        private void tsbAdicionarArquivo_Click(object sender, EventArgs e)
        {
            try
            {
                AtualizarVirtualPath(); 
                if (ofdSQL.ShowDialog() == DialogResult.OK)
                {
                    AdicionarArquivos(ofdSQL.FileNames);
                }
            }
            catch (Exception erro)
            {
                AppUtil.ExibirErro(erro);
            }
        }
        private string CalcularCaminhoVirtual(string diretorioBase, string arquivo)
        {
            string pathVirtual = arquivo;
            if (AppUtil.CanGetRelativePath(diretorioBase, arquivo))
            {
                pathVirtual = AppUtil.GetRelativePath(diretorioBase, arquivo);
            }
            return pathVirtual;
        }
        private void AdicionarArquivos(params string[] listaArquivos)
        {
            if (listaArquivos != null && listaArquivos.Length > 0)
            {
                if (string.IsNullOrEmpty(EstruturaSaveRadUtil<ListaSQL>.FilePath))
                {
                    if (MessageBox.Show("Para continuar é preciso salvar!") == DialogResult.OK)
                    {
                        if (string.IsNullOrEmpty(EstruturaSaveRadUtil<ListaSQL>.SalvarArquivo(false)))
                            return;
                    }
                }
                ListaSQL.ProjetoRow projeto = ListaSQL.ObterProjetoAtual();
                foreach (string arquivo in listaArquivos)
                {
                    if (string.IsNullOrEmpty(arquivo)) continue;
                    EstruturaSaveRadUtil<ListaSQL>.ConteudoClasse.Arquivos.AddArquivosRow(
                    CalcularCaminhoVirtual(
                        EstruturaSaveRadUtil<ListaSQL>.FilePath,
                        arquivo
                        ),
                    Guid.NewGuid(), 
                    projeto);
                }                
            }
            AtualizarListaArquivoUsuario();
        }

        private void propriedadesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmPropriedadesListaSQL().ShowDialog();
        }

        private void tsbExecutar_Click(object sender, EventArgs e)
        {
            rtbResultado.Clear();
            if (Salvar(false))
            {
                executarLista.FilePath = EstruturaSaveRadUtil<ListaSQL>.FilePath;
                executarLista.Work();
            }
        }
        private bool ExisteConexao()
        {
            if (AppUtil.Conexao == null)
            {
                if (new frmConfiguracoes().ShowDialog() != DialogResult.OK)
                {
                    MessageBox.Show("Uma string de conexão deve ser informada!");
                    return false;
                }
            }
            return true;
        }
        private int CalculoPorcentagem(int atual, int total)
        {
            return ((int)(atual + 1) * 100 / total);
        }
        private void bwrExecutarSQLs_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            object[] Info = (object[])e.UserState;
            string msg = Info[1].ToString();
            switch ((TipoNotificacao)Info[0])
            {
                case TipoNotificacao.ProcessoAtual:
                    {
                        if (e.ProgressPercentage > 0)
                        {
                            pbrAtual.Value = e.ProgressPercentage;
                        }
                        break;
                    }
                case TipoNotificacao.Erro:
                    rtbResultado.AppendText(msg);
                    break;

                case TipoNotificacao.ProcessoGeral:
                    {
                        rtbResultado.AppendText(msg);
                        if (e.ProgressPercentage > 0)
                        {
                            pbrTotal.Value = e.ProgressPercentage;
                        }
                        break;
                    }
            }
        }
        private bool Salvar(bool perguntarSeDesejaSalvar)
        {
            if (!ExisteConexao())
            {
                MessageBox.Show("O arquivo não foi salvo!");
                return false;
            }
            ListaSQL.ProjetoRow projeto = ListaSQL.ObterProjetoAtual();
            if (projeto != null)
            {
                projeto.StringConexao = AppUtil.Conexao.ConnectionString;
                projeto.Provider = AppUtil.FactoryName;
            }            
            AtualizarVirtualPath();
            return !String.IsNullOrEmpty(EstruturaSaveRadUtil<ListaSQL>.SalvarArquivo(perguntarSeDesejaSalvar));
            
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Salvar(true);
        }

        private void AtualizarVirtualPath()
        {
            ListaSQL.ProjetoRow projeto = ListaSQL.ObterProjetoAtual();
            foreach (ListaSQL.ArquivosRow arquivo in projeto.GetArquivosRows())
            {
                arquivo.Delete();
            }
            AdicionarArquivos(rtbArquivos.Lines);

            
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EstruturaSaveRadUtil<ListaSQL>.AbrirArquivo(EstruturaSaveRadUtil<ListaSQL>.ConteudoClasse.HasChanges());
            frmExecutarSQL_Load(sender, e);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EstruturaSaveRadUtil<ListaSQL>.ConteudoClasse = null;
            frmExecutarSQL_Load(sender, e);
        }

        private void frmExecutarSQL_Load(object sender, EventArgs e)
        {
            rtbResultado.Clear();
            ListaSQL.ProjetoRow projeto = ListaSQL.ObterProjetoAtual();
            AtualizarListaArquivoUsuario();            
            AppUtil.TryConectar(projeto.StringConexao, projeto.Provider);
            frmExecutarSQL_Activated(sender, e);
        }

        private void AtualizarListaArquivoUsuario()
        {
            rtbArquivos.Clear();
            foreach (ListaSQL.ArquivosRow arquivo in ListaSQL.ObterProjetoAtual().GetArquivosRows())
            {
                rtbArquivos.InsertText(arquivo.VirtualPath + "\n");
            }
        }

        private void bwrExecutarSQLs_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ( e.Cancelled )
                rtbResultado.AppendText("O processo não foi finalizado pois estava com erro.");
            else
                rtbResultado.AppendText("Processo finalizado com sucesso.");
        }

        private void rtbArquivos_Leave(object sender, EventArgs e)
        {
            AtualizarVirtualPath();            
        }

    }
}