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
                statusBar1.Text = "Unable to load: " + filename;
                EnableControls(false);
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
            debugDialogMenuItem.Enabled = 
            resetKeyToolStripMenuItem.Enabled =
            checkToolStripMenuItem.Enabled = 
            pCSX2PhotoBatchFileToolStripMenuItem.Enabled =
            pCSX2PhotoYAMLToolStripMenuItem.Enabled =
                enable;
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


            builder.Append("\n# Uncomment line below to Set Salary Cap -> 198.2M\n");
            builder.Append("# SET(0x9ACCC, 0x38060300)\n\n");

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
            if (mTool.SaveType == SaveType.Franchise)
            {
                SchedulerHelper helper = new SchedulerHelper(mTool);
                mTextBox.AppendText("\n\n#Schedule\n");
                mTextBox.AppendText(helper.GetSchedule());
            }
            else
                MessageBox.Show(this, "Load a franchise file to see schedule.", "A roster file currently loaded");
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

        private void saveTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = "Save Text As";
            dlg.Filter = "txt/csv (*.txt, *.csv|*.txt;*.csv)";
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(dlg.FileName, this.mTextBox.Text);
            }
            dlg.Dispose();
        }

        private void mSaveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = "Apply and Save data to";
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                ApplyTextToSave();
                mTool.SaveFile(dlg.FileName);
                StaticUtils.ShowErrors(false);
            }
            dlg.Dispose();
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

        private string GetKey(string text)
        {
            string retVal = null;
            Regex keyReg = new Regex("[\\s]*([#|Key=](.*lname,.*))", RegexOptions.IgnoreCase);
            Match m = keyReg.Match(text);
            if (m != Match.Empty)
            {
                retVal = m.Groups[1].Value.Replace("Key=","");
            }
            return retVal;
        }

        private void ValidatePlayers()
        {
            string key = GetKey(mTextBox.Text);
            if( key == null)
                key = mTool.GetKey(listAttributesToolStripMenuItem.Checked, listApperanceToolStripMenuItem.Checked);

            PlayerValidator v = new PlayerValidator(key);
            if (mTextBox.Text.Length > 100)
            {
                string results = v.ValidatePlayers(mTextBox.Text);
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
                    statusBar1.Text = "No Issues Found with Height/Weight/Body type";
                }
            }
            else
            {
                MessageBox.Show(
                    "This function will validate the player Height, Weight and BodyType base on the text in the main text area.\n" +
                    "List the players in order to use it"
                    , "Player Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (mTextBox.Text.Length > 22)
                {
                    try
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
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("List contents first.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

================== NFL2K5 Related Sites ==================
NFL2K5 Rosters site: http://nfl2k5rosters.com/  (internet archive)
Operation Sports NFL2K5 forum: https://forums.operationsports.com/forums/espn-nfl-2k5-football/
NFL 2K5 Discord Community: https://discord.gg/sBVXzYb
MOD 2K5 Discord Community: https://discord.gg/DQtGbdfG

======= Transfer Saves from USB to PS2 Memory card =======
https://www.youtube.com/watch?v=u_lYjJEi-Gg

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

        private void resetKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mTool != null)
            {
                Console.WriteLine("Resetting Key");
                mTool.SetKey("Key=");
            }
        }


        private void CheckFaces()
        {
            string prevKey = mTool.GetKey(true, true).Replace("#", "Key=");
            FaceForm ff = new FaceForm();
            StringBuilder sb = new StringBuilder();

            mTool.SetKey("Key=Position,fname,lname,Photo,Skin");
            sb.Append(mTool.GetKey(true, true).Replace("#", "Key="));
            sb.Append("\nLookupAndModify\n"+
                "#Team=FreeAgents   (This line is a comment, but allows the player editor to function on this data)\n\n");

            string skin = "";
            string face = "";
            int faceInt = 0;
            int playerLimit = 1928;
            for (int player = 0; player < playerLimit; player++)
            {
                skin = mTool.GetPlayerField(player, "Skin");
                face = mTool.GetAttribute(player, PlayerOffsets.Photo);
                faceInt = Int32.Parse(face);
                switch (skin)
                {
                    case "Skin1":   // white guys
                    case "Skin9":
                    case "Skin17":
                        if (!ff.CheckFace(faceInt, "lightPlayers"))
                        {
                            sb.Append(mTool.GetPlayerData(player, true, true));
                            sb.Append("\n");
                        }
                        break;
                    case "Skin2":  // mixed White&black(light) guys, Samoans
                    case "Skin18": // mixed White&black(light) guys, Samoans, Latino, White,
                        break;
                    // dark guys 
                    case "Skin3": // inconsistently assigned 
                    case "Skin4":
                    case "Skin5":
                    case "Skin6":
                    case "Skin10":
                    case "Skin11":
                    case "Skin12":
                    case "Skin13":
                    case "Skin14":
                    case "Skin19":
                    case "Skin20":
                    case "Skin21":
                    case "Skin22":
                        if (!ff.CheckFace(faceInt, "darkPlayers"))
                        {
                            sb.Append(mTool.GetPlayerData(player, true, true));
                            sb.Append("\n");
                        }
                        break;
                }
            }
            MessageForm.ShowMessage("Results", sb.ToString(), SystemIcons.Information, false, false);
            ff.Dispose();
            mTool.SetKey(prevKey);
        }


        private void CheckDreads()
        {
            string prevKey = mTool.GetKey(true, true).Replace("#", "Key=");
            StringBuilder sb = new StringBuilder();
            FaceForm ff = new FaceForm();
            string dreads, photo;
            int photo_i = 0;
            int playerLimit = 1928;

            for (int i = 0; i < playerLimit; i++)
            {
                photo = mTool.GetPlayerField(i, "Photo");
                photo_i = Int32.Parse(photo);
                if (ff.CheckFace(photo_i, "Dreads"))
                {
                    dreads = mTool.GetPlayerField(i, "Dreads");
                    if (dreads != "Yes")
                    {
                        sb.Append(
                            String.Format("{0},{1},{2},Yes\n",
                                mTool.GetPlayerField(i, "Position"),
                                mTool.GetPlayerName(i, ','),
                                photo
                        ));
                    }
                }
            }
            if (sb.Length > 0)
            {
                sb.Insert(0, 
                    "#Check these players\n\nLookupAndModify\n"+
                    "Key=Position,fname,lname,Photo,Dreads\n\n"+
                    "#Team=FreeAgents    (This line is a comment, but allows the player editor to work)\n");
                MessageForm.ShowMessage("Verify Theese", sb.ToString());
            }
            ff.Dispose();
            mTool.SetKey(prevKey);
        }

        private void checkFacesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckFaces();
        }

        private void checkDreadsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckDreads();
        }

        private void aboutCheckOperationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string message =
