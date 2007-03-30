using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Collections;
using System.Xml;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using WPC.Common.EDI.EDIObjects;
using WPC.Common.EDI.EDIRegex;
using System.Text;

namespace WPC.OpenSource.EDIEditControl
{
	/// <summary>
	/// Rich Text Box editor with specific functionality for EDI highlighting and editing
	/// </summary>
	public class Editor : System.Windows.Forms.RichTextBox
	{        

        #region externs
        /// <summary>
        /// Forces a control to not update while changing it's appearance
        /// </summary>
        /// <param name="hWndLock">handle of control to lock</param>
        /// <returns>success or failure</returns>
        [DllImport("user32.dll")]
        public static extern bool LockWindowUpdate(IntPtr hWndLock);
        #endregion        

        #region private vars
        private System.ComponentModel.Container components = null;       
        
        private frmSearch _searchForm;

        /// <summary>
        /// A collection of token/color pairs that are to be used to colorize the text
        /// </summary>
        private Hashtable _tokens = new Hashtable();

        /// <summary>
        /// The segment delimiter used in parsing the file (pulled from byte 106 of the file)
        /// </summary>
        private string _segDelim = string.Empty;

        /// <summary>
        /// The element delimiter used in parsing the file (pulled from byte 4 of the file)
        /// </summary>
        private string _elemDelim = string.Empty;      
  
//        /// <summary>
//        /// Internal property to store if searches should be case sensitive
//        /// </summary>
//        private bool _matchCase = false;
//
//        private bool _searchUp = false;
//        private string _searchString = string.Empty;        
        
        // See corresponding public accessors for descriptions
        private bool _needsSave = false;
        private bool _ignoreLF = false;
        private bool _ignoreCR = false;
        private string _syntaxFile = string.Empty;
        private bool _wrapOnSegmentDelimiter = false;
        private string _filename = string.Empty;   
        #endregion
        
        #region public accessors
        /// <summary>
        /// Filename of  xml file containing token/color pairs (if empty at load, filename 
        /// will default to color.xml in application startup directory)
        /// </summary>
        public string SyntaxFile
        {
            get
            {
                return _syntaxFile;      
            }
            set
            {
                _syntaxFile = value;
            }
        }

        /// <summary>
        /// FileName used when saving this file
        /// </summary>
        public string FileName
        {
            get
            {   
                return _filename;
            }

            set
            {
                _filename = value;
            }
        }

        /// <summary>
        /// if true, will replace segment terminators with \n
        /// </summary>
        public bool WrapOnSegmentDelimiter
        {
            get
            {
                return _wrapOnSegmentDelimiter;
            }
            set
            {
                _wrapOnSegmentDelimiter = value;
            }
        }

        /// <summary>
        /// Set to true if the contents have been altered
        /// </summary>
        public bool NeedsSave
        {
            get
            {                
                return _needsSave;
            }
            set
            {
                _needsSave = value;
            }
        }   
    
        /// <summary>
        /// Set to true if the contents have extra CR'sbeen altered
        /// </summary>
        public bool IgnoreCR
        {
            get
            {
                return this._ignoreCR;
            }
        }   

        /// <summary>
        /// Set to true if the contents have extra LF'sbeen altered
        /// </summary>
        public bool IgnoreLF
        {
            get
            {
                return this._ignoreLF;
            }
        }   


        #endregion

        #region constructors/destructors
        /// <summary>
        /// Default constructor
        /// </summary>        
		public Editor()
		{
            this.WordWrap = false;
            this.ScrollBars = RichTextBoxScrollBars.Both;        
            this.HideSelection = false;
 
            _searchForm = new frmSearch();
            _searchForm.Target = this;

            BuildContextMenu();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if( components != null )
					components.Dispose();
			}
			base.Dispose( disposing );
		}
        #endregion

        #region public methods

        /// <summary>
        /// Inseri o texto depois da posição do cursor.
        /// </summary>
        /// <param name="Texto">The texto.</param>
        public void InsertText(string Texto)
        {
            InsertText(Texto, this.SelectionStart, true);
        }

