using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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

        private const string sIntAttrs = ",SeasonsWithTeam,totalSeasons,Wins,Losses,Ties,WinningSeasons,SuperBowls,PlayoffLosses,SuperBowlWins,SuperBowlLosses,PlayoffWins,PlayoffWins,Overall,OvrallOffense,RushFor,PassFor,OverallDefense,PassRush,PassCoverage,QB,RB,TE,WR,OL,DL,LB,SpecialTeams,Professionalism,Preparation,Conditioning,Motivation,Leadership,Discipline,Respect,PlaycallingRun,ShotgunRun,ShotgunRun,SplitbackRun,SplitbackRun,ShotgunPass,SplitbackPass,IFormPass,LoneBackPass,EmptyPass,";
        private const string sStringAttrs = ",Team,FirstName,LastName,Info1,Info2,Body,";

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
        private string GetControlValue(Control parentControl, string controlName)
        {
            Control c = FindControl(parentControl, controlName);
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


        private Control FindControl(Control parentControl, string controlName)
        {
            foreach (Control c in parentControl.Controls)
                if (c.Name == controlName)
                    return c;
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
    }
}
