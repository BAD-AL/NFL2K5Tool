namespace NFL2K5Tool
{
    partial class EnhancedPlayerEditor
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
            this.mFacePictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.mFacePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // mFacePictureBox
            // 
            this.mFacePictureBox.Location = new System.Drawing.Point(12, 12);
            this.mFacePictureBox.Name = "mFacePictureBox";
            this.mFacePictureBox.Size = new System.Drawing.Size(79, 99);
            this.mFacePictureBox.TabIndex = 0;
            this.mFacePictureBox.TabStop = false;
            // 
            // EnhancedPlayerEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 414);
            this.Controls.Add(this.mFacePictureBox);
            this.Name = "EnhancedPlayerEditor";
            this.Text = "EnhancedPlayerEditor";
            ((System.ComponentModel.ISupportInitialize)(this.mFacePictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox mFacePictureBox;
    }
}