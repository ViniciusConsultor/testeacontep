using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Acontep.Manutencao.Seguranca;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        ConfigurarAcessoSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //this.GridView3.DataBind();
        //this.DataBind();
    }

    /// <summary>
    /// A pagina default é usada como pagina inicial dos sistemas. Esse metodo
    /// faz configuracao de acesso se for passado sis e plt na query
    /// </summary>
    private void ConfigurarAcessoSistema()
    {
        string Codigo;

        if (String.IsNullOrEmpty(Request["sis"]))
            return;

        Codigo = Request["sis"].ToString();
        if (Permissao.IsAuthenticated)
            Permissao.ConfigAcessoSistema(Codigo);
        else
            Response.Redirect("~/Login.aspx");        
    }

}
