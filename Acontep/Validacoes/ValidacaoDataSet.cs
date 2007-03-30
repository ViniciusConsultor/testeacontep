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
    public class ValidacaoDataSet
    {
        private DataSet _owner;
        private Dictionary<DataTable, ValidacaoDataTable> _ValidacoesDataTable;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public ValidacaoDataTable this[DataTable dataTable]
        {
            get { return _ValidacoesDataTable[dataTable]; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public ValidacaoDataTable this[string tableName]
        {
            get { return this[_owner.Tables[tableName]]; }
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="tableIndex"></param>
       /// <returns></returns>
        public ValidacaoDataTable this[int tableIndex]
        {
            get { return this[_owner.Tables[tableIndex]]; }
        }

        /// <summary>
        /// Nome da propriedade extendida do <see cref="DataSet"/> que contem
        /// <c>ValidacaoDataTable</c>
        /// </summary>
        public static String ValidacaoExtendProperty
        {
            get { return "__Validacao"; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        public ValidacaoDataSet(DataSet owner)
        {
            _owner = owner;
            _ValidacoesDataTable = new Dictionary<DataTable, ValidacaoDataTable>();
            foreach (DataTable dataTable in owner.Tables)
            {
                ValidacaoDataTable validacaoDataTable = new ValidacaoDataTable(dataTable);
                _ValidacoesDataTable.Add(dataTable, validacaoDataTable);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataSet"></param>
        /// <returns></returns>
        public static ValidacaoDataSet ObterValidacaoDataSet(DataSet dataSet)
        {
            return (ValidacaoDataSet)dataSet.ExtendedProperties[ValidacaoExtendProperty];
        }
    }
}
