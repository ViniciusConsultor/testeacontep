using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Web;
using System.ComponentModel;
using System.Web.UI;
using System.Security.Permissions;

namespace ExtendControls.ModalMenssager
{
    [
    ToolboxData(
        "<{0}:ModalMessager runat=\"server\"> </{0}:ModalMessager>")
    ]
    public class ModalMessager : WebControl
    {

        //static ModalMenssager()
        //{
        //}
        string menssager = string.Empty;
        public void ModalMenssager()
        {
        }

        public string Menssager
        {
            get
            {
                return menssager;
            }
            set
            {
                menssager = value;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (Menssager != string.Empty)
            {
                //Page.ClientScript.re
            }
            base.OnPreRender(e);

        }

        protected override void RenderContents(
            HtmlTextWriter writer)
        {
            if (DesignMode)
            {
                Panel p = new Panel();
                p.Width = 20;
                p.GroupingText = this.ID;
                p.RenderControl(writer);
            }
        }
    }
}
