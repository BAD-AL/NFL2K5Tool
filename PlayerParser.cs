using System;
using System.Collections.Generic;
using System.Text;

namespace NFL2K5Tool
{
    /// <summary>
    /// Base class for some useful things.
    /// </summary>
    public abstract class PlayerParser
    {
        private string mKey;
        private string[] mKeyParts;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        public PlayerParser(string key)
        {
            mKey = key.Replace("#", "");
            mKeyParts = mKey.Split(new char[] { ',' });
        }

        /// <summary>
        /// just a reminder of how to parse player parts.
        /// </summary>
        public static List<string> GetPlayerParts(string playerLine)
        {
            return InputParser.ParsePlayerLine(playerLine);
        }

        /// <summary>
        /// Get the player's value of 'attribute'
        /// </summary>
        protected string Get(List<string> playerParts, string attribute)
        {
            string retVal = "";
            int index = -1;
            for (int i = 0; i < mKeyParts.Length; i++)
            {
                if (mKeyParts[i].Equals(attribute, StringComparison.InvariantCultureIgnoreCase))
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
        /// Sets an attribute of the player's parts
        /// </summary>
        /// <returns>true if set successfully</returns>
        protected bool Set(List<string> playerParts, string attribute, string value)
        {
            bool retVal = false;

            int index = -1;
            for (int i = 0; i < mKeyParts.Length; i++)
            {
                if (mKeyParts[i].Equals(attribute, StringComparison.InvariantCultureIgnoreCase))
                {
                    index = i;
                    break;
                }
            }
            if (index > -1)
            {
                playerParts[index] = value;
                retVal = true;
            }
            return retVal;
        }

        /// <summary>
        /// returns The player in string form
        /// </summary>
        public string StringifyPlayerParts(List<string> playerParts)
        {
            StringBuilder builder = new StringBuilder(300);
            foreach (string attr in playerParts)
            {
                if (attr.IndexOf(',') > -1)
                {
                    builder.Append("\"");
                    builder.Append(attr);
                    builder.Append("\"");
                }
                else
                    builder.Append(attr);
                builder.Append(",");
            }
            return builder.ToString();
        }


        private List<string> mWarnings = new List<string>();

        protected void AddWarning(string w)
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
    }
}
