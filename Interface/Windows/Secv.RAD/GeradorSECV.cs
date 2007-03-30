using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Collections;
using Secv.RAD.Properties;
using System.ComponentModel;
using System.IO;
using Acontep.Ng.Manutencao.Rad;
using System.Data.SqlClient;
using Acontep.Dado;

namespace Acontep.RAD
{
    public class GeradorSECV
    {
        #region Parametros

        DbConnection _Conexao;
        public DbConnection Conexao
        {
            get { return _Conexao; }
            set { _Conexao = value; }
        }

        DbProviderFactory _Factory;
        public DbProviderFactory Factory
        {
            get { return _Factory; }
            set { _Factory = value; }
        }
        string _Tabela = "";

        
        /// <summary>
        /// Gets or sets the tabela.
        /// </summary>
        /// <value>The tabela.</value>
        public string Tabela
        {
            get { return _Tabela; }
            set {
                if (value != _Tabela)
                {
                    lock (_Tabela)
                    {
                        _Tabela = value;
                        if (string.IsNullOrEmpty(value))
                        {
                            this.ColunasMetaData = new DataTable();
                        }
                        else
                            AtuailzarMetaDadosTabelaSelecionada();
                    }

                }
                
            }
        }
        /// DataTable para armazenar os metadados da Tabela
        DataTable _ColunasMetaData;
        /// <summary>
        /// DataTable para armazenar os metadados da Tabela
        /// </summary>
        public DataTable ColunasMetaData
        {
            get { return _ColunasMetaData; }
            private set { _ColunasMetaData = value; }
        }

        
        /// <summary>
        /// Conecta se houve modificação da string de conexao ou se a conexao estiver fechada. <see cref="GeradorShema"/>
        /// </summary>        
        /// <param name="StringConexao">String de conexao</param>
        /// <param name="Provider">Provider da string de conexao</param>
        public void Conectar(string StringConexao, string Provider)
        {
            if (!(
                    Conexao != null &&
                    Conexao.ConnectionString == StringConexao &&
                    Conexao.GetType().Name == Provider &&
                    Conexao.State == ConnectionState.Open)
                )
            {
                Factory = DbProviderFactories.GetFactory(Provider);
                Conexao = Factory.CreateConnection();
                Conexao.ConnectionString = StringConexao;
                Conexao.Open();                
            }
        }

        #endregion Parametros

        #region Consulta dos meta dados

        /// <summary>
        /// Retorna as tabelas do banco
        /// </summary>
        /// <returns></returns>
        public DataTable GetTables()
        {
            return Conexao.GetSchema("Tables");
        }
        /// <summary>
        /// Retorna as tabelas do banco
        /// </summary>
        /// <returns></returns>
        public DataTable GetForeignKeys(string tabela)
        {
            string[] table = new string[4];
            
            table[2] = tabela.Split('.')[1];
            return Conexao.GetSchema("ForeignKeys", table);
        }

        /// <summary>
        /// Retorna as tabelas do banco
        /// </summary>
        /// <returns></returns>
        public DataTable GetIndexColumns(string tabela)
        {
  
            if (!Settings.Default.BancoSQL)
            {
                string[] table = new string[4];
                table[1] = tabela.Split('.')[0];
                table[2] = tabela.Split('.')[1];
                return Conexao.GetSchema("IndexColumns", table);
            }
            else
            {
                string sql = @"
select distinct
	[schema].name as [constraint_schema],
	indice.name as [constraint_name],
	tabela.name as [table_name],	
	Colunas.Name as [column_name],
	index_column_id [ordinal_position],
	indice.name as [index_name]
from sys.indexes indice
    left join sys.objects tabela on tabela.OBJECT_id = indice.object_id
    left JOIN SYS.SCHEMAS [schema] ON tabela.SCHEMA_ID = [schema].SCHEMA_ID
    left JOIN SYS.index_columns ColunaIndex 
	    	ON  ColunaIndex.index_id = indice.index_id AND 
		    	ColunaIndex.Object_ID  = indice.Object_ID 
    left join sys.columns colunas
	    	on  ColunaIndex.OBJECT_id = colunas.object_id and 
		    	ColunaIndex.Column_id = colunas.Column_id
where 

    [schema].name = @Schema AND    
	tabela.name = @Tabela	
";
                using (BdUtil bd = new BdUtil(sql))
                {
                    bd.ContextoAcessoDado = new ContextoAcessoDado(Factory, Conexao.ConnectionString);
                    bd.AdicionarParametro("@Schema", DbType.AnsiString, tabela.Split('.')[0]);
                    bd.AdicionarParametro("@Tabela", DbType.AnsiString, tabela.Split('.')[1]);
                    return bd.ObterDataTable();
                }
            }
        }

