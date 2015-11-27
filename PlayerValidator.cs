using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace NFL2K5Tool
{
    /// <summary>
    /// Validates player attributes, sort a team
    /// TODO: Split this up into 2 classes with a shared base
    /// </summary>
    public class PlayerValidator:  IComparer<string>
    {
        private string mKey;
        private bool mSkills = false;
        private bool mAppearance = false;
        private string[] mKeyParts;

        private DataTable Table = new DataTable();


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="key">The attribute key.</param>
        public PlayerValidator(string key )
        {
            mKey = key.Replace("#", "");
            mAppearance = key.IndexOf("FaceMask") > -1;
            mSkills = key.IndexOf("PowerRunStyle") > -1;
            mKeyParts = mKey.Split(new char[] { ',' });
        }

        private List<string> mWarnings = new List<string>();

        private void AddWarning(string w)
        {
            mWarnings.Add(w);
        }

        /// <summary>
        /// Returns the player validation warnings 
        /// </summary>
        public List<string> GetWarnings()
        {
            return mWarnings;
        }

        /// <summary>
        /// Get the player's value of 'attribute'
        /// </summary>
        private string Get(List<string> playerParts, string attribute)
        {
            string retVal = "";
            int index = -1;
            for (int i = 0; i < mKeyParts.Length; i++)
            {
                if (mKeyParts[i].Equals( attribute, StringComparison.InvariantCultureIgnoreCase))
                {
                    index = i;
                    break;
                }
            }
            if (index > -1)
            {
                retVal = playerParts[index];
            }
            return retVal;
        }

        /// <summary>
        /// Processes the text looking for player attributes that seem incorrect.
        /// Adds warnings when something seems like it may be incorrect.
        /// </summary>
        /// <param name="text">The league/ team data</param>
        public void ValidatePlayers(string text)
        {
            text = text.Replace("\r\n", "\n");
            char[] chars = "\n".ToCharArray();
            string[] lines = text.Split(chars);
            string line;
            for (int i = 0; i < lines.Length; i++)
            {
                line = lines[i];
                if (line.StartsWith("#"))
                {
                }
                else if (line.IndexOf(",") > -1 && line.Length > 60)
                {
                    ValidatePlayer(line);
                }
            }
        }

        /// <summary>
        /// Will add warnings if there is an issue with the player. 
        /// </summary>
        /// <param name="line">The player line</param>
        public void ValidatePlayer(string line)
        {
            List<string> playerParts = InputParser.ParsePlayerLine(line);
            ValidateBodyType(playerParts);
            ValidateWeight(playerParts);
        }

        private void ValidateWeight(List<string> playerParts)
        {
            string pos = Get(playerParts, "Pos");
            string[] possibilities = null;
            switch (pos)
            {
                case "SS":
                case "FS":
                case "QB":
                case "RB":
                case "WR":
                    possibilities = GetRange(170, 260);
                    break;
                case "CB":
                case "P":
                case "K":
                    possibilities = GetRange(150, 240);
                    break;
                case "ILB":
                case "OLB":
                case "FB":
                    possibilities = GetRange(210, 280);
                    break;
                case "TE":
                    possibilities = GetRange(220, 280);
                    break;
                case "G":
                case "T":
                    possibilities = GetRange(260, 390);
                    break;
                case "C":
                    possibilities = GetRange(240, 380);
                    break;
                case "DE":
                    possibilities = GetRange(220, 320);
                    break;
                case "DT":
                    possibilities = GetRange(260, 390);
                    break;
            }
            ValidateAttribute("Weight", possibilities, playerParts);
        }

        // Skinny = 0, Normal, Large, ExtraLarge
        private void ValidateBodyType(List<string> playerParts)
        {
            string pos = Get(playerParts, "Pos");
            string[] possibilities=null;
            switch (pos)
            {
                case "CB":
                case "SS":
                case "FS":
                case "QB":
                case "RB":
                case "P":
                case "K":
                case "WR":
                    possibilities = new string[] {"Skinny", "Normal", /*"Large", "ExtraLarge"*/};
                    break;
                case "ILB":
                case "OLB":
                case "FB":
                case "TE":
                    possibilities = new string[] { /*"Skinny",*/ "Normal", "Large", /*"ExtraLarge"*/ };
                    break;
                case "G":
                case "T":
                case "C":
                    possibilities = new string[] { /*"Skinny",*/ "Normal", "Large", "ExtraLarge" };
                    break;
                case "DE":
                    possibilities = new string[] { /*"Skinny",*/ "Normal", "Large", "ExtraLarge" };
                    break;
                case "DT":
                    possibilities = new string[] { /*"Skinny",*/ "Normal", "Large", "ExtraLarge" };
                    break;
            }
            ValidateAttribute("BodyType", possibilities, playerParts);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attribute">the attribute to validate (BodyType, Weight...)</param>
        /// <param name="validValues"></param>
        /// <param name="playerParts"></param>
        private void ValidateAttribute(string attribute, string[] validValues, List<string> playerParts)
        {
            string val = Get(playerParts, attribute);
            int index = -1;
            for (int i = 0; i < validValues.Length; i++)
            {
                if( val.Equals(validValues[i]))
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                AddWarning(String.Format("Player {0} {1} {2},\t attribute {3}={4} ",
                    Get(playerParts,"Pos"), Get(playerParts,"fname"), Get(playerParts,"lname"), attribute, val
                    ));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns>an array of strings like [4,5,6,7,8...]</returns>
        private string[] GetRange(int start, int end)
        {
            int length = end - start + 1;
            string[] retVal = new string[length];
            int j =0;
            
            for (int i = start; i <= end; i++)
                retVal[j++] = i.ToString();

            return retVal;
        }

        /// <summary>
        /// returns a range of equiptment attributes
        /// </summary>
        /// <param name="start">usually 0 or 1</param>
        /// <param name="end"></param>
        /// <param name="eqpt">a string like "FaceMask"</param>
        /// <returns>an array like [FaceMask1, FaceMask2 ...]</returns>
        private string[] GetEqptRange(int start, int end, string eqpt)
        {
            int length = end - start;
            string[] retVal = new string[length];
            int j = 0;

            for (int i = start; i <= end; i++)
                retVal[j++] = eqpt + i.ToString();

            return retVal;
        }

        private string[] SortOrder = new string[] {
            "Team",
            "CB,", "DE,", "DT,", "FB,", "FS,", "G,", "RB,", "ILB,", "K,", "OLB,", "P,", "QB,", "SS,", "T,", "TE,", "WR,", "C,"
        };
        private Dictionary<string, string> mFormulas;

        /// <summary>
        /// Sorts the team passed in as a string
        /// </summary>
        /// <param name="team">the team to sort</param>
        /// <returns>The team sorted by position, player ranking</returns>
        public string SortTeam(string team)
        {
            string[] lines = team.Replace("\r\n", "\n").Split("\n".ToCharArray());
            Array.Sort(lines, this);
            StringBuilder builder = new StringBuilder();
            foreach (string line in lines)
            {
                builder.Append(line);
                builder.Append("\n");
            }
            return builder.ToString();
        }

        #region IComparer<string> Members

        public int Compare(string x, string y)
        {
            int xPos = -1;
            int yPos = -1;
            for (int i = 0; i < SortOrder.Length; i++)
            {
                if (x.StartsWith(SortOrder[i]))
                    xPos = i;
                if (y.StartsWith(SortOrder[i]))
                    yPos = i;
                if (xPos > -1 && yPos > -1)
                    break;
            }
            int retVal = xPos.CompareTo(yPos);
            if (retVal == 0 && x != y)
            {
                retVal = PositionCompare(x, y);
            }
            return retVal;
        }

        private int PositionCompare(string x, string y)
        {
            if (mFormulas == null)
                InitFormulas();
            string formula = GetFormula(x);
            if (formula != "")
            {
                List<string> playerPartsX = InputParser.ParsePlayerLine(x);
                try
                {
                    List<string> playerPartsY = InputParser.ParsePlayerLine(y);
                    string xExpr = GetExpression(formula, playerPartsX);
                    string yExpr = GetExpression(formula, playerPartsY);
                    double xResult = (double)this.Table.Compute(xExpr, "");
                    double yResult = (double)this.Table.Compute(yExpr, "");
                    return yResult.CompareTo(xResult);
                }
                catch (Exception )
                {
                    throw new Exception("Error comparing position: "+ playerPartsX[0] +"\nCheck formula for position.");
                }
            }
            return 0;
        }

        private string GetExpression(string formula, List<string> playerParts)
        {
            string work = formula;
            for (int i = 0; i < mSkillsArray.Length; i++)
            {
                if (work.IndexOf(mSkillsArray[i]) > -1)
                {
                    work = work.Replace(mSkillsArray[i], Get(playerParts, mSkillsArray[i]));
                }
            }
            return work;
        }

        string[] mSkillsArray = new string[]{
            "Agility","Jumping","PassRush","RunCoverage","PassBlocking",
            "RunBlocking","Catch","RunRoute","BreakTackle","HoldOntoBall","PassAccuracy",
            "PassArmStrength","PassReadCoverage","KickPower","KickAccuracy","Stamina","Durability",
            "Leadership","Scramble","Composure","Consistency","Aggressiveness","Coverage","Speed","Strength","Tackle"
        };

        string GetFormula(string line)
        {
            string retVal = "";
            string pos ="";
            int index = line.IndexOf(",");
            if (index > 0)
            {
                pos = line.Substring(0, index+1);
            }
            if (mFormulas.ContainsKey(pos))
            {
                retVal = mFormulas[pos];
            }
            return retVal;
        }

        private void InitFormulas()
        {
            mFormulas = new Dictionary<string, string>(17);
            int lineEnd = 0;
            int index = 0;
            string formula = "";
            string pos = "";
            for (int i = 1; i < SortOrder.Length; i++)
            {
                // get the formula for the pos
                pos = SortOrder[i].Replace(",","") + ":";
                index = FormulasString.IndexOf(pos);
                lineEnd = FormulasString.IndexOf("\n", index);
                formula = FormulasString.Substring(index, lineEnd - index).Trim().Replace(pos,"");
                mFormulas.Add(SortOrder[i], formula);
            }
        }

        #endregion
        /// <summary>
        /// The default sort formulas
        /// </summary>
        public const string sFormulasString =
@"#You can use the following skills in the sort formulas below:
#Speed,Agility,Strength,Jumping,Coverage,PassRush,RunCoverage,PassBlocking,RunBlocking,Catch,
#RunRoute,BreakTackle,HoldOntoBall,PassAccuracy,PassArmStrength,PassReadCoverage,
#Tackle,KickPower,KickAccuracy,Stamina,Durability,Leadership,Scramble,Composure,Consistency,Aggressiveness
# 
# Position sort Formulas:
QB: (PassAccuracy + PassArmStrength + Leadership) /3
RB: (3 * Speed + ( HoldOntoBall + BreakTackle))/ 5
FB: (Catch + 3*RunBlocking ) /4
WR: (3*Speed + 2*Catch + RunRoute)/5
TE: (2*Speed + 2*Catch + RunRoute + PassBlocking + RunBlocking)/7
C: (PassBlocking+RunBlocking)/2
G: (PassBlocking+RunBlocking)/2
T: (PassBlocking+RunBlocking)/2
DE: (3*Speed + PassRush + Strength+ Tackle) /6
DT: (RunCoverage + 2*Strength + PassRush) / 4
ILB: (PassRush + RunCoverage + 2*Speed ) /5
OLB: (3 * Speed + 2 * Tackle + PassRush + RunCoverage + Coverage )/8
CB: (3*Speed + 3 * Coverage + Tackle )/7
FS: (2* Coverage + Tackle + Speed) / 4
SS: (2* Coverage + 2*Tackle + Speed) / 5
K: (KickPower + 2*KickAccuracy)/3
P: KickPower
";

        private string mFormulasString = sFormulasString;

        /// <summary>
        /// The sort formula text to use.
        /// </summary>
        public string FormulasString
        {
            get { return mFormulasString; }
            set { mFormulasString = value; }
        }

    }
}
