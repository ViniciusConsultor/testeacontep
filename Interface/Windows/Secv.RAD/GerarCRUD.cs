using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Secv.RAD.Properties;
using Acontep.Ng.Manutencao.Rad;

namespace Acontep.RAD
{
    public enum Passos : int
    {
        Conexao = 0,
        Tabela,        
        Campos,
        Resultado
        
    }
    public partial class GerarCRUD : Form
    {

        #region variaveis

      
        GeradorSECV _GeradorShema = null;
        internal GeradorSECV GeradorShema
        {
            get {
                if (_GeradorShema == null)
                {
                    if ( toolStripMenuGerarFachadaNegocio.Checked )// cbxGerarNegocio.Checked)
                        GeradorShema = new GerarFachadaNegocio();
                    else 
                        GeradorShema = new GeradorCRUD();
                    this.GeradorShema.Conectar(StringConexaoTextBox.Text, ProviderComboBox.Text);
                }

                return _GeradorShema; }
            set { _GeradorShema = value; }
        }

        string _TabelaSelecionada = string.Empty;
        public string TabelaSelecionada
        {
            get { return GeradorShema.Tabela; }
            set
            {
                //if (string.IsNullOrEmpty(value))
                //    throw new NullReferenceException("A tabela não pode ser nula");
                GeradorShema.Tabela = value;
            }
        }
        public EstruturaSaveRAD.ClasseCRUDRow ClasseSelecionada
        {
            get
            {
                if (cmbTabela.SelectedItem == null) return null;
                return ((EstruturaSaveRAD.ClasseCRUDRow)((DataRowView)cmbTabela.SelectedItem).Row);
            }
        }
        EstruturaSaveRAD Conteudo
        {
            get
            {
                return EstruturaSaveRadUtil<EstruturaSaveRAD>.ConteudoClasse;
            }
            set
            {
                EstruturaSaveRadUtil<EstruturaSaveRAD>.ConteudoClasse = value;
            }
        }
        #endregion variaveis

        public GerarCRUD()
        {
            InitializeComponent();
        }
        private void Navegacao(object sender, EventArgs e)
        {
            try
            {
                if (sender is ToolStripDropDownButton)
                {
                    AntesDeAndarNaNavegacao();
                    if (((ToolStripDropDownButton)sender).Name == btnProximo.Name)
                    {
                        AndarNaNavegacao(PassoAtual + 1);
                    }
                    if (((ToolStripDropDownButton)sender).Name == btnVoltar.Name)
                    {
                        AndarNaNavegacao(PassoAtual - 1);
                    }
                }
            }
            catch (Exception Erro)
            {
                AppUtil.ExibirErro(Erro);
            }

        }

        

        private Passos PassoAtual
        {
            get
            {
                return (Passos)tabControl.SelectedIndex;
            }
        }

        /// <summary>
        /// Realiza as configuracoes antes de ir para o prox. passo
        /// </summary>
        private void AntesDeAndarNaNavegacao()
        {
            switch (PassoAtual)
            {
                case Passos.Conexao:
                    {
                        GeradorShema = null;                        
                        break;
                    }
                case Passos.Tabela:
                    {
                        if (string.IsNullOrEmpty(edtNameSpace.Text))
                        {
                            edtNameSpace.Focus();
                            throw new ApplicationException("Por favor, informe o namespace.");
                        }
                        GuardarListaTabelasSelecionadas();                        
                        break;
                    }
                case Passos.Campos:
                    {
                        AtualizarDataSetComMetodosSelecionados();
                        break;
                    }
                case Passos.Resultado:
                    {
                        break;
                    }            
            }
        }

        private void AtualizarDataSetComMetodosSelecionados()
        {            
        }

        /// <summary>
        /// Vai para o passo informado.
        /// </summary>
        /// <param name="passos"></param>
        private void AndarNaNavegacao(Passos passos)
        {
            btnVoltar.Enabled = passos > Passos.Conexao;
            btnProximo.Enabled = passos < Passos.Resultado;
            btnFinalizar.Enabled = passos > Passos.Conexao && passos < Passos.Resultado;
            toolStripMenuGerarFachadaNegocio.Enabled = btnProximo.Enabled;
            try
            {
                switch (passos)
                {
                    case Passos.Conexao:
                        {
                            break;
                        }

                    case Passos.Tabela:
                        {
                            AtualizarConexoes();
                            RecuperarListaTabelasDoBanco("%%");
                            break;
                        }
                    case Passos.Campos:
                        {

                            MontarCamposTabela();
                            //MontarTelaCampos();
                            break;
                        }
                    case Passos.Resultado:
                        {
                            MontarResultado();
                            break;
                        }
                }
            }
            finally
            {
                tabControl.SelectedIndex = (int)passos;
            }
        }

