using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.Design.WebControls;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Security.Permissions;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Design;
using ExtendControls.GridViewCustom;
using ExtendControls.FilterCommand;
using ExtendControls.TypeConverterCustom;

namespace ExtendControls.GridViewCustom
{
    public delegate void FilterCommandEventHandler(object sender, FilterCommandEventArgs e);

    [DesignTimeVisible(true)]
    [
     AspNetHostingPermission(SecurityAction.Demand,
        Level = AspNetHostingPermissionLevel.Minimal),
    AspNetHostingPermission(SecurityAction.InheritanceDemand,
        Level = AspNetHostingPermissionLevel.Minimal),
        //DefaultProperty("WebControlCustom"),
    ]
    public partial class GridView : System.Web.UI.WebControls.GridView
    {
        private const string _GRIDVIEW_JS = "HotGridView.js";
        private const string _RESOURCE_CSS = "css.css";
        private const string _THEMA = "thema";
        private const string _COLECTION = "colection";
        private const string _START = "START";
        private const string _POPUP = "PopUp";
        private const string _KEY_SCRIPT_GRID = "ScriptGridViewCustom";

        private bool _autoGenerateColumSelectAutoPostBack;
        private int _generateColumSelectIndex;
        private List<PopUpColum> _popupColum = new List<PopUpColum>();
        private AutoGenerateColumnType _autoGenerateColumSelect;
        private ModelSelectType _modelSelected;
        private bool _enableMouseOver = true;
        private string _webControlCustomColection;
        private List<int> _cachedSelectedIndices = new List<int>();
        private string ImageAsc;
        private string ImageDesc;
        private bool isFiltered;
        private bool expandedFiltered;

        List<string> _controlVisible = new List<string>();
        List<string> _controlEnable = new List<string>();

        //private TextBox searchBox;
        //private GridViewRow gvRowSearch;

        public enum AutoGenerateColumnType
        {
            None,
            RadioButton,
            CheckBox
        }

        public enum ModelSelectType
        {
            ColumnSelected,
            RowSelected,
            ColumnAndRowSelected//  ColumnSelected | RowSelected
        }

        static GridView()
        {
            //GridView.EventFilterCommand = new object();
        }

        public GridView()
        {
            //this._pageCount = -1;
            //this._editIndex = -1;
            //this._selectedIndex = -1;
            //this._sortExpression = string.Empty;
            //this.gvRowSearch = new GridViewRow(-1, -1, DataControlRowType.Header, DataControlRowState.Normal);
        }

        //[Category("Extended GridView Properties")]
        //public event FilterCommandEventHandler FilterCommand;
        //private static readonly object EventFilterCommand;

        //private void HandleFilterCommand(object sender, EventArgs e)
        //{
        //    FilterCommandEventArgs args1 = new FilterCommandEventArgs();
        //    args1.FilterExpression = this.searchBox.Text;
        //    this.OnFilterCommand(args1);
        //}
        //protected virtual void OnFilterCommand(FilterCommandEventArgs e)
        //{
        //    FilterCommandEventHandler handler1 = (FilterCommandEventHandler)base.Events[GridView.EventFilterCommand];
        //    this.SelectedIndex = -1;
        //    if (handler1 != null)
        //    {
        //        handler1(this, e);
        //    }
        //}
        //private void CallFilterCommandHandler()
        //{
        //    if (this.IsFiltered)
        //    {
        //        this.SelectedIndex = -1;
        //        FilterCommandEventArgs args1 = new FilterCommandEventArgs(this.searchBox.Text);
        //        this.OnFilterCommand(args1);
        //    }
        //}

