using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using Acontep.Ng.Manutencao.Rad;
using Acontep.Ng.Manutencao.Rad.Properties;

namespace Acontep.Ng.Manutencao.Rad
{
    public static class EstruturaSaveRadUtil<T> where T : DataSet, new()
    {
        public static T ConteudoClasse
        {
            get
            {
                if (AppDomain.CurrentDomain.GetData(typeof(T).ToString()) == null)
                {
                    ConteudoClasse = new T();
                }
                return (T)AppDomain.CurrentDomain.GetData(typeof(T).ToString());
            }
            set
            {
                AppDomain.CurrentDomain.SetData(typeof(T).ToString(), value);
            }
        }

        static string _filePath = string.Empty;
        public static string FilePath
        {
            get{return _filePath;}
            set { _filePath = value; }
        }
    

        public static void AbrirArquivo(string Local) 
        {
            ConteudoClasse.Clear();
            ConteudoClasse.ReadXml(Local);
            FilePath = Local;
            ConteudoClasse.AcceptChanges();

        }
        /// <summary>
        /// Salva o arquivo
        /// </summary>
        public static void AbrirArquivo()
        {
            AbrirArquivo(false);
        }
        public static bool AbrirArquivo(bool PerguntaSalvar)
        {
            if ( PerguntaSalvar && ConteudoClasse.HasChanges())
            {
                SalvarArquivo();
            }
            OpenFileDialog openFileDialog = CriarOpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                AbrirArquivo(openFileDialog.FileName);
                return true;
            }
            return false;
    
        }

        /// <summary>
        /// Criar the open file dialog.
        /// </summary>
        /// <returns></returns>
        private static OpenFileDialog CriarOpenFileDialog()
        {
            
            OpenFileDialog open = new OpenFileDialog();
            open.AddExtension = true;
            SetarConfiguracoesJanelaArquivo(open);
            if (System.IO.Directory.Exists(Settings.Default.DiretororioPadrao))
            {
                open.InitialDirectory = Settings.Default.DiretororioPadrao;
            }
            open.Title = "Abrir arquivo";
            return open;
        }
        private static void SetarConfiguracoesJanelaArquivo(FileDialog fileDialog)
        {
            if (ConteudoClasse.GetType() == typeof(EstruturaSaveRAD))
            {
                fileDialog.DefaultExt = "CRUD";
                fileDialog.Filter = "CRUD|*.CRUD|*.*|*.*";
            }
            else if (ConteudoClasse.GetType() == typeof(ListaSQL))
            {
                fileDialog.DefaultExt = "btx";
                fileDialog.Filter = "btx|*.btx|*.*|*.*";
            }
            else
            {
                fileDialog.DefaultExt = "";
                fileDialog.Filter = "*.*|*.*";
            }
        }
        /// <summary>
        /// Criar the save file dialog.
        /// </summary>
        /// <returns></returns>
        private static SaveFileDialog CriarSaveFileDialog()
        {
            SaveFileDialog save = new SaveFileDialog();
            save.AddExtension = true;
            SetarConfiguracoesJanelaArquivo(save);

            if ( System.IO.Directory.Exists(Settings.Default.DiretororioPadrao) )
            {
                save.InitialDirectory = Settings.Default.DiretororioPadrao;
            }
            save.Title = "Salvar arquivo";
            return save;
        }
        /// <summary>
        /// Salva o arquivo
        /// </summary>
        /// <param name="Local">Diretorio com o nome do arquivo.</param>
        public static void SalvarArquivo(string Local) 
        {
            ConteudoClasse.WriteXml(Local);
            FilePath = Local;
            ConteudoClasse.AcceptChanges();
        }
        /// <summary>
        /// Salva o arquivo
        /// </summary>
        /// <returns>Local onde foi salvo o arquivo</returns>
        public static string SalvarArquivo()
        {
            return SalvarArquivo(true);
        }
        /// <summary>
        /// Salva o arquivo
        /// </summary>
        /// <returns>Local onde foi salvo o arquivo</returns>
        public static string SalvarArquivo(bool PerguntarSeDesejaSalvar)
        {
            if (ConteudoClasse.HasChanges())
            {
                if ( ! PerguntarSeDesejaSalvar || MessageBox.Show("Deseja salvar as modificações?", "Salvar?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (!string.IsNullOrEmpty(FilePath))
                    {
                        SalvarArquivo(FilePath);
                    }
                    else
                    {
                        SaveFileDialog saveFileDialog = CriarSaveFileDialog();
                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            SalvarArquivo(saveFileDialog.FileName);
                            return saveFileDialog.FileName;
                        }
                    }
                }
            }
            return FilePath;
        }
    }
}
