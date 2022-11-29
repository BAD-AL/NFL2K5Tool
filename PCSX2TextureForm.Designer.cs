namespace NFL2K5Tool
{
    partial class PCSX2TextureForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkTexturesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showYamlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mClearButton = new System.Windows.Forms.Button();
            this.mCloseButton = new System.Windows.Forms.Button();
            this.mTextBox = new NFL2K5Tool.SearchTextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(501, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkTexturesToolStripMenuItem,
            this.showYamlToolStripMenuItem,
            this.clearToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // checkTexturesToolStripMenuItem
            // 
            this.checkTexturesToolStripMenuItem.Name = "checkTexturesToolStripMenuItem";
            this.checkTexturesToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.checkTexturesToolStripMenuItem.Text = "Check Textures";
            this.checkTexturesToolStripMenuItem.Click += new System.EventHandler(this.checkTexturesToolStripMenuItem_Click);
            // 
            // showYamlToolStripMenuItem
            // 
            this.showYamlToolStripMenuItem.Name = "showYamlToolStripMenuItem";
            this.showYamlToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.showYamlToolStripMenuItem.Text = "Show Yaml";
            this.showYamlToolStripMenuItem.Click += new System.EventHandler(this.showYamlToolStripMenuItem_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.Close_Click);
            // 
            // mClearButton
            // 
            this.mClearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.mClearButton.Location = new System.Drawing.Point(330, 360);
            this.mClearButton.Name = "mClearButton";
            this.mClearButton.Size = new System.Drawing.Size(75, 23);
            this.mClearButton.TabIndex = 2;
            this.mClearButton.Text = "&Clear";
            this.mClearButton.UseVisualStyleBackColor = true;
            this.mClearButton.Click += new System.EventHandler(this.mClearButton_Click);
            // 
            // mCloseButton
            // 
            this.mCloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.mCloseButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.mCloseButton.Location = new System.Drawing.Point(420, 360);
            this.mCloseButton.Name = "mCloseButton";
            this.mCloseButton.Size = new System.Drawing.Size(75, 23);
            this.mCloseButton.TabIndex = 3;
            this.mCloseButton.Text = "Close";
            this.mCloseButton.UseVisualStyleBackColor = true;
            this.mCloseButton.Click += new System.EventHandler(this.Close_Click);
            // 
            // mTextBox
            // 
            this.mTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mTextBox.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mTextBox.Location = new System.Drawing.Point(2, 27);
            this.mTextBox.Name = "mTextBox";
            this.mTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.mTextBox.SearchString = null;
            this.mTextBox.Size = new System.Drawing.Size(499, 326);
            this.mTextBox.StatusControl = null;
            this.mTextBox.TabIndex = 0;
            this.mTextBox.Text = "";
            // 
            // PCSX2TextureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 388);
            this.Controls.Add(this.mCloseButton);
            this.Controls.Add(this.mClearButton);
            this.Controls.Add(this.mTextBox);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "PCSX2TextureForm";
            this.Text = "PCSX2TextureForm";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SearchTextBox mTextBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkTexturesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showYamlToolStripMenuItem;
        private System.Windows.Forms.Button mClearButton;
        private System.Windows.Forms.Button mCloseButton;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
    }
}