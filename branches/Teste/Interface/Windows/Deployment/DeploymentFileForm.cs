using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;

namespace Deployment
{
    public partial class DeploymentFileForm : Form
    {
        public DeploymentFileForm()
        {
            InitializeComponent();
        }

        private void btnAlteracao_Click(object sender, EventArgs e)
        {
            if (tbxAlteracao.Text != string.Empty)
                fbdCaminho.SelectedPath = tbxAlteracao.Text;
            if (fbdCaminho.ShowDialog() == DialogResult.OK)
            {
                tbxAlteracao.Text = fbdCaminho.SelectedPath;
            }
        }

        private void btnBase_Click(object sender, EventArgs e)
        {
            if (tbxBase.Text != string.Empty)
                fbdCaminho.SelectedPath = tbxBase.Text;
            if (fbdCaminho.ShowDialog() == DialogResult.OK)
            {
                tbxBase.Text = fbdCaminho.SelectedPath;
            }
        }

        private void btnResultado_Click(object sender, EventArgs e)
        {
            if (tbxResultado.Text != string.Empty)
                fbdCaminho.SelectedPath = tbxResultado.Text;
            if (fbdCaminho.ShowDialog() == DialogResult.OK)
            {
                tbxResultado.Text = fbdCaminho.SelectedPath;
            }
        }

        private void btnComparar_Click(object sender, EventArgs e)
        {
            if (!limparPastaResultado())
                return;
            //if(tbxAlteracao.Text[tbxAlteracao.Text.Length-1] != '\\')
            //    tbxAlteracao.Text += @"\";
            foreach (string arquivos in Directory.GetFiles(tbxAlteracao.Text))
                comparaArquivo(arquivos);
            comparaPasta(Directory.GetDirectories(tbxAlteracao.Text));
            tbxLog.Lines = log.ToArray();
        }
        List<string> log = new List<string>();
        public void comparaPasta(string[] pastas)
        {
            foreach (string pasta in pastas)
            {
                if (Directory.GetDirectories(pasta).Length > 0)
                    comparaPasta(Directory.GetDirectories(pasta));
                string[] arquivos = Directory.GetFiles(pasta);
                if (arquivos.Length > 0)
                {
                    FileInfo info = new FileInfo(arquivos[0]);
                    string relativo = info.Directory.ToString().Substring(tbxAlteracao.Text.Length);
                    if (!Directory.Exists(tbxBase.Text + relativo))
                    {
                        Directory.CreateDirectory(tbxResultado.Text + relativo);
                        log.Add("Criado: " + tbxAlteracao.Text + relativo);
                    }
                    //else if(!Directory.Exists(tbxResultado.Text + relativo))
                    //    Directory.CreateDirectory(tbxResultado.Text + relativo);
                }
                foreach (string arquivo in arquivos)
                {                    
                    comparaArquivo(arquivo);                    
                }
            }
        }

