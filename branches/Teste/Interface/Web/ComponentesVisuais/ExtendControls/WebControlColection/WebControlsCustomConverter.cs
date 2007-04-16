using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Globalization;
using System.Collections;

namespace ExtendControls.WebControlColection
{
    public class WebControlsCustomConverter : TypeConverter
    {
        public WebControlsCustomConverter()
        {
        }

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
            if (!(value is string))
            {
                //throw base.GetConvertFromException(value);
                throw new Exception("Tipo incorreto");
            }
            if (((string)value).Length == 0)
            {
                return new WebControlCustom[0];
            }
            string[] textArray1 = ((string)value).Split(new char[] { ',' });
            List<WebControlCustom> webcontrols = new List<WebControlCustom>();
            //ArrayList webcontrols = new ArrayList();
            //SelectedWebControlsCustom[] webcontrols = new SelectedWebControlsCustom[textArray1.Length];
            for (int num1 = 0; num1 < textArray1.Length; num1++)
            {
                WebControlCustom item = new WebControlCustom(true,true,textArray1[num1].Trim());
                //item.Enable = true;
                //item.Text = textArray1[num1].Trim();
                webcontrols.Add(item);
                //textArray1[num1] = textArray1[num1].Trim();
                //textArray1[num1] = textArray1[num1].Trim();
            }
            return webcontrols.ToArray();
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType != typeof(string))
            {
                throw base.GetConvertToException(value, destinationType);
            }
            if (value == null)
            {
                return string.Empty;
            }
            return GetItemsSelected(value);
            //return string.Join(",", ((List<string>)value).ToArray());
        }

        private string GetItemsSelected(object value)
        {
            //----
            string list = string.Empty;
            if (value != null)
            {
                WebControlCustom[] items;
                try
                {
                    items = (WebControlCustom[])value;
                }
                catch
                {
                    //items = new SelectedWebControlsCustom[1] { SelectedWebControlsCustom.Criar() };
                    items = new WebControlCustom[0];
                }
                if (items.Length == 0)
                    return "";

                //foreach (SelectedWebControlsCustom i in items)
                foreach (WebControlCustom i in items)
                {
                    if (i.Enable && !string.IsNullOrEmpty(i.Text))
                    {
                        if (list == string.Empty)
                            list = i.Text;
                        else
                            list += ',' + i.Text;
                    }
                }
            }
            return list;
            //----->
        }
    }
}
