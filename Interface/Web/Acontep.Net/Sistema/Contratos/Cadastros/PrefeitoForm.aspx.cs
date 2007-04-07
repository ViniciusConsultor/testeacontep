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
using Acontep;
using Acontep.Manutencao.Seguranca;
using eWorld.UI;

public partial class Sistema_Contratos_Cadastros_PrefeitoForm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Permissao.ConfigAcessoFuncao("6");
    }
    public void HabitarModo(FormViewMode modo)
    {
        pnlManutencao.Visible = modo != FormViewMode.ReadOnly;
        pnlPesquisa.Visible = modo == FormViewMode.ReadOnly;
        fvwItem.ChangeMode(modo);
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        
        HabitarModo(FormViewMode.ReadOnly);
    }
    protected void odsManutencao_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception == null)
        {
            HabitarModo(FormViewMode.ReadOnly);
            DadosWeb.AtribuirMensagem(MensagemWeb.MensagemSucesso);
            btnPesquisar_Click(sender, e);
        }
    }
    protected void btnPesquisar_Click(object sender, EventArgs e)
    {
        ddlResultado.DataBind();
    }
    protected void btnIncluir_Click(object sender, EventArgs e)
    {
        HabitarModo(FormViewMode.Insert);
    }
    protected void btnAlterar_Click(object sender, EventArgs e)
    {
        HabitarModo(FormViewMode.Edit);
    }
    protected void NenhumItemSelecionado(object sender, EventArgs e)
    {
        ((DropDownList)sender).Items.Clear();
        ((DropDownList)sender).Items.Add(new ListItem(":: Nenhum item selecionado ::", string.Empty));
    }
    protected void odsManutencao_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        e.InputParameters["CEP"] = Formatacao.RemoverFormatacaoCep(FormUtil.ObterControle<MaskedTextBox>(fvwItem.Controls, "tbxCep").Text);
    }
    protected void btnExcluir_Click(object sender, EventArgs e)
    {
        if (ddlResultado.SelectedValue != null)
        {
            odsManutencao.DeleteParameters[0].DefaultValue = ddlResultado.SelectedValue;
            odsManutencao.Delete();
            DadosWeb.AtribuirMensagem(MensagemWeb.MensagemExclusaoSucesso);
            btnPesquisar_Click(sender, e);
        }
        else
        {
            DadosWeb.AtribuirMensagem(MensagemWeb.MensagemSelecioneItem);
        }

    }

    string IDCidade = String.Empty;
    protected void odsManutencao_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.ReturnValue != null && ((DataTable)e.ReturnValue).Rows.Count > 0)
            IDCidade = ((DataTable)e.ReturnValue).Rows[0]["IDCidade"].ToString();
    }
    protected void ddlCidade_DataBound(object sender, EventArgs e)
    {
        ((DropDownList)sender).SelectedValue = IDCidade;
    }
}
