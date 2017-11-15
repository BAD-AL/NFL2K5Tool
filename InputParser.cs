using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace NFL2K5Tool
{
    public class InputParser
    {
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
        /// Gets teh team corresponding to the text position
        /// </summary>
        public static string GetTeam(int textPosition, string data)
        {
            string team = "49ers";
            Regex r = new Regex("TEAM\\s*=\\s*([a-zA-Z49]+)", RegexOptions.IgnoreCase);
            MatchCollection mc = r.Matches(data);
            Match theMatch = null;

            foreach (Match m in mc)
            {
                if (m.Index > textPosition)
                    break;
                theMatch = m;
            }

            if (theMatch != null)
            {
                team = theMatch.Groups[1].Value;
            }
            return team;
        }

        /// <summary>
        /// returns the line that linePosition falls on in data
        /// </summary>
        public static string GetLine(int textPosition, string data)
        {
            string ret = null;
            if (textPosition < data.Length)
            {
                int i = 0;
                int lineStart = 0;
                int posLen = 0;
                for (i = textPosition; i > 0; i--)
                {
                    if (data[i] == '\n')
                    {
                        lineStart = i + 1;
                        break;
                    }
                }
                i = lineStart;
                if (i < data.Length)
                {
                    char current = data[i];
                    while (i < data.Length - 1 && current != '\n')
                    {
                        posLen++;
                        i++;
                        current = data[i];
                    }
                    ret = data.Substring(lineStart, posLen);
                }
            }
            return ret;
        }

        /// <summary>
        /// returns the n'th line after the textPosition in 'data'. returns null if end of input reached before number of lines is reached.
        /// </summary>
        public static string GetLineAfter(int textPosition, int linesAfter, string data)
        {
            int ne = 0;
            int i;
            for (i = textPosition; i < data.Length; i++)
            {
                if (data[i] == '\n')
                    ne++;
                if (ne == linesAfter)
                {
                    i++;
                    break;
                }
            }
            if (i < data.Length)
                return GetLine(i, data);
            
            return null;
        }

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
            Tool.GetKey(true, true);
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

        protected virtual bool ProcessLine(string line)
        {
            bool retVal = true;
            line = line.Trim();
            if (line.EndsWith(","))
                line = line.Substring(0, line.Length - 1);

            if (line.StartsWith("#") || line.Length < 1)
            {
            }
            else if (line.StartsWith("KEY=", StringComparison.InvariantCultureIgnoreCase))
            {
                Tool.SetKey(line.Substring(4));
            }
            else if (line.StartsWith("CoachKEY=", StringComparison.InvariantCultureIgnoreCase))
            {
                Tool.CoachKey = line.Substring(9);
            }
            else if (line.StartsWith("SET"))
            {
                ApplySet(line);
            }
            else if (line.IndexOf("LookupAndModify", StringComparison.InvariantCultureIgnoreCase) > -1)
            {
                mCurrentState = ParsingStates.PlayerLookupAndApply;
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
                    return false;
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
            else if (line.StartsWith("KR1,") || line.StartsWith("KR2,") || line.StartsWith("PR,") || line.StartsWith("LS,"))
            {
                SetSpecialTeamPlayer(line);
            }
            else if (line.StartsWith("Coach,", StringComparison.InvariantCultureIgnoreCase))
            {
                SetCoachData(line);
            }
            else
            {
                switch (mCurrentState)
                {
                    case ParsingStates.PlayerModification:
                        retVal = InsertPlayer(line);
                        break;
                    case ParsingStates.Schedule:
                        mScheduleList.Add(line.ToLower());
                        break;
                    case ParsingStates.PlayerLookupAndApply:
                        retVal = LookupPlayerAndApply(line);
                        break;
                }
            }
            return retVal;
        }

        private bool LookupPlayerAndApply(string line)
        {
            bool retVal = false;
            List<string> attributes = ParsePlayerLine(line);
            string pos = attributes[0];
            string firstName = attributes[1];
            string lastName = attributes[2];

            List<int> playersToApplyTo = Tool.FindPlayer(pos, firstName, lastName);
            if(playersToApplyTo.Count > 0)
                retVal = SetPlayerData(playersToApplyTo[0], line, false);

            return retVal;
        }

        private void SetCoachData(string line)
        {
            //"Coach,Team,fname,lname,Body,Photo";
            string[] keyParts = Tool.CoachKey.Split(",".ToCharArray());
            List<string> parts = ParseCoachLine(line);
            int teamIndex = Tool.GetTeamIndex(parts[1]);
            string[] enumNames = Enum.GetNames(typeof(CoachOffsets));
            CoachOffsets current = CoachOffsets.Body;
            try
            {
                for (int i = 2; i < keyParts.Length; i++)
                {
                    if (i == parts.Count) break; // stop processing if we're out of parts
                    switch (keyParts[i].ToLower())
                    {
                        case "firstname":
                        case "fname":
                            Tool.SetCoachAttribute(teamIndex, CoachOffsets.FirstName, parts[i]);
                            break;
                        case "lastname":
                        case "lname":
                            Tool.SetCoachAttribute(teamIndex, CoachOffsets.LastName, parts[i]);
                            break;
                        default:
                            current = (CoachOffsets)Enum.Parse(typeof(CoachOffsets), keyParts[i], true);
                            Tool.SetCoachAttribute(teamIndex, current, parts[i]);
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                StaticUtils.AddError(string.Format("Error setting data for line:\r\n{0}\r\n\r\nPerhaps check '{1}' attribute.", line, current.ToString()));
            }
        }


        // Expecting a line like "KR1,CB2"
        private void SetSpecialTeamPlayer(string line)
        {
            string[] parts = line.Split(",".ToCharArray());
            if (parts.Length == 2)
            {
                try
                {
                    SpecialTeamer guy = (SpecialTeamer)Enum.Parse(typeof(SpecialTeamer), parts[0]);
                    Positions pos = (Positions)Enum.Parse(typeof(Positions), parts[1].Substring(0, parts[1].Length - 1));
                    int depth = 1;
                    Int32.TryParse(parts[1].Substring(parts[1].Length - 1), out depth);
                    Tool.SetSpecialTeamPosition(mTracker.Team, guy, pos, depth);
                }
                catch
                {
                    StaticUtils.AddError(string.Format("Team:{0} Error adding special team player {1}", mTracker.Team, line));
                }
            }
            else if (parts.Length == 3)
            {
                try
                {
                    SpecialTeamer guy = (SpecialTeamer)Enum.Parse(typeof(SpecialTeamer), parts[0]);
                    Tool.SetSpecialTeamPosition(mTracker.Team, guy, parts[1], parts[2]);
                }
                catch
                {
                    StaticUtils.AddError(string.Format("Team:{0} Error adding special team player {1}", mTracker.Team, line));
                }
            }
        }

        private bool InsertPlayer(string line)
        {
            int playerIndex = GetPlayerIndex(line);
            bool useExisting = this.UseExistingNames || (playerIndex >= GamesaveTool.FirstDraftClassPlayer);
            return SetPlayerData(playerIndex, line, useExisting);
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
            List<string> retVal = null;
            if (!String.IsNullOrEmpty(line))
            {
                retVal = new List<string>(line.Split(sDelim));
                for (int i = 0; i < retVal.Count; i++)
                {
                    // Fixup the issue with commas inside quoted strings. 
                    // (This however only works for strings that have 1 comma inside)
                    if (retVal[i].EndsWith("\"") && i > 0 && retVal[i - 1].StartsWith("\""))
                    {
                        retVal[i - 1] += (sDelim[0] + retVal[i]);
                        retVal.RemoveAt(i);
                    }
                    else if (retVal[i].Length == 0) // remove empty strings
                    {
                        retVal.RemoveAt(i);
                    }
                }
            }
            return retVal;
        }
        /// <summary>
        /// Parses a line of text into a list of strings.
        /// </summary>
        /// <param name="line">a comma deliminated string of attributes.</param>
        /// <returns>a list of strings</returns>
        public static List<string> ParseCoachLine(string line)
        {
            if (sDelim == null)
            {
                sDelim = CharCount(line, ';') > CharCount(line, ',') ? sSemiColon : sComma;
            }
            List<string> retVal = new List<string>();
            if (!String.IsNullOrEmpty(line))
            {
                int quoteCount = 0;
                StringBuilder builder = new StringBuilder(line);
                for (int i = 0; i < builder.Length; i++)
                {
                    if (builder[i] == '"')
                        quoteCount++;
                    else if (quoteCount % 2 == 1 && builder[i] == sDelim[0])
                        builder[i] = '|';
                }

                retVal = new List<string>(builder.ToString().Split(sDelim));
                for (int i = 0; i < retVal.Count; i++)
                {
                    if (retVal[i].IndexOf('|') > -1)
                        retVal[i] = retVal[i].Replace('|', ',');
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
        /// Returns the index of the nth occurrence of the given character
        /// </summary>
        public static int NthIndex(string input, char thingToCount, int index)
        {
            int count = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == thingToCount)
                {
                    count++;
                    if (count == index)
                        return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Sets a player's attributes
        /// </summary>
        /// <param name="player">The index of the player</param>
        /// <param name="line">The data tp apply.</param>
        public bool SetPlayerData(int player, string line, bool useExistingName)
        {
            return SetPlayerStuff1(player, line, useExistingName);
        }

        private void SetPlayerStuff2(int player, string line, bool useExistingName)
        {
            string attribute = "";
            if (player > -1 && player < Tool.MaxPlayers)
            {
                string[] keyParts = Tool.GetKey(true, true).Replace("#", "").Split(",".ToCharArray());
                string field = "";
                List<string> attributes = ParsePlayerLine(line);
                for (int i = 0; i < attributes.Count; i++)
                {
                    field = keyParts[i];
                    attribute = attributes[i];
                    if (attribute != "?" && attribute != "_")
                        Tool.SetPlayerField(player, field, attribute);
                }
            }
        }

        // About 6 x faster than SetPlayerStuff2
        private bool SetPlayerStuff1(int player, string line, bool useExistingName)
        {
            string attribute = "";
            string playerName = "";
            if (player > -1 && player < Tool.MaxPlayers)
            {
                int attr = -1;
                List<string> attributes = ParsePlayerLine(line);
                if ( useExistingName && !CheckPlayerNameExists( attributes, out playerName ))
                {
                    StaticUtils.AddError("Could not find matching name in string database. player not added: "+ playerName);
                    return false;
                }
                for (int i = 0; i < attributes.Count; i++)
                {
                    try
                    {
                        attr = Tool.Order[i];
                        attribute = attributes[i];
                        if (attr == -1)
                        {
                            // Name setting perhaps should be done at another, smarter level?
                            // How we gonna decide to use pointers or not?
                            if( !Tool.SetPlayerFirstName(player, attribute, useExistingName))
                                StaticUtils.AddError("Error setting FirstName >" + attribute + "< for '" + line + "' Can only use existing names for college players.");
                        }
                        else if (attr == -2)
                        {
                            // How we gonna decide to use pointers or not?
                            if( !Tool.SetPlayerLastName(player, attribute, useExistingName))
                                StaticUtils.AddError("Error setting LastName >" + attribute + "< for '" + line + "' Can only use existing names for college players.");
                        }
                        else if (attribute == "?" || attribute == "_")
                        {// do nothing
                        }
                        else if (attr >= (int)AppearanceAttributes.College)
                        {
                            if (attr != (int)AppearanceAttributes.College)
                                attribute = attribute.Replace(" ", ""); // strip spaces
                            Tool.SetPlayerAppearanceAttribute(player, (AppearanceAttributes)attr, attribute);
                        }
                        else
                        {
                            Tool.SetAttribute(player, (PlayerOffsets)attr, attribute);
                        }
                    }
                    catch (Exception)
                    {
                        string desc = attr > 99 ? ((AppearanceAttributes)attr).ToString() : ((PlayerOffsets)attr).ToString();
                        StaticUtils.AddError("Error setting attribute '" + desc + "' to '" + attribute + "'");
                    }
                }
            }
            return true;
        }

        public List<string> MissingNames = new List<string>();

        private bool CheckPlayerNameExists(List<string> attributes, out string playerName)
        {
            int firstNameIndex = Array.IndexOf(Tool.Order, -1);
            int lastNameIndex = Array.IndexOf(Tool.Order, -2);

            string firstName = attributes[firstNameIndex];
            string lastName = attributes[lastNameIndex];
            bool firstNameExists = Tool.CheckNameExists(firstName);
            bool lastNameExists  = Tool.CheckNameExists(lastName);
            bool retVal = firstNameExists && lastNameExists;
            if (!retVal)
            {
                playerName = String.Format("{0} {1} firstNameExists={2} lastNameExists={3}",
                    firstName, lastName, firstNameExists, lastNameExists);
                if (!firstNameExists)
                    MissingNames.Add(firstName);
                else
                    MissingNames.Add(lastName);
            }
            else
                playerName = "";
            return retVal;
        }

        private bool SetCurrentTeam(string team)
        {
            if (Tool.GetTeamIndex(team) < 0)
            {//error condition
                StaticUtils.AddError(string.Format("Team '{0}' is Invalid.", team));
                return false;
            }
            else
            {
                mTracker.Team = team;
                mTracker.Reset();
                if (team == "DraftClass")
                    UseExistingNames = true;
                else
                    UseExistingNames = false;
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

        /*// <summary>
        /// get all necessary player names.
        /// Put them in the Save file.
        /// </summary>
        /// <returns>a list of NameObjects we can use to reference when inserting players, sorted by name.</returns>
        private static List<NameObject> SetupNames(GamesaveTool tool, string[] lines, string key)
        {
            List<string> neededNames = NameHelper.GetNeededNames(lines, key);
            List<NameObject> allNamesInSave = NameHelper.GetAllNames(tool);
            List<NameObject> unNeededNames = new List<NameObject>(200);
            Dictionary<string, NameObject> nameMap = new Dictionary<string, NameObject>(3000);
            NameObjectComparer noc = new NameObjectComparer(NameObjectCompareMode.Name);
            allNamesInSave.Sort(noc);
            NameObject tmp = new NameObject();
            NameObject reference = null;
            int index = -1;
            //populate unneeded names
            for (int i = 0; i < allNamesInSave.Count; i++)
            {
                index = neededNames.BinarySearch(allNamesInSave[i].Name);
                if (index < 0)
                    unNeededNames.Add(allNamesInSave[i]);
            }
            // populate name map
            

            return null;
        }*/

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
        PlayerLookupAndApply,
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
