namespace ImageCapture
{
    partial class Form1
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
            this.mCaptureButton = new System.Windows.Forms.Button();
            this.mWindowNameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.mOffsetXSpinner = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.mOffsetYSpinner = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.mHeightSpinner = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.mWidthSpinner = new System.Windows.Forms.NumericUpDown();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.mSizePlayerButton = new System.Windows.Forms.Button();
            this.mSaveToFileCheckbox = new System.Windows.Forms.CheckBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.namesFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mTimedCaptureButton = new System.Windows.Forms.Button();
            this.mDeleteGarbageImages = new System.Windows.Forms.Button();
            this.debugLabel = new System.Windows.Forms.Label();
            this.portionFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.mOffsetXSpinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mOffsetYSpinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mHeightSpinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mWidthSpinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mCaptureButton
            // 
            this.mCaptureButton.Location = new System.Drawing.Point(12, 171);
            this.mCaptureButton.Name = "mCaptureButton";
            this.mCaptureButton.Size = new System.Drawing.Size(75, 23);
            this.mCaptureButton.TabIndex = 0;
            this.mCaptureButton.Text = "&Capture";
            this.mCaptureButton.UseVisualStyleBackColor = true;
            this.mCaptureButton.Click += new System.EventHandler(this.mCaptureButton_Click);
            // 
            // mWindowNameTextBox
            // 
            this.mWindowNameTextBox.Location = new System.Drawing.Point(12, 45);
            this.mWindowNameTextBox.Name = "mWindowNameTextBox";
            this.mWindowNameTextBox.Size = new System.Drawing.Size(167, 20);
            this.mWindowNameTextBox.TabIndex = 1;
            this.mWindowNameTextBox.Text = "Windows Media Player";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Window Name";
            // 
            // mOffsetXSpinner
            // 
            this.mOffsetXSpinner.Location = new System.Drawing.Point(16, 92);
            this.mOffsetXSpinner.Maximum = new decimal(new int[] {
            1080,
            0,
            0,
            0});
            this.mOffsetXSpinner.Minimum = new decimal(new int[] {
            1080,
            0,
            0,
            -2147483648});
            this.mOffsetXSpinner.Name = "mOffsetXSpinner";
            this.mOffsetXSpinner.Size = new System.Drawing.Size(54, 20);
            this.mOffsetXSpinner.TabIndex = 3;
            this.mOffsetXSpinner.Value = new decimal(new int[] {
            63,
            0,
            0,
            0});
            this.mOffsetXSpinner.ValueChanged += new System.EventHandler(this.mOffsetYSpinner_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Offset X";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(85, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Offset Y";
            // 
            // mOffsetYSpinner
            // 
            this.mOffsetYSpinner.Location = new System.Drawing.Point(88, 92);
            this.mOffsetYSpinner.Maximum = new decimal(new int[] {
            1080,
            0,
            0,
            0});
            this.mOffsetYSpinner.Minimum = new decimal(new int[] {
            1080,
            0,
            0,
            -2147483648});
            this.mOffsetYSpinner.Name = "mOffsetYSpinner";
            this.mOffsetYSpinner.Size = new System.Drawing.Size(54, 20);
            this.mOffsetYSpinner.TabIndex = 5;
            this.mOffsetYSpinner.Value = new decimal(new int[] {
            302,
            0,
            0,
            -2147483648});
            this.mOffsetYSpinner.ValueChanged += new System.EventHandler(this.mOffsetYSpinner_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(85, 129);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Height";
            // 
            // mHeightSpinner
            // 
            this.mHeightSpinner.Location = new System.Drawing.Point(88, 145);
            this.mHeightSpinner.Maximum = new decimal(new int[] {
            1080,
            0,
            0,
            0});
            this.mHeightSpinner.Name = "mHeightSpinner";
            this.mHeightSpinner.Size = new System.Drawing.Size(54, 20);
            this.mHeightSpinner.TabIndex = 9;
            this.mHeightSpinner.Value = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.mHeightSpinner.ValueChanged += new System.EventHandler(this.mOffsetYSpinner_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 129);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Width";
            // 
            // mWidthSpinner
            // 
            this.mWidthSpinner.Location = new System.Drawing.Point(16, 145);
            this.mWidthSpinner.Maximum = new decimal(new int[] {
            1080,
            0,
            0,
            0});
            this.mWidthSpinner.Name = "mWidthSpinner";
            this.mWidthSpinner.Size = new System.Drawing.Size(54, 20);
            this.mWidthSpinner.TabIndex = 7;
            this.mWidthSpinner.Value = new decimal(new int[] {
            460,
            0,
            0,
            0});
            this.mWidthSpinner.ValueChanged += new System.EventHandler(this.mOffsetYSpinner_ValueChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(185, 27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(237, 397);
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDoubleClick);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            // 
            // mSizePlayerButton
            // 
            this.mSizePlayerButton.Location = new System.Drawing.Point(93, 171);
            this.mSizePlayerButton.Name = "mSizePlayerButton";
            this.mSizePlayerButton.Size = new System.Drawing.Size(75, 23);
            this.mSizePlayerButton.TabIndex = 12;
            this.mSizePlayerButton.Text = "Launch player";
            this.mSizePlayerButton.UseVisualStyleBackColor = true;
            this.mSizePlayerButton.Click += new System.EventHandler(this.mSizePlayerButton_Click);
            // 
            // mSaveToFileCheckbox
            // 
            this.mSaveToFileCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mSaveToFileCheckbox.AutoSize = true;
            this.mSaveToFileCheckbox.Location = new System.Drawing.Point(185, 431);
            this.mSaveToFileCheckbox.Name = "mSaveToFileCheckbox";
            this.mSaveToFileCheckbox.Size = new System.Drawing.Size(86, 17);
            this.mSaveToFileCheckbox.TabIndex = 13;
            this.mSaveToFileCheckbox.Text = "Save To File";
            this.mSaveToFileCheckbox.UseVisualStyleBackColor = true;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Location = new System.Drawing.Point(12, 254);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(163, 157);
            this.pictureBox2.TabIndex = 14;
            this.pictureBox2.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(434, 24);
            this.menuStrip1.TabIndex = 15;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.namesFormToolStripMenuItem,
            this.portionFormToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // namesFormToolStripMenuItem
            // 
            this.namesFormToolStripMenuItem.Name = "namesFormToolStripMenuItem";
            this.namesFormToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.namesFormToolStripMenuItem.Text = "Names Form";
            this.namesFormToolStripMenuItem.Click += new System.EventHandler(this.namesFormToolStripMenuItem_Click);
            // 
            // mTimedCaptureButton
            // 
            this.mTimedCaptureButton.Location = new System.Drawing.Point(12, 194);
            this.mTimedCaptureButton.Name = "mTimedCaptureButton";
            this.mTimedCaptureButton.Size = new System.Drawing.Size(156, 23);
            this.mTimedCaptureButton.TabIndex = 16;
            this.mTimedCaptureButton.Text = "Timed Capture";
            this.mTimedCaptureButton.UseVisualStyleBackColor = true;
            this.mTimedCaptureButton.Click += new System.EventHandler(this.mTimedCaptureButton_Click);
            // 
            // mDeleteGarbageImages
            // 
            this.mDeleteGarbageImages.Location = new System.Drawing.Point(16, 224);
            this.mDeleteGarbageImages.Name = "mDeleteGarbageImages";
            this.mDeleteGarbageImages.Size = new System.Drawing.Size(152, 23);
            this.mDeleteGarbageImages.TabIndex = 17;
            this.mDeleteGarbageImages.Text = "Delete Garbage Images";
            this.mDeleteGarbageImages.UseVisualStyleBackColor = true;
            this.mDeleteGarbageImages.Click += new System.EventHandler(this.mDeleteGarbageImages_Click);
            // 
            // debugLabel
            // 
            this.debugLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.debugLabel.AutoSize = true;
            this.debugLabel.Location = new System.Drawing.Point(277, 429);
            this.debugLabel.Name = "debugLabel";
            this.debugLabel.Size = new System.Drawing.Size(38, 13);
            this.debugLabel.TabIndex = 18;
            this.debugLabel.Text = "Height";
            this.debugLabel.Click += new System.EventHandler(this.debugLabel_Click);
            // 
            // portionFormToolStripMenuItem
            // 
            this.portionFormToolStripMenuItem.Name = "portionFormToolStripMenuItem";
            this.portionFormToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.portionFormToolStripMenuItem.Text = "Portion Form";
            this.portionFormToolStripMenuItem.Click += new System.EventHandler(this.portionFormToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 451);
            this.Controls.Add(this.debugLabel);
            this.Controls.Add(this.mDeleteGarbageImages);
            this.Controls.Add(this.mTimedCaptureButton);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.mSaveToFileCheckbox);
            this.Controls.Add(this.mSizePlayerButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.mHeightSpinner);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.mWidthSpinner);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.mOffsetYSpinner);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.mOffsetXSpinner);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mWindowNameTextBox);
            this.Controls.Add(this.mCaptureButton);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Cappture Screen";
            ((System.ComponentModel.ISupportInitialize)(this.mOffsetXSpinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mOffsetYSpinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mHeightSpinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mWidthSpinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button mCaptureButton;
        private System.Windows.Forms.TextBox mWindowNameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown mOffsetXSpinner;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown mOffsetYSpinner;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown mHeightSpinner;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown mWidthSpinner;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button mSizePlayerButton;
        private System.Windows.Forms.CheckBox mSaveToFileCheckbox;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem namesFormToolStripMenuItem;
        private System.Windows.Forms.Button mTimedCaptureButton;
        private System.Windows.Forms.Button mDeleteGarbageImages;
        private System.Windows.Forms.Label debugLabel;
        private System.Windows.Forms.ToolStripMenuItem portionFormToolStripMenuItem;
    }
}

