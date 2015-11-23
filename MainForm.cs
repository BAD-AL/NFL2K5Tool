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

        public MainForm()
        {
            InitializeComponent();
            //Text = Directory.GetCurrentDirectory();
            EnableControls(false);
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
            dlg.Filter = "XBOX Save files (*.DAT, *.zip)|*.DAT;*.zip";
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                mTool.LoadSaveFile(dlg.FileName);
                string shortName = dlg.FileName.Substring(dlg.FileName.LastIndexOf(Path.DirectorySeparatorChar) + 1);
                Text = "NFL2K5Tool - " + shortName;
                statusBar1.Text = dlg.FileName + " loaded";
                EnableControls(true);
            }
        }

        /// <summary>
        /// Controls enabling/disabling of controls (to make sure we have data to access before we try to access it)
        /// </summary>
        private void EnableControls(bool enable)
        {
            scheduleToolStripMenuItem.Enabled=
            autoUpdateDepthChartToolStripMenuItem.Enabled =
            autoUpdatePBPToolStripMenuItem.Enabled =
            autoUpdatePhotoToolStripMenuItem.Enabled =
            teamPlayersToolStripMenuItem.Enabled=
            mListPlayersButton2.Enabled = 
            mListContentsButton.Enabled = 
            applyDataWithoutSavingToolStripMenuItem.Enabled =
            saveToolStripMenuItem.Enabled =
            mSaveButton.Enabled =
            debugDialogMenuItem.Enabled = enable;
        }



        private void mListContentsButton_Click(object sender, EventArgs e)
        {
            mTextBox.Clear();
            StringBuilder builder = new StringBuilder(5000);

            builder.Append(mTool.GetKey(listAttributesToolStripMenuItem.Checked, listApperanceToolStripMenuItem.Checked));
            builder.Append("\n");
            
            if( listTeamsToolStripMenuItem.Checked)
                builder.Append(mTool.GetLeaguePlayers(listAttributesToolStripMenuItem.Checked, listApperanceToolStripMenuItem.Checked));
            
            if (listFreeAgentsToolStripMenuItem.Checked)
                builder.Append(mTool.GetTeamPlayers("FreeAgents", listAttributesToolStripMenuItem.Checked, listApperanceToolStripMenuItem.Checked));
            
            if( listDraftClassToolStripMenuItem.Checked)
                builder.Append(mTool.GetTeamPlayers("DraftClass", listAttributesToolStripMenuItem.Checked, listApperanceToolStripMenuItem.Checked));

            if (listScheduleToolStripMenuItem.Checked)
            {
                SchedulerHelper helper = new SchedulerHelper(mTool);
                builder.Append("\n\n#Schedule\n");
                builder.Append(helper.GetSchedule());
            }

            mTextBox.AppendText(builder.ToString());
        }

        private void scheduleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SchedulerHelper helper = new SchedulerHelper(mTool);
            mTextBox.AppendText("\n\n#Schedule\n");
            mTextBox.AppendText(helper.GetSchedule());
        }

        private void teamPlayersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(mTool.GetKey(listAttributesToolStripMenuItem.Checked, listApperanceToolStripMenuItem.Checked));
            builder.Append("\n");
            if (listTeamsToolStripMenuItem.Checked)
                builder.Append(mTool.GetLeaguePlayers(listAttributesToolStripMenuItem.Checked, listApperanceToolStripMenuItem.Checked));

            if (listFreeAgentsToolStripMenuItem.Checked)
                builder.Append(mTool.GetTeamPlayers("FreeAgents", listAttributesToolStripMenuItem.Checked, listApperanceToolStripMenuItem.Checked));

            if (listDraftClassToolStripMenuItem.Checked)
                builder.Append(mTool.GetTeamPlayers("DraftClass", listAttributesToolStripMenuItem.Checked, listApperanceToolStripMenuItem.Checked));
            mTextBox.Text = builder.ToString();
        }

        private void mClearButton_Click(object sender, EventArgs e)
        {
            mTextBox.Clear();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mTextBox.SetSearchString();
        }

        private void stringToHexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DebugDialog form = new DebugDialog();
            form.Tool = mTool;
            form.Show(this);
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

        private void listScheduleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listScheduleToolStripMenuItem.Checked = !listScheduleToolStripMenuItem.Checked;
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

        }

        private void mSaveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                ApplyTextToSave();
                mTool.SaveFile(dlg.FileName);
                StaticUtils.ShowErrors(false);
            }
        }

        private void ApplyTextToSave()
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            statusBar1.Text = "Applying data...";
            InputParser parser = new InputParser();
            parser.Tool = this.mTool;
            parser.ProcessText(mTextBox.Text);
            sw.Stop();
            statusBar1.Text = "Done Applying data." + (sw.ElapsedMilliseconds / 1000.0) + "s";
            StaticUtils.ShowErrors(false);
        }

        private void autoUpdateDepthChartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mTool.AutoUpdateDepthChart();
        }

        private void applyDataWithoutSavingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplyTextToSave();
        }

        private void autoUpdatePhotoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mTool.AutoUpdatePhoto();
        }

        private void autoUpdatePBPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mTool.AutoUpdatePBP();
        }

        private void mLoadTextFileButton_Click(object sender, EventArgs e)
        {
            LoadTextFile();
        }

        private void loadTextFileAction(object sender, EventArgs e)
        {
            LoadTextFile();
        }

        private void LoadTextFile()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    mTextBox.Text = File.ReadAllText(dlg.FileName);
                }
                catch (Exception )
                {
                    MessageBox.Show(this, "Error Loadig file"+dlg.FileName, 
                        "Error!",MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            dlg.Dispose();
        }

    }
}
