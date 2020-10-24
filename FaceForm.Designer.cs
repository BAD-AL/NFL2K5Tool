namespace NFL2K5Tool
{
    partial class FaceForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FaceForm));
            this.mPictureBoxPanel = new System.Windows.Forms.Panel();
            this.mPhotoScrollBar = new System.Windows.Forms.VScrollBar();
            this.mCategoryComboBox = new System.Windows.Forms.ComboBox();
            this.mLastNameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.mShowNumbersCheckbox = new System.Windows.Forms.CheckBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.runSpecialActionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.classifyMode = new System.Windows.Forms.ToolStripMenuItem();
            this.showUnclassifiedPhotosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mPictureBoxPanel
            // 
            this.mPictureBoxPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mPictureBoxPanel.Location = new System.Drawing.Point(12, 12);
            this.mPictureBoxPanel.Name = "mPictureBoxPanel";
            this.mPictureBoxPanel.Size = new System.Drawing.Size(763, 454);
            this.mPictureBoxPanel.TabIndex = 0;
            this.mPictureBoxPanel.Resize += new System.EventHandler(this.mPictureBoxPanel_Resize);
            // 
            // mPhotoScrollBar
            // 
            this.mPhotoScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mPhotoScrollBar.Location = new System.Drawing.Point(779, 12);
            this.mPhotoScrollBar.Name = "mPhotoScrollBar";
            this.mPhotoScrollBar.Size = new System.Drawing.Size(17, 459);
            this.mPhotoScrollBar.TabIndex = 1;
            // 
            // mCategoryComboBox
            // 
            this.mCategoryComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mCategoryComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mCategoryComboBox.FormattingEnabled = true;
            this.mCategoryComboBox.Location = new System.Drawing.Point(12, 476);
            this.mCategoryComboBox.Name = "mCategoryComboBox";
            this.mCategoryComboBox.Size = new System.Drawing.Size(121, 21);
            this.mCategoryComboBox.TabIndex = 2;
            this.mCategoryComboBox.SelectedIndexChanged += new System.EventHandler(this.mSkinComboBox_SelectedIndexChanged);
            // 
            // mLastNameTextBox
            // 
            this.mLastNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mLastNameTextBox.Location = new System.Drawing.Point(210, 474);
            this.mLastNameTextBox.Name = "mLastNameTextBox";
            this.mLastNameTextBox.Size = new System.Drawing.Size(150, 20);
            this.mLastNameTextBox.TabIndex = 3;
            this.mLastNameTextBox.Visible = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(146, 477);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Last Name";
            this.label1.Visible = false;
            // 
            // mShowNumbersCheckbox
            // 
            this.mShowNumbersCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.mShowNumbersCheckbox.AutoSize = true;
            this.mShowNumbersCheckbox.Location = new System.Drawing.Point(695, 478);
            this.mShowNumbersCheckbox.Name = "mShowNumbersCheckbox";
            this.mShowNumbersCheckbox.Size = new System.Drawing.Size(98, 17);
            this.mShowNumbersCheckbox.TabIndex = 5;
            this.mShowNumbersCheckbox.Text = "Show Numbers";
            this.mShowNumbersCheckbox.UseVisualStyleBackColor = true;
            this.mShowNumbersCheckbox.Click += new System.EventHandler(this.mShowNumbersCheckbox_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runSpecialActionToolStripMenuItem,
            this.classifyMode,
            this.showUnclassifiedPhotosToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(210, 92);
            // 
            // runSpecialActionToolStripMenuItem
            // 
            this.runSpecialActionToolStripMenuItem.Name = "runSpecialActionToolStripMenuItem";
            this.runSpecialActionToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.runSpecialActionToolStripMenuItem.Text = "Run Special Action";
            this.runSpecialActionToolStripMenuItem.Click += new System.EventHandler(this.runSpecialActionItem_Click);
            // 
            // classifyMode
            // 
            this.classifyMode.Name = "classifyMode";
            this.classifyMode.Size = new System.Drawing.Size(173, 22);
            this.classifyMode.Text = "Classify Mode";
            this.classifyMode.Click += new System.EventHandler(this.classifyMode_Click);
            // 
            // showUnclassifiedPhotosToolStripMenuItem
            // 
            this.showUnclassifiedPhotosToolStripMenuItem.Name = "showUnclassifiedPhotosToolStripMenuItem";
            this.showUnclassifiedPhotosToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.showUnclassifiedPhotosToolStripMenuItem.Text = "Show Unclassified Photos";
            this.showUnclassifiedPhotosToolStripMenuItem.Click += new System.EventHandler(this.showUnclassifiedPhotosToolStripMenuItem_Click);
            // 
            // FaceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 509);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mShowNumbersCheckbox);
            this.Controls.Add(this.mLastNameTextBox);
            this.Controls.Add(this.mCategoryComboBox);
            this.Controls.Add(this.mPhotoScrollBar);
            this.Controls.Add(this.mPictureBoxPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FaceForm";
            this.Text = "FaceForm";
            this.Click += new System.EventHandler(this.FaceForm_Click);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel mPictureBoxPanel;
        private System.Windows.Forms.VScrollBar mPhotoScrollBar;
        private System.Windows.Forms.ComboBox mCategoryComboBox;
        private System.Windows.Forms.TextBox mLastNameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox mShowNumbersCheckbox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem runSpecialActionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem classifyMode;
        private System.Windows.Forms.ToolStripMenuItem showUnclassifiedPhotosToolStripMenuItem;
    }
}