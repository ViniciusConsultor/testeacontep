using System;
using System.Data;
using System.Collections.Generic;

namespace Acontep.Validacoes
{
    /// <summary>
    /// Utilitario de geração de <see cref="Validacao"/> a partir de estrturas de dados
    /// </summary>
    public static class ValidacaoUtil
    {
        /// <summary>
        /// Gera <see cref="Validacao"/> a partir de um <see cref="DataColumn"/>
        /// </summary>
        /// <remarks>
        /// Uma instancia de <see cref="Validacao"/> é criada a partir da definição do <see cref="DataColumn"/>
        /// usando <see cref="DataColumn.DataType"/>, <see cref="DataColumn.AllowDBNull"/> e, se <see cref="DataColumn.DataType"/>
        /// for do tipo <see cref="System.String"/>, <see cref="DataColumn.MaxLength"/>.
        /// O nome do campo a ser usado nas mensagens de validação é obtido a partir de <see cref="DataColumn.Caption"/>.
        /// </remarks>
        /// <param name="dataColumn"><see cref="DataColumn"/> a partir do qual uma instancia de <see cref="Validacao"/> vai ser gerada</param>
        public static Validacao DeDataColumn(DataColumn dataColumn)
        {
            return DeDataColumn(dataColumn, dataColumn.Caption.ToLower());
        }

        /// <summary>
        /// Gera <see cref="Validacao"/> a partir de um <see cref="DataColumn"/>
        /// </summary>
        /// <remarks>
        /// Uma instancia de <see cref="Validacao"/> é criada a partir da definição do <see cref="DataColumn"/>
        /// usando <see cref="DataColumn.DataType"/>, <see cref="DataColumn.AllowDBNull"/> e, se <see cref="DataColumn.DataType"/>
        /// for do tipo <see cref="System.String"/>, <see cref="DataColumn.MaxLength"/>.
        /// O nome do campo usado nas mensagens de validação é <paramref name="rotuloMensagemErro"/>.
        /// </remarks>
        /// <param name="dataColumn"><see cref="DataColumn"/> a partir do qual uma instancia de <see cref="Validacao"/> vai ser gerada</param>
        /// <param name="rotuloMensagemErro">Nome usado para identificar o campo nas mensagens de erro</param>
        public static Validacao DeDataColumn(DataColumn dataColumn, string rotuloMensagemErro)
        {
            Validacao validacao = new Validacao(rotuloMensagemErro, dataColumn.DataType, !dataColumn.AllowDBNull);
            if (dataColumn.DataType == typeof(String) && dataColumn.MaxLength > 0)
                validacao.LarguraMaxima = dataColumn.MaxLength;
            return validacao;
        }

        /// <summary>
        /// Retorna um instancia de <see cref="Validacao"/> para cada <see cref="DataColumn"/> em <paramref name="dataTable"/> 
        /// </summary>
        /// <seealso cref="DeDataColumn(DataColumn)"/>
        /// <param name="dataTable"><see cref="DataTable"/> de onde vai se instanciar um <see cref="Validacao"/> para cada <see cref="DataColumn"/></param>
        public static Dictionary<DataColumn, Validacao> DeDataTable(DataTable dataTable)
        {
            Dictionary<DataColumn, Validacao> listaValidacao = new Dictionary<DataColumn, Validacao>();
            foreach (DataColumn dataColumn in dataTable.Columns)
            {
                listaValidacao.Add(dataColumn, ValidacaoUtil.DeDataColumn(dataColumn));
            }
            return listaValidacao;
        }

        /// <summary>
        /// Para cada <see cref="DataTable"/> em <see cref="DataSet.Tables"/> retorna um instancia de <see cref="Validacao"/> de cada <see cref="DataColumn"/> em <paramref name="dataTable"/> 
        /// </summary>
        /// <seealso cref="DeDataTable"/>
        /// <param name="dataSet"><see cref="DataSet"/> de onde se vai gerar as instancias de <see cref="Validacao"/> para cada <see cref="DataColumn"/> em cada <see cref="DataTable"/></param>
        public static Dictionary<DataTable, Dictionary<DataColumn, Validacao>> DeDataSet(DataSet dataSet)
        {
            Dictionary<DataTable, Dictionary<DataColumn, Validacao>> listaConjuntoValidacao = new Dictionary<DataTable, Dictionary<DataColumn, Validacao>>();
            foreach (DataTable dataTable in dataSet.Tables)
                listaConjuntoValidacao.Add(dataTable, DeDataTable(dataTable));
            return listaConjuntoValidacao;
        }
    }
}