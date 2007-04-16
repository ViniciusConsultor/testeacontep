using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data.Common;
using System.IO;



namespace Acontep.Ng.Manutencao.Rad
{
    public enum TipoNotificacao : int
    {
        ProcessoAtual = 1,
        ProcessoGeral,
        Erro,
        Finalizado
    }
    public class ExecutarListaSQL
    {
        event RunWorkerCompletedEventHandler _RunWorkerCompleted;
        public event RunWorkerCompletedEventHandler RunWorkerCompleted
        {
            add
            {
                _RunWorkerCompleted += value;
            }
            remove
            {
                _RunWorkerCompleted -= value;
            }
        }
        event ProgressChangedEventHandler _ProgressChanged;
        public event ProgressChangedEventHandler ProgressChanged
        {
            add
            {
                _ProgressChanged += value;
            }
            remove
            {
                _ProgressChanged -= value;
            }
        }
        Exception _LastError;
        public Exception LastError
        {
            get { return _LastError; }
            private set { _LastError = value; }

        }
        string _FilePath;
        public string FilePath
        {
            get { return _FilePath; }
            set { _FilePath = value; }
        }
        public ExecutarListaSQL()
        {
        }

        public ExecutarListaSQL( string filePath ) : this()
        {            
            this._FilePath = filePath;
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

        public bool Work()
        {
            
            lock (FilePath)
            {
                try
                {
                    if (!File.Exists(FilePath))
                    {
                        throw new ApplicationException("O arquivo [" + FilePath + "] não foi encontrado!");
                    }
                    EstruturaSaveRadUtil<ListaSQL>.AbrirArquivo(FilePath);
                    List<string> arquivos = new List<string>(ListaSQL.ObterProjetoAtual().GetArquivosRows().Length);
                    foreach (ListaSQL.ArquivosRow arquivo in ListaSQL.ObterProjetoAtual().GetArquivosRows())
                    {
                        string arquivoCorrente = arquivo.VirtualPath;
                        if (!Path.IsPathRooted(arquivoCorrente))
                        {
                            arquivoCorrente = Path.Combine(Path.GetDirectoryName(
                            EstruturaSaveRadUtil<ListaSQL>.FilePath), arquivoCorrente);
                        }
                        arquivos.Add(arquivoCorrente);
                    }
                    return ProcessarArquivos(arquivos.ToArray());
                }
                catch (Exception erro)
                {
                    this.LastError = erro;
                    return false;
                }
            }   
        }
        bool ProcessarArquivos(string[] arquivos)
        {
            bool Cancelado = true;
            try
            {
                string arquivoCorrente = string.Empty;
                string ComandoAtual = string.Empty;
                int qtdLinhasAfetadas = 0;

                ListaSQL.ProjetoRow projeto = ListaSQL.ObterProjetoAtual();
                DbConnection conexao = DbProviderFactories.GetFactory(projeto.Provider).CreateConnection();
                conexao.ConnectionString = projeto.StringConexao;
                conexao.Open();
                try
                {
                    DbTransaction tran = conexao.BeginTransaction();
                    try
                    {
                        DbCommand command = conexao.CreateCommand();
                        command.Transaction = tran;
                        if ( ! projeto.IsTimeOutNull() )
                            command.CommandTimeout = projeto.TimeOut;

                        for (int z = 0; z < arquivos.Length; z++)
                        {
                            qtdLinhasAfetadas = 0;
                            arquivoCorrente = arquivos[z];

                            string[] Comandos = this.RecuperarComando(File.ReadAllText(arquivoCorrente, Encoding.Default));

                            for (int i = 0; i < Comandos.Length; i++)
                            {
                                ComandoAtual = Comandos[i];
                                if (string.IsNullOrEmpty(ComandoAtual))
                                    continue;
                                command.CommandText = ComandoAtual;
                                qtdLinhasAfetadas += command.ExecuteNonQuery();
                                ReportProgress(CalculoPorcentagem(i, Comandos.Length), new object[] { TipoNotificacao.ProcessoAtual, "" });
                            }

                            ReportProgress(
                               CalculoPorcentagem(z, arquivos.Length),
                               new object[] { 
                               TipoNotificacao.ProcessoGeral,
                               string.Format("O arquivo {0} foi executado com sucesso! [{1} registro(s) afetados]\n",
                                       arquivoCorrente, qtdLinhasAfetadas)
                           }
                            );
                        }
                        tran.Commit();
                        Cancelado = false;
                    }
                    catch (Exception erro)
                    {
                        tran.Rollback();
                        string msgErro = string.Format("Erro ao executar o arquivo {0} no comando:\n\n {1} \n\n[DETALHES]\n\n >>>\n\n[{2}]\n",
                            arquivoCorrente, ComandoAtual, erro.ToString());
                        ReportProgress(0, new object[] { 
                            TipoNotificacao.Erro,
                            msgErro
                         });
                        throw new ApplicationException(msgErro);
                    }
                }
                finally
                {
                    if (conexao.State != System.Data.ConnectionState.Closed)
                        conexao.Close();
                }
            }
            catch (Exception erro)
            {
                Cancelado = true;
                LastError = erro;
            }
            ReportCompleted(Cancelado, LastError);
            return !Cancelado;
            
        }
        private int CalculoPorcentagem(int atual, int total)
        {
            return ((int)(atual + 1) * 100 / total);
        }
        void ReportProgress(int progressPercentage, object userState)
        {
            if (this._ProgressChanged!= null)
            {
                this._ProgressChanged.Invoke(this, new ProgressChangedEventArgs(progressPercentage, userState));
            }
        }

        void ReportCompleted(bool cancelled, object result )
        {
            if ( this._RunWorkerCompleted != null )
                this._RunWorkerCompleted(this, new RunWorkerCompletedEventArgs(result, LastError, cancelled));
        }
        
    }
}