        //protected override void RaisePostBackEvent(string eventArgument)
        //{
        //    if (eventArgument.StartsWith("rc"))
        //    {
        //        //int num1 = int.Parse(eventArgument.Substring(2));
        //        //RowClickEventArgs args1 = new RowClickEventArgs(this.Rows[num1]);
        //        //this.OnRowClick(args1);
        //    }
        //    else if (eventArgument.StartsWith("rdc"))
        //    {
        //        //int num2 = int.Parse(eventArgument.Substring(3));
        //        //RowDoubleClickEventArgs args2 = new RowDoubleClickEventArgs(this.Rows[num2]);
        //        //this.OnRowDoubleClick(args2);
        //    }
        //    else
        //    {
        //        base.RaisePostBackEvent(eventArgument);
        //    }
        //}


        //[Category("Extended GridView Properties")]
        //public event FilterCommandEventHandler FilterCommand
        //{
        //    add
        //    {
        //        base.Events.AddHandler(GridViewCustom.EventFilterCommand, value);
        //    }
        //    remove
        //    {
        //        base.Events.RemoveHandler(GridViewCustom.EventFilterCommand, value);
        //    }
        //}

        internal virtual int ActualColumns
        {
            get
            {
                if (this.AutoGenerateColumnSelect == AutoGenerateColumnType.None)
                {
                    return this.Columns.Count;
                }
                return (this.Columns.Count + 2);
            }
        }

        //private void CreateCustomHeader()
        //{
        //    TableCell cell1 = new TableHeaderCell();
        //    cell1.HorizontalAlign = HorizontalAlign.Right;
        //    cell1.ColumnSpan = this.ActualColumns;
        //    //cell1.MergeStyle(this.FilterStyle);
        //    this.searchBox = new TextBox();
        //    cell1.Controls.Add(this.searchBox);
        //    this.searchBox.ID = "txtSearch";
        //    //this.searchBox.CssClass = this.CSSFilterbox;
        //    //this.searchBox.SkinID = this.SkinIDFilterbox;
        //    this.searchBox.AutoPostBack = true;
        //    this.searchBox.TextChanged += new EventHandler(this.HandleFilterCommand);
        //    this.gvRowSearch.Cells.Add(cell1);
        //}

        #region Properties

        // PROPERTY:: AutoGenerateColumnSelect
        //[Category("Behavior")]
        [Category("Extended GridView Properties")]
        [Description("Whether a GenerateColumnSelect column is generated automatically at runtime")]
        [DefaultValue(GridView.AutoGenerateColumnType.None)]
        public AutoGenerateColumnType AutoGenerateColumnSelect
        {
            get
            {
                object o = _autoGenerateColumSelect;
                if (o == null)
                    return AutoGenerateColumnType.None;
                return (AutoGenerateColumnType)o;
            }
            set { _autoGenerateColumSelect = value; }
        }

        // PROPERTY:: ModelSelected
        //[Category("Behavior")]
        [Category("Extended GridView Properties")]
        [Description("Define a forma de seleção da linha")]
        [DefaultValue(GridView.ModelSelectType.ColumnSelected)]
        public ModelSelectType ModelSelect
        {
            get
            {
                object o = _modelSelected;
                if (o == null)
                    return ModelSelectType.ColumnSelected;
                return (ModelSelectType)o;
            }
            set { _modelSelected = value; }
        }

        // PROPERTY:: EnableMouseOver
        //[Category("Behavior")]
        [Category("Extended GridView Properties")]
        [Description("On/Of MouseOver")]
        [DefaultValue(true)]
        public bool EnableMouseOver
        {
            get
            {
                return _enableMouseOver;
            }
            set { _enableMouseOver = value; }
        }


        // PROPERTY:: GenerateColumnIndex
        //[Category("Behavior")]
        //[Description("Indicates the 0-based position of the GenerateColumnSelect")]
        //[DefaultValue(0)]
        ///Resolver limitação do JavaScript VerifyCheck de recusão para poder mudar  a posição do AutoGenerateColumnType
        [Browsable(false)]
        public int GenerateColumnSelectIndex
        {
            get
            {
                object o = _generateColumSelectIndex;
                if (o == null)
                    return 0;
                return (int)o;
            }
            set
            {
                _generateColumSelectIndex = (value < 0 ? 0 : value);
            }
        }