        /// <summary>
        /// Insere o texto na posição informada podendo ou não alterar a posição do cursor
        /// </summary>
        /// <param name="Texto">The texto.</param>
        /// <param name="ModificarPosicaoCursor">Setar a posição do cursor logo após o texto adicionado</param>
        /// <param name="startIndex">a partir de qual posição deve ser adicionada</param>
        public void InsertText(string Texto, int startIndex, bool ModificarPosicaoCursor)
        {
            if (startIndex < 0)
                throw new IndexOutOfRangeException("a posição inicial deve ser maior que zero");
            int posAtual = startIndex;
            if ( startIndex == 0 )
            {
                base.Text = Texto + base.Text;                
            }
            else 
            {
                StringBuilder inicio = new StringBuilder(base.Text.Substring(0, startIndex));
                StringBuilder fim = new StringBuilder();
                if (startIndex < Text.Length)
                {
                    fim = new StringBuilder(base.Text.Substring(startIndex + 1));
                }
                Text = string.Format("{0}{1}{2}", inicio, Texto, fim);
            }
            if ( ModificarPosicaoCursor )
                this.SelectionStart = posAtual + Texto.Length;
        }

        /// <summary>
        /// Using the provided string, locates 'data' in the text control and sets to highlight
        /// </summary>
        /// <param name="data">String to search for</param>
        /// <param name="startFromTop">If true, start looking from 0, if not, then start from cursor location</param>
        public void SetHighlightByString(string data,bool startFromTop)
        {            
            try
            {
                if (data != string.Empty && data != null)
                {
                    int length = data.Length;
                    // data = data.Replace(this._elemDelim,EDI_Helpers.DelimiterToHex(_elemDelim));
                    data = EDI_Helpers.StringToHex(data);
                    Regex r = new Regex(data, SegmentRegex.regExpOptions);
                    int iStart = 0;
                    if (!startFromTop)
                        iStart = this.SelectionStart;

                    MatchCollection mc = r.Matches(this.Text, iStart);
                    if (mc.Count > 0)
                    {
                        this.SelectionStart = mc[0].Index;
                        this.SelectionLength = length;
                    }
                }
            }
            catch(System.Exception ex)
            {
                MessageBox.Show(this,"Error highlighting string. Message: " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Displays a search dialog and then performs the search in current (selected) text
        /// </summary>
        /// <param name="ShowDialog">If true, force the showing of the Find dialog</param>
        public void Search(bool ShowDialog)
        {
            try
            {
                // show the dialog if told to OR if _searchString is empty
                if (ShowDialog || _searchForm.SearchString == string.Empty)
                {
                    _searchForm.ShowDialog(this);
//                    RichTextBoxFinds options = new RichTextBoxFinds();
//
//                    while (_searchForm.ShowDialog(this) == DialogResult.OK)
//                    {
//                        if (_searchForm.SearchString != string.Empty)
//                        {                                                        
//                            if (_searchForm.MatchCase)
//                                options = options & RichTextBoxFinds.MatchCase;
//
//                            if (_searchForm.SearchUp)
//                                options = options & RichTextBoxFinds.Reverse;
//
//                            this.Find(_searchForm.SearchString, this.SelectionStart + 1,options);
//                        }                                
//                    }
                }
                else // caller says don't show dialog and we have a search string
                {
                    RichTextBoxFinds options = new RichTextBoxFinds();
                        
                    if (_searchForm.MatchCase)
                        options = options & RichTextBoxFinds.MatchCase;

                    if (_searchForm.SearchUp)
                        options = options & RichTextBoxFinds.Reverse;

                    this.Find(_searchForm.SearchString, this.SelectionStart + 1,options);
                }
            }
            catch(System.Exception ex)
            {
                MessageBox.Show(this,"Error performing search. Message: " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }           
        } 
 
        /// <summary>
        /// Evaluates the contained text, gets the delimiters and then colorizes the contents
        /// </summary>
        public void UpdateAppearance()
        {
            bool bNeedsSaveStill = this._needsSave;
            this.Cursor = Cursors.WaitCursor;
            // lock updates while we colorize
            LockWindowUpdate(this.Handle);  
          
            try
            {
                string isaSource = this.Text;
                if( WPC.Common.EDIProcessors.EDIPreProcessor.ValidateISA(ref isaSource).Count == 0)
                {
                    // save this cause we're about to change the text...                    

                    EDI_Helpers.RetrieveDelimiters(isaSource.Substring(0,106), out this._segDelim, out this._elemDelim, false);                
                    
                    switch (_segDelim)
                    {
                        case "\r":
                            this._ignoreLF = true;
                            break;

                        case "\n":
                            this._ignoreCR = true;
                            break;

                        default:
                            this._ignoreCR = true;
                            this._ignoreLF = true;
                            break;
                    }                       

                
                    

                    // if delim is not linefeed or CR, add return to each line EXCEPT the last.
                    if (this._segDelim != "\r" && this._segDelim != "\n" && this._wrapOnSegmentDelimiter)
                    {
                        string data = this.Text;
                        data = data.Replace(this._segDelim, this._segDelim + "\n");
                        data = data.Substring(0,data.Length - 1);
                        // now make sure we're not recursively adding extra linefeeds
                        if( data.IndexOf("\n\n") != -1)
                        {
                            while (data.IndexOf("\n\n") != -1)
                                data = data.Replace("\n\n","\n");
                        }

                        this.Text = data;
                    }

                    Colorize();                    
                }              
            }
            catch(System.Exception ex)
            {
                MessageBox.Show(this,"Error colorizing text. Message: " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.NeedsSave = bNeedsSaveStill;

                // enable display updates now...
                LockWindowUpdate(IntPtr.Zero);
                this.Cursor = Cursors.IBeam;
                this.ClearUndo();
            }
        }

        
        #endregion   

        #region File Handling Methods
        /// <summary>
        /// Save current data to file using the current <see cref="FileName">FileName</see> poperty value
        /// </summary>
        public void SaveFile()
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(this._filename);

                if (_wrapOnSegmentDelimiter)
                    Text.Replace(Environment.NewLine,this._segDelim); //str to char test

                sw.Write(Text);
                NeedsSave = false;
            }
            catch(System.Exception ex)
            {
                MessageBox.Show(this,"Error saving file. Message: " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sw != null)
                    sw.Close();
            }
        }

        /// <summary>
        /// Save current data to new filename
        /// </summary>
        /// <param name="filename"></param>
        public void SaveFileAs(string filename)
        {
            _filename = filename;
            SaveFile();
        }

        /// <summary>
        /// Opens an existing transaction file and populates the object
        /// </summary>
        /// <param name="filename"></param>
        public void Openfile(string filename)
        {
            if (!File.Exists(filename))
                throw new Exception("Cannot find file: " + filename + ". Unable to open");

            StreamReader sr = new StreamReader(filename);
            this._filename = filename;
            
            try
            {
                // Load text  
                Text = sr.ReadToEnd();
                UpdateAppearance();
            }
            catch(System.Exception ex)
            {
                MessageBox.Show(this,"Error opening file. Message: " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sr.Close();  
            }
        }

        #endregion

        #region overrides
        /// <summary>
        /// On keypress, set NeedsSave to true
        /// </summary>
        /// <param name="e">KeyEventArgs</param>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            this._needsSave = true;
            base.OnKeyUp (e);
        }


        #endregion overrides

        #region private methods
        /// <summary>
        /// gather token/color pairs and colorize loaded text
        /// </summary>
        private void Colorize()
        {
            ResetColors();

            // if we haven't already loaded the tokens...
            if (_tokens.Count == 0)
                LoadTokens(); 
            
            ColorConverter cc = new ColorConverter();
            string segD  = EDI_Helpers.DelimiterToHex(this._segDelim);
            string elemD  = EDI_Helpers.DelimiterToHex(this._elemDelim);

            try
            {
                // for each token, colorize if needed
                foreach (DictionaryEntry d in _tokens)
                {
                    string token = d.Key.ToString();
                    string color = d.Value.ToString();
                    string regex = string.Empty;

                    // adding this if statement simplifies the regex string later a great deal.
                    if (token.ToLower() == "isa")
                    {
                        this.SelectionStart = 0;
                        this.SelectionLength = 3;
                        this.SelectionColor = (Color) cc.ConvertFromString(color);
                        this.SelectionFont = new Font(this.SelectionFont, FontStyle.Bold);
                    }
                    else
                    {
                        regex = "((" + segD + "\\s" + ")|" + segD + ")" + token + elemD;

                        Regex r = new Regex(regex,SegmentRegex.regExpOptions);   
                        MatchCollection mc = r.Matches(this.Text);
                        if (mc.Count > 0)
                            HightlightTokens(mc, color);
                    }
                }
            }
            finally
            {
                // set the default to black, and put the cursor at the beginning of the text box
                this.SelectionStart = 0;
                this.SelectionLength = 0;
                this.SelectionColor = (Color) cc.ConvertFromString("black");
                this.SelectionFont = new Font(this.Font, FontStyle.Regular);
            }
        }

        /// <summary>
        /// Sets each 'match' in match collection to a specific color (and makes it bold)
        /// </summary>
        /// <param name="matches">MatchCollection of strings to highlight</param>
        /// <param name="color">color to use</param>
        private void HightlightTokens(MatchCollection matches, string color)
        {
            ColorConverter cc = new ColorConverter();

            try
            {
                foreach (Match m in matches)
                {
                    this.SelectionStart = m.Index + 1;
                    this.SelectionLength = m.Length - 1;
                    this.SelectionColor = (Color) cc.ConvertFromString(color);
                    this.SelectionFont = new Font(this.SelectionFont, FontStyle.Bold);
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// Open Syntax file, and load token/color pairs
        /// </summary>
        private void LoadTokens()
        {
            try
            {
                XmlDocument dom = new XmlDocument();
                if(_syntaxFile == string.Empty)
                    _syntaxFile = Application.StartupPath + "\\color.xml";

                dom.Load(_syntaxFile);

                foreach(XmlNode n in dom.SelectNodes("//tokens/token"))
                {
                    // only worry about changes, not defaults
                    if (n.Attributes["color"].Value.ToLower() != "black")
                        _tokens.Add(n.Attributes["name"].Value, n.Attributes["color"].Value);
                }
            }
            catch(System.Exception ex)
            {
                throw new Exception("Error Loading syntax file: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Set selection to beginning of file, and colors to defaults.
        /// </summary>
        private void ResetColors()
        {
            ColorConverter cc = new ColorConverter();
            this.SelectionStart = 0;
            this.SelectionLength = this.Text.Length;
            this.SelectionColor = (Color) cc.ConvertFromString("black");
            this.SelectionFont = new Font(this.Font, FontStyle.Regular);
        }

        private void BuildContextMenu()
        {
            this.ContextMenu = new ContextMenu();

            MenuItem m = new MenuItem("&Undo");
            this.ContextMenu.MenuItems.Add(m);
            m.Click += new System.EventHandler(this.ContextMenuHandler);

            m = new MenuItem("&Redo");
            this.ContextMenu.MenuItems.Add(m);
            m.Click += new System.EventHandler(this.ContextMenuHandler);
            
            m = new MenuItem("-");
            this.ContextMenu.MenuItems.Add(m);

            m = new MenuItem("Cu&t");
            this.ContextMenu.MenuItems.Add(m);
            m.Click += new System.EventHandler(this.ContextMenuHandler);

            m = new MenuItem("&Copy");
            this.ContextMenu.MenuItems.Add(m);
            m.Click += new System.EventHandler(this.ContextMenuHandler);

            m = new MenuItem("&Paste");
            this.ContextMenu.MenuItems.Add(m);
            m.Click += new System.EventHandler(this.ContextMenuHandler);

            m = new MenuItem("&Delete");
            this.ContextMenu.MenuItems.Add(m);
            m.Click += new System.EventHandler(this.ContextMenuHandler);

            m = new MenuItem("-");
            this.ContextMenu.MenuItems.Add(m);

            m = new MenuItem("&Select All");
            this.ContextMenu.MenuItems.Add(m);
            m.Click += new System.EventHandler(this.ContextMenuHandler);

            m = new MenuItem("-");
            this.ContextMenu.MenuItems.Add(m);

            m = new MenuItem("&Find...");
            this.ContextMenu.MenuItems.Add(m);
            m.Click += new System.EventHandler(this.ContextMenuHandler);

            m = new MenuItem("Find A&gain");
            this.ContextMenu.MenuItems.Add(m);
            m.Click += new System.EventHandler(this.ContextMenuHandler);
        }

        private void ContextMenuHandler(object sender, System.EventArgs e)
        {
            MenuItem menu = (MenuItem) sender;
            switch (menu.Text)
            {
                case "&Undo":
                    this.Undo();
                    break;

                case "&Redo":
                    this.Redo();
                    break;

                case "Cu&t":
                    this.Cut();
                    break;

                case "&Copy":
                    this.Copy();
                    break;

                case "&Paste":
                    this.Paste();
                    break;

                case "&Delete":
                    if (this.SelectedText == string.Empty)
                        this.SelectionLength = 1;

                    this.SelectedText = string.Empty;
                    break;

                case "&Select All":
                    this.SelectAll();
                    break;

                case "&Find...":
                    this.Search(true);
                    break;

                case "Find A&gain":
                    this.Search(false);
                    break;
            }
        }
        #endregion
	}
}
