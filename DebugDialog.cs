using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NFL2K5Tool
{
    public partial class DebugDialog : Form
    {
        public DebugDialog()
        {
            InitializeComponent();
        }

        public GamesaveTool Tool { get; set; }


        private byte[] SaveFile { get { return Tool.GameSaveData; } }

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
            FindLocations();
        }

        private void FindLocations()
        {
            List<long> locs = FindStringInFile(textBox1.Text);
            foreach (int loc in locs)
            {
                textBox3.AppendText(String.Format("{0:x}\n", loc));
            }
        }

        private List<long> FindStringInFile(string str)
        {
            List<long> retVal = new List<long>();
            textBox3.Clear();
            if (SaveFile != null && SaveFile.Length > 80)
            {
                int i = 0;
                byte[] hexNumber = new byte[str.Length * 2];
                foreach (char c in str)
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
                        retVal.Add(num3);
                    }
                }
            }
            return retVal;
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
                FindLocations();
                e.Handled = true;
            }
        }

        // need to find pointers for non-players
        private void mFindPointers_Click(object sender, EventArgs e)
        {
            textBox3.Clear();
            List<long> locs = FindStringInFile(textBox1.Text);
            List<int> pointers;

            for (int i = 0; i < locs.Count; i++)
            {
                pointers = FindPointersForLocation(locs[i]);
                foreach (int dude in pointers)
                {
                    textBox3.AppendText(dude.ToString("X"));
                    textBox3.AppendText("\r\n");
                }
            }
            
        }

        private List<int> FindPointersForLocation(long location)
        {
            List<int> pointerLocations = new List<int>();
            int pointer = 0;
            long dataLocation = 0;
            int limit = this.SaveFile.Length - 4;
            for (long i = 0; i < limit; i++)
            {
                pointer = SaveFile[i + 2] << 16;
                pointer += SaveFile[i + 1] << 8;
                pointer += SaveFile[i];
                dataLocation = i + pointer - 1;

                if (dataLocation == location)
                {
                    pointerLocations.Add((int)i);
                }
            }
            return pointerLocations;
        }

        private void mPointsToLocButton_Click(object sender, EventArgs e)
        {
            textBox3.Clear();
            string val = textBox1.Text;
            if (val.StartsWith("0x"))
                val = val.Substring(2);
            int loc = Int32.Parse(val, System.Globalization.NumberStyles.AllowHexSpecifier);
            List<int> pointers = FindPointersForLocation(loc);
            foreach (int dude in pointers)
            {
                textBox3.AppendText(dude.ToString("X"));
                textBox3.AppendText("\r\n");
            }
        }

        private void UpdatePlayerNameTextBox()
        {
            int player = (int) mPlayerUpDown.Value;
            mPlayerNameTextBox.Text = Tool.GetPlayerName(player, ' ');
        }

        private void mPlayerUpDown_ValueChanged(object sender, EventArgs e)
        {
            UpdatePlayerNameTextBox();
        }

        private void mSetFirstNameButton_Click(object sender, EventArgs e)
        {
            if( mNameTextBox.Text.Length > 0)
                Tool.SetPlayerFirstName((int)mPlayerUpDown.Value, mNameTextBox.Text);
        }

        private void mSetLastNameButton_Click(object sender, EventArgs e)
        {
            if (mNameTextBox.Text.Length > 0)
                Tool.SetPlayerLastName((int)mPlayerUpDown.Value, mNameTextBox.Text);
        }

    }
}
