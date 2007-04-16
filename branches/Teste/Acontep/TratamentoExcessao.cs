using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using Acontep.Dado;
using Acontep.Diagnostico;

namespace Acontep
{

    /// <summary>
    /// Tipos de excessões
    /// </summary>
    public enum TipoErroContraint : int
    {
        /// <summary>
        /// Violação de FK de insert.
        /// </summary>
        FK_Insert,
        /// <summary>
        /// Violação de Fk de delete.
        /// </summary>
        FK_Delete,
        /// <summary>
        /// Violação de primary key.
        /// </summary>
        PrimaryKey,
        /// <summary>
        /// Violação de Unique Key.
        /// </summary>
        UniqueKey,
        /// <summary>
        /// Violação de constraint. Ainda não está implementado.
        /// </summary>
        Constraint,
        /// <summary>
        /// Sem identificação de violação
        /// </summary>
        NA

    }
    /// <summary>
    /// Summary description for Excecoes
    /// </summary>
    public static class TratamentoExcessao
    {

        // 2627 - "Violation of PRIMARY KEY constraint 'PK_Funcionario'. Cannot insert duplicate key in object 'dbo.Funcionario'.\r\nThe statement has been terminated."
        // Numero e texto para exeção de violação de chave primária
        private const int PRIMARY_KEY_NUMBER = 2627;
        private const string PRIMARY_KEY_MESSAGE = "Violation of PRIMARY KEY constraint";

        // 2601 - "Cannot insert duplicate key row in object 'dbo.Funcionario' with unique index 'IX_Funcionario_Nome'.\r\nThe statement has been terminated."
        // Numero e texto para exeção de violação de chave única
        private const int UNIQUE_KEY_NUMBER = 2601;
        private const string UNIQUE_KEY_MESSAGE = "with unique index";

        // 547 - "The INSERT statement conflicted with the FOREIGN KEY constraint \"FK_Dependente_Funcionario\". The conflict occurred in database \"Teste\", table \"dbo.Funcionario\", dataColumn 'Codigo'.\r\nThe statement has been terminated."
        // Numero e texto para exeção de violação de chave estrangeira - insert
        private const int FOREIGN_KEY_INSERT_NUMBER = 547;
        private const string FOREIGN_KEY_INSERT_MESSAGE = "The INSERT statement conflicted with the FOREIGN KEY constraint";

        // 547 - "The DELETE statement conflicted with the REFERENCE constraint \"FK_Dependente_Funcionario\". The conflict occurred in database \"Teste\", table \"dbo.Dependente\", dataColumn 'CodigoFuncionario'.\r\nThe statement has been terminated."
        // Numero e texto para exeção de violação de chave estrangeira - delete
        private const int FOREIGN_KEY_DELETE_NUMBER = 547;
        private const string FOREIGN_KEY_DELETE_MESSAGE = "The DELETE statement conflicted with the REFERENCE constraint";

        /// <summary>
        /// Método para tratar a exceção
        /// </summary>
        /// <param name="excecao">Exceção</param>
        /// <param name="mensagem"></param>
        /// <returns>Indicativo se a exceção foi tratada com sucesso ou não</returns>
        public static bool Tratar(Exception excecao, out string mensagem)
        {
            mensagem = "";
            try
            {

                // Caso a excessão não seja SQLExcpetion, mas possua uma InnerException do tipo SqlException
                if (excecao is SqlException || excecao.InnerException is SqlException)
                {
                    SqlException sqlException = excecao is SqlException ? (SqlException)excecao : (SqlException)excecao.InnerException;
                    return TratarSqlException(sqlException, out mensagem);
                }
                else if (excecao is AcontepException)
                {
                    mensagem = excecao.Message;
                    return true;
                }
                else
                {
                    Log.ErroGeral("Acontep.TratamentoExcessao", excecao);
                    return false;
                }
            }
            catch
            {
                Log.ErroGeral("Acontep.TratamentoExcessao", excecao);
                return false;
            }
        }