@"The 'check' operation results are crafted to be pasted into the main text area.
They are built to be 'LookupAndModify' Commands and editable with the 
Player Edit Form (unless otherwise stated).

Intended workflow:
1. <check operation>
2. Copy results message
3. Paste into Main Text area
4. Double click first player
5. Change/verify player attributes
6. Use 'Next Player' button to go through all the players.
7. Press 'Ok' once all desired changes are made
8. Apply data to gamesave ('Save' button or File->Apply data without saving).
";
            MessageForm.ShowMessage("About Check operations", message, SystemIcons.Question, false, false);
        }

        private void checkSpecialTeamsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder warnings = new StringBuilder();
            string goodReturners = "WR,RB,FB,CB,SS,FS";
            string[] teams = GamesaveTool.Teams;
            string kr1 = "";
            string kr2 = "";
            string pr = "";
            for (int i = 0; i < teams.Length; i++)
            {
                kr1 = mTool.GetSpecialTeamPosition(teams[i], SpecialTeamer.KR1).Replace("KR1,","").TrimEnd("0123456789".ToCharArray());
                kr2 = mTool.GetSpecialTeamPosition(teams[i], SpecialTeamer.KR2).Replace("KR2,","").TrimEnd("0123456789".ToCharArray());
                pr  = mTool.GetSpecialTeamPosition(teams[i], SpecialTeamer.PR ).Replace("PR,", "").TrimEnd("0123456789".ToCharArray());
                if (goodReturners.IndexOf(kr1) < 0) kr1 = "bad";
                if (goodReturners.IndexOf(kr2) < 0) kr2 = "bad";
                if (goodReturners.IndexOf(pr)  < 0) pr  = "bad";

                if (kr1 == "bad" || kr2 == "bad" || pr == "bad")
                {
                    warnings.Append("Team = ");
                    warnings.Append(teams[i]);
                    warnings.Append("\n");
                    if (kr1 == "bad") warnings.Append(mTool.GetSpecialTeamPosition(teams[i], SpecialTeamer.KR1) +"\n");
                    if (kr2 == "bad") warnings.Append(mTool.GetSpecialTeamPosition(teams[i], SpecialTeamer.KR2) + "\n");
                    if (pr == "bad")  warnings.Append(mTool.GetSpecialTeamPosition(teams[i], SpecialTeamer.PR) + "\n");
                }
            }
            if (warnings.Length > 0)
            {
                warnings.Append("\nTry running   Edit -> AutoUpdateDepthChart or manually edit to fix");
                MessageForm.ShowMessage("Check these", warnings.ToString(), SystemIcons.Warning, false, false);
            }
            else
                statusBar1.Text = "No special teams issues found";
        }

        private void pCSX2PhotoYAMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mTool != null)
            {
                PCSX2TextureForm form = new PCSX2TextureForm(this.mTool);
                form.ShowYaml();
                form.Show();
                //string results = mTool.GetPlayerPhotoPCSX2Yaml();
                //MessageForm.ShowMessage("PCSX2 Photo data", results);
            }
        }

        private void deleteTrailingCommasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string output = DeleteTrailingCommas( mTextBox.Text);
            mTextBox.Text = output;
        }

        public static string DeleteTrailingCommas(string text)
        {
            Regex rs = new Regex(",+\n");
            Regex rrs = new Regex(",+$");
            string ret = rs.Replace(text, "\n");
            ret = rrs.Replace(ret, "");

            return ret;
        }

        private void pCSX2PhotoBatchFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mTool != null)
            {
                PCSX2TextureForm form = new PCSX2TextureForm(this.mTool);
                form.ShowBatchFile();
                form.Show();
            }
        }
    }
}