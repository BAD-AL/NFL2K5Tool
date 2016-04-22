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
            this.mListContentsButton = new System.Windows.Forms.Button();
            this.mClearButton = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.statusBar1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadSaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadTextFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.applyDataWithoutSavingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugDialogMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listTeamsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listApperanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listAttributesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listScheduleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listFreeAgentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listDraftClassToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scheduleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.teamPlayersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.validateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sortPlayersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editSortFormulasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoUpdateDepthChartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoUpdatePhotoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoUpdatePBPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reuseNamesToConserveNameSpaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.increaseFontSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.decreaseFontSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nameColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mListPlayersButton2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mSaveButton = new System.Windows.Forms.Button();
            this.mLoadTextFileButton = new System.Windows.Forms.Button();
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
            // mListContentsButton
            // 
            this.mListContentsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mListContentsButton.Enabled = false;
            this.mListContentsButton.Location = new System.Drawing.Point(191, 360);
            this.mListContentsButton.Name = "mListContentsButton";
            this.mListContentsButton.Size = new System.Drawing.Size(119, 23);
            this.mListContentsButton.TabIndex = 1;
            this.mListContentsButton.Text = "List &Contents";
            this.mListContentsButton.UseVisualStyleBackColor = true;
            this.mListContentsButton.Click += new System.EventHandler(this.mListContentsButton_Click);
            // 
            // mClearButton
            // 
            this.mClearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mClearButton.Location = new System.Drawing.Point(316, 360);
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
            this.viewToolStripMenuItem,
            this.editToolStripMenuItem,
            this.uIToolStripMenuItem});
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
            this.loadTextFileToolStripMenuItem,
            this.applyDataWithoutSavingToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // loadSaveToolStripMenuItem
            // 
            this.loadSaveToolStripMenuItem.Name = "loadSaveToolStripMenuItem";
            this.loadSaveToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.loadSaveToolStripMenuItem.Text = "&Load Save";
            this.loadSaveToolStripMenuItem.Click += new System.EventHandler(this.loadSaveToolStripMenuItem_Click);
            // 
            // loadTextFileToolStripMenuItem
            // 
            this.loadTextFileToolStripMenuItem.Name = "loadTextFileToolStripMenuItem";
            this.loadTextFileToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.loadTextFileToolStripMenuItem.Text = "LoadTextFile";
            this.loadTextFileToolStripMenuItem.Click += new System.EventHandler(this.loadTextFileAction);
            // 
            // applyDataWithoutSavingToolStripMenuItem
            // 
            this.applyDataWithoutSavingToolStripMenuItem.Name = "applyDataWithoutSavingToolStripMenuItem";
            this.applyDataWithoutSavingToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.applyDataWithoutSavingToolStripMenuItem.Text = "Apply data without saving";
            this.applyDataWithoutSavingToolStripMenuItem.Click += new System.EventHandler(this.applyDataWithoutSavingToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.mSaveButton_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.findToolStripMenuItem,
            this.debugDialogMenuItem,
            this.listTeamsToolStripMenuItem,
            this.listApperanceToolStripMenuItem,
            this.listAttributesToolStripMenuItem,
            this.listScheduleToolStripMenuItem,
            this.listFreeAgentsToolStripMenuItem,
            this.listDraftClassToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
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
            // listScheduleToolStripMenuItem
            // 
            this.listScheduleToolStripMenuItem.Name = "listScheduleToolStripMenuItem";
            this.listScheduleToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.listScheduleToolStripMenuItem.Text = "List Schedule";
            this.listScheduleToolStripMenuItem.Click += new System.EventHandler(this.listScheduleToolStripMenuItem_Click);
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
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scheduleToolStripMenuItem,
            this.teamPlayersToolStripMenuItem,
            this.validateToolStripMenuItem,
            this.sortPlayersToolStripMenuItem,
            this.editSortFormulasToolStripMenuItem,
            this.autoUpdateDepthChartToolStripMenuItem,
            this.autoUpdatePhotoToolStripMenuItem,
            this.autoUpdatePBPToolStripMenuItem,
            this.reuseNamesToConserveNameSpaceToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // scheduleToolStripMenuItem
            // 
            this.scheduleToolStripMenuItem.Name = "scheduleToolStripMenuItem";
            this.scheduleToolStripMenuItem.Size = new System.Drawing.Size(273, 22);
            this.scheduleToolStripMenuItem.Text = "Show &Schedule Now";
            this.scheduleToolStripMenuItem.Click += new System.EventHandler(this.scheduleToolStripMenuItem_Click);
            // 
            // teamPlayersToolStripMenuItem
            // 
            this.teamPlayersToolStripMenuItem.Name = "teamPlayersToolStripMenuItem";
            this.teamPlayersToolStripMenuItem.Size = new System.Drawing.Size(273, 22);
            this.teamPlayersToolStripMenuItem.Text = "Show &Team Players Now";
            this.teamPlayersToolStripMenuItem.Click += new System.EventHandler(this.teamPlayersToolStripMenuItem_Click);
            // 
            // validateToolStripMenuItem
            // 
            this.validateToolStripMenuItem.Name = "validateToolStripMenuItem";
            this.validateToolStripMenuItem.Size = new System.Drawing.Size(273, 22);
            this.validateToolStripMenuItem.Text = "&Validate Players";
            this.validateToolStripMenuItem.Click += new System.EventHandler(this.validateToolStripMenuItem_Click);
            // 
            // sortPlayersToolStripMenuItem
            // 
            this.sortPlayersToolStripMenuItem.Name = "sortPlayersToolStripMenuItem";
            this.sortPlayersToolStripMenuItem.Size = new System.Drawing.Size(273, 22);
            this.sortPlayersToolStripMenuItem.Text = "&Sort Players";
            this.sortPlayersToolStripMenuItem.Click += new System.EventHandler(this.sortPlayersToolStripMenuItem_Click);
            // 
            // editSortFormulasToolStripMenuItem
            // 
            this.editSortFormulasToolStripMenuItem.Name = "editSortFormulasToolStripMenuItem";
            this.editSortFormulasToolStripMenuItem.Size = new System.Drawing.Size(273, 22);
            this.editSortFormulasToolStripMenuItem.Text = "&Edit sort formulas";
            this.editSortFormulasToolStripMenuItem.Click += new System.EventHandler(this.editSortFormulasToolStripMenuItem_Click);
            // 
            // autoUpdateDepthChartToolStripMenuItem
            // 
            this.autoUpdateDepthChartToolStripMenuItem.Name = "autoUpdateDepthChartToolStripMenuItem";
            this.autoUpdateDepthChartToolStripMenuItem.Size = new System.Drawing.Size(273, 22);
            this.autoUpdateDepthChartToolStripMenuItem.Text = "Auto Update &Depth Chart";
            this.autoUpdateDepthChartToolStripMenuItem.Click += new System.EventHandler(this.autoUpdateDepthChartToolStripMenuItem_Click);
            // 
            // autoUpdatePhotoToolStripMenuItem
            // 
            this.autoUpdatePhotoToolStripMenuItem.Name = "autoUpdatePhotoToolStripMenuItem";
            this.autoUpdatePhotoToolStripMenuItem.Size = new System.Drawing.Size(273, 22);
            this.autoUpdatePhotoToolStripMenuItem.Text = "Auto update Photo";
            this.autoUpdatePhotoToolStripMenuItem.Click += new System.EventHandler(this.autoUpdatePhotoToolStripMenuItem_Click);
            // 
            // autoUpdatePBPToolStripMenuItem
            // 
            this.autoUpdatePBPToolStripMenuItem.Name = "autoUpdatePBPToolStripMenuItem";
            this.autoUpdatePBPToolStripMenuItem.Size = new System.Drawing.Size(273, 22);
            this.autoUpdatePBPToolStripMenuItem.Text = "Auto update PBP";
            this.autoUpdatePBPToolStripMenuItem.Click += new System.EventHandler(this.autoUpdatePBPToolStripMenuItem_Click);
            // 
            // reuseNamesToConserveNameSpaceToolStripMenuItem
            // 
            this.reuseNamesToConserveNameSpaceToolStripMenuItem.Name = "reuseNamesToConserveNameSpaceToolStripMenuItem";
            this.reuseNamesToConserveNameSpaceToolStripMenuItem.Size = new System.Drawing.Size(273, 22);
            this.reuseNamesToConserveNameSpaceToolStripMenuItem.Text = "Reuse names to conserve name space";
            this.reuseNamesToConserveNameSpaceToolStripMenuItem.Click += new System.EventHandler(this.reuseNamesToConserveNameSpaceToolStripMenuItem_Click);
            // 
            // uIToolStripMenuItem
            // 
            this.uIToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.increaseFontSizeToolStripMenuItem,
            this.decreaseFontSizeToolStripMenuItem,
            this.nameColorToolStripMenuItem});
            this.uIToolStripMenuItem.Name = "uIToolStripMenuItem";
            this.uIToolStripMenuItem.Size = new System.Drawing.Size(30, 20);
            this.uIToolStripMenuItem.Text = "UI";
            // 
            // increaseFontSizeToolStripMenuItem
            // 
            this.increaseFontSizeToolStripMenuItem.Name = "increaseFontSizeToolStripMenuItem";
            this.increaseFontSizeToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.increaseFontSizeToolStripMenuItem.Text = "&Increase Font size";
            this.increaseFontSizeToolStripMenuItem.Click += new System.EventHandler(this.increaseFontSizeToolStripMenuItem_Click);
            // 
            // decreaseFontSizeToolStripMenuItem
            // 
            this.decreaseFontSizeToolStripMenuItem.Name = "decreaseFontSizeToolStripMenuItem";
            this.decreaseFontSizeToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.decreaseFontSizeToolStripMenuItem.Text = "&Decrease Font Size";
            this.decreaseFontSizeToolStripMenuItem.Click += new System.EventHandler(this.decreaseFontSizeToolStripMenuItem_Click);
            // 
            // nameColorToolStripMenuItem
            // 
            this.nameColorToolStripMenuItem.Name = "nameColorToolStripMenuItem";
            this.nameColorToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.nameColorToolStripMenuItem.Text = "&Name Color";
            this.nameColorToolStripMenuItem.Click += new System.EventHandler(this.nameColorToolStripMenuItem_Click);
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
            // 
            // mSaveButton
            // 
            this.mSaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mSaveButton.Location = new System.Drawing.Point(397, 360);
            this.mSaveButton.Name = "mSaveButton";
            this.mSaveButton.Size = new System.Drawing.Size(75, 23);
            this.mSaveButton.TabIndex = 11;
            this.mSaveButton.Text = "&Save";
            this.mSaveButton.UseVisualStyleBackColor = true;
            this.mSaveButton.Click += new System.EventHandler(this.mSaveButton_Click);
            // 
            // mLoadTextFileButton
            // 
            this.mLoadTextFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mLoadTextFileButton.Location = new System.Drawing.Point(94, 360);
            this.mLoadTextFileButton.Name = "mLoadTextFileButton";
            this.mLoadTextFileButton.Size = new System.Drawing.Size(91, 23);
            this.mLoadTextFileButton.TabIndex = 12;
            this.mLoadTextFileButton.Text = "Load Text File";
            this.mLoadTextFileButton.UseVisualStyleBackColor = true;
            this.mLoadTextFileButton.Click += new System.EventHandler(this.mLoadTextFileButton_Click);
            // 
            // mTextBox
            // 
            this.mTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mTextBox.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mTextBox.Location = new System.Drawing.Point(2, 27);
            this.mTextBox.Name = "mTextBox";
            this.mTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.mTextBox.SearchString = null;
            this.mTextBox.Size = new System.Drawing.Size(674, 327);
            this.mTextBox.StatusControl = null;
            this.mTextBox.TabIndex = 2;
            this.mTextBox.Text = "";
            this.mTextBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TextBox_MouseDown);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 411);
            this.Controls.Add(this.mLoadTextFileButton);
            this.Controls.Add(this.mSaveButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusBar1);
            this.Controls.Add(this.mClearButton);
            this.Controls.Add(this.mTextBox);
            this.Controls.Add(this.mListContentsButton);
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
        private System.Windows.Forms.Button mListContentsButton;
        private SearchTextBox mTextBox;
        private System.Windows.Forms.Button mClearButton;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label statusBar1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
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
        private System.Windows.Forms.ToolStripMenuItem autoUpdateDepthChartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem applyDataWithoutSavingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoUpdatePhotoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoUpdatePBPToolStripMenuItem;
        private System.Windows.Forms.Button mLoadTextFileButton;
        private System.Windows.Forms.ToolStripMenuItem loadTextFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listScheduleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scheduleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem teamPlayersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem validateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sortPlayersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editSortFormulasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem increaseFontSizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem decreaseFontSizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nameColorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reuseNamesToConserveNameSpaceToolStripMenuItem;
    }
}