        public void comparaArquivo(string arquivo)
        {            
            FileInfo info = new FileInfo(arquivo);
            string relativo = info.Directory.ToString().Substring(tbxAlteracao.Text.Length);//Caminho relativo - sem o caminho base 
            relativo = relativo == string.Empty ? @"\" : relativo; // caso seja arquivos da raiz
            //if (arquivo.Contains("FotografiaDeSucassoFn.cs"))
            //{
            //    arquivo.ToString();
            //}
            if (File.Exists(tbxBase.Text + Path.Combine(relativo,info.Name)))
            {
                if (!cbxModificar.Checked)
                    return;
                string dadosBase = new StreamReader(tbxBase.Text + Path.Combine(relativo,info.Name)).ReadToEnd();
                string dadosAlteracao = new StreamReader(tbxAlteracao.Text + Path.Combine(relativo,info.Name)).ReadToEnd();
                if (dadosBase != dadosAlteracao)
                {
                    if (info.LastWriteTime < new FileInfo(tbxBase.Text + Path.Combine(relativo, info.Name)).LastWriteTime)
                    {
                        if (cbxArqDifDatDifAviso.Checked)
                        {
                            if (MessageBox.Show("O arquivo de alteração é mais antigo que o base, deseja sobre escrever?", "Aviso", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                log.Add("Modificado: " + tbxAlteracao.Text + relativo + info.Name + " - Arquivo de altreração mas antigo");
                                CriarArquivo(tbxAlteracao.Text + Path.Combine(relativo, info.Name), tbxResultado.Text + relativo, info.Name);
                            }
                        }
                        else
                        {
                            log.Add("Modificado: " + tbxAlteracao.Text + relativo + info.Name + " - Arquivo de altreração mas antigo");
                            CriarArquivo(tbxAlteracao.Text + Path.Combine(relativo, info.Name), tbxResultado.Text + relativo, info.Name);
                        }
                    }
                    else
                    {
                        log.Add("Modificado: " + tbxAlteracao.Text + relativo + info.Name);
                        CriarArquivo(tbxAlteracao.Text + Path.Combine(relativo, info.Name), tbxResultado.Text + relativo, info.Name);
                    }
                }
                else
                {
                    if (info.LastWriteTime < new FileInfo(tbxBase.Text + Path.Combine(relativo, info.Name)).LastWriteTime)
                    {
                        if (cbxDatDifAviso.Checked)
                        {
                            if (MessageBox.Show("O arquivo de alteração tem data de modificação diferente apesar de aprarentimente ter a mesma informação, deseja sobre escrever?", "Aviso", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                CriarArquivo(tbxAlteracao.Text + Path.Combine(relativo, info.Name), tbxResultado.Text + relativo, info.Name);
                                log.Add("Aviso-Modificado: " + tbxAlteracao.Text + relativo + info.Name + ", tem data de modificação diferentes");
                            }
                        }
                        else
                        {
                            if (cbxArqIgualDatMenor.Checked)
                            {
                                CriarArquivo(tbxAlteracao.Text + Path.Combine(relativo, info.Name), tbxResultado.Text + relativo, info.Name);
                                log.Add("Aviso-Modificado: " + tbxAlteracao.Text + relativo + info.Name + ", tem data de modificação diferentes");
                            }
                        }
                    }
                    else if (info.LastWriteTime > new FileInfo(tbxBase.Text + Path.Combine(relativo, info.Name)).LastWriteTime)
                    {
                        if (cbxArqIgualDatMaior.Checked)
                        {
                            CriarArquivo(tbxAlteracao.Text + Path.Combine(relativo, info.Name), tbxResultado.Text + relativo, info.Name);
                            log.Add("Aviso-Modificado: " + tbxAlteracao.Text + relativo + info.Name + ", tem data de modificação diferentes");
                        }
                    }
                }
            }
            else
            {
                if (cbxCriar.Checked)
                {
                    CriarArquivo(tbxAlteracao.Text + Path.Combine(relativo, info.Name), tbxResultado.Text + relativo, info.Name);
                    log.Add("Criado: " + tbxAlteracao.Text + relativo + info.Name);
                }
            }
        }

        private bool limparPastaResultado()
        {
            if (!Directory.Exists(tbxResultado.Text))
                Directory.CreateDirectory(tbxResultado.Text);
            else if (Directory.GetDirectories(tbxResultado.Text).Length > 0 || Directory.GetFiles(tbxResultado.Text).Length > 0)
            {
                if (MessageBox.Show("Existe dados dentro da pasta, deseja limpar?", "Erro", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Directory.Delete(tbxResultado.Text);
                    Directory.CreateDirectory(tbxResultado.Text);
                }
                else
                {
                    tbxLog.Text = "Pasta de resultado com dados.";
                    return false;
                }
            }
            return true;
        }

        private void DeplymentFileForm_Load(object sender, EventArgs e)
        {
            //tbxAlteracao.Text = Application.StartupPath;
            //tbxBase.Text = Application.StartupPath;
            //tbxResultado.Text = Application.StartupPath;
        }

        private void CriarArquivo(string arquivo, string para, string nome)
        {
            if (!Directory.Exists(para))
                Directory.CreateDirectory(para);
            File.Copy(arquivo, Path.Combine(para,nome));
        }
    }
}