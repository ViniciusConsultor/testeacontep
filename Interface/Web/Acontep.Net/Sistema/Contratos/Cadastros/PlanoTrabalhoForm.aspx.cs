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
using Acontep;

public partial class Sistema_Contratos_Cadastros_PlanoTrabalhoForm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            HabilitarTipoPesquisa();
    }
    
    protected void ddlTipoPesquisa_SelectedIndexChanged(object sender, EventArgs e)
    {
        HabilitarTipoPesquisa();
    }

    private void HabilitarTipoPesquisa()
    {
        tbxPesquisa.Visible = "CP;CE".IndexOf(ddlTipoPesquisa.SelectedValue) > -1;
        tbxDataPesquisa.Visible = "CP;CE".IndexOf(ddlTipoPesquisa.SelectedValue) == -1;
        switch (ddlTipoPesquisa.SelectedValue)
        {
            case "CP":
                tbxPesquisa.Mask = Formatacao.MascaraPlanoTrabalho;
                break;
            case "CE":
                tbxPesquisa.Mask = String.Empty;
                break;
        }
    }
    protected void btnPesquisar_Click(object sender, EventArgs e)
    {
        gvwItem.DataBind();
    }
}
