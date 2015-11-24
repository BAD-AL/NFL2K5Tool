using System;
using System.Collections.Generic;
using System.Text;

namespace NFL2K5Tool
{
    /// <summary>
    /// Validates player attributes
    /// </summary>
    public class PlayerValidator
    {
        /// <summary>
        /// Will add warnings if there is an issue with the player. 
        /// </summary>
        /// <param name="line"></param>
        public void ValidatePlayer(string line)
        {
            List<string> playerParts = InputParser.ParsePlayerLine(line);
        }
    }
}
