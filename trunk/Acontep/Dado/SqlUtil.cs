using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;

namespace Secv.Dado
{
    /// <summary>
    /// Classe encapsulando comandos SQL sobre a API ADO.NET
    /// </summary>
    sealed public class SqlUtil
    {
        // String de conexão Conexão
        private ConnectionStringSettings strConnection;
        // Provider, para fornecer generecidade de banco de dados
        //DbProviderFactory providerFactory;
        // Conexão
        SqlConnection connection;
        // Transação
        private SqlTransaction transaction;
        // Lista de parâmetros
        private Dictionary<string, SqlParameter> parametros;
        // Timeout dos comandos
        private int comandoTimeout;
        // Valor do parâmetro de retorno de funções armazenadas
        private SqlParameter parametroRetorno;

        /// Nome padrão da string de conexão
        public static string NomeStringConexao
        {
            get
            {
                return "Geral";
            }
        }

        /// Nome do parâmetro de saída
        public static string NomeParametroRetorno
        {
            get
            {
                return "_pRetorno";
            }
        }

        /// <summary>
        /// Inicializa a classe
        /// </summary>
        /// <param name="stringDeConexao">Nome da string de conexão no arquivo de configuração</param>
        public SqlUtil(string stringDeConexao)
            : this(ConfigurationManager.ConnectionStrings[stringDeConexao])
        {
        }

        /// <summary>
        /// Inicializa a classe.
        /// Recupera a String de conexão do arquivo de configuração com o a
        /// chave com o valor armazenado na propriedade SqlUtil.NomeStringConexao
        /// </summary>
        public SqlUtil()
            : this(ConfigurationManager.ConnectionStrings[NomeStringConexao])
        {
        }

        /// <summary>
        /// Inicizaliza a classe 
        /// </summary>
        /// <param name="conexao">Configuração da String de conexão</param>
        public SqlUtil(ConnectionStringSettings conexao)
        {
            if (conexao == null)
            {
                throw new Exception("String de conexão nula");
            }
            //this.providerFactory = DbProviderFactories.GetFactory(conexao.ProviderName);
            this.comandoTimeout = 15;
            this.strConnection = conexao;
            this.connection = new SqlConnection(strConnection.ConnectionString);// providerFactory.CreateConnection();
            this.parametros = new Dictionary<string, SqlParameter>();
            this.transaction = null;
            // Parametro de retorno
            this.parametroRetorno = new SqlParameter();//providerFactory.CreateParameter();
            this.parametroRetorno.ParameterName = NomeParametroRetorno;
            this.parametroRetorno.Direction = ParameterDirection.ReturnValue;
        }

        /// <summary>
        /// 
        /// </summary>
        ~SqlUtil()
        {
            if (transaction != null)
            {
                transaction.Rollback();
            }
            Close();
        }

        /// <summary>
        /// Timeout para a execução dos comandos. Valor padrão 15 segundos
        /// </summary>
        public int CommandTimeout
        {
            set
            {
                comandoTimeout = value;
            }
            get
            {
                return comandoTimeout;
            }
        }

        /// <summary>
        /// Valor retornado por uma Store Procedure
        /// </summary>
        public object ValorRetorno
        {
            get
            {
                return parametroRetorno.Value;
            }
        }

        /// <summary>
        /// Abre a conexão
        /// </summary>
        private void Open()
        {
            if (this.connection.State != ConnectionState.Open)
            {
                this.connection.Open();
            }
        }

        /// <summary>
        /// Fecha a conexão
        /// </summary>
        public void Close()
        {
            if (this.connection.State == ConnectionState.Open)
            {
                this.connection.Close();
            }
        }

        /// <summary>
        /// Cria uma transação. A conexão é aberta
        /// </summary>
        public void BeginTransaction()
        {
            if (transaction != null)
            {
                throw new Exception("Transação já iniciada");
            }
            Open();
            this.transaction = connection.BeginTransaction();
        }

