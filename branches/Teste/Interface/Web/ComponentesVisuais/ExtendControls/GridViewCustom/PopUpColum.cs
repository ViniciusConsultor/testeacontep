using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace ExtendControls.GridViewCustom
{
    public class PopUpColum
    {
        int _row = 0;
        int _colum = 0;
        string _innerHtml = string.Empty;
        Hashtable table = new Hashtable();
        public PopUpColum()
        {

        }

        public int Row
        {
            get { return _row; }
            set { _row = value; }
        }

        public int Colum
        {
            get { return _colum; }
            set { _colum = value; }
        }

        public string InnerHtml
        {
            get { return _innerHtml; }
            set { _innerHtml = value; }
        }
    }
}
