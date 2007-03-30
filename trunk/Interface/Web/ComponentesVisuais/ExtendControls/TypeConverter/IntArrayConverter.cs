using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Globalization;
using System.Collections;

namespace ExtendControls.TypeConverterCustom
{
    public class IntArrayConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(int))
            {
                return true;
            }
            return false;
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (!(value is string))
            {
                throw base.GetConvertFromException(value);
            }
            if (((string)value).Length == 0)
            {
                return new string[0];
            }
            string[] textArray1 = ((string)value).Split(new char[] { ',' });
            int[] items = new int[textArray1.Length];
            for (int num1 = 0; num1 < textArray1.Length; num1++)
            {
                //textArray1[num1] = textArray1[num1].Trim();
                items[num1] = Convert.ToInt32(textArray1[num1].Trim());
            }
            return items;
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
            string items = string.Empty;
            foreach (int i in (int[])value)
            {
                if (items == string.Empty)
                    items += i.ToString();
                else
                    items += "," + i.ToString();
            }
            return items;
            //return string.Join(",", (int[])value);
        }
    }
}
