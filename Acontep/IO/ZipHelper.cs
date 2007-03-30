using System;
using System.Collections.Generic;
using System.Text;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.GZip;
using System.IO;


namespace Acontep.IO
{
    /// <summary>
    /// Summary description for HelpZip.
    /// </summary>
    public class HelpZip
    {
        /// <summary>
        /// Zipa um Arquivo ou um diretorio
        /// </summary>
        /// <param name="pOrigem">Informa um arquivo ou diretorio a ser zipado</param>
        /// <param name="pArquivoDestino">Informa o nome do arquivo zipado</param>
        /// <param name="MantarEstruturaPastas">Se true mantem a hierarquia original de pastas, caso contrario compacta tudo em um unico nivel no arquivo zipado</param>
        public static void ZipFile(string pOrigem, string pArquivoDestino, bool MantarEstruturaPastas)
        {
            string[] lstfilenames = new string[] { };
            //string lNovoNome;

            if (Directory.Exists(pOrigem))
                lstfilenames = Directory.GetFiles(pOrigem);
            else if (File.Exists(pOrigem))
                lstfilenames = new String[] { pOrigem };
            else
                throw new FileNotFoundException("A origem nao encontrada ou vazia(se for um diretorio, gera erro).", pArquivoDestino);

            ZipFile(lstfilenames, pArquivoDestino, MantarEstruturaPastas);
        }
        /// <summary>
        /// Zipa os arquivos
        /// </summary>
        /// <param name="lstfilenames">lista de arquivos</param>
        /// <param name="pArquivoDestino">arquivo de destino(arquivo compactado)</param>
        public static Stream ZipFile(string[] lstfilenames, string pArquivoDestino)
        {
            return ZipFile(lstfilenames, pArquivoDestino, false);
        }
        /// <summary>
        /// Zipa os arquivos
        /// </summary>
        /// <param name="lstfilenames">lista de arquivos</param>
        /// <param name="pArquivoDestino">arquivo de destino(arquivo compactado)</param>
        /// <param name="MantarEstruturaPastas">Mantem toda a estrutura de pasta onde os arquivos estão</param>
        private static Stream ZipFile(string[] lstfilenames, string pArquivoDestino, bool MantarEstruturaPastas)
        {
            string lNovoNome;
            Crc32 lcrc = new Crc32();
            FileStream lFileDestino = File.Create(pArquivoDestino);
            ZipOutputStream lZipOutputStream = new ZipOutputStream(lFileDestino);
            //FileStream lFileDestino = File.Open(pArquivoDestino, FileMode.OpenOrCreate);

            lZipOutputStream.SetLevel(9); // 0 - store only to 9 - means best compression
            try
            {

                foreach (string lfile in lstfilenames)
                {

                    FileStream lfs = File.OpenRead(lfile);

                    byte[] buffer = new byte[lfs.Length];
                    lfs.Read(buffer, 0, buffer.Length);
                    if (!MantarEstruturaPastas)
                    {
                        FileInfo lFileInfo = new FileInfo(lfile);
                        lNovoNome = lFileInfo.Name;
                    }
                    else
                    {
                        lNovoNome = lfile;
                    }

                    ZipEntry lentry = new ZipEntry(lNovoNome);

                    lentry.DateTime = DateTime.Now;

                    // set Size and the crc, because the information
                    // about the size and crc should be stored in the header
                    // if it is not set it is automatically written in the footer.
                    // (in this case size == crc == -1 in the header)
                    // Some ZIP programs have problems with zip files that don't store
                    // the size and crc in the header.
                    lentry.Size = lfs.Length;
                    lfs.Close();

                    lcrc.Reset();
                    lcrc.Update(buffer);

                    lentry.Crc = lcrc.Value;

                    lZipOutputStream.PutNextEntry(lentry);

                    lZipOutputStream.Write(buffer, 0, buffer.Length);

                }
            }
            finally
            {
                lZipOutputStream.Finish();
                lZipOutputStream.Close();                
            }
            return lZipOutputStream;
        }

