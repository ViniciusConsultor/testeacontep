using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExtendControls.GridViewCustom
{
    internal sealed class SelectRadioButtonCustom : CheckBoxField
    {
        public const string SelectRadioButtonCustomID = "SelectRadioButtonCustom";
        private string gridID = string.Empty;

        public SelectRadioButtonCustom(string id)
        {
            gridID = id;
        }

        protected override void InitializeDataCell(DataControlFieldCell cell, DataControlRowState rowState)
        {
            base.InitializeDataCell(cell, rowState);

            // Add a radiobutton anyway, if not done already
            if (cell.Controls.Count == 0)
            {
                if (gridID != string.Empty && !DesignMode)
                {
                    Control gvw = util.ObterControle<WebControl>(((Page)HttpContext.Current.CurrentHandler).Controls, gridID);
                    if (gvw != null)
                        if (((GridView)gvw).ModelSelect == GridView.ModelSelectType.RowSelected)
                        {
                            cell.CssClass = "coluna";
                            //cell.Style["visibility"] = "hidden";
                        }
                }
                RadioButton rbn = new RadioButton();
                rbn.ID = SelectRadioButtonCustom.SelectRadioButtonCustomID;
                cell.Controls.Add(rbn);
            }
        }
    }
}
