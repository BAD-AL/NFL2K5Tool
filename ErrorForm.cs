using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NFL2K5Tool
{
    public partial class ErrorForm : Form
    {
        public ErrorForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The text to show 
        /// </summary>
        public string ErrorText 
        { 
            get { return mErrorTextBox.Text; } 
            set { mErrorTextBox.Text = value; } 
        }
    }
}
