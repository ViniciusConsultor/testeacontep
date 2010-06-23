using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;

namespace WebApplication1
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //string[] scripts = Directory.GetFiles(MapPath("~/App_Themes/Azul/js"), "*.js", SearchOption.AllDirectories);
            
            string[] scripts = new string[]{
                 "jquery-1.3.2.js"
                ,"superfish.js"
                ,"jquery-ui-1.7.2.js"
                ,"tooltip.js"
                ,"tablesorter.js"
                ,"tablesorter-pager.js"
                ,"cookie.js"
                ,"custom.js"};
            foreach (string s in scripts)
            {
                HtmlGenericControl myJs = new HtmlGenericControl();
                myJs.TagName = "script";
                myJs.Attributes.Add("type", "text/javascript");
                myJs.Attributes.Add("language", "javascript"); //don't need it usually but for cross browser.
//                myJs.Attributes.Add("src", string.Format("App_Themes/Azul/js/{0}", Path.GetFileName(s)));
//                myJs.Attributes.Add("src", ResolveUrl(string.Format("/App_Themes/Azul/js/{0}", s)));
                myJs.Attributes.Add("src", (string.Format("/App_Themes/Azul/js/{0}", s)));
                this.Page.Header.Controls.Add(myJs);
            }
            //HtmlLink jsLink = new HtmlLink();
            //jsLink.Href = "~/js.js";
            //jsLink.Attributes.Add("language", "javascript");
            //jsLink.Attributes.Add("type", "text/javascript");
            //Header.Controls.Add(jsLink);


//                #region Write External JavaScript Library <script> Declaration...

//// Dynamically assign path to external Javascript library src
//attribute
//// by writing <script...></script> into the body of the page
//// obviating the need for <script...></script> to be located in
//the
//// <head> element which imposes conflict when using Themes.

//// Define an arbitrary but unique name to use as a key
//String key = "ExternalJavaScriptReference";
//String url = Request.ApplicationPath + "/Scripts/scripts.js";

//// Instantiate ClientScriptManager object
//ClientScriptManager cs = Page.ClientScript;

//// Do not register if this instance of the key is already
//registered.
//if (!cs.IsClientScriptIncludeRegistered(key))
//{
//cs.RegisterClientScriptInclude(key, url);
//}
//#endregion

//    <script type="text/javascript" language="javascript" src="/jquery-1.3.2.js"></script>
//    <script type="text/javascript" language="javascript" src="/superfish.js"></script>
//    <script type="text/javascript" language="javascript" src="/jquery-ui-1.7.2.js"></script>
//    <script type="text/javascript" language="javascript" src="/tooltip.js"></script>
//    <script type="text/javascript" language="javascript" src="/tablesorter.js"></script>
//    <script type="text/javascript" language="javascript" src="/tablesorter-pager.js"></script>
//    <script type="text/javascript" language="javascript" src="/cookie.js"></script>
//    <script type="text/javascript" language="javascript" src="/custom.js"></script>


        }
    }
}
