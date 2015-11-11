namespace NFL2K5Tool
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mLoadSaveButton = new System.Windows.Forms.Button();
            this.mListPlayersButton = new System.Windows.Forms.Button();
            this.mClearButton = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.statusBar1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadSaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugDialogMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listTeamsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listApperanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listAttributesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listFreeAgentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listDraftClassToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mListPlayersButton2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mSaveButton = new System.Windows.Forms.Button();
            this.mTextBox = new NFL2K5Tool.SearchTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mLoadSaveButton
            // 
            this.mLoadSaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mLoadSaveButton.Location = new System.Drawing.Point(13, 360);
            this.mLoadSaveButton.Name = "mLoadSaveButton";
            this.mLoadSaveButton.Size = new System.Drawing.Size(75, 23);
            this.mLoadSaveButton.TabIndex = 0;
            this.mLoadSaveButton.Text = "&Load Save";
            this.mLoadSaveButton.UseVisualStyleBackColor = true;
            this.mLoadSaveButton.Click += new System.EventHandler(this.mLoadSaveButton_Click);
            // 
            // mListPlayersButton
            // 
            this.mListPlayersButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mListPlayersButton.Enabled = false;
            this.mListPlayersButton.Location = new System.Drawing.Point(94, 360);
            this.mListPlayersButton.Name = "mListPlayersButton";
            this.mListPlayersButton.Size = new System.Drawing.Size(119, 23);
            this.mListPlayersButton.TabIndex = 1;
            this.mListPlayersButton.Text = "List Team &Players";
            this.mListPlayersButton.UseVisualStyleBackColor = true;
            this.mListPlayersButton.Click += new System.EventHandler(this.mListPlayersButton_Click);
            // 
            // mClearButton
            // 
            this.mClearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mClearButton.Location = new System.Drawing.Point(219, 360);
            this.mClearButton.Name = "mClearButton";
            this.mClearButton.Size = new System.Drawing.Size(75, 23);
            this.mClearButton.TabIndex = 3;
            this.mClearButton.Text = "Clear";
            this.mClearButton.UseVisualStyleBackColor = true;
            this.mClearButton.Click += new System.EventHandler(this.mClearButton_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericUpDown1.Location = new System.Drawing.Point(12, 18);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(55, 20);
            this.numericUpDown1.TabIndex = 4;
            this.numericUpDown1.Value = new decimal(new int[] {
            1936,
            0,
            0,
            0});
            // 
            // statusBar1
            // 
            this.statusBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.statusBar1.AutoSize = true;
            this.statusBar1.Location = new System.Drawing.Point(13, 390);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Size = new System.Drawing.Size(59, 13);
            this.statusBar1.TabIndex = 6;
            this.statusBar1.Text = "Load a File";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(678, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadSaveToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // loadSaveToolStripMenuItem
            // 
            this.loadSaveToolStripMenuItem.Name = "loadSaveToolStripMenuItem";
            this.loadSaveToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.loadSaveToolStripMenuItem.Text = "&Load Save";
            this.loadSaveToolStripMenuItem.Click += new System.EventHandler(this.loadSaveToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.findToolStripMenuItem,
            this.debugDialogMenuItem,
            this.listTeamsToolStripMenuItem,
            this.listApperanceToolStripMenuItem,
            this.listAttributesToolStripMenuItem,
            this.listFreeAgentsToolStripMenuItem,
            this.listDraftClassToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // findToolStripMenuItem
            // 
            this.findToolStripMenuItem.Name = "findToolStripMenuItem";
            this.findToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.findToolStripMenuItem.Text = "F&ind";
            this.findToolStripMenuItem.Click += new System.EventHandler(this.findToolStripMenuItem_Click);
            // 
            // debugDialogMenuItem
            // 
            this.debugDialogMenuItem.Enabled = false;
            this.debugDialogMenuItem.Name = "debugDialogMenuItem";
            this.debugDialogMenuItem.Size = new System.Drawing.Size(157, 22);
            this.debugDialogMenuItem.Text = "&Debug Dialog";
            this.debugDialogMenuItem.Click += new System.EventHandler(this.stringToHexToolStripMenuItem_Click);
            // 
            // listTeamsToolStripMenuItem
            // 
            this.listTeamsToolStripMenuItem.Checked = true;
            this.listTeamsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.listTeamsToolStripMenuItem.Name = "listTeamsToolStripMenuItem";
            this.listTeamsToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.listTeamsToolStripMenuItem.Text = "List Teams";
            this.listTeamsToolStripMenuItem.Click += new System.EventHandler(this.listTeamsToolStripMenuItem_Click);
            // 
            // listApperanceToolStripMenuItem
            // 
            this.listApperanceToolStripMenuItem.Checked = true;
            this.listApperanceToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.listApperanceToolStripMenuItem.Name = "listApperanceToolStripMenuItem";
            this.listApperanceToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.listApperanceToolStripMenuItem.Text = "List Apperance";
            this.listApperanceToolStripMenuItem.Click += new System.EventHandler(this.listApperanceToolStripMenuItem_Click);
            // 
            // listAttributesToolStripMenuItem
            // 
            this.listAttributesToolStripMenuItem.Checked = true;
            this.listAttributesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.listAttributesToolStripMenuItem.Name = "listAttributesToolStripMenuItem";
            this.listAttributesToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.listAttributesToolStripMenuItem.Text = "List Attributes";
            this.listAttributesToolStripMenuItem.Click += new System.EventHandler(this.listAttributesToolStripMenuItem_Click);
            // 
            // listFreeAgentsToolStripMenuItem
            // 
            this.listFreeAgentsToolStripMenuItem.Name = "listFreeAgentsToolStripMenuItem";
            this.listFreeAgentsToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.listFreeAgentsToolStripMenuItem.Text = "List Free Agents";
            this.listFreeAgentsToolStripMenuItem.Click += new System.EventHandler(this.listFreeAgentsToolStripMenuItem_Click);
            // 
            // listDraftClassToolStripMenuItem
            // 
            this.listDraftClassToolStripMenuItem.Name = "listDraftClassToolStripMenuItem";
            this.listDraftClassToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.listDraftClassToolStripMenuItem.Text = "List Draft Class";
            this.listDraftClassToolStripMenuItem.Click += new System.EventHandler(this.listDraftClassToolStripMenuItem_Click);
            // 
            // mListPlayersButton2
            // 
            this.mListPlayersButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mListPlayersButton2.Enabled = false;
            this.mListPlayersButton2.Location = new System.Drawing.Point(73, 15);
            this.mListPlayersButton2.Name = "mListPlayersButton2";
            this.mListPlayersButton2.Size = new System.Drawing.Size(88, 23);
            this.mListPlayersButton2.TabIndex = 8;
            this.mListPlayersButton2.Text = "Players";
            this.mListPlayersButton2.UseVisualStyleBackColor = true;
            this.mListPlayersButton2.Click += new System.EventHandler(this.mListPlayersButton2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.numericUpDown1);
            this.groupBox1.Controls.Add(this.mListPlayersButton2);
            this.groupBox1.Location = new System.Drawing.Point(501, 359);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(169, 44);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "List This many";
            this.groupBox1.Visible = false;
            // 
            // mSaveButton
            // 
            this.mSaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mSaveButton.Location = new System.Drawing.Point(300, 360);
            this.mSaveButton.Name = "mSaveButton";
            this.mSaveButton.Size = new System.Drawing.Size(75, 23);
            this.mSaveButton.TabIndex = 11;
            this.mSaveButton.Text = "&Save";
            this.mSaveButton.UseVisualStyleBackColor = true;
            this.mSaveButton.Click += new System.EventHandler(this.mSaveButton_Click);
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
            this.mTextBox.Size = new System.Drawing.Size(674, 327);
            this.mTextBox.StatusControl = null;
            this.mTextBox.TabIndex = 2;
            this.mTextBox.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 411);
            this.Controls.Add(this.mSaveButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusBar1);
            this.Controls.Add(this.mClearButton);
            this.Controls.Add(this.mTextBox);
            this.Controls.Add(this.mListPlayersButton);
            this.Controls.Add(this.mLoadSaveButton);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(694, 200);
            this.Name = "MainForm";
            this.Text = "NFL2K5 Tool";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button mLoadSaveButton;
        private System.Windows.Forms.Button mListPlayersButton;
        private SearchTextBox mTextBox;
        private System.Windows.Forms.Button mClearButton;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label statusBar1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadSaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debugDialogMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listApperanceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listAttributesToolStripMenuItem;
        private System.Windows.Forms.Button mListPlayersButton2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStripMenuItem listFreeAgentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listDraftClassToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listTeamsToolStripMenuItem;
        private System.Windows.Forms.Button mSaveButton;
    }
}

