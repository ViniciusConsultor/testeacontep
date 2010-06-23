using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Nome");
            dt.Columns.Add("email");
            dt.Columns.Add("telefone");
            for (int i = 0; i < 10; i++)
            {
                dt.Rows.Add("Registro " + i, "email" + i + "@dominio.com", i);
            }
            GridView1.DataSource = dt;
            GridView2.DataSource = dt.Clone();
            GridView1.DataBind();
            GridView2.DataBind();
        }
    }
}