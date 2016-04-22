using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NFL2K5Tool
{
    public partial class PictureHelper : Form
    {
        public PictureHelper()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder(1000);
            string line = "";
            string part = "";
            int index = -1;

            for (int i = 0; i < mDataFileTextBox.Lines.Length; i++)
            {
                line = mDataFileTextBox.Lines[i];
                index = line.IndexOf('=');
                if (index > -1)
                {
                    part = line.Substring(index + 1);
                    if (mPhotoListTextBox.Text.IndexOf(part) == -1 && line.IndexOf(',') > -1)
                    {
                        builder.Append(line);
                        builder.Append("\r\n");
                    }
                }
            }
            mOutputTextBox.Text = builder.ToString();
        }
    }
}