        /// <summary>
        /// Retorna as primary key do banco
        /// </summary>
        /// <returns></returns>
        public DataTable GetPrimaryKeys(string tabela)
        {
            string[] table = new string[4];
            
            table[2] = tabela.Split('.')[1];
            table[3] = "PK_" + table[2].ToUpper(); 
            DataTable dt = Conexao.GetSchema("IndexColumns", table);
            dt.DefaultView.Sort = "ordinal_position";
            return dt.DefaultView.ToTable();
        }
        public bool HasPrimaryKey(string tabela)
        {
            return GetPrimaryKeys(tabela).Rows.Count > 0;
        }
        public bool ColumnAllowNull(DataColumn Column)
        {
            return Column.AllowDBNull && Column.DataType.IsValueType;
        }
        public bool EhTipoTexto(DbType tipo)
        {
            switch (tipo)
            {
                case DbType.AnsiString:
                case DbType.AnsiStringFixedLength:
                case DbType.String:
                case DbType.StringFixedLength:
                    return true;
                default:
                    return false;
            }
        }
        public bool HasCamposTransacionais()
        {
            int i = 0;
            foreach (DataColumn coluna in this.ColunasMetaData.Columns)
            {
                string NomeColuna = coluna.ColumnName.Substring(coluna.ColumnName.IndexOf('_') + 1).ToUpper();
                switch (NomeColuna)
                {
                    case "USUTRA":
                    case "DATHOR":
                    case "USUINC":
                    case "DATINC":
                        {
                            i++;
                            if (i == 4) return true;
                            break;
                        }
                }
            }
            return i == 4;
            
        }

        /// <summary>
        /// Recupera os metadados da tabela selecionada
        /// </summary>
        protected void AtuailzarMetaDadosTabelaSelecionada()
        {
            DbDataAdapter adapter = Factory.CreateDataAdapter();
            DbCommand cmd = Factory.CreateCommand();
            cmd.Connection = Conexao;
            cmd.CommandText = "SELECT * FROM " + Tabela;
            adapter.SelectCommand = cmd;
            ColunasMetaData = new DataTable();
            try
            {
                adapter.FillSchema(ColunasMetaData, SchemaType.Source);
            }
            catch (SqlException erro)
            {
                if (erro.Number == 206)
                {
                    throw new AcontepException(string.Format("A tabela {0} não existe no banco de dados.", Tabela));
                }
            }
        }

        /// <summary>
        /// Recupera os metadados da tabela
        /// </summary>
        public virtual DataTable RetornarMetaDados(string provider, string conexao, string tabela)
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory(provider);            
            DbDataAdapter adapter = factory.CreateDataAdapter();
            DbCommand cmd = factory.CreateCommand();
            cmd.Connection = factory.CreateConnection();
            cmd.Connection.ConnectionString = conexao;
            cmd.CommandText = "SELECT * FROM " + tabela;
            adapter.SelectCommand = cmd;
            DataTable Colunas = new DataTable();
            try
            {
                adapter.FillSchema(Colunas, SchemaType.Source);
            }
            catch //(SqlException erro)
            {
                //if (erro.Number == 208)
                //{
                //    throw new AcontepException(string.Format("A tabela {0} não existe no banco de dados.", Tabela));
                //}
                //else
                //    throw;
            }
            return Colunas;
        }


