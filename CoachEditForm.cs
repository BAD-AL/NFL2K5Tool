using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace NFL2K5Tool
{
    public partial class CoachEditForm : Form
    {
        /// <summary>
        /// PBP names
        /// </summary>
        public Dictionary<string, string> ReversePBPs { get; set; }

        /// <summary>
        /// Photo names
        /// </summary>
        public Dictionary<string, string> ReversePhotos { get; set; }

        /// <summary>
        /// PBP names
        /// </summary>
        public Dictionary<string, string> PBPs { get; set; }

        /// <summary>
        /// Photo names
        /// </summary>
        public Dictionary<string, string> Photos { get; set; }

        private string mKeyString = "";

        private string[] mKeyParts = null;

        /// <summary>
        /// Form for editing Coaches.
        /// </summary>
        public CoachEditForm()
        {
            InitializeComponent();
            this.mBodySelectionControl.SetItems(this.mCoachBodyOptions);
            this.mBodySelectionControl.Text = "Body";
        }

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
                SetupGui();
            }
        }


        /// <summary>
        /// Initializes the Form.
        /// Call this after Colleges, Photos & PBPs have been set.
        /// </summary>
        private void SetupGui()
        {
            if (SetupKey())
            {
                //ClearControls(mStatsTab);
                //ClearControls(mStrategyTab);

                foreach (string attr in mKeyParts)
                {
                    if (attr.Length > 0 && sCoachStats.IndexOf(attr + ",") > -1)
                    {
                        AddStat(attr);
                    }
                }

                foreach (string attr in mKeyParts)
                {
                    if (attr.Length > 0 && sCoachStrategyAttrs.IndexOf(attr + ",") > -1)
                    {
                        AddStrategy(attr);
                    }
                }
            }
        }

        private void AddStat(string stat)
        {
            Control c = null;
            IntAttrControl iac = new IntAttrControl();
            iac.Name = iac.Text = stat;
            iac.ValueChanged += new EventHandler(ValueChanged);
            c = iac;
            int row = mStrategyTab.Controls.Count / 5;
            int col = mStrategyTab.Controls.Count % 5;
            c.Location = new Point(col * c.Width, row * c.Height);
            mStrategyTab.Controls.Add(c);
        }

        private void AddStrategy(string strat)
        {
            Control c = null;
            if (strat == "PowerRunStyle")
            {
                StringSelectionControl ssc = new StringSelectionControl();
                ssc.RepresentedValue = typeof(PowerRunStyle);
                ssc.Name = ssc.Text = strat;
                ssc.ValueChanged += new EventHandler(ValueChanged);
                c = ssc;
            }
            else
            {
                IntAttrControl iac = new IntAttrControl();
                iac.Name = iac.Text = strat;
                iac.ValueChanged += new EventHandler(ValueChanged);
                c = iac;
            }
            int row = mStrategyTab.Controls.Count / 5;
            int col = mStrategyTab.Controls.Count % 5;
            c.Location = new Point(col * c.Width, row * c.Height);
            mStrategyTab.Controls.Add(c);
        }

        private void ValueChanged(object sender, System.EventArgs e)
        {
            // need to have cached current coach, replace him with 'GetCoachString_UI()'
        }

        /// <summary>
        /// Returns the text representation of what the GUI is presenting.
        /// </summary>
        public string GetCoachString_UI()
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

        /// <summary>
        /// Returns true when a new key is set, false if it's the same key
        /// </summary>
        private bool SetupKey()
        {
            bool retVal = false;
            Regex keyReg = new Regex("^CoachKey=(.*)", RegexOptions.IgnoreCase);
            Match m = keyReg.Match(Data);
            if (m != Match.Empty)
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
                throw new InvalidOperationException("Please make sure the Coach key is present in the text.");
            }
            return retVal;
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
            //string playerLine = InputParser.GetLine(newValue, Data);
            //string team = InputParser.GetTeam(newValue, Data);
            //int index = m_TeamsComboBox.Items.IndexOf(team);
            //if (index > -1)
            //{
            //    m_TeamsComboBox.SelectedIndex = index;
            //}
            //string[] players = GetTeamPlayers(team);
            //if (players != null)
            //{
            //    for (int i = 0; i < players.Length; i++)
            //    {
            //        if (players[i].StartsWith(playerLine))
            //        {
            //            mPlayerIndexUpDown.Value = i;
            //            break;
            //        }
            //    }
            //    SetCurrentPlayer();
            //    mInitializing = false; // keep it here, initialization failed
            //    this.mPlayerIndexUpDown.Maximum = players.Length - 1;
            //}
        }

        private const string sCoachStats         = ",SeasonsWithTeam,totalSeasons,Wins,Losses,Ties,WinningSeasons,SuperBowls,PlayoffLosses,SuperBowlWins,SuperBowlLosses,PlayoffWins,PlayoffWins,";
        private const string sCoachStrategyAttrs = ",Overall,OvrallOffense,RushFor,PassFor,OverallDefense,PassRush,PassCoverage,QB,RB,TE,WR,OL,DL,LB,SpecialTeams,Professionalism,Preparation,Conditioning,Motivation,Leadership,Discipline,Respect,PlaycallingRun,ShotgunRun,ShotgunRun,SplitbackRun,SplitbackRun,ShotgunPass,SplitbackPass,IFormPass,LoneBackPass,EmptyPass,";
        private const string sStringAttrs        = ",Team,FirstName,LastName,Info1,Info2,Body,";
        private string sIntAttrs = sCoachStrategyAttrs + sCoachStats;

        public Control GetControl(string attribute)
        {
            Control ctrl = null;
            string find = String.Concat(",", attribute, ",");

            switch (attribute)
            {
                case "Body":      ctrl = this.mBodySelectionControl; break;
                case "FirstName": ctrl = fname; break;
                case "LastName":  ctrl = lname; break;

                default:
                    if (sIntAttrs.Contains(find))
                    {
                        IntAttrControl iac = new IntAttrControl();
                        iac.Name = iac.Text = attribute;
                        ctrl = iac;
                    }
                    break;
            }
            return ctrl;
        }

        /// <summary>
        /// returns null when control is not found, control value otherwise
        /// </summary>
        private string GetControlValue( string controlName)
        {
            Control c = FindControl(this, controlName);
            if (c != null)
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
            return null;
        }


        private static Control FindControl(Control parentControl, string controlName)
        {
            Control ctrl = null;
            foreach (Control c in parentControl.Controls)
            {
                if (c.Name == controlName)
                    return c;
                if (c.Controls.Count > 0)
                {
                    ctrl = FindControl(c, controlName);
                    if (ctrl != null)
                        return ctrl;
                }
            }
            return null;
        }


        string[] mCoachBodyOptions = new string[] { 
            "[Dennis Erickson]",      "[Lovie Smith]",    "[Marvin Lewis]",   "[Mike Mularkey]",
            "[Mike Shanahan]",        "[Butch Davis]",    "[Jon Gruden]",     "[Dennis Green]",
            "[Marty Schottenheimer]", "[Dick Vermeil]",   "[Tony Dungy]",     "[Dallas Coach]",
            "[Dave Wannstedt]",       "[Andy Reid]",      "[Jim Mora Jr.]",   "[Tom Coughlin]",
            "[Jack Del Rio]",         "[Herman Edwards]", "[Steve Mariucci]", "[Mike Sherman]",
            "[John Fox]",             "[Bill Belichick]", "[Norv Turner]",    "[Mike Martz]",  
            "[Brian Billick]",        "[Joe Gibbs]",      "[Jim Haslett]",    "[Mike Holmgren]",  
            "[Bill Cowher]",          "[Dom Capers]",     "[Jeff Fisher]",    "[Mike Tice]"
        };

        private void mBodySelectionControl_ValueChanged(object sender, EventArgs e)
        {
            string file = String.Concat(System.IO.Directory.GetCurrentDirectory(), "\\PlayerData\\CoachBodies\\", mBodySelectionControl.Value);
            Image img = StaticUtils.GetImageFromPath(file);
            mBodyPictureBox.Image = img;
        }
    }
}