        /// <summary>
        /// Tratamento de exceções SqlException
        /// </summary>
        /// <param name="excecao"></param>
        /// <param name="mensagem"></param>
        /// <returns></returns>
        public static bool TratarSqlException(SqlException excecao, out string mensagem)
        {
            mensagem = string.Empty;
            try
            {
                // Obtendo o nome da constraint
                string constraint = ObterNomeConstraint(excecao.Message, excecao.Number);
                // Obtendo a mensagem para a constraint
                mensagem = ObterMensagemConstrant(constraint, ObterTipoConstraint(excecao.Message, excecao.Number));
                if (string.IsNullOrEmpty(mensagem))
                {
                    mensagem = ObterMensagemConstrantPadrao(excecao);
                }
                if (!string.IsNullOrEmpty(mensagem))
                {
                    return true;
                }
                Log.ErroGeral("Acontep.TratamentoExcessao", excecao);
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Obtem (gera) a mensagem padrão para a exceção
        /// </summary>
        /// <param name="excecao">Exceção</param>
        /// <returns>Mensagem padrão para a exceção</returns>
        public static string ObterMensagemConstrantPadrao(SqlException excecao)
        {
            string mensagem = null;
            if (excecao.Number == 2627)
            {
                mensagem = ObterMensagemConstrant("PRIMARY_KEY", TipoErroContraint.PrimaryKey);
            }
            else if (excecao.Number == 2601)
            {
                mensagem = ObterMensagemConstrant("UNIQUE_KEY", TipoErroContraint.UniqueKey);
            }
            else if (excecao.Number == 547)
            {
                mensagem = ObterMensagemConstrant("FOREIGN_KEY", TipoErroContraint.NA);
            }
            return mensagem;
        }

        /// <summary>
        /// Recupera a mensagem armazenada no banco de dados para a constraint fornecida
        /// Tabela: ConstrInfo
        /// </summary>
        /// <param name="constraint">Nome da constraint</param>
        /// <returns>Mensagem da constraint</returns>
        /// <param name="tipo">Tipo da constraint</param>
        public static string ObterMensagemConstrant(string constraint, TipoErroContraint tipo)
        {
            string sql = @"SELECT Ctr_Descri
                       FROM ConstrInfo
                       WHERE Ctr_Nome = @Ctr_Nome AND
                        ( Ctr_Tipo = @Ctr_Tipo OR Ctr_Tipo is null )";
            BdUtil bd = new BdUtil(sql);
            bd.AdicionarParametro("@Ctr_Nome", DbType.String, constraint);
            bd.AdicionarParametro("@Ctr_Tipo", DbType.String, tipo.ToString());
            DataTable dt = bd.ObterDataTable();
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["Ctr_Descri"].ToString();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Obtem o nome da constraint contido na mensagem da exceção
        /// </summary>
        /// <param name="message">Mensagem da exceção</param>
        /// <param name="number">número da exceção retornado pelo Sql Server</param>
        /// <returns></returns>
        private static string ObterNomeConstraint(string message, int number)
        {
            string constraint = null;
            if (number == PRIMARY_KEY_NUMBER)
            {
                constraint = ExtrairNomeConstraint(PRIMARY_KEY_MESSAGE, message, '\'');
                constraint = constraint.Replace("\'", "").Trim();
            }
            else if (number == UNIQUE_KEY_NUMBER)
            {
                constraint = ExtrairNomeConstraint(UNIQUE_KEY_MESSAGE, message, '\'');
                constraint = constraint.Replace("\'", "").Trim();
            }
            else if (number == FOREIGN_KEY_INSERT_NUMBER && message.Contains("INSERT"))
            {
                constraint = ExtrairNomeConstraint(FOREIGN_KEY_INSERT_MESSAGE, message, '\"');
                constraint = constraint.Replace("\"", "").Trim();
            }
            else if (number == FOREIGN_KEY_DELETE_NUMBER && message.Contains("DELETE"))
            {
                constraint = ExtrairNomeConstraint(FOREIGN_KEY_DELETE_MESSAGE, message, '\"');
                constraint = constraint.Replace("\"", "").Trim();
            }
            return constraint;
        }

        private static TipoErroContraint ObterTipoConstraint(string message, int number)
        {
            if (message.Contains(PRIMARY_KEY_MESSAGE))
            {
                return TipoErroContraint.PrimaryKey;
            }
            else if (message.Contains(UNIQUE_KEY_MESSAGE) || message.Contains("Violation of UNIQUE KEY constraint"))
            {
                return TipoErroContraint.UniqueKey;
            }
            else if (number == FOREIGN_KEY_INSERT_NUMBER && message.Contains("INSERT"))
            {
                return TipoErroContraint.FK_Insert;
            }
            else if (number == FOREIGN_KEY_DELETE_NUMBER && message.Contains("DELETE"))
            {
                return TipoErroContraint.FK_Delete;
            }
            else
                return TipoErroContraint.NA;
        }

        /// <summary>
        /// Extrai o nome da constraint
        /// </summary>
        /// <param name="inicioMensagem">parte inicial (fixa) da mensagem</param>
        /// <param name="message">mensagem da exceção</param>
        /// <param name="delimitador">delimitidor do nome da constraint na mensagem de exceção</param>
        /// <returns></returns>
        private static string ExtrairNomeConstraint(string inicioMensagem, string message, char delimitador)
        {
            int inicioConstraint = message.IndexOf(inicioMensagem) + inicioMensagem.Length;
            int finalConstraint = message.IndexOf('.', inicioConstraint);
            return message.Substring(inicioConstraint, finalConstraint - inicioConstraint);
        }
    }
}