        /// <summary>
        /// Converte a lista de campos em datacolumn
        /// </summary>
        /// <param name="Campos"></param>
        /// <returns></returns>
        protected DataColumn[] RetornarColunas(EstruturaSaveRAD.ListaCamposRow[] Campos)
        {
            DataColumn[] Colunas = new DataColumn[Campos.Length];
            for (int i = 0; i < Campos.Length; i++)
            {
                Colunas[i] = ColunasMetaData.Columns[Campos[i].Nome];
            }
            return Colunas;
        }
        /// <summary>
        /// Pesquisa por uma coluna em um DataTable
        /// </summary>
        /// <param name="table">DataTable</param>
        /// <param name="nomeColuna">Nome da coluna</param>
        /// <returns>DataColumn encontrado ou null se não encontrar</returns>
        protected DataColumn PesquisarColuna(DataColumn[] colunas, string nomeColuna)
        {
            foreach (DataColumn coluna in colunas)
            {
                if (coluna.ColumnName == nomeColuna)
                {
                    return coluna;
                }
            }
            return null;
        }
    
        #endregion Consulta dos meta dados




        protected virtual string GerarComandoSelect(string NomeMetodo, DataColumn[] CamposProjecao, DataColumn[] Filtros, EstruturaSaveRAD.ClasseCRUDRow ccr)
        {
            return string.Empty;
        }
        protected virtual string GerarComandoInsert(EstruturaSaveRAD.MetodoRow mrw)
        {
            return string.Empty;
        }
        protected virtual string GerarComandoUpdate(EstruturaSaveRAD.MetodoRow mrw)
        {
            return string.Empty;
        }
        protected virtual string GerarComandoDelete(EstruturaSaveRAD.MetodoRow mrw)
        {
            return string.Empty;
        }
        public string GerarClasses(bool AdicionarNameSpace, EstruturaSaveRAD estruturaSaveRAD, EstruturaSaveRAD.ClasseCRUDRow ClasseSelecionada, out string msgErros)
        {
            StringBuilder ListaClasses = new StringBuilder();
            StringBuilder ListaErros = new StringBuilder();

            if (estruturaSaveRAD == null || estruturaSaveRAD.ClasseCRUD == null || estruturaSaveRAD.ClasseCRUD.Count == 0)
            {
                msgErros = "Nenhuma classe foi informada.";
                return ListaClasses.ToString();
            }
            bool SomenteUmaClasse = AdicionarNameSpace && (estruturaSaveRAD.ClasseCRUD.Count == 1 || ClasseSelecionada != null);

            ICollection ClassesParaGerar = null;

            if (ClasseSelecionada != null)
            {
                ClassesParaGerar = new EstruturaSaveRAD.ClasseCRUDRow[] { ClasseSelecionada };
            }
            else
            {
                ClassesParaGerar = estruturaSaveRAD.ClasseCRUD.Rows;
            }
            foreach (EstruturaSaveRAD.ClasseCRUDRow ccr in ClassesParaGerar)
            {

                StringBuilder Classe = new StringBuilder();
                if (!HasPrimaryKey(ccr.Tabela))
                {
                    ListaErros.AppendFormat("A tabela {0} não contém a primary key \"PK_{1}\".\n", ccr.Tabela, ccr.Tabela.Split('.')[1]);                 
                }
                if (!HasCamposTransacionais())
                {
                    ListaErros.AppendFormat("A tabela {0} não contém todos os campos transacionais.\n", ccr.Tabela);
                }
                Classe.AppendLine(GerarComandoSelectDosIndices(ccr.Tabela, ccr));
                EstruturaSaveRAD.MetodoRow MetodoCrud = CriarMetodoCRUD(estruturaSaveRAD, ccr);
                try
                {
                    foreach (EstruturaSaveRAD.MetodoRow metodo in ccr.GetMetodoRows())
                    {
                        Classe.AppendLine(GerarComandoObter(metodo));
                        Classe.AppendLine(GerarComandoInsert(metodo));
                        Classe.AppendLine(GerarComandoUpdate(metodo));
                        Classe.AppendLine(GerarComandoDelete(metodo));
                    }
                    ListaClasses.AppendLine(GerarDeclaracaoClasse(ccr, SomenteUmaClasse, Classe.ToString()));
                }
                finally
                {
                    MetodoCrud.Delete();
                }
            }
            msgErros = ListaErros.ToString();
            if (AdicionarNameSpace && !SomenteUmaClasse)
            {
                
                return GerarNameSpace(estruturaSaveRAD.ClasseCRUD[0].NameSpace, ListaClasses.ToString());
            }
            else
            {
                return ListaClasses.ToString();
            }
        }