        /// <summary>
        /// Atualiza as conexoes das classes.
        /// </summary>
        private void AtualizarConexoes()
        {
            foreach (EstruturaSaveRAD.ClasseCRUDRow classe in Conteudo.ClasseCRUD.Rows)
            {
                if (classe.RowState != DataRowState.Deleted)
                {
                    classe.Provider = ProviderComboBox.Text;
                    classe.StringConexao = StringConexaoTextBox.Text;
                }

            }
        }

        /// <summary>
        /// Monta o resultado.
        /// </summary>
        private void MontarResultado()
        {
            string msgOut = String.Empty;
            edtResultado.Text = GeradorShema.GerarClasses(true, Conteudo, ClasseSelecionada, out msgOut);
            if (!string.IsNullOrEmpty(msgOut))
            {
                MessageBox.Show(msgOut, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// Limpa a lista de campos checados
        /// </summary>
        /// <param name="Lista">Lista que deseja desmarcar os itens</param>
        private void AlterarEstadoDosCampos(CheckedListBox Lista, CheckState CheckState)
        {
            for (int i = 0; i < Lista.Items.Count; i++)
            {
                Lista.SetItemCheckState(i, CheckState);
            }
        }
        /// <summary>
        /// Procura o valor dentro da lista e marca com o estado informado
        /// </summary>
        /// <param name="Lista"></param>
        /// <param name="Valor"></param>
        /// <param name="State"></param>
        void SetItemCheckState(CheckedListBox Lista, string Valor, CheckState State)
        {
            int indice = Lista.FindStringExact(Valor);
            if (indice >= 0)
                Lista.SetItemCheckState(indice, State);            
        }

        /// <summary>
        /// Dada uma lista de campos, procura e marca os itens...
        /// </summary>
        /// <param name="listaCamposRow">Lista de campos que devem estar marcados depois de executada esta operação</param>
        private void SetarCamposSelecionados(EstruturaSaveRAD.ListaCamposRow[] listaCamposRow)
        {
            AlterarEstadoDosCampos(chkCamposTabela, CheckState.Unchecked);
            foreach (EstruturaSaveRAD.ListaCamposRow lcr in listaCamposRow)
            {
                SetItemCheckState(chkCamposTabela, lcr.Nome, CheckState.Checked);
            }
        }

        /// <summary>
        /// Mosta os campos da tabela selecionada
        /// </summary>
        private void MontarCamposTabela()
        {
            try
            {
                chkCamposTabela.Items.Clear();
                // Preechendo o CheckListBox com os campos da tabela já selecionados

                if (ClasseSelecionada != null)
                {

                    DataTable dtTabela = GeradorShema.RetornarMetaDados(ClasseSelecionada.Provider, ClasseSelecionada.StringConexao, ClasseSelecionada.Tabela);

                    foreach (DataColumn row in dtTabela.Columns)
                    {
                        chkCamposTabela.Items.Add(row, true);
                    }
                }
            }
            catch (AcontepException erro)
            {
                MessageBox.Show(erro.Message);
            }
        }
        /// <summary>
        /// Monta a lista de metodos da tabela selecionada
        /// </summary>
        private void MontarListaMetodos()
        {
            chkMetodos.Items.Clear();
            EstruturaSaveRAD.ClasseCRUDRow[] classes = (EstruturaSaveRAD.ClasseCRUDRow[])Conteudo.ClasseCRUD.Select(string.Format("Tabela = '{0}'", TabelaSelecionada));
            if (classes.Length == 1)
            {
                // Preechendo o CheckListBox com os campos da tabela já selecionados
                foreach (EstruturaSaveRAD.MetodoRow row in classes[0].GetMetodoRows())
                {
                    chkMetodos.Items.Add(row, false);
                }
            }
        }
  

        /// <summary>
        /// Guarda a lista de tabelas selecionadas.
        /// </summary>
        private void GuardarListaTabelasSelecionadas()
        {
            IDictionary<string, DataRow> TabelasSelecionadas = new  Dictionary<string, DataRow>();

            foreach (DataRow tabela in chkTabelas.CheckedItems)
            {
                TabelasSelecionadas.Add(ObterNomeTabela(tabela), tabela);
            }
            int qtd = Conteudo.ClasseCRUD.Count-1;
            //estruturaSaveRAD.ClasseCRUD.BeginLoadData();
            try
            {
                #region Verificação e atualizacao das tabelas
                while (qtd >= 0)
                {
                    EstruturaSaveRAD.ClasseCRUDRow classe = Conteudo.ClasseCRUD[qtd];
                    if (classe.RowState != DataRowState.Deleted)
                    {
                        if (TabelasSelecionadas.ContainsKey(classe.Tabela))
                        {
                            TabelasSelecionadas.Remove(classe.Tabela);
                        }
                    }
                    qtd--;
                }
                #endregion

                foreach (string NomeNovaTabela in TabelasSelecionadas.Keys)
                {
                    this.CriarClasse(null, NomeNovaTabela);
                }
            }
            finally
            {
                cmbTabela.DataSource = Conteudo.ClasseCRUD;
                //TabelaComboBox_SelectedIndexChanged(null, null);
            }
        }

        /// <summary>
        /// Recupera a lista de tabelas existentes no banco;
        /// </summary>
        private void RecuperarListaTabelasDoBanco( string Filtro )
        {
            chkTabelas.Items.Clear();
            foreach (DataRow row in GeradorShema.GetTables().Select(string.Format(" TABLE_SCHEMA + '.' + TABLE_NAME like '{0}'", Filtro)))
            {
                chkTabelas.Items.Add(row, Conteudo.ClasseCRUD.Select(string.Format("Tabela ='{0}'", ObterNomeTabela(row) )).Length>0);
            }
        }

        private void removerMetodoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (chkMetodos.CheckedItems.Count == 0)
            {
                MessageBox.Show("Selecione um método a ser removido.");
            }
            for (int i = chkMetodos.CheckedItems.Count - 1; i >= 0; i--)
            {
                ApagarMetodo(chkMetodos.CheckedItems[i] as EstruturaSaveRAD.MetodoRow);               
            }
            MontarListaMetodos();
        }

        /// <summary>
        /// Apagar o metodo.
        /// </summary>
        /// <param name="Metodo">The metodo.</param>
        private void ApagarMetodo(EstruturaSaveRAD.MetodoRow Metodo)
        {
            if (Metodo == null)
                return;
            DataRow[] Metodos = Conteudo.Metodo.Select("ID='" + Metodo.ID + "'");
            if ( Metodos.Length > 0 )
                Metodos[0].Delete();
        }
        /// <summary>
        /// Apaga os campos do metodo.
        /// </summary>
        /// <param name="Campos">The campos.</param>
        private void ApagarCamposDoMetodo(EstruturaSaveRAD.ListaCamposRow[] Campos)
        {
            foreach (EstruturaSaveRAD.ListaCamposRow lcr in Campos)
            {
                lcr.Delete();
            }
        }

        /// <summary>
        /// Cria ou atualiza a classe.
        /// </summary>
        /// <param name="CRW">The CRW.</param>
        /// <param name="NomeTabela">The nome tabela.</param>
        private EstruturaSaveRAD.ClasseCRUDRow CriarClasse(EstruturaSaveRAD.ClasseCRUDRow CRW, string NomeTabela)
        {
            EstruturaSaveRAD.ClasseCRUDRow crw = CRW;
            if (crw == null)
            {
                crw = Conteudo.ClasseCRUD.NewClasseCRUDRow();
                crw.ID = Guid.NewGuid();
            }
            crw.NameSpace = this.edtNameSpace.Text;
            string[] provaveisNomesClasse = NomeTabela.Split('.');

            crw.NomeClasse = provaveisNomesClasse[provaveisNomesClasse.Length-1]  + Settings.Default.TerminacaoClasse;
            crw.StringConexao = this.StringConexaoTextBox.Text;
            crw.Provider = this.ProviderComboBox.SelectedItem.ToString();
            crw.Tabela = NomeTabela;
            if ( CRW == null )
                Conteudo.ClasseCRUD.AddClasseCRUDRow(crw);
            return crw;
        }
        /// <summary>
        /// Adiciona ou atualiza a classe row
        /// </summary>
        /// <param name="CCR"></param>
        private EstruturaSaveRAD.MetodoRow CriarMetodo(EstruturaSaveRAD.MetodoRow MRW)
        {
            EstruturaSaveRAD.MetodoRow mrw = MRW;
            if (mrw == null)
            {
                mrw = Conteudo.Metodo.NewMetodoRow();
                mrw.ID = Guid.NewGuid();
            }
            mrw.NomeMetodo = edtNomeMetodo.Text;
            mrw.ClasseCRUDRow = ((EstruturaSaveRAD.ClasseCRUDRow)((DataRowView)cmbTabela.SelectedItem).Row);
            mrw.Opcoes = opcoesCRUDCampos.RetornarOpcoesCRUD();
            if (MRW == null)
                Conteudo.Metodo.AddMetodoRow(mrw);
            ApagarCamposDoMetodo(mrw.GetListaCamposRows());

            foreach (DataColumn nomeColuna in chkCamposTabela.CheckedItems)
            {
                EstruturaSaveRAD.ListaCamposRow lcr = Conteudo.ListaCampos.NewListaCamposRow();
                lcr.MetodoRow = mrw;
                lcr.Nome = nomeColuna.ColumnName;
                lcr.ID = Guid.NewGuid();
                lcr.IDMetodo = mrw.ID;
                Conteudo.ListaCampos.AddListaCamposRow(lcr);
            }
            return mrw;
        }
        private EstruturaSaveRAD.MetodoRow MetodoSelecionado
        {
            get
            {
                return chkMetodos.SelectedItem as EstruturaSaveRAD.MetodoRow;
            }
            set
            {
                chkMetodos.SelectedItem = value;
            }
        }
        private void adicionarMetodoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EstruturaSaveRAD.MetodoRow metodo = MetodoSelecionado;
            bool Add = metodo == null;
            
            metodo = CriarMetodo(metodo);
            if ( Add )
                chkMetodos.Items.Add(metodo);
            chkMetodos.ClearSelected();
        }

        private void chkMetodos_Format(object sender, ListControlConvertEventArgs e)
        {
            try
            {
                EstruturaSaveRAD.MetodoRow metodo = ((EstruturaSaveRAD.MetodoRow)e.ListItem);
                if (metodo.RowState != DataRowState.Detached)
                    e.Value = string.Format("{0} - {1}", metodo.NomeMetodo, metodo.Opcoes);
            }
            catch (Exception erro)
            {
                AppUtil.ExibirErro(erro);
            }
        }

        private void chkCamposTabela_Format(object sender, ListControlConvertEventArgs e)
        {
            e.Value = ((DataColumn)e.ListItem).ColumnName;
        }

        private void chkMetodos_SelectedIndexChanged(object sender, EventArgs e)
        {
            ExibirMetodoSelecionado();
        }

        /// <summary>
        /// Exibir detalhes do metodo selecionado.
        /// </summary>
        void ExibirMetodoSelecionado()
        {
            if (MetodoSelecionado == null) return;

            this.edtNomeMetodo.Text = MetodoSelecionado.NomeMetodo;

            SetarCamposSelecionados(MetodoSelecionado.GetListaCamposRows());
            this.opcoesCRUDCampos.SetarOpcoes(MetodoSelecionado.Opcoes);
        }

        private void chkMetodos_DoubleClick(object sender, EventArgs e)
        {
            chkMetodos.ClearSelected();
        }
       
        private void chkTabelas_Format(object sender, ListControlConvertEventArgs e)
        {
            DataRow linhaTabela = ((DataRow)e.ListItem);

            e.Value = ObterNomeTabela(linhaTabela);            
        }

        private static string ObterNomeTabela(DataRow linhaTabela)
        {
            return string.Format("{0}.{1}", linhaTabela["TABLE_SCHEMA"], linhaTabela["TABLE_NAME"]);
        }

        private void TabelaComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.TabelaSelecionada = cmbTabela.Text;
            if (cmbTabela.SelectedItem != null)
            {
                edtNomeClasse.Text = ((EstruturaSaveRAD.ClasseCRUDRow)((DataRowView)cmbTabela.SelectedItem).Row).NomeClasse;
            }
            else
            {
                edtNomeClasse.Text = string.Empty;                
            }
            MontarCamposTabela();
            MontarListaMetodos();
            MontarMenuAcaoCampos();
        }

