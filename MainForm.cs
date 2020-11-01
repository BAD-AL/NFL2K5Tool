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
        private GamesaveTool mTool;// = new GamesaveTool();
        private const string mSortStringFileName = "PlayerData\\SortFormulas.txt";

        public MainForm()
        {
            InitializeComponent();
            //Text = Directory.GetCurrentDirectory();
            EnableControls(false);
            mTextBox.StatusControl = statusBar1;
            
            autoCorrectScheduleToolStripMenuItem.Checked = SchedulerHelper.AUTO_CORRECT_SCHEDULE;

            nameColorToolStripMenuItem.BackColor = Color.White;
            nameColorToolStripMenuItem.ForeColor = Color.Blue;
            this.Text = "NFL2K5Tool " + System.Reflection.Assembly.GetCallingAssembly().GetName().Version
                +" beta";

            mTextBox.AllowDrop = true;
            mTextBox.DragOver += new DragEventHandler(file_DragOver);
            mTextBox.DragDrop += new DragEventHandler(file_DragDrop);

            mLoadSaveButton.AllowDrop = true;
            mLoadSaveButton.DragOver += new DragEventHandler(file_DragOver);
            mLoadSaveButton.DragDrop += new DragEventHandler(file_DragDrop);
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
            dlg.Filter = "XBOX Save files (*.DAT, *.zip, *.max)|*.DAT;*.zip;*.max";
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                LoadSaveFile(dlg.FileName);
            }
            dlg.Dispose();
            StaticUtils.ShowErrors(false);
        }

        private void LoadSaveFile(string filename)
        {
            if (filename.EndsWith(".txt", StringComparison.InvariantCultureIgnoreCase) ||
                filename.EndsWith(".csv", StringComparison.InvariantCultureIgnoreCase)
                )
            {
                SetText(File.ReadAllText(filename));
                return;
            }
            mTool = new GamesaveTool();
            if (mTool.LoadSaveFile(filename))
            {
                string shortName = filename.Substring(filename.LastIndexOf(Path.DirectorySeparatorChar) + 1);
                Text = "NFL2K5Tool - " + shortName;
                statusBar1.Text = filename + " loaded";
                EnableControls(true);
            }
            else
            {
                mTool = null;
                MessageBox.Show("Is the requested file a .txt, .csv, .zip or .dat file?","Error!");
            }
        }


        /// <summary>
        /// Controls enabling/disabling of controls (to make sure we have data to access before we try to access it)
        /// </summary>
        private void EnableControls(bool enable)
        {
            scheduleToolStripMenuItem.Enabled=
            sortPlayersToolStripMenuItem.Enabled=
            validateToolStripMenuItem.Enabled =
            playerEditorToolStripMenuItem.Enabled =
            autoUpdateDepthChartToolStripMenuItem.Enabled =
            autoUpdatePBPToolStripMenuItem.Enabled =
            autoUpdatePhotoToolStripMenuItem.Enabled =
            teamPlayersToolStripMenuItem.Enabled=
            mListPlayersButton2.Enabled = 
            mListContentsButton.Enabled = 
            applyDataWithoutSavingToolStripMenuItem.Enabled =
            saveToolStripMenuItem.Enabled =
            mSaveButton.Enabled =
            globalEditorToolStripMenuItem.Enabled =
            autoUpdateSpecialTeamsDepthToolStripMenuItem.Enabled =
            debugDialogMenuItem.Enabled = enable;
        }

        private void mListContentsButton_Click(object sender, EventArgs e)
        {
            ListContents();
        }

        private void ListContents()
        {
            mTextBox.Clear();
            StringBuilder builder = new StringBuilder(5000);

            if (listTeamsToolStripMenuItem.Checked || listFreeAgentsToolStripMenuItem.Checked || listDraftClassToolStripMenuItem.Checked)
            {
                builder.Append(mTool.GetKey(listAttributesToolStripMenuItem.Checked, listApperanceToolStripMenuItem.Checked));
                builder.Append("\n");
            }

            if (listTeamsToolStripMenuItem.Checked)
                builder.Append(mTool.GetLeaguePlayers(listAttributesToolStripMenuItem.Checked, listApperanceToolStripMenuItem.Checked, listSpecialTeamsToolStripMenuItem.Checked));

            if (listFreeAgentsToolStripMenuItem.Checked)
                builder.Append(mTool.GetTeamPlayers("FreeAgents", listAttributesToolStripMenuItem.Checked, listApperanceToolStripMenuItem.Checked, false));

            if (listDraftClassToolStripMenuItem.Checked)
                builder.Append(mTool.GetTeamPlayers("DraftClass", listAttributesToolStripMenuItem.Checked, listApperanceToolStripMenuItem.Checked, false));

            if (listCoachesToolStripMenuItem1.Checked)
            {
                builder.Append(mTool.GetCoachData());
            }

            if (listScheduleToolStripMenuItem.Checked  )
            {
                if (mTool.SaveType == SaveType.Franchise)
                {
                    SchedulerHelper helper = new SchedulerHelper(mTool);
                    builder.Append("\n\n#Schedule\n");
                    builder.Append(helper.GetSchedule());
                }
                else
                {
                    Console.WriteLine("Cannot list schedule of {0} GameSave file", mTool.SaveType);
                }
            }

            SetText(builder.ToString());
        }

        Regex mColorizeRegex = new Regex("^[A-Z]+,[A-Za-z \\.']+,[A-Z,a-z ']+,", RegexOptions.Multiline);
        
        /// <summary>
        /// Sets the text box text and colorizes the player names
        /// </summary>
        /// <param name="text"></param>
        private void SetText( string text)
        {
            mTextBox.Visible = false;
            mTextBox.SelectionLength = 0;
            mTextBox.Text = text;
            MatchCollection mc = mColorizeRegex.Matches(mTextBox.Text);
            foreach (Match m in mc)
            {
                mTextBox.SelectionStart = m.Index;
                mTextBox.SelectionLength = m.Length - 1;
                mTextBox.SelectionColor = nameColorToolStripMenuItem.ForeColor;
            }
            mTextBox.Visible = true;
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
                builder.Append(mTool.GetLeaguePlayers(listAttributesToolStripMenuItem.Checked, listApperanceToolStripMenuItem.Checked, listSpecialTeamsToolStripMenuItem.Checked));

            if (listFreeAgentsToolStripMenuItem.Checked)
                builder.Append(mTool.GetTeamPlayers("FreeAgents", listAttributesToolStripMenuItem.Checked, listApperanceToolStripMenuItem.Checked, false));

            if (listDraftClassToolStripMenuItem.Checked)
                builder.Append(mTool.GetTeamPlayers("DraftClass", listAttributesToolStripMenuItem.Checked, listApperanceToolStripMenuItem.Checked, false));
            SetText(builder.ToString());
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

        private void debugDialogMenuItem_Click(object sender, EventArgs e)
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

        // hidden button;  used in debugging
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
            mTextBox.AppendText(builder.ToString());
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
            InputParser parser = new InputParser(this.mTool);
            //parser.UseExistingNames = reuseNamesToConserveNameSpaceToolStripMenuItem.Checked;

            parser.ProcessText(mTextBox.Text);
            sw.Stop();
            statusBar1.Text = "Done Applying data." + (sw.ElapsedMilliseconds / 1000.0) + "s";
            StaticUtils.ShowErrors(false);

            string lookupPlayers = parser.GetLookupPlayers();
            if (lookupPlayers != null)
                MessageForm.ShowMessage("Lookup Players", lookupPlayers, SystemIcons.Information, false, false);
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
            AutoUpdatePhoto();
        }

        private void autoUpdatePBPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoUpdatePBP();
        }

        private void mLoadTextFileButton_Click(object sender, EventArgs e)
        {
            LoadTextFile();
        }

        private void loadTextFileAction(object sender, EventArgs e)
        {
            LoadTextFile();
        }

        private void validateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ValidatePlayers();
        }

        private void sortPlayersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SortPlayers();
        }

        private void editSortFormulasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditSortFormulas();
        }

        private void LoadTextFile()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    SetText(File.ReadAllText(dlg.FileName));
                }
                catch (Exception )
                {
                    MessageBox.Show(this, "Error Loadig file"+dlg.FileName, 
                        "Error!",MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            dlg.Dispose();
        }

        private void AutoUpdatePhoto()
        {
            PlayerUpdater v = new PlayerUpdater(mTool.GetKey(listAttributesToolStripMenuItem.Checked, listApperanceToolStripMenuItem.Checked));
            SetText(v.UpdatePlayers(mTextBox.Text, false, true));
            statusBar1.Text = "Player Photos updated";
        }

        private void AutoUpdatePBP()
        {
            PlayerUpdater v = new PlayerUpdater(mTool.GetKey(listAttributesToolStripMenuItem.Checked, listApperanceToolStripMenuItem.Checked));
            SetText(v.UpdatePlayers(mTextBox.Text, true, false));
            statusBar1.Text = "Player PBPs updated";
        }

        private void ValidatePlayers()
        {
            PlayerValidator v = new PlayerValidator(mTool.GetKey(listAttributesToolStripMenuItem.Checked, listApperanceToolStripMenuItem.Checked));
            string results =  v.ValidatePlayers(mTextBox.Text);
            if (results.Length > 0)
            {
                MessageForm ef = new MessageForm(SystemIcons.Warning);
                ef.TextClicked += new EventHandler(validatorForm_TextClicked);
                ef.ShowCancelButton = false;
                ef.MessageText = results;
                ef.Text = "Warning, verify player attributes";
                ef.Closed += new EventHandler(validatorForm_Closed);
                ef.Show(this);
            }
            else
            {
                MessageBox.Show("No Issues Found", "Player Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        void validatorForm_Closed(object sender, EventArgs e)
        {
            MessageForm ef = sender as MessageForm;
            if (ef != null)
            {
                ef.TextClicked -= new EventHandler(validatorForm_TextClicked);
                ef.Closed -= new EventHandler(validatorForm_Closed);
            }
        }

        void validatorForm_TextClicked(object sender, EventArgs e)
        {
            StringEventArgs sea = e as StringEventArgs;
            if (sea != null && sea.Value.Length > 0)
            {
                int index = sea.Value.IndexOf('\t');
                string searchStr = "";
                if (index > 0)
                {
                    searchStr = sea.Value.Substring(0, index - 1);
                    index = this.mTextBox.Find(searchStr, RichTextBoxFinds.MatchCase);
                    if (index > 0)
                    {
                        this.mTextBox.SelectionStart = index;
                        this.mTextBox.SelectionLength = searchStr.Length;
                        this.mTextBox.ScrollToCaret();
                    }
                }
            }
        }

        private void SortPlayers()
        {
            string allInput = mTextBox.Text.Replace("\r\n", "\n");
            PlayerSorter ps = new PlayerSorter(mTool.GetKey(listAttributesToolStripMenuItem.Checked, listApperanceToolStripMenuItem.Checked));

            if (File.Exists(mSortStringFileName))
                ps.FormulasString = File.ReadAllText(mSortStringFileName);

            string teamPattern = "Team";

            try
            {
                int index = 0;
                int nn = -1;
                string current = "";
                string modified = "";
                string result = allInput;
                do
                {
                    // Sort team by team
                    index = allInput.IndexOf(teamPattern, index);
                    if (index > -1)
                    {
                        nn = allInput.IndexOf("\n\n", index);
                        if (nn < 0)
                            nn = allInput.Length;
                        current = allInput.Substring(index, nn - index);
                        modified = ps.SortTeam(current);
                        result = result.Replace(current, modified);
                        index = nn;
                    }
                } while (index > -1);

                SetText(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    String.Concat("Error sorting players. \nCheck formulas file for errors.\n", ex.Message, "\n", ex.InnerException.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EditSortFormulas()
        {
            if (File.Exists(mSortStringFileName))
            {
                MessageForm ef = new MessageForm(SystemIcons.Question);
                ef.Text = "Edit the sort formulas";
                ef.MessageText = ef.MessageText = File.ReadAllText(mSortStringFileName);
                ef.MessageEditable = true;
                ef.AuxButtonText = "&Restore Default";
                DialogResult result = ef.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    string res = ef.MessageText.Replace("\r\n","\n");
                    res = res.Replace("\n", Environment.NewLine); // make the text file nice for text editors
                    File.WriteAllText(mSortStringFileName, res);
                }
                else if (result == DialogResult.Abort)
                {
                    // restore the default sort data
                    string res = PlayerSorter.sFormulasString.Replace("\r\n", "\n");
                    res = res.Replace("\n", Environment.NewLine); // make the text file nice for text editors
                    File.WriteAllText(mSortStringFileName, res);
                }
                ef.Dispose();
            }
            else
            {
                MessageBox.Show(this, "File: " + mSortStringFileName + " does not exist.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void decreaseFontSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Font f = new Font(mTextBox.Font.FontFamily,mTextBox.Font.Size -1);
            mTextBox.Font = f;
        }

        private void increaseFontSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Font f = new Font(mTextBox.Font.FontFamily, mTextBox.Font.Size + 1);
            mTextBox.Font = f;
        }

        private void nameColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                nameColorToolStripMenuItem.ForeColor = dlg.Color;
            }

        }

        private static DateTime m_LastTime;

        private void TextBox_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            DateTime now = DateTime.Now;
            TimeSpan span = new TimeSpan(now.Ticks - m_LastTime.Ticks);
            if ( span.TotalMilliseconds < 1000) // less than a second
            {
                //try {
                    DoubleClicked();
                //} catch (Exception ex) { StaticUtils.AddError("Encountered error on double click" + ex.Message); }
            }
            m_LastTime = now;
        }

        private void DoubleClicked()
        {
            string line = InputParser.GetLine(mTextBox.SelectionStart, mTextBox.Text);
            if (!String.IsNullOrEmpty(line) && line.StartsWith("Coach", StringComparison.InvariantCultureIgnoreCase))
            {
                EditCoach();
            }
            else if (!String.IsNullOrEmpty(line) && InputParser.ParsePlayerLine(line).Count > 2)
            {
                EditPlayer();
            }
        }

        private void EditCoach()
        {
            CoachEditForm form = new CoachEditForm();
            form.ReversePBPs = DataMap.ReversePBPMap;
            form.ReversePhotos = DataMap.ReversePhotoMap;
            form.PBPs = DataMap.PBPMap;
            form.Photos = DataMap.PhotoMap;
            form.Data = mTextBox.Text;
            form.SelectionStart = mTextBox.SelectionStart;
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                SetText(form.Data);
                mTextBox.SelectionStart = form.SelectionStart;
                mTextBox.ScrollToCaret();
            }
            form.Dispose();
        }

        private void EditPlayer()
        {
            if (mTool != null)
            {
                PlayerEditForm form = new PlayerEditForm();
                form.Colleges = mTool.GetColleges();
                form.PBPs = DataMap.PBPMap;
                form.Photos = DataMap.PhotoMap;
                form.ReversePBPs = DataMap.ReversePBPMap;
                form.ReversePhotos = DataMap.ReversePhotoMap;                
                form.Data = mTextBox.Text;
                form.SelectionStart = mTextBox.SelectionStart;
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    SetText(form.Data);
                    mTextBox.SelectionStart = form.SelectionStart;
                    mTextBox.ScrollToCaret();
                }
                form.Dispose();
            }
            else
            {
                MessageBox.Show("Please open a gamesave file first", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void autoCorrectScheduleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            autoCorrectScheduleToolStripMenuItem.Checked = !autoCorrectScheduleToolStripMenuItem.Checked;
            SchedulerHelper.AUTO_CORRECT_SCHEDULE = autoCorrectScheduleToolStripMenuItem.Checked;
        }

        private void formatScheduleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> lines = new List<string>(this.mTextBox.Lines);
            SchedulerHelper helper = new SchedulerHelper(mTool);
            helper.ReLayoutScheduleWeeks(lines);
            mTextBox.Lines = lines.ToArray();
        }

        private void autoUpdateSpecialTeamsDepthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mTool.AutoUpdateSpecialteamsDepth();
        }

        private void listSpecialTeamsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listSpecialTeamsToolStripMenuItem.Checked = !listSpecialTeamsToolStripMenuItem.Checked;
        }

        private void playerEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditPlayer();
        }

        private void listCoachesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listCoachesToolStripMenuItem1.Checked = !listCoachesToolStripMenuItem1.Checked;
        }

        private void fullCoachAttributesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fullCoachAttributesToolStripMenuItem.Checked = !fullCoachAttributesToolStripMenuItem.Checked;
            
            if (fullCoachAttributesToolStripMenuItem.Checked)
                mTool.CoachKey = mTool.CoachKeyAll;
            else
                mTool.CoachKey = GamesaveTool.DefaultCoachKey;
        }

        private void globalEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlobalEditForm form = new GlobalEditForm(this.mTool );
            form.ShowDialog(this);
            form.Dispose();
        }

        private void textCommandsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string content = StaticUtils.GetEmbeddedTextFile("TextCommand.txt");
            MessageForm form = new MessageForm(System.Drawing.SystemIcons.Information);
            form.Text = "Text Commands";
            form.MessageText = content;
            form.ShowCancelButton = false;
            form.Show();
        }

        private void aboutNFL2K5ToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string version = 
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            string content = String.Format(
@"About NFL2K5Tool
Version {0}

YouTube Tutorial: https://youtu.be/NCqsq_2GqYs 
GitHub: https://github.com/BAD-AL/NFL2K5Tool 
Forum: https://forums.operationsports.com/forums/espn-nfl-2k5-football/881901-nfl2k5tool.html

====== NFL2K5 Related Sites ====== 
NFL2K5 Rosters site: http://nfl2k5rosters.com/
Operation Sports NFL2K5 forum: https://forums.operationsports.com/forums/espn-nfl-2k5-football/

", version);
                ;
            MessageForm form = new MessageForm(System.Drawing.SystemIcons.Information);
            form.Text = "NFL2K5Tool Version " + version;
            form.MessageText = content;
            form.ShowCancelButton = false;
            form.ShowDialog();
            form.Dispose();
        }

        private void file_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void file_DragDrop(object sender, DragEventArgs e)
        {
            Control tb = sender as Control;
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files != null && files.Length == 1 && tb != null)
            {
                LoadSaveFile( files[0]);
            }
        }
    }
}
