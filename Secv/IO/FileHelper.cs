using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Acontep.IO
{
    /// <summary>
    /// Classe utilitária para o gerenciamento de arquivos no Acontep
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// Retorna o diretorio onde estão guardados os arquivos para transferência.
        /// Todos os arquivos nessa pasta estão passíveis de um "delete".
        /// Caso não exista, é criado o diretório.
        /// </summary>
        /// <returns></returns>
        public static string ObterPathRepositorioTransfer()
        {
            string path = System.Configuration.ConfigurationManager.AppSettings["PathRepositorioTransfer"];
            CriarPathSeNaoExitir(path);
            return path;
        }

        /// <summary>
        /// Retorna o diretorio onde estão guardados os arquivos para transferência.
        /// Todos os arquivos nessa pasta estão passíveis de um "delete".
        /// Caso não exista, é criado o diretório.
        /// </summary>
        /// <returns></returns>
        public static string ObterPathRepositorioTransfer(string combinarAoCaminho)
        {
            string caminho = ObterPathRepositorioTransfer();
            caminho = Path.Combine(caminho, combinarAoCaminho);
            CriarPathSeNaoExitir(caminho);
            return caminho;
        }
        
        /// <summary>
        /// Retorna o diretorio onde estão guardados os arquivos de cada sistema.
        /// Todos os arquivos nessa pasta sofreão backup.
        /// Caso não exista, é criado o diretório.
        /// </summary>
        /// <returns></returns>
        public static string ObterPathRepositorioSistemas(int sis_CodSis)
        {
            string path = Path.Combine(System.Configuration.ConfigurationManager.AppSettings["PathRepositorioSistemas"], sis_CodSis.ToString());
            CriarPathSeNaoExitir(path);
            return path;
        }

        /// <summary>
        /// Retorna o diretorio onde estão guardados os arquivos de cada sistema.
        /// Todos os arquivos nessa pasta sofreão backup.
        /// Caso não exista, é criado o diretório.
        /// </summary>
        /// <returns></returns>
        public static string ObterPathRepositorioSistemas(int sis_CodSis, string combinarAoCaminho)
        {
            string caminho = ObterPathRepositorioSistemas(sis_CodSis);
            caminho = Path.Combine(caminho, combinarAoCaminho);
            CriarPathSeNaoExitir(caminho);
            return caminho;
        }

        /// <summary>
        /// Retorna o diretorio onde estão guardados os arquivos de cada tabela. Por padrão, o nome do arquivo será o ID do registro da tabela.
        /// Todos os arquivos nessa pasta sofreão backup.
        /// Caso não exista, é criado o diretório.
        /// </summary>
        /// <returns></returns>
        public static string ObterPathRepositorioTabelas(string nomeTabela)
        {
            string path = Path.Combine(System.Configuration.ConfigurationManager.AppSettings["PathRepositorioTabelas"], nomeTabela);
            CriarPathSeNaoExitir(path);
            return path;
        }

        /// <summary>
        /// Retorna o path do arquivo. Caso não exista, retorna nulo.
        /// </summary>
        /// <param name="tabela">The tabela.</param>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static string ObterPathArquivo(string tabela, int id)
        {
            string path = ObterPathRepositorioTabelas(tabela);
            foreach (string arquivo in Directory.GetFiles(path, id + ".*"))
            {
                return arquivo;
            }
            return null;
        }

        /// <summary>
        /// Apaga o arquivo caso ele exista
        /// </summary>
        /// <param name="tabela">nome da tabela</param>
        /// <param name="id">id da tabela</param>
        public static void ApagarArquivo(string tabela, int id)
        {
            string path = ObterPathArquivo(tabela, id);
            if (path != null && File.Exists(path))
            {
                File.Delete(path);
            }
        }
        /// <summary>
        /// Atualizar o arquivo (apaga se ele existir e inclui o novo arquivo)
        /// </summary>
        /// <param name="tabela">nome da tabela</param>
        /// <param name="id">id do arquivo</param>
        /// <param name="original_fileName"></param>
        /// <param name="conteudo"></param>
        public static void AtualizarArquivo(string tabela, int id, string original_fileName, byte[] conteudo)
        {
            ApagarArquivo(tabela, id);
            if (conteudo.Length > 0)
            {
                string diretorio = ObterPathRepositorioTabelas(tabela);
                string caminho = Path.Combine(diretorio, id + Path.GetExtension(original_fileName));
                using (Stream file = new StreamWriter(caminho).BaseStream)
                {
                    file.Write(conteudo, 0, conteudo.Length);
                }
            }
        }
        /// <summary>
        /// Cria o diretório para a tabela caso ele não exista
        /// </summary>
        /// <param name="path"></param>
        public static void CriarPathSeNaoExitir(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