        #region Geracao De Classe

        /// <summary>
        /// Gerar a declaracao classe.
        /// </summary>
        /// <param name="ccr">The CCR.</param>
        /// <param name="addNameSpace">if set to <c>true</c> [add name space].</param>
        /// <param name="ConteudoClasse">The conteudo classe.</param>
        /// <returns></returns>
        public string GerarDeclaracaoClasse(EstruturaSaveRAD.ClasseCRUDRow ccr, bool addNameSpace, string ConteudoClasse)
        {
            StringBuilder Classe = new StringBuilder();

            if (addNameSpace)
            {
                Classe.AppendLine(GerarNameSpace(ccr.NameSpace));
                Classe.AppendLine("{"); //abre namespace
            }
            Classe.AppendLine("[System.ComponentModel.DataObject(true)]");
            Classe.Append("public static partial class " + RetornarNomeClasse(ccr));
            Classe.AppendLine(" \n {"); //abre classe
            Classe.AppendLine(ConteudoClasse);
            Classe.AppendLine("}"); //fecha classe
            if (addNameSpace)
                Classe.AppendLine("}"); //fecha namespace

            return Classe.ToString();
        }
        protected virtual string RetornarNomeClasse(EstruturaSaveRAD.ClasseCRUDRow ccr)
        {
            return (ccr.IsNomeClasseNull() ? "[NOME CLASSE]" : ccr.NomeClasse);
        }
        ///// <summary>
        ///// Gerar a classe.
        ///// </summary>
        ///// <param name="AdicionarNameSpace">if set to <c>true</c> [adiciona o namespace].</param>
        ///// <param name="ccr">The CCR.</param>
        ///// <returns></returns>
        //public string GerarClasse(bool AdicionarNameSpace, EstruturaSaveRAD estruturaSaveRAD)
        //{
        //    return GerarClasses(AdicionarNameSpace, estruturaSaveRAD);
        //}

        ///// <summary>
        ///// Gera a classe sem o namespace
        ///// </summary>
        ///// <param name="ccrs"></param>
        ///// <returns></returns>
        //public string GerarClasses(EstruturaSaveRAD estruturaSaveRAD)
        //{
        //    return GerarClasses(true, estruturaSaveRAD);
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="ccrs"></param>
        ///// <returns></returns>
        //private string GerarClasses(bool AdicionarNameSpace, EstruturaSaveRAD estruturaSaveRAD)
        //{
        //    return GerarClasses(AdicionarNameSpace, estruturaSaveRAD, null);
        //}
        

        #endregion Geracao De Classe
        #region Geracao de NameSpace

