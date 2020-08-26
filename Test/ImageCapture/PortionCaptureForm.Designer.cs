namespace ImageCapture
{
    partial class PortionCaptureForm
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
            this.label4 = new System.Windows.Forms.Label();
            this.mHeightSpinner = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.mWidthSpinner = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.mOffsetYSpinner = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.mOffsetXSpinner = new System.Windows.Forms.NumericUpDown();
            this.mCaptureButton = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.mPreviousButton = new System.Windows.Forms.Button();
            this.mNextButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.mHeightSpinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mWidthSpinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mOffsetYSpinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mOffsetXSpinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(84, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Height";
            // 
            // mHeightSpinner
            // 
            this.mHeightSpinner.Location = new System.Drawing.Point(87, 78);
            this.mHeightSpinner.Maximum = new decimal(new int[] {
            1080,
            0,
            0,
            0});
            this.mHeightSpinner.Name = "mHeightSpinner";
            this.mHeightSpinner.Size = new System.Drawing.Size(54, 20);
            this.mHeightSpinner.TabIndex = 20;
            this.mHeightSpinner.Value = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.mHeightSpinner.ValueChanged += new System.EventHandler(this.mWidthSpinner_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Width";
            // 
            // mWidthSpinner
            // 
            this.mWidthSpinner.Location = new System.Drawing.Point(15, 78);
            this.mWidthSpinner.Maximum = new decimal(new int[] {
            1080,
            0,
            0,
            0});
            this.mWidthSpinner.Name = "mWidthSpinner";
            this.mWidthSpinner.Size = new System.Drawing.Size(54, 20);
            this.mWidthSpinner.TabIndex = 18;
            this.mWidthSpinner.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.mWidthSpinner.ValueChanged += new System.EventHandler(this.mWidthSpinner_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(84, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Offset Y";
            // 
            // mOffsetYSpinner
            // 
            this.mOffsetYSpinner.Location = new System.Drawing.Point(87, 25);
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
            this.mOffsetYSpinner.TabIndex = 16;
            this.mOffsetYSpinner.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.mOffsetYSpinner.ValueChanged += new System.EventHandler(this.mWidthSpinner_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Offset X";
            // 
            // mOffsetXSpinner
            // 
            this.mOffsetXSpinner.Location = new System.Drawing.Point(15, 25);
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
            this.mOffsetXSpinner.TabIndex = 14;
            this.mOffsetXSpinner.Value = new decimal(new int[] {
            350,
            0,
            0,
            0});
            this.mOffsetXSpinner.ValueChanged += new System.EventHandler(this.mWidthSpinner_ValueChanged);
            // 
            // mCaptureButton
            // 
            this.mCaptureButton.Location = new System.Drawing.Point(11, 104);
            this.mCaptureButton.Name = "mCaptureButton";
            this.mCaptureButton.Size = new System.Drawing.Size(75, 23);
            this.mCaptureButton.TabIndex = 13;
            this.mCaptureButton.Text = "&Capture";
            this.mCaptureButton.UseVisualStyleBackColor = true;
            this.mCaptureButton.Click += new System.EventHandler(this.mCaptureButton_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Location = new System.Drawing.Point(0, 133);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(205, 181);
            this.pictureBox2.TabIndex = 24;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(211, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(435, 370);
            this.pictureBox1.TabIndex = 23;
            this.pictureBox1.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(0, 321);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(205, 20);
            this.textBox1.TabIndex = 25;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // mPreviousButton
            // 
            this.mPreviousButton.Location = new System.Drawing.Point(0, 347);
            this.mPreviousButton.Name = "mPreviousButton";
            this.mPreviousButton.Size = new System.Drawing.Size(75, 23);
            this.mPreviousButton.TabIndex = 26;
            this.mPreviousButton.Text = "&Previous";
            this.mPreviousButton.UseVisualStyleBackColor = true;
            this.mPreviousButton.Click += new System.EventHandler(this.mPreviousButton_Click);
            // 
            // mNextButton
            // 
            this.mNextButton.Location = new System.Drawing.Point(81, 347);
            this.mNextButton.Name = "mNextButton";
            this.mNextButton.Size = new System.Drawing.Size(75, 23);
            this.mNextButton.TabIndex = 27;
            this.mNextButton.Text = "&Next";
            this.mNextButton.UseVisualStyleBackColor = true;
            this.mNextButton.Click += new System.EventHandler(this.mNextButton_Click);
            // 
            // PortionCaptureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 394);
            this.Controls.Add(this.mNextButton);
            this.Controls.Add(this.mPreviousButton);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.mHeightSpinner);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.mWidthSpinner);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.mOffsetYSpinner);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.mOffsetXSpinner);
            this.Controls.Add(this.mCaptureButton);
            this.Name = "PortionCaptureForm";
            this.Text = "PortionCaptureForm";
            ((System.ComponentModel.ISupportInitialize)(this.mHeightSpinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mWidthSpinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mOffsetYSpinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mOffsetXSpinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown mHeightSpinner;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown mWidthSpinner;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown mOffsetYSpinner;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown mOffsetXSpinner;
        private System.Windows.Forms.Button mCaptureButton;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button mPreviousButton;
        private System.Windows.Forms.Button mNextButton;
    }
}