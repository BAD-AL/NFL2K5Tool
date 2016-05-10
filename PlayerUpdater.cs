using System;
using System.Collections.Generic;
using System.Text;

namespace NFL2K5Tool
{
    public class PlayerUpdater: PlayerParser
    {
        public PlayerUpdater(string key):base(key) {}

        public string AutoUpdatePBP(string line)
        {
            string retVal = line;
            string val = "";
            List<string> playerParts = GetPlayerParts(line);
            string firstName = Get(playerParts, "fname");
            string lastName = Get(playerParts, "lname");
            string number = Get(playerParts, "JerseyNumber");
            if (number.Length < 2)
                number = "#0" + number;
            else
                number = "#" + number;

            string key = lastName + ", " + firstName;
            if (DataMap.PBPMap.ContainsKey(key))
                val = DataMap.PBPMap[key];
            else if (DataMap.PBPMap.ContainsKey(lastName))
                val = DataMap.PBPMap[lastName];
            else if( DataMap.PBPMap.ContainsKey(number))
                val = DataMap.PBPMap[number];
            if( val.Length > 0 && Set(playerParts, "PBP", val))
                retVal = StringifyPlayerParts(playerParts);
            return retVal;
        }

        public string AutoUpdatePhoto(string line)
        {
            string retVal = line;
            string val = "";
            List<string> playerParts = GetPlayerParts(line);
            string firstName = Get(playerParts, "fname");
            string lastName = Get(playerParts, "lname");
            string number = Get(playerParts, "JerseyNumber");
            if (number.Length < 2)
                number = "#0" + number;
            else
                number = "#" + number;

            string key = lastName + ", " + firstName;
            if (DataMap.PhotoMap.ContainsKey(key))
                val = DataMap.PhotoMap[key];
            else if (DataMap.PhotoMap.ContainsKey(lastName))
                val = DataMap.PhotoMap[lastName];
            else if( DataMap.PhotoMap.ContainsKey(number))
                val = DataMap.PhotoMap[number];
            if (val.Length > 0 && Set(playerParts, "Photo", val))
                retVal = StringifyPlayerParts(playerParts);
            return retVal;
        }

        public string UpdatePlayers(string text, bool updatePBP, bool updatePhoto)
        {
            StringBuilder builder = new StringBuilder(text.Length);
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
                    if( updatePBP)
                        line = AutoUpdatePBP(line);
                    if (updatePhoto)
                        line = AutoUpdatePhoto(line);
                }
                builder.Append(line);
                builder.Append("\n");
            }
            return builder.ToString();
        }

    }
}