        /// <summary>
        /// Cria o namespace
        /// </summary>
        /// <param name="ccr"></param>
        /// <returns></returns>
        private string GerarNameSpace(EstruturaSaveRAD.ClasseCRUDRow ccr)
        {
            return GerarNameSpace(ccr.NameSpace);
        }
        /// <summary>
        /// Cria o namespace
        /// </summary>
        /// <param name="ccr"></param>
        /// <returns></returns>
        private string GerarNameSpace(string NameSpace)
        {
            // Declaração using e namespace
            StringBuilder Texto = new StringBuilder();
            Texto.AppendLine("// Classe gerada automaticamente pela ferramenta GeradorCRUD");
            Texto.AppendFormat("// em {0:ddd dd/MM/yyyy} \n", DateTime.Now);
            Texto.AppendFormat("// Por {0}\\{1} na máquina {2} \n", System.Environment.UserDomainName, System.Environment.UserName, System.Environment.MachineName);
            Texto.AppendLine("using System;");
            Texto.AppendLine("using System.Collections.Generic;");
            Texto.AppendLine("using System.Text;");
            Texto.AppendLine("using System.Data;");

            Texto.AppendFormat("\nusing {0};\n", Settings.Default.NameSpaceClasseDado);

            Texto.AppendFormat("namespace {0}", NameSpace);
            return Texto.ToString();
        }
        /// <summary>
        /// Cria o namespace
        /// </summary>
        /// <param name="ccr"></param>
        /// <returns></returns>
        private string GerarNameSpace(string NameSpace, string Conteudo)
        {
            // Declaração using e namespace
            StringBuilder Texto = new StringBuilder();
            Texto.AppendLine(GerarNameSpace(NameSpace));
            Texto.AppendLine("{");
            Texto.AppendLine(Conteudo);
            Texto.AppendLine("}");
            return Texto.ToString();
        }

        #endregion Geracao de NameSpace

        /// <summary>
        /// Cria o comando de OBTER.
        /// </summary>
        /// <param name="mrw">The MRW.</param>
        /// <returns></returns>
        protected string GerarComandoObter(EstruturaSaveRAD.MetodoRow mrw)
        {
            if (!ListaOpcoesContemOpcao(mrw, DataObjectMethodType.Select))
            {
                return "";
            }
            this.Tabela = mrw.ClasseCRUDRow.Tabela;
            return GerarComandoSelect(
                    string.IsNullOrEmpty(mrw.NomeMetodo) ? Settings.Default.NomeDefaultMetodoSelect : mrw.NomeMetodo,
                    mrw.GetListaCamposRows(),
                    ColunasMetaData.PrimaryKey,
                    mrw.ClasseCRUDRow
            );
        }

        /// <summary>
        /// Cria o comando select.
        /// </summary>
        /// <param name="NomeMetodo">The nome metodo.</param>
        /// <param name="CamposProjecao">The campos projecao.</param>
        /// <param name="Filtros">The filtros.</param>
        /// <param name="ccr">The CCR.</param>
        /// <returns></returns>
        protected string GerarComandoSelect(string NomeMetodo, EstruturaSaveRAD.ListaCamposRow[] CamposProjecao, DataColumn[] Filtros, EstruturaSaveRAD.ClasseCRUDRow ccr)
        {
            return GerarComandoSelect(NomeMetodo, RetornarColunas(CamposProjecao), Filtros,ccr);
        }

