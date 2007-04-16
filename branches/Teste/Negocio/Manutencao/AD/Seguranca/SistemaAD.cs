using System;
using System.Collections.Generic;
using System.Text;
using Acontep.Dado;
using System.Data;

namespace Acontep.AD.Manutencao.Seguranca
{
    public static partial class SistemaAD
    {
        /// <summary>
        /// Retorna Sis_Descri, Sis_LogAcesso 
        /// </summary>
        /// <param name="Sis_CodSis"></param>
        /// <returns></returns>
        public static DataTable ObterSistema(string Codigo)
        {
            string sql = @"Select * FROM Permissao.Sistema
                           WHERE (Codigo = @Codigo)";
            BdUtil bdUtil = new BdUtil(sql);
            bdUtil.AdicionarParametro("@Codigo", DbType.AnsiString, Codigo);
            return bdUtil.ObterDataTable();
            //DataSet retorno = bdUtil.ObterDataSet();
            //Sis_Descri = retorno.Tables[0].Rows[0]["Sis_Descri"].ToString();
            //Sis_LogAcesso = (byte)retorno.Tables[0].Rows[0]["Sis_LogAcesso"];
            //return bdUtil.ExecuteScalar<string>();
        }
    }
}
