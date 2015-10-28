using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NFL2K5Tool
{
    public partial class StringToHexForm : Form
    {
        public StringToHexForm()
        {
            InitializeComponent();
        }

        public byte[] SaveFile;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            UpdateText();
        }

        private void UpdateText()
        {
            int ch;
            StringBuilder builder = new StringBuilder(20);

            builder.Append("0x");
            foreach (char c in textBox1.Text)
            {
                ch = (int)c;
                builder.Append(ch.ToString("X2"));
                if (checkBox1.Checked)
                {
                    builder.Append("00");
                }
            }
            textBox2.Text = builder.ToString();
        }

        private void mFindButton_Click(object sender, EventArgs e)
        {
            FindStringInFile();
        }

        private void FindStringInFile()
        {
            textBox3.Clear();
            if (SaveFile != null && SaveFile.Length > 80)
            {
                int i = 0;
                byte[] hexNumber = new byte[textBox1.Text.Length * 2];
                foreach (char c in textBox1.Text)
                {
                    hexNumber[i] = (byte)c;
                    hexNumber[i + 1] = 0;
                    i += 2;
                }
                long num = (long)(SaveFile.Length - hexNumber.Length);
                for (long num3 = 0L; num3 < num; num3 += 1L)
                {
                    if (Find(hexNumber, num3, SaveFile))
                    {
                        textBox3.AppendText(String.Format("{0:x}\n", num3));
                    }
                }
            }
        }

        private bool Find(byte[] hexNumber, long location, byte[]data)
        {
            int i;
            for (i = 0; i < hexNumber.Length; i++)
            {
                if (hexNumber[i] != data[(int)(checked((IntPtr)(unchecked(location + (long)i))))])
                {
                    break;
                }
            }
            return i == hexNumber.Length;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FindStringInFile();
                e.Handled = true;
            }
        }
    }
}
