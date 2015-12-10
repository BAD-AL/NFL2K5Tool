namespace NFL2K5Tool
{
    partial class DateValueControl
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
            this.mLabel = new System.Windows.Forms.Label();
            this.mDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // mLabel
            // 
            this.mLabel.AutoSize = true;
            this.mLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.mLabel.Location = new System.Drawing.Point(0, 4);
            this.mLabel.Name = "mLabel";
            this.mLabel.Size = new System.Drawing.Size(35, 13);
            this.mLabel.TabIndex = 0;
            this.mLabel.Text = "label1";
            // 
            // mDateTimePicker
            // 
            this.mDateTimePicker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mDateTimePicker.CustomFormat = "MM/dd/yyy";
            this.mDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.mDateTimePicker.Location = new System.Drawing.Point(4, 21);
            this.mDateTimePicker.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.mDateTimePicker.MinDate = new System.DateTime(1954, 1, 1, 0, 0, 0, 0);
            this.mDateTimePicker.Name = "mDateTimePicker";
            this.mDateTimePicker.Size = new System.Drawing.Size(119, 20);
            this.mDateTimePicker.TabIndex = 1;
            this.mDateTimePicker.ValueChanged += new System.EventHandler(this.DateChanged);
            // 
            // DateValueControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.mDateTimePicker);
            this.Controls.Add(this.mLabel);
            this.Name = "DateValueControl";
            this.Size = new System.Drawing.Size(126, 48);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label mLabel;
        private System.Windows.Forms.DateTimePicker mDateTimePicker;

    }
}
