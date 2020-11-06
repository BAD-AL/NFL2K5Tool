using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace NFL2K5Tool
{
    /// <summary>
    /// The class that reads and modifies the xbox game save file.
    /// </summary>
    public class GamesaveTool
    {
        Dictionary<string, int> mCoachMap = new Dictionary<string, int>(34);
        Dictionary<int, string> mReverseCoachMap = new Dictionary<int, string>(34);

        // Duane starks is the first player in the original roster 
        private int mPlayerStart = 0xB288; // 0xAFA8 for roster
        //may be able to add strings after this section; (All 0's from 0x8f2f0 - 0x9131f)
        private  int mModifiableNameSectionEnd = 0x8906f;

        // Do not modify strings in this section.
        // There is however a blank section that could possibly be used for 
        // names after it (0x8f2f0 - 0x91310)
        private int mCollegePlayerNameSectionStart = 0x8bab0;
        private int mCollegePlayerNameSectionEnd = 0x8f2ef;

        private int mStringTableStart = 0x75c40;
        private int mStringTableEnd   = 0x94d10;

        private int m49ersPlayerPointersStart = 0x44a8; // playerLoc = ptrLoc + ptrVal -1;
        private int m49ersNumPlayersAddress = 0x45c4; //  35
        //const int cTeamNameOffset = 0x104;// interesting, but not used 
        private int mFreeAgentCountLocation = 0x358; // default :  c5
        private int mFreeAgentPlayersPointer = 0x35c; // points to the start of the free agent pointers

        // offset from player pointer start 
        private int mCoachPointerOffset = 0x14c;
        private int mCoachStringSectionLength = 0x14b1; //5297 bytes

        private int mMaxPlayers = 2317; //1944(roster) including free agents and draft class

        public const int FirstDraftClassPlayer = 1937;
        private const int mDraftClassSize = 380;

        private const int cPlayerDataLength = 0x54;
        private const int cTeamDiff = 0x1f4; // 500 bytes
        
        const string NFL2K5Folder = "53450030";

        public SaveType SaveType 
        { 
            get { return mSaveType; }
            private set
            {
                mSaveType = value;
                Console.WriteLine("#Loading SaveType:{0}", mSaveType);
            }
        }

        private SaveType mSaveType = SaveType.Franchise;

        private string mZipFile = "";
        private string mPS2SaveFile = "";

        public GamesaveTool()
        {
            mCoachMap.Add("Dennis Green", 0x00);
            mCoachMap.Add("Jim Mora Jr.", 0x01);
            mCoachMap.Add("Brian Billick", 0x02);
            mCoachMap.Add("Mike Mularkey", 0x03);
            mCoachMap.Add("John Fox", 0x04);
            mCoachMap.Add("Lovie Smith", 0x05);
            mCoachMap.Add("Marvin Lewis", 0x06);
            mCoachMap.Add("Mike Shanahan", 0x08);
            mCoachMap.Add("Dallas Coach", 0x07);
            mCoachMap.Add("Steve Mariucci", 0x09);
            mCoachMap.Add("Mike Sherman", 0x0A);
            mCoachMap.Add("Tony Dungy", 0x0B);
            mCoachMap.Add("Jack Del Rio", 0x0C);
            mCoachMap.Add("Dick Vermeil", 0x0D);
            mCoachMap.Add("Dave Wannstedt", 0x0E);
            mCoachMap.Add("Mike Tice", 0x0F);
            mCoachMap.Add("Bill Belichick", 0x10);
            mCoachMap.Add("Jim Haslett", 0x11);
            mCoachMap.Add("Tom Coughlin", 0x12);
            mCoachMap.Add("Herman Edwards", 0x13);
            mCoachMap.Add("Norv Turner", 0x14);
            mCoachMap.Add("Andy Reid", 0x15);
            mCoachMap.Add("Bill Cowher", 0x16);
            mCoachMap.Add("Mike Martz", 0x17);
            mCoachMap.Add("Marty Schottenheimer", 0x18);
            mCoachMap.Add("Dennis Erickson", 0x19);
            mCoachMap.Add("Mike Holmgren", 0x1A);
            mCoachMap.Add("Jon Gruden", 0x1B);
            mCoachMap.Add("Jeff Fisher", 0x1C);
            mCoachMap.Add("Joe Gibbs", 0x1D);
            mCoachMap.Add("Butch Davis", 0x1E);
            mCoachMap.Add("Dom Capers", 0x25);   // Skips big here... Are 0x26-0x31 also valid?
            mCoachMap.Add("Generic1", 0x32);
            mCoachMap.Add("Generic2", 0x33);

            mReverseCoachMap.Add(0x00, "Dennis Green");
            mReverseCoachMap.Add(0x01, "Jim Mora Jr.");
            mReverseCoachMap.Add(0x02, "Brian Billick");
            mReverseCoachMap.Add(0x03, "Mike Mularkey");
            mReverseCoachMap.Add(0x04, "John Fox");
            mReverseCoachMap.Add(0x05, "Lovie Smith");
            mReverseCoachMap.Add(0x06, "Marvin Lewis");
            mReverseCoachMap.Add(0x08, "Mike Shanahan");
            mReverseCoachMap.Add(0x07, "Dallas Coach");
            mReverseCoachMap.Add(0x09, "Steve Mariucci");
            mReverseCoachMap.Add(0x0A, "Mike Sherman");
            mReverseCoachMap.Add(0x0B, "Tony Dungy");
            mReverseCoachMap.Add(0x0C, "Jack Del Rio");
            mReverseCoachMap.Add(0x0D, "Dick Vermeil");
            mReverseCoachMap.Add(0x0E, "Dave Wannstedt");
            mReverseCoachMap.Add(0x0F, "Mike Tice");
            mReverseCoachMap.Add(0x10, "Bill Belichick");
            mReverseCoachMap.Add(0x11, "Jim Haslett");
            mReverseCoachMap.Add(0x12, "Tom Coughlin");
            mReverseCoachMap.Add(0x13, "Herman Edwards");
            mReverseCoachMap.Add(0x14, "Norv Turner");
            mReverseCoachMap.Add(0x15, "Andy Reid");
            mReverseCoachMap.Add(0x16, "Bill Cowher");
            mReverseCoachMap.Add(0x17, "Mike Martz");
            mReverseCoachMap.Add(0x18, "Marty Schottenheimer");
            mReverseCoachMap.Add(0x19, "Dennis Erickson");
            mReverseCoachMap.Add(0x1A, "Mike Holmgren");
            mReverseCoachMap.Add(0x1B, "Jon Gruden");
            mReverseCoachMap.Add(0x1C, "Jeff Fisher");
            mReverseCoachMap.Add(0x1D, "Joe Gibbs");
            mReverseCoachMap.Add(0x1E, "Butch Davis");
            mReverseCoachMap.Add(0x25, "Dom Capers");
            mReverseCoachMap.Add(0x32, "Generic1");
            mReverseCoachMap.Add(0x33, "Generic2");
        }

        private void InitializeForFranchise()
        {
            SaveType = SaveType.Franchise;
            mPlayerStart = 0xB288; // 0xAFA8 for roster
            mModifiableNameSectionEnd = 0x8906f;
            mCollegePlayerNameSectionStart = 0x8bab0;
            mCollegePlayerNameSectionEnd = 0x8f2ef;

            mStringTableStart = 0x75c40;
            mStringTableEnd   = 0x94d10;

            mFreeAgentPlayersPointer = 0x35c;
            mFreeAgentCountLocation = 0x358; // default :  c5 00
            m49ersPlayerPointersStart = 0x44a8; // playerLoc = ptrLoc + ptrVal -1;
            m49ersNumPlayersAddress = 0x45c4; // 35 
            mMaxPlayers = 2317;
            SchedulerHelper.FranchiseGameOneYearLocation = 0x917ef;
            Year = 2000 + GameSaveData[SchedulerHelper.FranchiseGameOneYearLocation];
            AutoPlayerStartLocation();
        }

        private void InitializeForRoster()
        {
            SaveType = SaveType.Roster;
            mPlayerStart = 0xAFA8; 
            mModifiableNameSectionEnd = 0x88d8f;
            mCollegePlayerNameSectionStart = 0x8b7d0;
            mCollegePlayerNameSectionEnd = 0x8f00f;

            mStringTableStart = 0x75960;
            mStringTableEnd   = 0x88d80;

            mFreeAgentPlayersPointer = 0x7c;
            mFreeAgentCountLocation = 0x78; // default : 00 c5 // guess???
            m49ersPlayerPointersStart = 0x41c8; // playerLoc = ptrLoc + ptrVal -1;
            m49ersNumPlayersAddress = 0x42e4; // 35
            mMaxPlayers = 1943;

            Year = 0; //?? does a year apply to a roster at all???
            AutoPlayerStartLocation();
        }

        private int GetCoachPointer(int teamIndex)
        {
            int retVal = m49ersPlayerPointersStart + mCoachPointerOffset + teamIndex * cTeamDiff;
            return retVal;
        }

        public byte[] GetCoachBytes(int teamIndex)
        {
            List<Byte> coachBytes = new List<byte>();
            if (teamIndex < 32)
            {
                int coachPointer = GetCoachPointer(teamIndex);
                int coach_ptr = GetPointerDestination(coachPointer);
                int end = coach_ptr + ((int)CoachOffsets.EmptyPass) + 1;
                for (int i = coach_ptr; i < end; i++)
                {
                    coachBytes.Add(GameSaveData[i]);
                }
            }
            return coachBytes.ToArray();
        }

        /// <summary>
        /// All the coach attributes.
        /// </summary>
        public string CoachKeyAll { get { return mCoachKeyAll; } }

        private string mCoachKeyAll = 
            "Coach,Team,FirstName,LastName,Info1,Info2,Info3,Body,Photo,Wins,Losses,Ties,SeasonsWithTeam,totalSeasons,WinningSeasons,SuperBowls,SuperBowlWins,SuperBowlLosses,PlayoffWins,"+
            "PlayoffLosses,Overall,OvrallOffense,RushFor,PassFor,OverallDefense,PassRush,PassCoverage,QB,RB,TE,WR,OL,DL,LB,SpecialTeams,Professionalism,Preparation,"+
            "Conditioning,Motivation,Leadership,Discipline,Respect,PlaycallingRun,ShotgunRun,IFormRun,SplitbackRun,EmptyRun,ShotgunPass,SplitbackPass,IFormPass,LoneBackPass,EmptyPass";

        public const string DefaultCoachKey = "Coach,Team,FirstName,LastName,Body,Photo";
        private string mCoachKey = DefaultCoachKey;

        /// <summary>
        /// Coach attribute key
        /// </summary>
        public string CoachKey
        {
            get { return mCoachKey; }
            set
            {
                string lastAttr = "";
                try
                {
                    string[] parts = value.Split(",".ToCharArray());
                    foreach (string part in parts)
                    {
                        if (part.Length > 0 && !part.Equals("Team", StringComparison.InvariantCultureIgnoreCase) && !part.Equals("Coach", StringComparison.InvariantCultureIgnoreCase))
                        {
                            lastAttr = part;
                            Enum.Parse(typeof(CoachOffsets), part, true); // throws exception on invalid part.
                        }
                    }
                    mCoachKey = value;
                }
                catch (Exception )
                {
                    StaticUtils.AddError(String.Format("Error setting CoachKey part='{0}' in '{1}'", lastAttr, value));
                }
            }
        }

        public void SetCoachAttribute(int teamIndex, CoachOffsets attr, string value)
        {
            if (teamIndex < 32)
            {
                int coachPointer = GetCoachPointer(teamIndex);
                int coach_loc = GetPointerDestination(coachPointer);
                int loc = coach_loc + (int)attr;
                int val, v1, v2;
                string strVal;

                switch (attr)
                {
                    case CoachOffsets.FirstName:
                    case CoachOffsets.LastName:
                    case CoachOffsets.Info1:
                    case CoachOffsets.Info2:
                        //string_ptr = coach_ptr + (int)attr;
                        SetCoachString(value.Replace("\"", ""), loc);
                        //if (attr == CoachOffsets.Info2) // clear Info3
                        //{
                        //    int coachStringEnd = GetPointerDestination(GetPointerDestination(GetCoachPointer(0))) + mCoachStringSectionLength;
                        //    loc = coach_loc + (int)CoachOffsets.Info3;
                        //    byte b1 = (byte)(coachStringEnd & 0xff);
                        //    byte b2 = (byte)((coachStringEnd >> 8) & 0xff);
                        //    byte b3 = (byte)((coachStringEnd >> 16) & 0xff);
                        //    SetByte(loc, (byte)b1);
                        //    SetByte(loc + 1, (byte)b2);
                        //    SetByte(loc + 2, (byte)b3);
                        //    //SetByte(loc + 3, (byte)0);
                        //}
                        break;
                    case CoachOffsets.Photo:
                        val = Int32.Parse(value);
                        v1 = val & 0xff;
                        v2 = val >> 8;
                        SetByte(loc, (byte)v1);
                        SetByte(loc + 1, (byte)v2);
                        break;
                    case CoachOffsets.Body:
                        strVal = value.Replace("[", "").Replace("]", "");
                        if (mCoachMap.ContainsKey(strVal))
                            SetByte(loc, (byte)mCoachMap[strVal]);
                        else
                            StaticUtils.AddError(String.Format("Error Setting Body '{0}' value for {1} Coach ",
                                value, mTeamsDataOrder[teamIndex]));
                        break;
                    default:
                        v1 = Int32.Parse(value);
                        SetByte(loc, (byte)v1);
                        break;
                }
            }
        }

        public string GetCoachAttribute(int teamIndex, CoachOffsets attr)
        {
            string retVal = "!!!!Invalid!!!!";
            if (teamIndex < 32)
            {
                int coachPointer = GetCoachPointer(teamIndex);
                int coach_ptr = GetPointerDestination(coachPointer);
                int str_ptr = 0;
                int loc = coach_ptr + (int)attr;
                switch (attr)
                {
                    case CoachOffsets.FirstName:
                    case CoachOffsets.LastName:
                    case CoachOffsets.Info1:
                    case CoachOffsets.Info2:
                    case CoachOffsets.Info3:
                        str_ptr = coach_ptr + (int)attr;
                        retVal = GetName(str_ptr);
                        break;
                    case CoachOffsets.Body:
                        int body_ptr = coach_ptr + (int)CoachOffsets.Body;
                        int bodyNumber = GameSaveData[body_ptr];
                        retVal = mReverseCoachMap[bodyNumber];
                        break;
                    case CoachOffsets.Photo:
                        int val = GameSaveData[loc + 1] << 8;
                        val += GameSaveData[loc];
                        retVal = "" + val;
                        switch (retVal.Length)
                        {
                            case 3: retVal = "0" + retVal; break;
                            case 2: retVal = "00" + retVal; break;
                            case 1: retVal = "000" + retVal; break;
                        }
                        break;
                    default:
                        retVal = "" + GameSaveData[loc];
                        break;
                }
            }
            if (retVal.IndexOf(',') > -1)
            {
                retVal = "\"" + retVal + "\"";
            }
            return retVal;
        }

        /// <summary>
        /// FirstName,LastName,Photo,Body
        /// </summary>
        /// <param name="teamIndex"></param>
        /// <returns></returns>
        public string GetCoachData(int teamIndex)
        {
            string retVal = "!!!!!!!!INVALID!!!!!!!!!!!!";
            if (teamIndex < 32)
            {
                StringBuilder builder = new StringBuilder("Coach,");
                builder.Append(mTeamsDataOrder[teamIndex]);
                builder.Append(",");
                string key = CoachKey.ToLower().Replace("fname", "FirstName").Replace("lname", "LastName");
                string[] parts = key.Split(",".ToCharArray());
                CoachOffsets attr = CoachOffsets.FirstName;
                foreach (string part in parts)
                {
                    if ("Coach,Team".IndexOf(part, StringComparison.InvariantCultureIgnoreCase) == -1)
                    {
                        attr = (CoachOffsets)Enum.Parse(typeof(CoachOffsets), part, true);
                        if ("Body".Equals(part, StringComparison.InvariantCultureIgnoreCase))
                        {
                            builder.Append("[");
                            builder.Append(GetCoachAttribute(teamIndex, attr));
                            builder.Append("],");
                        }
                        else
                        {
                            builder.Append(GetCoachAttribute(teamIndex, attr));
                            builder.Append(",");
                        }
                    }
                }
                builder.Remove(builder.Length - 1, 1);// remove last comma

                retVal = builder.ToString();
            }
            return retVal;
        }

        public string GetCoachData()
        {
            StringBuilder builder = new StringBuilder(1000);
            builder.Append("\n\nCoachKEY=");
            builder.Append(this.CoachKey);
            builder.Append("\n");
            for (int i = 0; i < 32; i++)
            {
                builder.Append(this.GetCoachData(i));
                builder.Append("\r\n");
            }
            return builder.ToString();
        }

        /// <summary>
        /// In the tool I rely heavily on being able to get a player by his player index;
        /// so I need to know where the players start. this function goes through all teams and 
        /// finds all the players that are pointed to and sets the first player data location (mPlayerStart)
        /// </summary>
        private void AutoPlayerStartLocation()
        {
            //mPlayerStart = 0xaff0; // not always true, needs adjustment
            int firstPlayerLoc = GameSaveData.Length;

            string team = "";
            for (int t = 0; t < 33; t++)
            {
                team = mTeamsDataOrder[t];
                int teamIndex = GetTeamIndex(team);
                int teamPlayerPointersStart = teamIndex * cTeamDiff + m49ersPlayerPointersStart;
                if ("FreeAgents".Equals(team, StringComparison.InvariantCultureIgnoreCase))
                    teamPlayerPointersStart = GetPointerDestination(mFreeAgentPlayersPointer);

                int numPlayers = GetNumPlayers(team);
                int pointerLoc = -1;

                for (int i = 0; i < numPlayers; i++)
                {
                    pointerLoc = teamPlayerPointersStart + (i * 4); // 4== ptr length
                    int ptr = GameSaveData[pointerLoc + 3] << 24;
                    ptr += GameSaveData[pointerLoc + 2] << 16;
                    ptr += GameSaveData[pointerLoc + 1] << 8;
                    ptr += GameSaveData[pointerLoc];
                    int playerLoc = ptr + pointerLoc - 1;
                    if (playerLoc < firstPlayerLoc && ValidPlayer(playerLoc))
                        firstPlayerLoc = playerLoc;
                }
            }
            if (firstPlayerLoc != mPlayerStart)
                mPlayerStart = firstPlayerLoc;
        }


        Regex mInValidPlayerTest = new Regex(",[0-9]{2,3},");
        // I don't think this is necessary, but I'm keeping it for now.
        private bool ValidPlayer(int playerLoc)
        {
            bool retVal = false;
            int prevFirstPlayer = mPlayerStart;
            mPlayerStart = playerLoc;
            try
            {
                StringBuilder builder = new StringBuilder();
                GetPlayerAppearanceAttribute(0, AppearanceAttributes.College, builder);
                builder.Remove(builder.Length - 1, 1);// remove comma
                builder.Replace("\"",""); // remove quotes
                string college = builder.ToString();

                AppearanceAttributes[] attrs = { 
                 AppearanceAttributes.Hand, AppearanceAttributes.BodyType, AppearanceAttributes.Skin, AppearanceAttributes.Face, 
                 AppearanceAttributes.Dreads, AppearanceAttributes.Helmet, AppearanceAttributes.FaceMask, AppearanceAttributes.Visor, 
                 AppearanceAttributes.EyeBlack, AppearanceAttributes.MouthPiece, AppearanceAttributes.LeftGlove, AppearanceAttributes.RightGlove, 
                 AppearanceAttributes.LeftWrist, AppearanceAttributes.RightWrist, AppearanceAttributes.LeftElbow, AppearanceAttributes.RightElbow, 
                 AppearanceAttributes.Sleeves, AppearanceAttributes.LeftShoe, AppearanceAttributes.RightShoe, AppearanceAttributes.NeckRoll, 
                 AppearanceAttributes.Turtleneck
                                           };

                if (Colleges.ContainsKey(college))
                {
                    foreach (AppearanceAttributes attr in attrs)
                    {
                        GetPlayerAppearanceAttribute(0, attr, builder);
                    }
                    string test = builder.ToString();
                    if (mInValidPlayerTest.Match(test) == Match.Empty)
                        retVal = true;
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                mPlayerStart = prevFirstPlayer;
            }
            return retVal;
        }

        private int FirstPlayerFnamePointerLoc { get  { return mPlayerStart + 0x10; } }

		// The team data is ordered like this:
        private string[] mTeamsDataOrder =  {
                 "49ers", "Bears","Bengals", "Bills", "Broncos", "Browns","Buccaneers", "Cardinals", 
                 "Chargers", "Chiefs","Colts","Cowboys",  "Dolphins", "Eagles","Falcons","Giants","Jaguars",
                 "Jets","Lions","Packers", "Panthers", "Patriots","Raiders","Rams","Ravens","Redskins",
                 "Saints","Seahawks","Steelers", "Texans", "Titans",  "Vikings", 
                 
                 // these guys aren't really teams, investigate deleting these 2 from the array
                 "FreeAgents", "DraftClass" 
                 };
        
        //Players are listed in this order in the original 2004 season; No longer used
        //private string[] mTeamsPlayerOrder = {
        //           "Cardinals", "Falcons", "Ravens", "Bills", "Panthers", "Bears", "Bengals", "Cowboys",
        //           "Broncos", "Lions", "Packers", "Colts", "Jaguars", "Chiefs", "Dolphins", "Vikings", 
        //           "Patriots", "Saints", "Giants", "Jets", "Raiders", "Eagles", "Steelers", "Rams",  "Chargers", 
        //           "49ers", "Seahawks", "Buccaneers", "Titans", "Redskins", "Browns", "Texans", 
        //           "FreeAgents", "DraftClass"
        //           };

        /// <summary>
        /// The gamesave file/data
        /// </summary>
        public byte[] GameSaveData = null;

        /// <summary>
        /// the year 
        /// </summary>
        public int Year { get; private set; }

        /// <summary>
        /// Gets the players by team (does not get free agents or draft class)
        /// </summary>
        /// <param name="attributes">true to list out skills</param>
        /// <param name="appearance">true to list out appearance</param>
        /// <param name="specialTeamers"> true to list special teams</param>
        /// <returns></returns>
        public string GetLeaguePlayers(bool attributes, bool appearance, bool specialTeamers)
        {
            StringBuilder builder = new StringBuilder(300 * 55 * 35);
            for (int i = 0; i < 32; i++)
            {
                builder.Append(GetTeamPlayers(mTeamsDataOrder[i], attributes, appearance, specialTeamers));
            }
            return builder.ToString();
        }

        /// <summary>
        /// Get all the players on Draft class.
        /// I could not find pointers for the players, so I'm assuming the draft class is always 380 players for now.
        /// </summary>
        /// <param name="attributes">include skill attributes</param>
        /// <param name="appearance">include appearance attributes.</param>
        /// <returns>string with all the players for the given team.</returns>
        public string GetDraftClass(bool attributes, bool appearance)
        {
            int limit  = FirstDraftClassPlayer +  mDraftClassSize;
            if( mSaveType == SaveType.Roster)
                limit = FirstDraftClassPlayer + 7;

            StringBuilder builder = new StringBuilder(300 * mDraftClassSize + 1);
            builder.Append("\nTeam = ");
            builder.Append("DraftClass");
            builder.Append("    Players:");
            builder.Append(mDraftClassSize);
            builder.Append("\n");

            for (int i = FirstDraftClassPlayer; i < limit; i++)
            {
                builder.Append(GetPlayerData(i, attributes, appearance));
                builder.Append("\n");
            }
            return builder.ToString();
        }

        public byte[] GetTeamBytes(string team)
        {
            int teamIndex = GetTeamIndex(team);
            int teamPlayerPointersStart = teamIndex * cTeamDiff + m49ersPlayerPointersStart;
            int end = teamPlayerPointersStart + cTeamDiff;

            byte[] retVal = new byte[end - teamPlayerPointersStart];
            Array.Copy(GameSaveData, teamPlayerPointersStart, retVal, 0, retVal.Length);
            return retVal;
        }

        /// <summary>
        /// Get all the players on team specified
        /// </summary>
        /// <param name="team"></param>
        /// <param name="attributes">include skill attributes</param>
        /// <param name="appearance">include appearance attributes.</param>
        /// <returns>string with all the players for the given team.</returns>
        public string GetTeamPlayers(string team, bool attributes, bool appearance, bool specialTeams)
        {
            int teamIndex = GetTeamIndex(team);
            int teamPlayerPointersStart = teamIndex * cTeamDiff + m49ersPlayerPointersStart;
            if ("FreeAgents".Equals(team, StringComparison.InvariantCultureIgnoreCase))
                teamPlayerPointersStart = GetPointerDestination(mFreeAgentPlayersPointer);
            else if ("DraftClass".Equals(team, StringComparison.InvariantCultureIgnoreCase))
                return GetDraftClass(attributes, appearance);

            List<int> playerIndexes = GetPlayerIndexesForTeam(team);
            StringBuilder builder = new StringBuilder(300 * playerIndexes.Count + 1);
            builder.Append("\nTeam = ");
            builder.Append(team);
            builder.Append("    Players:");
            builder.Append(playerIndexes.Count);
            builder.Append("\n");

            for (int i = 0; i < playerIndexes.Count; i++)
            {
                builder.Append(GetPlayerData(playerIndexes[i], attributes, appearance));
                builder.Append("\n");
            }
            if (specialTeams)
            {
                builder.Append(GetSpecialTeamDepthChart(team));
                builder.Append("\n");
            }
            return builder.ToString();
        }

        /// <summary>
        /// returns a list of player indexes for the given team
        /// </summary>
        public List<int> GetPlayerIndexesForTeam(string team)
        {
            List<int> retVal = new List<int>(55);
            int numPlayers = 0;
            int teamIndex = GetTeamIndex(team);
            int teamPlayerPointersStart = teamIndex * cTeamDiff + m49ersPlayerPointersStart;
            if ("FreeAgents".Equals(team, StringComparison.InvariantCultureIgnoreCase))
                teamPlayerPointersStart = GetPointerDestination( mFreeAgentPlayersPointer);
            else if ("DraftClass".Equals(team, StringComparison.InvariantCultureIgnoreCase))
            {
                int lastDraftClassPlayer = FirstDraftClassPlayer + mDraftClassSize + 1;
                for (int i = FirstDraftClassPlayer; i < lastDraftClassPlayer; i++)
                    retVal.Add(i);
                
                return retVal;
            }

            numPlayers = GetNumPlayers(team);
            int playerIndex = -1;

            try
            {
                for (int i = 0; i < numPlayers; i++)
                {
                    playerIndex = GetPlayerIndexByPointer(teamPlayerPointersStart + (i * 4)); // 4== ptr length
                    retVal.Add(playerIndex);
                }
            }
            catch (Exception )
            {
                StaticUtils.AddError(String.Concat("Error getting players for:",team," Invalid pointer found. player index=",playerIndex));
            }
            return retVal;
        }

        /// <summary>
        /// reads the pointer to see where it points to, then does math returning the player's index.
        /// </summary>
        /// <param name="pointerLoc">The address of the player pointer</param>
        /// <returns>The index of the player</returns>
        private int GetPlayerIndexByPointer(int pointerLoc)
        {
            int ptr = GameSaveData[pointerLoc + 3] << 24;
            ptr += GameSaveData[pointerLoc + 2] << 16;
            ptr += GameSaveData[pointerLoc + 1] << 8;
            ptr += GameSaveData[pointerLoc];
            int playerLoc = ptr + pointerLoc - 1;
            int retVal = (playerLoc - mPlayerStart) / cPlayerDataLength;
            if (retVal > mMaxPlayers)
                throw new Exception("Error! Invalid player index:" + retVal);
            return retVal;
        }

        // debugging method
        public string GetNumberOfPlayersOnAllTeams()
        {
            StringBuilder builder = new StringBuilder();
            foreach (string team in mTeamsDataOrder)
            {
                builder.Append(team);
                builder.Append(" Count = ");
                builder.Append(GetNumPlayers(team));
                builder.Append("\n");
            }
            return builder.ToString();
        }

        /// <summary>
        /// Returns the number of players the team has
        /// </summary>
        public int GetNumPlayers(string team)
        {
            int retVal = 0;
            int index = GetTeamIndex(team);

            int loc = m49ersNumPlayersAddress + index * cTeamDiff;
            if ("FreeAgents".Equals(team, StringComparison.InvariantCultureIgnoreCase))
            {
                loc = mFreeAgentCountLocation;
                retVal = GameSaveData[loc + 1] << 8;
            }
            retVal += GameSaveData[loc];
            return retVal;
        }

        /// <summary>
        /// Gets the team index
        /// </summary>
        /// <param name="team"></param>
        /// <returns></returns>
        public int GetTeamIndex(string team)
        {
            for (int i = 0; i < mTeamsDataOrder.Length; i++)
                if (mTeamsDataOrder[i].Equals(team, StringComparison.OrdinalIgnoreCase))
                    return i;
            return -1;
        }

        /// <summary>
        /// Returns the player's team
        /// </summary>
        public string GetPlayerTeam(int player)
        {
            int playerLocation = GetPlayerDataStart(player);
            List<int> players;
            //GetPointerDestination
            for (int i = 0; i < mTeamsDataOrder.Length -1; i++) // hmm... this isn't going to work for draft class...
            {
                players = GetPlayerIndexesForTeam(mTeamsDataOrder[i]);
                for (int j = 0; j < players.Count; j++)
                {
                    if (players[j] == player)
                        return mTeamsDataOrder[i];
                }
            }
            return "DraftClass";
        }

        /// <summary>
        /// Automatically update the Play by play names
        /// </summary>
        public void AutoUpdatePBP()
        {
            Console.WriteLine("#AutoUpdatePBP");
            string key, firstName, lastName, number, val;
            for (int player = 0; player < MaxPlayers; player++)
            {
                firstName = GetPlayerFirstName(player);
                lastName = GetPlayerLastName(player);
                number = GetAttribute(player, PlayerOffsets.JerseyNumber);
                if (number.Length < 2)
                    number = "#0" + number;
                else
                    number = "#" + number;

                key = lastName + ", " + firstName;
                if (DataMap.PBPMap.ContainsKey(key))
                    val = DataMap.PBPMap[key];
                else if (DataMap.PBPMap.ContainsKey(lastName))
                    val = DataMap.PBPMap[lastName];
                else //if (DataMap.PBPMap.ContainsKey(number))
                    val = DataMap.PBPMap[number];
                SetAttribute(player, PlayerOffsets.PBP, val);
            }
        }

        /// <summary>
        /// Automatically update the Player photos.
        /// </summary>
        public void AutoUpdatePhoto()
        {
            Console.WriteLine("#AutoUpdatePhoto");
            string key, firstName, lastName, number, val;
            for (int player = 0; player < MaxPlayers; player++)
            {
                firstName = GetPlayerFirstName(player);
                lastName = GetPlayerLastName(player);
                number = GetAttribute(player, PlayerOffsets.JerseyNumber);
                if (number.Length < 2)
                    number = "#0" + number;
                else
                    number = "#" + number;

                key = lastName + ", " + firstName;
                if (DataMap.PhotoMap.ContainsKey(key))
                    val = DataMap.PhotoMap[key];
                else //if (DataMap.PhotoMap.ContainsKey(number))
                    val = DataMap.PhotoMap["NoPhoto"];
                SetAttribute(player, PlayerOffsets.Photo, val);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentYear"></param>
        /// <param name="teams">Teams to update</param>
        public void AutoUpdateYearsProFromYear(int currentYear, string[] teams)
        {
            string dob = "";
            string[] parts;
            char[] chars = new char[] {'/'};
            int birthYear = 0;
            int yearsPro = 0;
            List<int> playerPointers = null;

            for (int t = 0; t < teams.Length; t++)
            {
                if ("DraftClass".Equals(teams[t]))
                    continue;
                playerPointers = GetPlayerIndexesForTeam(teams[t]);
                for (int i = 0; i < playerPointers.Count; i++)
                {
                    dob = GetAttribute(playerPointers[i], PlayerOffsets.DOB); // retVal = string.Concat(new object[] { month, "/", day, "/", year });
                    parts = dob.Split(chars);
                    if (parts.Length > 2 && Int32.TryParse(parts[2], out birthYear))
                    {
                        yearsPro = currentYear - (birthYear + 22);
                        if (yearsPro < 0)
                            yearsPro = 0;
                        SetAttribute(playerPointers[i], PlayerOffsets.YearsPro, yearsPro.ToString());
                    }
                }
            }
            if (Array.IndexOf(teams, "DraftClass") > -1)
            {
                // Draft class
                int end = FirstDraftClassPlayer + mDraftClassSize + 1;
                int targetYear = currentYear - 21;
                for (int i = FirstDraftClassPlayer; i < end; i++)
                {
                    dob = GetAttribute(i, PlayerOffsets.DOB);
                    parts = dob.Split(chars);
                    dob = string.Concat(new object[] { parts[0], "/", parts[1], "/", targetYear.ToString() });
                    SetAttribute(i, PlayerOffsets.DOB, dob);
                    SetAttribute(i, PlayerOffsets.YearsPro, "0");
                }
            }
        }

        public string GetDepthCharts()
        {
            StringBuilder sb = new StringBuilder(1000);
            for (int i = 0; i < 32; i++)
            {
                sb.Append(mTeamsDataOrder[i]);
                sb.Append(" DepthChart:\n");
                sb.Append(GetDepthChartForTeam(mTeamsDataOrder[i]));
                sb.Append("\n\n");
            }
            return sb.ToString();
        }

        public string GetDepthChartForTeam(string team)
        {
            // QB = 0, K, P, WR, CB, FS, SS, RB, FB, TE, OLB, ILB, C, G, T, DT, DE
            byte[] positions = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int teamIndex = GetTeamIndex(team);
            int teamPlayerPointersStart = teamIndex * cTeamDiff + m49ersPlayerPointersStart;
            if ("FreeAgents".Equals(team, StringComparison.InvariantCultureIgnoreCase) || "DraftClass".Equals(team, StringComparison.InvariantCultureIgnoreCase))
                return "" ;
            List<int> playerIndexes = GetPlayerIndexesForTeam(team);
            List<string> depthChart = new List<string>(55);

            for (int i = 0; i <playerIndexes.Count; i++)
            {
                depthChart.Add(string.Format("{0}{1},{2}", 
                    GetPlayerPosition(playerIndexes[i]),
                    GetHumanReadablePositionDepth(playerIndexes[i]),
                    GetPlayerName(playerIndexes[i], ',')
                    ));
            }
            depthChart.Sort();
            return String.Join("\n", depthChart.ToArray());
        }

        public int GetHumanReadablePositionDepth(int player)
        {
            int retVal = 0;
            int playerLocation = GetPlayerDataStart(player);
            byte depthVal = GameSaveData[playerLocation + (int)PlayerOffsets.Depth];

            Positions pos = GetPlayerPositionEnum(player);
            switch (pos)
            {
                case Positions.QB:
                case Positions.TE:
                case Positions.C:
                case Positions.FB:
                case Positions.FS:
                case Positions.K:
                case Positions.P:
                case Positions.RB:
                case Positions.SS:
                    retVal = Array.IndexOf(mSinglePosDepthArray, depthVal);
                    break;
                default:
                    retVal = Array.IndexOf(mMultiDepthArray, depthVal);
                    break;
            }
            retVal++;
            return retVal;
        }

        public int GetPlayerPositionDepth(int player)
        {
            int retVal = 0;
            int playerLocation = GetPlayerDataStart(player);
            retVal = GameSaveData[playerLocation + (int)PlayerOffsets.Depth];
            return retVal;
        }

        public void SetPlayerPositionDepth(int player, byte depth)
        {
            int playerLocation = GetPlayerDataStart(player);
            SetByte(playerLocation + (int)PlayerOffsets.Depth, depth);
        }

        /// <summary>
        /// Automatically updates the depth charts for all teams.
        /// </summary>
        public void AutoUpdateDepthChart()
        {
            Console.WriteLine("#AutoUpdateDepthChart");
            for (int i = 0; i < 32; i++)
            {
                AutoUpdateDepthChartForTeam(mTeamsDataOrder[i]);
            }
        }

        // Maybe make this one configurable?
        byte[] mMultiDepthArray = new byte[] { 0x60, 0x10, 0x84, 0x34, 0xA8, 0xCC, 0x58, 0xFC };
        byte[] mSinglePosDepthArray = new byte[] { 0x0, 0x4, 0x8, 0x0C, 0xF }; //not so sure on the last one here

        /// <summary>
        /// Will go through a team and auto assign depth to all the players.
        /// Example:
        /// The first  CB encountered on a roster will be starting on the Right side.
        /// The second CB encountered on a roster will be starting on the Left side.
        /// The third  CB encountered on a roster will be backing up those 2 guys.
        /// The fourth CB encountered on a roster will be backing up those 3 guys.
        /// ...
        /// For positions like QB (where only 1 of that position type plays at a time) 
        /// It just assigns depth in order it encounters the players.
        /// </summary>
        /// <param name="team">The team to update the depth chart for.</param>
        public void AutoUpdateDepthChartForTeam(string team)
        {
            // QB = 0, K, P, WR, CB, FS, SS, RB, FB, TE, OLB, ILB, C, G, T, DT, DE
            byte[] positions = new byte[] {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 };
            int teamIndex = GetTeamIndex(team);
            int teamPlayerPointersStart = teamIndex * cTeamDiff + m49ersPlayerPointersStart;
            if ("FreeAgents".Equals(team, StringComparison.InvariantCultureIgnoreCase) || "DraftClass".Equals(team, StringComparison.InvariantCultureIgnoreCase))
                return;
            string name = "";
            int depth = 0;
            Positions pos;
            List<int> playerIndexes = GetPlayerIndexesForTeam(team);
            for (int i = 0; i < playerIndexes.Count; i++)
            {
                pos = GetPlayerPositionEnum(playerIndexes[i]);
                name = GetPlayerName(playerIndexes[i], ',');
                switch (pos)
                {
                    case Positions.QB:
                    case Positions.TE:
                    case Positions.C:
                    case Positions.FB:
                    case Positions.FS:
                    case Positions.K:
                    case Positions.P:
                    case Positions.RB:
                    case Positions.SS:
                        depth = positions[(int)pos];
                        if (depth > mSinglePosDepthArray.Length - 1)
                            SetPlayerPositionDepth(playerIndexes[i], mSinglePosDepthArray[mSinglePosDepthArray.Length - 1]);
                        else
                            SetPlayerPositionDepth(playerIndexes[i], mSinglePosDepthArray[depth]);
                        break;
                    default:
                        depth = positions[(int)pos];
                        if (depth > mMultiDepthArray.Length - 1)
                            SetPlayerPositionDepth(playerIndexes[i], mMultiDepthArray[mMultiDepthArray.Length - 1]);
                        else
                            SetPlayerPositionDepth(playerIndexes[i], mMultiDepthArray[depth]);
                        break;
                }
                positions[(int)pos]++;
            }
        }

        public void AutoUpdateSpecialteamsDepth()
        {
            string team;
            for(int i = 0; i < 32; i++)
            {
                team = mTeamsDataOrder[i];
                AutoUpdateSpecialTeams(team);
            }
        }

        /// <summary>
        /// Tries to set fastest guy not starting at CB,RB,WR as starting PR & KR, second fastest guy to KR2
        /// and a guy playing Center to Long snapper.
        /// </summary>
        /// <param name="team">The team to set the special teams for.</param>
        private void AutoUpdateSpecialTeams(string team)
        {
            int teamIndex = GetTeamIndex(team);
            int teamPlayerPointersStart = teamIndex * cTeamDiff + m49ersPlayerPointersStart;
            List<int> playerIndexes = GetPlayerIndexesForTeam(team);
            //GetPlayerPositionDepth
            byte fast1 = 0;
            byte fast2 = 0;
            byte center = 0;
            int speedTest1 = 0;
            int speedTest2 = 0;
            string playerPosition = "";

            for(byte i = (byte)(playerIndexes.Count-1) ; i > 0 ; i--)
            {
                playerPosition = GetPlayerPosition(playerIndexes[i]) + ",";
                if ("CB,WR,RB,".IndexOf(playerPosition) > -1)
                {
                    Int32.TryParse(GetAttribute(playerIndexes[i], PlayerOffsets.Speed), out speedTest1);
                    Int32.TryParse(GetAttribute(playerIndexes[fast1], PlayerOffsets.Speed), out speedTest2);
                    if (speedTest1 > speedTest2 && GetPlayerPositionDepth(playerIndexes[i]) > 2)
                    {
                        fast2 = fast1;
                        fast1 = i;
                    }
                    else if (fast2 == 0)
                    {
                        fast2 = i;
                    }
                }
                else if (playerPosition == "C," && center == 0)
                {
                    center = i;
                }
            }
            SetByte(teamPlayerPointersStart + (int)SpecialTeamer.KR1, fast1);
            SetByte(teamPlayerPointersStart + (int)SpecialTeamer.KR2, fast2);
            SetByte(teamPlayerPointersStart + (int)SpecialTeamer.PR, fast1);
            SetByte(teamPlayerPointersStart + (int)SpecialTeamer.LS, center);
        }

        /// <summary>
        /// Returns the given team's special team set like:
        ///         KR1,RB3
        ///         KR2,WR3
        ///         PR,RB3
        ///         LS,C3
        /// </summary>
        /// <param name="team"></param>
        /// <returns></returns>
        public string GetSpecialTeamDepthChart(string team)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(GetSpecialTeamPosition(team, SpecialTeamer.KR1));
            builder.Append("\r\n");
            builder.Append(GetSpecialTeamPosition(team, SpecialTeamer.KR2));
            builder.Append("\r\n");
            builder.Append(GetSpecialTeamPosition(team, SpecialTeamer.PR));
            builder.Append("\r\n");
            builder.Append(GetSpecialTeamPosition(team, SpecialTeamer.LS));
            builder.Append("\r\n");
            return builder.ToString();
        }


        public string GetSpecialTeamPosition(string team, SpecialTeamer guy)
        {
            int teamIndex = GetTeamIndex(team);
            int teamPlayerPointersStart = teamIndex * cTeamDiff + m49ersPlayerPointersStart;
            List<int> playerIndexes = GetPlayerIndexesForTeam(team);
            int playerIndex = GameSaveData[teamPlayerPointersStart + (int)guy];
            
            String pos = "ERROR";
            int depth = 0;
            if (playerIndex < playerIndexes.Count)
            {
                pos = GetPlayerPosition(playerIndexes[playerIndex]);
                for (int i = 0; i <= playerIndex; i++)
                {
                    if (pos == GetPlayerPosition(playerIndexes[i]))
                        depth++;
                }
            }
            string retVal = String.Format("{0},{1}{2}", guy.ToString(), pos.ToString(), depth);
            return retVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="team"></param>
        /// <param name="stPosition"></param>
        /// <param name="pos"></param>
        /// <param name="depth">Depth 1-based (CB1 ==> first CB)</param>
        public void SetSpecialTeamPosition(string team, SpecialTeamer stPosition, Positions pos, int depth)
        {
            int index = -1;
            int teamIndex = GetTeamIndex(team);
            int teamPlayerPointersStart = teamIndex * cTeamDiff + m49ersPlayerPointersStart;
            List<int> playerIndexes = GetPlayerIndexesForTeam(team);
            int testDepth = 0;
            string position = pos.ToString();
            for (int i = 0; i < playerIndexes.Count; i++)
            {
                if (GetPlayerPosition(playerIndexes[i]) == position)
                {
                    testDepth++;
                    if (testDepth == depth)
                    {
                        index = i;
                        break;
                    }
                }
            }

            if (index > -1)
            {
                SetByte(teamPlayerPointersStart + (int)stPosition, (byte)index);
            }
            else
            {
                throw new InvalidOperationException(string.Format("Depth {0} at position {1} does not exist", depth, pos.ToString()));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="team"></param>
        /// <param name="stPosition"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        public void SetSpecialTeamPosition(string team, SpecialTeamer stPosition, string firstName, string lastName)
        {
            string theName = firstName + " " + lastName;
            int index = -1;
            int teamIndex = GetTeamIndex(team);
            int teamPlayerPointersStart = teamIndex * cTeamDiff + m49ersPlayerPointersStart;
            List<int> playerIndexes = GetPlayerIndexesForTeam(team);
            for (int i = 0; i < playerIndexes.Count; i++)
            {
                if (theName == GetPlayerName(playerIndexes[i], ' '))
                {
                    index = i;
                    break;
                }
            }
            if (index > -1)
            {
                SetByte(teamPlayerPointersStart + (int)stPosition, (byte)index);
            }
            else
            {
                throw new InvalidOperationException("Error setting special teamer! '"+ theName + "' is not on the team!");
            }
        }

        /// <summary>
        /// Loads the gamesave fle
        /// TODO: error checking; 
        /// </summary>
        /// <param name="fileName">the filename to load</param>
        public bool LoadSaveFile(string fileName)
        {
            bool retVal = false;
            if (File.Exists(fileName))
            {
                if (fileName.EndsWith(".dat", StringComparison.InvariantCultureIgnoreCase))
                {
                    mZipFile = "";
                    mPS2SaveFile = "";
                    GameSaveData = File.ReadAllBytes(fileName);
                }
                else if (fileName.EndsWith(".zip", StringComparison.InvariantCultureIgnoreCase))
                {
                    mPS2SaveFile = "";
                    GameSaveData = StaticUtils.ExtractFileFromZip(fileName, null, "SAVEGAME.DAT");
                    mZipFile = fileName;
                }
                else if (fileName.EndsWith(".max", StringComparison.InvariantCultureIgnoreCase))
                {
                    mZipFile = "";
                    GameSaveData = StaticUtils.ExtractFileFromPS2Save(fileName);
                    mPS2SaveFile = fileName;
                }
                else
                {
                    return false;
                }
                mColleges.Clear();
                if (GameSaveData[0] == (byte)'R' && GameSaveData[1] == (byte)'O' &&
                    GameSaveData[2] == (byte)'S' && GameSaveData[3] == (byte)'T')
                    InitializeForRoster();
                else
                    InitializeForFranchise();

                retVal = true;
            }
            return retVal;
        }

        public void SaveFile(string fileName)
        {
            FileInfo fi = new FileInfo(fileName);
            if (File.Exists(fileName)&& fi.IsReadOnly)
            {
                StaticUtils.AddError( String.Format("File: '{0}' is Read only", fileName) );
            }
            else if (fileName.EndsWith(".dat", StringComparison.InvariantCultureIgnoreCase))
            {
                if (File.Exists(fileName))
                    File.Delete(fileName);
                File.WriteAllBytes(fileName, GameSaveData);

                //Attempt to sign 'EXTRA' file in the same directory as the save file
                int index = fileName.LastIndexOf('\\') +1;
                if (index > 0)
                {
                    string extraFile = fileName.Substring(0, index) + "EXTRA";
                    if (File.Exists(extraFile))
                    {
                        string tmpFile = Path.GetTempFileName();
                        StaticUtils.SignNfl2K5Save(tmpFile, GameSaveData);
                        File.Copy(tmpFile, extraFile, true);
                        File.Delete(tmpFile);
                    }
                }
                Console.WriteLine("# Data successfully written to file: {0}.", fileName);
            }
            else if (fileName.EndsWith(".zip", StringComparison.InvariantCultureIgnoreCase) && mZipFile.Length > 4)
            {
                if( mZipFile != fileName)
                    File.Copy(mZipFile, fileName, true);
                string tmpFile = Path.GetTempFileName();
                File.WriteAllBytes(tmpFile, GameSaveData);
                StaticUtils.ReplaceFileInArchive(fileName, null, "SAVEGAME.DAT", tmpFile);
                File.Delete(tmpFile);
                
                tmpFile = Path.GetTempFileName();
                StaticUtils.SignNfl2K5Save(tmpFile, GameSaveData);
                StaticUtils.ReplaceFileInArchive(fileName, null, "EXTRA", tmpFile);
                File.Delete(tmpFile);
            }
            else if (fileName.EndsWith(".max", StringComparison.InvariantCultureIgnoreCase) )
            {
                if (mPS2SaveFile != fileName)
                    File.Copy(mPS2SaveFile, fileName, true);
                PS2FileHelper helper = null;
                
                helper = new PS2FileHelper(fileName);
                string tmpFile = Path.GetTempFileName();
                File.WriteAllBytes(tmpFile, GameSaveData);
                helper.ReplaceFile(helper.MainSaveFileName, tmpFile);

                if (helper.HasFile("EXTRA"))
                {
                    tmpFile = Path.GetTempFileName();
                    StaticUtils.SignNfl2K5Save(tmpFile, GameSaveData);
                    helper.ReplaceFile("EXTRA", tmpFile);
                }
                helper.SaveMaxFileAs(fileName);
                helper.Dispose();
                File.Delete(tmpFile);
            }
            else
            {
                StaticUtils.AddError("Error! Need to specify a .zip, .max or .DAT file name. If specifying a zip file, the original file loaded must have come from a zip.");
            }
        }

        /// <summary>
        /// Sets the year in the gamesave. TODO: implement this.
        /// </summary>
        /// <param name="year"></param>
        public void SetYear(string year)
        {
            try
            {
                this.Year = Int32.Parse(year);
            }
            catch
            {
                StaticUtils.AddError("Error Setting year to:"+ year);
            }
        }

        /// <summary>
        /// uses the Schedule helper class to apply the schedule.
        /// </summary>
        /// <param name="scheduleList">list of games to apply.</param>
        public void ApplySchedule(List<string> scheduleList)
        {
            SchedulerHelper helper = new SchedulerHelper(this);
            helper.FranchiseScheduleMode = true;
            helper.ApplySchedule(scheduleList);
        }

        /// <summary>
        /// Gets the schedule
        /// </summary>
        public string GetSchedule()
        {
            SchedulerHelper helper = new SchedulerHelper(this);
            helper.FranchiseScheduleMode = true;
            return helper.GetSchedule();
        }

        /// <summary>
        /// Can be useful for debugging when you want to check why you're writing at a specific point in the file.
        /// </summary>
        /// <param name="location"></param>
        /// <param name="b"></param>
        public void SetByte(int loc, byte b)
        {
            GameSaveData[loc] = b;
        }

        private int[] mOrder; // order of attributes

        /// <summary>
        /// the order of the Attributes
        /// </summary>
        public int[] Order { get { return mOrder; } }

        public int MaxPlayers { get { return mMaxPlayers; } }

        private string mCustomKey = null;
        /// <summary>
        /// The attributes key
        /// </summary>
        public string GetKey(bool attributes, bool appearance)
        {
            string retVal = "";
            if (!string.IsNullOrEmpty(mCustomKey))
                retVal = mCustomKey;
            else 
                retVal = GetDefaultKey(attributes, appearance);

            if (retVal[0] != '#')
                retVal = "#" + retVal;
            return retVal;
        }

        public string GetDefaultKey(bool attributes, bool appearance)
        {
            StringBuilder builder = new StringBuilder(350);
            StringBuilder dummy = new StringBuilder(200);
            int prevLength = 0;
            builder.Append("#Position,fname,lname,JerseyNumber,");
            int size = 4;
            if (attributes)
                size += mAttributeOrder.Length;
            if (appearance)
                size += mAppearanceOrder.Length;
            mOrder = new int[size];
            mOrder[0] = (int)PlayerOffsets.Position;
            mOrder[1] = -1;
            mOrder[2] = -2;
            mOrder[3] = (int)PlayerOffsets.JerseyNumber;
            int i = 4;
            if (attributes)
            {
                foreach (PlayerOffsets attr in mAttributeOrder)
                {
                    builder.Append(attr.ToString());
                    builder.Append(",");
                    mOrder[i++] = (int)attr;
                }
            }
            if (appearance)
            {
                foreach (AppearanceAttributes app in mAppearanceOrder)
                {
                    prevLength = dummy.Length;
                    GetPlayerAppearanceAttribute(0, app, dummy);
                    mOrder[i++] = (int)app;
                    if (dummy.Length > prevLength)
                    {
                        builder.Append(app.ToString());
                        builder.Append(",");
                    }
                }
            }
            return builder.ToString();
        }

        public void SetKey(string line)
        {
            if ( !string.IsNullOrEmpty(line) && line.StartsWith("KEY=", StringComparison.InvariantCultureIgnoreCase))
                line = line.Substring(4);
            line = line.TrimEnd(",".ToCharArray());
            mCustomKey = line;
            int tmp = 0;
            if (!string.IsNullOrEmpty(mCustomKey))
            {
                string[] parts = mCustomKey.Split(",".ToCharArray());
                mOrder = new int[parts.Length];
                for (int i = 0; i < parts.Length; i++)
                {
                    tmp = GetAttributeValue(parts[i]);
                    if (tmp == Int32.MinValue)
                    {
                        throw new Exception("Error Setting Key");
                    }
                    mOrder[i] = tmp;                    
                }
            }
        }

        // returns Int32.MinValue on error.
        private int GetAttributeValue(string a)
        {
            if (a == "fname") return -1;
            if (a == "lname") return -2;
            try
            {
                AppearanceAttributes aa = (AppearanceAttributes)Enum.Parse(typeof(AppearanceAttributes), a);
                return (int)aa;
            }
            catch { }
            try
            {
                PlayerOffsets po = (PlayerOffsets)Enum.Parse(typeof(PlayerOffsets), a);
                return (int)po;
            }
            catch
            {
                StaticUtils.AddError("Attribute '" + a + "' is invalid");
            }
            return Int32.MinValue;
        }

        /// <summary>
        /// Default Attribute order: 
        /// #fname,lname,position,number,Speed,Agility,Strength,Jumping,Coverage,PassRush,RunCoverage,PassBlocking,RunBlocking,Catch,RunRoute,
        /// BreakTackle,HoldOnToBall,PowerRunStyle,PassAccuracy,PassArmStrength,PassReadCoverage,Tackle,KickPower,KickAccuracy,Stamana,Durability,
        /// Leadership,Scramble,Composure,Consistency,Aggressiveness,
        /// 
        /// Default appearance order:
        /// College,DOB,Hand,Weight,Height,BodyType,Skin,Face,Dreads,Helmet,FaceMask,FaceShield,EyeBlack,MouthPiece,LeftGlove,RightGlove,
        /// LeftWrist,RightWrist,LeftElbow,RightElbow,Sleeves,LeftShoe,RightShoe,NeckRoll,Turtleneck
        /// </summary>
        /// <param name="player"></param>
        /// <param name="attributes"></param>
        /// <param name="appearance"></param>
        /// <returns></returns>
        public string GetPlayerData(int player, bool attributes, bool appearance)
        {
            StringBuilder builder = new StringBuilder(300);
            int attr=0;
            if (mOrder == null || mOrder.Length < 1)
                GetKey(attributes, appearance);
            for (int i = 0; i < mOrder.Length; i++)
            {
                attr = mOrder[i];
                if (attr == -1)
                    builder.Append(GetPlayerFirstName(player));
                else if (attr == -2)
                    builder.Append(GetPlayerLastName(player));
                else if (attr >= (int)AppearanceAttributes.College)
                    GetPlayerAppearanceAttribute(player, (AppearanceAttributes)attr, builder);
                else
                    builder.Append(GetAttribute(player, (PlayerOffsets)attr));
                if( builder[builder.Length -1] != ',')
                    builder.Append(",");
            }
            return builder.ToString();
        }

        /// <summary>
        /// Get the location where the player's data starts.
        /// </summary>
        public int GetPlayerDataStart(int player)
        {
            int ret = -1;
            if (player <= mMaxPlayers)
                ret = mPlayerStart + player * cPlayerDataLength;
            return ret;
        }

        private void GetPlayerAppearance(int player, StringBuilder builder)
        {
            foreach (AppearanceAttributes attr in this.mAppearanceOrder)
            {
                GetPlayerAppearanceAttribute(player, attr, builder);
            }
        }

        public void SetPlayerAppearanceAttribute(int player, AppearanceAttributes attr, string strVal)
        {
            switch (attr)
            {
                case AppearanceAttributes.BodyType:
                    SetBody(player, strVal); break;
                case AppearanceAttributes.Dreads:
                    SetDreads(player, strVal); break;
                case AppearanceAttributes.EyeBlack:
                    SetEyeblack(player, strVal); break;
                case AppearanceAttributes.Hand:
                    SetHand(player, strVal); break;
                case AppearanceAttributes.Turtleneck:
                    SetTurtleneck(player, strVal); break;
                case AppearanceAttributes.Face:
                    SetFace(player, strVal); break;
                case AppearanceAttributes.FaceMask:
                    SetFaceMask(player, strVal); break;
                case AppearanceAttributes.Visor:
                    SetVisor(player, strVal); break;
                case AppearanceAttributes.Skin:
                    SetSkin(player, strVal); break;
                case AppearanceAttributes.DOB:
                    SetAttribute(player, PlayerOffsets.DOB, strVal); break;
                case AppearanceAttributes.Helmet:
                    SetHelmet(player, strVal); break;
                case AppearanceAttributes.RightShoe:
                    SetRightShoe(player, strVal); break;
                case AppearanceAttributes.LeftShoe:
                    SetLeftShoe(player, strVal); break;
                case AppearanceAttributes.LeftGlove:
                    SetLeftGlove(player, strVal); break;
                case AppearanceAttributes.MouthPiece:
                    SetMouthPiece(player, strVal); break;
                case AppearanceAttributes.Sleeves:
                    SetSleeves(player, strVal); break;
                case AppearanceAttributes.NeckRoll:
                    SetNeckRoll(player, strVal); break;
                case AppearanceAttributes.RightGlove:
                    SetRightGlove(player, strVal); break;
                case AppearanceAttributes.LeftWrist:
                    SetLeftWrist(player, strVal); break;
                case AppearanceAttributes.RightWrist:
                    SetRightWrist(player, strVal); break;
                case AppearanceAttributes.LeftElbow:
                    SetLeftElbow(player, strVal); break;
                case AppearanceAttributes.Weight:
                    SetAttribute(player, PlayerOffsets.Weight, strVal); break;
                case AppearanceAttributes.Height:
                    SetAttribute(player, PlayerOffsets.Height, strVal); break;
                case AppearanceAttributes.RightElbow:
                    SetRightElbow(player, strVal); break;
                case AppearanceAttributes.College:
                    SetCollege(player, strVal); break;
                case AppearanceAttributes.YearsPro:
                    SetAttribute(player, PlayerOffsets.YearsPro, strVal); break;
                case AppearanceAttributes.Photo:
                    SetAttribute(player, PlayerOffsets.Photo, strVal); break;
                case AppearanceAttributes.PBP:
                    SetAttribute(player, PlayerOffsets.PBP, strVal); break;
            }
        }

        private void GetPlayerAppearanceAttribute(int player, AppearanceAttributes attr, StringBuilder builder)
        {
            switch (attr)
            {
                case AppearanceAttributes.BodyType:
                    GetBody(player, builder);break;
                case AppearanceAttributes.Dreads:
                    GetDreads(player, builder);break;
                case AppearanceAttributes.EyeBlack:
                    GetEyeblack(player, builder);break;
                case AppearanceAttributes.Hand:
                    GetHand(player, builder);break;
                case AppearanceAttributes.Turtleneck:
                    GetTurtleneck(player, builder); break;
                case AppearanceAttributes.Face:
                    GetFace(player, builder); break;
                case AppearanceAttributes.FaceMask:
                    GetFaceMask(player, builder); break;
                case AppearanceAttributes.Visor:
                    GetVisor(player, builder); break;
                case AppearanceAttributes.Skin:
                    GetSkin(player, builder); break;
                case AppearanceAttributes.DOB:
                    builder.Append(GetAttribute(player, PlayerOffsets.DOB)); 
                    builder.Append(","); 
                    break;
                case AppearanceAttributes.Helmet:
                    GetHelmet(player, builder); break;
                case AppearanceAttributes.RightShoe:
                    GetRightShoe(player, builder); break;
                case AppearanceAttributes.LeftShoe:
                    GetLeftShoe(player, builder); break;
                case AppearanceAttributes.LeftGlove:
                    GetLeftGlove(player, builder); break;
                case AppearanceAttributes.MouthPiece:
                    GetMouthPiece(player, builder); break;
                case AppearanceAttributes.Sleeves:
                    GetSleeves(player, builder); break;
                case AppearanceAttributes.NeckRoll:
                    GetNeckRoll(player, builder); break;
                case AppearanceAttributes.RightGlove:
                    GetRightGlove(player, builder); break;
                case AppearanceAttributes.LeftWrist:
                    GetLeftWrist(player, builder); break;
                case AppearanceAttributes.RightWrist:
                    GetRightWrist(player, builder); break;
                case AppearanceAttributes.LeftElbow:
                    GetLeftElbow(player, builder); break;
                case AppearanceAttributes.Weight:
                    builder.Append(GetAttribute(player, PlayerOffsets.Weight));
                    builder.Append(","); 
                    break;
                case AppearanceAttributes.Height:
                    builder.Append(GetAttribute(player, PlayerOffsets.Height));
                    builder.Append(",");
                    break;
                case AppearanceAttributes.RightElbow:
                    GetRightElbow(player, builder); break;
                case AppearanceAttributes.College:
                    builder.Append(GetCollege(player));
                    builder.Append(",");
                    break;
                case AppearanceAttributes.YearsPro:
                    builder.Append(GetAttribute(player, PlayerOffsets.YearsPro));
                    builder.Append(",");
                    break;
                case AppearanceAttributes.Photo:
                    builder.Append(GetAttribute(player, PlayerOffsets.Photo));
                    builder.Append(",");
                    break;
                case AppearanceAttributes.PBP:
                    builder.Append(GetAttribute(player, PlayerOffsets.PBP));
                    builder.Append(",");
                    break;
            }
        }

        private AppearanceAttributes[] mAppearanceOrder = new AppearanceAttributes[]{
             AppearanceAttributes.College, AppearanceAttributes.DOB,  AppearanceAttributes.PBP, 
             AppearanceAttributes.Photo, AppearanceAttributes.YearsPro, AppearanceAttributes.Hand, 
             AppearanceAttributes.Weight, AppearanceAttributes.Height, AppearanceAttributes.BodyType, 
             AppearanceAttributes.Skin, AppearanceAttributes.Face, AppearanceAttributes.Dreads, 
             AppearanceAttributes.Helmet, AppearanceAttributes.FaceMask, AppearanceAttributes.Visor, 
             AppearanceAttributes.EyeBlack, AppearanceAttributes.MouthPiece, AppearanceAttributes.LeftGlove, 
             AppearanceAttributes.RightGlove, AppearanceAttributes.LeftWrist, AppearanceAttributes.RightWrist,
             AppearanceAttributes.LeftElbow, AppearanceAttributes.RightElbow, AppearanceAttributes.Sleeves,
             AppearanceAttributes.LeftShoe, AppearanceAttributes.RightShoe, AppearanceAttributes.NeckRoll, 
             AppearanceAttributes.Turtleneck
        };

        private PlayerOffsets[] mAttributeOrder = new PlayerOffsets[] { 
                        PlayerOffsets.Speed,           PlayerOffsets.Agility,          PlayerOffsets.Strength,     PlayerOffsets.Jumping,       PlayerOffsets.Coverage,
                        PlayerOffsets.PassRush,        PlayerOffsets.RunCoverage,      PlayerOffsets.PassBlocking, PlayerOffsets.RunBlocking,   PlayerOffsets.Catch,
                        PlayerOffsets.RunRoute,        PlayerOffsets.BreakTackle,      PlayerOffsets.HoldOntoBall, PlayerOffsets.PowerRunStyle, PlayerOffsets.PassAccuracy,
                        PlayerOffsets.PassArmStrength, PlayerOffsets.PassReadCoverage, PlayerOffsets.Tackle,       PlayerOffsets.KickPower,     PlayerOffsets.KickAccuracy,
                        PlayerOffsets.Stamina,         PlayerOffsets.Durability,       PlayerOffsets.Leadership,   PlayerOffsets.Scramble,      PlayerOffsets.Composure,
                        PlayerOffsets.Consistency,     PlayerOffsets.Aggressiveness};

        private void GetPlayerAttributes(int player, StringBuilder builder)
        {
            int loc = GetPlayerDataStart(player);
            
            for (int i = 0; i < mAttributeOrder.Length; i++)
            {
                builder.Append(GetAttribute(player, mAttributeOrder[i]));
                builder.Append(',');
            }
        }

        public string GetAttribute(int player, PlayerOffsets attr)
        {
            string retVal = "";
            int loc = GetPlayerDataStart(player) + (int)attr;
            int val = GameSaveData[loc];
            switch (attr)
            {
                case PlayerOffsets.Position:
                    retVal = GetPlayerPosition(player);
                    break;
                case PlayerOffsets.PowerRunStyle:
                    PowerRunStyle style = (PowerRunStyle)val;
                    retVal = style.ToString();
                    break;
                case PlayerOffsets.Face:
                    Face f = (Face)val;
                    retVal = f.ToString();
                    break;
                case PlayerOffsets.JerseyNumber:
                    val = GameSaveData[loc+1] << 5 & 0x60;
                    val += GameSaveData[loc] >> 3 & 0x1f;
                    retVal = val.ToString();
                    break;
                case PlayerOffsets.DOB:
                    int year = (GameSaveData[loc + 2] & 0x0f) << 3;
                    year += GameSaveData[loc + 1] >> 5;
                    int day = GameSaveData[loc+1] & 0x1f;
                    int month = (int)(GameSaveData[loc]  >> 4);
                    if (year > 54)
                        year += 1900;
                    else
                        year += 2000;
                    retVal = string.Concat(new object[] { month, "/", day, "/", year });
                    break;
                case PlayerOffsets.Weight:
                    val += 150;
                    retVal = val.ToString();
                    break;
                case PlayerOffsets.Height:
                    int feet = val / 12;
                    int inches = val % 12;
                    retVal = string.Concat(feet, "\'", inches, "\"");
                    break;
                case PlayerOffsets.College:
                    retVal = GetCollege(player);
                    break;
                case PlayerOffsets.PBP:
                case PlayerOffsets.Photo:
                    val = GameSaveData[loc+1] << 8;
                    val += GameSaveData[loc];
                    retVal = "" + val;
                    switch (retVal.Length)
                    {
                        case 3: retVal = "0"   + retVal; break;
                        case 2: retVal = "00"  + retVal; break;
                        case 1: retVal = "000" + retVal; break;
                    }
                    break;
                default:
                    retVal += val;
                    break;
            }
            return retVal;
        }

        private char[] slash = { '/' };
        Regex mDobRegex = new Regex("([0-9]{1,2})/([0-9]{1,2})/([0-9]{4})");

        public void SetAttribute(int player, PlayerOffsets attr, string stringVal)
        {
            int loc = GetPlayerDataStart(player) + (int)attr;
            int val = 0;
            int v1, v2, v3;
            switch (attr)
            {
                case PlayerOffsets.PowerRunStyle:
                    PowerRunStyle style = (PowerRunStyle)Enum.Parse(typeof(PowerRunStyle), stringVal);
                    SetByte(loc, (byte)style);
                    break;
                case PlayerOffsets.Face:
                    Face f = (Face)Enum.Parse(typeof(Face), stringVal);
                    SetByte(loc, (byte)f);
                    break;
                case PlayerOffsets.JerseyNumber:
                    val = Int32.Parse(stringVal);
                    v1 = GameSaveData[loc] & 7;
                    v2 = GameSaveData[loc + 1] & 0xfc;
                    v2 += val >> 5;
                    v1 += (val & 0x1f ) << 3;
                    SetByte(loc, (byte)v1);
                    SetByte(loc + 1, (byte)v2);
                    break;
                case PlayerOffsets.DOB:
                    Match m = mDobRegex.Match(stringVal);
                    if (m != Match.Empty)
                    {
                        int month = Int32.Parse(m.Groups[1].Value);
                        int day = Int32.Parse(m.Groups[2].Value);
                        int year = Int32.Parse(m.Groups[3].Value);

                        if (year < 1954)
                            year = 1954;
                        else if (year > 2050)
                            year = 2050;

                        if (year > 2000)
                            year -= 2000;
                        else if (year > 1900)
                            year -= 1900;

                        v1 = (GameSaveData[loc] & 0x0f) + (month << 4);
                        v2 = day;
                        v2 += ((year & 7) << 5);
                        v3 = GameSaveData[loc + 2] & 0xf0;
                        v3 += (year >> 3);
                        SetByte(loc, (byte)v1);
                        SetByte(loc + 1, (byte)v2);
                        SetByte(loc + 2, (byte)v3);
                    }
                    else
                    {
                        Console.WriteLine("#Note: DOB format = 'dd/mm/yyyy'");
                        throw new FormatException(String.Format("Error! DOB incorrectly formatted '{0}'", stringVal));
                    }
                    break;
                case PlayerOffsets.Weight:
                    val = Int32.Parse(stringVal);
                    val -= 150;
                    SetByte(loc, (byte)val);
                    break;
                case PlayerOffsets.Height:
                    val = GetInches(stringVal);
                    SetByte(loc, (byte)val);
                    break;
                case PlayerOffsets.College:
                    SetCollege(player, stringVal);
                    break;
                case PlayerOffsets.Position:
                    Positions p = (Positions)Enum.Parse(typeof(Positions), stringVal);
                    SetByte(loc, (byte)p);
                    break;
                case PlayerOffsets.PBP:
                case PlayerOffsets.Photo:
                    val = Int32.Parse(stringVal);
                    v1 = val & 0xff;
                    v2 = val >> 8;
                    SetByte(loc,     (byte)v1);
                    SetByte(loc + 1, (byte)v2);
                    break;
                default:
                    val = Int32.Parse(stringVal);
                    SetByte(loc, (byte)val);
                    break;
            }
        }

        /// <summary>
        /// Set a player's first name.
        /// </summary>
        public bool SetPlayerFirstName(int player, string firstName, bool useExistingName)
        {
            return SetPlayerNameText(player, firstName, false, useExistingName);
        }

        /// <summary>
        /// Set a player's last name
        /// </summary>
        /// <param name="player">the player index</param>
        /// <param name="lastName"></param>
        /// <param name="useExistingName">true to simply update the name pointer to a shared name</param>
        /// <returns>true if successful</returns>
        public bool SetPlayerLastName(int player, string lastName, bool useExistingName)
        {
            return SetPlayerNameText(player, lastName, true, useExistingName);
        }

        /// <summary>
        /// Set's a player's first or last name.
        /// </summary>
        /// <param name="player">the player index</param>
        /// <param name="name">the name to set</param>
        /// <param name="isLastName">true if you want to set the player's last name, otherwise sets his first name</param>
        /// <param name="useExistingName">true to use a shared name.</param>
        /// <returns>true if successful</returns>
        private bool SetPlayerNameText(int player, string name, bool isLastName, bool useExistingName)
        {
            bool retVal = true;
            int ptrLoc1 = player * cPlayerDataLength + FirstPlayerFnamePointerLoc;
            if (isLastName)
                ptrLoc1 += 4;
            if (useExistingName)
            {
                //size = 0xDF41
                //int start = mCollegePlayerNameSectionStart;  // we'll look through the college player names because we're not changing those.
                //int start = mModifiableNameSectionEnd - 0xDF4; // first player's name is here, (I think)
                //List<long> locations = StaticUtils.FindStringInFile(name, GameSaveData, start, mCollegePlayerNameSectionEnd, true);
                List<long> locations = StaticUtils.FindStringInFile(name, GameSaveData, mStringTableStart, mStringTableEnd, true);
                if (locations.Count < 1)
                {
                    retVal = false;
                }
                else
                {
                    int newPtrVal = (int)locations[0] - ptrLoc1 + 1;
                    //lay it down little endian style
                    SetByte(ptrLoc1,   (byte)(0xff & newPtrVal));
                    SetByte(ptrLoc1+1, (byte)(0xff & (newPtrVal >> 8 )));
                    SetByte(ptrLoc1+2, (byte)(0xff & (newPtrVal >> 16)));
                }
            }
            else
                SetName(name, ptrLoc1);
            return retVal;
        }

        private void SetName(string name, int ptrLoc)
        {
            string prevName = GetName(ptrLoc);
            if (prevName != name)
            {
                int diff = 2 * (name.Length - prevName.Length);
                int stringLoc = GetPointerDestination(ptrLoc);

                if (diff > 0)
                    ShiftDataDown(stringLoc, diff, mModifiableNameSectionEnd);
                else if (diff < 0)
                    ShiftDataUp(stringLoc, -1 * diff, mModifiableNameSectionEnd);

                AdjustPlayerNamePointers(stringLoc + 2 * prevName.Length, diff);

                // lay down name
                for (int i = 0; i < name.Length; i++)
                {
                    SetByte(stringLoc, (byte)name[i]);
                    SetByte(stringLoc + 1, 0);
                    stringLoc += 2;
                }
                SetByte(stringLoc, 0); // set null char
                SetByte(stringLoc + 1, 0);
            }
        }
        /// <summary>
        /// checks to see if the gamesave file contains the given name
        /// </summary>
        /// <param name="name">first or last name</param>
        /// <returns></returns>
        public bool CheckNameExists(string name)
        {
            bool retVal = false;
            List<long> locations = StaticUtils.FindStringInFile(name, GameSaveData, mStringTableStart, mStringTableEnd, true);
            if (locations != null && locations.Count > 0)
                retVal = true;
            return retVal;
        }

        /// <summary>
        /// Set Attribute, Name, Appearence
        /// </summary>
        /// <param name="player"></param>
        /// <param name="fieldName">the field to set</param>
        /// <param name="val">the value to set</param>
        /// <returns></returns>
        public bool SetPlayerField(int player, string fieldName, string val)
        {
            bool retVal = false;
            if ("firstName".Equals(fieldName, StringComparison.InvariantCultureIgnoreCase) || "fname".Equals(fieldName, StringComparison.InvariantCultureIgnoreCase))
                retVal = SetPlayerFirstName(player, val, false);
            else if ("lastName".Equals(fieldName, StringComparison.InvariantCultureIgnoreCase) || "lname".Equals(fieldName, StringComparison.InvariantCultureIgnoreCase))
                retVal = SetPlayerLastName(player, val, false);
            else if (Enum.IsDefined(typeof(AppearanceAttributes), fieldName))
            {
                AppearanceAttributes aa = (AppearanceAttributes)Enum.Parse(typeof(AppearanceAttributes), fieldName);
                if (aa != AppearanceAttributes.College)
                    val = val.Replace(" ", ""); // strip spaces
                SetPlayerAppearanceAttribute(player, aa, val);
                retVal = true;
            }
            else if (Enum.IsDefined(typeof(PlayerOffsets), fieldName))
            {
                PlayerOffsets po = (PlayerOffsets)Enum.Parse(typeof(PlayerOffsets), fieldName);
                SetAttribute(player, po, val);
            }
            return retVal;
        }

        /// <summary>
        /// Get a player's field.
        /// </summary>
        /// <param name="player">the player index to get</param>
        /// <param name="fieldName">the name of the field to get</param>
        /// <returns>String representation of the field.</returns>
        public string GetPlayerField(int player, string fieldName)
        {
            string retVal = "";
            if ("firstName".Equals(fieldName, StringComparison.InvariantCultureIgnoreCase) || "fname".Equals(fieldName, StringComparison.InvariantCultureIgnoreCase))
                retVal = GetPlayerFirstName(player);
            else if ("lastName".Equals(fieldName, StringComparison.InvariantCultureIgnoreCase) || "lname".Equals(fieldName, StringComparison.InvariantCultureIgnoreCase))
                retVal = GetPlayerLastName(player);
            else if (Enum.IsDefined(typeof(AppearanceAttributes), fieldName))
            {
                AppearanceAttributes aa = (AppearanceAttributes)Enum.Parse(typeof(AppearanceAttributes), fieldName);
                StringBuilder sb = new StringBuilder(15);
                GetPlayerAppearanceAttribute(player, aa, sb);
                sb.Length -= 1;   // get rid of trailing comma
                retVal = sb.ToString();
            }
            else if (Enum.IsDefined(typeof(PlayerOffsets), fieldName))
            {
                PlayerOffsets po = (PlayerOffsets)Enum.Parse(typeof(PlayerOffsets), fieldName);
                retVal = GetAttribute(player, po);
            }
            return retVal;
        }

        //0x8960f is the end of the name section; although it's not obvious 
        // that the stuff after that section is useful (it's all 2a 00 repeating)
        private void ShiftDataDown(int startIndex, int amount, int dataEnd)
        {
            for (int i = dataEnd - amount; i > startIndex; i--)
            {
                SetByte(i, GameSaveData[i - amount]); // for debugging
                //GameSaveData[i] = GameSaveData[i - amount]; // for speed
            }
        }

        private void ShiftDataUp(int startIndex, int amount, int dataEnd)
        {
            for (int i = startIndex; i < dataEnd; i++)
            {
                SetByte(i, GameSaveData[i + amount]); // for debugging
                //GameSaveData[i] = GameSaveData[i + amount]; // for speed
            }
        }

        /// <summary>
        /// Gets the player's name 
        /// </summary>
        /// <param name="player">an int from 0 to 2317</param>
        public string GetPlayerName(int player, char sepChar)
        {
            string retVal = "!!!!!!!!INVALID!!!!!!!!!!!!";
            if (player > -1 && player <= mMaxPlayers)
            {
                int ptrLoc = player * cPlayerDataLength + FirstPlayerFnamePointerLoc;
                retVal = GetName(ptrLoc) + sepChar + GetName(ptrLoc + 4);
            }
            return retVal;
        }

        /// <summary>
        /// Gets the player's first name 
        /// </summary>
        /// <param name="player">an int from 0 to 2317</param>
        public string GetPlayerFirstName(int player)
        {
            string retVal = "!!!!!!!!INVALID!!!!!!!!!!!!";
            if (player > -1 && player <= mMaxPlayers)
            {
                int ptrLoc = player * cPlayerDataLength + FirstPlayerFnamePointerLoc;
                retVal = GetName(ptrLoc);
            }
            return retVal;
        }

        /// <summary>
        /// Gets the player's last name 
        /// </summary>
        /// <param name="player">an int from 0 to 2317</param>
        public string GetPlayerLastName(int player)
        {
            string retVal = "!!!!!!!!INVALID!!!!!!!!!!!!";
            if (player > -1 && player <= mMaxPlayers)
            {
                int ptrLoc = player * cPlayerDataLength + FirstPlayerFnamePointerLoc;
                retVal = GetName(ptrLoc + 4);
            }
            return retVal;
        }

        /// <summary>
        /// a pointer is 4 bytes:
        /// goes from left to right (least to most significant)
        /// </summary>
        /// <param name="namePointerLoc">The location of the pointer</param>
        /// <returns>the string the pointer points to.</returns>
        public string GetName(int namePointerLoc)
        {
            string retVal = "!!!!!INVALID!!!!!!!!";
            int dataLocation = GetPointerDestination(namePointerLoc);
            retVal = GetString(dataLocation);
            return retVal;
        }

        /// <summary>
        /// Returns the address that the pointer at the given location points to.
        /// </summary>
        public int GetPointerDestination(int pointerLoc)
        {
            int pointer = 0;
            pointer = GameSaveData[pointerLoc + 3] << 24;
            pointer += GameSaveData[pointerLoc + 2] << 16;
            pointer += GameSaveData[pointerLoc + 1] << 8;
            pointer += GameSaveData[pointerLoc];
            int dataLocation = pointerLoc + pointer - 1;
            return dataLocation;
        }

        /// <summary>
        /// Gets the string at the specified location
        /// </summary>
        public string GetString(int loc)
        {
            string retVal = "";
            StringBuilder builder = new StringBuilder();
            for (int i = loc; i < loc + 99; i += 2)
            {
                if (GameSaveData[i] == 0)
                    break;
                builder.Append((char)GameSaveData[i]);
            }

            if (builder.Length > 0)
                retVal = builder.ToString();
            return retVal;
        }

        private void SetCoachString(string name, int ptrLoc)
        {
            string prevName = GetName(ptrLoc);
            if (prevName != name)
            {
                int diff = 2 * (name.Length - prevName.Length);
                int stringLoc = GetPointerDestination(ptrLoc);

                int coachStringEnd = GetPointerDestination(GetPointerDestination(GetCoachPointer(0))) + mCoachStringSectionLength;

                if (diff > 0)
                    ShiftDataDown(stringLoc, diff, coachStringEnd);
                else if (diff < 0)
                    ShiftDataUp(stringLoc, -1 * diff, coachStringEnd);

                AdjustCoachStringPointers(stringLoc + 2 * prevName.Length, diff);

                // lay down name
                for (int i = 0; i < name.Length; i++)
                {
                    SetByte(stringLoc, (byte)name[i]);
                    SetByte(stringLoc + 1, 0);
                    stringLoc += 2;
                }
                SetByte(stringLoc, 0); // set null char
                SetByte(stringLoc + 1, 0);
            }
        }

        /// <summary>
        /// If a pointer points to something before the changed area, leave it alone.
        /// else adjust it.
        /// </summary>
        /// <param name="locationOfChange">The location where the string table changed</param>
        /// <param name="difference">the amount to adjust the pointrs by.</param>
        private void AdjustCoachStringPointers(int locationOfChange, int difference)
        {
            int firstNamePtrLoc = 0;
            int lastNamePtrLoc = 0;
            int info1StringPtrLoc = 0;
            int info2StringPtrLoc = 0;
            int loc = 0;

            // Assumes that the 49ers coach FirstName remained the first name in the section. Not sure if Finn's editor mandates this.
            int coachStringEnd = GetPointerDestination(GetPointerDestination(GetCoachPointer(0))) + mCoachStringSectionLength;
            
            for (int coach = 0; coach < 32; coach++)
            {
                firstNamePtrLoc = GetPointerDestination(GetCoachPointer(coach));// data starts with first name ptr ;)
                lastNamePtrLoc = firstNamePtrLoc + (int)CoachOffsets.LastName;
                info1StringPtrLoc = firstNamePtrLoc + (int)CoachOffsets.Info1;
                info2StringPtrLoc = firstNamePtrLoc + (int)CoachOffsets.Info2;

                loc = GetPointerDestination(firstNamePtrLoc);
                if (loc < coachStringEnd && loc >= locationOfChange)
                    AdjustPointer(firstNamePtrLoc, difference);

                loc = GetPointerDestination(lastNamePtrLoc);
                if (loc < coachStringEnd && loc >= locationOfChange)
                    AdjustPointer(lastNamePtrLoc, difference);

                loc = GetPointerDestination(info1StringPtrLoc);
                if (loc < coachStringEnd && loc >= locationOfChange)
                    AdjustPointer(info1StringPtrLoc, difference);

                loc = GetPointerDestination(info2StringPtrLoc);
                if (loc < coachStringEnd && loc >= locationOfChange)
                    AdjustPointer(info2StringPtrLoc, difference);
            }
        }

        /// <summary>
        /// If a pointer points to something before the changed area, leave it alone.
        /// else adjust it.
        /// </summary>
        /// <param name="locationOfChange">The location where the string table changed</param>
        /// <param name="difference">the amount to adjust the pointrs by.</param>
        private void AdjustPlayerNamePointers(int locationOfChange, int difference)
        {
            int firstNamePtrLoc = 0;
            int lastNamePtrLoc = 0;
            int loc = 0;
            for (int player = 0; player <= mMaxPlayers; player++)
            {
                firstNamePtrLoc = player * cPlayerDataLength + FirstPlayerFnamePointerLoc;
                lastNamePtrLoc = firstNamePtrLoc + 4;

                loc = GetPointerDestination(firstNamePtrLoc);
                if (loc < mModifiableNameSectionEnd && loc >= locationOfChange)
                    AdjustPointer(firstNamePtrLoc, difference);

                loc = GetPointerDestination(lastNamePtrLoc);
                if (loc < mModifiableNameSectionEnd && loc >= locationOfChange)
                    AdjustPointer(lastNamePtrLoc, difference);
            }
        }

        /// <summary>
        /// updates a pointer to the requested change
        /// </summary>
        private void AdjustPointer(int namePointerLoc, int change)
        {
            int pointer = GameSaveData[namePointerLoc + 2] << 16;
            pointer += GameSaveData[namePointerLoc + 1] << 8;
            pointer += GameSaveData[namePointerLoc];

            pointer += change;
            byte b1 = (byte)(pointer & 0xff);
            byte b2 = (byte)((pointer >> 8) & 0xff);
            byte b3 = (byte)((pointer >> 16) & 0xff);

            SetByte(namePointerLoc, b1);
            SetByte(namePointerLoc + 1, b2);
            SetByte(namePointerLoc + 2, b3);
        }


        #region Get/Set attributes

        public byte[] GetPlayerBytes(int player)
        {
            byte[] retVal = new byte[0x54];
            int loc = GetPlayerDataStart(player);

            for(int i=0; i < retVal.Length; i++)
            {
                retVal[i] = GameSaveData[loc+i];
            }
            return retVal;
        }

        public byte[] GetPlayerBytes(string position, string firstName, string lastName )
        {
            List<int> players = FindPlayer(position, firstName, lastName);
            if (players.Count > 0)
            {
                return GetPlayerBytes(players[0]);
            }
            return null;
        }

        // input like 6'3" or 70"
        private int GetInches(string stringVal)
        {
            int inches = 0; // Int32.Parse(stringVal.Substring(2));
            if (stringVal.Length == 3 && stringVal[2] == '"')
            {
                Int32.TryParse(stringVal.Substring(0,2), out inches);
                return inches;
            }
            if (stringVal[0] == '"' && stringVal[stringVal.Length - 1] == '"')
                stringVal = stringVal.Substring(1, stringVal.Length - 3);
            int feet = stringVal[0] - 0x30;
            stringVal = stringVal.Replace("\"", "");
            Int32.TryParse(stringVal.Substring(2), out inches);
            inches += feet * 12;
            return inches;
        }


        private string GetPlayerPosition(int player)
        {
            int loc = GetPlayerDataStart(player);
            loc += (int)PlayerOffsets.Position;
            Positions p = (Positions)GameSaveData[loc];
            return p.ToString();
        }

        private Positions GetPlayerPositionEnum(int player)
        {
            int loc = GetPlayerDataStart(player);
            loc += (int)PlayerOffsets.Position;
            Positions p = (Positions)GameSaveData[loc];
            return p;
        }

        //Face is stored in all but last bit 
        private void GetFaceMask(int player, StringBuilder builder)
        {
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.FaceMask;
            int b = GameSaveData[loc];
            b = (b & 0x7f) >> 2;
            FaceMask ret = (FaceMask)b;
            builder.Append(ret.ToString());
            builder.Append(",");
        }

        /// <summary>
        /// set face for a dude
        /// </summary>
        /// <param name="player">the player (0-2317)</param>
        /// <param name="val">string representation of the face enum</param>
        private void SetFaceMask(int player, String val)
        {
            val = val.Replace("Type", "FaceMask");// Legacy NFL2K5 history format
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.FaceMask;
            FaceMask ret = (FaceMask)Enum.Parse(typeof(FaceMask), val);
            int dude = (int)ret;
            dude = dude << 2;
            int b = GameSaveData[loc];
            b &= 0x83;// clear all but first & last 2 bits
            b += dude;
            SetByte(loc, (byte)b);
        }

        //Face is stored in all but last bit 
        private void GetFace(int player, StringBuilder builder)
        {
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.Face;
            int b = GameSaveData[loc];
            b = b >> 1;
            Face ret = (Face)b;
            builder.Append(ret.ToString());
            builder.Append(",");
        }

        /// <summary>
        /// set face for a dude
        /// </summary>
        /// <param name="player">the player (0-2100+)</param>
        /// <param name="val">string representation of the face enum</param>
        private void SetFace(int player, String val)
        {
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.Face;
            Face ret = (Face)Enum.Parse(typeof(Face), val);
            int dude = (int)ret;
            int b = GameSaveData[loc];
            b &= 0x01; // clear all but last bit
            b += dude <<1;
            SetByte(loc, (byte)b);
        }

        // Turtleneck is stored in bits 6&7 (0-3)
        private void GetTurtleneck(int player, StringBuilder builder)
        {
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.Turtleneck_Body_EyeBlack_Hand_Dreads;
            int b = GameSaveData[loc];
            b &= 0x60;
            b = b >> 5;
            Turtleneck ret = (Turtleneck)b;
            builder.Append(ret.ToString());
            builder.Append(",");
        }

        /// <summary>
        /// set turtleneck for a dude
        /// </summary>
        /// <param name="player">the player (0-2100+)</param>
        /// <param name="val">string repereentation of the Turtleneck enum</param>
        private void SetTurtleneck(int player, String val)
        {
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.Turtleneck_Body_EyeBlack_Hand_Dreads;
            Turtleneck ret = (Turtleneck)Enum.Parse(typeof(Turtleneck), val);
            int dude = (int)ret;
            dude = dude << 5;
            int b = GameSaveData[loc];
            b &= 0x9F; // clear bits 6&7
            b += dude;
            SetByte(loc, (byte)b);
        }

        // Body is stored in bits 4&5 (0-3)
        private void GetBody(int player, StringBuilder builder)
        {
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.Turtleneck_Body_EyeBlack_Hand_Dreads;
            int b = GameSaveData[loc];
            b &= 0x18;
            b = b >> 3;
            Body ret = (Body)b;
            builder.Append(ret.ToString());
            builder.Append(",");
        }

        /// <summary>
        /// set body type for a dude
        /// </summary>
        /// <param name="player">the player (0-2100+)</param>
        /// <param name="val">string repersentation of the body enum</param>
        private void SetBody(int player, String val)
        {
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.Turtleneck_Body_EyeBlack_Hand_Dreads;
            Body ret = (Body)Enum.Parse(typeof(Body), val);
            int dude = (int)ret;
            dude = dude << 3;
            int b = GameSaveData[loc];
            b &= 0xe7; // clear bits 4&5
            b += dude;
            SetByte(loc, (byte)b);
        }

        // eyeblack is stored in bit 3
        private void GetEyeblack(int player, StringBuilder builder)
        {
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.Turtleneck_Body_EyeBlack_Hand_Dreads;
            int b = GameSaveData[loc];
            b &= 0x4;
            b = b >> 2;
            YesNo ret = (YesNo)b;
            builder.Append(ret.ToString());
            builder.Append(",");
        }

        /// <summary>
        /// set eyeblack for a dude
        /// </summary>
        /// <param name="player">the player (0-2100+)</param>
        /// <param name="val">string repersentation of the YesNo enum</param>
        private void SetEyeblack(int player, String val)
        {
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.Turtleneck_Body_EyeBlack_Hand_Dreads;
            YesNo ret = (YesNo)Enum.Parse(typeof(YesNo), val);
            int dude = (int)ret;
            dude = dude << 2;
            int b = GameSaveData[loc];
            b &= 0xfb; // clear bit 3
            b += dude;
            SetByte(loc, (byte)b);
        }

        // Hand is stored in bit 2
        private void GetHand(int player, StringBuilder builder)
        {
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.Turtleneck_Body_EyeBlack_Hand_Dreads;
            int b = GameSaveData[loc];
            b &= 0x2;
            b = b >> 1;
            Hand ret = (Hand)b;
            builder.Append(ret.ToString());
            builder.Append(",");
        }

        /// <summary>
        /// set Hand for a dude
        /// </summary>
        /// <param name="player">the player (0-2100+)</param>
        /// <param name="val">string repersentation of the hand enum</param>
        private void SetHand(int player, String val)
        {
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.Turtleneck_Body_EyeBlack_Hand_Dreads;
            Hand ret = (Hand)Enum.Parse(typeof(Hand), val);
            int dude = (int)ret;
            dude = dude << 1;
            int b = GameSaveData[loc];
            b &= 0xfd; // clear bit 2
            b += dude;
            SetByte(loc, (byte)b);
        }

        // Dreads is stored in bit 1
        private void GetDreads(int player, StringBuilder builder)
        {
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.Turtleneck_Body_EyeBlack_Hand_Dreads;
            int b = GameSaveData[loc];
            b &= 0x1;
            YesNo ret = (YesNo)b;
            builder.Append(ret.ToString());
            builder.Append(",");
        }

        /// <summary>
        /// set Dreads for a dude
        /// </summary>
        /// <param name="player">the player (0-2100+)</param>
        /// <param name="val">string repersentation of the YesNo enum</param>
        private void SetDreads(int player, String val)
        {
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.Turtleneck_Body_EyeBlack_Hand_Dreads;
            YesNo ret = (YesNo)Enum.Parse(typeof(YesNo), val);
            int dude = (int)ret;
            int b = GameSaveData[loc];
            b &= 0xfe; // clear bit 1
            b += dude;
            SetByte(loc, (byte)b);
        }

        /// <summary>
        /// Visor is really weird, it has 3 states (Clear, Dark, None) but is stored across 2 adjacent bytes
        /// </summary>
        /// <param name="player">the player (0-2100+)</param>
        /// <param name="builder"></param>
        private void GetVisor(int player, StringBuilder builder)
        {
            int loc1 = GetPlayerDataStart(player) + (int)PlayerOffsets.FaceMask;
            int loc2 = GetPlayerDataStart(player) + (int)PlayerOffsets.Face;
            int fm = GameSaveData[loc1] & 0x80; // need 1st bit
            int f = GameSaveData[loc2] & 0x01; //need last bit
            Visor ret = Visor.None;
            if (fm > 0)
                ret = Visor.Clear;
            else if (f > 0)
                ret = Visor.Dark;
            builder.Append(ret.ToString());
            builder.Append(",");
        }

        /// <summary>
        /// set visor for a dude; visor is weird
        /// </summary>
        /// <param name="player">the player (0-2100+)</param>
        /// <param name="val">string representation of the face enum</param>
        private void SetVisor(int player, String val)
        {
            int loc1 = GetPlayerDataStart(player) + (int)PlayerOffsets.FaceMask;
            int loc2 = GetPlayerDataStart(player) + (int)PlayerOffsets.Face;
            Visor ret = (Visor)Enum.Parse(typeof(Visor), val);

            int dude = (int)ret;
            int b1 = GameSaveData[loc1] & 0x7f;
            int b2 = GameSaveData[loc2] & 0xfe;

            switch (ret)
            {
                case Visor.Clear:
                    b1 += 0x80;
                    break;
                case Visor.Dark:
                    b2 += 1;
                    break;
            }
            SetByte(loc1, (byte)b1);
            SetByte(loc2, (byte)b2);
        }

        //YaY! More crazy bit splitting
        private void GetSkin(int player, StringBuilder builder)
        {
            int loc1 = GetPlayerDataStart(player) + (int)PlayerOffsets.DOB; //skin is in the same bytes as DOB
            int loc2 = GetPlayerDataStart(player) - 1 + (int)PlayerOffsets.DOB; //skin is in the same bytes as DOB
            int sk = (GameSaveData[loc1] & 0xF) << 1;
            sk += GameSaveData[loc2] >> 7;
            Skin ret = (Skin)sk;
            builder.Append(ret.ToString());
            builder.Append(",");
        }

        /// <summary>
        /// set skin for a dude
        /// I hope I don't have to look at this code in a year...
        /// Why on earth couldn't they just use simple bytes for each to represent everything????!!!!
        /// </summary>
        /// <param name="player">the player (0-2100+)</param>
        /// <param name="val">string representation of the face enum</param>
        private void SetSkin(int player, String val)
        {
            int loc1 = GetPlayerDataStart(player) + (int)PlayerOffsets.DOB; //skin is in the same bytes as DOB
            int loc2 = GetPlayerDataStart(player) - 1 + (int)PlayerOffsets.DOB; 
            Skin sk = (Skin)Enum.Parse(typeof(Skin), val);
            int dude = (int)sk;
            int b1 = GameSaveData[loc1] & 0xf0;
            int b2 = GameSaveData[loc2] & 0x7f;
            b1 += dude >> 1;
            b2 += (dude & 1) << 7;
            SetByte(loc1, (byte)b1);
            SetByte(loc2, (byte)b2);
        }

        private void GetHelmet(int player, StringBuilder builder)
        {
            Helmet retVal = Helmet.Standard;
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.Helmet_LeftShoe_RightShoe;
            // helmet is 2nd bit
            int val = GameSaveData[loc] & 0x40;
            if (val > 0)
                retVal = Helmet.Revolution;
            builder.Append(retVal.ToString());
            builder.Append(",");
        }

        private void SetHelmet(int player, String helmet)
        {
            Helmet h = (Helmet)Enum.Parse(typeof(Helmet), helmet);
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.Helmet_LeftShoe_RightShoe;
            // helmet is 2nd bit
            int val = GameSaveData[loc] & 0xBF;
            val += (int)h << 6;
            SetByte(loc, (byte)val);
        }

        private void GetLeftShoe(int player, StringBuilder builder)
        {
            Shoe retVal = Shoe.Shoe1;
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.Helmet_LeftShoe_RightShoe;
            // LShoe is last 3 bits; helmet is 2nd bit; RShoe is bits 3,4,5 
            int val = GameSaveData[loc] & 0x7;
            retVal = (Shoe)val;
            builder.Append(retVal.ToString());
            builder.Append(",");
        }

        private void SetLeftShoe(int player, String shoe)
        {
            shoe = shoe.Replace("Style", "Shoe");// Legacy NFL2K5 history format
            Shoe h = (Shoe)Enum.Parse(typeof(Shoe), shoe);
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.Helmet_LeftShoe_RightShoe;
            //LShoe is last 3 bits; 
            int val = GameSaveData[loc] & 0xf8;
            val += (int)h;
            SetByte(loc, (byte)val);
        }

        private void GetRightShoe(int player, StringBuilder builder)
        {
            Shoe retVal = Shoe.Shoe1;
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.Helmet_LeftShoe_RightShoe;
            // RShoe is bits 4,5,6  
            int val = GameSaveData[loc] & 0x38;
            val = val >> 3;
            retVal = (Shoe)val;
            builder.Append(retVal.ToString());
            builder.Append(",");
        }

        private void SetRightShoe(int player, String shoe)
        {
            shoe = shoe.Replace("Style", "Shoe");// Legacy NFL2K5 history format
            Shoe h = (Shoe)Enum.Parse(typeof(Shoe), shoe);
            int loc = GetPlayerDataStart(player) + (int) PlayerOffsets.Helmet_LeftShoe_RightShoe;
            // RShoe is bits 4,5,6 
            int val = GameSaveData[loc] & 0xc7;
            val += ((int)h << 3 ) ;
            SetByte(loc, (byte)val);
        }

        private void GetMouthPiece(int player, StringBuilder builder)
        {
            YesNo retVal = YesNo.No; // 3rd bit
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.MouthPiece_LeftGlove_Sleeves_NeckRoll;
            int val = (GameSaveData[loc] & 0x20) >> 5;
            retVal = (YesNo)val;
            builder.Append(retVal.ToString());
            builder.Append(",");
        }

        private void SetMouthPiece(int player, String piece)
        {
            YesNo h = (YesNo)Enum.Parse(typeof(YesNo), piece);
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.MouthPiece_LeftGlove_Sleeves_NeckRoll;
            int val = GameSaveData[loc] & 0xdf;
            val |= ((int)h << 5);
            SetByte(loc, (byte)val);
        }

        private void GetLeftGlove(int player, StringBuilder builder)
        {
            Glove retVal = Glove.None; 
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.MouthPiece_LeftGlove_Sleeves_NeckRoll;
            int val = (GameSaveData[loc] & 0xC0) >> 6;
            val += ((GameSaveData[loc+1] & 0x03) << 2);
            retVal = (Glove)val;
            builder.Append(retVal.ToString());
            builder.Append(",");
        }

        private void SetLeftGlove(int player, String glove)
        {
            Glove g = (Glove)Enum.Parse(typeof(Glove), glove);
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.MouthPiece_LeftGlove_Sleeves_NeckRoll;
            int val1 = GameSaveData[loc] & 0x3f; 
            int val2 = GameSaveData[loc+1] & 0xfc; // most sig 2 bits of glove go to least sig bits in this value
            val1 += ((int)g & 3) << 6;
            val2 += ((int)g >> 2);
            SetByte(loc, (byte)val1);
            SetByte(loc+1, (byte)val2);
        }

        private void GetRightGlove(int player, StringBuilder builder)
        {
            Glove retVal = Glove.None;
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.RightGlove_LeftWrist;
            int val = (GameSaveData[loc] & 0x3c) >> 2;
            retVal = (Glove)val;
            builder.Append(retVal.ToString());
            builder.Append(",");
        }

        private void SetRightGlove(int player, String glove)
        {
            Glove g = (Glove)Enum.Parse(typeof(Glove), glove);
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.RightGlove_LeftWrist;
            int val = GameSaveData[loc];
            val = (val & 0xc3) + ((int)g << 2);
            SetByte(loc, (byte)val);
        }

        private void GetSleeves(int player, StringBuilder builder)
        {
            Sleeves retVal = Sleeves.None;
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.MouthPiece_LeftGlove_Sleeves_NeckRoll;
            int val = (GameSaveData[loc] & 3);
            retVal = (Sleeves)val;
            builder.Append(retVal.ToString());
            builder.Append(",");
        }

        private void SetSleeves(int player, String sleeve)
        {
            Sleeves s = (Sleeves)Enum.Parse(typeof(Sleeves), sleeve);
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.MouthPiece_LeftGlove_Sleeves_NeckRoll;
            int val = GameSaveData[loc] & 0xfc;
            val += (int)s;
            SetByte(loc, (byte)val);
        }

        private void GetNeckRoll(int player, StringBuilder builder)
        {
            NeckRoll retVal = NeckRoll.None;
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.MouthPiece_LeftGlove_Sleeves_NeckRoll;
            int val = ((GameSaveData[loc] & 0x1c) >> 2);
            retVal = (NeckRoll)val;
            builder.Append(retVal.ToString());
            builder.Append(",");
        }

        private void SetNeckRoll(int player, String roll)
        {
            NeckRoll s = (NeckRoll)Enum.Parse(typeof(NeckRoll), roll);
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.MouthPiece_LeftGlove_Sleeves_NeckRoll;
            int val = GameSaveData[loc] & 0xe3;
            val += ((int)s << 2);
            SetByte(loc, (byte)val);
        }

        private void GetLeftWrist(int player, StringBuilder builder)
        {
            Wrist retVal = Wrist.None;
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.RightGlove_LeftWrist;
            int val = ((GameSaveData[loc + 1] << 8) + GameSaveData[loc] )  >> 6;
            val &= 0xf;
            retVal = (Wrist)val;
            builder.Append(retVal.ToString());
            builder.Append(",");
        }

        private void SetLeftWrist(int player, String w)
        {
            Wrist s = (Wrist)Enum.Parse(typeof(Wrist), w);
            int val = (int)s;
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.RightGlove_LeftWrist;
            int b1 = GameSaveData[loc] & 0x3f;
            int b2 = GameSaveData[loc+1] & 0xfc;
            b1 += (0x3 & val) << 6;
            b2 += val >> 2;
            SetByte(loc, (byte)b1);
            SetByte(loc+1, (byte)b2);
        }

        private void GetRightWrist(int player, StringBuilder builder)
        {
            Wrist retVal = Wrist.None;
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.RightWrist_LeftElbow;
            int val = (GameSaveData[loc] & 0x3c) >> 2;
            retVal = (Wrist)val;
            builder.Append(retVal.ToString());
            builder.Append(",");
        }

        private void SetRightWrist(int player, String w)
        {
            Wrist s = (Wrist)Enum.Parse(typeof(Wrist), w);
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.RightWrist_LeftElbow;
            int val = GameSaveData[loc] & 0xc3;
            val += ((int)s << 2);
            SetByte(loc, (byte)val);
        }

        private void GetLeftElbow(int player, StringBuilder builder)
        {
            Elbow retVal = Elbow.None;
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.RightWrist_LeftElbow;
            int val = ((GameSaveData[loc + 1] << 8) + GameSaveData[loc]) & 0x3c0;
            val = val >> 6;
            retVal = (Elbow)val;
            builder.Append(retVal.ToString());
            builder.Append(",");
        }

        private void SetLeftElbow(int player, String w)
        {
            Elbow s = (Elbow)Enum.Parse(typeof(Elbow), w);
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.RightWrist_LeftElbow;
            int val1 = (GameSaveData[loc] & 0x3f) + (((int)s & 3 ) << 6) ;
            int val2 = (GameSaveData[loc+1] & 0xfc) + (((int)s & 0xfc )>>2);

            SetByte(loc, (byte)val1);
            SetByte(loc + 1, (byte)val2);
        }

        private void GetRightElbow(int player, StringBuilder builder)
        {
            Elbow retVal = Elbow.None;
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.RightElbow;
            int val = (GameSaveData[loc] & 0x3c) >> 2;
            retVal = (Elbow)val;
            builder.Append(retVal.ToString());
            builder.Append(",");
        }

        private void SetRightElbow(int player, String w)
        {
            Elbow s = (Elbow)Enum.Parse(typeof(Elbow), w);
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.RightElbow;
            int val = (GameSaveData[loc] & 0xc3 ) + ((int)s << 2);
            SetByte(loc, (byte)val);
        }

        /// <summary>
        /// Get the colleges in the File.
        /// </summary>
        public string[] GetColleges()
        {
            string[] retVal = new string[Colleges.Keys.Count];
            Colleges.Keys.CopyTo(retVal, 0);
            return retVal;
        }

        private Dictionary<string, int> Colleges
        {
            get
            {
                if (mColleges.Count == 0)
                    PopulateColleges();
                return mColleges;
            }
        }

        private Dictionary<string, int> mColleges = new Dictionary<string, int>(3500);

        private void PopulateColleges()
        {
            int loc = GetPlayerDataStart(0);
            int ptrDest = GetPointerDestination(loc); // get pointer to college structure
            int i = ptrDest;
            while (GameSaveData[i] != 0)
                i -= 8; // back up to beginning of colleges (college structure is 8 bytes)
            i += 8;
            string collegeName = "";
            while (true)
            {
                collegeName = GetName(i);
                mColleges.Add(collegeName, i);
                i += 8;
                if (collegeName.IndexOf("Barnum & Bailey") == 0)
                    break;
            }
        }

        public string GetCollege(int player)
        {
            int loc = GetPlayerDataStart(player);
            int pointerDest = GetPointerDestination(loc);
            string retVal = "None";
            try
            {
                retVal = GetName(pointerDest);
                if (retVal.IndexOf(',') > -1)
                    retVal = String.Concat("\"", retVal, "\"");
            }
            catch (Exception )
            {
                Console.WriteLine("Invalid college detected for player {0}, on team {1}; Returning 'None'.",
                    GetPlayerName(player,' '), GetPlayerTeam(player) );
            }
            return retVal;
        }

        private void SetCollege(int player, string college)
        {
            //int dataLocation = pointerLoc + pointer - 1;
            int ptrVal = 0;
            int loc = GetPlayerDataStart(player);
            if( college[0] == '"')
                college = college.Replace("\"", "");

            if (Colleges.ContainsKey(college))
            {
                ptrVal = mColleges[college] - loc + 1;
            }
            else 
            {
                // works, but is very slow; I've seen some saves with more colleges than others
                // should not hit this case very often; but we only populate until we hit Barnum & Bailey.
                // not sure if there is a 'count' for colleges.
                List<long> ptrs = StaticUtils.FindPointersToString(college, GameSaveData, 0, GameSaveData.Length);
                if( ptrs.Count > 0)
                    ptrVal = (int)ptrs[0] - loc + 1;
            }
            if (ptrVal != 0)
            {
                SetByte(loc, (byte)(0xff & ptrVal));
                SetByte(loc + 1, (byte)(ptrVal >> 8));
                SetByte(loc + 2, (byte)(ptrVal >> 16));
                SetByte(loc + 3, (byte)(ptrVal >> 24));
            }
        }
        #endregion

        public void GetSkinPhotoMappings(StringBuilder builder)
        {
            StringBuilder tmp = new StringBuilder();
            string currentSkin;
            for (int j = 1; j < 23; j++)
            {
                currentSkin = "Skin" + j;
                builder.Append(currentSkin);
                builder.Append(":[");
                for (int i = 0; i <= mMaxPlayers; i++)
                {
                    tmp.Length = 0;
                    GetSkin(i, tmp);
                    if (tmp.ToString().IndexOf(currentSkin) > -1)
                    {
                        GetPlayerAppearanceAttribute(i, AppearanceAttributes.Photo, builder);
                    }
                }
                builder.Append("],\r\n");
            }
        }

        /// <summary>
        /// returns a list of ints representing the players with the given firstName, lastName & position
        /// </summary>
        public List<int> FindPlayer(string pos, string firstName, string lastName)
        {
            List<int> retVal = new List<int>();
            for(int i = 0; i < mMaxPlayers;i++)
            {
                if ( pos == null && GetPlayerLastName(i) == lastName && GetPlayerFirstName(i) == firstName )
                    retVal.Add(i);
                else if (GetPlayerLastName(i) == lastName && GetPlayerFirstName(i) == firstName && GetPlayerPosition(i) == pos)
                    retVal.Add(i);
            }
            return retVal;
        }

        #region FormulaRegionStuff


        /// <summary>
        /// Aplies a formulaic change to players.
        /// Formulas are printed to the console when the function is run so that the user can see the 
        /// formula when executed from the Global editor.
        /// </summary>
        /// <param name="formula">The formula used to select players.</param>
        /// <param name="targetAttribute">The attribute you want to set.</param>
        /// <param name="targetValue">The target value you want to set.</param>
        /// <param name="positions">The list of positions to search.</param>
        /// <param name="formulaMode"> The formula mode to use. </param>
        /// <param name="applyChanges">false to not apply the changed (just want to see who's affected)</param>
        /// <returns>
        /// Null if no players are selected
        /// string starting with 'Exception!' if an exception occured
        /// string list of players changed if successful
        /// </returns>
        public string ApplyFormula(string formula, 
                                    string targetAttribute, 
                                    string targetValue, 
                                    List<string> positions, 
                                    FormulaMode formulaMode, 
                                    bool applyChanges )
        {
            string retVal = null;
            try
            {
                if (formulaMode != FormulaMode.Normal)
                {
                    Console.WriteLine("ApplyFormula('{0}','{1}','{2}', [{3}], {4})",
                        formula, targetAttribute, targetValue, String.Join(",", positions.ToArray()),
                        formulaMode
                        );
                }
                else
                {
                    Console.WriteLine("ApplyFormula('{0}','{1}','{2}', [{3}])",
                        formula, targetAttribute, targetValue, String.Join(",", positions.ToArray()));
                }

                if ("Always".Equals(formula, StringComparison.InvariantCultureIgnoreCase))
                    formula = "true";
                List<int> playerIndexes = GetPlayersByFormula(formula, positions);
                string targetValueParam = targetValue; // save a copy for the 'usePercent' case

                if (playerIndexes.Count > 0)
                {
                    String tmp = "";
                    int temp_i = 0;
                    StringBuilder sb = new StringBuilder(30 * playerIndexes.Count);
                    int p = 0;
                    
                    sb.Append("#Players affected = ");
                    sb.Append(playerIndexes.Count);
                    sb.Append("\n");
                    sb.Append("#Team,FirstName,LastName,");
                    sb.Append(targetAttribute);
                    sb.Append("\n");
                    int tmp_2 = 0;
                    for (int i = 0; i < playerIndexes.Count; i++)
                    {
                        p = playerIndexes[i];
                        if (formulaMode == FormulaMode.Add )
                        {
                            tmp = GetPlayerField(p, targetAttribute);
                            if (Int32.TryParse(tmp, out temp_i)) 
                            {
                                if( Int32.TryParse(targetValueParam, out tmp_2))
                                    targetValue = (temp_i += tmp_2 ).ToString();
                                else
                                    return String.Format("Exception! value '{0}' is not an integer!", targetValueParam);
                            }
                            else
                                return String.Format( "Exception! Field '{0}' is not a number!", targetAttribute);
                        }
                        else if ( formulaMode == FormulaMode.Percent)
                        {
                            tmp = GetPlayerField(p, targetAttribute);
                            if (Int32.TryParse(tmp, out temp_i))
                            {
                                double percent = double.Parse(targetValueParam) * 0.01;
                                //Console.WriteLine("Test: {0}", ((int)(temp_i * percent)).ToString());
                                targetValue = ((int)(temp_i * percent)).ToString();
                            }
                            else
                                return String.Format("Exception! Cannot take a percent of '{0}'!", targetAttribute);
                        }
                        if (applyChanges)
                        {
                            this.SetPlayerField(p, targetAttribute, targetValue);
                        }
                        
                        sb.Append(GetPlayerTeam(p));
                        sb.Append(",");
                        sb.Append(GetPlayerFirstName(p));
                        sb.Append(",");
                        sb.Append(GetPlayerLastName(p));
                        sb.Append(",");
                        //sb.Append(GetPlayerField(p, targetAttribute));
                        sb.Append(targetValue);
                        sb.Append("\n");
                    }
                    retVal = sb.ToString();
                }
            }
            catch (Exception ex)
            {
                retVal = "Exception! Check formula: " + ex.Message;
            }
            return retVal;
        }

        /// <summary>
        /// Returns the indexes of the players matching the specified formula.
        /// Formula reference: https://docs.microsoft.com/en-us/dotnet/api/system.data.datacolumn.expression
        /// </summary>
        /// <param name="formula">the formula to match with</param>
        /// <param name="positions">the positions to search, null for all positions</param>
        /// <returns>a list of player indexes.</returns>
        public List<int> GetPlayersByFormula(string formula, List<string> positions)
        {
            List<int> retVal = new List<int>();
            if (formula != null)
            {
                //Console.WriteLine("GetPlayersByFormula: '{0}'; [{1}] ", formula , String.Join(",", positions.ToArray()) );
                System.Data.DataTable table = new System.Data.DataTable();
                string evaluationString = "";
                formula = formula.Replace("||", " or ").Replace("&&", " and ").Replace("=", " = ").Replace("!=", " <> ");
                while (formula.Contains("  ")) // replace 2 spaces with 1 space
                    formula = formula.Replace("  ", " ");

                bool addMe = false;
                string pos = "";
                for (int i = 0; i < mMaxPlayers; i++)
                {
                    pos = this.GetAttribute(i, PlayerOffsets.Position);
                    if (positions == null || positions.IndexOf(pos) > -1)
                    {
                        evaluationString = SubstituteAttributesForValues(i, formula);
                        evaluationString = SubstituteRandom(formula);
                        Object result = table.Compute(evaluationString, "");
                        addMe = (bool)result;
                        if (addMe)
                            retVal.Add(i);
                    }
                }
            }
            return retVal;
        }

        Regex mRandomRegex = new Regex("[Rr]andom_([0-9]+)_([0-9]+)");
        Random mRand = new Random();
        /// <summary>
        /// Random_1_100
        /// </summary>
        /// <param name="formula"></param>
        /// <returns></returns>
        private string SubstituteRandom(string formula)
        {
            string retVal = formula;
            Match m = mRandomRegex.Match(formula);
            if (m != Match.Empty)
            {
                int min = Int32.Parse(m.Groups[1].ToString());
                int max = Int32.Parse(m.Groups[2].ToString());
                int val = mRand.Next(min, max);
                retVal = formula.Substring(0, m.Index) + val + formula.Substring(m.Index + m.Length);
            }
            return retVal;
        }


        private string SubstituteAttributesForValues(int playerIndex, string formula)
        {
            string retVal = GetAppearanceAttributeExpression(formula, playerIndex);
            string playerTeam = "";
            string[] parts = ("Team,"+GetDefaultKey(true, false)).Split(",".ToCharArray());
            
            StringBuilder sb = new StringBuilder(10);
            string tmp = "";

            foreach (string attr in parts)
            {
                if (attr == "Team")
                {
                    playerTeam = ""+GetTeamIndex(GetPlayerTeam(playerIndex));
                    retVal = retVal.Replace("Team", playerTeam);
                    for (int j = 0; j < mTeamsDataOrder.Length; j++)
                    {
                        if( retVal.IndexOf(mTeamsDataOrder[j]) > -1)
                        {
                            retVal = retVal.Replace(mTeamsDataOrder[j], "" + j);
                        }
                    }
                }
                else if (!String.IsNullOrEmpty(attr) && formula.IndexOf(attr) > -1)
                {
                    tmp = this.GetPlayerField(playerIndex, attr);
                    retVal = retVal.Replace(attr, tmp);
                }
            }
            return retVal;
        }

        private string GetAppearanceAttributeExpression(string expr, int player)
        {
            string[] attrs = new string[] {
                //"DOB","YearsPro","PBP","Photo","Height",
                "Hand","BodyType","Skin","Face","Dreads","Helmet","FaceMask","Visor",
                "EyeBlack","MouthPiece","LeftGlove","RightGlove","LeftWrist","RightWrist","LeftElbow",
                "RightElbow","Sleeves","LeftShoe","RightShoe","NeckRoll","Turtleneck"
            };
            StringBuilder sb = new StringBuilder(15);
            string v = "";
            foreach (string attr in attrs)
            {
                sb.Length = 0;
                if (expr.Contains(attr))
                {
                    Type t = mTypeMap[attr];
                    List<string> values = new List<string>( Enum.GetNames(t));
                    for (int i = 0; i < values.Count; i++)
                    {
                        if (expr.Contains(values[i]))
                            expr = expr.Replace(values[i], "" + i);
                    }
                    v = GetPlayerField(player, attr);
                    expr = expr.Replace(attr, values.IndexOf(v)+"");
                }
            }
            return expr;
        }

        private Dictionary<string, Type> mTypeMap = new Dictionary<string, Type>(){
	            {"BodyType", typeof(Body)}, 
	            {"Dreads", typeof(YesNo)}, 
	            {"EyeBlack", typeof(YesNo)}, 
	            {"MouthPiece", typeof(YesNo)},
	            {"LeftGlove", typeof(Glove)},
	            {"RightGlove", typeof(Glove)},
	            {"LeftWrist", typeof(Wrist)},
	            {"RightWrist", typeof(Wrist)},
	            {"LeftElbow", typeof(Elbow)},
	            {"RightElbow", typeof(Elbow)},
	            {"LeftShoe", typeof(Shoe)},
	            {"RightShoe", typeof(Shoe)},

	            {"Hand", typeof(Hand)},
	            {"Skin", typeof(Skin)}, 
	            {"Face", typeof(Face)}, 
	            {"Helmet", typeof(Helmet)}, 
	            {"FaceMask", typeof(FaceMask)}, 
	            {"Visor", typeof(Visor)}, 
	            {"Sleeves", typeof(Sleeves)},
	            {"NeckRoll", typeof(NeckRoll)},
	            {"Turtleneck", typeof(Turtleneck)},
	            {"PowerRunStyle", typeof(PowerRunStyle)}
        };
        #endregion
    }

}
