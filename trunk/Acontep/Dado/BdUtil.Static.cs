using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;

namespace Acontep.Dado
{
    /// <summary>
    /// Operacoes de classe de BdUtil
    /// </summary>
    public partial class BdUtil
    {
        /// <summary>
        /// Verifica se a direcao do parametro corresponde a um valor de saida/retorno
        /// </summary>
        /// <param name="direcao"></param>
        /// <returns></returns>
        public static bool EhParametroSaidaRetornoValor(ParameterDirection direcao)
        {
            switch (direcao)
            {
                case ParameterDirection.InputOutput:
                case ParameterDirection.Output:
                case ParameterDirection.ReturnValue:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Abertura de conexao, se fechada, e retorno o estado original da conexao para controle posterior
        /// se fecha ou nao ao conexao, ao fim da operacao
        /// </summary>
        /// <param name="connection">Conexao que vai estar aberta ao fim do metodo</param>
        /// <param name="estadoOriginal">Obtem o estado original da conexao, para controle se deve ou nao fechar a conexao</param>
        internal static void AbrirConexao(DbConnection connection, out ConnectionState estadoOriginal)
        {
            estadoOriginal = connection.State;
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        /// <summary>
        /// Fecha conexao, se estava fechada quando foi aberta no inicio do comando
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="estadoOriginal"></param>
        internal static void FecharConexao(DbConnection connection, ConnectionState estadoOriginal)
        {
            if ((connection != null) && (estadoOriginal == ConnectionState.Closed))
            {
                connection.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contextoAcessoDado"></param>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static DbCommand CriarCommandParaContexto(ContextoAcessoDado contextoAcessoDado, string commandText, CommandType commandType, int commandTimeout)
        {
            if (contextoAcessoDado == null)
                throw new NullReferenceException("Nao existe um escopo para a criacao do DbCommand");
            DbCommand command = contextoAcessoDado.ProviderFactory.CreateCommand();
            command.Connection = contextoAcessoDado.Connection;
            command.CommandText = commandText;
            command.CommandType = commandType;
            command.CommandTimeout = commandTimeout;
            if (contextoAcessoDado.Transaction != null)
            {
                command.Transaction = contextoAcessoDado.Transaction;
            }
            return command;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contextoAcessoDado"></param>
        /// <param name="comm"></param>
        /// <returns></returns>
        public static DbDataAdapter CriarDataAdapterParaContexto(ContextoAcessoDado contextoAcessoDado, DbCommand comm)
        {
            if (contextoAcessoDado == null)
                throw new NullReferenceException("Nao existe uma escopo para a criacao do DbDataAdapter");
            DbDataAdapter dataAdapter;
            dataAdapter = contextoAcessoDado.ProviderFactory.CreateDataAdapter();
            dataAdapter.SelectCommand = comm;

            return dataAdapter;
        }

        /// <summary>
        /// Extrair <see cref="DbCommand"/> de uma lista de <see cref="CommandWrapper"/>s através do <see cref="TipoCommandDataAdapter"/>
        /// </summary>
        /// <param name="contextoAcessoDado"></param>
        /// <param name="tipoCommandDataAdapter"></param>
        /// <param name="commandWrappersDataAdapter"></param>
        /// <returns></returns>
        internal static DbCommand ObterCommand(ContextoAcessoDado contextoAcessoDado, TipoCommandDataAdapter tipoCommandDataAdapter, Dictionary<TipoCommandDataAdapter, CommandWrapper> commandWrappersDataAdapter)
        {
            if (commandWrappersDataAdapter.ContainsKey(tipoCommandDataAdapter))
            {
                CommandWrapper commandWrapper = commandWrappersDataAdapter[tipoCommandDataAdapter];
                return commandWrapper.ObterCommand(contextoAcessoDado);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="contextoAcessoDado"></param>
        /// <param name="commandWrappers"></param>
        /// <returns></returns>
        internal static DbDataAdapter CriarDataAdapterDeCommandWrappers(ContextoAcessoDado contextoAcessoDado, Dictionary<TipoCommandDataAdapter, CommandWrapper> commandWrappers)
        {
            if (contextoAcessoDado == null)
                throw new NullReferenceException("Nao existe uma escopo para a criacao do DbDataAdapter");
            DbCommand selectCommand = ObterCommand(contextoAcessoDado, TipoCommandDataAdapter.Select, commandWrappers);
            if (selectCommand == null)
                throw new NullReferenceException("A lista de CommandWrappers deve conter no minimo um command de select para que um DbDataAdapter seja criado");
            DbDataAdapter dataAdapter;
            dataAdapter = contextoAcessoDado.ProviderFactory.CreateDataAdapter();
            dataAdapter.SelectCommand = selectCommand;
            dataAdapter.InsertCommand = ObterCommand(contextoAcessoDado, TipoCommandDataAdapter.Insert, commandWrappers);
            dataAdapter.UpdateCommand = ObterCommand(contextoAcessoDado, TipoCommandDataAdapter.Update, commandWrappers);
            dataAdapter.DeleteCommand = ObterCommand(contextoAcessoDado, TipoCommandDataAdapter.Delete, commandWrappers);

            return dataAdapter;
        }
    }
}
