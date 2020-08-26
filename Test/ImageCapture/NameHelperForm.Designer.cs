namespace ImageCapture
{
    partial class NameHelperForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.mErrorTextBox = new System.Windows.Forms.RichTextBox();
            this.mGeneratePhotoDataButton = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 23);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(99, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "&Go";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // mErrorTextBox
            // 
            this.mErrorTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mErrorTextBox.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mErrorTextBox.Location = new System.Drawing.Point(12, 91);
            this.mErrorTextBox.Name = "mErrorTextBox";
            this.mErrorTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.mErrorTextBox.Size = new System.Drawing.Size(482, 202);
            this.mErrorTextBox.TabIndex = 1;
            this.mErrorTextBox.Text = "";
            // 
            // mGeneratePhotoDataButton
            // 
            this.mGeneratePhotoDataButton.Location = new System.Drawing.Point(177, 23);
            this.mGeneratePhotoDataButton.Name = "mGeneratePhotoDataButton";
            this.mGeneratePhotoDataButton.Size = new System.Drawing.Size(117, 23);
            this.mGeneratePhotoDataButton.TabIndex = 2;
            this.mGeneratePhotoDataButton.Text = "Generate Photo Data";
            this.mGeneratePhotoDataButton.UseVisualStyleBackColor = true;
            this.mGeneratePhotoDataButton.Click += new System.EventHandler(this.mGeneratePhotoDataButton_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(301, 23);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(82, 20);
            this.numericUpDown1.TabIndex = 3;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // NameHelperForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 298);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.mGeneratePhotoDataButton);
            this.Controls.Add(this.mErrorTextBox);
            this.Controls.Add(this.button1);
            this.Name = "NameHelperForm";
            this.Text = "NameHelperForm";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox mErrorTextBox;
        private System.Windows.Forms.Button mGeneratePhotoDataButton;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
    }
}