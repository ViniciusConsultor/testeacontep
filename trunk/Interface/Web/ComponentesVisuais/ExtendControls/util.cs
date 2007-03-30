using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.IO;

namespace ExtendControls
{
    public class util
    {
        private const string _KEY_SCRIPT_UTIL = "ScriptUtil";
        private const string _UTIL_JS = "util.js";
        public static string configEstructe(Page page)
        {
            return configPath(page.Request.PhysicalApplicationPath, true);
        }

        public static string configPath(string root,bool create)
        {
            string path = Path.Combine(root, "ExtendControls");
            if (!Directory.Exists(path) && create)
                Directory.CreateDirectory(path);                
            path = Path.Combine(path, "Scripts&Resources");
            if(!Directory.Exists(path) && create)
                Directory.CreateDirectory(path);            
            return path;
        }

        public static string obterPathRelativy(Page page)
        {
            return util.configPath("/" + page.Request.Path.Split('/')[1], false);
        }

        public static void writeScript(string script, string path)
        {
            StreamWriter sw = new StreamWriter(path);
            sw.Write(script);
            sw.Close();
        }
        public static void registerScript(Page page, string key, string file, string script)
        {
            string path = util.configEstructe(page);
            if (!page.ClientScript.IsClientScriptIncludeRegistered(key))
            {
                string pathRelativity = Path.Combine(util.obterPathRelativy(page), file);
                string path_Script = Path.Combine(path, file);
                if (!System.IO.File.Exists(path_Script))
                    util.writeScript(script, path_Script);
                else
                {
                    StreamReader scriptRead = new StreamReader(path_Script);
                    if (scriptRead.ReadToEnd() != script)
                    {
                        scriptRead.Close();
                        util.writeScript(script, path_Script);
                    }
                    else
                        scriptRead.Close();
                }
                page.ClientScript.RegisterClientScriptInclude(key, pathRelativity);
            }
        }

        public static void registerScriptUtil(Page page)
        {
            registerScript(page, _KEY_SCRIPT_UTIL, _UTIL_JS, UtilResource.util);
            //string path = util.configEstructe(page);
            //if (!page.ClientScript.IsClientScriptIncludeRegistered(_KEYSCRIPTUTIL))
            //{
            //    string pathRelativity_Util = Path.Combine(util.configPath("/" + page.Request.Path.Split('/')[1], false), _UTIL_JS);
            //    string path_Util = Path.Combine(path, _UTIL_JS);
            //    if (!System.IO.File.Exists(path_Util))
            //        util.writeScript(UtilResource.util, path_Util);
            //    else
            //    {
            //        StreamReader scriptRead = new StreamReader(path_Util);
            //        if (scriptRead.ReadToEnd() != UtilResource.util)
            //        {
            //            scriptRead.Close();
            //            util.writeScript(UtilResource.util, path_Util);
            //        }
            //        else
            //            scriptRead.Close();
            //    }
            //    page.ClientScript.RegisterClientScriptInclude(_KEYSCRIPTUTIL, pathRelativity_Util);
            //}
        }
    
        public static Control ObterControle<T>(ControlCollection controls, string ID)
        {
            foreach (Control controle in controls)
            {
                if (controle.Controls.Count > 0)
                {
                    Control obj = ObterControle<T>(controle.Controls, ID);
                    if (obj != null)
                        return obj;
                }
                if (controle is T)
                {
                    if (controle.ID == ID)
                        return controle;
                }
            }
            return null;
        }

        public static Control[] ObterControle<T>(ControlCollection controls)
        {
            List<Control> items = new List<Control>();
            foreach (Control controle in controls)
            {
                if (controle.Controls.Count > 0)
                {
                    Control[] obj = ObterControle<T>(controle.Controls);
                    if (obj != null)
                    {                        
                        foreach (Control i in obj)
                        {
                            //if (items.BinarySearch(i) == -1)
                                items.Add(i);
                        }
                    }
                }
                if (controle is T)
                    items.Add(controle);
            }
            if (items.Count == 0)
                return null;
            else
                return items.ToArray();
        }
    }
}
