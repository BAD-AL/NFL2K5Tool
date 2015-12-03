using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace NFL2K5Tool
{
    public class PlayerSorter : PlayerParser, IComparer<string>
    {
        private DataTable Table = new DataTable();

        public PlayerSorter(string key):base(key) { }

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
                catch (Exception)
                {
                    throw new Exception("Error comparing position: " + playerPartsX[0] + "\nCheck formula for position.");
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
            string pos = "";
            int index = line.IndexOf(",");
            if (index > 0)
            {
                pos = line.Substring(0, index + 1);
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
                pos = SortOrder[i].Replace(",", "") + ":";
                index = FormulasString.IndexOf(pos);
                lineEnd = FormulasString.IndexOf("\n", index);
                formula = FormulasString.Substring(index, lineEnd - index).Trim().Replace(pos, "");
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
