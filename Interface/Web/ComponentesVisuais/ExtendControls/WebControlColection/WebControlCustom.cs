using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Collections;
using System.Web.UI;
using System.Globalization;
using System.Web.UI.WebControls;


namespace ExtendControls.WebControlColection
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class WebControlCustom
    {
        bool _visible;
        bool _enable;
        string _text;

        static WebControlCustom()
        {
        }

        public WebControlCustom()
            : this(false, false,"WebControl")
        {
        }

        public WebControlCustom(bool visible, bool enable, string text)
        {
            _visible = visible;
            _enable = enable;
            _text = text;
        }

        [
        Category("Behavior"),
        DefaultValue(""),
        Description("Visible Control"),
        NotifyParentProperty(true),
        //TypeConverter(typeof(Tobool)),
        ]
        public bool Visible
        {
            get
            {
                return _visible;
            }
            set
            {
                _visible = value;
            }
        }

        [
        Category("Behavior"),
        DefaultValue(""),
        Description("Name of contact"),
        NotifyParentProperty(true),
        //TypeConverter(typeof(Tobool)),
        ]
        public bool Enable
        {
            get
            {
                return _enable;
            }
            set
            {
                _enable = value;
            }
        }

        public override string ToString()
        {
            return _text;
        }

        [
        //Browsable(false),
        Category("Behavior"),
        DefaultValue("WebControl"),
        Description("Name of WebControl"),
        NotifyParentProperty(true)
        ]
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
            }
        }

        public static ArrayList GetWebControls(ControlCollection controls)
        {
            ArrayList retorno = new ArrayList();
            retorno.Clear();            
            foreach (Control item in util.ObterControle<WebControl>(controls))
            {                
                if ((item is WebControl) && !(item is WebControlCustomColection))
                {
                    if (!ControleExiste(retorno, item.ID))
                    {
                        WebControlCustom control = new WebControlCustom(false, false, item.ID);
                        retorno.Add(control);
                    }
                }
            }            
            return retorno;
        }

        public static bool ControleExiste(ArrayList items, string ID)
        {
            foreach (WebControlCustom i in items)
                if (i.Text == ID)
                    return true;
            return false;
        }

        public static bool ControleExiste(ArrayList items, string ID, out int pos)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if(items[i] is WebControlCustom)
                    if (((WebControlCustom)items[i]).Text == ID)
                    {
                        pos = i;
                        return true;
                    }
            }
            pos = -1;
            return false;
        }

        /// <summary>
        /// Verifica se os items listados ainda existem na lista corrent 
        /// e configura suas proprieddaes nessa lista.
        /// </summary>
        /// <param name="corrent">Lista de WebControls Atual </param>
        /// <param name="Listed">Lista de WebControls definidos</param>
        /// <returns>Retorna a lista de webcontrols que existem e suas propriedades definas</returns>
        public static ArrayList AtualizaWebControlList(ArrayList corrent, ArrayList listed)
        {
            foreach (WebControlCustom item in corrent)
            {
                int pos = -1;
                if (ControleExiste(listed, item.Text, out pos))
                {
                    item.Enable = ((WebControlCustom)listed[pos]).Enable;
                    item.Visible = ((WebControlCustom)listed[pos]).Visible;
                }

            }            
            return corrent;
        }


        //public static List<WebControlCustom> WebControls(ControlCollection controls)
        //{
        //    List<WebControlCustom> retorno = new List<WebControlCustom>();
        //    retorno.Clear();
        //    //for (int i = 0; i < 8; i++)
        //    //{
        //    //    WebControlCustom control = new WebControlCustom(false, false, "WebControl_" + i.ToString());
        //    //    retorno.Add(control);
        //    //}
        //    foreach (Control item in controls)
        //    {
        //        if ((item is WebControl) && !(item is WebControlCustomColection))
        //        {
        //            WebControlCustom control = new WebControlCustom(false, false, item.ID);
        //            retorno.Add(control);
        //        }
        //    }
        //    return retorno;
        //}        
    }

    //public class Tobool : TypeConverter
    //{
    //    public Tobool()
    //    {
    //    }

    //    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    //    {
    //        if (sourceType == typeof(string))
    //        {
    //            return true;
    //        }
    //        return false;
    //    }

    //    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    //    {
    //        if (!(value is string))
    //        {
    //            throw new Exception("Tipo incorreto");
    //        }
    //        if (((string)value).Length == 0)
    //        {
    //            return false;
    //        }
    //        if ((string)value == "true")
    //            return true;

    //        return false;
    //    }

    //    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
    //    {
    //        if (destinationType != typeof(string))
    //        {
    //            throw base.GetConvertToException(value, destinationType);
    //        }
    //        if (value == null)
    //        {
    //            return false;
    //        }
    //        if ((bool)value == true)
    //            return true;
    //        return false;
    //    }
    //}
}
