using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Principal;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    
    protected void Page_PreRender(object sender, EventArgs e)
    {
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// Determina se a url digitada pode ser acessada diretamente sem passar por algum passo previo.
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    private bool IsRootUrl(string url)
    {
        // Simula um acesso a uma funcao que nao esta marcada para aparecer no menu.
        // Lembrar de considerar casos onde query vai ser usado para passar informacoes como parametro no codigo final
        return !url.EndsWith("CotacaoCadForm.aspx");
    }

    private void RedirectToUrl()
    {
        string url = FormsAuthentication.GetRedirectUrl(String.Empty, false);
        if (!IsRootUrl(url))
            url = FormsAuthentication.DefaultUrl;
        Response.Redirect(url);
        //TODO: Entender porque o status do controle de login status nao atualiza se usarmos Server.Redirect
        //Server.Transfer(url);        
    }

    protected void Login1_LoggedIn(object sender, EventArgs e)
    {
        RedirectToUrl();
    }
}
