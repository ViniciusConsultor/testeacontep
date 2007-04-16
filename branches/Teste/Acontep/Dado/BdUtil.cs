using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Acontep.Diagnostico;
using System.Diagnostics;
using System.Data.SqlClient;

namespace Acontep.Dado
{
    /// <summary>
    /// Utilitario de execucao de comandos via ADO.NET. Sempre instanciar dentro do <see cref="Escopo"/> onde se deseja usar.
    /// </summary>
    /// <remarks>
    /// Se existir um <see cref="ContextoAcessoDado"/> em <see cref="Escopo.Atual"/> entao usa o mesmo,
    /// compartilhando conexoes e transações dentro de um mesmo escopo.
    /// Se nao existir <see cref="Escopo.Atual"/> um <see cref="ContextoAcessoDado"/> é criado exclusivamente
    /// para a instancia do <c>BdUtil</c>.
    /// Opcionalmente o <see cref="ContextoAcessoDado"/> pode ser atribuido diretamente via propriedade 
    /// <see cref="BdUtil.ContextoAcessoDado"/>
    /// </remarks>
    public partial class BdUtil : Componente
    {
        #region Constantes

        /// <summary>
        /// Identificado do retorno
        /// </summary>
        private const string NOME_PARAMETRO_RETORNO = "__Retorno";

        #endregion

        private ContextoAcessoDado _ContextoAcessoDado;
        private bool _FazerDisposeContextoAcessoDado;
        private Dictionary<TipoCommandDataAdapter, CommandWrapper> _CommandWrappersDataAdapter;
        private TipoCommandDataAdapter _TipoCommandDataAdapter;
        private int _UpdateBatchSize;
        private DbDataAdapter _DataAdapter;