        /// <summary>
        /// Zipa um Arquivo ou um diretorio
        /// </summary>
        /// <param name="pOrigem">Informa um arquivo ou diretorio a ser zipado</param>
        /// <param name="pArquivoDestino">Informa o nome do arquivo zipado</param>
        public static void ZipFile(string pOrigem, string pArquivoDestino)
        {            
            ZipFile(pOrigem, pArquivoDestino, false);
        }
        /// <summary>
        /// Zipa os dados
        /// </summary>
        /// <param name="pDadosOrigem">Dados a serem compactados</param>
        /// <param name="pNomeArquivoOrigem">Nome do aquivo atribuidos aso dados</param>
        /// <param name="pNomeArquivoDestino">arquivo de destino(arquivo compactado)</param>
        /// <returns></returns>
        public static Stream ZipFile(Stream pDadosOrigem, string pNomeArquivoOrigem, string pNomeArquivoDestino)
        {
            Crc32 lcrc = new Crc32();
            System.IO.MemoryStream lFileDestino = new MemoryStream();

            ZipOutputStream lZipOutputStream = new ZipOutputStream(lFileDestino);
            lZipOutputStream.SetLevel(9); // 0 - store only to 9 - means best compression
            try
            {
                ZipEntry lentry = new ZipEntry(pNomeArquivoOrigem);
                lentry.DateTime = DateTime.Now;

                // set Size and the crc, because the information
                // about the size and crc should be stored in the header
                // if it is not set it is automatically written in the footer.
                // (in this case size == crc == -1 in the header)
                // Some ZIP programs have problems with zip files that don't store
                // the size and crc in the header.

                byte[] buffer = new byte[pDadosOrigem.Length];
                pDadosOrigem.Read(buffer, 0, buffer.Length);


                lentry.Size = buffer.Length;

                lcrc.Reset();
                lcrc.Update(buffer);
                lentry.Crc = lcrc.Value;
                lZipOutputStream.PutNextEntry(lentry);
                lZipOutputStream.Write(buffer, 0, buffer.Length);
                lZipOutputStream.CloseEntry();
            }
            finally
            {
                lZipOutputStream.Finish();
                lZipOutputStream.Close();
            }
            return lZipOutputStream;
        }

