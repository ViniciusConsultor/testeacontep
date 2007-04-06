using System;
using System.Collections.Generic;
using System.Text;
using Acontep.Dado;
using System.Data;

namespace Acontep.AD.Contratos
{
    public static partial class EmpreiteiraAD
    {

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public static DataTable ObterEmpreiteiraPorID(int IDEmpreiteira)
        {
               string sql = @"
SELECT 
	E.*,
    M.IDEstado
FROM 
	Contratos.Empreiteira E
    LEFT join Cadastros.Municipio M on M.IDCidade = E.IDCidade
WHERE 
    IDEmpreiteira = @IDEmpreiteira
";
  
            BdUtil bd = new BdUtil(sql);
            bd.AdicionarParametro("@IDEmpreiteira", System.Data.DbType.Int32, IDEmpreiteira);
            return bd.ObterDataTable();
        }
    
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public static DataTable ObterEmpreiteira(string Filtro, string ValorFiltro)
        {
            string sql = @"
SELECT 
	*
FROM 
	Contratos.Empreiteira
 WHERE 
    {0} LIKE '%'+ @{0}
";
  
            BdUtil bd = new BdUtil(string.Format(sql, Filtro));
            bd.AdicionarParametro("@" + Filtro, System.Data.DbType.AnsiString, ValorFiltro);
            return bd.ObterDataTable();
        }
    }
}
