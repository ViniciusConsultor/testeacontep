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
    M.IDCidade as IDCliente,
    NomeCompleto + ' (' + E.Sigla + ')' as Nome
FROM 
    Cadastros.Municipio M
    JOIN Contratos.Prefeito P on M.IDPrefeito = P.IDPrefeito
    JOIN Cadastros.Estado E on M.IDEstado = E.IDEstado    
";
            return new BdUtil(sql).ObterDataTable();
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public static DataTable PesquisarPlanoTrabalho(string tipoPesquisa, string valor, DateTime Data)
        {
            string sql = @"
SELECT 
    *
FROM 
    Contratos.PlanoTrabalho Pt
    JOIN Contratos.Emenda E on E.IDEmenda = Pt.IDEmenda
WHERE   
    {0} = @{0}
";
            string Campo = string.Empty;
            DbType tipo = DbType.AnsiString;
            object ValorParaPesquisa = null;
            switch (tipoPesquisa.ToUpper())
            {
                case "CP":
                    Campo = "CodigoPlanoTrabalho";
                    tipo = DbType.AnsiString;
                    ValorParaPesquisa = valor;
                    break;
                case "CE":
                    Campo = "Numero";
                    tipo = DbType.AnsiString;
                    ValorParaPesquisa = valor;
                    break;
                case "DC":
                    Campo = "DataContratacao";
                    tipo = DbType.Date;
                    ValorParaPesquisa = Data;
                    break;
                case "DV":
                    Campo = "DataVigencia";
                    tipo = DbType.Date;
                    ValorParaPesquisa = Data;
                    break;
                case "DE":
                    Campo = "DataEmpenho";
                    ValorParaPesquisa = Data;
                    tipo = DbType.Date;
                    break;
            }
            BdUtil bd = new BdUtil(string.Format(sql, Campo));
            bd.AdicionarParametro("@" + Campo, tipo, ValorParaPesquisa);
            return bd.ObterDataTable();
        }

        

    }
}