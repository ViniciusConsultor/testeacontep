using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Acontep.Validacoes
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ValidacaoDataTable
    {
        private DataTable _owner;
        private Dictionary<DataColumn, Validacao> _Validacoes;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public Validacao this[DataColumn column]
        {
            get { return _Validacoes[column]; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public Validacao this[string columnName]
        {
            get { return this[_owner.Columns[columnName]]; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        public Validacao this[int columnIndex]
        {
            get { return this[_owner.Columns[columnIndex]]; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        public ValidacaoDataTable(DataTable owner)
        {
            _owner = owner;
            _Validacoes = ValidacaoUtil.DeDataTable(owner);
            _owner.ColumnChanged += new DataColumnChangeEventHandler(DataTable_ColumnChanged);
            _owner.TableNewRow += new DataTableNewRowEventHandler(DataTable_TableNewRow);
            _owner.ExtendedProperties[ValidacaoDataTable.ValidacaoExtendProperty] = this;
        }

        /// <summary>
        /// Nome da propriedade extendida do <see cref="DataTable"/> que contem
        /// <c>ValidacaoDataTable</c>
        /// </summary>
        public static String ValidacaoExtendProperty
        {
            get { return "__Validacao"; }
        }

        /// <summary>
        /// Obtem a instancia de <see cref="ValidacaoDataTable"/> associada a um <see cref="DataTable"/>
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static ValidacaoDataTable ObterValidacaoDataTable(DataTable dataTable)
        {
            object objValidacaoDataTable = dataTable.ExtendedProperties[ValidacaoExtendProperty];
            return (ValidacaoDataTable)objValidacaoDataTable;
        }

        //void DataTable_ColumnChanging(object sender, DataColumnChangeEventArgs e)
        //{
        //    if (!_Validacoes.ContainsKey(e.Column))
        //        return;
        //    ValidandoColuna(e.Row, e.Column, e.ProposedValue);
        //}

        void DataTable_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            if (!_Validacoes.ContainsKey(e.Column))
                return;
            ValidandoColuna(e.Row, e.Column, e.Row[e.Column]);
        }

        void DataTable_TableNewRow(object sender, DataTableNewRowEventArgs e)
        {
            FazerValidacao(e.Row);
        }

        /// <summary>
        /// Valida todas as colunas de todas as linhas do <see cref="DataTable"/>
        /// </summary>
        public void FazerValidacao()
        {
            foreach (DataRow dataRow in _owner.Rows)
            {
                FazerValidacao(dataRow);
            }
        }

        /// <summary>
        /// Valida todas as colunas da linha passada como parametro
        /// </summary>
        /// <param name="dataRow"></param>
        public void FazerValidacao(DataRow dataRow)
        {
            foreach (DataColumn column in dataRow.Table.Columns)
            {
                FazerValidacao(dataRow, column);
            }
        }

        /// <summary>
        /// Valida a coluna informada da linha passada como parametro
        /// </summary>
        /// <param name="dataRow"></param>
        /// <param name="dataColumn"></param>
        public void FazerValidacao(DataRow dataRow, DataColumn dataColumn)
        {
            ValidandoColuna(dataRow, dataColumn, dataRow[dataColumn]);
        }

        private void ValidandoColuna(DataRow row, DataColumn column, object proposedValue)
        {
            if (!_Validacoes.ContainsKey(column))
                return;

            Validacao validation = _Validacoes[column];
            if (validation != null)
            {
                string mensagem = validation.ObterMensagemValidacao(proposedValue);
                row.SetColumnError(column, mensagem);
            }
        }
    }
}
