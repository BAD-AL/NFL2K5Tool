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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayerEditForm));
            this.fname = new System.Windows.Forms.TextBox();
            this.lname = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.mSkillsTab = new System.Windows.Forms.TabPage();
            this.mAppearanceTab = new System.Windows.Forms.TabPage();
            this.mGenericFacePictureBox = new System.Windows.Forms.PictureBox();
            this.mCancelButton = new System.Windows.Forms.Button();
            this.mOkButton = new System.Windows.Forms.Button();
            this.m_TeamsComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.mNextButton = new System.Windows.Forms.Button();
            this.mPreviousButton = new System.Windows.Forms.Button();
            this.mPlayerIndexUpDown = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.mFacePictureBox = new System.Windows.Forms.PictureBox();
            this.mSkinColorLabel = new System.Windows.Forms.Label();
            this.mHeightLabel = new System.Windows.Forms.Label();
            this.mWeightLabel = new System.Windows.Forms.Label();
            this.mBodyTypeLabel = new System.Windows.Forms.Label();
            this.mJerseyNumberLabel = new System.Windows.Forms.Label();
            this.mFaceMaskPictureBox = new System.Windows.Forms.PictureBox();
            this.Position = new NFL2K5Tool.StringSelectionControl();
            this.tabControl1.SuspendLayout();
            this.mAppearanceTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mGenericFacePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mPlayerIndexUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mFacePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mFaceMaskPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // fname
            // 
            this.fname.Location = new System.Drawing.Point(39, 25);
            this.fname.Name = "fname";
            this.fname.Size = new System.Drawing.Size(83, 20);
            this.fname.TabIndex = 0;
            this.fname.Leave += new System.EventHandler(this.ValueChanged);
            // 
            // lname
            // 
            this.lname.Location = new System.Drawing.Point(130, 25);
            this.lname.Name = "lname";
            this.lname.Size = new System.Drawing.Size(91, 20);
            this.lname.TabIndex = 1;
            this.lname.Leave += new System.EventHandler(this.ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 9);
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
            this.tabControl1.Size = new System.Drawing.Size(645, 434);
            this.tabControl1.TabIndex = 5;
            // 
            // mSkillsTab
            // 
            this.mSkillsTab.Location = new System.Drawing.Point(4, 22);
            this.mSkillsTab.Name = "mSkillsTab";
            this.mSkillsTab.Padding = new System.Windows.Forms.Padding(3);
            this.mSkillsTab.Size = new System.Drawing.Size(637, 408);
            this.mSkillsTab.TabIndex = 0;
            this.mSkillsTab.Text = "Skills";
            this.mSkillsTab.UseVisualStyleBackColor = true;
            // 
            // mAppearanceTab
            // 
            this.mAppearanceTab.Controls.Add(this.mGenericFacePictureBox);
            this.mAppearanceTab.Location = new System.Drawing.Point(4, 22);
            this.mAppearanceTab.Name = "mAppearanceTab";
            this.mAppearanceTab.Padding = new System.Windows.Forms.Padding(3);
            this.mAppearanceTab.Size = new System.Drawing.Size(637, 408);
            this.mAppearanceTab.TabIndex = 1;
            this.mAppearanceTab.Text = "Appearance";
            this.mAppearanceTab.UseVisualStyleBackColor = true;
            // 
            // mGenericFacePictureBox
            // 
            this.mGenericFacePictureBox.BackColor = System.Drawing.Color.MistyRose;
            this.mGenericFacePictureBox.Location = new System.Drawing.Point(3, 293);
            this.mGenericFacePictureBox.Name = "mGenericFacePictureBox";
            this.mGenericFacePictureBox.Size = new System.Drawing.Size(220, 109);
            this.mGenericFacePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mGenericFacePictureBox.TabIndex = 1;
            this.mGenericFacePictureBox.TabStop = false;
            // 
            // mCancelButton
            // 
            this.mCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.mCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.mCancelButton.Location = new System.Drawing.Point(579, 489);
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
            this.mOkButton.Location = new System.Drawing.Point(498, 489);
            this.mOkButton.Name = "mOkButton";
            this.mOkButton.Size = new System.Drawing.Size(75, 23);
            this.mOkButton.TabIndex = 8;
            this.mOkButton.Text = "&OK";
            this.mOkButton.UseVisualStyleBackColor = true;
            this.mOkButton.Click += new System.EventHandler(this.mOkButton_Click);
            // 
            // m_TeamsComboBox
            // 
            this.m_TeamsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_TeamsComboBox.FormattingEnabled = true;
            this.m_TeamsComboBox.Location = new System.Drawing.Point(498, 25);
            this.m_TeamsComboBox.Name = "m_TeamsComboBox";
            this.m_TeamsComboBox.Size = new System.Drawing.Size(83, 21);
            this.m_TeamsComboBox.TabIndex = 3;
            this.m_TeamsComboBox.SelectedIndexChanged += new System.EventHandler(this.m_TeamsComboBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(585, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Team Index";
            // 
            // mNextButton
            // 
            this.mNextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mNextButton.Location = new System.Drawing.Point(99, 487);
            this.mNextButton.Name = "mNextButton";
            this.mNextButton.Size = new System.Drawing.Size(82, 23);
            this.mNextButton.TabIndex = 7;
            this.mNextButton.Text = "&NextPlayer";
            this.mNextButton.UseVisualStyleBackColor = true;
            this.mNextButton.Click += new System.EventHandler(this.mNextButton_Click);
            // 
            // mPreviousButton
            // 
            this.mPreviousButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mPreviousButton.Location = new System.Drawing.Point(11, 487);
            this.mPreviousButton.Name = "mPreviousButton";
            this.mPreviousButton.Size = new System.Drawing.Size(82, 23);
            this.mPreviousButton.TabIndex = 6;
            this.mPreviousButton.Text = "&Prev Player";
            this.mPreviousButton.UseVisualStyleBackColor = true;
            this.mPreviousButton.Click += new System.EventHandler(this.mPreviousButton_Click);
            // 
            // mPlayerIndexUpDown
            // 
            this.mPlayerIndexUpDown.Location = new System.Drawing.Point(588, 26);
            this.mPlayerIndexUpDown.Maximum = new decimal(new int[] {
            1158,
            0,
            0,
            0});
            this.mPlayerIndexUpDown.Name = "mPlayerIndexUpDown";
            this.mPlayerIndexUpDown.Size = new System.Drawing.Size(62, 20);
            this.mPlayerIndexUpDown.TabIndex = 4;
            this.mPlayerIndexUpDown.ValueChanged += new System.EventHandler(this.mPlayerIndexUpDown_ValueChanged);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(291, 489);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(82, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Show player string";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // mFacePictureBox
            // 
            this.mFacePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mFacePictureBox.Location = new System.Drawing.Point(303, 5);
            this.mFacePictureBox.Name = "mFacePictureBox";
            this.mFacePictureBox.Size = new System.Drawing.Size(49, 50);
            this.mFacePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mFacePictureBox.TabIndex = 11;
            this.mFacePictureBox.TabStop = false;
            this.mFacePictureBox.Click += new System.EventHandler(this.mFacePictureBox_Click);
            // 
            // mSkinColorLabel
            // 
            this.mSkinColorLabel.AutoSize = true;
            this.mSkinColorLabel.ForeColor = System.Drawing.Color.White;
            this.mSkinColorLabel.Location = new System.Drawing.Point(358, 2);
            this.mSkinColorLabel.Name = "mSkinColorLabel";
            this.mSkinColorLabel.Size = new System.Drawing.Size(35, 13);
            this.mSkinColorLabel.TabIndex = 12;
            this.mSkinColorLabel.Text = "SkinX";
            this.mSkinColorLabel.Click += new System.EventHandler(this.mSkinColorLabel_Click);
            // 
            // mHeightLabel
            // 
            this.mHeightLabel.AutoSize = true;
            this.mHeightLabel.Location = new System.Drawing.Point(358, 17);
            this.mHeightLabel.Name = "mHeightLabel";
            this.mHeightLabel.Size = new System.Drawing.Size(38, 13);
            this.mHeightLabel.TabIndex = 13;
            this.mHeightLabel.Text = "Height";
            this.mHeightLabel.Click += new System.EventHandler(this.mHeightLabel_Click);
            // 
            // mWeightLabel
            // 
            this.mWeightLabel.AutoSize = true;
            this.mWeightLabel.Location = new System.Drawing.Point(358, 34);
            this.mWeightLabel.Name = "mWeightLabel";
            this.mWeightLabel.Size = new System.Drawing.Size(41, 13);
            this.mWeightLabel.TabIndex = 14;
            this.mWeightLabel.Text = "Weight";
            this.mWeightLabel.Click += new System.EventHandler(this.mWeightLabel_Click);
            // 
            // mBodyTypeLabel
            // 
            this.mBodyTypeLabel.AutoSize = true;
            this.mBodyTypeLabel.Location = new System.Drawing.Point(302, 56);
            this.mBodyTypeLabel.Name = "mBodyTypeLabel";
            this.mBodyTypeLabel.Size = new System.Drawing.Size(94, 13);
            this.mBodyTypeLabel.TabIndex = 15;
            this.mBodyTypeLabel.Text = "BodyType: Normal";
            this.mBodyTypeLabel.Click += new System.EventHandler(this.mBodyTypeLabel_Click);
            // 
            // mJerseyNumberLabel
            // 
            this.mJerseyNumberLabel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.mJerseyNumberLabel.Location = new System.Drawing.Point(8, 28);
            this.mJerseyNumberLabel.Name = "mJerseyNumberLabel";
            this.mJerseyNumberLabel.Size = new System.Drawing.Size(30, 17);
            this.mJerseyNumberLabel.TabIndex = 16;
            this.mJerseyNumberLabel.Text = "#00";
            // 
            // mFaceMaskPictureBox
            // 
            this.mFaceMaskPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mFaceMaskPictureBox.Location = new System.Drawing.Point(430, 5);
            this.mFaceMaskPictureBox.Name = "mFaceMaskPictureBox";
            this.mFaceMaskPictureBox.Size = new System.Drawing.Size(49, 50);
            this.mFaceMaskPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mFaceMaskPictureBox.TabIndex = 17;
            this.mFaceMaskPictureBox.TabStop = false;
            // 
            // Position
            // 
            this.Position.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Position.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Position.Location = new System.Drawing.Point(227, 7);
            this.Position.Name = "Position";
            this.Position.RepresentedValue = typeof(string);
            this.Position.Size = new System.Drawing.Size(70, 48);
            this.Position.TabIndex = 2;
            this.Position.Value = "";
            this.Position.ValueChanged += new System.EventHandler(this.ValueChanged);
            // 
            // PlayerEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.mCancelButton;
            this.ClientSize = new System.Drawing.Size(669, 513);
            this.Controls.Add(this.mFaceMaskPictureBox);
            this.Controls.Add(this.mJerseyNumberLabel);
            this.Controls.Add(this.mBodyTypeLabel);
            this.Controls.Add(this.mWeightLabel);
            this.Controls.Add(this.mHeightLabel);
            this.Controls.Add(this.mSkinColorLabel);
            this.Controls.Add(this.mFacePictureBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Position);
            this.Controls.Add(this.mPlayerIndexUpDown);
            this.Controls.Add(this.mPreviousButton);
            this.Controls.Add(this.mNextButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_TeamsComboBox);
            this.Controls.Add(this.mOkButton);
            this.Controls.Add(this.mCancelButton);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lname);
            this.Controls.Add(this.fname);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PlayerEditForm";
            this.Text = "Edit Player";
            this.tabControl1.ResumeLayout(false);
            this.mAppearanceTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mGenericFacePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mPlayerIndexUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mFacePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mFaceMaskPictureBox)).EndInit();
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button mNextButton;
        private System.Windows.Forms.Button mPreviousButton;
        private System.Windows.Forms.NumericUpDown mPlayerIndexUpDown;
        private StringSelectionControl Position;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox mFacePictureBox;
        private System.Windows.Forms.Label mSkinColorLabel;
        private System.Windows.Forms.Label mHeightLabel;
        private System.Windows.Forms.Label mWeightLabel;
        private System.Windows.Forms.Label mBodyTypeLabel;
        private System.Windows.Forms.Label mJerseyNumberLabel;
        private System.Windows.Forms.PictureBox mFaceMaskPictureBox;
        private System.Windows.Forms.PictureBox mGenericFacePictureBox;
    }
}