        /// <summary>
        /// Cria o comando de select baseado nos indices.
        /// </summary>
        /// <param name="Tabela">The tabela.</param>
        /// <param name="ccr">The CCR.</param>
        /// <returns></returns>
        public string GerarComandoSelectDosIndices(string Tabela, EstruturaSaveRAD.ClasseCRUDRow ccr)
        {
            this.Tabela = Tabela;
            //
            // Ordenando os índices
            //
            DataTable indices = GetIndexColumns(Tabela);
            DataView indicesDataView = indices.DefaultView;
            indicesDataView.Sort = "CONSTRAINT_NAME, ORDINAL_POSITION";
            indices = indicesDataView.ToTable();

            //if (indices.Rows.Count == 0)
            //    return string.Empty;
            //string indiceCorrente = indices.Rows[0]["CONSTRAINT_NAME"].ToString();
            DataColumn[] Colunas = new DataColumn[ColunasMetaData.Columns.Count];
            ColunasMetaData.Columns.CopyTo(Colunas, 0);

            List<DataColumn> camposDoIndice = new List<DataColumn>();
            List<string> camposParaBuscaGerados = new List<string>();
            IDictionary<string, List<DataColumn>> Itens = new Dictionary<string, List<DataColumn>>();
            StringBuilder comandos = new StringBuilder();
            foreach (DataRow row in indices.Rows)
            {
                if (!String.IsNullOrEmpty(row["constraint_schema"].ToString()) && Tabela.Split('.')[0] != row["constraint_schema"].ToString())
                    continue;
                if ( ! Itens.ContainsKey(row["CONSTRAINT_NAME"].ToString() ))
                {
                    Itens.Add(row["CONSTRAINT_NAME"].ToString(), new List<DataColumn>());
                }
                Itens[row["CONSTRAINT_NAME"].ToString()].Add(PesquisarColuna(Colunas, row["COLUMN_NAME"].ToString()));
            }
            foreach( string NomeIndice in Itens.Keys )
            {
                comandos.AppendLine(
                    GerarComandoSelect(
                        Settings.Default.NomeDefaultMetodoSelectComParametros + NomeIndice,
                        Colunas, Itens[NomeIndice].ToArray(), ccr
                    )
                );
            }

                //if (camposDoIndice.Count > 0 && camposDoIndice[0] != null)
                //{
                    
                //    camposParaBuscaGerados.Add(row["CONSTRAINT_NAME"].ToString().ToLower());
                //    camposDoIndice.Clear();
                //}
                //if (!camposParaBuscaGerados.Contains(row["CONSTRAINT_NAME"].ToString().ToLower()))
                //{
                    
             
                //        camposDoIndice.Add();
             
                   
                    
                //}
             
            //}
            return comandos.ToString();
            //for (int i = 0; i < indices.Rows.Count; i++)
            //{
            //        DataRow indiceRow = indices.Rows[i];
            //        if (indiceCorrente != indiceRow["CONSTRAINT_NAME"].ToString())
            //        {
            //            camposParaBuscaGerados.Add(indiceCorrente.ToLower());
            //            // Adicionando o método de pesquisa para o índice                    
            //            comandos.AppendLine(GerarComandoSelect(Settings.Default.NomeDefaultMetodoSelectComParametros + indiceCorrente, Colunas, camposDoIndice.ToArray(), ccr));

            //            camposDoIndice.Clear();
            //            // Pegando o próximo índice
            //            indiceCorrente = indiceRow["CONSTRAINT_NAME"].ToString();
            //        }
            //        // Armazenando os campos do índice
            //        camposDoIndice.Add(PesquisarColuna(Colunas, indiceRow["COLUMN_NAME"].ToString()));
            //}
            //// Adicionando o último índice
            //comandos.AppendLine(GerarComandoSelect(Settings.Default.NomeDefaultMetodoSelectComParametros + (indices.Rows[indices.Rows.Count - 1]["CONSTRAINT_NAME"].ToString()), Colunas, camposDoIndice.ToArray(), ccr));
            //return comandos.ToString();
        }

        #region Geracao da declaracao dos parametros que serão utilizados

