namespace ImageCapture
{
    partial class PlayerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayerForm));
            this.mPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            this.mSourceTextBox = new System.Windows.Forms.TextBox();
            this.mBrowseButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.mPlayer)).BeginInit();
            this.SuspendLayout();
            // 
            // mPlayer
            // 
            this.mPlayer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mPlayer.Enabled = true;
            this.mPlayer.Location = new System.Drawing.Point(12, 12);
            this.mPlayer.Name = "mPlayer";
            this.mPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("mPlayer.OcxState")));
            this.mPlayer.Size = new System.Drawing.Size(634, 392);
            this.mPlayer.TabIndex = 0;
            // 
            // mSourceTextBox
            // 
            this.mSourceTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mSourceTextBox.Location = new System.Drawing.Point(13, 411);
            this.mSourceTextBox.Name = "mSourceTextBox";
            this.mSourceTextBox.Size = new System.Drawing.Size(552, 20);
            this.mSourceTextBox.TabIndex = 1;
            this.mSourceTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mSourceTextBox_KeyDown);
            // 
            // mBrowseButton
            // 
            this.mBrowseButton.Location = new System.Drawing.Point(571, 410);
            this.mBrowseButton.Name = "mBrowseButton";
            this.mBrowseButton.Size = new System.Drawing.Size(75, 23);
            this.mBrowseButton.TabIndex = 2;
            this.mBrowseButton.Text = "Browse";
            this.mBrowseButton.UseVisualStyleBackColor = true;
            this.mBrowseButton.Click += new System.EventHandler(this.mBrowseButton_Click);
            // 
            // PlayerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 439);
            this.Controls.Add(this.mBrowseButton);
            this.Controls.Add(this.mSourceTextBox);
            this.Controls.Add(this.mPlayer);
            this.Name = "PlayerForm";
            this.Text = "PlayerForm";
            ((System.ComponentModel.ISupportInitialize)(this.mPlayer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AxWMPLib.AxWindowsMediaPlayer mPlayer;
        private System.Windows.Forms.TextBox mSourceTextBox;
        private System.Windows.Forms.Button mBrowseButton;
    }
}