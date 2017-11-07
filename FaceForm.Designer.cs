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
            this.mPictureBoxPanel = new System.Windows.Forms.Panel();
            this.mPhotoScrollBar = new System.Windows.Forms.VScrollBar();
            this.mSkinComboBox = new System.Windows.Forms.ComboBox();
            this.mLastNameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mPictureBoxPanel
            // 
            this.mPictureBoxPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mPictureBoxPanel.Location = new System.Drawing.Point(12, 12);
            this.mPictureBoxPanel.Name = "mPictureBoxPanel";
            this.mPictureBoxPanel.Size = new System.Drawing.Size(592, 370);
            this.mPictureBoxPanel.TabIndex = 0;
            this.mPictureBoxPanel.Resize += new System.EventHandler(this.mPictureBoxPanel_Resize);
            // 
            // mPhotoScrollBar
            // 
            this.mPhotoScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mPhotoScrollBar.Location = new System.Drawing.Point(608, 12);
            this.mPhotoScrollBar.Name = "mPhotoScrollBar";
            this.mPhotoScrollBar.Size = new System.Drawing.Size(17, 375);
            this.mPhotoScrollBar.TabIndex = 1;
            // 
            // mSkinComboBox
            // 
            this.mSkinComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mSkinComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mSkinComboBox.FormattingEnabled = true;
            this.mSkinComboBox.Items.AddRange(new object[] {
            "All",
            "Black",
            "Hispanic",
            "Pacific Islander",
            "White"});
            this.mSkinComboBox.Location = new System.Drawing.Point(12, 392);
            this.mSkinComboBox.Name = "mSkinComboBox";
            this.mSkinComboBox.Size = new System.Drawing.Size(121, 21);
            this.mSkinComboBox.TabIndex = 2;
            this.mSkinComboBox.SelectedIndexChanged += new System.EventHandler(this.mSkinComboBox_SelectedIndexChanged);
            // 
            // mLastNameTextBox
            // 
            this.mLastNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mLastNameTextBox.Location = new System.Drawing.Point(230, 390);
            this.mLastNameTextBox.Name = "mLastNameTextBox";
            this.mLastNameTextBox.Size = new System.Drawing.Size(150, 20);
            this.mLastNameTextBox.TabIndex = 3;
            this.mLastNameTextBox.TextChanged += new System.EventHandler(this.mLastNameTextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(166, 393);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Last Name";
            // 
            // FaceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 425);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mLastNameTextBox);
            this.Controls.Add(this.mSkinComboBox);
            this.Controls.Add(this.mPhotoScrollBar);
            this.Controls.Add(this.mPictureBoxPanel);
            this.Name = "FaceForm";
            this.Text = "FaceForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel mPictureBoxPanel;
        private System.Windows.Forms.VScrollBar mPhotoScrollBar;
        private System.Windows.Forms.ComboBox mSkinComboBox;
        private System.Windows.Forms.TextBox mLastNameTextBox;
        private System.Windows.Forms.Label label1;
    }
}