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
            this.SuspendLayout();
            // 
            // mPictureBoxPanel
            // 
            this.mPictureBoxPanel.AutoScroll = true;
            this.mPictureBoxPanel.Location = new System.Drawing.Point(12, 12);
            this.mPictureBoxPanel.Name = "mPictureBoxPanel";
            this.mPictureBoxPanel.Size = new System.Drawing.Size(590, 355);
            this.mPictureBoxPanel.TabIndex = 0;
            // 
            // FaceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 379);
            this.Controls.Add(this.mPictureBoxPanel);
            this.Name = "FaceForm";
            this.Text = "FaceForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mPictureBoxPanel;
    }
}