        // PROPERTY:: AutoGenerateColumnSelectCausePostBack
        //[Category("Behavior")]
        [Category("Extended GridView Properties")]
        [Description("it defines if the AutoGenerateColumnSelect field postback cause")]
        [DefaultValue(false)]
        public bool AutoGenerateColumnSelectAutoPostBack
        {
            get
            {
                object o = _autoGenerateColumSelectAutoPostBack;
                if (o == null)
                    return false;
                return (bool)o;
            }
            set
            {
                _autoGenerateColumSelectAutoPostBack = value;
                if (value)
                    this.AutoGenerateSelectButton = value;
            }
        }

        [Category("Extended GridView Properties"), Editor(typeof(ImageUrlEditor), typeof(UITypeEditor)), Description("Specifies path for the image used as an Ascending Sorting image")]
        public string AscImage
        {
            get
            {
                if (string.IsNullOrEmpty(this.ImageAsc))
                    return string.Empty;
                return this.ImageAsc;
            }
            set
            {
                this.ImageAsc = value;
            }
        }

        [Editor(typeof(ImageUrlEditor), typeof(UITypeEditor)), Category("Extended GridView Properties"), Description("Specifies path for the image used as a Descending Sorting image")]
        public string DescImage
        {
            get
            {
                if (string.IsNullOrEmpty(this.ImageDesc))
                    return string.Empty;
                return this.ImageDesc;
            }
            set
            {
                this.ImageDesc = value;
            }
        }

        //[Category("Behavior")]
        [Category("Extended GridView Properties")]
        [Description("it defines if the AutoGenerateColumnSelect field postback cause")]
        [DefaultValue("")]
        [TypeConverter(typeof(WebControlColection.WebControlCustomColectionConverter))]
        //[Editor(typeof(System.Web.UI.Design.WebControls.ListItemsCollectionEditor), typeof(WebControlCustomColection))]        
        //[TypeConverter("System.Web.UI.Design.DataColumnSelectionConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        public string WebControlCustomColection
        {
            get
            {
                //System.Web.UI.Design.DataColumnSelectionConverter
                //System.Web.UI.WebControls.DropDownList
                //System.Web.UI.Design.WebControls.ListItemsCollectionEditor
                if (_webControlCustomColection != null)
                    return _webControlCustomColection;
                else
                    return null;
            }
            set
            {
                _webControlCustomColection = value;
            }
        }

        // PROPERTY:: SelectedIndices
        [Browsable(false)]
        internal virtual List<int> getSelectedIndexs
        {
            get
            {
                //SelectedIndex = -1;
                _cachedSelectedIndices = new List<int>();
                string tipo = string.Empty;
                switch (this.AutoGenerateColumnSelect)
                {
                    case AutoGenerateColumnType.CheckBox:
                        tipo = SelectCheckBoxCustom.SelectCheckBoxCustomID;
                        for (int i = 0; i < Rows.Count; i++)
                        {
                            // Retrieve the reference to the checkbox
                            CheckBox cb = (CheckBox)Rows[i].FindControl(tipo);
                            if (cb == null)
                                return _cachedSelectedIndices;
                            if (cb.Checked)
                            {
                                //if (_cachedSelectedIndices.Count == 0)
                                //    SelectedIndex = i;
                                _cachedSelectedIndices.Add(i);
                            }
                        }
                        break;
                    case AutoGenerateColumnType.RadioButton:
                        tipo = SelectRadioButtonCustom.SelectRadioButtonCustomID;
                        for (int i = 0; i < Rows.Count; i++)
                        {
                            // Retrieve the reference to the checkbox
                            RadioButton cb = (RadioButton)Rows[i].FindControl(tipo);
                            if (cb == null)
                                return _cachedSelectedIndices;
                            if (cb.Checked)
                            {
                                //if (_cachedSelectedIndices.Count == 0)
                                //    SelectedIndex = i;
                                _cachedSelectedIndices.Add(i);
                            }
                        }
                        break;
                }
                return _cachedSelectedIndices;
            }
        }

