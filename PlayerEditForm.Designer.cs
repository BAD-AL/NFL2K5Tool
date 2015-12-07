namespace NFL2K5Tool
{
    partial class PlayerEditForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.fname = new System.Windows.Forms.TextBox();
            this.lname = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.mSkillsTab = new System.Windows.Forms.TabPage();
            this.mAppearanceTab = new System.Windows.Forms.TabPage();
            this.mCancelButton = new System.Windows.Forms.Button();
            this.mOkButton = new System.Windows.Forms.Button();
            this.m_TeamsComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.mNextButton = new System.Windows.Forms.Button();
            this.mPreviousButton = new System.Windows.Forms.Button();
            this.mPlayerIndexUpDown = new System.Windows.Forms.NumericUpDown();
            this.Pos = new NFL2K5Tool.StringSelectionControl();
            this.tabControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mPlayerIndexUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // fname
            // 
            this.fname.Location = new System.Drawing.Point(15, 25);
            this.fname.Name = "fname";
            this.fname.Size = new System.Drawing.Size(100, 20);
            this.fname.TabIndex = 0;
            // 
            // lname
            // 
            this.lname.Location = new System.Drawing.Point(121, 25);
            this.lname.Name = "lname";
            this.lname.Size = new System.Drawing.Size(100, 20);
            this.lname.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Name";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.mSkillsTab);
            this.tabControl1.Controls.Add(this.mAppearanceTab);
            this.tabControl1.Location = new System.Drawing.Point(12, 51);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(645, 348);
            this.tabControl1.TabIndex = 5;
            // 
            // mSkillsTab
            // 
            this.mSkillsTab.Location = new System.Drawing.Point(4, 22);
            this.mSkillsTab.Name = "mSkillsTab";
            this.mSkillsTab.Padding = new System.Windows.Forms.Padding(3);
            this.mSkillsTab.Size = new System.Drawing.Size(637, 322);
            this.mSkillsTab.TabIndex = 0;
            this.mSkillsTab.Text = "Skills";
            this.mSkillsTab.UseVisualStyleBackColor = true;
            // 
            // mAppearanceTab
            // 
            this.mAppearanceTab.Location = new System.Drawing.Point(4, 22);
            this.mAppearanceTab.Name = "mAppearanceTab";
            this.mAppearanceTab.Padding = new System.Windows.Forms.Padding(3);
            this.mAppearanceTab.Size = new System.Drawing.Size(637, 322);
            this.mAppearanceTab.TabIndex = 1;
            this.mAppearanceTab.Text = "Appearance";
            this.mAppearanceTab.UseVisualStyleBackColor = true;
            // 
            // mCancelButton
            // 
            this.mCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.mCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.mCancelButton.Location = new System.Drawing.Point(579, 403);
            this.mCancelButton.Name = "mCancelButton";
            this.mCancelButton.Size = new System.Drawing.Size(75, 23);
            this.mCancelButton.TabIndex = 9;
            this.mCancelButton.Text = "&Cancel";
            this.mCancelButton.UseVisualStyleBackColor = true;
            // 
            // mOkButton
            // 
            this.mOkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.mOkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.mOkButton.Location = new System.Drawing.Point(498, 403);
            this.mOkButton.Name = "mOkButton";
            this.mOkButton.Size = new System.Drawing.Size(75, 23);
            this.mOkButton.TabIndex = 8;
            this.mOkButton.Text = "&OK";
            this.mOkButton.UseVisualStyleBackColor = true;
            // 
            // m_TeamsComboBox
            // 
            this.m_TeamsComboBox.FormattingEnabled = true;
            this.m_TeamsComboBox.Location = new System.Drawing.Point(444, 24);
            this.m_TeamsComboBox.Name = "m_TeamsComboBox";
            this.m_TeamsComboBox.Size = new System.Drawing.Size(121, 21);
            this.m_TeamsComboBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(441, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Team";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(585, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Player Index";
            // 
            // mNextButton
            // 
            this.mNextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mNextButton.Location = new System.Drawing.Point(99, 401);
            this.mNextButton.Name = "mNextButton";
            this.mNextButton.Size = new System.Drawing.Size(82, 23);
            this.mNextButton.TabIndex = 7;
            this.mNextButton.Text = "&NextPlayer";
            this.mNextButton.UseVisualStyleBackColor = true;
            // 
            // mPreviousButton
            // 
            this.mPreviousButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mPreviousButton.Location = new System.Drawing.Point(11, 401);
            this.mPreviousButton.Name = "mPreviousButton";
            this.mPreviousButton.Size = new System.Drawing.Size(82, 23);
            this.mPreviousButton.TabIndex = 6;
            this.mPreviousButton.Text = "&Prev Player";
            this.mPreviousButton.UseVisualStyleBackColor = true;
            // 
            // mPlayerIndexUpDown
            // 
            this.mPlayerIndexUpDown.Location = new System.Drawing.Point(588, 26);
            this.mPlayerIndexUpDown.Maximum = new decimal(new int[] {
            58,
            0,
            0,
            0});
            this.mPlayerIndexUpDown.Name = "mPlayerIndexUpDown";
            this.mPlayerIndexUpDown.Size = new System.Drawing.Size(62, 20);
            this.mPlayerIndexUpDown.TabIndex = 4;
            // 
            // Pos
            // 
            this.Pos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Pos.Location = new System.Drawing.Point(227, 7);
            this.Pos.Name = "Pos";
            this.Pos.RepresentedValue = typeof(string);
            this.Pos.Size = new System.Drawing.Size(106, 48);
            this.Pos.TabIndex = 2;
            this.Pos.Value = "";
            // 
            // PlayerEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 427);
            this.Controls.Add(this.Pos);
            this.Controls.Add(this.mPlayerIndexUpDown);
            this.Controls.Add(this.mPreviousButton);
            this.Controls.Add(this.mNextButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_TeamsComboBox);
            this.Controls.Add(this.mOkButton);
            this.Controls.Add(this.mCancelButton);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lname);
            this.Controls.Add(this.fname);
            this.Name = "PlayerEditForm";
            this.Text = "Edit Player";
            this.tabControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mPlayerIndexUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox fname;
        private System.Windows.Forms.TextBox lname;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage mSkillsTab;
        private System.Windows.Forms.TabPage mAppearanceTab;
        private System.Windows.Forms.Button mCancelButton;
        private System.Windows.Forms.Button mOkButton;
        private System.Windows.Forms.ComboBox m_TeamsComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button mNextButton;
        private System.Windows.Forms.Button mPreviousButton;
        private System.Windows.Forms.NumericUpDown mPlayerIndexUpDown;
        private StringSelectionControl Pos;
    }
}