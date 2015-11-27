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
        public MessageForm(Icon icon)
        {
            InitializeComponent();
            this.Icon = icon;
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
    }
}
