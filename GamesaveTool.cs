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
        private const int cPlayerStart = 0xB288;
        private const int cPlayerDataLength = 0x54;
        private const int cMaxPlayers = 2317; // including free agents and draft class

        // Duane starks is the first player in the original roster 
        const int cDuaneStarksFnamePointerLoc = 0xB298;
        const int cNumberOfColleges = 265;

        const int cCollegeStringsStart = 0x7A23C;

        //may be able to add strings after this section; (All 0's from 0x8f2f0 - 0x9131f)
        const int cCollegeStringsEnd = 0x8f2c0;

        const int cModifiableNameSectionEnd = 0x8906f;

        const string NFL2K5Folder = "53450030";

        // Do not modify strings in this section.
        // There is however a blank section that could possibly be used for 
        // names after it (0x8f2f0 - 0x91310)
        const int cCollegePlayerNameSectionStart = 0x8bab0;
        const int cCollegePlayerNameSectionEnd = 0x8f2ef;

        const int cFreeAgentCountLocation = 0x357; // default : 00 c5
        const int c49ersPlayerPointersStart = 0x44a8; // playerLoc = ptrLoc + ptrVal -1;
        const int cFreeAgentsPlayerPointersStart = 0x3f644;
        const int c49ersNumPlayersAddress = 0x45c3; // 00 35
        const int cTeamDiff = 0x1f4; // 500 bytes

		// The team data is ordered like this:
        private string[] mTeamsDataOrder =  {
                 "49ers", "Bears","Bengals", "Bills", "Broncos", "Browns","Buccaneers", "Cardinals", 
                 "Chargers", "Chiefs","Colts","Cowboys",  "Dolphins", "Eagles","Falcons","Giants","Jaguars",
                 "Jets","Lions","Packers", "Panthers", "Patroits","Raiders","Rams","Ravens","Redskins",
                 "Saints","Seahawks","Steelers", "Texans", "Titans",  "Vikings", 
                 
                 // these guys aren't really teams, investigate deleting these 2 from the array
                 "FreeAgents", "DraftClass" 
                 };
        
        //Players are listed in this order in the original 2004 season; No longer used
        //private string[] mTeamsPlayerOrder = {
        //           "Cardinals", "Falcons", "Ravens", "Bills", "Panthers", "Bears", "Bengals", "Cowboys",
        //           "Broncos", "Lions", "Packers", "Colts", "Jaguars", "Chiefs", "Dolphins", "Vikings", 
        //           "Patroits", "Saints", "Giants", "Jets", "Raiders", "Eagles", "Steelers", "Rams",  "Chargers", 
        //           "49ers", "Seahawks", "Buccaneers", "Titans", "Redskins", "Browns", "Texans", 
        //           "FreeAgents", "DraftClass"
        //           };

        private string[] mColleges;

        /// <summary>
        /// The gamesave file/data
        /// </summary>
        public byte[] GameSaveData = null;

        /// <summary>
        /// Gets the players by team (does not get free agents or draft class)
        /// </summary>
        /// <param name="attributes">true to list out skills</param>
        /// <param name="appearance">true to list out appearance</param>
        /// <param name="freeAgents"></param>
        /// <param name="draftClass"></param>
        /// <returns></returns>
        public string GetLeaguePlayers(bool attributes, bool appearance, bool freeAgents, bool draftClass)
        {
            StringBuilder builder = new StringBuilder(300 * 55 * 35);
            for (int i = 0; i < 32; i++)
            {
                builder.Append(GetTeamPlayers(mTeamsDataOrder[i], attributes, appearance));
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
            int firstPlayer = 1937;
            int numPlayers = 380;
            int limit  = firstPlayer +  numPlayers;
            StringBuilder builder = new StringBuilder(300 * numPlayers + 1);
            builder.Append("\nTeam = ");
            builder.Append("DraftClass");
            builder.Append("    Players:");
            builder.Append(numPlayers);
            builder.Append("\n");
            
            for (int i = firstPlayer; i < limit; i++)
            {
                builder.Append(GetPlayerData(i, attributes, appearance));
                builder.Append("\n");
            }
            return builder.ToString();
        }

        /// <summary>
        /// Get all the players on team specified
        /// </summary>
        /// <param name="team"></param>
        /// <param name="attributes">include skill attributes</param>
        /// <param name="appearance">include appearance attributes.</param>
        /// <returns>string with all the players for the given team.</returns>
        public string GetTeamPlayers(string team, bool attributes, bool appearance)
        {
            int teamIndex = GetTeamIndex(team);
            int teamPlayerPointersStart = teamIndex * cTeamDiff + c49ersPlayerPointersStart;
            if ("FreeAgents".Equals(team, StringComparison.InvariantCultureIgnoreCase))
                teamPlayerPointersStart = cFreeAgentsPlayerPointersStart;
            else if ("DraftClass".Equals(team, StringComparison.InvariantCultureIgnoreCase))
                return GetDraftClass(attributes, appearance);
            /*
            int numPlayers = GetNumPlayers(team);
            int playerIndex = -1;

            StringBuilder builder = new StringBuilder(300 * numPlayers +1);
            builder.Append("\nTeam = ");
            builder.Append(team);
            builder.Append("    Players:");
            builder.Append(numPlayers);
            builder.Append("\n");

            for (int i = 0; i < numPlayers; i++)
            {
                playerIndex = GetPlayerIndexByPointer( teamPlayerPointersStart + (i * 4)); // 4== ptr length
                builder.Append(GetPlayerData(playerIndex, attributes, appearance));
                builder.Append("\n");
            }
            return builder.ToString();
            */
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
            return builder.ToString();
        }

        private List<int> GetPlayerIndexesForTeam(string team)
        {
            List<int> retVal = new List<int>(55);
            int teamIndex = GetTeamIndex(team);
            int teamPlayerPointersStart = teamIndex * cTeamDiff + c49ersPlayerPointersStart;
            if ("FreeAgents".Equals(team, StringComparison.InvariantCultureIgnoreCase))
                teamPlayerPointersStart = cFreeAgentsPlayerPointersStart;
            //else if ("DraftClass".Equals(team, StringComparison.InvariantCultureIgnoreCase))
            //    return GetDraftClass(attributes, appearance);

            int numPlayers = GetNumPlayers(team);
            int playerIndex = -1;

            for (int i = 0; i < numPlayers; i++)
            {
                playerIndex = GetPlayerIndexByPointer(teamPlayerPointersStart + (i * 4)); // 4== ptr length
                retVal.Add(playerIndex);
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
            int retVal = (playerLoc - cPlayerStart) / cPlayerDataLength;
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
            int index = GetTeamIndex(team);
            int location = c49ersNumPlayersAddress + index * cTeamDiff;
            if ("FreeAgents".Equals(team, StringComparison.InvariantCultureIgnoreCase))
                location = cFreeAgentCountLocation;

            int retVal = GameSaveData[location] << 8;
            retVal += GameSaveData[location + 1];
            return retVal;
        }

        private int GetTeamIndex(string team)
        {
            for (int i = 0; i < mTeamsDataOrder.Length; i++)
                if (team == mTeamsDataOrder[i])
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
        /// Loads the gamesave fle
        /// TODO: error checking; filetype setup (roster/franchise)
        /// </summary>
        /// <param name="fileName">the filename to load</param>
        public void LoadSaveFile(string fileName)
        {
            GameSaveData = File.ReadAllBytes(fileName);
            PopulateColleges();
        }

        /// <summary>
        /// Can be useful for debugging when you want to check why you're writing at a specific point in the file.
        /// </summary>
        /// <param name="location"></param>
        /// <param name="b"></param>
        protected void SetByte(int location, byte b)
        {
            GameSaveData[location] = b;
        }

        private int[] mOrder; // order of attributes

        /// <summary>
        /// The attributes key
        /// </summary>
        public string GetKey(bool attributes, bool appearance)
        {
            StringBuilder builder = new StringBuilder(350);
            StringBuilder dummy = new StringBuilder(200);
            int prevLength = 0;
            builder.Append("#Pos,fname,lname,Number,");
            mOrder = new int[4 + mAttributeOrder.Length + mappearanceOrder.Length];
            mOrder[0] = (int)PlayerOffsets.Position;;
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
                foreach (AppearanceAttributes app in mappearanceOrder)
                {
                    prevLength = dummy.Length;
                    GetPlayerappearanceAttribute(0, app, dummy);
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

        private char[] mComma = new char[] { ',' };

        /// <summary>
        /// Sets a player's attributes
        /// </summary>
        /// <param name="player">The index of the player</param>
        /// <param name="line">The data tp apply.</param>
        public void SetPlayerData(int player, string line)
        {
            int attr = -1;
            string[] attributes = line.Split(mComma);
            for (int i = 0; i < attributes.Length; i++)
            {
                // figure out how to set names
                attr = mOrder[i];
                if (attr == -1)
                {
                    // Name setting perhaps should be done at another, smarter level?
                    // How we gonna decide to use pointers or not?
                    SetPlayerFirstName(player, attributes[i], false);
                }
                else if (attr == -2)
                {
                    // How we gonna decide to use pointers or not?
                    SetPlayerLastName(player, attributes[i], false);
                }
                if (attr > 99)
                {
                    SetPlayerappearanceAttribute(player, (AppearanceAttributes)attr, attributes[i]);
                }
                else
                {
                    SetAttribute(player, (PlayerOffsets)attr, attributes[i]);
                }
            }
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

            builder.Append(GetPlayerPosition(player));
            builder.Append(',');
            builder.Append(GetPlayerName(player, ','));
            builder.Append(',');
            builder.Append(GetAttribute(player, PlayerOffsets.JerseyNumber));
            builder.Append(',');
            
            if (attributes)
                GetPlayerAttributes(player,builder);
            if (appearance)
                GetPlayerappearance(player, builder);

            return builder.ToString();
        }

        /// <summary>
        /// Get the location where the player's data starts.
        /// </summary>
        public int GetPlayerDataStart(int player)
        {
            int ret = cPlayerStart + player * cPlayerDataLength;
            return ret;
        }

        private void GetPlayerappearance(int player, StringBuilder builder)
        {
            foreach (AppearanceAttributes attr in this.mappearanceOrder)
            {
                GetPlayerappearanceAttribute(player, attr, builder);
            }
        }

        private void SetPlayerappearanceAttribute(int player, AppearanceAttributes attr, string strVal)
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
                    
                    break;
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
            }
        }

        private void GetPlayerappearanceAttribute(int player, AppearanceAttributes attr, StringBuilder builder)
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
            }
        }

        private AppearanceAttributes[] mappearanceOrder = new AppearanceAttributes[]{
             AppearanceAttributes.College, AppearanceAttributes.DOB, AppearanceAttributes.Hand, 
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

        private string GetAttribute(int player, PlayerOffsets attr)
        {
            string retVal = "";
            int loc = GetPlayerDataStart(player) + (int)attr;
            int val = GameSaveData[loc];
            switch (attr)
            {
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
                    //TODO: get this working
                    // The year still isn't correct :( [month and Day are good though]; moving on...
                    int lsd_year = ((GameSaveData[loc + 2] & 0x0001) << 3) + (GameSaveData[loc + 1] >> 5);
                    int msd_year = (GameSaveData[loc + 2] & 0x0e)  ;
                    int day = GameSaveData[loc+1] & 0x1f;
                    int month = (int)(GameSaveData[loc]  >> 4);
                    //if (month != 0 && day != 0 && year != 0)
                        retVal = string.Concat(new object[] { month, "/", day, "/", msd_year, lsd_year });
                    //else
                    //    retVal = "1/1/1954";
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
                default:
                    retVal += val;
                    break;
            }
            return retVal;
        }

        private char[] slash = { '/' };

        private void SetAttribute(int player, PlayerOffsets attr, string stringVal)
        {
            int loc = GetPlayerDataStart(player) + (int)attr;
            int val = 0;
            int v1, v2;
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
                    v1 = (val >> 5 & 3);
				    v2 = (val  << 3 & 0xf8);
                    SetByte(loc + 1, (byte)v1);
                    SetByte(loc, (byte)v2);
                    break;
                case PlayerOffsets.DOB:
                    //TODO: get this working
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
            int ptrLoc1 = player * cPlayerDataLength + cDuaneStarksFnamePointerLoc;
            if (isLastName)
                ptrLoc1 += 4;
            if (useExistingName)
            {
                // we'll look through the college player names because we're not changing those.
                List<long> locations = StaticUtils.FindStringInFile(name, GameSaveData, cCollegeStringsStart, cCollegeStringsEnd, true);
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
            {
                string prevName = GetName(ptrLoc1);
                if (prevName != name)
                {
                    int diff = 2 * (name.Length - prevName.Length);
                    int stringLoc = GetPointerDestination(ptrLoc1);

                    if (diff > 0)
                        ShiftDataDown(GetPointerDestination(ptrLoc1), diff);
                    else if (diff < 0)
                        ShiftDataUp(GetPointerDestination(ptrLoc1), -1 * diff);

                    AdjustStringPointers(stringLoc + 2 * prevName.Length, diff);

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
            return retVal;
        }

        //0x8960f is the end of the name section; although it's not obvious 
        // that the stuff after that section is useful (it's all 2a 00 repeating)
        private void ShiftDataDown(int startIndex, int amount)
        {
            for (int i = cModifiableNameSectionEnd - amount; i > startIndex; i--)
            {
                //SetByte(i, GameSaveData[i - amount]); // for debugging
                GameSaveData[i] = GameSaveData[i - amount]; // for speed
            }
        }

        private void ShiftDataUp(int startIndex, int amount)
        {
            for (int i = startIndex; i < cModifiableNameSectionEnd; i++)
            {
                //SetByte(i, GameSaveData[i + amount]); // for debugging
                GameSaveData[i] = GameSaveData[i + amount]; // for speed
            }
        }

        /// <summary>
        /// Gets the player's name 
        /// </summary>
        /// <param name="player">an int from 0 to 2317</param>
        /// <returns></returns>
        public string GetPlayerName(int player, char sepChar)
        {
            string retVal = "!!!!!!!!INVALID!!!!!!!!!!!!";
            if (player > -1 && player <= cMaxPlayers)
            {
                int ptrLoc = player * cPlayerDataLength + cDuaneStarksFnamePointerLoc;
                retVal = GetName(ptrLoc) + sepChar + GetName(ptrLoc + 4);
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

        /// <summary>
        /// If a pointer points to something before the changed area, leave it alone.
        /// else adjust it.
        /// </summary>
        /// <param name="locationOfChange">The location where the string table changed</param>
        /// <param name="difference">the amount to adjust the pointrs by.</param>
        private void AdjustStringPointers(int locationOfChange, int difference)
        {
            int firstNamePtr = 0;
            int lastNamePtr = 0;
            int loc = 0;
            for (int player = 0; player <= cMaxPlayers; player++)
            {
                firstNamePtr = player * cPlayerDataLength + cDuaneStarksFnamePointerLoc;
                lastNamePtr = firstNamePtr + 4;
                loc = GetPointerDestination(firstNamePtr);
                if (loc < cModifiableNameSectionEnd)
                {
                    if (loc >= locationOfChange)
                    {
                        AdjustPointer(firstNamePtr, difference);
                    }
                    loc = GetPointerDestination(lastNamePtr);
                    if (loc >= locationOfChange)
                    {
                        AdjustPointer(lastNamePtr, difference);
                    }
                }
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


        private void PopulateColleges()
        {
            mColleges = new string[cNumberOfColleges];
            int loc = cCollegeStringsStart;
            //end == 0x7bc50
            for (int i = 0; i < mColleges.Length; i++)
            {
                mColleges[i] = GetString(loc);
                loc = loc + (mColleges[i].Length + 1) * 2;
            }
        }

        // could improve this by also ignoring spaces???
        private int GetCollegeIndex(string college)
        {
            int retVal = -1;
            for (int i = 0; i < mColleges.Length; i++)
            {
                if (college.Equals(mColleges[i], StringComparison.InvariantCultureIgnoreCase))
                {
                    retVal = i;
                    break;
                }
            }
            return retVal;
        }

        #region Get/Set attributes

        // input like 6'3"
        private int GetInches(string stringVal)
        {
            int feet = stringVal[0] - 30;
            stringVal = stringVal.Replace("\"", "");
            int inches = Int32.Parse(stringVal.Substring(2));
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
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.FaceMask;
            FaceMask ret = (FaceMask)Enum.Parse(typeof(FaceMask), val);
            int dude = (int)ret;
            dude = dude << 2;
            int b = GameSaveData[loc];
            b &= 0x80;// clear all but first bit
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
            b += dude;
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
            b &= 0x1F; // clear bits 6&7
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
            b &= 0x67; // clear bits 4&5
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
            b &= 0x7b; // clear bit 3
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
            YesNo ret = (YesNo)Enum.Parse(typeof(YesNo), val);
            int dude = (int)ret;
            dude = dude << 1;
            int b = GameSaveData[loc];
            b &= 0x7d; // clear bit 2
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
            b &= 0x7e; // clear bit 1
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
            int b1 = GameSaveData[loc1];
            int b2 = GameSaveData[loc2];

            switch (ret)
            {
                case Visor.None:
                    b1 &= 0x7f;
                    b2 &= 0xfe;
                    break;
                case Visor.Clear:
                    b1 += 0x80;
                    b2 &= 0xfe;
                    break;
                case Visor.Dark:
                    b1 &= 0x7f;
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
            int loc2 = GetPlayerDataStart(player) + 1 + (int)PlayerOffsets.DOB; //skin is in the same bytes as DOB
            int b = GameSaveData[loc2] << 8;
            b += GameSaveData[loc1]; // Skin is in bits 5-9 of this int
            b &= 0xfff;
            b = b >> 7;
            Skin ret = (Skin)b;
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
            int loc2 = GetPlayerDataStart(player) + 1 + (int)PlayerOffsets.DOB; //skin is in the same bytes as DOB
            Skin ret = (Skin)Enum.Parse(typeof(Skin), val);
            int dude = (int)ret;
            dude = dude << 7;
            int b = GameSaveData[loc2] << 8;
            b += GameSaveData[loc1];
            b &= 0xf07f; // clear out bits 5-9
            b += dude;
            // now put hi byte in DOB+1; low byte in DOB
            int b1 = b & 0xff;
            int b2 = b >> 8;
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
            int val = GameSaveData[loc];
            if (h == Helmet.Standard)
                val = val & 0xBF;
            else
                val = val | 0x40;
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
            Shoe h = (Shoe)Enum.Parse(typeof(Shoe), shoe);
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.Helmet_LeftShoe_RightShoe;
            //LShoe is last 3 bits; 
            int val = GameSaveData[loc] & 0xf8;
            val |= (int)val;
            SetByte(loc, (byte)val);
        }

        private void GetRightShoe(int player, StringBuilder builder)
        {
            Shoe retVal = Shoe.Shoe1;
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.Helmet_LeftShoe_RightShoe;
            // RShoe is bits 3,4,5 
            int val = GameSaveData[loc] & 0x38;
            val = val >> 3;
            retVal = (Shoe)val;
            builder.Append(retVal.ToString());
            builder.Append(",");
        }

        private void SetRightShoe(int player, String shoe)
        {
            Shoe h = (Shoe)Enum.Parse(typeof(Shoe), shoe);
            int loc = GetPlayerDataStart(player) + (int) PlayerOffsets.Helmet_LeftShoe_RightShoe;
            // RShoe is bits 3,4,5 
            int val = GameSaveData[loc] & 0xc7;
            val |= (val << 3 ) ;
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
            int val1 = GameSaveData[loc]; // 
            int val2 = GameSaveData[loc+1]; // most sig 2 bits of glove go to least sig bits in this value
            val1 = (val1 & 0x3f) + ( ((int)g & 3) << 6);
            val2 = (val2 & 0xfc) + ((int)g >> 2);
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
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.RightGlove_LeftWrist;
            int val = ((GameSaveData[loc + 1] << 8) + GameSaveData[loc]) & 0xfcf3;
            val += ((int)s << 6);
            SetByte(loc, (byte)val);
            SetByte(loc + 1, (byte)(val >> 8));
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
            int val2 = (GameSaveData[loc+1] & 0xfc) + ((int)s & 0xfc );

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

        //CollegeIndex(p) = (((collegePointerVal - (-2127 /*0xfffff7b1*/)) + player * cPlayerDataLength)) / 8;
        private string GetCollege(int player)
        {
            int loc = GetPlayerDataStart(player);
            int collegePointerVal = (GameSaveData[loc + 3] << 24) + (GameSaveData[loc + 2] << 16) + (GameSaveData[loc + 1] << 8) + GameSaveData[loc];
            int collegeIndex = (((collegePointerVal - (-2127 /*0xfffff7b1*/)) + player * cPlayerDataLength)) / 8;
            return mColleges[collegeIndex];
        }

        private void SetCollege(int player, string college)
        {
            int loc = GetPlayerDataStart(player);
            int collegeIndex = GetCollegeIndex(college);
            int pointerVal = 0;
            if (collegeIndex > -1)
            {
                // check this 
                pointerVal = ((-2127 /*0xfffff7b1*/) - player * cPlayerDataLength) + collegeIndex * 8;
                Console.WriteLine("player {0} college pointer {1}", player, pointerVal);
                byte b1 = (byte)pointerVal; ;
                byte b2 = (byte)(pointerVal >> 8);

                SetByte(loc, b1);
                SetByte(loc + 1, b2);
            }
            else
            {
                //TODO: Add error ?
            }
        }
        #endregion

    }
}
