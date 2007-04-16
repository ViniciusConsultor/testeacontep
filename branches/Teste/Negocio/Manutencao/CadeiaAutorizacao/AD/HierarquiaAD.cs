using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Secv.Dado;
namespace Secv.AD
{
    public static partial class HierarquiaAD
    {

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public static DataTable Obter_Hierarquias()
        {
            string sql = @"
SELECT 	
	Hie_Id, 
	Hie_Descri, 
	Hie_Funcao
FROM 
	CadeiaAutorizacao.Hierarquia
";
            BdUtil bd = new BdUtil(sql);
            return bd.ObterDataTable();
        }
    }
}
