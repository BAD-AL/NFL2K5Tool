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

    }
}
