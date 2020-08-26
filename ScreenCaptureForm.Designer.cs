namespace NFL2K5Tool
{
    partial class ScreenCaptureForm
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
            this.mPictureBox = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.mCaptureButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.mHeightUpDown = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.mWidthUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.mYUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.mXUpDown = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.mLocationTextBox = new System.Windows.Forms.TextBox();
            this.mCancelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.mPictureBox)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mHeightUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mWidthUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mYUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mXUpDown)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // mPictureBox
            // 
            this.mPictureBox.Location = new System.Drawing.Point(12, 12);
            this.mPictureBox.Name = "mPictureBox";
            this.mPictureBox.Size = new System.Drawing.Size(190, 222);
            this.mPictureBox.TabIndex = 0;
            this.mPictureBox.TabStop = false;
            this.mPictureBox.Click += new System.EventHandler(this.mPictureBox_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.mCaptureButton);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.mHeightUpDown);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.mWidthUpDown);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.mYUpDown);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.mXUpDown);
            this.groupBox1.Location = new System.Drawing.Point(233, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(155, 224);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 152);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(127, 23);
            this.button1.TabIndex = 19;
            this.button1.Text = "Save PB img";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(0, 188);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(108, 17);
            this.checkBox1.TabIndex = 18;
            this.checkBox1.Text = "Save on PB click";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // mCaptureButton
            // 
            this.mCaptureButton.Location = new System.Drawing.Point(6, 123);
            this.mCaptureButton.Name = "mCaptureButton";
            this.mCaptureButton.Size = new System.Drawing.Size(127, 23);
            this.mCaptureButton.TabIndex = 17;
            this.mCaptureButton.Text = "&Capture";
            this.mCaptureButton.UseVisualStyleBackColor = true;
            this.mCaptureButton.Click += new System.EventHandler(this.mCaptureButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Height";
            // 
            // mHeightUpDown
            // 
            this.mHeightUpDown.Location = new System.Drawing.Point(49, 97);
            this.mHeightUpDown.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.mHeightUpDown.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.mHeightUpDown.Name = "mHeightUpDown";
            this.mHeightUpDown.Size = new System.Drawing.Size(84, 20);
            this.mHeightUpDown.TabIndex = 15;
            this.mHeightUpDown.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.mHeightUpDown.ValueChanged += new System.EventHandler(this.upDown_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Width";
            // 
            // mWidthUpDown
            // 
            this.mWidthUpDown.Location = new System.Drawing.Point(49, 71);
            this.mWidthUpDown.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.mWidthUpDown.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.mWidthUpDown.Name = "mWidthUpDown";
            this.mWidthUpDown.Size = new System.Drawing.Size(84, 20);
            this.mWidthUpDown.TabIndex = 13;
            this.mWidthUpDown.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.mWidthUpDown.ValueChanged += new System.EventHandler(this.upDown_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Y";
            // 
            // mYUpDown
            // 
            this.mYUpDown.Location = new System.Drawing.Point(49, 45);
            this.mYUpDown.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.mYUpDown.Name = "mYUpDown";
            this.mYUpDown.Size = new System.Drawing.Size(84, 20);
            this.mYUpDown.TabIndex = 11;
            this.mYUpDown.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.mYUpDown.ValueChanged += new System.EventHandler(this.upDown_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "X";
            // 
            // mXUpDown
            // 
            this.mXUpDown.Location = new System.Drawing.Point(49, 19);
            this.mXUpDown.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.mXUpDown.Name = "mXUpDown";
            this.mXUpDown.Size = new System.Drawing.Size(84, 20);
            this.mXUpDown.TabIndex = 9;
            this.mXUpDown.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.mXUpDown.ValueChanged += new System.EventHandler(this.upDown_ValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.mLocationTextBox);
            this.groupBox2.Location = new System.Drawing.Point(12, 291);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(376, 40);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Location";
            // 
            // mLocationTextBox
            // 
            this.mLocationTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mLocationTextBox.Location = new System.Drawing.Point(6, 14);
            this.mLocationTextBox.Name = "mLocationTextBox";
            this.mLocationTextBox.Size = new System.Drawing.Size(364, 20);
            this.mLocationTextBox.TabIndex = 0;
            // 
            // mCancelButton
            // 
            this.mCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.mCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.mCancelButton.Location = new System.Drawing.Point(402, 262);
            this.mCancelButton.Name = "mCancelButton";
            this.mCancelButton.Size = new System.Drawing.Size(75, 23);
            this.mCancelButton.TabIndex = 11;
            this.mCancelButton.Text = "&Cancel";
            this.mCancelButton.UseVisualStyleBackColor = true;
            // 
            // ScreenCaptureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.mCancelButton;
            this.ClientSize = new System.Drawing.Size(394, 335);
            this.Controls.Add(this.mCancelButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.mPictureBox);
            this.Name = "ScreenCaptureForm";
            this.Text = "ScreenCaptureForm";
            ((System.ComponentModel.ISupportInitialize)(this.mPictureBox)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mHeightUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mWidthUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mYUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mXUpDown)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox mPictureBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button mCaptureButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown mHeightUpDown;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown mWidthUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown mYUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown mXUpDown;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox mLocationTextBox;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button mCancelButton;
    }
}