namespace NFL2K5Tool
{
    partial class PhotoDataEditor
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
            this.mPhotoPictureBox = new System.Windows.Forms.PictureBox();
            this.mPhotoUpDown = new System.Windows.Forms.NumericUpDown();
            this.mDeleteLinkButton = new System.Windows.Forms.Button();
            this.mNameBox = new System.Windows.Forms.TextBox();
            this.mTextBox1 = new NFL2K5Tool.SearchTextBox();
            this.mTextBox2 = new NFL2K5Tool.SearchTextBox();
            this.mThinButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.mPhotoPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mPhotoUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // mPhotoPictureBox
            // 
            this.mPhotoPictureBox.Location = new System.Drawing.Point(2, 12);
            this.mPhotoPictureBox.Name = "mPhotoPictureBox";
            this.mPhotoPictureBox.Size = new System.Drawing.Size(100, 108);
            this.mPhotoPictureBox.TabIndex = 0;
            this.mPhotoPictureBox.TabStop = false;
            // 
            // mPhotoUpDown
            // 
            this.mPhotoUpDown.Location = new System.Drawing.Point(2, 126);
            this.mPhotoUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.mPhotoUpDown.Name = "mPhotoUpDown";
            this.mPhotoUpDown.Size = new System.Drawing.Size(100, 20);
            this.mPhotoUpDown.TabIndex = 1;
            this.mPhotoUpDown.ValueChanged += new System.EventHandler(this.mPhotoUpDown_ValueChanged);
            // 
            // mDeleteLinkButton
            // 
            this.mDeleteLinkButton.Location = new System.Drawing.Point(2, 174);
            this.mDeleteLinkButton.Name = "mDeleteLinkButton";
            this.mDeleteLinkButton.Size = new System.Drawing.Size(92, 23);
            this.mDeleteLinkButton.TabIndex = 2;
            this.mDeleteLinkButton.Text = "Delete Link";
            this.mDeleteLinkButton.UseVisualStyleBackColor = true;
            // 
            // mNameBox
            // 
            this.mNameBox.Location = new System.Drawing.Point(2, 148);
            this.mNameBox.Name = "mNameBox";
            this.mNameBox.Size = new System.Drawing.Size(100, 20);
            this.mNameBox.TabIndex = 3;
            // 
            // mTextBox1
            // 
            this.mTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mTextBox1.Location = new System.Drawing.Point(138, 13);
            this.mTextBox1.MaxLength = 32767000;
            this.mTextBox1.Name = "mTextBox1";
            this.mTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.mTextBox1.SearchString = null;
            this.mTextBox1.Size = new System.Drawing.Size(274, 376);
            this.mTextBox1.StatusControl = null;
            this.mTextBox1.TabIndex = 4;
            this.mTextBox1.Text = "";
            // 
            // mTextBox2
            // 
            this.mTextBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mTextBox2.Location = new System.Drawing.Point(424, 12);
            this.mTextBox2.MaxLength = 32767000;
            this.mTextBox2.Name = "mTextBox2";
            this.mTextBox2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.mTextBox2.SearchString = null;
            this.mTextBox2.Size = new System.Drawing.Size(295, 376);
            this.mTextBox2.StatusControl = null;
            this.mTextBox2.TabIndex = 5;
            this.mTextBox2.Text = "";
            // 
            // mThinButton
            // 
            this.mThinButton.Location = new System.Drawing.Point(13, 354);
            this.mThinButton.Name = "mThinButton";
            this.mThinButton.Size = new System.Drawing.Size(75, 23);
            this.mThinButton.TabIndex = 6;
            this.mThinButton.Text = "Thin it!";
            this.mThinButton.UseVisualStyleBackColor = true;
            this.mThinButton.Click += new System.EventHandler(this.mThinButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 303);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // PhotoDataEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 401);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.mThinButton);
            this.Controls.Add(this.mTextBox2);
            this.Controls.Add(this.mTextBox1);
            this.Controls.Add(this.mNameBox);
            this.Controls.Add(this.mDeleteLinkButton);
            this.Controls.Add(this.mPhotoUpDown);
            this.Controls.Add(this.mPhotoPictureBox);
            this.Name = "PhotoDataEditor";
            this.Text = "PhotoDataEditor";
            ((System.ComponentModel.ISupportInitialize)(this.mPhotoPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mPhotoUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox mPhotoPictureBox;
        private System.Windows.Forms.NumericUpDown mPhotoUpDown;
        private System.Windows.Forms.Button mDeleteLinkButton;
        private System.Windows.Forms.TextBox mNameBox;
        private SearchTextBox mTextBox1;
        private SearchTextBox mTextBox2;
        private System.Windows.Forms.Button mThinButton;
        private System.Windows.Forms.Button button1;
    }
}