namespace NFL2K5Tool
{
    partial class PictureHelper
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
            this.mDataFileTextBox = new System.Windows.Forms.TextBox();
            this.mPhotoListTextBox = new System.Windows.Forms.TextBox();
            this.mOutputTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mDataFileTextBox
            // 
            this.mDataFileTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.mDataFileTextBox.Location = new System.Drawing.Point(12, 21);
            this.mDataFileTextBox.MaxLength = 3276700;
            this.mDataFileTextBox.Multiline = true;
            this.mDataFileTextBox.Name = "mDataFileTextBox";
            this.mDataFileTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.mDataFileTextBox.Size = new System.Drawing.Size(289, 454);
            this.mDataFileTextBox.TabIndex = 0;
            // 
            // mPhotoListTextBox
            // 
            this.mPhotoListTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.mPhotoListTextBox.Location = new System.Drawing.Point(307, 21);
            this.mPhotoListTextBox.MaxLength = 3276700;
            this.mPhotoListTextBox.Multiline = true;
            this.mPhotoListTextBox.Name = "mPhotoListTextBox";
            this.mPhotoListTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.mPhotoListTextBox.Size = new System.Drawing.Size(289, 454);
            this.mPhotoListTextBox.TabIndex = 1;
            // 
            // mOutputTextBox
            // 
            this.mOutputTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.mOutputTextBox.Location = new System.Drawing.Point(614, 21);
            this.mOutputTextBox.MaxLength = 3276700;
            this.mOutputTextBox.Multiline = true;
            this.mOutputTextBox.Name = "mOutputTextBox";
            this.mOutputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.mOutputTextBox.Size = new System.Drawing.Size(289, 454);
            this.mOutputTextBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Data File";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(307, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Photo list";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(611, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Output";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(910, 21);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(48, 45);
            this.button1.TabIndex = 6;
            this.button1.Text = "Go";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // PictureHelper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(961, 487);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mOutputTextBox);
            this.Controls.Add(this.mPhotoListTextBox);
            this.Controls.Add(this.mDataFileTextBox);
            this.Name = "PictureHelper";
            this.Text = "PictureHelper";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox mDataFileTextBox;
        private System.Windows.Forms.TextBox mPhotoListTextBox;
        private System.Windows.Forms.TextBox mOutputTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
    }
}