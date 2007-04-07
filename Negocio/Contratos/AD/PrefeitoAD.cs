using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Acontep.Dado;

namespace Acontep.AD.Contratos
{
    public static partial class PrefeitoAD
    {

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public static DataTable ObterPorID(System.Int32 IDPrefeito)
        {
            string sql = @"
SELECT 
	P.*,
    M.IDEstado
FROM 
	Contratos.Prefeito P
	LEFT JOIN Cadastros.Municipio M on M.IDCidade = P.IDCidade  
 WHERE 
	P.IDPrefeito = @IDPrefeito";
            BdUtil bd = new BdUtil(sql);
            bd.AdicionarParametro("@IDPrefeito", System.Data.DbType.Int32, -1, IDPrefeito);
            return bd.ObterDataTable();
        }

    }
}
