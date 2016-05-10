using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace NFL2K5Tool
{
    public partial class PhotoDataEditor : Form
    {
        public PhotoDataEditor()
        {
            InitializeComponent();
            
        }

        string sNoPhoto = "PlayerData\\PlayerPhotos\\0004.jpg";

        private void mPhotoUpDown_ValueChanged(object sender, EventArgs e)
        {
            string key = string.Format("{0:0000}", mPhotoUpDown.Value);
            if (DataMap.ReversePhotoMap.ContainsKey(key))
                mNameBox.Text = DataMap.ReversePhotoMap[key];

            string path = String.Format("PlayerData\\PlayerPhotos\\{0}.jpg",key);
            if (File.Exists(path))
                mPhotoPictureBox.ImageLocation = path;
            else
                mPhotoPictureBox.ImageLocation = sNoPhoto;
        }

        private void mThinButton_Click(object sender, EventArgs e)
        {
            // textbox 1 => names + numbers
            // textbox 2 => filenames
            
            List<string> nameLines = new List<string>(mTextBox1.Lines);
            int initialLies = nameLines.Count;
            string number = "";
            int index = -1;
            for (int i = nameLines.Count - 1; i > -1; i--)
            {
                index = nameLines[i].IndexOf('=')+1;
                //if (index > 0)
                {
                    number = nameLines[i].Trim();//.Substring(index);
                    if (mTextBox2.Text.IndexOf(number ) == -1)
                    {
                        nameLines.RemoveAt(i);
                    }
                }
            }
            int resultingLineCount = nameLines.Count;
            this.Text = "Before:" + initialLies + " After:" + resultingLineCount;
            StringBuilder builder = new StringBuilder(10000);
            foreach (string line in nameLines)
            {
                builder.Append(line);
                builder.Append("\r\n");
            }
            mTextBox1.Text = builder.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder(10000);
            List<string> nameLines = new List<string>(mTextBox1.Lines);
            //string number = "";
            //int index = -1;
            string line = "";
            for (int i = 0; i < nameLines.Count; i++)
            {
                //line = nameLines[i];
                //index = line.IndexOf('=') + 1;
                //if (index > 0)
                //{
                //    number = line.Substring(index);
                //    if (number.Length < 4)
                //        line = line.Replace(number, String.Format("{0:0000}", Int32.Parse(number)));
                //}
                //builder.Append(line);
                //builder.Append("\n");
                builder.Append(string.Format("~UNK{0}={0}\n",nameLines[i]));
                //builder.Append(string.Format("{0:0000}\n",i));
            }
            mTextBox1.Text = builder.ToString();
        }
    }
}
