﻿namespace NFL2K5Tool
{
    partial class SearchTextBox
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
            components = new System.ComponentModel.Container();
            this.mContextMenu = new System.Windows.Forms.ContextMenu();
            this.mCutMenuItem = new System.Windows.Forms.MenuItem();
            this.mCopyMenuItem = new System.Windows.Forms.MenuItem();
            this.mPasteMenuItem = new System.Windows.Forms.MenuItem();
            this.mFindMenuItem = new System.Windows.Forms.MenuItem();
            this.mFindNextMenuItem = new System.Windows.Forms.MenuItem();
            this.mFindPrevMenuItem = new System.Windows.Forms.MenuItem();
            this.mSelectAllMenuItem = new System.Windows.Forms.MenuItem();
            this.mClearMenuItem = new System.Windows.Forms.MenuItem();
            this.mCopyAllMenuItem = new System.Windows.Forms.MenuItem();

            this.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;

            this.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            // 
            // mCutMenuItem
            // 
            this.mCutMenuItem.Index = 0;
            this.mCutMenuItem.Text = "C&ut       (Ctrl+X)";
            this.mCutMenuItem.Click += new System.EventHandler(this.ContextMenuItem_Click);
            //
            // mCopyAllMenuItem
            //
            this.mCopyAllMenuItem.Index = 1;
            this.mCopyAllMenuItem.Text = "&Copy All";
            this.mCopyAllMenuItem.Click += new System.EventHandler(this.ContextMenuItem_Click);
            // 
            // mCopyMenuItem
            // 
            this.mCopyMenuItem.Index = 2;
            this.mCopyMenuItem.Text = "&Copy    (Ctrl+C)";
            this.mCopyMenuItem.Click += new System.EventHandler(this.ContextMenuItem_Click);
            // 
            // mPasteMenuItem
            // 
            this.mPasteMenuItem.Index = 3;
            this.mPasteMenuItem.Text = "&Paste   (Ctrl+V)";
            this.mPasteMenuItem.Click += new System.EventHandler(this.ContextMenuItem_Click);
            // 
            // mSelectAllMenuItem
            // 
            this.mSelectAllMenuItem.Index = 4;
            this.mSelectAllMenuItem.Text = "Select &All  (Ctrl+A)";
            this.mSelectAllMenuItem.Click += new System.EventHandler(this.ContextMenuItem_Click);
            // 
            // mFindMenuItem
            // 
            this.mFindMenuItem.Index = 5;
            this.mFindMenuItem.Text = "&Find          (Ctrl+F)";
            this.mFindMenuItem.Click += new System.EventHandler(this.ContextMenuItem_Click);
            // 
            // mFindNextMenuItem
            // 
            this.mFindNextMenuItem.Index = 6;
            this.mFindNextMenuItem.Text = "Find &Next (F3)";
            this.mFindNextMenuItem.Click += new System.EventHandler(this.ContextMenuItem_Click);
            // 
            // mFindPrevMenuItem
            // 
            this.mFindPrevMenuItem.Index = 7;
            this.mFindPrevMenuItem.Text = "Find &Prev (F2)";
            this.mFindPrevMenuItem.Click += new System.EventHandler(this.ContextMenuItem_Click);

            this.mClearMenuItem.Index = 8;
            this.mClearMenuItem.Text = "Clear";
            this.mClearMenuItem.Click += new System.EventHandler(this.ContextMenuItem_Click);
            // 
            // mContextMenu
            // 
            this.mContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                this.mCutMenuItem,
                this.mCopyAllMenuItem,
                this.mCopyMenuItem,
                this.mPasteMenuItem,
                this.mSelectAllMenuItem,
                this.mFindMenuItem,
                this.mFindNextMenuItem,
                this.mFindPrevMenuItem,
                this.mClearMenuItem,
            });

            this.ContextMenu = this.mContextMenu;
        }

        private System.Windows.Forms.ContextMenu mContextMenu;
        private System.Windows.Forms.MenuItem mCutMenuItem;
        private System.Windows.Forms.MenuItem mCopyMenuItem;
        private System.Windows.Forms.MenuItem mPasteMenuItem;
        private System.Windows.Forms.MenuItem mFindMenuItem;
        private System.Windows.Forms.MenuItem mFindNextMenuItem;
        private System.Windows.Forms.MenuItem mFindPrevMenuItem;
        private System.Windows.Forms.MenuItem mSelectAllMenuItem;
        private System.Windows.Forms.MenuItem mClearMenuItem;
        private System.Windows.Forms.MenuItem mCopyAllMenuItem;
        #endregion
    }
}
