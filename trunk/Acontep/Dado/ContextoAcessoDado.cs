using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Acontep.Dado
{
    /// <summary>
    /// 
    /// </summary>
    public class ContextoAcessoDado : Componente
    {
        internal const string NOME_CONEXAO_GERAL = "Geral";

        private string _ConnectionStringSettingName;
        private DbConnection _Connection;
        private DbTransaction _Transaction;
        private DbProviderFactory _ProviderFactory;

        /// <summary>
        /// 
        /// </summary>
        public string ConnectionStringSettingName
        {
            get 
            {
                ChecarDisposed();
                return _ConnectionStringSettingName;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DbConnection Connection
        {
            get 
            {
                ChecarDisposed();
                if (this._Connection == null && this._ProviderFactory != null)
                    this._Connection = ProviderFactory.CreateConnection();
                return this._Connection;
            }
            set 
            {
                ChecarDisposed();
                _ConnectionStringSettingName = String.Empty;
                this._Connection = value; 
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DbTransaction Transaction
        {
            get 
            {
                ChecarDisposed();
                return this._Transaction;
            }
            set 
            {
                ChecarDisposed();
                this._Transaction = value; 
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DbProviderFactory ProviderFactory
        {
            get 
            {
                ChecarDisposed();
                return this._ProviderFactory;
            }
            set 
            {
                ChecarDisposed();
                this._ProviderFactory = value; 
            }
        }

        /// <summary>
        /// Construtor usando configuração de string de conexao customizada, independente de app.config
        /// </summary>
        /// <param name="providerFactory">Provider a ser usado</param>
        /// <param name="connectionString">String de conexao</param>
        public ContextoAcessoDado(DbProviderFactory providerFactory, string connectionString)
        {
            if (providerFactory == null || String.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("ProviderFactory e ConnectionString precisam conter um valor");
            }
            this._ProviderFactory = providerFactory;
            this._ConnectionStringSettingName = String.Empty;
            this._Connection = this.ProviderFactory.CreateConnection();
            this._Connection.ConnectionString = connectionString;
        }

        /// <summary>
        /// Construtor usando configuração de string de conexao customizada, independente de app.config
        /// </summary>
        /// <param name="providerName">Nome do provider a ser usado</param>
        /// <param name="connectionString">String de conexao</param>
        public ContextoAcessoDado(string providerName, string connectionString)
        {
            if (String.IsNullOrEmpty(providerName) || String.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("ProviderName e ConnectionString precisam conter um valor");
            }
            this._ProviderFactory = DbProviderFactories.GetFactory(providerName);
            this._ConnectionStringSettingName = String.Empty;
            this._Connection = this.ProviderFactory.CreateConnection();
            this._Connection.ConnectionString = connectionString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionStringSettingName"></param>
        public ContextoAcessoDado(string connectionStringSettingName)
        {
            ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings[connectionStringSettingName];
            if (connectionStringSettings == null)
            {
                throw new InvalidOperationException("A conexão especificada não existe em <connectionString> em <configuration>. Se nenhum conexão foi especificada a conexão Geral precisa estar definida");
            }
            this._ProviderFactory = DbProviderFactories.GetFactory(connectionStringSettings.ProviderName);
            this._ConnectionStringSettingName = connectionStringSettings.Name;
            this._Connection = this.ProviderFactory.CreateConnection();
            this._Connection.ConnectionString = connectionStringSettings.ConnectionString;
        }

        /// <summary>
        /// 
        /// </summary>
        public ContextoAcessoDado()
            : this(ContextoAcessoDado.NOME_CONEXAO_GERAL)
        {
        }

        #region IDisposable Members


        /// <summary>
        /// 
        /// </summary>
        protected override string MensagemErroObjectDisposedException
        {
            get { return "ContextoAcessoDado"; }
        }

        /// <summary>
        /// Libera recursos envolvidos
        /// </summary>
        /// <param name="disposing">Indica se o metodo esta sendo chamado por dispose ou pelo finalizador</param>
        protected override void Dispose(bool disposing)
        {
            // Se chamado pelo dispose libera componentes gerenciados
            if (!EstaFinalizado && disposing)
            {
                Exception transactionException = null;
                try
                {
                    if (_Transaction != null)
                        _Transaction.Dispose();
                }
                catch (Exception exception)
                {
                    //Garante que qualquer erro no dispose da transacao nao impeca
                    //a chamada do dispose da conexao
                    transactionException = exception;
                }
                if (_Connection != null)
                {
                    if (_Connection.State == ConnectionState.Open)
                        _Connection.Close();
                    _Connection.Dispose();
                }
                //Se ocorreu alguma excessao no dispose da transacao dispara agora
                if (transactionException != null)
                    throw transactionException;
            }
            base.Dispose(disposing);
        }

        #endregion
    }
}
