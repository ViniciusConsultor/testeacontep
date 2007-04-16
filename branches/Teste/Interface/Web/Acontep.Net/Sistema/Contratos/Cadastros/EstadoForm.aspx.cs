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
using Acontep.Manutencao.Seguranca;
using Acontep.UI.Web;

public partial class Sistema_Contratos_Cadastros_EstadoForm : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Permissao.ConfigAcessoFuncao("15");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
      
    }
    protected void btnIncluir_Click(object sender, EventArgs e)
    {
        fvwItem.ChangeMode(FormViewMode.Insert);
    }
    protected void btnAlterar_Click(object sender, EventArgs e)
    {
        if ( ddlEstado.SelectedIndex > -1 )
            fvwItem.ChangeMode(FormViewMode.Edit);
    }
    protected void btnExcluir_Click(object sender, EventArgs e)
    {
        if (ddlEstado.SelectedIndex > -1)
        {
            odsManterItem.DeleteParameters[0].DefaultValue = ddlEstado.SelectedValue;
            odsManterItem.Delete();
        }
    }
    protected void btnPesquisar_Click(object sender, EventArgs e)
    {
        ddlEstado.DataBind();
    }

    protected void odsManterItem_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception == null)
        {
            DadosWeb.AtribuirMensagem(MensagemWeb.MensagemAlteradoSucesso);
            btnPesquisar_Click(sender, (EventArgs)e);
        }
    }
    protected void odsManterItem_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception == null)
        {
            DadosWeb.AtribuirMensagem(MensagemWeb.MensagemInclusaoSucesso);
            btnPesquisar_Click(sender, (EventArgs)e);
        }
    }
    protected void odsManterItem_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception == null)
        {
            DadosWeb.AtribuirMensagem(MensagemWeb.MensagemExclusaoSucesso);
            btnPesquisar_Click(sender, (EventArgs)e);
        }
    }
    
}