        // METHOD:: GetSelectedIndices
        [Browsable(false)]
        [DefaultValue("")]
        [TypeConverter(typeof(IntArrayConverter))]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content),        
        //PersistenceMode(PersistenceMode.InnerDefaultProperty)
        //] 
        public virtual List<int> SelectedIndexs
        {
            get
            {
                //return (ArrayList)getSelectedIndexs.ToArray(typeof(int));
                return getSelectedIndexs;
            }
            set
            {
                _cachedSelectedIndices.Clear();
                for (int i = 0; i < value.Count; i++)
                {
                    if ((int)value[i] < -1)
                        throw new ArgumentOutOfRangeException("value");

                    if (!DesignMode)
                    {
                        if ((int)value[i] < Rows.Count)
                        {
                            GridViewRow r = Rows[(int)value[i]];
                            CheckBox cb = (CheckBox)r.FindControl(SelectCheckBoxCustom.SelectCheckBoxCustomID);
                            if (cb != null)
                                cb.Checked = true;
                        }
                        else
                            throw new ArgumentOutOfRangeException("value");
                    }
                    _cachedSelectedIndices.Add(value[i]);
                }
            }
        }

        //[Bindable(true), DefaultValue(-1), Description("GridView_SelectedIndex")]
        public override int SelectedIndex
        {
            get
            {
                List<int> i = SelectedIndexs;
                if (i.Count > 0)
                {
                    //SelectedIndex = (int)i[0];
                    return (int)i[0];
                }
                return base.SelectedIndex;
                //return -1;
            }
            set
            {
                //int[] selected = new int[1];
                //selected[0] = value;
                //_cachedSelectedIndices.Clear();
                //_cachedSelectedIndices.Add(value);
                clear();
                base.SelectedIndex = value;
            }
        }

        public List<Object> SelectedValues
        {
            get
            {
                List<Object> values = new List<object>();
                //Items Selecionados
                List<int> indexs = new List<int>(SelectedIndexs);
                //Necessario devido ao retorno do primeiro item selecionado do gris caso tenha algum selecioado
                ClearSelectedIndexs();
                foreach (int i in indexs)
                {
                    SelectedIndex = i;
                    values.Add(SelectedValue);
                }
                SelectedIndexs = indexs;
                return values;
            }
        }
        private void clear()
        {
            if (Page == null)
                return;
            _cachedSelectedIndices = new List<int>();
            string tipo = string.Empty;
            bool flagStatus = true;
            foreach (GridViewRow row in Rows)
            {
                CheckBox cb = null;
                switch (this.AutoGenerateColumnSelect)
                {
                    case AutoGenerateColumnType.CheckBox:
                        cb = (CheckBox)row.FindControl(SelectCheckBoxCustom.SelectCheckBoxCustomID);
                        break;
                    case AutoGenerateColumnType.RadioButton:
                        cb = (RadioButton)row.FindControl(SelectCheckBoxCustom.SelectCheckBoxCustomID);
                        break;
                }
                if (cb != null)
                    cb.Checked = false;
                if (flagStatus)
                    row.RowState = DataControlRowState.Normal;
                else
                    row.RowState = DataControlRowState.Alternate;
                flagStatus = !flagStatus;
            }
            //for (int i = 0; i < Rows.Count; i++)
            //{                        
            //    // Retrieve the reference to the checkbox
            //    CheckBox cb = (CheckBox)Rows[i].FindControl(tipo);
            //    if (cb != null)
            //        cb.Checked = false;
            //    Rows[i].RowState = DataControlRowState.Normal;
            //}
            //if (!Page.ClientScript.IsStartupScriptRegistered("ClearSelectedIdex_" + ClientID))
            //    Page.ClientScript.RegisterStartupScript(GetType(), "ClearSelectedIdex_" + ClientID, "document.getElementById('" + string.Format(_GRIDVIEWHEADERID, ClientID) + "').checked = false;", true);
            //break;

            //tipo = SelectRadioButtonCustom.SelectRadioButtonCustomID;
            //for (int i = 0; i < Rows.Count; i++)
            //{
            //    // Retrieve the reference to the checkbox
            //    if (cb != null)
            //        cb.Checked = false;
            //    Rows[i].RowState = DataControlRowState.Normal;
            //}
            //break;

        }
        public void ClearSelectedIndexs()
        {
            SelectedIndex = -1;
        }

