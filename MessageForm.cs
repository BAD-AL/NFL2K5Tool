using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NFL2K5Tool
{
    public partial class MessageForm : Form
    {
        /// <summary>
        /// Raised when user doubleClicks in the textbox
        /// </summary>
        public event EventHandler TextClicked;

        public MessageForm(Icon icon)
        {
            InitializeComponent();
            this.Icon = icon;
        }

        public bool ShowCancelButton
        {
            set { this.mCancelButton.Visible = value; }
        }

        public bool MessageEditable
        {
            get { return !mTextBox.ReadOnly;  }
            set { mTextBox.ReadOnly = !value; }
        }

        /// <summary>
        /// sets the text on the Aux button.
        /// Aux button returns a dialog result of 'Abort'
        /// </summary>
        public string AuxButtonText
        {
            get { return mAuxButton.Text; }

            set
            {
                mAuxButton.Text = value;
                mAuxButton.Visible = (mAuxButton.Text.Length > 0);
            }
        }

        /// <summary>
        /// The text to show 
        /// </summary>
        public string MessageText 
        { 
            get { return mTextBox.Text; } 
            set { mTextBox.Text = value; } 
        }

        private void mOkButton_Click(object sender, EventArgs e)
        {
            if (!this.Modal)
                Close();
        }

        private void mTextBox_DoubleClick(object sender, EventArgs e)
        {
            if (TextClicked != null)
            {
                this.TextClicked(this, new StringEventArgs(mTextBox.GetCurrentLine()));
            }
        }

        /// <summary>
        /// Get a string from the user.
        /// </summary>
        /// <param name="title">The title of the dialog</param>
        /// <param name="message">The initial message to display.</param>
        /// <returns>valid string when the user hits 'ok', null when they cancel.</returns>
        public static string GetString(string title, string message)
        {
            string retVal = null;
            MessageForm mf = new MessageForm(SystemIcons.Question);
            mf.MessageEditable = true;
            mf.Text = title;
            mf.MessageText = message;

            if (mf.ShowDialog() == DialogResult.OK)
            {
                retVal = mf.MessageText;
            }
            mf.Dispose();
            return retVal;
        }
    }

    public class StringEventArgs : EventArgs
    {
        /// <summary>
        /// The string value
        /// </summary>
        public string Value { get; set; }

        public StringEventArgs(string val)
        {
            Value = val;
        }
    }
}
