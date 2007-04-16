using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace ExtendControls.GridViewCustom
{
    public partial class GridView : System.Web.UI.WebControls.GridView
    {
        #region Constants
        private const string _CHECKBOXCOLUMHEADERTEMPLATE = "<input type='checkbox' hidefocus='true' id='{0}' name='{0}' {1} onclick='CheckAll(this,{2})'>";
        private const string _GRIDVIEWHEADERID = "{0}_HeaderButton";
        private const int _COLUMWIDTH = 20;
        //private const string RadioButtonColumHeaderTemplate = "<input type='radio' hidefocus='true' GroupName='{0}' id='{0}' name='{0}' {1} onclick='CheckAll(this)'>";        
        //private const string RadioButtonColumHeaderID = "{0}_HeaderButton";      

        #endregion
        
        // METHOD:: add a brand new checkbox column
        protected virtual ArrayList AddCheckBoxColumn(ArrayList columnList)
        {
            // Get a new container of type ArrayList that contains the given collection. 
            // This is required because ICollection doesn't include Add methods
            // For guidelines on when to use ICollection vs IList see Cwalina's blog

            // Determine the check state for the header checkbox
            string shouldCheck = "";
            string checkBoxID = String.Format(_GRIDVIEWHEADERID, ClientID);
            if (!DesignMode)
            {
                object o = Page.Request[checkBoxID];
                if (o != null)
                {
                    shouldCheck = "checked=\"checked\"";
                }
            }
            
            // Create a new custom CheckBoxField object 
            SelectCheckBoxCustom field = new SelectCheckBoxCustom((ModelSelect == ModelSelectType.RowSelected ? ID : string.Empty));
            field.HeaderText = String.Format(_CHECKBOXCOLUMHEADERTEMPLATE, checkBoxID, shouldCheck, AutoGenerateColumnSelectAutoPostBack.ToString().ToLower());
            field.HeaderStyle.Width = _COLUMWIDTH;
            field.ItemStyle.Width = _COLUMWIDTH;
            if (ModelSelect == ModelSelectType.RowSelected)
                field.HeaderStyle.CssClass = "coluna";                           
                
            field.ReadOnly = true;

            // Insert the checkbox field into the list at the specified position
            if (GenerateColumnSelectIndex > columnList.Count)
            {
                // If the desired position exceeds the number of columns 
                // add the checkbox field to the right. Note that this check
                // can only be made here because only now we know exactly HOW 
                // MANY columns we're going to have. Checking Columns.Count in the 
                // property setter doesn't work if columns are auto-generated
                columnList.Add(field);
                GenerateColumnSelectIndex = columnList.Count - 1;
            }
            else
                columnList.Insert(GenerateColumnSelectIndex, field);

            // Return the new list
            return columnList;
        }

        // METHOD:: add a brand new radiobutton column
        protected virtual ArrayList AddRadioButtonColumn(ArrayList columnList)
        {
            // Get a new container of type ArrayList that contains the given collection. 
            // This is required because ICollection doesn't include Add methods
            // For guidelines on when to use ICollection vs IList see Cwalina's blog
            //ArrayList list = new ArrayList(columnList);

            // o Cabecario do radiobutton ñ aparece então ñ precisa de impressão de html no designer
            // Determine the check state for the header radiobutton
            //string shouldCheck = "";
            //string radiobuttonID = String.Format(RadioButtonColumHeaderID, ClientID);
            //if (!DesignMode)
            //{
            //    object o = Page.Request[radiobuttonID];
            //    if (o != null)
            //    {
            //        shouldCheck = "radio=\"radio\"";
            //    }
            //}

            // Create a new custom RadioButtonList object 
            //InputRadioButtonList field = new InputRadioButtonList();
            SelectRadioButtonCustom field = new SelectRadioButtonCustom((ModelSelect == ModelSelectType.RowSelected ? ID : string.Empty));
            field.HeaderText = "";//String.Format(RadioButtonColumHeaderTemplate, radiobuttonID, shouldCheck);
            field.HeaderStyle.Width = _COLUMWIDTH;
            field.ItemStyle.Width = _COLUMWIDTH;
            if (ModelSelect == ModelSelectType.RowSelected)
                field.HeaderStyle.CssClass = "coluna";                           
            field.ReadOnly = true;
            

            // Insert the checkbox field into the list at the specified position
            if (GenerateColumnSelectIndex > columnList.Count)
            {
                // If the desired position exceeds the number of columns 
                // add the checkbox field to the right. Note that this check
                // can only be made here because only now we know exactly HOW 
                // MANY columns we're going to have. Checking Columns.Count in the 
                // property setter doesn't work if columns are auto-generated
                columnList.Add(field);
                GenerateColumnSelectIndex = columnList.Count - 1;
            }
            else
                columnList.Insert(GenerateColumnSelectIndex, field);

            // Return the new list
            return columnList;
        }

    
        // METHOD:: retrieve the style object based on the row state
        protected virtual TableItemStyle GetRowStyleFromState(DataControlRowState state)
        {
            switch (state)
            {
                case DataControlRowState.Alternate:
                    return AlternatingRowStyle;
                case DataControlRowState.Edit:
                    return EditRowStyle;
                case DataControlRowState.Selected:
                    return SelectedRowStyle;
                default:
                    return RowStyle;
                // DataControlRowState.Insert is not relevant here
            }
        }

        //private void AddCustomHeader()
        //{
        //    Table table1 = (Table)this.Controls[0];
        //    table1.Rows.AddAt(0, this.gvRowSearch);
        //}
    }
}