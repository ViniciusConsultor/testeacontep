using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;

namespace Acontep.Dado
{
    /// <summary>
    /// Encapsula um command, sem instanciar ele
    /// </summary>
    internal class CommandWrapper : Componente
    {
        internal const int COMMAND_TIMEOUT = 30;
        
        #region Campos
        
        string _CommandText;
        CommandType _CommandType;
        int _CommandTimeout = COMMAND_TIMEOUT;
        DbCommand _Command;
        private Dictionary<System.String, DbParameter> _ParametrosOutputReturnValue;

        #endregion Campos

        /// <summary>
        /// Obtem ou atribui o tempo de timeout do command
        /// </summary>
        public int CommandTimeout
        {
            get
            {
                ChecarDisposed();
                if (_Command != null)
                {
                    return _Command.CommandTimeout;
                }
                else
                {
                    return _CommandTimeout;
                }
            }
            set
            {
                ChecarDisposed();
                if (_Command != null)
                {
                    _Command.CommandTimeout = value;
                }
                _CommandTimeout = value;
            }
        }

        /// <summary>
        /// Obtem ou atribui <see cref="DbCommand.CommandText"/>
        /// </summary>
        public string CommandText
        {
            get 
            {
                ChecarDisposed();
                if (_Command != null)
                {
                    return _Command.CommandText;
                }
                else
                {
                    return _CommandText;
                }
            }
            set 
            {
                ChecarDisposed();
                if (_Command != null)
                {
                    _Command.CommandText = value;
                }
                _CommandText = value;
            }
        }

        public CommandType CommandType
        {
            get
            {
                ChecarDisposed();
                if (_Command != null)
                {
                    return _Command.CommandType;
                }
                else
                {
                    return _CommandType;
                }
            }
            set
            {
                ChecarDisposed();
                if (_Command != null)
                {
                    _Command.CommandType = value;
                }
                _CommandType = value;
            }
        }

        public Dictionary<System.String, DbParameter> ParametrosOutputReturnValue
        {
            get 
            {
                ChecarDisposed();
                return _ParametrosOutputReturnValue;
            }
            set 
            {
                ChecarDisposed();
                _ParametrosOutputReturnValue = value;
            }
        }

        public DbCommand ObterCommand(ContextoAcessoDado contextoAcessoDado)
        {
            ChecarDisposed();
            if (_Command == null)
            {
                _Command = BdUtil.CriarCommandParaContexto(contextoAcessoDado, _CommandText, _CommandType, _CommandTimeout);
            }
            return _Command;
        }

        public CommandWrapper(string commandText, CommandType commandType)
        {
            _CommandText = commandText;
            _CommandType = CommandType;
            _ParametrosOutputReturnValue = new Dictionary<System.String, DbParameter>();
        }

        protected override string MensagemErroObjectDisposedException
        {
            get { return "CommandWrapper"; }
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
                if (_Command != null)
                    _Command.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