        /// <summary>
        /// Monta a declaracao do parametro adicionando o tipo do dado
        /// </summary>
        /// <param name="Filtros"></param>
        /// <param name="UsarIdentity"></param>
        /// <returns></returns>
        protected string MontarDeclaracaoParametro(DataColumn[] Filtros, bool UsarIdentity)
        {
            return MontarDeclaracaoParametro(Filtros, UsarIdentity, true);
        }
        /// <summary>
        /// Monta a declaracao do parametro adicionando o tipo do dado
        /// </summary>
        /// <param name="Filtros"></param>
        /// <param name="UsarIdentity"></param>
        /// <returns></returns>
        protected string MontarDeclaracaoParametro(DataColumn[] Filtros, bool UsarIdentity, bool AddTipoDado)
        {
            return MontarDeclaracaoParametro(Filtros, UsarIdentity, AddTipoDado, false, false);
        }
        /// <summary>
        /// Monta a declaracao do parametro adicionando o tipo do dado e sem adicionar o "original_" para os campos identity        
        /// </summary>
        /// <param name="Filtros"></param>
        /// <param name="UsarIdentity"></param>
        /// <param name="AddCamposTransacionaisAlteracao">Adiciona automaticamente campos de transacionais de alteração</param>
        /// <param name="AddCamposTransacionaisInclusao">Adiciona automaticamente campos de transacionais de inclusção</param>
        /// <param name="AddTipoDado">Adiciona o tipo do dado</param>
        /// <returns></returns>
        protected string MontarDeclaracaoParametro(DataColumn[] Filtros, bool UsarIdentity, bool AddTipoDado, bool AddCamposTransacionaisAlteracao, bool AddCamposTransacionaisInclusao)
        {
            return MontarDeclaracaoParametro(Filtros, UsarIdentity, AddTipoDado, AddCamposTransacionaisAlteracao, AddCamposTransacionaisInclusao, false);
        }
        /// <summary>
        /// Monta a declaracao do parametro adicionando o tipo do dado
        /// </summary>
        /// <param name="Filtros"></param>
        /// <param name="UsarIdentity"></param>
        /// <param name="AddCamposTransacionaisAlteracao">Adiciona automaticamente campos de transacionais de alteração</param>
        /// <param name="AddCamposTransacionaisInclusao">Adiciona automaticamente campos de transacionais de inclusção</param>
        /// <param name="AddTipoDado">Adiciona o tipo do dado</param>
        /// <returns></returns>
        protected string MontarDeclaracaoParametro(DataColumn[] Filtros, bool UsarIdentity, bool AddTipoDado, bool AddCamposTransacionaisAlteracao, bool AddCamposTransacionaisInclusao, bool AdicionarOriginal)
        {
            List<string> resultado = new List<string>(Filtros.Length);
            for (int i = 0; i < Filtros.Length; i++)
            {
                DataColumn Filtro = Filtros[i];
                string parametro = string.Empty;
                string prefixo = String.Empty;
                if (Filtro.AutoIncrement)
                    if (!UsarIdentity)
                        continue;
                    else if (AdicionarOriginal)
                        prefixo = Settings.Default.NomePrefixoCamposManutencao;


                string NomeColuna = Filtro.ColumnName.Substring(Filtro.ColumnName.IndexOf('_') + 1).ToUpper();
                if (AddCamposTransacionaisAlteracao)
                {
                    switch (NomeColuna)
                    {
                        case "USUTRA":
                        case "DATHOR":
                            continue;
                    }
                }
                if (AddCamposTransacionaisInclusao)
                {
                    switch (NomeColuna)
                    {
                        case "USUINC":
                        case "DATINC":
                            continue;
                    }
                }

                if (AddTipoDado)
                {
                    parametro = Filtro.DataType.ToString();
                    parametro += ColumnAllowNull(Filtro)? "? " : " ";
                }
                else
                {
                    parametro = "\n ";
                }
                parametro += prefixo + Filtro.ColumnName;
                resultado.Add(parametro);
                //resultado[i] = (AddTipoDado ? Filtro.DataType.ToString() : "\n") + 
                //    " " + Filtro.ColumnName;
            }
            return string.Join(", ", resultado.ToArray());
        }
        /// <summary>
        /// Monta a declaracao do parametro adicionando o tipo do dado
        /// </summary>
        /// <param name="Filtros"></param>
        /// <param name="UsarIdentity"></param>
        /// <returns></returns>
        protected string MontarDeclaracaoParametro(EstruturaSaveRAD.ListaCamposRow[] Campos, bool UsarIdentity)
        {
            return MontarDeclaracaoParametro(Campos, UsarIdentity, true);
        }

        /// <summary>
        /// Adiciona a declaraçao da lista de parâmetros ao método de consulta (Assinatura)
        /// </summary>
        /// <param name="consulta">Consulta</param>
        /// <returns>Lista de parâmetros</returns>
        protected string MontarDeclaracaoParametro(string[] parametros)
        {
            StringBuilder declaracaoParametros = new StringBuilder();
            List<string> param = new List<string>(parametros.Length);
            for (int i = 0; i < parametros.Length; i++)
            {
                param.Add("object " + parametros[i]);
            }
            return string.Join(", ", param.ToArray());
        }
        /// <summary>
        /// Monta a declaracao de parametro. Geralmente utilizada na declaracao do metodo
        /// </summary>
        /// <param name="Campos"></param>
        /// <param name="UsarIdentity"></param>
        /// <param name="AddTipoDado"></param>
        /// <returns></returns>
        protected string MontarDeclaracaoParametro(EstruturaSaveRAD.ListaCamposRow[] Campos, bool UsarIdentity, bool AddTipoDado)
        {
            DataColumn[] Colunas = RetornarColunas(Campos);
            return MontarDeclaracaoParametro(Colunas, UsarIdentity, AddTipoDado);
        }

