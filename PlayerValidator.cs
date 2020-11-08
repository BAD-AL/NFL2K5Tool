using System;
using System.Collections.Generic;
using System.Text;

namespace NFL2K5Tool
{
    /// <summary>
    /// Validates player attributes, sort a team
    /// TODO: Split this up into 2 classes with a shared base
    /// </summary>
    public class PlayerValidator: PlayerParser
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="key">The attribute key.</param>
        public PlayerValidator(string key ):base(key) { }

        /// <summary>
        /// Processes the text looking for player attributes that seem incorrect.
        /// Adds warnings when something seems like it may be incorrect.
        /// </summary>
        /// <param name="text">The league/ team data</param>
        public string ValidatePlayers(string text)
        {
            StringBuilder builder = new StringBuilder(); 
            text = text.Replace("\r\n", "\n");
            char[] chars = "\n".ToCharArray();
            string[] lines = text.Split(chars);
            string line;
            for (int i = 0; i < lines.Length; i++)
            {
                line = lines[i].Trim();
                if (line.StartsWith("#") || line.StartsWith("Key="))
                {
                }
                else if (line.IndexOf(",") > -1 && !(  // don't check the following lines
                          line.StartsWith("Coach")
                       || line.StartsWith("SET")
                       || line.StartsWith("ApplyFormula")
                    ))
                {
                    try {                builder.Append(ValidatePlayer(line));                 } 
                    catch (Exception) { Console.WriteLine("# issue validating line: " + line); }
                }
            }
            if (builder.Length > 0)
            {
                builder.Insert(0, "LookupAndModify\n"+
                    "Key=Position,fname,lname,BodyType,Height,Weight\n"+
                    "#Team = FreeAgents  (this is a comment but allows player editor to function on this data)\n"
                    );
            }
            return builder.ToString();
        }

        /// <summary>
        /// Will add warnings if there is an issue with the player. 
        /// </summary>
        /// <param name="line">The player line</param>
        public string ValidatePlayer(string line)
        {
            List<string> playerParts = InputParser.ParsePlayerLine(line);
            PlayerValidationResult res = new PlayerValidationResult(Get(playerParts, "Position"), Get(playerParts, "fname"), Get(playerParts, "lname"));
            ValidateBodyType(playerParts, res);
            ValidateWeight(playerParts, res);
            if (res.Invalid)
            {
                res.Height = Get(playerParts, "Height");
                res.BodyType = Get(playerParts, "BodyType");
                res.Weight = Get(playerParts, "Weight");
                return String.Format("{0},{1},{2},{3},{4},{5}\n", res.Position, res.FirstName, res.LastName, res.BodyType, res.Height, res.Weight);
            }
            return "";
        }

        private void ValidateWeight(List<string> playerParts, PlayerValidationResult res)
        {
            string pos = Get(playerParts, "Position");
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
            if (!ValidateAttribute("Weight", possibilities, playerParts))
            {
                res.Invalid = true;
            }
        }

        // Skinny = 0, Normal, Large, ExtraLarge
        private void ValidateBodyType(List<string> playerParts, PlayerValidationResult res)
        {
            string pos = Get(playerParts, "Position");
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
            if (!ValidateAttribute("BodyType", possibilities, playerParts))
            {
                res.Invalid = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attribute">the attribute to validate (BodyType, Weight...)</param>
        /// <param name="validValues"></param>
        /// <param name="playerParts"></param>
        private bool ValidateAttribute(string attribute, string[] validValues, List<string> playerParts)
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
                return false;
            
            return true;
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

    }

    public class PlayerValidationResult
    {
        public PlayerValidationResult(string position, string firstName, string lastName)
        {
            this.Position = position;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Invalid = false;
        }

        public string Position  { get; set; }
        public string FirstName { get; set; }
        public string LastName  { get; set; }

        public String BodyType  { get; set; }
        public String Height    { get; set; }
        public String Weight    { get; set; }

        public bool Invalid { get; set; }
    }
}