        /// <summary>
        /// Confirmação da transação. Fecha a conexão
        /// </summary>
        public void Commit()
        {
            if (transaction != null)
            {
                this.transaction.Commit();
                this.connection.Close();
                this.transaction = null;
            }
            else
            {
                throw new Exception("Transação não iniciada");
            }
        }

        /// <summary>
        /// Cancelamento da transação. Fecha a conexão
        /// </summary>
        public void Rollback()
        {
            if (transaction != null)
            {
                this.transaction.Rollback();
                this.connection.Close();
                this.transaction = null;
            }
            else
            {
                throw new Exception("Transação não iniciada");
            }
        }

        /// <summary>
        /// Adiciona o parâmetro
        /// </summary>
        /// <param name="nomeParametro">Nome</param>
        /// <param name="tipo">Tipo</param>
        /// <param name="tamanho">Tamanho</param>
        /// <param name="valor">Valor</param>
        public void AdicionarParametro(string nome, DbType tipo, int tamanho, Object valor)
        {
            SqlParameter parameter = new SqlParameter();// providerFactory.CreateParameter();
            parameter.ParameterName = nome;
            parameter.DbType = tipo;
            parameter.Size = tamanho;
            parameter.Value = valor;
            parametros.Remove(parameter.ParameterName);
            this.parametros.Add(parameter.ParameterName, parameter);
        }

        /// <summary>
        /// Adiciona o parâmetro
        /// </summary>
        /// <param name="nomeParametro">Nome</param>
        /// <param name="tipo">Tipo</param>
        /// <param name="valor">Valor</param>
        public void AdicionarParametro(string nome, DbType tipo, Object valor)
        {
            SqlParameter parameter = new SqlParameter();//providerFactory.CreateParameter();
            parameter.ParameterName = nome;
            parameter.DbType = tipo;
            parameter.Value = valor;
            parametros.Remove(parameter.ParameterName);
            this.parametros.Add(parameter.ParameterName, parameter);
        }

        /// <summary>
        /// Adiciona um parâmetro de retorno para uma rotina armazenada
        /// </summary>
        /// <param name="nomeParametro">Nome</param>
        /// <param name="tipo">Tipo</param>
        public void AdicionarParametroSaida(string nome, DbType tipo)
        {
            SqlParameter parameter = new SqlParameter();//providerFactory.CreateParameter();
            parameter.ParameterName = nome;
            parameter.DbType = tipo;
            parameter.Direction = ParameterDirection.Output;
            parametros.Remove(parameter.ParameterName);
            this.parametros.Add(parameter.ParameterName, parameter);
        }

        /// <summary>
        /// Adiciona um parâmetro de retorno para uma rotina armazenada
        /// </summary>
        /// <param name="nomeParametro">Nome</param>
        /// <param name="tipo">Tipo</param>
        /// <param name="tamanho">Tamanho do parâmetro</param>
        public void AdicionarParametroSaida(string nome, DbType tipo, int tamanho)
        {
            SqlParameter parameter = new SqlParameter();//providerFactory.CreateParameter();
            parameter.ParameterName = nome;
            parameter.DbType = tipo;
            parameter.Size = tamanho;
            parameter.Direction = ParameterDirection.Output;
            parametros.Remove(parameter.ParameterName);
            this.parametros.Add(parameter.ParameterName, parameter);
        }

        /// <summary>
        /// Obtem um parâmetro da coleção de parâmetros armazenada.
        /// Deve ser utilizada para obter os valores dos parâmetros de retorno de rotinas armazenadas
        /// </summary>
        /// <param name="pNome">Nome</param>
        /// <returns>Valor do parâmetro</returns>
        public object ObterValorParametro(string pNome)
        {
            SqlParameter parametro = (SqlParameter)this.parametros[pNome];
            if (parametro != null)
            {
                return parametro.Value;
            }
            return null;
        }

        /// <summary>
        /// Remove o parâmetro
        /// </summary>
        /// <param name="pNome"></param>
        public void RemoverParametro(string pNome)
        {
            this.parametros.Remove(pNome);
        }

