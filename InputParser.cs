using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace NFL2K5Tool
{
    public class InputParser
    {
        List<string> mErrors = new List<string>();
        private Regex mTeamRegex = new Regex("Team\\s*=\\s*([0-9a-zA-Z]+)");
        private Regex mWeekRegex  = new Regex("Week ([1-9][0	-7]?)");
        private Regex mGameRegex  = new Regex("([0-9a-z]+)\\s+at\\s+([0-9a-zA-Z]+)");
        private ParsingStates mCurrentState = ParsingStates.PlayerModification;
        private InputParserTeamTracker mTracker = new InputParserTeamTracker();

        public GamesaveTool Tool { get; set; }

        /// <summary>
        /// Process the text in the given file, applying it to the gamesave data.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns> empty string on success, a non empty string on failure.</returns>
        public void ProcessFile(string fileName)
        {
            try
            {
                StreamReader sr = new StreamReader(fileName);
                string contents = sr.ReadToEnd();
                sr.Close();
                ProcessText(contents);
            }
            catch (Exception e)
            {
                StaticUtils.Errors.Add(String.Format("Error processing file '{0}'. {1}", fileName, e.Message));
            }
        }

        public void ReadFromStdin()
        {
            string line = "";
            int lineNumber = 0;
            Console.WriteLine("Reading from standard in...");
            try
            {
                while ((line = Console.ReadLine()) != null)
                {
                    lineNumber++;
                    ProcessLine(line);
                    //Console.WriteLine("Line "+lineNumber);
                }
                //ApplySchedule();
            }
            catch (Exception e)
            {
                StaticUtils.Errors.Add(
                string.Format(
                 "Error Processing line {0}:'{1}'.\n{2}\n{3}",
                    lineNumber, line, e.Message, e.StackTrace));
            }
        }

        public void ProcessText(string text)
        {
            mDelim = CharCount(text, ';') > CharCount(text, ',') ? mSemiColon : mComma;
            char[] chars = "\n\r".ToCharArray();
            string[] lines = text.Split(chars);
            ProcessLines(lines);
        }

        public void ProcessLines(string[] lines)
        {
            Tool.GetKey(true, true); // TODO, plumb support for specifying 'Key'; Figure out what to take in
            int i = 0;
            try
            {
                for (i = 0; i < lines.Length; i++)
                {
                    ProcessLine(lines[i]);
                    //Console.WriteLine(i);
                }
                //ApplySchedule();
            }
            catch (Exception e)
            {
                StringBuilder sb = new StringBuilder(150);
                sb.Append("Error! ");
                if (i < lines.Length)
                    sb.Append(string.Format("line #{0}:\t'{1}'", i, lines[i]));
                sb.Append(e.Message);
                sb.Append("\n");
                sb.Append(e.StackTrace);
                //						"Error Processing line {0}:\t'{1}'.\n{2}\n{3}",
                //						i,lines[i], e.Message,e.StackTrace);
                sb.Append("\n\nOperation aborted at this point. Data not applied.");
                StringInputDlg.ShowError(sb.ToString());
            }
        }

        private Match mTeamMatch = Match.Empty;

        protected virtual void ProcessLine(string line)
        {
            line = line.Trim();
            if (line.EndsWith(","))
                line = line.Substring(0, line.Length - 1);

            if (line.StartsWith("#") || line.Length < 1)
            {
            }
            else if ( (mTeamMatch = mTeamRegex.Match(line)) != Match.Empty)
            {
                Console.WriteLine("'{0}' ", line);
                mCurrentState = ParsingStates.PlayerModification;
                string team = mTeamMatch.Groups[1].ToString();
                bool ret = SetCurrentTeam(team);
                if (!ret)
                {
                    StaticUtils.Errors.Add(string.Format("ERROR with line '{0}'.", line));
                    StaticUtils.Errors.Add(string.Format("Team input must be in the form 'TEAM = team '"));
                    return;
                }
            }
            else 
            {
                switch (mCurrentState)
                {
                    case ParsingStates.PlayerModification:
                        InsertPlayer(line);
                        break;
                    case ParsingStates.Schedule:
                        break;
                }
            }
        }

        private void InsertPlayer(string line)
        {
            int playerIndex = GetPlayerIndex(line);
            SetPlayerData(playerIndex, line);
        }

        private int GetPlayerIndex(string line)
        {
            int retVal = -1;
            List<int> playerIndexes = Tool.GetPlayerIndexesForTeam(mTracker.Team);

            if (mTracker.PlayerCount < playerIndexes.Count)
            {
                retVal = playerIndexes[mTracker.PlayerCount++];
            }
            else
            {
                StaticUtils.Errors.Add(String.Format("Error, team player limit reached. {0}; cannot add player: {1}",mTracker.Team, line));
            }
            return retVal;
        }


        private char[] mComma = new char[] { ',' };
        private char[] mSemiColon = new char[] { ';' };
        // will be set when starting to parse.
        private char[] mDelim;

        private List<string> ParsePlayerLine(string line)
        {
            List<string> retVal = new List<string>(line.Split(mDelim));
            for (int i = 0; i < retVal.Count; i++)
            {
                // Fixup the issue with commas inside quoted strings. 
                // (This however only works for strings that have 1 comma inside)
                if (retVal[i].EndsWith("\"") && i > 0 && retVal[i - 1].StartsWith("\""))
                {
                    retVal[i - 1] += ( mDelim[0] + retVal[i]);
                    retVal.RemoveAt(i);
                }
            }
            return retVal;
        }

        int CharCount(string input, char thingToCount)
        {
            int retVal = 0;
            for (int i = 0; i < input.Length; i++)
                if (input[i] == thingToCount)
                    retVal++;

            return retVal;
        }

        /// <summary>
        /// Sets a player's attributes
        /// </summary>
        /// <param name="player">The index of the player</param>
        /// <param name="line">The data tp apply.</param>
        public bool SetPlayerData(int player, string line)
        {
            bool retVal = false;
            if (player > -1 && player < Tool.MaxPlayers)
            {
                int attr = -1;
                List<string> attributes = ParsePlayerLine(line);
                for (int i = 0; i < attributes.Count; i++)
                {
                    try
                    {
                        attr = Tool.Order[i];
                        if (attr == -1)
                        {
                            // Name setting perhaps should be done at another, smarter level?
                            // How we gonna decide to use pointers or not?
                            Tool.SetPlayerFirstName(player, attributes[i], false);
                        }
                        else if (attr == -2)
                        {
                            // How we gonna decide to use pointers or not?
                            Tool.SetPlayerLastName(player, attributes[i], false);
                        }
                        else if (attr >= (int)AppearanceAttributes.College)
                        {
                            Tool.SetPlayerAppearanceAttribute(player, (AppearanceAttributes)attr, attributes[i]);
                        }
                        else
                        {
                            Tool.SetAttribute(player, (PlayerOffsets)attr, attributes[i]);
                        }
                    }
                    catch (Exception)
                    {
                        string desc = attr > 99 ? ((AppearanceAttributes)attr).ToString() : ((PlayerOffsets)attr).ToString();

                        StaticUtils.Errors.Add("Error setting attribute '" + desc + "' to '" + attributes[i]);
                    }
                }
                retVal = true;
            }
            return retVal;
        }

        private string GetAwayTeam(string line)
        {
            Match m = mGameRegex.Match(line);
            string awayTeam = m.Groups[1].ToString();
            return awayTeam;
        }

        private string GetHomeTeam(string line)
        {
            Match m = mGameRegex.Match(line);
            string team = m.Groups[2].ToString();
            return team;
        }

        private int GetWeek(string line)
        {
            Match m = mWeekRegex.Match(line);
            string week_str = m.Groups[1].ToString();
            int ret = -1;
            try
            {
                ret = Int32.Parse(week_str);
                ret--; // our week starts at 0
            }
            catch
            {
                StaticUtils.Errors.Add(string.Format("Week '{0}' is invalid.", week_str));
            }
            return ret;
        }

        private bool SetCurrentTeam(string team)
        {
            if (Tool.GetTeamIndex(team) < 0)
            {//error condition
                mErrors.Add(string.Format("Team '{0}' is Invalid.", team));
                return false;
            }
            else
            {
                mTracker.Team = team;
                mTracker.Reset();
            }
            return true;
        }

    }

    public enum ParsingStates
    {
        PlayerModification,
        Schedule
    }

    /// <summary>
    /// class used to help keep track of number of players being input.
    /// </summary>
    public class InputParserTeamTracker
    {
        public int CBs = 0;
        public int DEs = 0;
        public int DTs = 0;
        public int FBs = 0;
        public int Gs  = 0;
        public int RBs = 0;
        public int OLBs = 0;
        public int ILBs = 0;
        public int Ps = 0;
        public int QBs = 0;
        public int SSs = 0;
        public int Ts = 0;
        public int TEs = 0;
        public int WRs = 0;
        public int Cs = 0;

        public int PlayerCount = 0;

        public string Team = "";

        public void Reset()
        {
             CBs = 0;
             DEs = 0;
             DTs = 0;
             FBs = 0;
             Gs = 0;
             RBs = 0;
             OLBs = 0;
             ILBs = 0;
             Ps = 0;
             QBs = 0;
             SSs = 0;
             Ts = 0;
             TEs = 0;
             WRs = 0;
             Cs = 0;
             PlayerCount = 0;
        }
    }
}
