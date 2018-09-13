namespace NFL2K5Tool
{
    partial class CoachEditForm
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
            this.Photo = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LastName = new System.Windows.Forms.TextBox();
            this.FirstName = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.mStatsTab = new System.Windows.Forms.TabPage();
            this.mStrategyTab = new System.Windows.Forms.TabPage();
            this.mBodyTab = new System.Windows.Forms.TabPage();
            this.mBodyPictureBox = new System.Windows.Forms.PictureBox();
            this.m_TeamsComboBox = new System.Windows.Forms.ComboBox();
            this.Info1 = new System.Windows.Forms.TextBox();
            this.Info2 = new System.Windows.Forms.TextBox();
            this.mOkButton = new System.Windows.Forms.Button();
            this.mCancelButton = new System.Windows.Forms.Button();
            this.Body = new NFL2K5Tool.StringSelectionControl();
            ((System.ComponentModel.ISupportInitialize)(this.Photo)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.mBodyTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mBodyPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // Photo
            // 
            this.Photo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Photo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Photo.Location = new System.Drawing.Point(219, 4);
            this.Photo.Name = "Photo";
            this.Photo.Size = new System.Drawing.Size(49, 50);
            this.Photo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Photo.TabIndex = 19;
            this.Photo.TabStop = false;
            this.Photo.Click += new System.EventHandler(this.mFacePictureBox_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Name";
            // 
            // LastName
            // 
            this.LastName.Location = new System.Drawing.Point(101, 28);
            this.LastName.Name = "LastName";
            this.LastName.Size = new System.Drawing.Size(91, 20);
            this.LastName.TabIndex = 17;
            this.LastName.Leave += new System.EventHandler(this.ValueChanged);
            // 
            // FirstName
            // 
            this.FirstName.Location = new System.Drawing.Point(10, 28);
            this.FirstName.Name = "FirstName";
            this.FirstName.Size = new System.Drawing.Size(83, 20);
            this.FirstName.TabIndex = 16;
            this.FirstName.Leave += new System.EventHandler(this.ValueChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.mBodyTab);
            this.tabControl1.Controls.Add(this.mStrategyTab);
            this.tabControl1.Controls.Add(this.mStatsTab);
            this.tabControl1.Location = new System.Drawing.Point(6, 107);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(489, 375);
            this.tabControl1.TabIndex = 22;
            // 
            // mStatsTab
            // 
            this.mStatsTab.Location = new System.Drawing.Point(4, 22);
            this.mStatsTab.Name = "mStatsTab";
            this.mStatsTab.Padding = new System.Windows.Forms.Padding(3);
            this.mStatsTab.Size = new System.Drawing.Size(481, 349);
            this.mStatsTab.TabIndex = 0;
            this.mStatsTab.Text = "Stats";
            this.mStatsTab.UseVisualStyleBackColor = true;
            // 
            // mStrategyTab
            // 
            this.mStrategyTab.Location = new System.Drawing.Point(4, 22);
            this.mStrategyTab.Name = "mStrategyTab";
            this.mStrategyTab.Padding = new System.Windows.Forms.Padding(3);
            this.mStrategyTab.Size = new System.Drawing.Size(481, 349);
            this.mStrategyTab.TabIndex = 1;
            this.mStrategyTab.Text = "Strategy";
            this.mStrategyTab.UseVisualStyleBackColor = true;
            // 
            // mBodyTab
            // 
            this.mBodyTab.Controls.Add(this.mBodyPictureBox);
            this.mBodyTab.Controls.Add(this.Body);
            this.mBodyTab.Location = new System.Drawing.Point(4, 22);
            this.mBodyTab.Name = "mBodyTab";
            this.mBodyTab.Size = new System.Drawing.Size(481, 349);
            this.mBodyTab.TabIndex = 2;
            this.mBodyTab.Text = "Body";
            this.mBodyTab.UseVisualStyleBackColor = true;
            // 
            // mBodyPictureBox
            // 
            this.mBodyPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.mBodyPictureBox.Location = new System.Drawing.Point(3, 3);
            this.mBodyPictureBox.Name = "mBodyPictureBox";
            this.mBodyPictureBox.Size = new System.Drawing.Size(296, 347);
            this.mBodyPictureBox.TabIndex = 0;
            this.mBodyPictureBox.TabStop = false;
            // 
            // m_TeamsComboBox
            // 
            this.m_TeamsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_TeamsComboBox.FormattingEnabled = true;
            this.m_TeamsComboBox.Location = new System.Drawing.Point(408, 7);
            this.m_TeamsComboBox.Name = "m_TeamsComboBox";
            this.m_TeamsComboBox.Size = new System.Drawing.Size(83, 21);
            this.m_TeamsComboBox.TabIndex = 23;
            this.m_TeamsComboBox.SelectedIndexChanged += new System.EventHandler(this.m_TeamsComboBox_SelectedIndexChanged);
            // 
            // Info1
            // 
            this.Info1.Location = new System.Drawing.Point(10, 54);
            this.Info1.Name = "Info1";
            this.Info1.Size = new System.Drawing.Size(203, 20);
            this.Info1.TabIndex = 24;
            this.Info1.Leave += new System.EventHandler(this.ValueChanged);
            // 
            // Info2
            // 
            this.Info2.Location = new System.Drawing.Point(10, 81);
            this.Info2.Name = "Info2";
            this.Info2.Size = new System.Drawing.Size(203, 20);
            this.Info2.TabIndex = 25;
            this.Info2.Leave += new System.EventHandler(this.ValueChanged);
            // 
            // mOkButton
            // 
            this.mOkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.mOkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.mOkButton.Location = new System.Drawing.Point(335, 485);
            this.mOkButton.Name = "mOkButton";
            this.mOkButton.Size = new System.Drawing.Size(75, 23);
            this.mOkButton.TabIndex = 26;
            this.mOkButton.Text = "&OK";
            this.mOkButton.UseVisualStyleBackColor = true;
            this.mOkButton.Click += new System.EventHandler(this.mOkButton_Click);
            // 
            // mCancelButton
            // 
            this.mCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.mCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.mCancelButton.Location = new System.Drawing.Point(416, 485);
            this.mCancelButton.Name = "mCancelButton";
            this.mCancelButton.Size = new System.Drawing.Size(75, 23);
            this.mCancelButton.TabIndex = 27;
            this.mCancelButton.Text = "&Cancel";
            this.mCancelButton.UseVisualStyleBackColor = true;
            // 
            // Body
            // 
            this.Body.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Body.BackColor = System.Drawing.Color.Moccasin;
            this.Body.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Body.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Body.Location = new System.Drawing.Point(350, 3);
            this.Body.Name = "Body";
            this.Body.RepresentedValue = typeof(string);
            this.Body.Size = new System.Drawing.Size(126, 48);
            this.Body.TabIndex = 21;
            this.Body.Value = "";
            this.Body.ValueChanged += new System.EventHandler(this.mBodySelectionControl_ValueChanged);
            // 
            // CoachEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 508);
            this.Controls.Add(this.mOkButton);
            this.Controls.Add(this.mCancelButton);
            this.Controls.Add(this.Info2);
            this.Controls.Add(this.Info1);
            this.Controls.Add(this.m_TeamsComboBox);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.Photo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LastName);
            this.Controls.Add(this.FirstName);
            this.Name = "CoachEditForm";
            this.Text = "CoachEditForm";
            ((System.ComponentModel.ISupportInitialize)(this.Photo)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.mBodyTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mBodyPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Photo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox LastName;
        private System.Windows.Forms.TextBox FirstName;
        private StringSelectionControl Body;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage mStatsTab;
        private System.Windows.Forms.TabPage mStrategyTab;
        private System.Windows.Forms.TabPage mBodyTab;
        private System.Windows.Forms.PictureBox mBodyPictureBox;
        private System.Windows.Forms.ComboBox m_TeamsComboBox;
        private System.Windows.Forms.TextBox Info1;
        private System.Windows.Forms.TextBox Info2;
        private System.Windows.Forms.Button mOkButton;
        private System.Windows.Forms.Button mCancelButton;
    }
}