        [Category("Extended GridView Properties"), DefaultValue(false), Description("Specifies whether to enable Grid Filter Textbox"), Bindable(true)]
        [Browsable(false)]
        public bool IsFiltered
        {
            get
            {
                return this.isFiltered;
            }
            set
            {
                this.isFiltered = value;
            }
        }

        [Category("Extended GridView Properties"), DefaultValue(false), Description("Formato de visualização inicia do filtro"), Bindable(true)]
        [Browsable(false)]
        public bool ExpandedFiltered
        {
            get
            {
                return this.expandedFiltered;
            }
            set
            {
                this.expandedFiltered = value;
            }
        }
        //[Category("Extended GridView Properties"), DefaultValue(null), Description("Defini o campo que possui PopUp"), Bindable(true)]
        //[Browsable(false)]
        //public List<PopUpColum> PopupColum
        //{
        //    get
        //    {
        //        return _popupColum;
        //    }
        //    set
        //    {
        //        _popupColum = value;
        //    }
        //}

        #endregion


        #region Members overrides

        protected override void OnInit(EventArgs e)
        {
            this.Page.RegisterRequiresControlState(this);
            base.OnInit(e);
            //if (this.IsFiltered)
            //    this.CreateCustomHeader();
        }

        // METHOD:: CreateColumns
        protected override ICollection CreateColumns(PagedDataSource dataSource, bool useDataSource)
        {
            // Let the GridView create the default set of columns
            ICollection columnList = base.CreateColumns(dataSource, useDataSource);
            // Add a checkbox/radiobutom column if required            
            ArrayList list = new ArrayList(columnList);
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].GetType() == typeof(CommandField))
                {
                    if (((CommandField)list[i]).ShowSelectButton == true)
                        ((CommandField)list[i]).Visible = false;
                }
                else if (list[i] is BoundField)
                {
                    BoundField bfd = (BoundField)list[i];
                    bfd.SortExpression = bfd.DataField.ToString();
                }
            }
            ArrayList extendedColumnList = null;
            switch (AutoGenerateColumnSelect)
            {
                case AutoGenerateColumnType.CheckBox:
                    extendedColumnList = AddCheckBoxColumn(list);
                    return extendedColumnList;
                case AutoGenerateColumnType.RadioButton:
                    extendedColumnList = AddRadioButtonColumn(list);
                    return extendedColumnList;
            }
            return list;
        }

        protected override void OnRowCreated(GridViewRowEventArgs e)
        {
            base.OnRowCreated(e);
            if (e.Row == null)
                return;
            switch (e.Row.RowType)
            {
                case DataControlRowType.Header:
                    foreach (TableCell cell1 in e.Row.Cells)
                    {
                        if (cell1.HasControls())
                        {
                            LinkButton button1 = cell1.Controls[0] as LinkButton;
                            if (button1 != null)
                            {
                                System.Web.UI.WebControls.Image image1 = new System.Web.UI.WebControls.Image();
                                image1.ImageUrl = "default.gif";
                                if (this.SortExpression == button1.CommandArgument)
                                {
                                    if (this.SortDirection == SortDirection.Ascending)
                                        image1.ImageUrl = this.AscImage;
                                    else
                                        image1.ImageUrl = this.DescImage;
                                }
                                if (!image1.ImageUrl.Equals("default.gif"))
                                {
                                    cell1.Controls.Add(new LiteralControl("&nbsp;"));
                                    cell1.Controls.Add(image1);
                                    cell1.Controls.Add(new LiteralControl("&nbsp;"));
                                }
                            }
                        }
                    }
                    //if (this.IsFiltered)
                    //    this.AddCustomHeader();
                    break;
                case DataControlRowType.DataRow:
                    //if (PopupColum != -1 && PopupColum <= e.Row.Cells.Count)
                    //{
                    //    TableCell cell = e.Row.Cells[PopupColum];
                    //    cell.Attributes["onmouseover"] = "showPopUp('" + ClientID + _POPUP + "', true)";
                    //    cell.Attributes["onmouseout"] = "showPopUp('" + ClientID + _POPUP + "', false)";
                    //}
                    break;
            }
        }

        // METHOD:: OnLoad
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        // METHOD:: OnPreRender
        protected override void OnPreRender(EventArgs e)
        {
            #region Regitro de ClientScript
            string keyCss_Grid = "CssGridViewCustom";
            Type t = GetType();
            string path = util.configEstructe(Page);
            util.registerScript(Page, _KEY_SCRIPT_GRID, _GRIDVIEW_JS, GridViewResource.HotGridView);
            util.registerScriptUtil(Page);
            if (!Page.ClientScript.IsClientScriptIncludeRegistered(keyCss_Grid) && ModelSelect == ModelSelectType.RowSelected)
            {
                string pathRelativity = Path.Combine(util.configPath("/" + Page.Request.Path.Split('/')[1], false), _RESOURCE_CSS);
                path = Path.Combine(path, _RESOURCE_CSS);
                if (!System.IO.File.Exists(path))
                    util.writeScript(GridViewResource.css, path);
                else
                {
                    StreamReader scriptRead = new StreamReader(path);
                    if (scriptRead.ReadToEnd() != GridViewResource.css)
                    {
                        scriptRead.Close();
                        util.writeScript(GridViewResource.css, path);
                    }
                    else
                        scriptRead.Close();
                }
                Page.ClientScript.RegisterClientScriptBlock(t, keyCss_Grid, "<link href=" + pathRelativity + " type=\"text/css\" rel=\"stylesheet\" />");
            }

            Page.ClientScript.RegisterHiddenField("__EVENTTARGET", "");
            Page.ClientScript.RegisterHiddenField("__EVENTARGUMENT", "");
            //O formato de uma pagina simples é var theForm = document.forms['form1'] ...
            Page.ClientScript.RegisterClientScriptBlock(t, "", @"var theForm = document.forms['aspnetForm'];
if (!theForm)
    theForm = document.aspnetForm;
if (!theForm)
    theForm = document.forms['form1'];
if (!theForm)
    theForm = document.form1;    
var flagAutoPostBack = false;
var flagInput = false;", true);
            Page.ClientScript.RegisterClientScriptBlock(t, ClientID + _THEMA,
                string.Format(@"var {0}" + _THEMA + " = new gridThema('{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}');",
                ClientID, ColorTranslator.ToHtml(RowStyle.ForeColor), ColorTranslator.ToHtml(RowStyle.BackColor), RowStyle.Font.Bold ? 700 : 400,
               ColorTranslator.ToHtml(SelectedRowStyle.ForeColor), ColorTranslator.ToHtml(SelectedRowStyle.BackColor),
               SelectedRowStyle.Font.Bold ? 700 : 400, ColorTranslator.ToHtml(AlternatingRowStyle.ForeColor),
               ColorTranslator.ToHtml(AlternatingRowStyle.BackColor), AlternatingRowStyle.Font.Bold ? 700 : 400), true);
            //if (PopupColum != -1 && !Page.ClientScript.IsClientScriptBlockRegistered(t, ClientID + _POPUP))
            //    Page.ClientScript.RegisterClientScriptBlock(t, ClientID + _POPUP, "<div id='" + ClientID + _POPUP + "' style='position:absolute; visibility:hidden;'>ujfutfu</div>");

            if (!string.IsNullOrEmpty(WebControlCustomColection))
            {
                if (!Page.ClientScript.IsClientScriptBlockRegistered(WebControlCustomColection))
                {
                    WebControlColection.WebControlCustomColection colection = (WebControlColection.WebControlCustomColection)util.ObterControle<WebControlColection.WebControlCustomColection>(Page.Controls, WebControlCustomColection);
                    if (colection != null)
                    {
                        int selecionado = SelectedIndexs.Count;
                        foreach (WebControlColection.WebControlCustom i in colection.WebControl)
                        {
                            Control control = util.ObterControle<WebControl>(Page.Controls, i.Text);
                            if (control != null)
                            {
                                if (i.Enable)
                                {
                                    //if ( selecionado == 0)
                                    //    ((WebControl)control).Enabled = false;
                                    _controlEnable.Add(control.ClientID);
                                }
                                if (i.Visible)
                                {
                                    _controlVisible.Add(control.ClientID);
                                }
                            }
                        }
                        Page.ClientScript.RegisterClientScriptBlock(t, WebControlCustomColection, "var " + WebControlCustomColection + _COLECTION + " = new webControlCustom(['" + string.Join(",", _controlEnable.ToArray()).Replace(",", "','") + "'],['" + string.Join("','", _controlVisible.ToArray()) + "'],'"+ WebControlCustomColection +"');", true);
                        if (selecionado == 0)// && SelectedIndex == -1)
                        {
                            Page.ClientScript.RegisterStartupScript(t, WebControlCustomColection + _START + "Enable", "EnableItems(" + WebControlCustomColection + _COLECTION + ".enable,false);", true);
                            Page.ClientScript.RegisterStartupScript(t, WebControlCustomColection + _START + "Visible", "VisibleItems(" + WebControlCustomColection + _COLECTION + ".visible,false);", true);
                        }
                        //else
                        //{
                        //    Page.ClientScript.RegisterStartupScript(t, WebControlCustomColection + _START + "Enable", "EnableItems(" + WebControlCustomColection + _COLECTION + ".enable,true);", true);
                        //    Page.ClientScript.RegisterStartupScript(t, WebControlCustomColection + _START + "Visible", "VisibleItems(" + WebControlCustomColection + _COLECTION + ".visible,true);", true);
                        //}
                    }
                }
                if (!Page.ClientScript.IsStartupScriptRegistered("verifyCheckGrids" + ClientID))
                {

                    //Page.ClientScript.RegisterStartupScript(t, ID + _START + "verifyCheck", string.Format("VerifyCheckGrid('{0}',true,{1});", ClientID, ClientID + _THEMA), true);
                    Page.ClientScript.RegisterStartupScript(t, "verifyCheckGrids" + ClientID, string.Format("controlGrids = new gridCheck('{0}',{1},{2});", ClientID, ClientID + _THEMA, WebControlCustomColection + _COLECTION), true);
                    //Page.ClientScript.RegisterStartupScript(t, "uyfufu", "setInterval(teste,5,null);", true);
                    //Page.ClientScript.RegisterStartupScript(t, "ttttttt", "document.onunload = alert('fffff');", true);
                    //    RegisterClientScriptBlock(t, "ttttttt", "window.onclick += alert('tt7654654654t');", true);
                    //Page.ClientScript.RegisterStartupScript(t, "iiiii", "document.onbeforeunload = teste()", true);
                    //document.onbeforeunload 
                }
            }                        

            #endregion Regitro de ClientScript

            // Do as usual
            base.OnPreRender(e);

            // Adjust each data row
            // Build the ID of the GridView in the header
            string gridviewheaderID = String.Format(_GRIDVIEWHEADERID, ClientID);
            int selectIndex = this.SelectedIndex;
            foreach (GridViewRow r in Rows)
            {
                // Get the appropriate style object for the row
                TableItemStyle style = GetRowStyleFromState(r.RowState);
                string alternat = r.RowState.ToString().Split(',')[0].Trim() == "Alternate" ? "true" : "false";
                r.Attributes["onclick"] = String.Format("ApplyStyle(this, {0} , '{1}', {2}, '{3}', {4}, {5}, {6})",
                    ClientID + _THEMA, gridviewheaderID, string.IsNullOrEmpty(WebControlCustomColection) ? "null" : WebControlCustomColection + _COLECTION, r.RowIndex,
                            AutoGenerateColumnSelectAutoPostBack.ToString().ToLower(), alternat,
                            (ModelSelect == ModelSelectType.RowSelected || ModelSelect == ModelSelectType.ColumnAndRowSelected) ? "true" : "false");
                if (EnableMouseOver)
                {
                    r.Attributes["onmouseover"] = string.Format("ConfigThema(this,true,{0},{1},'mouseover',{2})", ClientID + _THEMA, alternat, string.IsNullOrEmpty(WebControlCustomColection) ? "null" : WebControlCustomColection + _COLECTION);
                    r.Attributes["onmouseout"] = string.Format("ConfigThema(this,false,{0},{1},'mouseout'),{2}", ClientID + _THEMA, alternat, string.IsNullOrEmpty(WebControlCustomColection) ? "null" : WebControlCustomColection + _COLECTION);
                }                
                switch (this.AutoGenerateColumnSelect)
                {
                    case AutoGenerateColumnType.CheckBox:
                        // Retrieve the reference to the checkbox
                        CheckBox cb = (CheckBox)r.FindControl(SelectCheckBoxCustom.SelectCheckBoxCustomID);
                        if (cb != null)
                        {
                            cb.Attributes["onclick"] = "FlagInput()";
                            // Update the style of the checkbox if checked
                            if (cb.Checked || (this.SelectedIndex == r.RowIndex))// && !Page.IsPostBack))
                            {
                                cb.Checked = true;
                                r.BackColor = SelectedRowStyle.BackColor;
                                r.ForeColor = SelectedRowStyle.ForeColor;
                                r.Font.Bold = SelectedRowStyle.Font.Bold;
                            }
                            else
                            {
                                if (r.RowState.ToString().Split(',')[0].Trim() == "Alternate")
                                {
                                    r.BackColor = AlternatingRowStyle.BackColor;
                                    r.ForeColor = AlternatingRowStyle.ForeColor;
                                    r.Font.Bold = AlternatingRowStyle.Font.Bold;
                                }
                                else
                                {
                                    r.BackColor = RowStyle.BackColor;
                                    r.ForeColor = RowStyle.ForeColor;
                                    r.Font.Bold = RowStyle.Font.Bold;
                                }
                            }
                        }
                        break;
                    case AutoGenerateColumnType.RadioButton:
                        // Retrieve the reference to the radiobutton
                        RadioButton rbn = (RadioButton)r.FindControl(SelectRadioButtonCustom.SelectRadioButtonCustomID);
                        if (rbn != null)
                        {
                            rbn.InputAttributes.Add("onclick", "FlagInput()");
                            if (rbn.Checked || (this.SelectedIndex == r.RowIndex))// && !Page.IsPostBack))
                            {
                                rbn.Checked = true;
                                r.BackColor = SelectedRowStyle.BackColor;
                                r.ForeColor = SelectedRowStyle.ForeColor;
                                r.Font.Bold = SelectedRowStyle.Font.Bold;
                            }
                            else
                            {
                                if (r.RowState.ToString().Split(',')[0].Trim() == "Alternate")
                                {
                                    r.BackColor = AlternatingRowStyle.BackColor;
                                    r.ForeColor = AlternatingRowStyle.ForeColor;
                                    r.Font.Bold = AlternatingRowStyle.Font.Bold;
                                }
                                else
                                {
                                    r.BackColor = RowStyle.BackColor;
                                    r.ForeColor = RowStyle.ForeColor;
                                    r.Font.Bold = RowStyle.Font.Bold;
                                }
                            }
                        }
                        break;                    
                }
            }
        }
        #endregion
    }
}
