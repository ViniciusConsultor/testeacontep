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
using System.Threading;
using Acontep.Manutencao.Seguranca;
using Acontep.UI.Web;
using System.IO;
using Acontep.Diagnostico;
using Acontep.Manutencao;
using Acontep.AD.Manutencao.Seguranca;

public partial class Site : System.Web.UI.MasterPage
{
//    private const string MASTERPAGE_UTIL_JS = "/MasterPages/MasterPage.js";
//    private const string MASTERPAGE_UTIL_JS_KEY = "MasterPageScriptUtil";


    protected void Page_Init(object sender, EventArgs e)
    {
        //lblPlanta.Text = "";
        //lblSistema.Text = "";
        //if (Permissao.IsAuthenticated)
        //{
        //    lvwUsuario.DataBind();            
        //}                  
    }

    //public void Page_Error(object sender, EventArgs e)
    //{
    //    Exception objError = Server.GetLastError().GetBaseException();
    //    string strError = "<b>Error Has Been Caught in Page_Error event</b><hr><br>" +
    //                "<br><b>Error in: </b>" + Request.Url.ToString() +
    //                "<br><b>Error Message: </b>" + objError.Message.ToString() +
    //                "<br><b>Stack Trace:</b><br>" +
    //                      objError.StackTrace.ToString();
    //    //Response.Write(strError.ToString());
    //    Server.ClearError();
    //}
    
    protected string UrlTema
    {
        get
        {
            return Page.ResolveUrl("~/App_Themes/GrupoConstancioVieira/Imagens");
        }
    }

    protected string WidthFuncao
    {
        get
        {
            switch (FormUtil.ConfigTamanhoForm)
            {
                case ConfigTamanhoForm.Dinamico:
                    return "0";
                case ConfigTamanhoForm.Grande:
                    return "1024"; // width para resolucao 1024x760
                default:
                    return "800"; // width para resolucao 800x600
            }
        }
    }

    private void ExibirMensagem()
    {
        Acontep.UI.Web.MensagemWeb[] menssagens = Acontep.UI.Web.DadosWeb.ObterMensagem();
        lblMsg.Text = string.Empty;
        string avisos = string.Empty;
        string erros = string.Empty;
        foreach (MensagemWeb msg in menssagens)
        {
            switch (msg.TipoMsg)
            {
                case MensagemWeb.Tipo.Sucesso:
                    lblMsg.Text = msg.Mensagem;
                    break;
                case MensagemWeb.Tipo.Aviso:
                    avisos += msg.Mensagem + "<br>";
                    break;
                case MensagemWeb.Tipo.Erro:
                    erros += msg.Mensagem + "<br>";
                    break;
            }
        }
        lblAviso.Text = avisos;
        lblErro.Text = erros;
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ExibirMensagem();
        Page.Title = "Acontep:"  + NomeFuncao;
        Log.InfoDebugUI("Ambiente de teste");
        ////if(!((Page)HttpContext.Current.CurrentHandler).ClientScript.IsClientScriptIncludeRegistered(UTIL_JS_KEY))
        ////    ((Page)HttpContext.Current.CurrentHandler).ClientScript.RegisterClientScriptInclude(UTIL_JS_KEY,
        ////        ((Page)HttpContext.Current.CurrentHandler).Request.ApplicationPath + UTIL_JS);
        ////if (!((Page)HttpContext.Current.CurrentHandler).ClientScript.IsClientScriptIncludeRegistered(MASTERPAGE_UTIL_JS_KEY))
        ////    ((Page)HttpContext.Current.CurrentHandler).ClientScript.RegisterClientScriptInclude(MASTERPAGE_UTIL_JS_KEY,
        ////        ((Page)HttpContext.Current.CurrentHandler).Request.ApplicationPath + MASTERPAGE_UTIL_JS);
        //((Page)HttpContext.Current.CurrentHandler).ClientScript.RegisterStartupScript(GetType(),_CONTROLE_MENU_KEY,"var widthFuncao = " + WidthFuncao +  ";controleMenu();",true);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Permissao.IsAuthenticated)
        {
            lvwUsuario.DataBind();
            if (!string.IsNullOrEmpty(AuthenticationTicket.Atual.NomeSistema))
            {

                lbnSistema.Text = AuthenticationTicket.Atual.NomeSistema;
                //lbnModulo.Text = AuthenticationTicket.Atual.Mod_Descri + " - ";
                //lbnFuncao.Text = AuthenticationTicket.Atual.Fun_Descri + " - ";
            }
            else
            {                
                lbnSistema.Text = string.Empty;
                //lbnModulo.Text = string.Empty;
                //lbnFuncao.Text = string.Empty;
            }
        }
        else
        {
            lbnSistema.Text = string.Empty;
        }

        // Verifica se eh page antes do cast para obtermos a pagina que esta usando esta master page
        if (HttpContext.Current.Handler is Page)
        {
            // Se foi postback atualiza estado do menu
            

            // Se for primeiro post da pagina obtem o ultimo estado do menu
            
        }
    }

    
    protected void ibnEntrar_Click(object sender, ImageClickEventArgs e)
    {
        HttpContext.Current.Response.Redirect("~/Login.aspx");
    }
    protected void ibnSair_Click(object sender, ImageClickEventArgs e)
    {
        Sair();
    }

    private static void Sair()
    {
        Permissao.Sair();
        HttpContext.Current.Response.Redirect("~/");
        HttpContext.Current.Session.Abandon();
    }    
    
    protected void lssSair_LoggingOut(object sender, LoginCancelEventArgs e)
    {
        Sair();
    }
    public static string NomeUsuario
    {
        get { return AuthenticationTicket.Atual != null ? AuthenticationTicket.Atual.Nome : ""; }
    }
    public static string NomeFuncao
    {
        get { return AuthenticationTicket.Atual != null ? AuthenticationTicket.Atual.NomeFuncao : ""; }
    }
    protected void ibnWorkFlow_Click(object sender, ImageClickEventArgs e)
    {
    }
    protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
    {
        wpmWebPart.DisplayMode = wpmWebPart.DisplayModes[e.Item.Text.Trim()];

    }
   
    
}
