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

        /// <summary>
        /// Constructor.
        /// </summary>
        public PlayerEditForm()
        {
            InitializeComponent();

            // issues setting these in the designer; too lazy to set the attributes on the controls to get it to work.
            Pos.Text = "Position";
            Pos.RepresentedValue = typeof(Positions);
            Pos.BorderStyle = BorderStyle.None;
        }

        /// <summary>
        /// the colleges.
        /// </summary>
        public string[] Colleges { get; set; }

        /// <summary>
        /// PBP names
        /// </summary>
        public Dictionary<string, string> PBPs { get; set; }

        /// <summary>
        /// Photo names
        /// </summary>
        public Dictionary<string,string> Photos { get; set; }

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
        private const string mAppearanceString = "College,DOB,PBP,Photo,YearsPro,Hand,Weight,Height,BodyType,Skin,Face,Dreads,Helmet,FaceMask,Visor,EyeBlack,MouthPiece,LeftGlove,RightGlove,LeftWrist,RightWrist,LeftElbow,RightElbow,Sleeves,LeftShoe,RightShoe,NeckRoll,Turtleneck,";
        
        /// <summary>
        /// Returns true when a new key is set, false if it's the same key
        /// </summary>
        private bool SetupKey()
        {
            bool retVal = false;
            Regex keyReg = new Regex("^#(Pos,fname,lname,.*)", RegexOptions.IgnoreCase);
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
                MessageBox.Show("Please make sure the key '#Pos,fname,lname...' is present at the top of the text box.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            for (int i = parentControl.Controls.Count - 1; i > -1; i--)
            {
                c = parentControl.Controls[i];
                parentControl.Controls.Remove(c);
                c.Dispose();
            }
        }

        private void AddSkill(string skill)
        {
            if (skill != "PowerRunStyle")
            {
                IntAttrControl ctrl = new IntAttrControl();
                ctrl.Name = ctrl.Text = skill;
                int row = mSkillsTab.Controls.Count / 5;
                int col = mSkillsTab.Controls.Count % 5;
                ctrl.Location = new Point(col * ctrl.Width, row * ctrl.Height);
                mSkillsTab.Controls.Add(ctrl);
            }
        }

        private void AddAppearance(string appearance)
        {
            Control c = null;
            IntAttrControl intAttrCtrl;
            if( "Weight,DOB,YearsPro,".IndexOf(appearance+",") > -1)
            {
                switch( appearance)
                {
                    case "YearsPro":
                        intAttrCtrl = new IntAttrControl();
                        intAttrCtrl.Name = intAttrCtrl.Text = appearance;
                        c = intAttrCtrl;
                        break;
                    case "Weight":
                        intAttrCtrl = new IntAttrControl();
                        intAttrCtrl.Name = intAttrCtrl.Text = appearance;
                        intAttrCtrl.Min = 150;
                        intAttrCtrl.Max = intAttrCtrl.Min + 255;
                        c = intAttrCtrl;
                        break;
                    case "DOB":
                        break;
                }
            }
            else {
                StringSelectionControl ctrl = new StringSelectionControl();
                c = ctrl;
                ctrl.Name = ctrl.Text = appearance;
                c = ctrl;
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
                    case "FaceMask": ctrl.RepresentedValue = typeof(FaceMask); break;
                    case "Visor": ctrl.RepresentedValue = typeof(Visor); break;
                    case "LeftGlove": 
                    case "RightGlove": ctrl.RepresentedValue = typeof(Glove); break;
                    case "LeftWrist": 
                    case "RightWrist": ctrl.RepresentedValue = typeof(Wrist); break;
                    case "LeftElbow":
                    case "RightElbow": ctrl.RepresentedValue = typeof(Elbow); break;
                    case "Sleeves": ctrl.RepresentedValue = typeof(Sleeves); break;
                    case "LeftShoe":
                    case "RightShoe": ctrl.RepresentedValue = typeof(Shoe); break;
                    case "NeckRoll": ctrl.RepresentedValue = typeof(NeckRoll); break;
                    case "Turtleneck": ctrl.RepresentedValue = typeof(Turtleneck); break;
                    case "Height": //5'6"-7'0"
                        ctrl.SetItems(new string[] { 
                            "5'6\"", "5'7\"", "5'8\"", "5'9\"", "5'10\"", "5'11\"", 
                            "6'0\"", "6'1\"", "6'2\"", "6'3\"", "6'4\"",  "6'5\"", 
                            "6'6\"", "6'7\"", "6'8\"", "6'9\"", "6'10\"", "6'11\"", 
                            "7'0\"" });
                        break;
                    // need to special case the setting of these items.
                    case "College":
                        ctrl.DropDownStyle = ComboBoxStyle.DropDown;
                        ctrl.SetItems(Colleges); 
                        break;
                    case "PBP":
                        string[] values = new string[this.PBPs.Count];
                        this.PBPs.Values.CopyTo(values, 0);
                        ctrl.SetItems(values);
                        ctrl.DropDownStyle = ComboBoxStyle.DropDown;
                        break;
                    case "Photo":
                        ctrl.DropDownStyle = ComboBoxStyle.DropDown;
                        string[] vals = new string[this.Photos.Count];
                        this.Photos.Values.CopyTo(vals, 0);
                        ctrl.SetItems(vals);
                        break;
                }
            }
            if (c != null)
            {
                int row = mAppearanceTab.Controls.Count / 5;
                int col = mAppearanceTab.Controls.Count % 5;
                c.Location = new Point(col * c.Width, row * c.Height);
                mAppearanceTab.Controls.Add(c);
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
                    m_TeamsComboBox.SelectedIndex = index;
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
            SetPlayerData(playerLine);
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

            Regex findTeamRegex = new Regex("TEAM\\s*=\\s*" + team);

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

                Data = tmp.ToString();
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
            {
                SetControlValue(mKeyParts[i], playerParts[i]);
            }
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
                    if (iac != null)
                    {
                        iac.Value = Int32.Parse(val);
                        return true;
                    }
                    else if (ssc != null)
                    {
                        if (controlName == "PBP")
                        {
                            if (PBPs.ContainsKey(val))
                            {
                                ssc.Value = PBPs[val];
                                return true;
                            }
                        }
                        else if (controlName == "Photo")
                        {
                            if (Photos.ContainsKey(val))
                            {
                                ssc.Value = Photos[val];
                                return true;
                            }
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
                retVal = GetControlValue(mAppearanceTab, controlName);
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
                    IntAttrControl iac = c as IntAttrControl;
                    StringSelectionControl ssc = c as StringSelectionControl;
                    if (iac != null)
                        return iac.Value.ToString();
                    else if (ssc != null)
                    {
                        if (controlName == "PBP" )
                            return PBPs[ssc.Value];
                        else if( controlName == "Photo")
                            return Photos[ssc.Value];
                        else if (controlName == "College" && ssc.Value.IndexOf(',') > -1)
                            return string.Concat("\"", ssc.Value, "\"");
                        return ssc.Value;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Gets a player 'line' from Data from 'team' playing 'position'.
        /// </summary>
        /// <param name="team"></param>
        /// <param name="position"></param>
        /// <returns>the line </returns>
        private string GetPlayerString(string team, int playerIndex)
        {
            string pattern = "TEAM\\s*=\\s*" + team;
            Regex findTeamRegex = new Regex(pattern, RegexOptions.IgnoreCase);
            Match m = findTeamRegex.Match(Data);
            if (m != Match.Empty)
            {
                int teamIndex = m.Index;
                if (teamIndex == -1)
                    return null;
                int lineEnd = Data.IndexOf("\n", playerIndex + 1);
                string playerLine = Data.Substring(playerIndex, lineEnd - playerIndex);
                return playerLine;
            }
            return null;
        }

        /// <summary>
        /// Returns the text representation of what the GUI is presenting.
        /// </summary>
        /// <returns></returns>
        public string GetPlayerString_UI()
        {
            StringBuilder sb = new StringBuilder(60);
            sb.Append("position");
            sb.Append(", ");
            sb.Append(fname.Text);
            sb.Append(",");
            sb.Append(lname.Text);
            sb.Append(",");
            //TODO: more stuff filled out here
            return sb.ToString();
        }
    }
}
