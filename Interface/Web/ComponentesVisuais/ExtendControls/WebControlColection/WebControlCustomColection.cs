using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.ComponentModel;
using System.Web.UI;
using System.Drawing.Design;
using System.Collections;
using System.Security.Permissions;
using System.Web.UI.WebControls;

namespace ExtendControls.WebControlColection
{
    [
    AspNetHostingPermission(SecurityAction.Demand,
        Level = AspNetHostingPermissionLevel.Minimal),
    AspNetHostingPermission(SecurityAction.InheritanceDemand,
        Level = AspNetHostingPermissionLevel.Minimal),
    DefaultProperty("WebControl"),
    ParseChildren(true, "WebControl"),
    ToolboxData(
        "<{0}:WebControlCustomColection runat=\"server\"> </{0}:WebControlCustomColection>")
    ]
    public class WebControlCustomColection : WebControl
    {
        private ArrayList _webControlsList;
        private static readonly Type[] knownTypes;
        private const string _KEY_SCRIPT = "ScriptWebControlCustomColection";
        private const string _WEB_CONTROL_CUSTOM_COLECTION_JS = "WebControlCustomColection.js";


        static WebControlCustomColection()
        {
            WebControlCustomColection.knownTypes = new Type[] { typeof(WebControlCustom) };
            //this.webControlsList = colection;
            //throw new Exception("Statico");
        }

        public WebControlCustomColection()
        {

        }

        [
        Category("Behavior"),
        Description("The WebControlColection"),
        DesignerSerializationVisibility(
            DesignerSerializationVisibility.Content),
        Editor(typeof(WebControlCustomEditor), typeof(UITypeEditor)),
        PersistenceMode(PersistenceMode.InnerDefaultProperty)
        ]
        public ArrayList WebControl
        {
            get
            {
                if (DesignMode)
                {
                    if (_webControlsList == null)
                    {
                        if (Page != null)
                            _webControlsList = WebControlCustom.GetWebControls(Page.Controls);
                        else
                            _webControlsList = new ArrayList();
                    }
                    else
                    {
                        if (Page != null)
                        {
                            if (_webControlsList.Count == 0)
                                _webControlsList = WebControlCustom.GetWebControls(Page.Controls);
                            //else
                            //    _webControlsList = WebControlCustom.AtualizaWebControlList(WebControlCustom.GetWebControls(Page.Controls)
                            //    , _webControlsList);
                        }
                    }
                }
                else
                {
                    if (_webControlsList == null)
                    {

                        _webControlsList = new ArrayList();
                    }
                }
                return _webControlsList;
            }
            //get
            //{
            //    if (DesignMode)
            //    {
            //        if (_webControlsList == null)
            //        {
            //            if (Page != null)
            //                _webControlsList = WebControlCustom.GetWebControls(Page.Controls);
            //            else
            //                _webControlsList = new ArrayList();
            //        }
            //        else
            //        {
            //            //if (_webControlsList.Count == 0)
            //            //{
            //            //    if (Page != null)
            //            //        _webControlsList = WebControlCustom.GetWebControls(Page.Controls);
            //            //}
            //            if (Page != null)
            //            {
            //                _webControlsList = WebControlCustom.AtualizaWebControlList(WebControlCustom.GetWebControls(Page.Controls)
            //                    , _webControlsList);
            //            }
            //            //TODO: Fazer uma comparação dos quer existem com os listados
            //        }                
            //    }            //    else
            //    {
            //        if (_webControlsList == null)
            //        {
            //            if (Page != null)
            //                _webControlsList = WebControlCustom.GetWebControls(Page.Controls);
            //            else
            //                _webControlsList = new ArrayList();
            //        }
            //    }
            //    return _webControlsList;
            //}
            //set
            //{
            //    //_webControlsList.Clear();
            //    if (value != null)
            //    {
            //        if (value is ArrayList)
            //            _webControlsList = value;
            //        else
            //            throw new Exception("ArrayList");
            //    }
            //    else
            //        _webControlsList = new ArrayList();
            //}
        }


        // The contacts are rendered in an HTML table.
        protected override void RenderContents(
            HtmlTextWriter writer)
        {
            if (DesignMode)
            {
                Panel p = new Panel();
                p.Width = 20;
                p.GroupingText = this.ID;
                p.RenderControl(writer);
                //Table t = CreateContactsTable();
                //if (t != null)
                //{
                //    t.RenderControl(writer);
                //}
            }
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            util.registerScript(Page, _KEY_SCRIPT, _WEB_CONTROL_CUSTOM_COLECTION_JS, WebControlCustomColectionResource.WebControlCustomColection);
        }

        //private Table CreateContactsTable()
        //{
        //    Table t = null;

        //    if (contactsList != null && contactsList.Count > 0)
        //    {
        //        t = new Table();

        //        foreach (WebControlCustom item in contactsList)
        //        {
        //            WebControlCustom aContact = item as WebControlCustom;

        //            if (aContact != null)
        //            {
        //                TableRow r = new TableRow();

        //                TableCell c1 = new TableCell();
        //                c1.Text = aContact.Enable.ToString();
        //                r.Controls.Add(c1);

        //                TableCell c2 = new TableCell();
        //                c2.Text = aContact.Visible.ToString();
        //                r.Controls.Add(c2);

        //                TableCell c3 = new TableCell();
        //                c3.Text = aContact.Text;
        //                r.Controls.Add(c3);

        //                t.Controls.Add(r);
        //            }
        //        }
        //    }
        //    return t;
        //}

    }
}