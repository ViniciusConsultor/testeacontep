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

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ExibirMensagem();

    }
    public static string NomeUsuario
    {
        get { return AuthenticationTicket.Atual != null ? AuthenticationTicket.Atual.Nome : ""; }
    }
    public static string NomeFuncao
    {
        get { return AuthenticationTicket.Atual != null ? AuthenticationTicket.Atual.NomeFuncao : ""; }
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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Permissao.IsAuthenticated)
        {
            lvwUsuario.DataBind();
            if (!string.IsNullOrEmpty(AuthenticationTicket.Atual.NomeSistema))
            {

                //lbnSistema.Text = AuthenticationTicket.Atual.NomeSistema;
                //lbnModulo.Text = AuthenticationTicket.Atual.Mod_Descri + " - ";
                //lbnFuncao.Text = AuthenticationTicket.Atual.Fun_Descri + " - ";
            }
            else
            {
                //lbnSistema.Text = string.Empty;
                //lbnModulo.Text = string.Empty;
                //lbnFuncao.Text = string.Empty;
            }
        }
        else
        {
            //lbnSistema.Text = string.Empty;
        }

        // Verifica se eh page antes do cast para obtermos a pagina que esta usando esta master page
        if (HttpContext.Current.Handler is Page)
        {
            // Se foi postback atualiza estado do menu


            // Se for primeiro post da pagina obtem o ultimo estado do menu

        }

    }
    private void ExibirMensagem()
    {
        Acontep.UI.Web.MensagemWeb[] menssagens = Acontep.UI.Web.DadosWeb.ObterMensagem();
        lblMsgErroMaster.Text = string.Empty;

        foreach (MensagemWeb msg in menssagens)
        {

            if (!string.IsNullOrEmpty(lblMsgErroMaster.Text))
                lblMsgErroMaster.Text += "<br />";
            lblMsgErroMaster.Text += msg.Mensagem;
        }
        lblMsgErroMaster.Visible = lblMsgErroMaster.Text.Length > 0; 
    }
}
