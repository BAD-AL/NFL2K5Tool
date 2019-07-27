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
    /// <summary>
    /// This form is used to mass edit players by position and other attributes.
    /// </summary>
    public partial class GlobalEditForm : Form
    {
        // must be set in order to use 
        private GamesaveTool Tool { get; set; }

        private DataTable Table = new DataTable();

        public GlobalEditForm(GamesaveTool tool)
        {
            Tool = tool;
            InitializeComponent();
            PopulateAttributeComboBox();
            intAttrControl1.Text = stringSelectionControl1.Text = "To";
        }

        private void PopulateAttributeComboBox()
        {
            string[] keys = Tool.GetDefaultKey(true, true).Split(new char[] {','});

            foreach (string attr in keys)
            {
                if ("#Position,fname,lname,JerseyNumber,Photo,College,DOB,PBP,Photo,Height,".IndexOf(attr) == -1)
                    mAttributeComboBox.Items.Add(attr);
            }
            mAttributeComboBox.SelectedIndex = 0;
        }

        private void mAttributeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string newValue = mAttributeComboBox.Items[mAttributeComboBox.SelectedIndex].ToString().Replace("Right", "").Replace("Left", "");

            if (newValue == "BodyType")
                newValue = "Body";
            else if (",Dreads,EyeBlack,MouthPiece,".IndexOf(newValue) > -1)
                newValue = "YesNo";

            Type t = Type.GetType( "NFL2K5Tool."+ newValue, false);

            if (t != null)
            {
                this.stringSelectionControl1.RepresentedValue = t;
                this.stringSelectionControl1.Visible = true;
                this.stringSelectionControl1.SetToInitialValue();
                this.intAttrControl1.Visible = false;
            }
            else
            {
                this.stringSelectionControl1.Visible = false;
                this.intAttrControl1.Visible = true;
                this.intAttrControl1.Value = 0;
            }
        }

        private List<string> GetPositions()
        {
            List<string> retVal = new List<string>();
            CheckBox cb = null;
            foreach (Control c in mPositionsGroupBox.Controls)
            {
                cb = c as CheckBox;
                if (cb != null && cb.Checked)
                {
                    retVal.Add(cb.Text);
                }
            }
            return retVal;
        }

        private string GetFormula()
        {
            string formula = "true";
            string f = mFormulaTextBox.Text.Trim();
            if (f != "" && !f.Equals("Always", StringComparison.InvariantCultureIgnoreCase))
                formula = f;
            return formula;
        }

        private void mShowAffectedPlayersButton_Click(object sender, EventArgs e)
        {
            ProcessFormula(false);
        }

        private void mSetAttributeButton_Click(object sender, EventArgs e)
        {
            ProcessFormula(true);
        }


        private bool mIncreaseYearsPro = false;

        private void mAddOneYear_Click(object sender, EventArgs e)
        {
            mIncreaseYearsPro = true;
            ProcessFormula(true);
            mIncreaseYearsPro = false;
        }

        /// <summary>
        /// get tle players that the formulaand positions checkboxes apply to.
        /// Sets the players attributes when 'applyChanges' = true.
        /// </summary>
        private void ProcessFormula(bool applyChanges)
        {
            try
            {
                List<int> playerIndexes = GetPlayerIntexesFor(GetFormula(), GetPositions());
                if (playerIndexes.Count > 0)
                {
                    String tmp = "";
                    int temp_i = 0;
                    StringBuilder sb = new StringBuilder(30 * playerIndexes.Count);
                    int p = 0;
                    string attr = mAttributeComboBox.Items[mAttributeComboBox.SelectedIndex].ToString();
                    if (mIncreaseYearsPro)
                        attr = "YearsPro";
                    string val = intAttrControl1.Visible ? intAttrControl1.Value + "" : stringSelectionControl1.Value;
                    sb.Append("Team,FirstName,LastName,");
                    sb.Append(attr);
                    sb.Append("\n");
                    for (int i = 0; i < playerIndexes.Count; i++)
                    {
                        p = playerIndexes[i];
                        if (applyChanges)
                        {
                            if (mIncreaseYearsPro)
                            {
                                tmp = Tool.GetPlayerField(p,attr);
                                temp_i = Int32.Parse(tmp);
                                temp_i++;
                                val = temp_i.ToString();
                            }
                            else if (intAttrControl1.Visible && mPercentCheckbox.Checked)
                            {
                                tmp = Tool.GetPlayerField(p, attr);
                                temp_i = Int32.Parse(tmp);
                                val = ((int)(temp_i * (intAttrControl1.Value * 0.01))).ToString();
                            }
                            this.Tool.SetPlayerField(p, attr, val);
                        }

                        sb.Append(Tool.GetPlayerTeam(p));
                        sb.Append(",");
                        sb.Append(Tool.GetPlayerFirstName(p));
                        sb.Append(",");
                        sb.Append(Tool.GetPlayerLastName(p));
                        sb.Append(",");
                        sb.Append(Tool.GetPlayerField(p, attr));
                        sb.Append("\n");
                    }
                    MessageForm.ShowMessage("Players affected = "+playerIndexes.Count, sb.ToString(), SystemIcons.Question, false, true);
                }
                else
                {
                    MessageBox.Show(this,
                    "Check formula and Positions Check boxes ",
                    "No Players"
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    "Ensure the formula is correct: " + GetFormula() + "\n" + ex.Message,
                    "Error, Check formula"
                    );
            }
        }

        //private string GetPlayerText(List<int> playerIndexes)
        //{
        //    int p = 0;
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("Number of players = ");
        //    sb.Append(playerIndexes.Count);
        //    sb.Append("\n");
        //    for (int i = 0; i < playerIndexes.Count; i++)
        //    {
        //        p = playerIndexes[i];
        //        sb.Append(Tool.GetPlayerTeam(p));
        //        sb.Append(",");
        //        sb.Append(Tool.GetPlayerFirstName(p));
        //        sb.Append(",");
        //        sb.Append(Tool.GetPlayerLastName(p));
        //        sb.Append("\n");
        //    }
        //    return sb.ToString();
        //}

        private List<int> GetPlayerIntexesFor(string formula, List<string> positions)
        {
            List<int> playerIndexes = Tool.GetPlayersByFormula(formula, positions);
            return playerIndexes;
        }

        private void mCheckAllButton_Click(object sender, EventArgs e)
        {
            bool check = !mQBcheckBox.Checked;
            CheckBox cb = null;
            foreach (Control c in mPositionsGroupBox.Controls)
            {
                cb = c as CheckBox;
                if( cb != null)
                    cb.Checked = check;
            }
        }

        private void mFormulaButton_Click(object sender, EventArgs e)
        {
            string howTo = 
@"When selecting players to modify you can use the following player attributes:
    Number,Speed,Agility,Strength,Jumping,Coverage,PassRush,RunCoverage,
    PassBlocking,RunBlocking,Catch,RunRoute,BreakTackle,HoldOnToBall,
    PowerRunStyle,PassAccuracy,PassArmStrength,PassReadCoverage,Tackle,
    KickPower,KickAccuracy,Stamana,Durability,Leadership,Scramble,Composure,
    Consistency,Aggressiveness,
    Hand,BodyType,Skin,Face,Dreads,Helmet,FaceMask,Visor,
    EyeBlack,MouthPiece,LeftGlove,RightGlove,LeftWrist,RightWrist,LeftElbow,
    RightElbow,Sleeves,LeftShoe,RightShoe,NeckRoll,Turtleneck

Expressions like the following will work:
    Hand = Left and (Speed > 80 or Jumping < 50)

Math Operators:
/ * - +
Logic Operators:
and
or
=
<> (Not Equal)

";
            MessageForm.ShowMessage("Usage", howTo, SystemIcons.Question, false, true);
        }

    }
}
