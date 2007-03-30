using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Acontep.Dado;

namespace Acontep.AD.Cadastros
{
    public static partial class MunicipioAD
    {
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public static DataTable ObterPor_NomeESiglaEstado(System.String Nome)
        {
            string sql = @"
SELECT 
	IDCidade, 
	E.IDEstado, 
	Sigla + ' - '+ M.Nome as Nome,
    E.Nome as Estado,
	M.DATINC, 
	M.DATHOR, 
	M.USUINC, 
	M.USUTRA 
FROM 
	Cadastros.Municipio M
    JOIN Cadastros.Estado E on E.IDEstado = M.IDEstado
 WHERE 
	( M.Nome LIKE @Nome + '%' )
ORDER BY Sigla, M.Nome";
            BdUtil bd = new BdUtil(sql);
            bd.AdicionarParametro("@Nome", System.Data.DbType.AnsiString, 50, Nome);
            return bd.ObterDataTable();
        }

    }
}