        #endregion Geracao da declaracao dos parametros que serão utilizados


        /// <summary>
        /// Pergunta se a Lista de opcoes contém a opcao.
        /// </summary>
        /// <param name="mrw">The MRW.</param>
        /// <param name="opc">The opcão.</param>
        /// <returns></returns>
        protected bool ListaOpcoesContemOpcao(EstruturaSaveRAD.MetodoRow mrw, System.ComponentModel.DataObjectMethodType opc)
        {
            string ListaOpcoes = mrw.Opcoes.ToUpper();
            switch (opc)
            {
                case DataObjectMethodType.Select:
                    {
                        return ListaOpcoes.IndexOf("S") > -1;
                    }
                case DataObjectMethodType.Insert:
                    {
                        return ListaOpcoes.IndexOf("I") > -1;
                    }
                case DataObjectMethodType.Update:
                    {
                        return ListaOpcoes.IndexOf("U") > -1;
                    }
                case DataObjectMethodType.Delete:
                    {
                        return ListaOpcoes.IndexOf("D") > -1;
                    }
            }
            return false;
        }

        /// <summary>
        /// Gera as classes e salva em um diretório
        /// </summary>
        /// <param name="Conteudo">The conteudo.</param>
        /// <param name="pFolderPath">The p folder path.</param>
        public string GerarAndSalvarClasses(EstruturaSaveRAD Conteudo, string pFolderPath)
        {
            StringBuilder msgErros = new StringBuilder();
            foreach (EstruturaSaveRAD.ClasseCRUDRow ccr in Conteudo.ClasseCRUD)
            {
                string msgOut = string.Empty;
                string FullFileName = Path.Combine(pFolderPath, RetornarNomeArquivoCRUD( ccr ) );
                File.WriteAllText(FullFileName, this.GerarClasses(true, Conteudo, ccr, out msgOut));
                if (msgOut != string.Empty)
                {
                    msgErros.Append(msgOut);
                }
            }
            return msgErros.ToString();
        }

        public string RetornarNomeArquivoCRUD(EstruturaSaveRAD.ClasseCRUDRow ccr)
        {
            return string.Format("{0}.Designer.cs", RetornarNomeClasse(ccr).Trim());
        }


        #region Geracao de CRUD 

        /// <summary>
        /// Adiciona ou atualiza a classe row
        /// </summary>
        /// <param name="CCR"></param>
        private EstruturaSaveRAD.MetodoRow CriarMetodoCRUD(EstruturaSaveRAD estruturaSaveRAD, EstruturaSaveRAD.ClasseCRUDRow ccr)
        {
            EstruturaSaveRAD.MetodoRow mrw = estruturaSaveRAD.Metodo.NewMetodoRow();
            mrw.ID = Guid.NewGuid();
            
            mrw.NomeMetodo = string.Empty;
            mrw.ClasseCRUDRow = ccr;
            mrw.Opcoes = "CIUD";
            estruturaSaveRAD.Metodo.AddMetodoRow(mrw);

            foreach (DataColumn nomeColuna in this.RetornarMetaDados(ccr.Provider, ccr.StringConexao, ccr.Tabela).Columns )
            {            
                EstruturaSaveRAD.ListaCamposRow lcr = estruturaSaveRAD.ListaCampos.NewListaCamposRow();
                lcr.MetodoRow = mrw;
                lcr.Nome = nomeColuna.ColumnName;
                lcr.ID = Guid.NewGuid();
                lcr.IDMetodo = mrw.ID;
                estruturaSaveRAD.ListaCampos.AddListaCamposRow(lcr);
            }
            return mrw;
        }

        #endregion
    }
}
