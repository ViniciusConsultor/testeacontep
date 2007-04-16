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
using eWorld.UI;

public partial class Sistema_Contratos_Cadastros_EmpreiteiraForm : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Permissao.ConfigAcessoFuncao("3");
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public string IDEstadoPadrao
    {
        get
        {
            return ConfigurationManager.AppSettings["IDEstadoPadrao"];
        }
    }
    private void HabilitarMantutencao(FormViewMode formViewMode)
    {
        pnlManutencao.Visible = formViewMode != FormViewMode.ReadOnly;
        pnlPesquisa.Visible = formViewMode == FormViewMode.ReadOnly;
        fvwItem.ChangeMode(formViewMode);
    }

    protected void btnIncluir_Click(object sender, EventArgs e)
    {
        HabilitarMantutencao(FormViewMode.Insert);
    }


    protected void btnAlterar_Click(object sender, EventArgs e)
    {
        HabilitarMantutencao(FormViewMode.Edit);

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        HabilitarMantutencao(FormViewMode.ReadOnly);

    }
    protected void odsManutencao_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        e.InputParameters["CNPJ"] = FormUtil.ObterControle<MaskedTextBox>(fvwItem.Controls, "tbxCNPJ").Text.Replace(".", "").Replace("-", "").Replace("/", "");
        e.InputParameters["IDCidade"] = FormUtil.ObterControle < DropDownList>(fvwItem.Controls, "ddlCidade").SelectedValue;
        e.InputParameters["Cep"] = FormUtil.ObterControle<MaskedTextBox>(fvwItem.Controls, "tbxCep").Text.Replace(".", "").Replace("-", "").Replace("/", "");
    }

    protected void TextoNenumItemInformado(object sender, EventArgs e)
    {
        ((DropDownList)sender).Items.Clear();
        ((DropDownList)sender).Items.Add(new ListItem(":: Não informado(a) ::", string.Empty));

    }
    protected void odsItem_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        this.HabilitarMantutencao(FormViewMode.ReadOnly);
        DadosWeb.AtribuirMensagem(MensagemWeb.MensagemInclusaoSucesso);
        btnPesquisar_Click(sender, e);
    }
    protected void btnPesquisar_Click(object sender, EventArgs e)
    {
        gvwEmpreiteira.DataBind();
    }
    string IDCidade = String.Empty;
    protected void ddlCidade_DataBound(object sender, EventArgs e)
    {
        ((DropDownList)sender).SelectedValue = IDCidade;
    }
    protected void odsManutencao_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.ReturnValue != null && ((DataTable)e.ReturnValue).Rows.Count > 0)
            IDCidade = ((DataTable)e.ReturnValue).Rows[0]["IDCidade"].ToString();
    }
    protected void btnExcluir_Click(object sender, EventArgs e)
    {
        HabilitarMantutencao(FormViewMode.ReadOnly);
        if (gvwEmpreiteira.SelectedDataKey != null)
        {
            odsManutencao.DeleteParameters[0].DefaultValue = gvwEmpreiteira.SelectedDataKey.Value.ToString();
            odsManutencao.Delete();
            DadosWeb.AtribuirMensagem(MensagemWeb.MensagemExclusaoSucesso);
        }
        else
        {
            DadosWeb.AtribuirMensagem(MensagemWeb.MensagemSelecioneItem);
        }
        btnPesquisar_Click(sender, e);

    }
}