        private void MontarMenuAcaoCampos()
        {
            bool enable = cmbTabela.Items.Count > 0;
            toolStripDropDownButtonRemover.Enabled = enable;
            adicionarMetodoToolStripMenuItem.Enabled = enable;
            toolStripDropDownButtonMarcar.Enabled = enable;
        }

        private void TabelaComboBox_Format(object sender, ListControlConvertEventArgs e)
        {
            e.Value = ((EstruturaSaveRAD.ClasseCRUDRow)((DataRowView)e.ListItem).Row).Tabela;
        }

        

        private void GerarCRUD_Load(object sender, EventArgs e)
        {
            toolStripMetodo.Visible = true;
            opcoesCRUDCampos.SetarOpcoes("DUSI");
            CarregarInformacoesLidasDoArquivo();
            AndarNaNavegacao(Passos.Conexao);
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetarNovaVersao();
            EstruturaSaveRadUtil<EstruturaSaveRAD>.SalvarArquivo();
            Conteudo.Clear();
            tabControl.SelectedIndex = 0;
        }

      
        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (EstruturaSaveRadUtil<EstruturaSaveRAD>.AbrirArquivo(true))
                    CarregarInformacoesLidasDoArquivo();                
            }
            catch (Exception erro)
            {
                AppUtil.ExibirErro(erro);                
            }
        }


        /// <summary>
        /// Carregars the informacoes lidas do arquivo.
        /// </summary>
        private void CarregarInformacoesLidasDoArquivo()
        {
            tabControl.SelectedIndex = 0;
            if (Conteudo.ClasseCRUD.Count > 0)
            {
                edtNameSpace.Text = Conteudo.ClasseCRUD[0].NameSpace;
                StringConexaoTextBox.Text = Conteudo.ClasseCRUD[0].StringConexao;
                ProviderComboBox.Text = Conteudo.ClasseCRUD[0].Provider;
                AtualizarArquivoParaNovaVersao();
            }
            else
            {
                edtNameSpace.Text = Settings.Default.NameSpaceDefault;
            }            
        }

        private void AtualizarArquivoParaNovaVersao()
        {
            if (Conteudo.Versao.Count == 0)
            {
                foreach (EstruturaSaveRAD.ClasseCRUDRow tabela in Conteudo.ClasseCRUD)
                {
                    if (tabela.Tabela.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries).Length < 2)
                        tabela.Tabela = "dbo." + tabela.Tabela;
                }
                MessageBox.Show("Arquivo migrado para a nova versão. Poderá aconteder dos \"schemas\" estarem diferentes.", "Atualização de versão do arquivo");
            }
        }

        /// <summary>
        /// Handles the Click event of the MarcarTodosToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MarcarTodosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.AlterarEstadoDosCampos(chkMetodos, MarcarTodosToolStripMenuItem.Checked ? CheckState.Checked : CheckState.Unchecked );
        }

        private void salvarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AntesDeAndarNaNavegacao();
                SetarNovaVersao();
                EstruturaSaveRadUtil<EstruturaSaveRAD>.SalvarArquivo();
            }
            catch (Exception erro)
            {
                AppUtil.ExibirErro(erro);
            }
        }

        private void SetarNovaVersao()
        {
            if (Conteudo.Versao.Count == 0)
            {
                Conteudo.Versao.AddVersaoRow(Application.ProductVersion,
                    string.Format("{0}\\{1}", System.Environment.UserDomainName, System.Environment.UserName),
                    System.Environment.MachineName,
                    DateTime.Now
                    );
            }
            else if (Application.ProductVersion.CompareTo(Conteudo.Versao[0].Numero) == 1)
            {
                Conteudo.Versao[0].Numero = Application.ProductVersion;
                Conteudo.Versao[0].Usuario = string.Format("{0}\\{1}", System.Environment.UserDomainName, System.Environment.UserName);
                Conteudo.Versao[0].Maquina = System.Environment.MachineName;
                Conteudo.Versao[0].Data = DateTime.Now;
            }
        }

        private bool VersaoMenor(string p)
        {
            
            throw new Exception("The method or operation is not implemented.");
        }

        private void toolStripButtonAreaTransferencia_Click(object sender, EventArgs e)
        {
            edtResultado.Copy();
        }

        private void toolStripButtonGerarArquivoCS_Click(object sender, EventArgs e)
        {

            saveFileDialogArquivoCS.FileName = GeradorShema.RetornarNomeArquivoCRUD(ClasseSelecionada);
            if (saveFileDialogArquivoCS.ShowDialog(this) == DialogResult.OK)    
            {
                edtResultado.SaveFile(saveFileDialogArquivoCS.FileName, RichTextBoxStreamType.PlainText);                
            }
        }

       

        private void adicionarTabelaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AntesDeAndarNaNavegacao();
                AndarNaNavegacao(Passos.Tabela);
            }
            catch (Exception erro)
            {
                AppUtil.ExibirErro(erro);
            }
        }
 
        private void tabelaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                EstruturaSaveRAD.ClasseCRUDRow[] classes = (EstruturaSaveRAD.ClasseCRUDRow[])Conteudo.ClasseCRUD.Select(string.Format("Tabela = '{0}'", cmbTabela.Text));
                if (classes.Length > 0)
                {
                    classes[0].Delete();
                    cmbTabela.DataSource = Conteudo.ClasseCRUD;
//                    TabelaComboBox_SelectedIndexChanged(sender, e);
                }

            }
            catch(Exception erro )
            {
                AppUtil.ExibirErro(erro);
            }
        }

        private void todasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                while (Conteudo.ClasseCRUD.Count > 0)
                {
                    Conteudo.ClasseCRUD[0].Delete();
                }
                cmbTabela.DataSource = Conteudo.ClasseCRUD;
                TabelaComboBox_SelectedIndexChanged(sender, e);
            }
            catch (Exception erro)
            {
                AppUtil.ExibirErro(erro);
            }
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            try
            {
                RecuperarListaTabelasDoBanco(edtFiltro.Text);                
            }
            catch (Exception erro)
            {
                AppUtil.ExibirErro(erro);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void edtNomeClasse_TextChanged(object sender, EventArgs e)
        {
            if (cmbTabela.SelectedItem != null)
            {
                EstruturaSaveRAD.ClasseCRUDRow classe = ((EstruturaSaveRAD.ClasseCRUDRow)((DataRowView)cmbTabela.SelectedItem).Row);
                if (string.IsNullOrEmpty(this.edtNomeClasse.Text))
                {
                    classe.NomeClasse = classe.Tabela;
                }
                else
                {
                    classe.NomeClasse = edtNomeClasse.Text;
                }
            }
        }

        private void toolStripDropDownButtonFinalizar_Click(object sender, EventArgs e)
        {
            try
            {
                AntesDeAndarNaNavegacao();
                AndarNaNavegacao(Passos.Resultado);
            }
            catch (Exception erro)
            {
                AppUtil.ExibirErro(erro);
            }
        }

        private void todosOsParametrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.AlterarEstadoDosCampos(chkCamposTabela, todosOsParametrosToolStripMenuItem.Checked ? CheckState.Checked : CheckState.Unchecked);

        }

        private void desmarcarCamposTransacionaisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < chkCamposTabela.Items.Count; i++)
                {
                    foreach (string CamposTransacionais in Settings.Default.CamposTransacionais.ToLower().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        string Coluna = ((DataColumn)chkCamposTabela.Items[i]).ColumnName.ToLower();
                        int indice = Coluna.IndexOf(CamposTransacionais);
                        if ( indice > -1 && indice + CamposTransacionais.Length == ((DataColumn)chkCamposTabela.Items[i]).ColumnName.Length)
                            chkCamposTabela.SetItemCheckState(i, desmarcarCamposTransacionaisToolStripMenuItem.Checked ? CheckState.Checked : CheckState.Unchecked);
                    }
                }
            }
            catch (Exception erro)
            {
                AppUtil.ExibirErro(erro);
            }

        }

        private void toolStripButtonGerarTodosArquivosCS_Click(object sender, EventArgs e)
        {

            if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK)    
            {
                string msgOut = GeradorShema.GerarAndSalvarClasses(Conteudo, folderBrowserDialog.SelectedPath);                
                Process.Start(Path.Combine( Environment.GetEnvironmentVariable("windir"), "explorer.exe"), folderBrowserDialog.SelectedPath).Start();
                if (msgOut.Length > 0)
                {
                    MessageBox.Show(msgOut, "Avisos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void toolStripMenuGerarFachadaNegocio_CheckedChanged(object sender, EventArgs e)
        {
            GeradorShema = null;            
        }
    }
}