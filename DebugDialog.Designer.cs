namespace NFL2K5Tool
{
    partial class DebugDialog
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.mFindButton = new System.Windows.Forms.Button();
            this.mFindPointers = new System.Windows.Forms.Button();
            this.mPointsToLocButton = new System.Windows.Forms.Button();
            this.mNameTextBox = new System.Windows.Forms.TextBox();
            this.mPlayerUpDown = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mSetLastNameButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.mSetFirstNameButton = new System.Windows.Forms.Button();
            this.mPlayerNameTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.mPlayerUpDown)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(13, 24);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(294, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(12, 84);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(411, 20);
            this.textBox2.TabIndex = 1;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(12, 111);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(66, 17);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "Unicode";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // textBox3
            // 
            this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox3.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3.Location = new System.Drawing.Point(11, 134);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox3.Size = new System.Drawing.Size(411, 81);
            this.textBox3.TabIndex = 3;
            // 
            // mFindButton
            // 
            this.mFindButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mFindButton.Location = new System.Drawing.Point(313, 2);
            this.mFindButton.Name = "mFindButton";
            this.mFindButton.Size = new System.Drawing.Size(99, 23);
            this.mFindButton.TabIndex = 4;
            this.mFindButton.Text = "Find in savefile";
            this.mFindButton.UseVisualStyleBackColor = true;
            this.mFindButton.Click += new System.EventHandler(this.mFindButton_Click);
            // 
            // mFindPointers
            // 
            this.mFindPointers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mFindPointers.Location = new System.Drawing.Point(313, 31);
            this.mFindPointers.Name = "mFindPointers";
            this.mFindPointers.Size = new System.Drawing.Size(99, 23);
            this.mFindPointers.TabIndex = 5;
            this.mFindPointers.Text = "Find pointers";
            this.mFindPointers.UseVisualStyleBackColor = true;
            this.mFindPointers.Click += new System.EventHandler(this.mFindPointers_Click);
            // 
            // mPointsToLocButton
            // 
            this.mPointsToLocButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mPointsToLocButton.Location = new System.Drawing.Point(313, 55);
            this.mPointsToLocButton.Name = "mPointsToLocButton";
            this.mPointsToLocButton.Size = new System.Drawing.Size(99, 23);
            this.mPointsToLocButton.TabIndex = 6;
            this.mPointsToLocButton.Text = "Points To Loc";
            this.mPointsToLocButton.UseVisualStyleBackColor = true;
            this.mPointsToLocButton.Click += new System.EventHandler(this.mPointsToLocButton_Click);
            // 
            // mNameTextBox
            // 
            this.mNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mNameTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mNameTextBox.Location = new System.Drawing.Point(99, 19);
            this.mNameTextBox.Name = "mNameTextBox";
            this.mNameTextBox.Size = new System.Drawing.Size(157, 20);
            this.mNameTextBox.TabIndex = 7;
            // 
            // mPlayerUpDown
            // 
            this.mPlayerUpDown.Location = new System.Drawing.Point(43, 19);
            this.mPlayerUpDown.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.mPlayerUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.mPlayerUpDown.Name = "mPlayerUpDown";
            this.mPlayerUpDown.Size = new System.Drawing.Size(50, 20);
            this.mPlayerUpDown.TabIndex = 8;
            this.mPlayerUpDown.Value = new decimal(new int[] {
            2317,
            0,
            0,
            0});
            this.mPlayerUpDown.ValueChanged += new System.EventHandler(this.mPlayerUpDown_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.mPlayerNameTextBox);
            this.groupBox1.Controls.Add(this.mSetLastNameButton);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.mSetFirstNameButton);
            this.groupBox1.Controls.Add(this.mPlayerUpDown);
            this.groupBox1.Controls.Add(this.mNameTextBox);
            this.groupBox1.Location = new System.Drawing.Point(13, 221);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(274, 94);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Set Name debug";
            // 
            // mSetLastNameButton
            // 
            this.mSetLastNameButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mSetLastNameButton.Location = new System.Drawing.Point(147, 65);
            this.mSetLastNameButton.Name = "mSetLastNameButton";
            this.mSetLastNameButton.Size = new System.Drawing.Size(109, 23);
            this.mSetLastNameButton.TabIndex = 12;
            this.mSetLastNameButton.Text = "Set Last Name";
            this.mSetLastNameButton.UseVisualStyleBackColor = true;
            this.mSetLastNameButton.Click += new System.EventHandler(this.mSetLastNameButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Player";
            // 
            // mSetFirstNameButton
            // 
            this.mSetFirstNameButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mSetFirstNameButton.Location = new System.Drawing.Point(32, 65);
            this.mSetFirstNameButton.Name = "mSetFirstNameButton";
            this.mSetFirstNameButton.Size = new System.Drawing.Size(109, 23);
            this.mSetFirstNameButton.TabIndex = 10;
            this.mSetFirstNameButton.Text = "Set First Name";
            this.mSetFirstNameButton.UseVisualStyleBackColor = true;
            this.mSetFirstNameButton.Click += new System.EventHandler(this.mSetFirstNameButton_Click);
            // 
            // mPlayerNameTextBox
            // 
            this.mPlayerNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mPlayerNameTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mPlayerNameTextBox.Location = new System.Drawing.Point(9, 45);
            this.mPlayerNameTextBox.Name = "mPlayerNameTextBox";
            this.mPlayerNameTextBox.ReadOnly = true;
            this.mPlayerNameTextBox.Size = new System.Drawing.Size(157, 20);
            this.mPlayerNameTextBox.TabIndex = 13;
            // 
            // DebugDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 318);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.mPointsToLocButton);
            this.Controls.Add(this.mFindPointers);
            this.Controls.Add(this.mFindButton);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Name = "DebugDialog";
            this.Text = "Debug Dialog";
            ((System.ComponentModel.ISupportInitialize)(this.mPlayerUpDown)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button mFindButton;
        private System.Windows.Forms.Button mFindPointers;
        private System.Windows.Forms.Button mPointsToLocButton;
        private System.Windows.Forms.TextBox mNameTextBox;
        private System.Windows.Forms.NumericUpDown mPlayerUpDown;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button mSetLastNameButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button mSetFirstNameButton;
        private System.Windows.Forms.TextBox mPlayerNameTextBox;
    }
}