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

public partial class Sistema_Contratos_Cadastros_EmendaForm : System.Web.UI.Page
{
    public string IDEstadoPadrao
    {
        get
        {
            return ConfigurationManager.AppSettings["IDEstadoPadrao"]; 
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Permissao.ConfigAcessoFuncao("1");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
      
    }
    protected void btnIncluir_Click(object sender, EventArgs e)
    {
        HabilitarMantutencao(FormViewMode.Insert);
    }

    private void HabilitarMantutencao(FormViewMode formViewMode)
    {
        pnlAlteracao.Visible = formViewMode != FormViewMode.ReadOnly;
        pnlGeralEmendas.Visible = formViewMode == FormViewMode.ReadOnly;
        fvwItem.ChangeMode(formViewMode);
    }

    protected void btnAlterar_Click(object sender, EventArgs e)
    {
        HabilitarMantutencao(FormViewMode.Edit);
       
    }
    protected void btnExcluir_Click(object sender, EventArgs e)
    {
        HabilitarMantutencao(FormViewMode.ReadOnly);
        if (gvwEmenda.SelectedDataKey != null)
        {
            odsEmenda.DeleteParameters[0].DefaultValue = gvwEmenda.SelectedDataKey.Value.ToString();
            odsEmenda.Delete();
            DadosWeb.AtribuirMensagem(MensagemWeb.MensagemInclusaoSucesso);
        }
        btnPesquisar_Click(sender, e);

    }
    protected void btnPesquisar_Click(object sender, EventArgs e)
    {
        gvwEmenda.DataBind();
    }
    protected void TextoTodos_DataBinding(object sender, EventArgs e)
    {
        ((DropDownList)sender).Items.Clear();
        ((DropDownList)sender).Items.Add(new ListItem(":: Todos(as) ::", string.Empty));
    }
    
    protected void TextoNenumItemInformado(object sender, EventArgs e)
    {
        ((DropDownList)sender).Items.Clear();
        ((DropDownList)sender).Items.Add(new ListItem(":: Não informado(a) ::", string.Empty));
    
    }
    protected void odsManutencao_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        string IDCidade = FormUtil.ObterControle<DropDownList>(fvwItem.Controls, "ddlCidade").SelectedValue;
        e.InputParameters["IDCidade"] = string.IsNullOrEmpty(IDCidade) ? null : IDCidade;
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        this.HabilitarMantutencao(FormViewMode.ReadOnly);

    }
    protected void odsItem_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        this.HabilitarMantutencao(FormViewMode.ReadOnly);
        DadosWeb.AtribuirMensagem(MensagemWeb.MensagemInclusaoSucesso);
        btnPesquisar_Click(sender, e);
    }
    protected void ddlCidade_DataBound(object sender, EventArgs e)
    {
        ((DropDownList)sender).SelectedValue = IDCidade;
    }
    string IDCidade = String.Empty;
    protected void odsItem_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.ReturnValue != null && ((DataTable)e.ReturnValue).Rows.Count > 0)
            IDCidade = ((DataTable)e.ReturnValue).Rows[0]["IDCidade"].ToString();
    }
    protected void ddlCidade_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
