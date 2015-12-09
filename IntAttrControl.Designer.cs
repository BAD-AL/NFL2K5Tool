namespace NFL2K5Tool
{
    partial class IntAttrControl
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
            this.mAttrUpDown = new System.Windows.Forms.NumericUpDown();
            this.mLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.mAttrUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // mAttrUpDown
            // 
            this.mAttrUpDown.Location = new System.Drawing.Point(6, 17);
            this.mAttrUpDown.Maximum = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.mAttrUpDown.Name = "mAttrUpDown";
            this.mAttrUpDown.Size = new System.Drawing.Size(47, 20);
            this.mAttrUpDown.TabIndex = 14;
            this.mAttrUpDown.ValueChanged += new System.EventHandler(this.mAttrUpDown_ValueChanged);
            // 
            // mLabel
            // 
            this.mLabel.AutoSize = true;
            this.mLabel.Location = new System.Drawing.Point(3, 0);
            this.mLabel.Name = "mLabel";
            this.mLabel.Size = new System.Drawing.Size(102, 13);
            this.mLabel.TabIndex = 13;
            this.mLabel.Text = "PassReadCoverage";
            // 
            // IntAttrControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.mAttrUpDown);
            this.Controls.Add(this.mLabel);
            this.Name = "IntAttrControl";
            this.Size = new System.Drawing.Size(126, 48);
            ((System.ComponentModel.ISupportInitialize)(this.mAttrUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown mAttrUpDown;
        private System.Windows.Forms.Label mLabel;
    }
}
