using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Secv.RAD.Properties;

using Acontep.Dado;
using System.Data.Common;
using System.Collections;
using System.IO;

namespace Acontep.RAD
{
    class AppUtil
    {


        public static DbConnection Conexao
        {
            get
            {
                DbConnection conteudo = AppDomain.CurrentDomain.GetData("Conexao") as DbConnection;
                return conteudo;
            }
            set
            {
                AppDomain.CurrentDomain.SetData("Conexao", value);
            }
        }


        public static string FactoryName
        {
            get { return "System.Data.SqlClient"; }
        }
        /// <summary>
        /// Conecta se houve modificação da string de conexao ou se a conexao estiver fechada. <see cref="GeradorShema"/>
        /// </summary>        
        /// <param name="StringConexao">String de conexao</param>
        /// <param name="Provider">Provider da string de conexao</param>
        public static void Conectar(string StringConexao, string Provider)
        {
            if (
                    Conexao == null ||
                    Conexao.ConnectionString != StringConexao ||
                    Conexao.GetType().Name != Provider
                )
            {
                FecharConexao();
                Conexao = DbProviderFactories.GetFactory(Provider).CreateConnection();
                Conexao.ConnectionString = StringConexao;
                Conexao.Open();
            }
        }
        public static bool TryConectar(string StringConexao, string Provider)
        {
            try
            {
                Conectar(StringConexao, Provider);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void FecharConexao()
        {
            if (Conexao != null && Conexao.State != System.Data.ConnectionState.Closed)
            {
                Conexao.Close();
            }

        }

        public static void ExibirErro(Exception erro)
        {
#if DEBUG
            MessageBox.Show(erro.ToString());
#else
            MessageBox.Show(erro.Message);
#endif
        }


        /// <summary>
        /// Determines whether this instance [can get retative path] the specified dir base.
        /// </summary>
        /// <param name="DirBase">The dir base.</param>
        /// <param name="PathArquivo">The path arquivo.</param>
        /// <returns>
        /// 	<c>true</c> if this instance [can get retative path] the specified dir base; otherwise, <c>false</c>.
        /// </returns>
        public static bool CanGetRelativePath(string DirBase, string PathArquivo)
        {
            if ( !Path.IsPathRooted(PathArquivo))
                return false;
            PathArquivo = Path.GetDirectoryName(PathArquivo);
            string FileName = System.IO.Path.GetFileName(PathArquivo);
            DirBase = Path.GetDirectoryName(DirBase);

            string[] OLDarrLinking = PathArquivo.Split(new char[] { Path.DirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);
            string[] OLDarrLinked = DirBase.Split(new char[] { Path.DirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);
            return OLDarrLinked[0] == OLDarrLinked[0];

        }

        /// <summary>
        /// Gets the relative path.
        /// </summary>
        /// <param name="DirBase">The dir base.</param>
        /// <param name="PathArquivo">The path arquivo.</param>
        /// <returns></returns>
        public static string GetRelativePath(string DirBase, string PathArquivo)
        {
            string FileName = System.IO.Path.GetFileName(PathArquivo);
            DirBase = System.IO.Path.GetDirectoryName(DirBase);
            PathArquivo = System.IO.Path.GetDirectoryName(PathArquivo);

            string[] OLDarrLinking = PathArquivo.Split(new char[] { Path.DirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);
            string[] OLDarrLinked = DirBase.Split(new char[] { Path.DirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);

            string[] listaDiretoriosDirBase = OLDarrLinked;
            string[] listaDiretoriosArquivo = OLDarrLinking;

            int intFolderCountLinking = OLDarrLinking.Length;
            int OLDintFolderCountLinking = intFolderCountLinking;


            int intFolderCountLinked = OLDarrLinked.Length;
            int OLDintFolderCountLinked = intFolderCountLinked;

            // counts the folders
            string prRelativePath = string.Empty;
            bool SameFolder = false;
            int QtdReferenciasParaRemover = 0;
            int copyOfintCounter = 0;
            //------------------------------------------------
            //------------------------------------------------
            //find last same root folder e.g. from c:\windows\system\test
            //and c:\windows\something c:\windows is last same root
            //compare from last element to first element

            for (int i = 0; i < listaDiretoriosDirBase.Length && i < listaDiretoriosArquivo.Length; i++)
            {

                if (listaDiretoriosDirBase[i] == listaDiretoriosArquivo[i])
                {
                    SameFolder = true;
                    QtdReferenciasParaRemover++;
                }
                else
                    break;
            }
            if (SameFolder && listaDiretoriosDirBase.Length == listaDiretoriosArquivo.Length) //exatly the same root
            {
                return FileName;//GetRelativePath = "";
            }

            //------------------------------------------------
            //------------------------------------------------
            copyOfintCounter = listaDiretoriosArquivo.Length-1; //last same folder
            //add the subfolders you have to "go" on e.g. test/test2/test3...

            while ((copyOfintCounter + 1) - QtdReferenciasParaRemover > 0)
            {
                if (listaDiretoriosArquivo[copyOfintCounter] != "")
                {
                    prRelativePath = Path.Combine(listaDiretoriosArquivo[copyOfintCounter], prRelativePath);
                }
                copyOfintCounter--;
            }
            ;

            
            //add the folders (../) you have to "go" out
            for (int i = OLDintFolderCountLinked - QtdReferenciasParaRemover; i > 0 ; i--)
            {
                prRelativePath = Path.Combine("..",  prRelativePath );
            }

            return System.IO.Path.Combine( prRelativePath, FileName);

        }
    }
}