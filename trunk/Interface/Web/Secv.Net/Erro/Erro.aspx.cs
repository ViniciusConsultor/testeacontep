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
using Acontep.UI.Web;

public partial class Erro_Erro : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Obtendo a mensagem de erro da sess�o, a qual foi colocada pelo m�todo 
            // Application_Error do Global.asax
            //string mensagem = Acontep.UI.Web.DadosWeb.ObterMensagem().Mensagem;
            lblMensagem.Text = string.Empty;
            MensagemWeb[] menssagens = Acontep.UI.Web.DadosWeb.ObterMensagem();
            foreach (MensagemWeb msg in menssagens)
            {
                if (msg.TipoMsg == MensagemWeb.Tipo.Erro && !(string.IsNullOrEmpty(msg.Mensagem)))
                {
                    lblMensagem.Text += msg.Mensagem + "<br>";
                }

            }
            if (string.IsNullOrEmpty(lblMensagem.Text))
            {
                // Caso a mensagem n�o seja encontrada na sess�o.
                // O usu�rio ser� levado para a p�gina de erro padr�o.
                throw new Exception("Mensagem de erro n�o encontrada na sess�o");
            }
        }
    }
}