        #region Construtores

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        public BdUtil(string commandText, CommandType commandType)
            : this()
        {
            ConfigTipoCommandDataAdapter(TipoCommandDataAdapter.Select, commandText, commandType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandText"></param>
        public BdUtil(string commandText)
            : this(commandText, CommandType.Text)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public BdUtil()
        {
            _CommandWrappersDataAdapter = new Dictionary<TipoCommandDataAdapter, CommandWrapper>();
            _FazerDisposeContextoAcessoDado = false;
        }

        #endregion Construtores

        #region Metodos privados/protegidos

        /// <summary>
        /// Ajusta valor de parametros para <see cref="DBNull"/> quando necessario
        /// </summary>
        /// <remarks>
        /// <para>Sao 3 os casos que o valor do parâmetro é substiuído por <see cref="DBNull"/>:</para>
        /// <para>1. O valor é nulo</para>
        /// <para>2. O valor é uma <see cref="System.String"/> e possui comprimento zero</para>
        /// <para>3. O valor é um <see cref="System.DateTime"/> e possui valor igual <see cref="DateTime.MinValue"/></para>
        /// </remarks>
        /// <param name="parametro"></param>
        private void AjustandoValorDeParametro(DbParameter parametro)
        {
            // Substituíndo parâmetros null por DBNull
            if (parametro.Value == null)
            {
                parametro.Value = DBNull.Value;
            }
            else if (parametro.Value is string)
            {
                string valor = (string)parametro.Value;
                if (valor.Trim().Length == 0)
                {
                    parametro.Value = DBNull.Value;
                }
            }
            else if (parametro.Value is DateTime)
            {
                DateTime valor = (DateTime)parametro.Value;
                if (valor == DateTime.MinValue)
                {
                    parametro.Value = DBNull.Value;
                }
            }
        }

        #endregion

        #region Propriedades

        /// <summary>
        /// <see cref="DbCommand"/> criado por <see cref="BdUtil"/>, referente ao <see cref="ObterTipoCommandDataAdapterConfigurado"/>
        /// </summary>
        public DbCommand Command
        {
            get
            {
                ChecarDisposed();
                TipoCommandDataAdapter tipoCommandDataAdapter = ObterTipoCommandDataAdapterConfigurado();
                if (!_CommandWrappersDataAdapter.ContainsKey(tipoCommandDataAdapter))
                {
                    ConfigCommand(tipoCommandDataAdapter, String.Empty);
                }
                CommandWrapper commandWrapper = _CommandWrappersDataAdapter[tipoCommandDataAdapter];
                return commandWrapper.ObterCommand(this.ContextoAcessoDado);
            }
        }

        /// <summary>
        /// Obtem ou atribui o tempo de espera antes de terminar a tentativa de executar um comando
        /// e gerar um erro
        /// </summary>
        public int CommandTimeout
        {
            get
            {
                if (CommandWrapperSelec == null)
                {
                    return CommandWrapper.COMMAND_TIMEOUT;
                }
                else
                {
                    return CommandWrapperSelec.CommandTimeout;
                }
            }
            set
            {
                if (CommandWrapperSelec == null)
                {
                    ConfigCommand(_TipoCommandDataAdapter, String.Empty);
                }
                CommandWrapperSelec.CommandTimeout = value;
            }
        }

        /// <summary>
        /// Obtem ou atribui o valor que habilita ou disabilita suporte a processamente em batch,
        /// e especifica o número de comandos que podem ser executados em um batch. Essa propriedade
        /// apenas surtira efeito se  metodo Update for usado.
        /// </summary>
        public int UpdateBatchSize
        {
            get
            {
                if (_DataAdapter == null)
                {
                    return _UpdateBatchSize;
                }
                else
                {
                    return _DataAdapter.UpdateBatchSize;
                }
            }
            set
            {
                if (_DataAdapter != null)
                {
                    _DataAdapter.UpdateBatchSize = value;
                }
                _UpdateBatchSize = value;
            }
        }

        /// <summary>
        /// <see cref="DbDataAdapter"/> com <see cref="Command"/> como valor para <see cref="DbDataAdapter.SelectCommand"/>
        /// </summary>
        public DbDataAdapter DataAdapter
        {
            get
            {
                ChecarDisposed();
                if (_DataAdapter == null)
                    _DataAdapter = BdUtil.CriarDataAdapterDeCommandWrappers(ContextoAcessoDado, _CommandWrappersDataAdapter);
                return _DataAdapter;
            }
        }

        /// <summary>
        /// Obtem o <see cref="ContextoAcessoDado"/> associado a esta instância
        /// </summary>
        /// <remarks>
        /// Se nao existir nenhum escopo disponivel cria um <see cref="ContextoAcessoDado"/> privado e gerencia
        /// a sua vida util, chamando <see cref="Acontep.Dado.ContextoAcessoDado.Dispose"/> quando <see cref="Dispose"/> ocorrer.
        /// Caso contrario, usa o <see cref="Acontep.Dado.ContextoAcessoDado"/> de <see cref="Acontep.Escopo"/> 
        /// </remarks>
        public ContextoAcessoDado ContextoAcessoDado
        {
            get
            {
                ChecarDisposed();
                if (_ContextoAcessoDado == null)
                {
                    if (Escopo.Atual == null)
                    {
                        _ContextoAcessoDado = new ContextoAcessoDado();
                        // So faz Dispose de contexto de acesso a dados se foi criado internamente
                        _FazerDisposeContextoAcessoDado = true;
                    }
                    else
                    {
                        _ContextoAcessoDado = Escopo.Atual.ContextoAcessoDado;
                    }
                }
                return _ContextoAcessoDado;
            }
            set
            {
                // Qualquer contexto de acesso a dado que seja criado fora desta classe
                // possui um ciclo proprio e nao sofrera dispose
                _FazerDisposeContextoAcessoDado = false;
                _ContextoAcessoDado = value;
            }
        }

        /// <summary>
        /// Seleciona o tipo de command do <see cref="DataAdapter"/> que as operações de configuracao
        /// de um command vão afetar e atribui <see cref="CommandText"/> para o tipo de comando. 
        /// <paramref name="valor"/> indica qual o comando que vai ser usado para qualquer operação
        /// dessa classe.
        /// </summary>
        public void ConfigTipoCommandDataAdapter(TipoCommandDataAdapter valor, string commandText)
        {
            this.ConfigTipoCommandDataAdapter(valor, commandText, CommandType.Text);
        }

        /// <summary>
        /// Seleciona o tipo de command do <see cref="DataAdapter"/> que as operações de configuracao
        /// de um command vão afetar e atribui <see cref="CommandText"/> para o tipo de comando. 
        /// <paramref name="valor"/> indica qual o comando que vai ser usado para qualquer operação
        /// dessa classe.
        /// </summary>
        public void ConfigTipoCommandDataAdapter(TipoCommandDataAdapter valor, string commandText, CommandType commandType)
        {
            _TipoCommandDataAdapter = valor;
            CommandText = commandText;
            CommandType = commandType;
        }

        /// <summary>
        /// Seleciona o tipo de command do <see cref="DataAdapter"/> que as operações de configuracao
        /// de um command vão afetar. Essa configuracacao indica qual o comando que vai ser usado para qualquer operação
        /// dessa classe.
        /// </summary>
        public void ConfigTipoCommandDataAdapter(TipoCommandDataAdapter valor)
        {
            _TipoCommandDataAdapter = valor;
        }

        /// <summary>
        /// Obtemo tipo de command do <see cref="DataAdapter"/> configurado para uso
        /// </summary>
        /// <returns></returns>
        public TipoCommandDataAdapter ObterTipoCommandDataAdapterConfigurado()
        {
            return _TipoCommandDataAdapter;
        }

        /// <summary>
        /// Texto do commando para o <see cref="ObterTipoCommandDataAdapterConfigurado"/>
        /// </summary>
        public string CommandText
        {
            get
            {
                if (CommandWrapperSelec == null)
                {
                    return String.Empty;
                }
                else
                {
                    return CommandWrapperSelec.CommandText;
                }
            }
            set
            {
                if (CommandWrapperSelec == null)
                {
                    ConfigCommand(_TipoCommandDataAdapter, value);
                }
                else
                {
                    CommandWrapperSelec.CommandText = value;
                }
            }
        }

        /// <summary>
        /// Tipo de command a ser executado.
        /// </summary>
        /// <seealso cref="System.Data.CommandType"/>
        public CommandType CommandType
        {
            get
            {
                if (CommandWrapperSelec == null)
                {
                    return CommandType.Text;
                }
                else
                {
                    return CommandWrapperSelec.CommandType;
                }
            }
            set
            {
                if (CommandWrapperSelec == null)
                {
                    ConfigCommand(_TipoCommandDataAdapter, String.Empty, value);
                }
                else
                {
                    CommandWrapperSelec.CommandType = value;
                }
            }
            
        }

        /// <summary>
        /// Obtem o command wrapper selecionado atraves de <see cref="ObterTipoCommandDataAdapterConfigurado"/>
        /// </summary>
        internal CommandWrapper CommandWrapperSelec
        {
            get
            {
                TipoCommandDataAdapter tipo = ObterTipoCommandDataAdapterConfigurado();
                if (!_CommandWrappersDataAdapter.ContainsKey(tipo))
                    return null;
                else
                    return _CommandWrappersDataAdapter[tipo];
            }
            set
            {
                _CommandWrappersDataAdapter[ObterTipoCommandDataAdapterConfigurado()] = value;
            }
        }

        #endregion Propriedades

        #region Parametros

        /// <summary>
        /// Atribui valor ao parametro de nomeParametro informado
        /// </summary>
        /// <param name="nomeParametro">Nome do parametro a ser atribuido valor</param>
        /// <param name="valor">Valor do parametro</param>
        public void AtribuirValorParametro(string nomeParametro, object valor)
        {
            ChecarDisposed();
            DbParameter parametro = Command.Parameters[nomeParametro];
            AtribuirValorParametro(parametro, valor);
        }

        /// <summary>
        /// Atribui valor ao parametro de nomeParametro informado
        /// </summary>
        /// <param name="parametro">Parametro a ser atribuido valor</param>
        /// <param name="valor">Valor do parametro</param>
        private void AtribuirValorParametro(DbParameter parametro, object valor)
        {
            parametro.Value = valor;
            AjustandoValorDeParametro(parametro);
        }

        /// <summary>
        /// Obtem um parâmetro da coleção de parâmetros armazenada.
        /// Deve ser utilizada para obter os valores dos parâmetros de retorno de rotinas armazenadas
        /// </summary>
        /// <param name="nome">Nome</param>
        /// <returns>Valor do parâmetro</returns>
        public object ObterValorParametro(string nome)
        {
            ChecarDisposed();
            return this.CommandWrapperSelec.ParametrosOutputReturnValue[nome].Value;
        }

        /// <summary>
        /// Obtem um parâmetro da coleção de parâmetros armazenada.
        /// Deve ser utilizada para obter os valores dos parâmetros de retorno de rotinas armazenadas
        /// </summary>
        /// <typeparam name="T">Tipo de dado do parametro</typeparam>
        /// <param name="nomeParametro">Nome do parametro</param>
        /// <exception cref="InvalidCastException">O tipo retornado nao pode ser convertido para <typeparamref name="T"/></exception>
        /// <exception cref="NullReferenceException"><typeparamref name="T"/> nao pode ter um valor nulo e um valor nulo foi retornado</exception>
        /// <returns>Valor do parâmetro</returns>
        public T ObterValorParametro<T>(string nomeParametro)
        {
            ChecarDisposed();
            return (T)this.CommandWrapperSelec.ParametrosOutputReturnValue[nomeParametro].Value;
        }

        /// <summary>
        /// Obtem o valor de retorno do ultimo comando executado
        /// </summary>
        /// <returns></returns>
        public object ObterValorRetorno()
        {
            ChecarDisposed();
            return ObterValorParametro(NOME_PARAMETRO_RETORNO);
        }

        /// <summary>
        /// Obtem o valor de retorno do ultimo comando executado
        /// </summary>
        /// <exception cref="InvalidCastException">O tipo retornado nao pode ser convertido para <typeparamref name="T"/></exception>
        /// <exception cref="NullReferenceException"><typeparamref name="T"/> nao pode ter um valor nulo e um valor nulo foi retornado</exception>
        /// <returns></returns>
        public T ObterValorRetorno<T>()
        {
            ChecarDisposed();
            return ObterValorParametro<T>(NOME_PARAMETRO_RETORNO);
        }

        #region Criacao de parametros, sem valor inicial
        
        /// <summary>
        /// Adiciona parametros no <see cref="Command"/> sendo contruido por esta instancia
        /// </summary>
        /// <param name="nomeParametro">Nome do parametro</param>
        /// <param name="tipoDado">Tipo de dado do parametro</param>
        /// <param name="tamanho">Tamanho</param>
        /// <param name="direcao">Direcao do parametro</param>
        /// <param name="colunaOrigem">Coluna de origem mapeada para o <see cref="DataSet"/> e usado para carregar ou retorno o <see cref="DbParameter.Value"/></param>
        /// <returns>Instancia do parametro que foi adicionado</returns>
        public DbParameter CriarParametro(string nomeParametro, DbType tipoDado, int tamanho, ParameterDirection direcao, string colunaOrigem)
        {
            ChecarDisposed();
            DbParameter parametro = ContextoAcessoDado.ProviderFactory.CreateParameter();
            parametro.ParameterName = nomeParametro;
            parametro.DbType = tipoDado;
            parametro.Direction = direcao;
            parametro.Size = tamanho;
            if (colunaOrigem != null)
            {
                parametro.SourceColumn = colunaOrigem;
            }
            Command.Parameters.Add(parametro);
            if (EhParametroSaidaRetornoValor(direcao))
            {
                CommandWrapperSelec.ParametrosOutputReturnValue.Add(nomeParametro, parametro);
            }
            return parametro;
        }

        /// <summary>
        /// Adiciona parametros no <see cref="Command"/> sendo contruido por esta instancia
        /// </summary>
        /// <param name="nomeParametro">Nome do parametro</param>
        /// <param name="tipoDado">Tipo de dado do parametro</param>
        /// <param name="precisao">Número máximo de dígitos usado para ser representado por <see cref="DbParameter.Value"/></param>
        /// <param name="escala">Número de casas decimais que <see cref="DbParameter.Value"/> é resolvido</param>
        /// <param name="direcao">Direcao do parametro</param>
        /// <param name="colunaOrigem">Coluna de origem mapeada para o <see cref="DataSet"/> e usado para carregar ou retorno o <see cref="DbParameter.Value"/></param>
        /// <returns>Instancia do parametro que foi adicionado</returns>
        public DbParameter CriarParametro(string nomeParametro, DbType tipoDado, byte precisao, byte escala, ParameterDirection direcao, string colunaOrigem)
        {
            DbParameter parametro = CriarParametro(nomeParametro, tipoDado, 0, direcao, colunaOrigem);
            SqlParameter sqlParameter = parametro as SqlParameter;
            if (parametro == null)
            {
                throw new NotSupportedException("O provider especificado não suporte definição de precisão para o parâmetro");
            }
            sqlParameter.Precision = precisao;
            sqlParameter.Scale = escala;
            return parametro;
        }

        /// <summary>
        /// Adiciona parametros no <see cref="Command"/> sendo contruido por esta instancia
        /// </summary>
        /// <param name="nomeParametro">Nome do parametro</param>
        /// <param name="tipoDado">Tipo de dado do parametro</param>
        /// <param name="precisao">Número máximo de dígitos usado para ser representado por <see cref="DbParameter.Value"/></param>
        /// <param name="escala">Número de casas decimais que <see cref="DbParameter.Value"/> é resolvido</param>
        /// <param name="colunaOrigem">Coluna de origem mapeada para o <see cref="DataSet"/> e usado para carregar ou retorno o <see cref="DbParameter.Value"/></param>
        /// <returns>Instancia do parametro que foi adicionado</returns>
        public DbParameter CriarParametro(string nomeParametro, DbType tipoDado, byte precisao, byte escala, string colunaOrigem)
        {
            return CriarParametro(nomeParametro, tipoDado, precisao, escala, ParameterDirection.Input, colunaOrigem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="tipoDado"></param>
        /// <param name="tamanho"></param>
        /// <param name="colunaOrigem"></param>
        /// <returns></returns>
        public DbParameter CriarParametro(string nome, DbType tipoDado, int tamanho, string colunaOrigem)
        {
            return CriarParametro(nome, tipoDado, tamanho, ParameterDirection.Input, colunaOrigem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="tipoDado"></param>
        /// <param name="colunaOrigem"></param>
        /// <returns></returns>
        public DbParameter CriarParametro(string nome, DbType tipoDado, string colunaOrigem)
        {
            return CriarParametro(nome, tipoDado, -1, colunaOrigem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="tipoDado"></param>
        /// <param name="tamanho"></param>
        /// <param name="colunaOrigem"></param>
        /// <returns></returns>
        public DbParameter CriarParametroInOut(string nome, DbType tipoDado, int tamanho, string colunaOrigem)
        {
            return CriarParametro(nome, tipoDado, tamanho, ParameterDirection.InputOutput, colunaOrigem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="tipoDado"></param>
        /// <param name="colunaOrigem"></param>
        /// <returns></returns>
        public DbParameter CriarParametroInOut(string nome, DbType tipoDado, string colunaOrigem)
        {
            return CriarParametroInOut(nome, tipoDado, -1, colunaOrigem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="tipoDado"></param>
        /// <param name="tamanho"></param>
        /// <param name="colunaOrigem"></param>
        /// <returns></returns>
        public DbParameter CriarParametroOut(string nome, DbType tipoDado, int tamanho, string colunaOrigem)
        {
            return CriarParametro(nome, tipoDado, tamanho, ParameterDirection.Output, colunaOrigem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="tipoDado"></param>
        /// <param name="colunaOrigem"></param>
        /// <returns></returns>
        public DbParameter CriarParametroOut(string nome, DbType tipoDado, string colunaOrigem)
        {
            return CriarParametroOut(nome, tipoDado, -1, colunaOrigem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tipoDado"></param>
        /// <param name="tamanho"></param>
        /// <param name="colunaOrigem"></param>
        /// <returns></returns>
        public DbParameter CriarParametroRetorno(DbType tipoDado, int tamanho, string colunaOrigem)
        {
            return CriarParametro(NOME_PARAMETRO_RETORNO, tipoDado, tamanho, ParameterDirection.ReturnValue, colunaOrigem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tipoDado"></param>
        /// <param name="colunaOrigem"></param>
        /// <returns></returns>
        public DbParameter CriarParametroRetorno(DbType tipoDado, string colunaOrigem)
        {
            return CriarParametroRetorno(tipoDado, -1, colunaOrigem);
        }

        #endregion Criacao de parametros, sem valor inicial

        #region Adicao de parametros

        /// <summary>
        /// Adiciona parametros no <see cref="Command"/> sendo contruido por esta instancia
        /// </summary>
        /// <param name="nomeParametro">Nome do parametro</param>
        /// <param name="tipoDado">Tipo de dado do parametro</param>
        /// <param name="tamanho">Tamanho</param>
        /// <param name="direcao">Direcao do parametro</param>
        /// <param name="valor">Valor do parametro</param>
        /// <returns>Instancia do parametro que foi adicionado</returns>
        public DbParameter AdicionarParametro(string nomeParametro, DbType tipoDado, int tamanho, ParameterDirection direcao, object valor)
        {
            DbParameter parametro = CriarParametro(nomeParametro, tipoDado, tamanho, direcao, null);
            AtribuirValorParametro(parametro, valor);
            return parametro;
        }

        /// <summary>
        /// Adiciona parametros no <see cref="Command"/> sendo contruido por esta instancia
        /// </summary>
        /// <param name="nomeParametro">Nome do parametro</param>
        /// <param name="tipoDado">Tipo de dado do parametro</param>
        /// <param name="precisao">Número máximo de dígitos usado para ser representado por <see cref="DbParameter.Value"/></param>
        /// <param name="escala">Número de casas decimais que <see cref="DbParameter.Value"/> é resolvido</param>
        /// <param name="direcao">Direcao do parametro</param>
        /// <param name="valor">Valor do parametro</param>
        /// <returns>Instancia do parametro que foi adicionado</returns>
        public DbParameter AdicionarParametro(string nomeParametro, DbType tipoDado, byte precisao, byte escala, ParameterDirection direcao, object valor)
        {
            DbParameter parametro = CriarParametro(nomeParametro, tipoDado, precisao, escala, direcao, null);
            AtribuirValorParametro(parametro, valor);
            return parametro;
        }

        /// <summary>
        /// Adiciona parametros no <see cref="Command"/> sendo contruido por esta instancia
        /// </summary>
        /// <param name="nomeParametro">Nome do parametro</param>
        /// <param name="tipoDado">Tipo de dado do parametro</param>
        /// <param name="precisao">Número máximo de dígitos usado para ser representado por <see cref="DbParameter.Value"/></param>
        /// <param name="escala">Número de casas decimais que <see cref="DbParameter.Value"/> é resolvido</param>
        /// <param name="valor">Valor do parametro</param>
        /// <returns>Instancia do parametro que foi adicionado</returns>
        public DbParameter AdicionarParametro(string nomeParametro, DbType tipoDado, byte precisao, byte escala, object valor)
        {
            DbParameter parametro = CriarParametro(nomeParametro, tipoDado, precisao, escala, null);
            AtribuirValorParametro(parametro, valor);
            return parametro;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="tipoDado"></param>
        /// <param name="tamanho"></param>
        /// <param name="valor"></param>
        /// <returns></returns>
        public DbParameter AdicionarParametro(string nome, DbType tipoDado, int tamanho, object valor)
        {
            return AdicionarParametro(nome, tipoDado, tamanho, ParameterDirection.Input, valor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="tipoDado"></param>
        /// <param name="valor"></param>
        /// <returns></returns>
        public DbParameter AdicionarParametro(string nome, DbType tipoDado, object valor)
        {
            return AdicionarParametro(nome, tipoDado, -1, valor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="tipoDado"></param>
        /// <param name="tamanho"></param>
        /// <param name="valor"></param>
        /// <returns></returns>
        public DbParameter AdicionarParametroInOut(string nome, DbType tipoDado, int tamanho, object valor)
        {
            return AdicionarParametro(nome, tipoDado, tamanho, ParameterDirection.InputOutput, valor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="tipoDado"></param>
        /// <param name="valor"></param>
        /// <returns></returns>
        public DbParameter AdicionarParametroInOut(string nome, DbType tipoDado, object valor)
        {
            return AdicionarParametroInOut(nome, tipoDado, -1, valor);
        }

        #endregion Adicao de parametros

        #endregion Parametros

        #region Execucoes padrao command

        /// <summary>
        /// Executa um sql que nao retorna valores
        /// </summary>
        /// <returns>Quantidade de registros afetados na execução</returns>
        public int ExecuteNonQuery()
        {
            ChecarDisposed();
            ConnectionState estadoOriginal;
            BdUtil.AbrirConexao(ContextoAcessoDado.Connection, out estadoOriginal);
            try
            {
                RegistrarLogCommand();
                return Command.ExecuteNonQuery();
            }
            finally
            {
                BdUtil.FecharConexao(ContextoAcessoDado.Connection, estadoOriginal);
            }
        }

        /// <summary>
        /// Retorna dados somente leitura e sequencial usando <see cref="DbDataReader"/> 
        /// </summary>
        /// <returns></returns>
        public DbDataReader ExecuteReader()
        {
            ChecarDisposed();
            ConnectionState estadoOriginal;
            // Abre e mantem a conexao aberta para se trabalhar com o Reader
            BdUtil.AbrirConexao(this.ContextoAcessoDado.Connection, out estadoOriginal);
            RegistrarLogCommand();
            return Command.ExecuteReader();
        }

        /// <summary>
        /// Retorna a primeira coluna do primeiro registro do resultado retornado pela query. Todos as outras
        /// colunas e linhas são ignoradas
        /// </summary>
        /// <returns></returns>
        public object ExecuteScalar()
        {
            ChecarDisposed();
            ConnectionState estadoOriginal;
            BdUtil.AbrirConexao(ContextoAcessoDado.Connection, out estadoOriginal);
            try
            {
                RegistrarLogCommand();
                return Command.ExecuteScalar();
            }
            finally
            {
                BdUtil.FecharConexao(ContextoAcessoDado.Connection, estadoOriginal);
            }
        }

        /// <summary>
        /// Retorna um unico valor de uma consulta
        /// </summary>
        /// <typeparam name="T">Tipo do dado a ser retornado</typeparam>
        /// <exception cref="InvalidCastException">O tipo retornado nao pode ser convertido para <typeparamref name="T"/></exception>
        /// <exception cref="NullReferenceException"><typeparamref name="T"/> nao pode ter um valor nulo e um valor nulo foi retornado</exception>
        /// <returns>Dado do tipo <typeparamref name="T"/></returns>
        public T ExecuteScalar<T>()
        {
            ChecarDisposed();
            ConnectionState estadoOriginal;
            BdUtil.AbrirConexao(ContextoAcessoDado.Connection, out estadoOriginal);
            try
            {
                object valor = this.ExecuteScalar();
                if (valor == null || Convert.IsDBNull(valor))
                {
                    return default(T);
                }
                return (T)Convert.ChangeType(valor, typeof(T));
            }
            finally
            {
                BdUtil.FecharConexao(ContextoAcessoDado.Connection, estadoOriginal);
            }
        }

        #endregion Execucoes padrao command

        #region Operacoes DataSet e DataTable

        /// <summary>
        /// Adiciona ou atualiza linhas em um <see cref="DataTable"/> para igualar às linhas do data source
        /// usando o nome do <see cref="DataTable"/>, o COMANDO SQL especificado e o <see cref="CommandBehavior"/>.
        /// </summary>
        /// <param name="dataSet">Um <see cref="DataSet"/> para preencher com registros e, se necessário, com schema</param>
        /// <returns>Quantidade de linhas adicionadas e atualizadas</returns>
        public int Fill(DataSet dataSet)
        {
            ChecarDisposed();
            RegistrarLogCommand();
            int retorno = DataAdapter.Fill(dataSet);
            return retorno;
        }

        /// <summary>
        /// Adiciona ou atualiza linhas em um <see cref="DataTable"/> para igualar às linhas do data source
        /// usando o nome do <see cref="DataTable"/>, o COMANDO SQL especificado e o <see cref="CommandBehavior"/>.
        /// </summary>
        /// <param name="dataSet">Um <see cref="DataSet"/> para preencher com registros e, se necessário, com schema</param>
        /// <param name="srcTables">Lista de nomes de tabelas, por ordem de retorno, a ser usado para mapeamento das tabelas retornadas</param>
        /// <returns>Quantidade de linhas adicionadas e atualizadas</returns>
        public int Fill(DataSet dataSet, params string[] srcTables)
        {
            ChecarDisposed();
            RegistrarLogCommand();
            int retorno;
            // Determina o tipo de mapeamento a ser usado
            if (srcTables == null || srcTables.Length == 0)
            {
                // Se nenhuma tabela de origem entao usa o nome retornado pela query
                retorno = this.Fill(dataSet);
            }
            else if (srcTables.Length == 1)
            {
                // Usa o overload disponivel em DbDataAdapter se apenas uma tabela foi informado
                retorno = DataAdapter.Fill(dataSet, srcTables[0]);
            }
            else
            {
                ConfigTableMappings(DataAdapter, srcTables);
                retorno = DataAdapter.Fill(dataSet);
            }
            return retorno;
        }

        /// <summary>
        /// Cria uma colecao de <see cref="DataTableMapping"/> a partir de <paramref name="srcTables"/>
        /// e associa ao <paramref name="adapter"/>
        /// </summary>
        /// <param name="adapter"></param>
        /// <param name="srcTables"></param>
        private static void ConfigTableMappings(DbDataAdapter adapter, string[] srcTables)
        {
            for (int i = 0; i < srcTables.Length; i++)
            {
                string nomeDataTable = "Table" + (i == 0 ? String.Empty : i.ToString());
                adapter.TableMappings.Add(nomeDataTable, srcTables[i]);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public int Fill(DataTable dataTable)
        {
            ChecarDisposed();
            RegistrarLogCommand();
            int retorno = DataAdapter.Fill(dataTable);
            return retorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet ObterDataSet()
        {
            DataSet dataSet = new DataSet();
            Fill(dataSet);
            return dataSet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable ObterDataTable()
        {
            DataTable dataTable = new DataTable();
            Fill(dataTable);
            return dataTable;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T ObterDataSet<T>() where T : DataSet, new()
        {
            T dataSet = new T();
            Fill(dataSet);
            return dataSet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="srcTables"></param>
        /// <returns></returns>
        public T ObterDataSet<T>(params string[] srcTables) where T : DataSet, new()
        {
            T dataSet = new T();
            Fill(dataSet, srcTables);
            return dataSet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T ObterDataTable<T>() where T : DataTable, new()
        {
            T dataTable = new T();
            Fill(dataTable);
            return dataTable;
        }

        /// <summary>
        /// Atualiza os registros do <see cref="DataSet"/>, atraves dos comandos configurados para insert, 
        /// update e delete, usando <see cref="TipoCommandDataAdapter"/>. 
        /// Se uma transacao já existe no <see cref="Escopo"/> ela é usada, caso contrário uma nova transação
        /// é iniciada para todo o processo de atualização.
        /// </summary>
        /// <param name="dataSet"></param>
        /// <returns></returns>
        public int Update(DataSet dataSet)
        {
            return this.Update(dataSet, OpcaoTransacao.Requerido);
        }

        /// <summary>
        /// Atualiza os registros do <see cref="DataSet"/>, atraves dos comandos configurados para insert, 
        /// update e delete, usando <see cref="TipoCommandDataAdapter"/>
        /// </summary>
        /// <param name="dataSet"></param>
        /// <param name="opcaoTransacao">Indica o tipo de transação para a atualização</param>
        /// <returns></returns>
        public int Update(DataSet dataSet, OpcaoTransacao opcaoTransacao)
        {
            using (Escopo escopo = new Escopo(opcaoTransacao))
            {
                int retorno = DataAdapter.Update(dataSet);
                escopo.Terminar();
                return retorno;
            }
        }

        /// <summary>
        /// Atualiza os registros do <see cref="DataSet"/>, correspondente ao <paramref name="srcTable"/>, 
        /// atraves dos comandos configurados para insert, update e delete, 
        /// usando <see cref="TipoCommandDataAdapter"/>. 
        /// Se uma transacao já existe no <see cref="Escopo"/> ela é usada, caso contrário uma nova transação
        /// é iniciada para todo o processo de atualização.
        /// </summary>
        /// <param name="dataSet"></param>
        /// <param name="srcTable"></param>
        /// <returns></returns>
        public int Update(DataSet dataSet, string srcTable)
        {
            return this.Update(dataSet.Tables[srcTable], OpcaoTransacao.Requerido);
        }

        /// <summary>
        /// Atualiza os registros do <see cref="DataTable"/>, atraves dos comandos configurados para insert, 
        /// update e delete, usando <see cref="TipoCommandDataAdapter"/>. 
        /// Se uma transacao já existe no <see cref="Escopo"/> ela é usada, caso contrário uma nova transação
        /// é iniciada para todo o processo de atualização.
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public int Update(DataTable dataTable)
        {
            return this.Update(dataTable, OpcaoTransacao.Requerido);
        }

        /// <summary>
        /// Atualiza os registros do <see cref="DataTable"/>, atraves dos comandos configurados para insert, 
        /// update e delete, usando <see cref="TipoCommandDataAdapter"/>
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="opcaoTransacao"></param>
        /// <returns></returns>
        public int Update(DataTable dataTable, OpcaoTransacao opcaoTransacao)
        {
            using (Escopo escopo = new Escopo(opcaoTransacao))
            {
                int retorno = DataAdapter.Update(dataTable);
                escopo.Terminar();
                return retorno;
            }
        }

        /// <summary>
        /// Atualiza as linhas, atraves dos comandos configurados para insert, 
        /// update e delete, usando <see cref="TipoCommandDataAdapter"/>. 
        /// Se uma transacao já existe no <see cref="Escopo"/> ela é usada, caso contrário uma nova transação
        /// é iniciada para todo o processo de atualização.
        /// </summary>
        /// <param name="dataRows"></param>
        /// <returns></returns>
        public int Update(DataRow[] dataRows)
        {
            return this.Update(dataRows, OpcaoTransacao.Requerido);
        }

        /// <summary>
        /// Atualiza as linhas, atraves dos comandos configurados para insert, 
        /// update e delete, usando <see cref="TipoCommandDataAdapter"/>
        /// </summary>
        /// <param name="dataRows"></param>
        /// <param name="opcaoTransacao"></param>
        /// <returns></returns>
        public int Update(DataRow[] dataRows, OpcaoTransacao opcaoTransacao)
        {
            using (Escopo escopo = new Escopo(opcaoTransacao))
            {
                int retorno = DataAdapter.Update(dataRows);
                escopo.Terminar();
                return retorno;
            }
        }

        #endregion Consulta DataSet e DataTable

        #region Configuracao dos comandos

        private DbCommand ObterCommand(TipoCommandDataAdapter tipoCommandDataAdapter)
        {
            return BdUtil.ObterCommand(this.ContextoAcessoDado, tipoCommandDataAdapter, _CommandWrappersDataAdapter);
        }

        private void ConfigCommand(TipoCommandDataAdapter tipoCommandDataAdapter, string commandText)
        {
            ConfigCommand(tipoCommandDataAdapter, commandText, CommandType.Text);
        }

        private void ConfigCommand(TipoCommandDataAdapter tipoCommandDataAdapter, string commandText, CommandType commandType)
        {
            ConfigCommand(tipoCommandDataAdapter, commandText, commandType, CommandWrapper.COMMAND_TIMEOUT);
        }

        private void ConfigCommand(TipoCommandDataAdapter tipoCommandDataAdapter, string commandText, CommandType commandType, int commandTimeout)
        {
            CommandWrapper commandWrapper;
            if (!_CommandWrappersDataAdapter.ContainsKey(tipoCommandDataAdapter))
            {
                commandWrapper = new CommandWrapper(commandText, commandType);
                _CommandWrappersDataAdapter.Add(tipoCommandDataAdapter, commandWrapper);
            }
            else
            {
                commandWrapper = _CommandWrappersDataAdapter[tipoCommandDataAdapter];
                commandWrapper.CommandText = commandText;
                commandWrapper.CommandType = commandType;
                commandWrapper.CommandTimeout = commandTimeout;
            }
            // Se já existe DataAdapter criada entao atualiza o seu command
            AtualizarCommandSeDataAdapterJaExiste(tipoCommandDataAdapter, commandWrapper);
        }

        private void AtualizarCommandSeDataAdapterJaExiste(TipoCommandDataAdapter tipoCommandDataAdapter, CommandWrapper commandWrapper)
        {
            if (_DataAdapter != null)
            {
                switch (tipoCommandDataAdapter)
                {
                    case TipoCommandDataAdapter.Select:
                        DataAdapter.SelectCommand = commandWrapper.ObterCommand(ContextoAcessoDado);
                        break;
                    case TipoCommandDataAdapter.Insert:
                        DataAdapter.InsertCommand = commandWrapper.ObterCommand(ContextoAcessoDado);
                        break;
                    case TipoCommandDataAdapter.Update:
                        DataAdapter.UpdateCommand = commandWrapper.ObterCommand(ContextoAcessoDado);
                        break;
                    case TipoCommandDataAdapter.Delete:
                        DataAdapter.DeleteCommand = commandWrapper.ObterCommand(ContextoAcessoDado);
                        break;
                }
            }
        }

        //public void ConfigSelectCommand(string commandText, CommandType commandType)
        //{
        //    ConfigCommand(TipoCommandDataAdapter.Select, commandText, commandType);
        //}

        //public void ConfigSelectCommand(string commandText)
        //{
        //    ConfigCommand(TipoCommandDataAdapter.Select, commandText, CommandType.Text);
        //}

        //public void ConfigInsertCommand(string commandText, CommandType commandType)
        //{
        //    ConfigCommand(TipoCommandDataAdapter.Insert, commandText, commandType);
        //}

        //public void ConfigInsertCommand(string commandText)
        //{
        //    ConfigCommand(TipoCommandDataAdapter.Insert, commandText, CommandType.Text);
        //}

        //public void ConfigUpdateCommand(string commandText, CommandType commandType)
        //{
        //    ConfigCommand(TipoCommandDataAdapter.Update, commandText, commandType);
        //}

        //public void ConfigUpdateCommand(string commandText)
        //{
        //    ConfigCommand(TipoCommandDataAdapter.Update, commandText, CommandType.Text);
        //}

        //public void ConfigDeleteCommand(string commandText, CommandType commandType)
        //{
        //    ConfigCommand(TipoCommandDataAdapter.Delete, commandText, commandType);
        //}

        //public void ConfigDeleteCommand(string commandText)
        //{
        //    ConfigCommand(TipoCommandDataAdapter.Delete, commandText, CommandType.Text);
        //}

        #endregion Outras operações

        #region IDisposable Members
        /// <summary>
        /// Retorna o nome do objeto que causou o erro de Dispoded
        /// </summary>
        protected override string MensagemErroObjectDisposedException
        {
            get { return "BdUtil"; }
        }

        /// <summary>
        /// Libera os recursos associados a classe
        /// </summary>
        /// <param name="disposing">Indica se o metodo esta sendo chamado por dispose ou pelo finalizador</param>
        protected override void Dispose(bool disposing)
        {
            // Se chamado pelo dispose libera componentes gerenciados
            if (disposing)
            {
                foreach (CommandWrapper commandWrapper in _CommandWrappersDataAdapter.Values)
                {
                    commandWrapper.Dispose();
                }
                if (_FazerDisposeContextoAcessoDado && _ContextoAcessoDado != null)
                    _ContextoAcessoDado.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion

        #region Informacoes log

        [Conditional("DEBUG")]
        private void RegistrarLogCommand()
        {
            StringBuilder textoLog = new StringBuilder();
            string textoParams = String.Empty;
            ObterTextoParametros(ref textoParams);

            textoLog.AppendLine("\"" + Command.CommandText + "\"");
            textoLog.AppendLine(textoParams);
            Log.DetalhesDebugNg(textoLog.ToString());
        }

        [Conditional("DEBUG")]
        private void ObterTextoParametros(ref string textoParams)
        {
            List<String> parametros = new List<string>(Command.Parameters.Count);
            foreach (DbParameter parametro in Command.Parameters)
            {
                parametros.Add(
                    String.Format("{0}({1})=({2}){3}",
                    parametro.ParameterName,
                    Enum.GetName(typeof(ParameterDirection), parametro.Direction),
                    parametro.DbType,
                    parametro.Value)
                    );
            }
            textoParams = String.Join(", ", parametros.ToArray());
        }

        #endregion Informacoes log
    }
}
