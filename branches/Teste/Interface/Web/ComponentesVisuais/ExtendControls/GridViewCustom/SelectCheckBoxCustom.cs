using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace ExtendControls.GridViewCustom
{
    internal sealed class SelectCheckBoxCustom : CheckBoxField
    {
        public const string SelectCheckBoxCustomID = "SelectCheckBoxCustom";
        private string gridID = string.Empty;

        public SelectCheckBoxCustom(string id)
        {
            gridID = id;
        }

        protected override void InitializeDataCell(DataControlFieldCell cell, DataControlRowState rowState)
        {
            base.InitializeDataCell(cell, rowState);

            // Add a checkbox anyway, if not done already            
            if (cell.Controls.Count == 0)
            {
                if (gridID != string.Empty && !DesignMode)
                {                    
                    Control gvw = util.ObterControle<WebControl>(((Page)HttpContext.Current.CurrentHandler).Controls, gridID);                    
                    if (gvw != null)
                        if (((GridView)gvw).ModelSelect == GridView.ModelSelectType.RowSelected)
                        {
                            cell.CssClass = "coluna";
                            //cell.Style["position"] =  "absolute";
                            //cell.Style["visibility"] = "hidden";
                        }
                }
                //string identificadorCheckBox = "<input type='checkbox";
                //string identificadorRadioButton = "<input type='radio'";
                //if (cell.ContainingField.HeaderText.Substring(0, identificadorCheckBox.Length) == identificadorCheckBox)
                //{
                CheckBox chk = new CheckBox();
                chk.ID = SelectCheckBoxCustom.SelectCheckBoxCustomID;
                //chk.Attributes["visibility"] = "hiden";
                    //Style.Add("visibility", "hiden");
                cell.Controls.Add(chk);
                //}
                //else if (cell.ContainingField.HeaderText.Substring(0, identificadorRadioButton.Length) == identificadorRadioButton)
                //{
                //    //<input runat="server" id="Radio1" type="radio" />
                //     //onclick='{2}'
                //    //Literal radioButton = new Literal();
                //    //radioButton.Text = String.Format("<input type='radio' runat='server' name=\"{1}\"/>", "Teste", "Teste");
                //    //cell.Controls.Add(radioButton);
                //    //TODO: olha o valor deste campo Resp. Valor null igualmente p/ ClientID
                //    //cell.Parent.ClientID                    
                //    //<input type='radio' hidefocus='true' GroupName='GridView3_HeaderButton' id='GridView3_HeaderButton' name='GridView3_HeaderButton'  onclick='CheckAll(this)'>                    

                //    RadioButton rbn = new RadioButton();
                //    rbn.ID = SelectRadioButtonCustom.SelectRadioButtonCustomID;                    
                //    cell.Controls.Add(rbn);
                //}                
            }
        }        
    }
}
