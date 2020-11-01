namespace NFL2K5Tool
{
    partial class ARMaxTestForm
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
            this.mInternalFilesListBox = new System.Windows.Forms.ListBox();
            this.mSaveFileNameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.mExtractButton = new System.Windows.Forms.Button();
            this.mFileExistsButton = new System.Windows.Forms.Button();
            this.mFileExistsTextBox = new System.Windows.Forms.TextBox();
            this.mFileExistsLabel = new System.Windows.Forms.Label();
            this.mAddFileButton = new System.Windows.Forms.Button();
            this.mDeleteFileBuggon = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mInternalFilesListBox
            // 
            this.mInternalFilesListBox.AllowDrop = true;
            this.mInternalFilesListBox.FormattingEnabled = true;
            this.mInternalFilesListBox.Location = new System.Drawing.Point(3, 68);
            this.mInternalFilesListBox.Name = "mInternalFilesListBox";
            this.mInternalFilesListBox.Size = new System.Drawing.Size(238, 225);
            this.mInternalFilesListBox.TabIndex = 0;
            this.mInternalFilesListBox.DragOver += new System.Windows.Forms.DragEventHandler(this.mSaveFileNameTextBox_DragOver);
            this.mInternalFilesListBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.mInternalFilesListBox_DragDrop);
            // 
            // mSaveFileNameTextBox
            // 
            this.mSaveFileNameTextBox.AllowDrop = true;
            this.mSaveFileNameTextBox.Location = new System.Drawing.Point(69, 7);
            this.mSaveFileNameTextBox.Name = "mSaveFileNameTextBox";
            this.mSaveFileNameTextBox.Size = new System.Drawing.Size(428, 20);
            this.mSaveFileNameTextBox.TabIndex = 1;
            this.mSaveFileNameTextBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.mSaveFileNameTextBox_DragDrop);
            this.mSaveFileNameTextBox.DragOver += new System.Windows.Forms.DragEventHandler(this.mSaveFileNameTextBox_DragOver);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Save File";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Files in Save";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(191, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(152, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Drag file into \'save file\' text box";
            // 
            // mExtractButton
            // 
            this.mExtractButton.Enabled = false;
            this.mExtractButton.Location = new System.Drawing.Point(3, 299);
            this.mExtractButton.Name = "mExtractButton";
            this.mExtractButton.Size = new System.Drawing.Size(126, 23);
            this.mExtractButton.TabIndex = 5;
            this.mExtractButton.Text = "Extract Contents";
            this.mExtractButton.UseVisualStyleBackColor = true;
            this.mExtractButton.Click += new System.EventHandler(this.mExtractButton_Click);
            // 
            // mFileExistsButton
            // 
            this.mFileExistsButton.Location = new System.Drawing.Point(286, 68);
            this.mFileExistsButton.Name = "mFileExistsButton";
            this.mFileExistsButton.Size = new System.Drawing.Size(75, 23);
            this.mFileExistsButton.TabIndex = 6;
            this.mFileExistsButton.Text = "File Exists";
            this.mFileExistsButton.UseVisualStyleBackColor = true;
            this.mFileExistsButton.Click += new System.EventHandler(this.mFileExistsButton_Click);
            // 
            // mFileExistsTextBox
            // 
            this.mFileExistsTextBox.Location = new System.Drawing.Point(367, 70);
            this.mFileExistsTextBox.Name = "mFileExistsTextBox";
            this.mFileExistsTextBox.Size = new System.Drawing.Size(130, 20);
            this.mFileExistsTextBox.TabIndex = 7;
            // 
            // mFileExistsLabel
            // 
            this.mFileExistsLabel.AutoSize = true;
            this.mFileExistsLabel.Location = new System.Drawing.Point(292, 94);
            this.mFileExistsLabel.Name = "mFileExistsLabel";
            this.mFileExistsLabel.Size = new System.Drawing.Size(61, 13);
            this.mFileExistsLabel.TabIndex = 8;
            this.mFileExistsLabel.Text = "--file exists--";
            // 
            // mAddFileButton
            // 
            this.mAddFileButton.Location = new System.Drawing.Point(3, 357);
            this.mAddFileButton.Name = "mAddFileButton";
            this.mAddFileButton.Size = new System.Drawing.Size(126, 23);
            this.mAddFileButton.TabIndex = 10;
            this.mAddFileButton.Text = "Add File To Save";
            this.mAddFileButton.UseVisualStyleBackColor = true;
            this.mAddFileButton.Click += new System.EventHandler(this.mAddFileButton_Click);
            // 
            // mDeleteFileBuggon
            // 
            this.mDeleteFileBuggon.Location = new System.Drawing.Point(3, 386);
            this.mDeleteFileBuggon.Name = "mDeleteFileBuggon";
            this.mDeleteFileBuggon.Size = new System.Drawing.Size(126, 23);
            this.mDeleteFileBuggon.TabIndex = 11;
            this.mDeleteFileBuggon.Text = "Delete File From Save";
            this.mDeleteFileBuggon.UseVisualStyleBackColor = true;
            this.mDeleteFileBuggon.Click += new System.EventHandler(this.mDeleteFileButton_Click);
            // 
            // ARMaxTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 487);
            this.Controls.Add(this.mDeleteFileBuggon);
            this.Controls.Add(this.mAddFileButton);
            this.Controls.Add(this.mFileExistsLabel);
            this.Controls.Add(this.mFileExistsTextBox);
            this.Controls.Add(this.mFileExistsButton);
            this.Controls.Add(this.mExtractButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mSaveFileNameTextBox);
            this.Controls.Add(this.mInternalFilesListBox);
            this.Name = "ARMaxTestForm";
            this.Text = "ARMaxTestForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox mInternalFilesListBox;
        private System.Windows.Forms.TextBox mSaveFileNameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button mExtractButton;
        private System.Windows.Forms.Button mFileExistsButton;
        private System.Windows.Forms.TextBox mFileExistsTextBox;
        private System.Windows.Forms.Label mFileExistsLabel;
        private System.Windows.Forms.Button mAddFileButton;
        private System.Windows.Forms.Button mDeleteFileBuggon;
    }
}