        /// <summary>
        /// Desconpacta o arquivo
        /// </summary>
        /// <param name="pArquivoZipado">caminho ao arquivo a ser descompactado</param>
        /// <param name="pDiretorioDestino">diretorio de destino dos arquivos descompactados</param>
        public static void UnZip(string pArquivoZipado, string pDiretorioDestino)
        {
            if (!System.IO.File.Exists(pArquivoZipado))
            {
                throw new System.IO.FileNotFoundException("O arquivo " + pArquivoZipado + " não foi encontrado. UnZip cancelado.");
            }

            FileStream lFile = File.OpenRead(pArquivoZipado);
            UnZip(lFile, pDiretorioDestino);

        }
        /// <summary>
        /// Desconpacta o arquivo
        /// </summary>
        /// <param name="pArquivoZipado">caminho ao arquivo a ser descompactado</param>
        /// <param name="pDiretorioDestino">diretorio de destino dos arquivos descompactados</param>
        /// <param name="mesmaPasta"></param>
        public static void UnZip(string pArquivoZipado, string pDiretorioDestino, bool mesmaPasta)
        {
            if (!System.IO.File.Exists(pArquivoZipado))
            {
                throw new System.IO.FileNotFoundException("O arquivo " + pArquivoZipado + " não foi encontrado. UnZip cancelado.");
            }

            FileStream lFile = File.OpenRead(pArquivoZipado);
            UnZip(lFile, mesmaPasta, pDiretorioDestino);

        }
        /// <summary>
        /// Desconpacta o arquivo
        /// </summary>
        /// <param name="File">stream do arquivo a ser descompactado</param>
        /// <param name="pDiretorioDestino">diretorio de destino dos arquivos descompactados</param>
        public static void UnZip(FileStream File, string pDiretorioDestino)
        {
            UnZip(File, true, pDiretorioDestino);
        }
        /// <summary>
        /// Desconpacta o arquivo
        /// </summary>
        /// <param name="File">stream do arquivo a ser descompactado</param>
        /// <param name="mesmaPasta">define se descompacta os ignorando a estrutura de pastas do arquivo compactado e colocando no <paramref name="pDiretorioDestino"/></param>
        /// <param name="pDiretorioDestino">diretorio de destino dos arquivos descompactados</param>
        public static void UnZip(FileStream File, bool mesmaPasta, string pDiretorioDestino )
        {
            UnZip(File, pDiretorioDestino, true, mesmaPasta);
        }
        /// <summary>
        /// Desconpacta o arquivo
        /// </summary>
        /// <param name="arquivo">stream do arquivo a ser descompactado</param>
        /// <param name="pDiretorioDestino">diretorio de destino dos arquivos descompactados</param>
        /// <param name="CloseFile">define se o stream do arquivo deve ser fechado apos a descompactação</param>
        public static void UnZip(FileStream arquivo, string pDiretorioDestino, bool CloseFile)
        {
            using (ZipInputStream s = new ZipInputStream(arquivo))
            {
                ZipEntry theEntry;

                string diretorio = pDiretorioDestino;
                string nomeArquivo = String.Empty;
                while ((theEntry = s.GetNextEntry()) != null)
                {

                    string[] directories = theEntry.Name.Replace(':','_').Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                    diretorio = Path.Combine(pDiretorioDestino, string.Join(Path.DirectorySeparatorChar.ToString(), directories));
                    nomeArquivo = Path.GetFileName(diretorio);
                    diretorio = Path.GetDirectoryName(diretorio);
                        if (!Directory.Exists(diretorio))
                        {
                            Directory.CreateDirectory(diretorio);
                        }
                        if (theEntry.IsDirectory)
                        {
                            continue;
                        }
                    using (FileStream streamWriter = File.Open(Path.Combine(diretorio, Path.GetFileName(nomeArquivo)), FileMode.OpenOrCreate) )
                    {

                        int size = 2048;
                        byte[] data = new byte[2048];
                        while (true)
                        {
                            size = s.Read(data, 0, data.Length);
                            if (size > 0)
                            {
                                streamWriter.Write(data, 0, size);
                            }
                            else
                            {
                                break;
                            }
                        }
                        streamWriter.Flush();
                    }
                }
            }

            if (CloseFile)
                arquivo.Close();
        }
        /// <summary>
        /// Desconpacta o arquivo
        /// </summary>
        /// <param name="arquivo"> stream do arquivo a ser descompactado</param>
        /// <param name="pDiretorioDestino">diretorio de destino dos arquivos descompactados</param>
        /// <param name="CloseFile">define se o stream do arquivo deve ser fechado apos a descompactação</param>
        /// <param name="mesmaPasta">define se descompacta os ignorando a estrutura de pastas do arquivo compactado e colocando no <paramref name="pDiretorioDestino"/> </param>
        public static void UnZip(FileStream arquivo, string pDiretorioDestino, bool CloseFile, bool mesmaPasta)
        {
            using (ZipInputStream s = new ZipInputStream(arquivo))
            {
                ZipEntry theEntry;

                string diretorio = pDiretorioDestino;
                string nomeArquivo = String.Empty;
                while ((theEntry = s.GetNextEntry()) != null)
                {

                    string[] directories = theEntry.Name.Replace(':', '_').Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                    string CaminhoValidoParaWindows = string.Join(Path.DirectorySeparatorChar.ToString(), directories);
                    if (!mesmaPasta)
                    {
                        diretorio = Path.Combine(pDiretorioDestino, CaminhoValidoParaWindows);
                    }
                    else
                    {
                        if (theEntry.IsDirectory)
                        {
                            continue;
                        }
                        diretorio = Path.Combine(pDiretorioDestino, Path.GetFileName(CaminhoValidoParaWindows));
                    }
                    nomeArquivo = Path.GetFileName(diretorio);
                    
                    diretorio = Path.GetDirectoryName(diretorio);
                    if (!Directory.Exists(diretorio))
                    {
                        Directory.CreateDirectory(diretorio);
                    }
                    
                    using (FileStream streamWriter = File.Open(Path.Combine(diretorio, Path.GetFileName(nomeArquivo)), FileMode.Create))
                    {

                        int size = 2048;
                        byte[] data = new byte[2048];
                        while (true)
                        {
                            size = s.Read(data, 0, data.Length);
                            if (size > 0)
                            {
                                streamWriter.Write(data, 0, size);
                            }
                            else
                            {
                                break;
                            }
                        }
                        streamWriter.Flush();
                    }
                }
            }

            if (CloseFile)
                arquivo.Close();
        }

        /// <summary>
        /// Desconpacta o arquivo
        /// </summary>
        /// <param name="pArquivoZipado">caminho ao arquivo a ser descompactado</param>
        /// <param name="pDiretorioDestino">diretorio de destino dos arquivos descompactados</param>
        public static void UnZipCCFV(string pArquivoZipado, string pDiretorioDestino)
        {
            if (!System.IO.File.Exists(pArquivoZipado))
            {
                throw new System.IO.FileNotFoundException("O arquivo " + pArquivoZipado + " nao foi localizado. UnZip arquivo cancelada");
            }

            FileStream lFile = File.OpenRead(pArquivoZipado);
            ZipInputStream s = new ZipInputStream(lFile);
            try
            {

                ZipEntry theEntry;
                while ((theEntry = s.GetNextEntry()) != null)
                {

                    FileStream streamWriter = File.Create(pDiretorioDestino + @"\" + Path.GetFileName(theEntry.Name));

                    int size = 2048;
                    byte[] data = new byte[2048];
                    while (true)
                    {
                        size = s.Read(data, 0, data.Length);
                        if (size > 0)
                        {
                            streamWriter.Write(data, 0, size);
                        }
                        else
                        {
                            break;
                        }
                    }

                    streamWriter.Close();
                }

            }
            finally
            {
                lFile.Close();
                s.Close();

            }

        }

    }
}
