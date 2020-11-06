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
                if (c == '*')
                    ch = 0;
                else 
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
            mResultsTextBox.Text = Tool.GetTeamPlayers(textBox1.Text, false, false, false);
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
            string photo, pbp;
            int max = (int)numericUpDown1.Value;
            for (int i = 0; i < max; i++)
            {
                builder.Append(Tool.GetPlayerData(i, false, false));
                if (includeDepthToolStripMenuItem.Checked)
                {
                    builder.Append(" Depth:");
                    builder.Append(Tool.GetPlayerPositionDepth(i).ToString("X2"));
                }
                if (includePhotePBPToolStripMenuItem.Checked)
                {
                    photo = Tool.GetAttribute(i, PlayerOffsets.Photo);
                    pbp = Tool.GetAttribute(i, PlayerOffsets.PBP);
                    builder.Append(photo);
                    builder.Append(",");
                    builder.Append(pbp);
                    builder.Append(",");
                    builder.Append(DataMap.GetPlayerNameForPhoto(photo));
                    builder.Append(",");
                    builder.Append(DataMap.GetPlayerNameForPBP(photo));
                    builder.Append(",");
                    
                    if (Tool.GetAttribute(i, PlayerOffsets.Photo) != Tool.GetAttribute(i, PlayerOffsets.PBP))
                    {
                        builder.Append("****,");
                    }
                }
                if (mNumBytes.Value > 0)
                {
                    int dataStart = Tool.GetPlayerDataStart(i) + (int)mOffsetUpDown.Value;
                    builder.Append(" ");
                    for (int j = 0; j < mNumBytes.Value; j++)
                    {
                        builder.Append(Tool.GameSaveData[dataStart + j].ToString("X2"));
                    }
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

        private void autoUpdateDepthChartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tool.AutoUpdateDepthChart();
        }

        private void includePhotePBPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            includePhotePBPToolStripMenuItem.Checked = !includePhotePBPToolStripMenuItem.Checked;
        }

        int FirstPlayerFNamePtr = 0xe290;
        int photoDistance = 0x32;
        int playerSize = 0xD0;

        // getting data from NFL2k2 gamesave
        private string GetNFL2K2PhotoData()
        {
            FirstPlayerFNamePtr = 0xe290;
            photoDistance = 0x32;
            playerSize = 0xD0;

            return GetNameData();
        }

        // getting data from NFL2k2 gamesave
        private string GetNFL2K3PhotoData()
        {
            FirstPlayerFNamePtr = 0xFB28;
            photoDistance = -10;
            playerSize = 0x54;

            return GetNameData();
        }

        // getting data from NFL2k4 gamesave
        private string GetNFL2K4PhotoData()
        {
            FirstPlayerFNamePtr = 0x1132c;
            photoDistance = -6;
            playerSize = 0x50;

            return GetNameData();
        }

        private string GetNameData()
        {
            StringBuilder builder = new StringBuilder(5000);
            int ptrLoc = 0;
            int photoLoc = 0;
            try
            {
                for (int i = 0; i < numericUpDown1.Value; i++)
                {
                    ptrLoc = FirstPlayerFNamePtr + (i * playerSize);
                    photoLoc = ptrLoc + photoDistance;
                    builder.Append(Tool.GetString(Tool.GetPointerDestination(ptrLoc + 4))); // lname
                    builder.Append(", ");
                    builder.Append(Tool.GetString(Tool.GetPointerDestination(ptrLoc))); // fname
                    builder.Append("=");
                    builder.Append(Get2BytePointer(photoLoc));
                    //if (Get2BytePointer(photoLoc) != Get2BytePointer(photoLoc + 2))
                    //    builder.Append("*** " + Get2BytePointer(photoLoc + 2));
                    //builder.Append("," + ptrLoc.ToString("X"));
                    builder.Append("\n");
                }
            }
            catch { }
            return builder.ToString();
        }

        private string Get2BytePointer(int photoLoc)
        {
            string retVal = "";
            int val = Tool.GameSaveData[photoLoc + 1] << 8;
            val += Tool.GameSaveData[photoLoc];
            if (val < 10)
                retVal = "000" + val;
            else if (val < 100)
                retVal = "00" + val;
            else if (val < 1000)
                retVal = "0" + val;
            else
                retVal = val.ToString();
            return retVal;
        }

        private void extractPHOHO2K2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mResultsTextBox.Text = GetNFL2K2PhotoData();
        }

        private void extractPhoto2K3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mResultsTextBox.Text = GetNFL2K3PhotoData();
        }

        private void extractPhoto2K4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mResultsTextBox.Text = GetNFL2K4PhotoData();
        }

        private void mSetByteLocUpDown_ValueChanged(object sender, EventArgs e)
        {
            mSetByteValTextBox.Text = Tool.GameSaveData[(int)mSetByteLocUpDown.Value].ToString("X2");
        }

        private void SetBytes()
        {
            byte b1 = 0;
            int loc = (int)mSetByteLocUpDown.Value;
            try
            {
                for (int i = 0; i < mSetByteValTextBox.Text.Length; i += 2)
                {
                    b1 = (byte)UInt16.Parse(mSetByteValTextBox.Text.Substring(i, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
                    Tool.GameSaveData[loc] = b1;
                    loc++;
                }
            }
            catch
            {
                mStatusLabel.Text = "Set Byte error.";
            }

        }

        private void mSetByteButton_Click(object sender, EventArgs e)
        {
            SetBytes();
        }

        private void mSetByteValTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SetBytes();
            }
        }

        private void mFindBytesButton_Click(object sender, EventArgs e)
        {
            FindBytesInFile();
        }

        private void FindBytesInFile()
        {
            string val = textBox1.Text;
            try
            {
                string part = "";
                if (val.StartsWith("0x"))
                    val = val.Substring(2);
                byte[] bytesToSearch = new byte[val.Length / 2];
                int j = 0;
                for (int i = 0; i < val.Length; i+=2)
                {
                    part = val.Substring(i, 2);
                    bytesToSearch[j++] = Byte.Parse(part, System.Globalization.NumberStyles.AllowHexSpecifier);
                }
                mResultsTextBox.Clear();
                List<long> locs = StaticUtils.FindByesInFile(bytesToSearch, Tool.GameSaveData, 0, Tool.GameSaveData.Length);
                foreach (int loc in locs)
                {
                    mResultsTextBox.AppendText(String.Format("{0:x}\n", loc));
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Could not search for: " + textBox1.Text +
                    ". Ensure you are searching with valid characters [0-9A-F], even number of characters",
                    "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void extractTeamSectionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            byte[] teamBytes;
            string[] teams = {
                 "49ers", "Bears","Bengals", "Bills", "Broncos", "Browns","Buccaneers", "Cardinals", 
                 "Chargers", "Chiefs","Colts","Cowboys",  "Dolphins", "Eagles","Falcons","Giants","Jaguars",
                 "Jets","Lions","Packers", "Panthers", "Patriots","Raiders","Rams","Ravens","Redskins",
                 "Saints","Seahawks","Steelers", "Texans", "Titans",  "Vikings"};
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < teams.Length; i++)
            {
                builder.Append(teams[i]);
                builder.Append(",");
                teamBytes = Tool.GetTeamBytes(teams[i]);
                for (int j = 0; j < teamBytes.Length; j++)
                {
                    builder.Append(teamBytes[j].ToString("X2"));
                    builder.Append(",");
                }
                builder.Append("\n");
            }
            mResultsTextBox.Text = builder.ToString();
        }

        private void mathTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable tab = new DataTable();
            Object result= tab.Compute(mResultsTextBox.Text, "");
            mResultsTextBox.Text = result.ToString();
        }

        private void launchTempFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PictureHelper h = new PictureHelper();
            h.Show();
        }

        private void launchPhotoDataEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PhotoDataEditor pde = new PhotoDataEditor();
            pde.Show();
        }

        private void listDepthChartsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] teams = {
                 "49ers", "Bears","Bengals", "Bills", "Broncos", "Browns","Buccaneers", "Cardinals", 
                 "Chargers", "Chiefs","Colts","Cowboys",  "Dolphins", "Eagles","Falcons","Giants","Jaguars",
                 "Jets","Lions","Packers", "Panthers", "Patriots","Raiders","Rams","Ravens","Redskins",
                 "Saints","Seahawks","Steelers", "Texans", "Titans",  "Vikings"};

            StringBuilder builder = new StringBuilder();
            foreach(string team in teams)
            {
                builder.Append(team +"\n");
                builder.Append( Tool.GetSpecialTeamDepthChart(team));
            }
            mResultsTextBox.Text = builder.ToString();
        }

        private void autoUpdateYearsProToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string baseYear = StringInputDlg.GetString("Base year:", "Enter the year for the season", "2004");
            string teamsString = StringInputDlg.GetString("Teams:", "Enter the teams you wish to update (seperated by comma), blank for all teams", "");
            string[] teams = teamsString.Split(new char[] { ',' });
            if (teamsString == "" || teams.Length < 1)
            {
                teams =  new string[]  {
                 "49ers", "Bears","Bengals", "Bills", "Broncos", "Browns","Buccaneers", "Cardinals", 
                 "Chargers", "Chiefs","Colts","Cowboys",  "Dolphins", "Eagles","Falcons","Giants","Jaguars",
                 "Jets","Lions","Packers", "Panthers", "Patriots","Raiders","Rams","Ravens","Redskins",
                 "Saints","Seahawks","Steelers", "Texans", "Titans",  "Vikings", 
                 "FreeAgents", "DraftClass" 
                 };
            }
            
            if (!String.IsNullOrEmpty(baseYear))
            {
                int year = 0;
                Int32.TryParse(baseYear, out year);
                Tool.AutoUpdateYearsProFromYear(year, teams);
			}
		}
		
        private void mGetBytesButton_Click(object sender, EventArgs e)
        {
            try
            {
                int length = Int32.Parse(mGetBytesTextBox.Text, System.Globalization.NumberStyles.AllowHexSpecifier);
                int start = (int)mSetByteLocUpDown.Value;
                int end = start + length;
                StringBuilder builder = new StringBuilder(length * 3);
                for (int i = start; i < end; i++)
                {
                    builder.Append(string.Format("{0:X2} ", Tool.GameSaveData[i]));
                }
                mResultsTextBox.Text = builder.ToString();
            }
            catch (Exception ex)
            {
                mStatusLabel.Text = ex.Message;
            }
        }

        private void autoUpdatePBPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tool.AutoUpdatePBP();
        }

        private void autoUpdatePhotosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tool.AutoUpdatePhoto();
        }
        string mApperanceKey = "#Position,fname,lname,JerseyNumber,College,DOB,PBP,Photo,YearsPro,Hand,Weight,Height,BodyType,Skin,Face,Dreads,Helmet,FaceMask,Visor,EyeBlack,MouthPiece,LeftGlove,RightGlove,LeftWrist,RightWrist,LeftElbow,RightElbow,Sleeves,LeftShoe,RightShoe,NeckRoll,Turtleneck";
        
        private void updatePlayerAppearanceFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fileName = null;
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.RestoreDirectory = true;
            dlg.Title = "Select Player appearance data file";
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                fileName = dlg.FileName;
            }
            dlg.Dispose();

            if (fileName != null)
            {
                String text = System.IO.File.ReadAllText(fileName).Replace("\r\n", "\n");
                String currentLine, playerKey;
                int index = 0;
                int numUpdated = 0;
                int endIndex = 0;
                if (text != null && text.IndexOf(mApperanceKey) < 10)
                {
                    Tool.SetKey(mApperanceKey.Replace("#", "KEY="));
                    InputParser inputParser = new InputParser(Tool);

                    for (int i = 0; i < Tool.MaxPlayers; i++)
                    {
                        playerKey = String.Concat(Tool.GetAttribute(i, PlayerOffsets.Position), ",", 
                                                  Tool.GetPlayerFirstName(i), ",", 
                                                  Tool.GetPlayerLastName(i), ",");
                        index = text.IndexOf(playerKey);
                        if (index > -1)
                        {
                            endIndex = text.IndexOf('\n', index + 10);
                            currentLine = text.Substring(index, endIndex-index);
                            inputParser.SetPlayerData(i, currentLine, false);
                            numUpdated++;
                        }
                    }
                    Tool.SetKey("");
                    mStatusLabel.Text = "Updated " + numUpdated + " Players";
                    MessageBox.Show(mStatusLabel.Text);
                }
                else
                {
                    MessageBox.Show(this, "Error", "Use a valid Apperance file!\nExtract only 'Apperance' from another save and stash in a file.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void apperanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("#");
            builder.Append(Tool.CoachKey);
            builder.Append("\r\n");
            for (int i = 0; i < 32; i++)
            {
                builder.Append( Tool.GetCoachData(i));
                builder.Append("\r\n");
            }
            mResultsTextBox.Text = builder.ToString();
        }

        private void gatherFaceSkinDtaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder();
            Tool.GetSkinPhotoMappings(builder);
            mResultsTextBox.Text = builder.ToString();
        }

        private void applyDataToCurrentSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "This process will apply the given data to the current save file in memory.\r\n"+
                "You will be prompted for for the data, it must like be in the following example (does not need to be facemask and glove data):\r\n"+
                "   Key=Position,fname,lname,FaceMask,LeftGlove,RightGlove\r\n"+
                "   LookupAndModify \r\n" +
                "   QB,Steve,Young,FaceMask2,None,Team1\r\n"+
                "   QB,Joe,Montana,FaceMask3,Team1,None\r\n\r\n" +
                "In the example above thie feature will search through all players named 'Steve Young' playing QB and\r\n"+
                "set his facemask, left glove and right glove to the data given. Will do the same with 'Joe Montana'"
                ,"Info");
            string dataToApply = MessageForm.GetString("Paste data below", "");
            if (dataToApply != null)
            {
                dataToApply = dataToApply.Replace("Team =","#Team =");
                string key = Tool.GetKey(true, true).Replace("#", "");
                InputParser inputParser = new InputParser(Tool);
                inputParser.ProcessText("LookupAndModify\r\n");
                inputParser.ProcessText(dataToApply);
                Tool.SetKey("Key=");
                Tool.GetKey(true, true);
            }
        }

        /// <summary>
        /// Takes list of position , names, looks up those players (if they exist) puts their data into the textbox.
        /// </summary>
        private void getPlayersByPositionAndNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder();
            List<int> playersToApplyTo = null;
            List<string> attributes = null;
            MessageBox.Show(
                     "This process willget the players specified by position,fname,lname.\r\n" +
                     "You will be prompted for for the data, it must like be in the following example:\r\n" +
                     "   QB,Steve,Young\r\n" +
                     "   QB,Joe,Montana\r\n" +
                     "In the example above thie feature will search through all players named 'Steve Young' and 'Joe Montana' playing QB and\r\n" +
                     "retrieve their data."
                     , "Info");
            string dataToApply = MessageForm.GetString("Paste data below", "");
            if (String.IsNullOrEmpty(dataToApply))
                return;

            string[] lines = dataToApply.Replace("\r\n", "\n").Split(new char[] { '\n' });
            foreach (string line in lines)
            {
                if (line.StartsWith("#") || line.Length < 3)
                {
                    // skip comments
                }
                else
                {
                    attributes = InputParser.ParsePlayerLine(line);
                    if (attributes != null && attributes.Count > 2)
                    {
                        playersToApplyTo = Tool.FindPlayer(attributes[0], attributes[1], attributes[2]);
                        if (playersToApplyTo.Count > 0)
                        {
                            builder.Append(Tool.GetPlayerData(playersToApplyTo[0], true, true));
                            builder.Append("\r\n");
                        }
                        else
                        {
                            //builder.Append("# notFound> ");
                            //builder.Append(line);
                            //builder.Append("\r\n");
                        }
                    }
                }
            }
            this.mResultsTextBox.Text = builder.ToString();
        }
        /// <summary>
        /// Goes through the Text in the results text box, removes players (lookup by name) who are on teams (above FreeAgents)
        /// </summary>
        private void removeFreeAgentsThatAreOnATeamtextOperationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder(mResultsTextBox.Text.Length);
            int freeAgentIndex = mResultsTextBox.Text.IndexOf("FreeAgents");
            if (freeAgentIndex < 100)
            {
                mStatusLabel.Text = "No data to process";
                return;
            }
            string teamsText = mResultsTextBox.Text.Substring(0, freeAgentIndex);
            string freeAgentText = mResultsTextBox.Text.Substring(freeAgentIndex);
            string[] lines = freeAgentText.Replace("\r\n","\n").Split(new char[]{'\n'});

            int commaIndex = 0;
            builder.Append(teamsText);
            string dude = "";
            foreach (string line in lines)
            {
                commaIndex = InputParser.NthIndex(line, ',', 3);
                if (commaIndex > -1)
                {
                    dude = line.Substring(0, commaIndex);
                    if (teamsText.IndexOf(dude) > -1)
                    {
                    }
                    else
                        builder.Append(line + "\r\n");
                }
            }
            mResultsTextBox.Text = builder.ToString();
            mStatusLabel.Text = "Updated free agent data";
        }

        private void getPlayerBytesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder();
            List<string> attributes = null;
            MessageBox.Show(
                     "This process will get the players specified by position,fname,lname.\r\n" +
                     "You will be prompted for for the data, it must like be in the following example:\r\n" +
                     "   QB,Steve,Young\r\n" +
                     "   QB,Joe,Montana\r\n" +
                     "In the example above thie feature will search through all players named 'Steve Young' and 'Joe Montana' playing QB and\r\n" +
                     "retrieve their data."
                     , "Info");
            string dataToApply = MessageForm.GetString("Paste data below", "");
            byte[] playerBytes = null;
            if (dataToApply != null)
            {
                string[] lines = dataToApply.Replace("\r\n", "\n").Split(new Char[] { '\n' });
                foreach (string line in lines)
                {
                    if (line.StartsWith("#") || line.Length < 3)
                    {
                        // skip comments
                    }
                    else
                    {
                        attributes = InputParser.ParsePlayerLine(line);
                        if (attributes != null && attributes.Count > 2)
                        {
                            playerBytes = Tool.GetPlayerBytes(attributes[0], attributes[1], attributes[2]);
                            if (playerBytes != null)
                            {
                                for (int i = 0; i < playerBytes.Length; i++)
                                {
                                    builder.Append(playerBytes[i].ToString("X2"));
                                    builder.Append(" ");
                                }
                                builder.Append("\r\n");
                            }
                            else
                            {
                                builder.Append("# notFound> ");
                                builder.Append(line);
                                builder.Append("\r\n");
                            }
                        }
                    }
                }
                mResultsTextBox.Text = builder.ToString();
            }
        }

        ScreenCaptureForm mImageCaptureForm = null;

        private void imageCaptureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mImageCaptureForm == null)
                mImageCaptureForm = new ScreenCaptureForm();
            mImageCaptureForm.Show(this);
        }

        private void getCoachItem_Click(object sender, EventArgs e)
        {
            string number = StringInputDlg.GetString("Enter Coach number", "(0-31", "0");
            try
            {
                int num = Int32.Parse(number);
                if (num > -1 && num < 32)
                {
                    Tool.CoachKey = Tool.CoachKeyAll;
                    mResultsTextBox.Text = String.Format("#coach:{0}\n{1}\n{2}",num,  Tool.CoachKeyAll, Tool.GetCoachData(num));
                }
            }
            catch { 
                
            }
        }


        private void getCoachBytesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string number = StringInputDlg.GetString("Enter Coach number", "(0-31", "0");
            try
            {
                int num = Int32.Parse(number);
                if (num > -1 && num < 32)
                {
                    byte[] data = Tool.GetCoachBytes(num);
                    StringBuilder sb = new StringBuilder(data.Length * 2 + 4);
                    sb.Append("0x");
                    for (int i = 0; i < data.Length; i++)
                    {
                        sb.Append(String.Format("{0:x2}", data[i]));
                    }
                    mResultsTextBox.Text = num + ": Coach Bytes\n" + sb.ToString();
                }
            }
            catch
            {

            }
        }

        private void replaceStringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string searchStr = StringInputDlg.GetString("Enter Search String", "");
            string replaceStr = StringInputDlg.GetString("Enter String to replace it with", "");

            if (replaceStr.Length > searchStr.Length)
            {
                MessageBox.Show("Error, cannot replace a string with a longer string");
                return;
            }
            while (replaceStr.Length < searchStr.Length)
                replaceStr = replaceStr + " ";

            List<long> locs = StaticUtils.FindStringInFile(searchStr, SaveFile, 0, SaveFile.Length);
            for (int i = 0; i < locs.Count; i++)
            {
                int stringLoc = (int)locs[i];
                for (int j = 0; j < replaceStr.Length; j++)
                {
                    Tool.SetByte(stringLoc, (byte)replaceStr[j]);
                    Tool.SetByte(stringLoc + 1, 0);
                    stringLoc += 2;
                }
            }
        }

        private void fixupRookieYearsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] teams = {
                 "49ers", "Bears","Bengals", "Bills", "Broncos", "Browns","Buccaneers", "Cardinals", 
                 "Chargers", "Chiefs","Colts","Cowboys",  "Dolphins", "Eagles","Falcons","Giants","Jaguars",
                 "Jets","Lions","Packers", "Panthers", "Patriots","Raiders","Rams","Ravens","Redskins",
                 "Saints","Seahawks","Steelers", "Texans", "Titans",  "Vikings", "FreeAgents" };

            List<int> playerPointers = null;
            int yearsPro =0;

            for (int t = 0; t < teams.Length; t++)
            {
                playerPointers = Tool.GetPlayerIndexesForTeam(teams[t]);
                for (int player = 0; player < playerPointers.Count; player++)
                {
                    yearsPro = Int32.Parse( Tool.GetAttribute(playerPointers[player], PlayerOffsets.YearsPro));
                    if (yearsPro == 0)
                        Tool.SetAttribute(playerPointers[player], PlayerOffsets.YearsPro, "1");
                    else if (yearsPro == 1)
                        Tool.SetAttribute(playerPointers[player], PlayerOffsets.YearsPro, "2");
                    
                    //dob = GetAttribute(playerPointers[i], PlayerOffsets.DOB); // retVal = string.Concat(new object[] { month, "/", day, "/", year });
                    //parts = dob.Split(chars);
                    //if (parts.Length > 2 && Int32.TryParse(parts[2], out birthYear))
                    //{
                    //    yearsPro = currentYear - (birthYear + 22);
                    //    if (yearsPro < 0)
                    //        yearsPro = 0;
                    //    SetAttribute(playerPointers[i], PlayerOffsets.YearsPro, yearsPro.ToString());
                    //}
                }
            }
        }

        private void findPlayersMenuItem_Click(object sender, EventArgs e)
        {
            String content = "LookupPlayer\r\n" + 
                mResultsTextBox.Text;
            InputParser parser = new InputParser(Tool);
            parser.ProcessText(content);
            string results = Tool.GetKey(true, true) + "\r\n" +
                parser.GetLookupPlayers();
            MessageForm.ShowMessage("Results", results, SystemIcons.Information, false, false);
        }

        private void listDepthChartsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageForm.ShowMessage("Results", Tool.GetDepthCharts(), SystemIcons.Information, false, false);
        }

        private void checkFacesskinMismatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckFaces();
        }

        private void tryPS2FileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ARMaxTestForm form = new ARMaxTestForm();
            form.Show();
        }

        private void checkDreadsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckDreads();
        }

        private void CheckFaces()
        {
            FaceForm ff = new FaceForm();
            StringBuilder sb = new StringBuilder();

            Tool.SetKey("Key=Position,fname,lname,Photo,Skin");
            sb.Append(Tool.GetKey(true, true).Replace("#", "Key="));
            sb.Append("\nLookupAndModify\n#Team=FreeAgents   (This line is a comment, but allows the player editor to work)\n\n");

            string skin = "";
            string face = "";
            int faceInt = 0;
            int playerLimit = 1928;
            for (int player = 0; player < playerLimit; player++)
            {
                skin = Tool.GetPlayerField(player, "Skin");
                face = Tool.GetAttribute(player, PlayerOffsets.Photo);
                faceInt = Int32.Parse(face);
                switch (skin)
                {
                    case "Skin1":   // white guys
                    case "Skin9":
                    case "Skin17":
                        if (!ff.CheckFace(faceInt, "lightPlayers"))
                        {
                            sb.Append(Tool.GetPlayerData(player, true, true));
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
                            sb.Append(Tool.GetPlayerData(player, true, true));
                            sb.Append("\n");
                        }
                        break;
                }
            }
            MessageForm.ShowMessage("Results", sb.ToString(), SystemIcons.Information, false, false);
        }


        private void CheckDreads()
        {
            StringBuilder sb = new StringBuilder();
            FaceForm ff = new FaceForm();
            string dreads, photo;
            int photo_i = 0;
            int playerLimit = 1928;

            for(int i=0; i < playerLimit; i++)
            {
                photo = Tool.GetPlayerField(i, "Photo");
                photo_i = Int32.Parse(photo);
                if (ff.CheckFace(photo_i, "Dreads"))
                {
                    dreads = Tool.GetPlayerField(i, "Dreads");
                    if (dreads != "Yes")
                    {
                        sb.Append(
                            String.Format("{0},{1},{2},Yes\n",
                                Tool.GetPlayerField(i, "Position"),
                                Tool.GetPlayerName(i, ','),
                                photo
                        ));
                    }
                }
            }
            if (sb.Length > 0)
            {
                sb.Insert(0, "#Check these players\n\nLookupAndModify\nKey=Position,fname,lname,Photo,Dreads\n\n#Team=FreeAgents    (This line is a comment, but allows the player editor to work)\n");
                MessageForm.ShowMessage("Verify Theese", sb.ToString());
            }
        }

    }
}