        /// <summary>
        /// Limpa a coleção de parâmetros
        /// </summary>
        public void LimparParametros()
        {
            this.parametros.Clear();
        }

        /// <summary>
        /// Executa um comando de consulta (SELECT)
        /// </summary>
        /// <param name="sql">texto do comando SELECT,
        /// onde os parâmetros devem seguir o padrão ADO.NET</param>
        /// <returns>DataTable com o resultado da consulta</returns>
        public DataTable ExecutarConsulta(string sql)
        {
            DataTable table = new DataTable();
            try
            {
                Open();
                SqlCommand command = new SqlCommand();//providerFactory.CreateCommand();
                command.CommandText = sql;
                command.Connection = connection;
                command.CommandTimeout = comandoTimeout;
                command.Transaction = this.transaction;
                ConfigParametros(command);
                SqlDataAdapter adapter = new SqlDataAdapter();//providerFactory.CreateDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(table);
                AtualizarValoresDeParametros(command);
            }
            finally
            {
                if (this.transaction == null)
                {
                    Close();
                }
            }
            return table;
        }

        public object ExecutarEscalar(string sql)
        {
            object retorno = null;
            try
            {
                Open();
                SqlCommand command = new SqlCommand();//providerFactory.CreateCommand();
                command.CommandText = sql;
                command.Connection = connection;
                command.CommandTimeout = comandoTimeout;
                command.Transaction = this.transaction;
                ConfigParametros(command);
                retorno = command.ExecuteScalar();
                AtualizarValoresDeParametros(command);
            }
            finally
            {
                if (this.transaction == null)
                {
                    Close();
                }
            }
            return retorno;
        }

        /// <summary>
        /// Executa uma rotina armazenada.
        /// </summary>
        /// <param name="nomeProcedimento">Nome da rotina armazenada</param>
        public void ExecutarRotina(string nomeProcedimento)
        {
            try
            {
                Open();
                SqlCommand command = new SqlCommand();//providerFactory.CreateCommand();
                command.CommandText = nomeProcedimento;
                command.Connection = connection;
                command.CommandTimeout = comandoTimeout;
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = this.transaction;
                ConfigParametros(command);
                command.ExecuteNonQuery();
                AtualizarValoresDeParametros(command);
            }
            finally
            {
                if (this.transaction == null)
                {
                    this.Close();
                }
            }
        }

