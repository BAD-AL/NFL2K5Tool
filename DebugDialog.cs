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
            mResultsTextBox.StatusControl = mStatusLabel;
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
            mResultsTextBox.Clear();
            List<long> locs = StaticUtils.FindStringInFile(textBox1.Text, SaveFile, 0, SaveFile.Length);
            foreach (int loc in locs)
            {
                mResultsTextBox.AppendText(String.Format("{0:x}\n", loc));
            }
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
            mResultsTextBox.Clear();
            List<long> locs = StaticUtils.FindStringInFile(textBox1.Text, SaveFile, 0, SaveFile.Length);
            List<int> pointers;

            for (int i = 0; i < locs.Count; i++)
            {
                pointers = FindPointersForLocation(locs[i]);
                foreach (int dude in pointers)
                {
                    mResultsTextBox.AppendText(dude.ToString("X"));
                    mResultsTextBox.AppendText("\r\n");
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
                pointer =  SaveFile[i + 3] << 24;
                pointer += SaveFile[i + 2] << 16;
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
            mResultsTextBox.Clear();
            string val = textBox1.Text;
            ShowPointersForLoc(val);
        }

        private void ShowPointersForLoc(string val)
        {
            if (val.StartsWith("0x"))
                val = val.Substring(2);
            try
            {
                mResultsTextBox.Clear();
                int loc = Int32.Parse(val, System.Globalization.NumberStyles.AllowHexSpecifier);
                List<int> pointers = FindPointersForLocation(loc);
                foreach (int dude in pointers)
                {
                    mResultsTextBox.AppendText(dude.ToString("X"));
                    mResultsTextBox.AppendText("\r\n");
                }
                if (pointers.Count == 0)
                {
                    mResultsTextBox.Text = "Pointr to: "+  val + " Not found";
                }
            }
            catch (Exception e)
            {
                mResultsTextBox.Text = e.Message; 
            }
        }

        private void UpdatePlayerNameTextBox()
        {
            int player = (int) mPlayerUpDown.Value;
            mPlayerNameTextBox.Text = Tool.GetPlayerName(player, ' ');
            mLocationLabel.Text = "0x" + Tool.GetPlayerDataStart(player).ToString("X");
        }

        private void mPlayerUpDown_ValueChanged(object sender, EventArgs e)
        {
            UpdatePlayerNameTextBox();
        }

        private void mSetFirstNameButton_Click(object sender, EventArgs e)
        {
            if( mNameTextBox.Text.Length > 0)
                Tool.SetPlayerFirstName((int)mPlayerUpDown.Value, mNameTextBox.Text, mUsePointerButton.Checked);
        }

        private void mSetLastNameButton_Click(object sender, EventArgs e)
        {
            if (mNameTextBox.Text.Length > 0)
                Tool.SetPlayerLastName((int)mPlayerUpDown.Value, mNameTextBox.Text, mUsePointerButton.Checked);
        }

        private void listNumberOfPlayersButton_Click(object sender, EventArgs e)
        {
           mResultsTextBox.Text = Tool.GetNumberOfPlayersOnAllTeams();
        }

        private void mGetTeamButton_Click(object sender, EventArgs e)
        {
            mResultsTextBox.Text = Tool.GetTeamPlayers(textBox1.Text, false, false);
        }

        private void mLocationLabel_Click(object sender, EventArgs e)
        {
            ShowPointersForLoc(mLocationLabel.Text);
        }

        private void mTeamButton_Click(object sender, EventArgs e)
        {
            mResultsTextBox.Text = Tool.GetPlayerTeam((int)mPlayerUpDown.Value);
        }

        private void mListPlayersButton2_Click(object sender, EventArgs e)
        {
            mResultsTextBox.Clear();
            StringBuilder builder = new StringBuilder(5000);
            builder.Append(Tool.GetKey(false, false));
            builder.Append("\n");
            int max = (int)numericUpDown1.Value;
            for (int i = 0; i < max; i++)
            {
                builder.Append(Tool.GetPlayerData(i, false, false));
                if (includeDepthToolStripMenuItem.Checked)
                {
                    builder.Append(" Depth:");
                    builder.Append(Tool.GetPlayerPositionDepth(i).ToString("X2"));
                }
                builder.Append("\n");
            }
            mResultsTextBox.AppendText(builder.ToString());
        }

        private void numericUpDown1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                mListPlayersButton2_Click(sender, e);
        }

        private void includeDepthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            includeDepthToolStripMenuItem.Checked = !includeDepthToolStripMenuItem.Checked;
        }

    }
}
