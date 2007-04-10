using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Acontep.Dado;

namespace Acontep.Fn.Contratos
{
    /// <summary>
    /// Summary description for PlanoTrabalhoFn
    /// </summary>
    [System.ComponentModel.DataObject(true)]
    public static class PlanoTrabalhoFn
    {
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public static DataTable ObterClientes()
        {
            string sql = @"
SELECT 
    IDCidade as IDCliente,
    NomeCompleto + ' (' + E.Sigla + ')' as Nome
FROM 
    Cadastros.Municipio M
    JOIN Contratos.Prefeito P on M.IDPrefeito = P.IDPrefeito
    JOIN Cadastros.Estado E on M.IDEstado = E.IDEstado    
";
            return new BdUtil(sql).ObterDataTable();
        }
    }
}