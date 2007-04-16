using System;
using System.ComponentModel;
using System.Globalization;
using System.Collections;
using System.Web.UI;
using ExtendControls;
using System.Web.UI.WebControls;



namespace ExtendControls.WebControlColection
{
    class WebControlCustomColectionConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            
            if (sourceType == typeof(string))
            {
                return true;
            }
            return false;
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value != null)
            {
                if (value.GetType() != typeof(string))
                {
                    throw base.GetConvertFromException(value);
                }
                return (string)value;
            }
            return string.Empty;            
        }

        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            string[] textArray1 = null;
            ArrayList list1 = new ArrayList();
            if (context != null)
            {
                IComponent component1 = context.Instance as IComponent;
                if (component1 != null)
                {
                    MarshalByValueComponent teste = new MarshalByValueComponent();

                    GridViewCustom.GridView view1 = component1 as GridViewCustom.GridView;
                    if (view1 != null)
                    {
                        

                        foreach (Control control in util.ObterControle<WebControlCustomColection>(view1.Page.Controls))
                        {
                            string text1 = control.ID;
                            if (!list1.Contains(text1))
                            {
                                list1.Add(text1);
                            }
                        }
                        list1.Sort();
                        //foreach (Control control in view1.Page.Controls)
                        //{
                        //    if(control is WebControlCustomColection)
                        //    {
                        //        string text1 = control.ID;
                        //        if (!list1.Contains(text1))
                        //        {
                        //            list1.Add(text1);
                        //        }
                        //    }
                        //}                        
                        list1.Sort();
                        textArray1 = new string[list1.Count];
                        list1.CopyTo(textArray1, 0);
                    }
                }
            }
            return new TypeConverter.StandardValuesCollection(textArray1);
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return false;
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            if ((context != null) && (context.Instance is IComponent))
            {
                return true;
            }
            return false;
        }
    }
}
