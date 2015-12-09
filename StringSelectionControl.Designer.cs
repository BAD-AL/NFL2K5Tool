namespace NFL2K5Tool
{
    partial class StringSelectionControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mLabel = new System.Windows.Forms.Label();
            this.mComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // mLabel
            // 
            this.mLabel.AutoSize = true;
            this.mLabel.Location = new System.Drawing.Point(2, 2);
            this.mLabel.Name = "mLabel";
            this.mLabel.Size = new System.Drawing.Size(28, 13);
            this.mLabel.TabIndex = 14;
            this.mLabel.Text = "Text";
            // 
            // mComboBox
            // 
            this.mComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mComboBox.FormattingEnabled = true;
            this.mComboBox.Location = new System.Drawing.Point(5, 19);
            this.mComboBox.Name = "mComboBox";
            this.mComboBox.Size = new System.Drawing.Size(118, 21);
            this.mComboBox.TabIndex = 15;
            this.mComboBox.SelectedIndexChanged += new System.EventHandler(this.mComboBox_SelectedIndexChanged);
            // 
            // StringSelectionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.mComboBox);
            this.Controls.Add(this.mLabel);
            this.Name = "StringSelectionControl";
            this.Size = new System.Drawing.Size(126, 48);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label mLabel;
        private System.Windows.Forms.ComboBox mComboBox;
    }
}