        /// <summary>
        /// Executa uma rotina armazenada que tem como retorno um DataTable
        /// </summary>
        /// <param name="nomeProcedimento">Nome da rotina armazenada</param>
        /// <param name="tabelaRetorno">DataTable com os dados retornados</param>
        public DataTable ExecutarRotinaConsulta(string nomeProcedimento)
        {
            try
            {
                Open();
                SqlCommand command = new SqlCommand();//providerFactory.CreateCommand();
                command.CommandText = nomeProcedimento;
                command.Connection = connection;
                command.CommandTimeout = comandoTimeout;
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = this.transaction;
                ConfigParametros(command);
                SqlDataAdapter adapter = new SqlDataAdapter();//providerFactory.CreateDataAdapter();
                adapter.SelectCommand = command;
                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
            finally
            {
                if (this.transaction == null)
                {
                    this.connection.Close();
                }
            }
        }

        /// <summary>
        /// Executa um comando de atualização (INSERT, UPDATE, DELETE, ...),
        /// </summary>
        /// <param name="sql">Texto com o comando SQL, onde os parâmetros 
        /// devem seguir o padrão ADO.NET</param>
        /// <returns>Número de linhas afetadas</returns>
        public int ExecutarAtualizacao(string sql)
        {
            try
            {
                Open();
                SqlCommand command = new SqlCommand();//providerFactory.CreateCommand();
                command.CommandText = sql;
                command.Connection = connection;
                command.CommandTimeout = comandoTimeout;
                command.Transaction = this.transaction;
                ConfigParametros(command);
                int resp = command.ExecuteNonQuery();
                return resp;
            }
            finally
            {
                if (this.transaction == null)
                {
                    this.connection.Close();
                }
            }
        }

        /// <summary>
        /// Atualiza os valores dos parâmetros de saída do comando (procedure) SQL
        /// na coleção de parâmetros da classe
        /// </summary>
        /// <param name="command">SqlCommand</param>
        private void AtualizarValoresDeParametros(SqlCommand command)
        {
            foreach (SqlParameter param in command.Parameters)
            {
                if (param.Direction == ParameterDirection.Output ||
                    param.Direction == ParameterDirection.InputOutput)
                {
                    SqlParameter p = parametros[param.ParameterName];
                    if (p == null)
                    {
                        throw new Exception("Parâmetro de saída não encontrado");
                    }
                    else
                    {
                        p.Value = param.Value;
                    }
                }
            }
            parametroRetorno.Value = command.Parameters[NomeParametroRetorno].Value;
        }

        /// <summary>
        /// Preenche o DataSet com a consulta executada
        /// </summary>
        /// <param name="sql">Consulta SQL</param>
        /// <param name="dataset">DataSet</param>
        /// <param name="tableName">Nome da tabela</param>
        public void ExecutarConsulta(string sql, DataSet dataset, string tableName)
        {
            try
            {
                Open();
                SqlCommand command = new SqlCommand();//providerFactory.CreateCommand();
                command.CommandText = sql;
                command.Connection = connection;
                command.CommandTimeout = comandoTimeout;
                ConfigParametros(command);
                SqlDataAdapter adapter = new SqlDataAdapter(command);//providerFactory.CreateDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(dataset, tableName);
            }
            finally
            {
                if (this.transaction == null)
                {
                    this.connection.Close();
                }
            }
        }

        /// <summary>
        /// Configura os parâmetros contidos no ArrayList no SqlCommand
        /// </summary>
        /// <param name="comando">comando sql</param>
        private void ConfigParametros(SqlCommand comando)
        {
            foreach (SqlParameter parametro in this.parametros.Values)
            {
                // Substituíndo parâmetros null por DBNull
                if (parametro.Value == null)
                {
                    parametro.Value = DBNull.Value;
                }
                else
                {
                    // Substituíndo valores de parâmetros strings vazios por DBNUll
                    if (parametro.Value is string)
                    {
                        string valor = (string)parametro.Value;
                        if (valor.Trim().Length == 0)
                        {
                            parametro.Value = DBNull.Value;
                        }
                    }
                    else
                    {
                        // Substituíndo valores de parâmetros DateTime MinValue por DBNull
                        if (parametro.Value is DateTime)
                        {
                            DateTime valor = (DateTime)parametro.Value;
                            if (valor == DateTime.MinValue)
                            {
                                parametro.Value = DBNull.Value;
                            }
                        }
                    }
                }
                // Criando e atribuíndo o valor do novo parâmetro para ser utilizado no comando
                SqlParameter novoParamentro = new SqlParameter();//providerFactory.CreateParameter();
                novoParamentro.ParameterName = parametro.ParameterName;
                novoParamentro.DbType = parametro.DbType;
                novoParamentro.Size = parametro.Size;
                if (parametro.Direction == ParameterDirection.Input ||
                    parametro.Direction == ParameterDirection.InputOutput)
                {
                    novoParamentro.Value = parametro.Value;
                }
                novoParamentro.Direction = parametro.Direction;
                comando.Parameters.Add(novoParamentro);
            }
            //
            // Adicionando o parâmetro de retorno da rotina armazenada
            //
            SqlParameter novoParametroRetorno = new SqlParameter();//providerFactory.CreateParameter();
            novoParametroRetorno.ParameterName = NomeParametroRetorno;
            novoParametroRetorno.Direction = ParameterDirection.ReturnValue;
            comando.Parameters.Add(novoParametroRetorno);
        }
    }
}
