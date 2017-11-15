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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.mStatsTab = new System.Windows.Forms.TabPage();
            this.mStrategyTab = new System.Windows.Forms.TabPage();
            this.mBodySelectionControl = new NFL2K5Tool.StringSelectionControl();
            this.mBodyTab = new System.Windows.Forms.TabPage();
            this.mBodyPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.mFacePictureBox)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.mBodyTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mBodyPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // mFacePictureBox
            // 
            this.mFacePictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mFacePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mFacePictureBox.Location = new System.Drawing.Point(437, 5);
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
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.mStatsTab);
            this.tabControl1.Controls.Add(this.mStrategyTab);
            this.tabControl1.Controls.Add(this.mBodyTab);
            this.tabControl1.Location = new System.Drawing.Point(6, 63);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(489, 354);
            this.tabControl1.TabIndex = 22;
            // 
            // mStatsTab
            // 
            this.mStatsTab.Location = new System.Drawing.Point(4, 22);
            this.mStatsTab.Name = "mStatsTab";
            this.mStatsTab.Padding = new System.Windows.Forms.Padding(3);
            this.mStatsTab.Size = new System.Drawing.Size(481, 328);
            this.mStatsTab.TabIndex = 0;
            this.mStatsTab.Text = "Stats";
            this.mStatsTab.UseVisualStyleBackColor = true;
            // 
            // mStrategyTab
            // 
            this.mStrategyTab.Location = new System.Drawing.Point(4, 22);
            this.mStrategyTab.Name = "mStrategyTab";
            this.mStrategyTab.Padding = new System.Windows.Forms.Padding(3);
            this.mStrategyTab.Size = new System.Drawing.Size(481, 328);
            this.mStrategyTab.TabIndex = 1;
            this.mStrategyTab.Text = "Strategy";
            this.mStrategyTab.UseVisualStyleBackColor = true;
            // 
            // mBodySelectionControl
            // 
            this.mBodySelectionControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mBodySelectionControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mBodySelectionControl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mBodySelectionControl.Location = new System.Drawing.Point(350, 3);
            this.mBodySelectionControl.Name = "mBodySelectionControl";
            this.mBodySelectionControl.RepresentedValue = typeof(string);
            this.mBodySelectionControl.Size = new System.Drawing.Size(126, 48);
            this.mBodySelectionControl.TabIndex = 21;
            this.mBodySelectionControl.Value = "";
            this.mBodySelectionControl.ValueChanged += new System.EventHandler(this.mBodySelectionControl_ValueChanged);
            // 
            // mBodyTab
            // 
            this.mBodyTab.Controls.Add(this.mBodyPictureBox);
            this.mBodyTab.Controls.Add(this.mBodySelectionControl);
            this.mBodyTab.Location = new System.Drawing.Point(4, 22);
            this.mBodyTab.Name = "mBodyTab";
            this.mBodyTab.Size = new System.Drawing.Size(481, 328);
            this.mBodyTab.TabIndex = 2;
            this.mBodyTab.Text = "Body";
            this.mBodyTab.UseVisualStyleBackColor = true;
            // 
            // mBodyPictureBox
            // 
            this.mBodyPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.mBodyPictureBox.Location = new System.Drawing.Point(3, 3);
            this.mBodyPictureBox.Name = "mBodyPictureBox";
            this.mBodyPictureBox.Size = new System.Drawing.Size(296, 322);
            this.mBodyPictureBox.TabIndex = 0;
            this.mBodyPictureBox.TabStop = false;
            // 
            // CoachEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 418);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.mFacePictureBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lname);
            this.Controls.Add(this.fname);
            this.Name = "CoachEditForm";
            this.Text = "CoachEditForm";
            ((System.ComponentModel.ISupportInitialize)(this.mFacePictureBox)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.mBodyTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mBodyPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox mFacePictureBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox lname;
        private System.Windows.Forms.TextBox fname;
        private StringSelectionControl mBodySelectionControl;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage mStatsTab;
        private System.Windows.Forms.TabPage mStrategyTab;
        private System.Windows.Forms.TabPage mBodyTab;
        private System.Windows.Forms.PictureBox mBodyPictureBox;
    }
}