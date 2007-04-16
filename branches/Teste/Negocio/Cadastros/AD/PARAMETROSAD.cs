using System;
using System.Collections.Generic;
using System.Text;
using Acontep.Dado;
using System.Data;

namespace Acontep.AD.Cadastros
{
    public static partial class ParametrosAD 
    {
        public static string ObterValorParametro(string par_NomPar)
        {
            string sql = "select Par_ValPar From parametros where Par_NomPar = @Par_NomPar";
            BdUtil bd = new BdUtil(sql);
            bd.AdicionarParametro("@Par_NomPar", DbType.String, par_NomPar);
            return bd.ExecuteScalar<string>();
        }


        public static string ObterValorParametro(System.Byte Plt_Planta, System.Int16? Sis_CodSis, System.String Par_NomPar)
        {
            string sql = @"
SELECT 	
	Par_ValPar
FROM 
	dbo.PARAMETROS
	  
 WHERE 
	( Plt_Planta LIKE @Plt_Planta ) AND ( Sis_CodSis LIKE @Sis_CodSis ) AND ( Par_NomPar LIKE @Par_NomPar )";
            BdUtil bd = new BdUtil(sql);
            bd.AdicionarParametro("@Plt_Planta", System.Data.DbType.Byte, -1, Plt_Planta);
            bd.AdicionarParametro("@Sis_CodSis", System.Data.DbType.Int16, -1, Sis_CodSis);
            bd.AdicionarParametro("@Par_NomPar", System.Data.DbType.AnsiString, 20, Par_NomPar);
            DataTable dte = bd.ObterDataTable();
            if(dte.Rows.Count == 0)
                return null;
            return dte.Rows[0]["Par_ValPar"].ToString();
        }



        //public static int Compara_VlrMaxAdiantado(decimal valor)
        //{
        //    DataTable dtt = ObterPor_PK_PARAMETROS(99, ((string)"VlrMaxAdiant").PadRight(20));
        //    int resultado = -1;
        //    if (Conversao.Para<decimal>(dtt.Rows[0]["Par_ValPar"], "Par_ValPar") > valor)
        //    {
        //        resultado = -1;
        //    }
        //    else
        //    {
        //        if (Conversao.Para<decimal>(dtt.Rows[0]["Par_ValPar"], "Par_ValPar") < valor)
        //        {
        //            resultado = 1;
        //        }
        //        else
        //        {
        //            resultado = 0;
        //        }
        //    }
        //    return resultado;
        //}

    }
}
