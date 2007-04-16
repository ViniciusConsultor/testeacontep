using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace WPC.OpenSource.EDIEditControl
{
	/// <summary>
	/// Search form for user to specify search parameters (modeled after Notepad search dialog)
	/// </summary>
	public class frmSearch : System.Windows.Forms.Form
	{
        #region private vars
		private System.ComponentModel.Container components = null;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton_Up;
        private System.Windows.Forms.RadioButton radioButton_Down;
        private System.Windows.Forms.CheckBox checkBox_MatchCase;
        private System.Windows.Forms.Button button_Find;
        private System.Windows.Forms.TextBox txtSearchString;
        private Editor _target;
        #endregion

        #region public vars
        /// <summary>
        /// String entered by user
        /// </summary>
        public string SearchString
        {
            get
            {
                return txtSearchString.Text;
            }
            set
            {
                txtSearchString.Text = value;
            }
        }

        /// <summary>
        /// If true, then a reverse order search should be performed
        /// </summary>
        public bool SearchUp
        {
            get
            {
                return this.radioButton_Up.Checked;
            }
        }

        /// <summary>
        /// If true, then search should be case sensetive
        /// </summary>
        public bool MatchCase
        {
            get
            {
                return this.checkBox_MatchCase.Checked;
            }
        }

        /// <summary>
        /// Editor to perform searches in
        /// </summary>
        public Editor Target
        {
            get
            {
                return _target;
            }
            set
            {
                _target = value;
            }
        }
        #endregion

        #region constructors/destructors
        /// <summary>
        /// Search form
        /// </summary>
		public frmSearch()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

        #endregion

        #region event handlers
        private void txtSearchString_TextChanged(object sender, System.EventArgs e)
        {
            this.button_Find.Enabled = (this.txtSearchString.Text != string.Empty);
        }

        private void button_Find_Click(object sender, System.EventArgs e)
        {
            Search();
        }

        #endregion

        #region private methods
        private void Search()
        {
            try
            {
                RichTextBoxFinds options = new RichTextBoxFinds();
                int iStart = 0;
                                                
                if (MatchCase)
                    options = RichTextBoxFinds.MatchCase;

                if (SearchUp)
                {
                    options = options | RichTextBoxFinds.Reverse;
                    iStart = _target.SelectionStart - 1;
                }
                else
                    iStart = _target.SelectionStart + _target.SelectionLength;

                int ret = _target.Find(SearchString, iStart,options);

                if (ret < 0)
                    MessageBox.Show(this, "String not found.","Not found",MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(System.Exception ex)
            {
                MessageBox.Show(this,"Error performing search. Message: " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }           
        } 
        #endregion private methods

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_Find = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSearchString = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton_Up = new System.Windows.Forms.RadioButton();
            this.radioButton_Down = new System.Windows.Forms.RadioButton();
            this.checkBox_MatchCase = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_Find
            // 
            this.button_Find.Enabled = false;
            this.button_Find.Location = new System.Drawing.Point(264, 8);
            this.button_Find.Name = "button_Find";
            this.button_Find.Size = new System.Drawing.Size(75, 23);
            this.button_Find.TabIndex = 4;
            this.button_Find.Text = "&Find";
            this.button_Find.Click += new System.EventHandler(this.button_Find_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Cancel.Location = new System.Drawing.Point(264, 40);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 5;
            this.button_Cancel.Text = "&Cancel";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "Fi&nd what:";
            // 
            // txtSearchString
            // 
            this.txtSearchString.Location = new System.Drawing.Point(72, 12);
            this.txtSearchString.Name = "txtSearchString";
            this.txtSearchString.Size = new System.Drawing.Size(184, 20);
            this.txtSearchString.TabIndex = 0;
            this.txtSearchString.TextChanged += new System.EventHandler(this.txtSearchString_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton_Up);
            this.groupBox1.Controls.Add(this.radioButton_Down);
            this.groupBox1.Location = new System.Drawing.Point(152, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(104, 40);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Direction";
            this.groupBox1.Visible = false;
            // 
            // radioButton_Up
            // 
            this.radioButton_Up.Location = new System.Drawing.Point(8, 16);
            this.radioButton_Up.Name = "radioButton_Up";
            this.radioButton_Up.Size = new System.Drawing.Size(40, 16);
            this.radioButton_Up.TabIndex = 2;
            this.radioButton_Up.Text = "&Up";
            // 
            // radioButton_Down
            // 
            this.radioButton_Down.Checked = true;
            this.radioButton_Down.Location = new System.Drawing.Point(48, 16);
            this.radioButton_Down.Name = "radioButton_Down";
            this.radioButton_Down.Size = new System.Drawing.Size(53, 16);
            this.radioButton_Down.TabIndex = 3;
            this.radioButton_Down.TabStop = true;
            this.radioButton_Down.Text = "&Down";
            // 
            // checkBox_MatchCase
            // 
            this.checkBox_MatchCase.Location = new System.Drawing.Point(8, 56);
            this.checkBox_MatchCase.Name = "checkBox_MatchCase";
            this.checkBox_MatchCase.Size = new System.Drawing.Size(104, 24);
            this.checkBox_MatchCase.TabIndex = 1;
            this.checkBox_MatchCase.Text = "&Match Case";
            // 
            // frmSearch
            // 
            this.AcceptButton = this.button_Find;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.button_Cancel;
            this.ClientSize = new System.Drawing.Size(344, 93);
            this.Controls.Add(this.checkBox_MatchCase);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtSearchString);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_Find);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Find";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
	}
}
