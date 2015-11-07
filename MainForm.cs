using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace NFL2K5Tool
{
    public partial class MainForm : Form
    {
        private GamesaveTool mTool = new GamesaveTool();
        private string searchString = "";

        public MainForm()
        {
            InitializeComponent();
            //Text = Directory.GetCurrentDirectory();
        }

        private void mLoadSaveButton_Click(object sender, EventArgs e)
        {
            LoadSaveFile();
        }

        private void LoadSaveFile()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = Directory.GetCurrentDirectory();
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                mTool.LoadSaveFile(dlg.FileName);
                string shortName = dlg.FileName.Substring(dlg.FileName.LastIndexOf(Path.DirectorySeparatorChar) + 1);
                Text = "NFL2K5Tool - " + shortName;
                statusBar1.Text = dlg.FileName + " loaded";
                mListPlayersButton2.Enabled = mListPlayersButton.Enabled = true;
            }
        }


        private void mListPlayersButton_Click(object sender, EventArgs e)
        {
            mTextBox.Clear();
            StringBuilder builder = new StringBuilder(5000);

            builder.Append(mTool.GetKey(listAttributesToolStripMenuItem.Checked, listApperanceToolStripMenuItem.Checked));
            builder.Append("\n");
            
            if( listTeamsToolStripMenuItem.Checked)
                builder.Append(mTool.GetLeaguePlayers(listAttributesToolStripMenuItem.Checked, listApperanceToolStripMenuItem.Checked, true, true));
            
            if (listFreeAgentsToolStripMenuItem.Checked)
                builder.Append(mTool.GetTeamPlayers("FreeAgents", listAttributesToolStripMenuItem.Checked, listApperanceToolStripMenuItem.Checked));
            
            if( listDraftClassToolStripMenuItem.Checked)
                builder.Append(mTool.GetTeamPlayers("DraftClass", listAttributesToolStripMenuItem.Checked, listApperanceToolStripMenuItem.Checked));
            
            mTextBox.AppendText(builder.ToString());
        }

        private void mClearButton_Click(object sender, EventArgs e)
        {
            mTextBox.Clear();
        }

        private void mTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                if (e.KeyCode == Keys.F)
                {
                    if (SetSearchString())
                        FindNextMatch();
                }
                else if (e.KeyCode == Keys.F3)
                    FindPrevMatch();
                //else if (e.KeyCode == Keys.G)
                //    EditPlayer();
                else if (e.KeyCode == Keys.L)
                {
                    CutLine();
                }
                else if (e.KeyCode == Keys.V)
                {
                    mTextBox.Paste(DataFormats.GetFormat(DataFormats.Text));
                    e.Handled = true;
                }
            }
            else if (e.Shift)
            {
                if (e.KeyCode == Keys.F3)
                    FindPrevMatch();
            }
            else if (e.KeyCode == Keys.F3)
                FindNextMatch();
            else if (e.KeyCode == Keys.F2)
                FindPrevMatch();
			
        }
        private bool FindPrevMatch()
        {
            bool ret = false;
            string message = "Not Found";

            if (searchString != null && !searchString.Equals(""))
            {
                Regex r = new Regex(searchString, RegexOptions.IgnoreCase);
                MatchCollection mc = r.Matches(mTextBox.Text);
                Match m = null;
                if (mc.Count > 0)
                {
                    m = mc[mc.Count - 1];
                }
                else
                {
                    ret = false;
                    goto end;
                }
                int i = 0;
                while (mc[i].Index < mTextBox.SelectionStart - mc[i].Length)
                    m = mc[i++];
                if (m != null && m.Length != 0)
                {
                    mTextBox.SelectionStart = m.Index + m.Length;
                    message = "Found";
                    ret = true;
                }
            }
        end:
            statusBar1.Text = message;
            return ret;
        }

        private bool FindNextMatch()
        {
            bool ret = false;
            bool wrapped = false;
            string message = "NotFound";

            if (searchString != null && !searchString.Equals(""))
            {
                Regex r;
                r = new Regex(searchString, RegexOptions.IgnoreCase);
                int startAt = mTextBox.SelectionStart+1;
                if (startAt > mTextBox.Text.Length)
                    startAt = 0;

                Match m = r.Match(mTextBox.Text, startAt);

                if (m.Length == 0)
                { // continue at the top if not found
                    m = r.Match(mTextBox.Text);
                    wrapped = true;
                }
                if (m.Length > 0)
                {
                    mTextBox.SelectionStart = m.Index;
                    mTextBox.SelectionLength = m.Length;
                    ret = true;
                    if (!wrapped)
                        message = "Found Line:" + mTextBox.GetLineFromCharIndex(mTextBox.SelectionStart);
                    else
                        message = "Text found, search starting at beginning.";
                }
            }
            statusBar1.Text = message;
            return ret;
        }
        /// <summary>
        /// Cuts the current line of text.
        /// </summary>
        private void CutLine()
        {
            int ls = GetLineStart();
            int le = GetLineEnd();
            int length = le - ls + 1;
            if (length > -1)
            {
                mTextBox.SelectionStart = ls;
                mTextBox.SelectionLength = length;
                mTextBox.Cut();
            }
        }
        /// <summary>
        /// returns the position of the start of the current line.
        /// </summary>
        /// <returns></returns>
        private int GetLineStart()
        {
            int i = 0;
            int textPosition = mTextBox.SelectionStart;
            if (textPosition >= mTextBox.Text.Length)
            {
                textPosition--;
            }
            int lineStart = 0;
            for (i = textPosition; i > 0; i--)
            {
                if (mTextBox.Text[i] == '\n')
                {
                    lineStart = i + 1;
                    break;
                }
            }
            return lineStart;
        }

        /// <summary>
        /// returns the position of the end of the current line.
        /// </summary>
        /// <returns></returns>
        private int GetLineEnd()
        {
            //			int ret = 0;
            int i = mTextBox.SelectionStart;
            if (i >= mTextBox.Text.Length)
            {
                return mTextBox.Text.Length - 1;
            }
            char current = mTextBox.Text[i];
            while (i < mTextBox.Text.Length /*&& current != ' ' && 
					current != ',' */ && current != '\n')
            {
                //				ret++;
                i++;
                current = mTextBox.Text[i];
            }
            return i;
        }
        private bool SetSearchString()
        {
            bool ret = false;
            if (mTextBox.SelectionLength > 0)
            {
                searchString = mTextBox.Text.Substring(mTextBox.SelectionStart, mTextBox.SelectionLength);
            }
            string result = StringInputDlg.GetString(
                                           "Enter Search String",
                                           "Please enter text (or a regex) to search for.",
                                           searchString);

            if (!result.Equals(""))
            {
                searchString = result;
                ret = true;
            }
            return ret;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetSearchString();
        }

        private void stringToHexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DebugDialog form = new DebugDialog();
            form.Tool = mTool;
            form.Show();
        }

        private void loadSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadSaveFile();
        }

        private void listApperanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listApperanceToolStripMenuItem.Checked = !listApperanceToolStripMenuItem.Checked;
        }

        private void listAttributesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listAttributesToolStripMenuItem.Checked = !listAttributesToolStripMenuItem.Checked;
        }

        private void listFreeAgentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listFreeAgentsToolStripMenuItem.Checked = !listFreeAgentsToolStripMenuItem.Checked;
        }

        private void listDraftClassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listDraftClassToolStripMenuItem.Checked = !listDraftClassToolStripMenuItem.Checked;
        }

        private void listTeamsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listTeamsToolStripMenuItem.Checked = !listTeamsToolStripMenuItem.Checked;
        }

        private void mListPlayersButton2_Click(object sender, EventArgs e)
        {
            mTextBox.Clear();
            StringBuilder builder = new StringBuilder(5000);
            builder.Append(mTool.GetKey(listAttributesToolStripMenuItem.Checked, listApperanceToolStripMenuItem.Checked));
            builder.Append("\n");
            int max = (int)numericUpDown1.Value;
            for (int i = 0; i < max; i++)
            {
                builder.Append(mTool.GetPlayerData(i, listAttributesToolStripMenuItem.Checked, listApperanceToolStripMenuItem.Checked));
                builder.Append("\n");
            }
            //builder.Append(mTool.GetLeaguePlayers(listAttributesToolStripMenuItem.Checked, listApperanceToolStripMenuItem.Checked, true, true));
            mTextBox.AppendText(builder.ToString());
        }

        private void mApplyButton_Click(object sender, EventArgs e)
        {
            InputParser parser = new InputParser();
            parser.Tool = this.mTool;
            parser.ProcessText(mTextBox.Text);
        }

        private void mSaveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(dlg.FileName))
                    File.Delete(dlg.FileName);
                File.WriteAllBytes(dlg.FileName, mTool.GameSaveData);
            }
        }


    }
}
