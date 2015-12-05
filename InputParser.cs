﻿using System;
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
        private Regex mWeekRegex  = new Regex("(?i)Week(?-i) ([1-9][0-7]?)");
        private Regex mGameRegex  = new Regex("([0-9a-z]+)\\s+at\\s+([0-9a-zA-Z]+)");
        private Regex mYearRegex = new Regex("YEAR\\s*=\\s*([0-9]+)");
        private ParsingStates mCurrentState = ParsingStates.PlayerModification;
        private InputParserTeamTracker mTracker = new InputParserTeamTracker();
        private List<string> mScheduleList;

        /// <summary>
        /// true to use existing names; false to replace the names.
        /// </summary>
        public bool UseExistingNames { get; set; }

        public InputParser(GamesaveTool tool)
        {
            this.Tool = tool;
        }


        private GamesaveTool Tool { get; set; }

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
                StaticUtils.AddError(String.Format("Error processing file '{0}'. {1}", fileName, e.Message));
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
                }
                ApplySchedule();
            }
            catch (Exception e)
            {
                StaticUtils.AddError(
                string.Format(
                 "Error Processing line {0}:'{1}'.\n{2}\n{3}",
                    lineNumber, line, e.Message, e.StackTrace));
            }
        }

        public void ProcessText(string text)
        {
            sDelim = CharCount(text, ';') > CharCount(text, ',') ? sSemiColon : sComma;
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
                }
                ApplySchedule();
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
            else if (line.StartsWith("SET"))
            {
                ApplySet(line);
            }
            else if ((mTeamMatch = mTeamRegex.Match(line)) != Match.Empty)
            {
                //Console.WriteLine("'{0}' ", line);
                mCurrentState = ParsingStates.PlayerModification;
                string team = mTeamMatch.Groups[1].ToString();
                bool ret = SetCurrentTeam(team);
                if (!ret)
                {
                    StaticUtils.AddError(string.Format("ERROR with line '{0}'.", line));
                    StaticUtils.AddError(string.Format("Team input must be in the form 'TEAM = team '"));
                    return;
                }
            }
            else if (mWeekRegex.Match(line) != Match.Empty)  //line.StartsWith("WEEK"))
            {
                mCurrentState = ParsingStates.Schedule;
                if (mScheduleList == null)
                    mScheduleList = new List<string>(300);
                mScheduleList.Add(line);
            }
            else if (mYearRegex.Match(line) != Match.Empty)//line.StartsWith("YEAR"))
            {
                SetYear(line);
            }
            else
            {
                switch (mCurrentState)
                {
                    case ParsingStates.PlayerModification:
                        InsertPlayer(line);
                        break;
                    case ParsingStates.Schedule:
                        mScheduleList.Add(line);
                        break;
                }
            }
        }

        private void InsertPlayer(string line)
        {
            int playerIndex = GetPlayerIndex(line);
            SetPlayerData(playerIndex, line, this.UseExistingNames);
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
                StaticUtils.AddError(String.Format("Error, team player limit reached. {0}; cannot add player: {1}",mTracker.Team, line));
            }
            return retVal;
        }


        private static char[] sComma = new char[] { ',' };
        private static char[] sSemiColon = new char[] { ';' };
        // will be set when starting to parse.
        private static char[] sDelim;

        /// <summary>
        /// Parses a line of text into a list of strings.
        /// </summary>
        /// <param name="line">a comma deliminated string of attributes.</param>
        /// <returns>a list of strings</returns>
        public static List<string> ParsePlayerLine(string line)
        {
            if (sDelim == null)
            {
                sDelim = CharCount(line, ';') > CharCount(line, ',') ? sSemiColon : sComma;
            }
            List<string> retVal = new List<string>(line.Split(sDelim));
            for (int i = 0; i < retVal.Count; i++)
            {
                // Fixup the issue with commas inside quoted strings. 
                // (This however only works for strings that have 1 comma inside)
                if (retVal[i].EndsWith("\"") && i > 0 && retVal[i - 1].StartsWith("\""))
                {
                    retVal[i - 1] += ( sDelim[0] + retVal[i]);
                    retVal.RemoveAt(i);
                }
            }
            return retVal;
        }

        static int CharCount(string input, char thingToCount)
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
        public bool SetPlayerData(int player, string line, bool useExistingName)
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
                            Tool.SetPlayerFirstName(player, attributes[i], useExistingName);
                        }
                        else if (attr == -2)
                        {
                            // How we gonna decide to use pointers or not?
                            Tool.SetPlayerLastName(player, attributes[i], useExistingName);
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

                        StaticUtils.AddError("Error setting attribute '" + desc + "' to '" + attributes[i]);
                    }
                }
                retVal = true;
            }
            return retVal;
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

        private void SetYear(string line)
        {
            Match m = mYearRegex.Match(line);
            string year = m.Groups[1].ToString();
            if (year.Length < 1)
            {
                StaticUtils.AddError(string.Format("'{0}' is not valid.", line));
            }
            else
            {
                Tool.SetYear(year);
                //Console.WriteLine("Year set to '{0}'", year);
            }
        }

        private void ApplySchedule()
        {
            if (mScheduleList != null && mScheduleList.Count > 0)
            {
                Tool.ApplySchedule(mScheduleList);
                mScheduleList = null;
            }
        }


        #region SetBytes logic

        private Regex simpleSetRegex;

        private void ApplySet(string line)
        {
            if (simpleSetRegex == null)
                simpleSetRegex = new Regex("SET\\s*\\(\\s*(0x[0-9a-fA-F]+)\\s*,\\s*(0x[0-9a-fA-F]+)\\s*\\)");

            if (simpleSetRegex.Match(line) != Match.Empty)
            {
                ApplySimpleSet(line);
            }
            else if (line.IndexOf("PromptUser", StringComparison.OrdinalIgnoreCase) > -1)
            {
                string simpleSetLine = StringInputDlg.PromptForSetUserInput(line);
                if (!string.IsNullOrEmpty(simpleSetLine))
                {
                    ApplySet(simpleSetLine);
                }
            }
            else
            {
                StaticUtils.AddError(string.Format("ERROR with line \"{0}\"", line));
            }
        }

        protected void ApplySimpleSet(string line)
        {
            if (simpleSetRegex == null)
                simpleSetRegex = new Regex("SET\\s*\\(\\s*(0x[0-9a-fA-F]+)\\s*,\\s*(0x[0-9a-fA-F]+)\\s*\\)");

            Match m = simpleSetRegex.Match(line);
            if (m == Match.Empty)
            {
                StaticUtils.AddError(string.Format("SET function not used properly. incorrect syntax>'{0}'", line));
                return;
            }
            string loc = m.Groups[1].ToString().ToLower();
            string val = m.Groups[2].ToString().ToLower();
            loc = loc.Substring(2);
            val = val.Substring(2);
            if (val.Length % 2 != 0)
                val = "0" + val;

            try
            {
                int location = Int32.Parse(loc, System.Globalization.NumberStyles.AllowHexSpecifier);
                byte[] bytes = GetHexBytes(val);
                if (location + bytes.Length > Tool.GameSaveData.Length)
                {
                    StaticUtils.AddError(string.Format("ApplySet:> Error with line {0}. Data falls off the end of rom.\n", line));
                }
                else if (location < 0)
                {
                    StaticUtils.AddError(string.Format("ApplySet:> Error with line {0}. location is negative.\n", line));
                }
                else
                {
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        Tool.SetByte(location + i, bytes[i]);
                    }
                }
            }
            catch (Exception e)
            {
                StaticUtils.AddError(string.Format("ApplySet:> Error with line {0}.\n{1}", line, e.Message));
            }
        }

        protected byte[] GetHexBytes(string input)
        {
            if (input == null)
                return null;

            byte[] ret = new byte[input.Length / 2];
            string b = "";
            int tmp = 0;
            int j = 0;

            for (int i = 0; i < input.Length; i += 2)
            {
                b = input.Substring(i, 2);
                tmp = Int32.Parse(b, System.Globalization.NumberStyles.AllowHexSpecifier);
                ret[j++] = (byte)tmp;
            }
            return ret;
        }
        #endregion
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
