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
	E.IDEstado, 
	Sigla + ' - '+ M.Nome as NomeSigla,
    E.Nome as Estado,
	M.*
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
