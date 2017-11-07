namespace NFL2K5Tool
{
    partial class CoachEditForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.lname = new System.Windows.Forms.TextBox();
            this.fname = new System.Windows.Forms.TextBox();
            this.mBodySelectionControl = new NFL2K5Tool.StringSelectionControl();
            ((System.ComponentModel.ISupportInitialize)(this.mFacePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // mFacePictureBox
            // 
            this.mFacePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mFacePictureBox.Location = new System.Drawing.Point(437, 12);
            this.mFacePictureBox.Name = "mFacePictureBox";
            this.mFacePictureBox.Size = new System.Drawing.Size(49, 50);
            this.mFacePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mFacePictureBox.TabIndex = 19;
            this.mFacePictureBox.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Name";
            // 
            // lname
            // 
            this.lname.Location = new System.Drawing.Point(101, 28);
            this.lname.Name = "lname";
            this.lname.Size = new System.Drawing.Size(91, 20);
            this.lname.TabIndex = 17;
            // 
            // fname
            // 
            this.fname.Location = new System.Drawing.Point(10, 28);
            this.fname.Name = "fname";
            this.fname.Size = new System.Drawing.Size(83, 20);
            this.fname.TabIndex = 16;
            // 
            // mBodySelectionControl
            // 
            this.mBodySelectionControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mBodySelectionControl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mBodySelectionControl.Location = new System.Drawing.Point(206, 5);
            this.mBodySelectionControl.Name = "mBodySelectionControl";
            this.mBodySelectionControl.RepresentedValue = typeof(string);
            this.mBodySelectionControl.Size = new System.Drawing.Size(126, 48);
            this.mBodySelectionControl.TabIndex = 21;
            this.mBodySelectionControl.Value = "";
            // 
            // CoachEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 308);
            this.Controls.Add(this.mBodySelectionControl);
            this.Controls.Add(this.mFacePictureBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lname);
            this.Controls.Add(this.fname);
            this.Name = "CoachEditForm";
            this.Text = "CoachEditForm";
            ((System.ComponentModel.ISupportInitialize)(this.mFacePictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox mFacePictureBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox lname;
        private System.Windows.Forms.TextBox fname;
        private StringSelectionControl mBodySelectionControl;
    }
}