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
            return mFormulaTextBox.Text.Trim();
        }

        private FormulaMode GetFormulaMode()
        {
            FormulaMode retVal = FormulaMode.Normal;
            if (intAttrControl1.Visible && mPercentCheckbox.Checked)
                retVal = FormulaMode.Percent;
            //else if (mIncreaseYearsPro)
            //    retVal = FormulaMode.Increment;
            return retVal;
        }

        private void mShowAffectedPlayersButton_Click(object sender, EventArgs e)
        {
            string result =
                Tool.ApplyFormula(GetFormula(), GetTargetAttribute(), GetTargetValue(), GetPositions(), GetFormulaMode(), false);
            ShowResults(result);
        }

        private void mSetAttributeButton_Click(object sender, EventArgs e)
        {
            string result =
                Tool.ApplyFormula(GetFormula(), GetTargetAttribute(), GetTargetValue(), GetPositions(), GetFormulaMode(), true);
            ShowResults(result);
        }

        private void mAddOneYear_Click(object sender, EventArgs e)
        {
            string result =
                Tool.ApplyFormula(GetFormula(), "YearsPro", "1", GetPositions(), FormulaMode.Add, true);
            ShowResults(result);
        }

        private void ShowResults(string results)
        {
            if (results == null)
            {
                MessageBox.Show(this,
                    "Check formula and Positions Check boxes ",
                    "No Players"
                    );
            }
            else if (results.StartsWith("Exception!"))
            {
                MessageBox.Show(this,
                    results,
                    "Error, Check formula"
                    );
            }
            else
            {
                MessageForm.ShowMessage("Affected  Players", results, SystemIcons.Question, false, false);
            }
        }

        private string GetTargetAttribute()
        {
            string attr = mAttributeComboBox.Items[mAttributeComboBox.SelectedIndex].ToString();// GetTargetAttribute(); //include line below
            return attr;
        }

        private string GetTargetValue()
        {
            string val = intAttrControl1.Visible ? intAttrControl1.Value + "" : stringSelectionControl1.Value; // GetTargetValue();
            return val;
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

Check the Console output to see the formula the editor uses.
You can use these formulas in the Main Text area as well.
";
            MessageForm.ShowMessage("Usage", howTo, SystemIcons.Question, false, false);
        }

    }
}
