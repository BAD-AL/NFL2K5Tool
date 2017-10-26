using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace NFL2K5Tool
{
    public partial class PlayerEditForm : Form
    {
        private string[] mKeyParts;
        private bool mInitializing = true;

        /// <summary>
        /// Constructor.
        /// </summary>
        public PlayerEditForm()
        {
            InitializeComponent();

            // issues setting these in the designer; too lazy to set the attributes on the controls to get it to work.
            Position.Text = "Position";
            Position.RepresentedValue = typeof(Positions);
            Position.BorderStyle = BorderStyle.None;
        }

        /// <summary>
        /// the colleges.
        /// </summary>
        public string[] Colleges { get; set; }

        /// <summary>
        /// PBP names
        /// </summary>
        public Dictionary<string, string> ReversePBPs { get; set; }

        /// <summary>
        /// Photo names
        /// </summary>
        public Dictionary<string,string> ReversePhotos { get; set; }

        /// <summary>
        /// PBP names
        /// </summary>
        public Dictionary<string, string> PBPs { get; set; }

        /// <summary>
        /// Photo names
        /// </summary>
        public Dictionary<string, string> Photos { get; set; }

        private string mData = "";
        /// <summary>
        /// The text corresponding to all the text in the main GUI.
        /// </summary>
        public string Data
        { 
            get { return mData; }
            set
            {
                mData = value;
                SetupTeams();
                SetupGui();
            }
        }
        private string mKeyString = "";
        private const string mSkillsString = "Speed,Agility,Strength,Jumping,Coverage,PassRush,RunCoverage,PassBlocking,RunBlocking,Catch,RunRoute,BreakTackle,HoldOntoBall,PowerRunStyle,PassAccuracy,PassArmStrength,PassReadCoverage,Tackle,KickPower,KickAccuracy,Stamina,Durability,Leadership,Scramble,Composure,Consistency,Aggressiveness,";
        private const string mAppearanceString = "JerseyNumber,College,DOB,PBP,Photo,YearsPro,Hand,Weight,Height,BodyType,Skin,Face,Dreads,Helmet,FaceMask,Visor,EyeBlack,MouthPiece,LeftGlove,RightGlove,LeftWrist,RightWrist,LeftElbow,RightElbow,Sleeves,LeftShoe,RightShoe,NeckRoll,Turtleneck,";
        
        /// <summary>
        /// Returns true when a new key is set, false if it's the same key
        /// </summary>
        private bool SetupKey()
        {
            bool retVal = false;
            Regex keyReg = new Regex("^#(Position,fname,lname,.*)", RegexOptions.IgnoreCase);
            Match m = keyReg.Match(Data);
            if (m != Match.Empty )
            {
                if (mKeyString != m.Groups[1].Value)
                {
                    mKeyString = m.Groups[1].Value;
                    mKeyParts = mKeyString.Split(new char[] { ',' });
                    retVal = true;
                }
            }
            else
            {
                MessageBox.Show("Please make sure the key '#Position,fname,lname...' is present at the top of the text box.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return retVal;
        }

        /// <summary>
        /// Initializes the Form.
        /// Call this after Colleges, Photos & PBPs have been set.
        /// </summary>
        private void SetupGui()
        {
            if (SetupKey())
            {
                ClearControls(mSkillsTab);
                ClearControls(mAppearanceTab);

                foreach (string attr in mKeyParts)
                {
                    if (attr.Length > 0 && mSkillsString.IndexOf(attr + ",") > -1)
                    {
                        AddSkill(attr);
                    }
                }

                foreach (string attr in mKeyParts)
                {
                    if (attr.Length > 0 && mAppearanceString.IndexOf(attr + ",") > -1)
                    {
                        AddAppearance(attr);
                    }
                }
            }
        }

        /// <summary>
        /// Clears and disposed of the child controls on parentControl.
        /// </summary>
        private void ClearControls(Control parentControl)
        {
            Control c = null;
            IntAttrControl iac = null;
            StringSelectionControl ssc = null;
            for (int i = parentControl.Controls.Count - 1; i > -1; i--)
            {
                c = parentControl.Controls[i];
                parentControl.Controls.Remove(c);
                ssc = c as StringSelectionControl;
                iac = c as IntAttrControl;
                if( ssc != null)
                    ssc.ValueChanged -= new EventHandler(ValueChanged);
                else if( iac != null)
                    iac.ValueChanged -= new EventHandler(ValueChanged);
                c.Dispose();
            }
        }

        private void AddSkill(string skill)
        {
            Control c = null;
            if (skill == "PowerRunStyle")
            {
                StringSelectionControl ssc = new StringSelectionControl();
                ssc.RepresentedValue = typeof(PowerRunStyle);
                ssc.Name = ssc.Text = skill;
                ssc.ValueChanged += new EventHandler(ValueChanged);
                c = ssc;
            }
            else
            {
                IntAttrControl iac = new IntAttrControl();
                iac.Name = iac.Text = skill;
                iac.ValueChanged += new EventHandler(ValueChanged);
                c = iac;
            }
            int row = mSkillsTab.Controls.Count / 5;
            int col = mSkillsTab.Controls.Count % 5;
            c.Location = new Point(col * c.Width, row * c.Height);
            mSkillsTab.Controls.Add(c);
        }

        private void AddAppearance(string appearance)
        {
            Control c = null;
            IntAttrControl intAttrCtrl = null;
            StringSelectionControl ctrl = null;
            if( "Weight,DOB,YearsPro,JerseyNumber,".IndexOf(appearance+",") > -1)
            {
                switch( appearance)
                {
                    case "JerseyNumber":
                    case "YearsPro":
                        intAttrCtrl = new IntAttrControl();
                        intAttrCtrl.Name = intAttrCtrl.Text = appearance;
                        intAttrCtrl.Max = 99;
                        break;
                    case "Weight":
                        intAttrCtrl = new IntAttrControl();
                        intAttrCtrl.Name = intAttrCtrl.Text = appearance;
                        intAttrCtrl.Min = 150;
                        intAttrCtrl.Max = intAttrCtrl.Min + 255;
                        break;
                    case "DOB":
                        DateValueControl dvc = new DateValueControl();
                        dvc.Name = dvc.Text = appearance;
                        dvc.ValueChanged += new EventHandler(ValueChanged);
                        c = dvc;
                        break;
                }
            }
            else if( "Hand,BodyType,Skin,Face,MouthPiece,EyeBlack,Dreads,Helmet,Sleeves,Visor,Turtleneck,Height,College,PBP,".IndexOf(appearance+",") > -1) {
                ctrl = new StringSelectionControl();
                ctrl.Name = ctrl.Text = appearance;
                switch (appearance)
                {
                    case "Hand": ctrl.RepresentedValue = typeof(Hand); break;
                    case "BodyType": ctrl.RepresentedValue = typeof(Body); break;
                    case "Skin": ctrl.RepresentedValue = typeof(Skin); break;
                    case "Face": ctrl.RepresentedValue = typeof(Face); break;
                    case "MouthPiece": 
                    case "EyeBlack": 
                    case "Dreads":  ctrl.RepresentedValue = typeof(YesNo);  break;
                    case "Helmet": ctrl.RepresentedValue = typeof(Helmet); break;
                    case "Sleeves": ctrl.RepresentedValue = typeof(Sleeves); break;
                    case "Visor": ctrl.RepresentedValue = typeof(Visor); break;
                    case "Turtleneck": ctrl.RepresentedValue = typeof(Turtleneck); break;
                    case "Height": //5'6"-7'0"
                        ctrl.SetItems(new string[] { 
                            "5'6\"", "5'7\"", "5'8\"", "5'9\"", "5'10\"", "5'11\"", 
                            "6'0\"", "6'1\"", "6'2\"", "6'3\"", "6'4\"",  "6'5\"", 
                            "6'6\"", "6'7\"", "6'8\"", "6'9\"", "6'10\"", "6'11\"", 
                            "7'0\"" });
                        break;
                    case "College":
                        ctrl.DropDownStyle = ComboBoxStyle.DropDown;
                        ctrl.SetItems(Colleges);
                        break;
                    case "PBP":
                        string[] values = new string[this.ReversePBPs.Count];
                        this.ReversePBPs.Values.CopyTo(values, 0);
                        ctrl.SetItems(values);
                        ctrl.DropDownStyle = ComboBoxStyle.DropDown;
                        break;
                }
            }
            else
            {
                ctrl = new PictureChooser();
                ctrl.Name = ctrl.Text = appearance;
                ctrl.ValueChanged += new EventHandler(PictureChooser_ValueChanged);
                        
                switch(appearance)
                {
                    // need to special case the setting of these items.
                    case "FaceMask": ctrl.RepresentedValue = typeof(FaceMask); break;
                    case "LeftElbow":
                    case "RightElbow": ctrl.RepresentedValue = typeof(Elbow); break;
                    case "LeftGlove":
                    case "RightGlove": ctrl.RepresentedValue = typeof(Glove); break;
                    case "LeftWrist":
                    case "RightWrist": ctrl.RepresentedValue = typeof(Wrist); break;
                    case "LeftShoe":
                    case "RightShoe": ctrl.RepresentedValue = typeof(Shoe); break;
                    case "NeckRoll": ctrl.RepresentedValue = typeof(NeckRoll); break;
                    case "Photo":
                        ctrl.DropDownStyle = ComboBoxStyle.DropDown;
                        ctrl.ValueChanged += new EventHandler(PictureChooser_ValueChanged);
                        string[] vals = new string[this.ReversePhotos.Count];
                        this.ReversePhotos.Values.CopyTo(vals, 0);
                        ctrl.SetItems(vals);
                        break;
                }
            }
            if (intAttrCtrl != null)
            {
                intAttrCtrl.ValueChanged += new EventHandler(ValueChanged);
                c = intAttrCtrl;
            }
            else if (ctrl != null)
            {
                ctrl.ValueChanged += new EventHandler(ValueChanged);
                c = ctrl;
            }

            if (c != null)
            {
                int row = mAppearanceTab.Controls.Count / 5;
                int col = mAppearanceTab.Controls.Count % 5;
                c.Location = new Point(col * c.Width, row * c.Height);
                mAppearanceTab.Controls.Add(c);
            }
        }

        void PictureChooser_ValueChanged(object sender, EventArgs e)
        {
            string path = "";
            PictureChooser chooser = sender as PictureChooser;
            if (chooser == null) return;

            switch (chooser.Name)
            {
                case "Photo":
                    string dude = DataMap.PhotoMap[chooser.Value];
                    path = String.Format("PlayerData\\PlayerPhotos\\{0}.jpg", dude);
                    string noPhoto = "PlayerData\\PlayerPhotos\\0004.jpg";
                    if (System.IO.File.Exists(path))
                        chooser.PictureBox.ImageLocation = path;
                    else if (System.IO.File.Exists(noPhoto))
                        chooser.PictureBox.ImageLocation = noPhoto;
                    break;
                case "FaceMask":
                case "RightShoe":
                case "LeftShoe":
                    path = String.Format("PlayerData\\EquipmentImages\\{0}.jpg", chooser.Value);
                    if (System.IO.File.Exists(path))
                        chooser.PictureBox.ImageLocation = path;
                    else
                        chooser.PictureBox.ImageLocation = null;
                    break;
                    // elbow == incomplete
                case "RightElbow":
                case "LeftElbow":
                case "RightWrist":
                case "LeftWrist":
                case "NeckRoll":
                case "LeftGlove":
                case "RightGlove":
                    path = String.Format("PlayerData\\EquipmentImages\\{0}{1}.jpg",chooser.RepresentedValue.Name,  chooser.Value);
                    if (System.IO.File.Exists(path))
                        chooser.PictureBox.ImageLocation = path;
                    else
                        chooser.PictureBox.ImageLocation = null;
                    break;
            }
        }

        /// <summary>
        /// Get and set the current team.
        /// </summary>
        public string CurrentTeam
        {
            get { return this.m_TeamsComboBox.SelectedItem.ToString(); }

            set
            {
                int index = m_TeamsComboBox.Items.IndexOf(value);
                if (index > -1)
                {
                    m_TeamsComboBox.SelectedIndex = index;
                    string[] players = GetTeamPlayers(value);
                    if (players != null)
                        mPlayerIndexUpDown.Maximum = players.Length;
                    else
                       mPlayerIndexUpDown.Maximum = 0;
                }
            }
        }

        private int mSelectionStart = 0;
        /// <summary>
        /// The position in the text that the user clicked.
        /// (We use this to figure out the player we're editing)
        /// </summary>
        public int SelectionStart 
        {
            get { return mSelectionStart; }
            set 
            {
                if (mSelectionStart != value)
                {
                    int oldVal = mSelectionStart;
                    mSelectionStart = value;
                    OnSelectionStartChanged(oldVal, mSelectionStart);
                }
            }
        }

        private void OnSelectionStartChanged(int oldVal, int newValue)
        {
            string playerLine = InputParser.GetLine(newValue, Data);
            string team = InputParser.GetTeam(newValue, Data);
            int index = m_TeamsComboBox.Items.IndexOf(team);
            if (index > -1)
            {
                m_TeamsComboBox.SelectedIndex = index;
            }
            string[] players = GetTeamPlayers(team);
            if (players != null)
            {
                for (int i = 0; i < players.Length; i++)
                {
                    if (players[i].StartsWith(playerLine))
                    {
                        mPlayerIndexUpDown.Value = i;
                        break;
                    }
                }
                SetCurrentPlayer();
                mInitializing = false; // keep it here, initialization failed
                this.mPlayerIndexUpDown.Maximum = players.Length - 1;
            }
        }

        private void SetupTeams()
        {
            Regex teamRegex = new Regex("TEAM\\s*=\\s*([a-z0-9]+)", RegexOptions.IgnoreCase);
            MatchCollection mc = teamRegex.Matches(Data);

            m_TeamsComboBox.Items.Clear();
            m_TeamsComboBox.BeginUpdate();
            foreach (Match m in mc)
            {
                string team = m.Groups[1].Value;
                m_TeamsComboBox.Items.Add(team);
            }
            m_TeamsComboBox.EndUpdate();
            if (m_TeamsComboBox.Items.Count > 0)
            {
                m_TeamsComboBox.SelectedItem = m_TeamsComboBox.Items[0];
            }
        }

        /// <summary>
        /// Replaces the current player with the values specified.
        /// </summary>
        private void ReplacePlayer()
        {
            string oldPlayer = GetPlayerString(m_TeamsComboBox.SelectedItem.ToString(),
                (int)mPlayerIndexUpDown.Value);
            if (oldPlayer == null)
                return;

            string newPlayer = GetPlayerString_UI();
            string team = m_TeamsComboBox.Items[m_TeamsComboBox.SelectedIndex].ToString();
            ReplacePlayer(team, oldPlayer, newPlayer);
        }

        private void ReplacePlayer(string team, string oldPlayer, string newPlayer)
        {
            int nextTeamIndex = -1;
            int currentTeamIndex = -1;
            string nextTeam = null;

            Regex findTeamRegex = new Regex("TEAM\\s*=\\s*" + team, RegexOptions.IgnoreCase);

            Match m = findTeamRegex.Match(Data);
            if (!m.Success)
                return;

            currentTeamIndex = m.Groups[1].Index;

            int test = m_TeamsComboBox.Items.IndexOf(team);

            if (test != m_TeamsComboBox.Items.Count - 1)
            {
                nextTeam = string.Format("TEAM\\s*=\\s*{0}", m_TeamsComboBox.Items[test + 1]);
                Regex nextTeamRegex = new Regex(nextTeam);
                Match nt = nextTeamRegex.Match(Data);
                if (nt.Success)
                    nextTeamIndex = nt.Index;
            }
            if (nextTeamIndex < 0)
                nextTeamIndex = Data.Length;

            int playerIndex = Data.IndexOf(oldPlayer, currentTeamIndex);
            if (playerIndex > -1)
            {
                int endLine = Data.IndexOf('\n', playerIndex);
                string start = Data.Substring(0, playerIndex);
                string last = Data.Substring(endLine);

                StringBuilder tmp = new StringBuilder(Data.Length + 200);
                tmp.Append(start);
                tmp.Append(newPlayer);
                tmp.Append(last);

                mData = tmp.ToString();
            }
            else
            {
                StaticUtils.AddError( string.Concat(
                    "An error occured looking up player ", oldPlayer,
                    "\nPlease verify that this player's attributes are correct."));
            }
        }

        /// <summary>
        /// Updates the GUI with the current player.
        /// </summary>
        private void SetCurrentPlayer()
        {
            if (m_TeamsComboBox.SelectedItem != null)
            {
                string team = m_TeamsComboBox.SelectedItem.ToString();
                int index = (int)mPlayerIndexUpDown.Value;
                string playerData = GetPlayerString(team, index);
                if (playerData != null)
                    SetPlayerData(playerData);
            }
        }

        /// <summary>
        /// Updates the GUI with the player 'line' passed.
        /// </summary>
        public void SetPlayerData(string playerLine)
        {
            List<string> playerParts = InputParser.ParsePlayerLine(playerLine);
            
            for (int i = 0; i < playerParts.Count; i++)
                SetControlValue(mKeyParts[i], playerParts[i]);
        }

        private void SetControlValue(string controlName, string val)
        {
            if (!SetControlValue(mSkillsTab, controlName, val))
                if (!SetControlValue(mAppearanceTab, controlName, val))
                    SetControlValue(this, controlName, val);
        }

        private bool SetControlValue(Control parentControl, string controlName, string val)
        {
            foreach (Control c in parentControl.Controls)
            {
                if (c.Name == controlName)
                {
                    IntAttrControl iac = c as IntAttrControl;
                    StringSelectionControl ssc = c as StringSelectionControl;
                    DateValueControl dvc = c as DateValueControl;

                    if (iac != null)
                    {
                        iac.Value = Int32.Parse(val);
                        return true;
                    }
                    else if (dvc != null)
                    {
                        dvc.Value = val;
                        return true;
                    }
                    else if (ssc != null)
                    {
                        if (controlName == "PBP")
                        {
                            if (ReversePBPs.ContainsKey(val))
                            {
                                ssc.Value = ReversePBPs[val];
                                return true;
                            }
                        }
                        else if (controlName == "Photo")
                        {
                            if (ReversePhotos.ContainsKey(val))
                                ssc.Value = ReversePhotos[val];
                            else
                                ssc.Value = "NoPhoto";
                            return true;
                        }
                        else if (controlName == "College")
                        {
                            if (val[0] == '"')
                                val = val.Replace("\"", "");
                            ssc.Value = val;
                            return true;
                        }
                        else
                        {
                            ssc.Value = val;
                            return true;
                        }
                    }
                    else
                    {
                        c.Text = val;
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// returns null when control is not found, control value otherwise
        /// </summary>
        private string GetControlValue(string controlName)
        {
            string retVal = "";
            if ((retVal = GetControlValue(mSkillsTab, controlName)) == null )
                if((retVal = GetControlValue(mAppearanceTab, controlName)) == null)
                    retVal = GetControlValue(this, controlName);
            return retVal;
        }

        /// <summary>
        /// returns null when control is not found, control value otherwise
        /// </summary>
        private string GetControlValue(Control parentControl, string controlName)
        {
            foreach (Control c in parentControl.Controls)
            {
                if (c.Name == controlName)
                {
                    TextBox tb = c as TextBox;
                    IntAttrControl iac = c as IntAttrControl;
                    StringSelectionControl ssc = c as StringSelectionControl;
                    DateValueControl dvc = c as DateValueControl;
                    if (iac != null)
                        return iac.Value.ToString();
                    else if (dvc != null)
                        return dvc.Value;
                    else if (ssc != null)
                    {
                        if (controlName == "PBP" && PBPs.ContainsKey(ssc.Value))
                        {
                            return PBPs[ssc.Value];
                        }
                        else if (controlName == "Photo" && Photos.ContainsKey(ssc.Value))
                        {
                            return Photos[ssc.Value];
                        }
                        else if (controlName == "College" && ssc.Value.IndexOf(',') > -1)
                            return string.Concat("\"", ssc.Value, "\"");
                        return ssc.Value;
                    }
                    else if (tb != null)
                        return tb.Text;
                }
            }
            return null;
        }

        /// <summary>
        /// Gets a player 'line' from Data from 'team' playing 'position'.
        /// </summary>
        /// <param name="team">The team</param>
        /// <param name="playerIndex">the player index on the team</param>
        /// <returns>the player line </returns>
        private string GetPlayerString(string team, int playerIndex)
        {
            string[] players = GetTeamPlayers(team);
            if (players != null && playerIndex < players.Length)
                return players[playerIndex];

            return null;
        }

        private string[] GetTeamPlayers(string team)
        {
            Regex findTeamRegex = new Regex("TEAM\\s*=\\s*" + team, RegexOptions.IgnoreCase);
            Regex nextTeam = new Regex("TEAM\\s*=\\s*", RegexOptions.IgnoreCase);
            Regex player = new Regex("(K,.+)|(QB,.+)|(P,.+)|(WR,.+)|(CB,.+)|(FS,.+)|(SS,.+)|(RB,.+)|(FB,.+)|(TE,.+)|(OLB,.+)|(ILB,.+)|(C,.+)|(G,.+)|(T,.+)|(DT,.+)|(DE,.+)");
            Match m = findTeamRegex.Match(Data);
            Match m2;
            string chunk = "";
            if (m != Match.Empty)
            {
                m2 = nextTeam.Match(Data, m.Index + m.Length);
                int end = Data.Length;
                if (m2 != Match.Empty)
                    end = m2.Index;

                chunk = Data.Substring(m.Index, end - m.Index);
                MatchCollection mc = player.Matches(chunk);
                if (mc.Count > 0)
                {
                    string[] retVal = new string[mc.Count];
                    for (int i = 0; i < retVal.Length; i++)
                        retVal[i] = mc[i].Value;
                    return retVal;
                }
            }
            return null;
        }

        /// <summary>
        /// Returns the text representation of what the GUI is presenting.
        /// </summary>
        public string GetPlayerString_UI()
        {
            StringBuilder sb = new StringBuilder(300);
            string a = "";
            for (int i = 0; i < mKeyParts.Length; i++)
            {
                a = GetControlValue(mKeyParts[i]);
                if (!String.IsNullOrEmpty(a))
                {
                    sb.Append(a);
                    sb.Append(",");
                }
            }
            return sb.ToString();
        }

        private void mPreviousButton_Click(object sender, EventArgs e)
        {
            if (mPlayerIndexUpDown.Value > 0)
                mPlayerIndexUpDown.Value--;
            else if (m_TeamsComboBox.SelectedIndex > 0)
                m_TeamsComboBox.SelectedIndex--;

            SetCurrentPlayer();
        }

        private void mNextButton_Click(object sender, EventArgs e)
        {
            string[] players = GetTeamPlayers(m_TeamsComboBox.SelectedItem.ToString());
            if (mPlayerIndexUpDown.Value < players.Length-1)
                mPlayerIndexUpDown.Value++;
            else if (m_TeamsComboBox.SelectedIndex < m_TeamsComboBox.Items.Count-1)
                m_TeamsComboBox.SelectedIndex++;
            else
                m_TeamsComboBox.SelectedIndex = 0;
            
            SetCurrentPlayer();
        }

        private void m_TeamsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetMaxPlayerIndex();
        }

        private void SetMaxPlayerIndex()
        {
            if (!mInitializing)
            {
                string[] players = GetTeamPlayers(m_TeamsComboBox.SelectedItem.ToString());
                if (players != null)
                {
                    mPlayerIndexUpDown.Value = 0;
                    mPlayerIndexUpDown.Maximum = players.Length - 1;
                    SetCurrentPlayer();
                }
            }
        }

        private void mPlayerIndexUpDown_ValueChanged(object sender, EventArgs e)
        {
            mInitializing = true;
            SetCurrentPlayer();
            mInitializing = false;
        }

        private void mOkButton_Click(object sender, EventArgs e)
        {
            ReplacePlayer();
        }

        private void ValueChanged(object sender, System.EventArgs e)
        {
            if (!mInitializing)
            {
                ReplacePlayer();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string newPlayer = GetPlayerString_UI();
            MessageBox.Show(newPlayer);
        }

    }
}
