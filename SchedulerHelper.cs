using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace NFL2K5Tool
{
    /*
     * Flying fin Schedule file
     * 1 game = 8 bytes
     * Weeks seperated by 0x0007000000000000, which also signifies an empty game.
     * Those are present for byes.
     * Looks like a lot of room for games at the end of the game segment.
     * 
     * struct game
     * {
     *     home_team,
     *     away_team,
     *     month,
     *     day_of_month,
     *     two_digit_year,
     *     hour_ofDay,
     *     minute_of_hour,
     *     ?, // null_byte for seperation??
     *  };
     * 
     * Franchise file:
     * Starts at 0x917EB, same format
     * 
     */
    /// <summary>
    /// Summary description for SchedulerHelper.
    /// </summary>
    public class SchedulerHelper
    {

        private string[] mTeams =
		{
			"49ers",  //00
			"bears",
			"bengals",
			"bills",
			"broncos",
			"browns", //  05
			"buccaneers",
			"cardinals",     
			"chargers",
			"chiefs",
			"colts",//     0A
			"cowboys",
			"dolphins",   
			"eagles",
			"falcons",
			"giants",//    0F
			"jaguars",
			"jets",
			"lions",
			"packers",
			"panthers",//  14
			"patriots",
			"raiders",
			"rams",
			"ravens",
			"redskins",//   19
			"saints",
			"seahawks",
			"steelers",
			"texans",
			"titans", //    1E
			"vikings",
			"free_agents", //20
		};
        // schedule begins a the 2nd Thursday of September.
        public static int FranchiseGameOneYearLocation = 0x917ef; // first game year.

        public GamesaveTool Tool { get; set; }

        private byte mYear = 0x04; // default 2004

        private byte[] mNullGame = { 0x00, 0x07, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

        public static bool AUTO_CORRECT_SCHEDULE = true;

        private int[] mTeamGames;

        // for schedule files it's '2' for franchise files it's '0x917EB'.
        private int mWeekOneStartLoc = mFranchiseFileWeekOneStartLoc;
        private const int mFranchiseFileWeekOneStartLoc = 0x917EB;
        private const int mScheduleFileWeekOneStartLoc = 2;

        int mWeek, mWeekGameCount, mTotalGameCount;
        private Regex mGameRegex = new Regex("([0-9a-z]+)\\s+at\\s+([0-9a-z]+)");


        private int[] mGamesPerWeek = { 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16 };
        //private byte[] mBinarySchedule;

        public int WeekOneStartLoc
        {
            get { return mWeekOneStartLoc; }
        }

        /// <summary>
        /// Tells us if we are in 'FranchiseScheduleMode'.
        /// Will set WeekOneStartLoc to '2' if we pass 'false'.
        /// Will set WeekOneStartLoc to '0x917EB' if we pass 'true'.
        /// </summary>
        public bool FranchiseScheduleMode
        {
            get { return mWeekOneStartLoc == mFranchiseFileWeekOneStartLoc; }
            set
            {
                if (value)
                    mWeekOneStartLoc = mFranchiseFileWeekOneStartLoc;
                else
                    mWeekOneStartLoc = mScheduleFileWeekOneStartLoc;
            }
        }

        public SchedulerHelper(GamesaveTool tool)
        {
            //this.mBinarySchedule = GetDefaultSchedule();
            this.Tool = tool;
        }


        /// <summary>
        /// Gets the default schedule for NFL2K5.
        /// Don't think this will be necessary. keeping it for now in case I want it later.
        /// </summary>
        /// <returns></returns>
        private byte[] GetDefaultSchedule()
        {
            byte[] ret = null;

            try
            {
                System.IO.Stream fs = this.GetType().Assembly.GetManifestResourceStream("NFL2K5Tool.NFL2004-05.nfl2k5");
                long size = fs.Length;
                ret = new byte[size];
                fs.Read(ret, 0, (int)size);
                fs.Close();
            }
            catch (Exception e)
            {
                string msg = string.Format("Error! GetDefaultSchedule '{0}'", e.Message);
                StaticUtils.AddError(msg);
                System.Diagnostics.Debug.Assert(false, msg);
            }
            return ret;
        }

        private void PatchSchedule()
        {
            byte[] defaultSchedule = GetDefaultSchedule();
            int start = WeekOneStartLoc - 2; // -2 because the file starts with 2 '0' bytes.
            for (int i = 0; i < defaultSchedule.Length; i++)
                Tool.SetByte(start + i, defaultSchedule[i]);
        }

        /// <summary>
        /// Applies a schedule to the rom.
        /// </summary>
        /// <param name="lines">the contents of the schedule file.</param>
        public void ApplySchedule(List<string> lines)
        {
            mWeek = -1;
            mWeekGameCount = 0;
            mTotalGameCount = 0;

            PatchSchedule();
            // set up the year 
            if (Tool.Year > 0)
            {
                string theYear = Tool.Year.ToString();
                if (theYear.Length > 1)
                {
                    theYear = theYear.Substring(theYear.Length - 2, 2);
                    mYear = Byte.Parse(theYear);
                }
            }

            if (AUTO_CORRECT_SCHEDULE)
            {
                ReLayoutScheduleWeeks(lines);
                lines = Ensure17Weeks(lines);
            }

            string line;
            for (int i = 0; i < lines.Count; i++)
            {
                line = lines[i].ToString().Trim().ToLower();
                if (line.StartsWith("#") || line.Length < 3)
                { // do nothing.
                }
                else if (line.StartsWith("week"))
                {
                    if (mWeek > 17)
                    {
                        StaticUtils.AddError("Error! You can have only 17 weeks in a season.");
                        break;
                    }
                    CloseWeek();
                }
                else
                {
                    ScheduleGame(line);
                }
            }
            CloseWeek();// close week 17

            if (mWeek < 17)
            {
                StaticUtils.AddError("Warning! You didn't schedule all 17 weeks. The schedule could be messed up.");
            }
            if (mTeamGames != null)
            {
                for (int i = 0; i < mTeamGames.Length; i++)
                {
                    if (mTeamGames[i] != 16)
                    {
                        StaticUtils.AddError(string.Format(
                            "Warning! The {0} have {1} games scheduled.",
                            GetTeamFromIndex(i), mTeamGames[i]));
                    }
                }
            }
        }

        private void CloseWeek()
        {
            if (mWeek > -1)
            {
                int i = mWeekGameCount;
                while (i < 16)
                {
                    ScheduleGame(0xff, 0xff, mWeek, i /*week_game_count*/);
                    i++;
                }
            }
            mWeek++;
            mTotalGameCount += mWeekGameCount;
            mWeekGameCount = 0;
        }


        /// <summary>
        /// Attempts to schedule a game.
        /// </summary>
        /// <param name="line"></param>
        /// <returns>True on success, false on failure.</returns>
        private bool ScheduleGame(string line)
        {
            bool ret = false;
            Match m = mGameRegex.Match(line);
            string awayTeam, homeTeam;

            if (m != Match.Empty)
            {
                awayTeam = m.Groups[1].ToString();
                homeTeam = m.Groups[2].ToString();
                if (mWeekGameCount > 16)
                {
                    StaticUtils.AddError(string.Format(
                        "Error! Week {0}: You can have no more than 16 games in a week.", mWeek + 1));
                    ret = false;
                }
                else if (ScheduleGame(awayTeam, homeTeam, mWeek, mWeekGameCount))
                {
                    mWeekGameCount++;
                    ret = true;
                }

            }
            if (mTotalGameCount + mWeekGameCount > 256)
            {
                StaticUtils.AddError(string.Format(
                    "Warning! Week {0}: There are more than 256 games scheduled.", mWeek + 1));
            }
            return ret;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="awayTeam"></param>
        /// <param name="homeTeam"></param>
        /// <param name="week">Week is 0-16 (0 = week 1).</param>
        /// <param name="gameOfWeek"></param>
        public bool ScheduleGame(string awayTeam, string homeTeam, int week, int gameOfWeek)
        {
            int awayIndex = GetTeamIndex(awayTeam);
            int homeIndex = GetTeamIndex(homeTeam);

            if (awayIndex == -1 || homeIndex == -1)
            {
                StaticUtils.AddError(string.Format("Error! Week {2}: Game '{0} at {1}'", awayTeam, homeTeam, week + 1));
                return false;
            }

            if (awayIndex == homeIndex && awayIndex < 0x20)
            {
                StaticUtils.AddError(string.Format(
                    "Warning! Week {0}: The {1} are scheduled to play against themselves.", week + 1, awayTeam));
            }

            if (week < 0 || week > 17)
            {
                StaticUtils.AddError(string.Format("Week {0} is not valid. Weeks range 0 -17 (0 = week 1).", week));
                return false;
            }
            if (GameLocation(week, gameOfWeek) < 0)
            {
                StaticUtils.AddError(string.Format("Game {0} for week {1} is not valid. Valid games for week {1} are 0-{2}.",
                    gameOfWeek, week, 16));
                //StaticUtils.AddError(string.Format("{0} at {1}",awayTeam, homeTeam));
                return false;
            }

            ScheduleGame(awayIndex, homeIndex, week, gameOfWeek);

            if (awayTeam == "null" || homeTeam == "null")
                return false;
            return true;
        }

        private void ScheduleGame(int awayTeamIndex, int homeTeamIndex, int week, int gameOfWeek)
        {
            int location = GameLocation(week, gameOfWeek);
            if (location > 0)
            {
                if (awayTeamIndex != 0xff && homeTeamIndex != 0xff)
                {
                    Tool.SetByte(location + (int)Game.HomeTeam, (byte)homeTeamIndex);
                    Tool.SetByte(location + (int)Game.AwayTeam, (byte)awayTeamIndex);
                    Tool.SetByte(location + (int)Game.YearTwoDigit, mYear);

                    try
                    {
                        DateTime time = GetGameTime(week, gameOfWeek);
                        Tool.SetByte(location + (int)Game.Month, (byte)time.Month);
                        Tool.SetByte(location + (int)Game.Day, (byte)time.Day);
                    }
                    catch { }
                    if (awayTeamIndex < 0x20)
                    {
                        IncrementTeamGames(awayTeamIndex);
                        IncrementTeamGames(homeTeamIndex);
                    }
                }
                else
                {
                    location -= 2;
                    int loc = 0;

                    for (int i = 0; i < mNullGame.Length; i++)
                    {
                        loc = location + i;
                        Tool.SetByte(loc, mNullGame[i]);
                    }
                }
            }
            else
            {
                StaticUtils.AddError(string.Format("INVALID game. Week={0} Game of Week ={1}",
                    week, gameOfWeek));
            }
        }

        private DateTime GetGameTime(int week, int gameOfWeek)
        {
            int location = GameLocation(week, gameOfWeek);
            DateTime time = new DateTime(
                    2000 + Tool.GameSaveData[location + (int)Game.YearTwoDigit],
                    Tool.GameSaveData[location + (int)Game.Month],
                    Tool.GameSaveData[location + (int)Game.Day],
                    Tool.GameSaveData[location + (int)Game.HourOfDay],
                    Tool.GameSaveData[location + (int)Game.MinuteOfHour],
                    0);


            DayOfWeek d = time.DayOfWeek;
            int newYear = 2000 + mYear;
            time = time.AddYears(newYear - time.Year);
            while (d != time.DayOfWeek)
                time = time.AddDays(1);
            return time;
        }

        /// <summary>
        /// Returns a string like "49ers at giants", for a valid week, game of week combo.
        /// </summary>
        /// <param name="week">The week in question.</param>
        /// <param name="gameOfWeek">The game to get.</param>
        /// <returns>Returns a string like "49ers at giants", for a valid week, game of week combo, returns null
        /// upon error. </returns>
        public string GetGame(int week, int gameOfWeek)
        {
            int location = GameLocation(week, gameOfWeek);
            if (location == -1)
                return null;

            // If the game is a bye
            if (location > 2 && Tool.GameSaveData[location - 1] == 0x07)
                return "";

            int awayIndex = Tool.GameSaveData[location + 1];
            int homeIndex = Tool.GameSaveData[location];
            string ret = "";

            if (awayIndex < 0x20)
            {
                ret = string.Format("{0} at {1}",
                    GetTeamFromIndex(awayIndex),
                    GetTeamFromIndex(homeIndex));
            }
            return ret;
        }

        /// <summary>
        /// Returns a week from the season.
        /// </summary>
        /// <param name="week">The week to get [0-16] (0= week 1).</param>
        /// <returns></returns>
        public string GetWeek(int week)
        {
            if (week < 0 || week > mGamesPerWeek.Length - 1)
                return null;
            StringBuilder sb = new StringBuilder(20 * 16);
            sb.Append("WEEK\n");

            string game;
            int numGames = 0;

            for (int i = 0; i < mGamesPerWeek[week]; i++)
            {
                game = GetGame(week, i);
                if (game != null && game.Length > 0)
                {
                    sb.Append(string.Format("{0}\n", game));
                    numGames++;
                }
            }
            sb.Append("\n");
            return sb.ToString().Replace("WEEK", string.Format("WEEK {0}  [{1} games]", week + 1, numGames));
        }

        public string GetSchedule()
        {
            StringBuilder sb = new StringBuilder(20 * 16 * 17);
            sb.Append("YEAR=" + Tool.Year + "\n\n");
            for (int week = 0; week < mGamesPerWeek.Length; week++)
                sb.Append(GetWeek(week));

            return sb.ToString();
        }

        private int GameLocation(int week, int gameOfweek)
        {
            if (week < 0 || week > mGamesPerWeek.Length - 1 ||
                gameOfweek > mGamesPerWeek[week] || gameOfweek < 0)
                return -1;

            int offset = 0;
            for (int i = 0; i < week; i++)
                offset += (mGamesPerWeek[i] * 8) + 8;
            //offset += (gamesPerWeek[i]*2);

            offset += gameOfweek * 8;
            int location = WeekOneStartLoc + offset;
            Console.WriteLine("Week{0}; Game {1}; Location = 0x{2:x}", week + 1, gameOfweek+1, location);
            return location;
        }

        public List<string> GetErrorMessages()
        {
            return StaticUtils.Errors;
        }


        private void IncrementTeamGames(int teamIndex)
        {
            if (mTeamGames == null)
                mTeamGames = new int[0x20];
            if (teamIndex < mTeamGames.Length)
                mTeamGames[teamIndex]++;
        }

        private List<string> Ensure17Weeks(List<string> lines)
        {
            int wks = CountWeeks(lines);
            string line1, line2;
            for (int i = lines.Count - 2; i > 0; i -= 2)
            {
                line1 = lines[i];
                line2 = lines[i + 1];
                if (wks > 16)
                {
                    break;
                }
                else if (line1.IndexOf("at") > -1 && line2.IndexOf("at") > -1)
                {
                    lines.Insert(i + 1, "WEEK ");
                    i--;
                    wks++;
                }
            }
            return lines;
        }

        /// <summary>
        /// Lay out the schedule first game to last game, dividing the games up by games per week.
        /// </summary>
        public void ReLayoutScheduleWeeks(List<string> lines)
        {
            // first, remove all the 'WEEK' lines
            for (int i = lines.Count - 1; i > -1; i--)
                if (mGameRegex.Match(lines[i]) == Match.Empty)
                    lines.RemoveAt(i);
            int startAt = lines.Count;
            int[] gamesPerWeek = { 16, 16, 14, 14, 14, 14, 14, 14, 14, 14, 16, 16, 16, 16, 16, 16, 16 };
            for (int j = gamesPerWeek.Length - 1; j > -1; j--)
            {
                startAt -= gamesPerWeek[j];
                if (startAt < 0)
                    startAt = 0;
                lines.Insert(startAt, "WEEK 1");
            }
        }

        private int CountWeeks(List<string> lines)
        {
            int count = 0;
            foreach (string line in lines)
            {
                if (line.IndexOf("week", StringComparison.OrdinalIgnoreCase) > -1)
                    count++;
            }
            return count;
        }


        public int GetTeamIndex(string teamName)
        {
            int ret = -1;
            if (teamName.ToLower() == "null")
                return 255;
            for (int i = 0; i < mTeams.Length; i++)
            {
                if (mTeams[i] == teamName)
                {
                    ret = i;
                    break;
                }
            }
            return ret;
        }

        /// <summary>
        /// Returns the team specified by the index passed. (0= bills).
        /// </summary>
        /// <param name="index"></param>
        /// <returns>team name on success, null on failure</returns>
        public string GetTeamFromIndex(int index)
        {
            if (index == 255)
                return "null";
            if (index < 0 || index > mTeams.Length - 1)
                return null;
            return mTeams[index];
        }

        //private string mBaseScheduleFileName = "BaseSchedule.nfl2k5";

        //private byte[] GetBinarySchedule(string fileName)
        //{
        //    byte[] ret = null;
        //    try
        //    {
        //        FileInfo f1 = new FileInfo(fileName);
        //        long len = f1.Length;
        //        FileStream s1 = new FileStream(fileName, FileMode.OpenOrCreate);
        //        ret = new byte[(int)len];
        //        s1.Read(ret,0,(int)len);
        //        s1.Close();
        //    }
        //    catch
        //    {
        //        StaticUtils.AddError(string.Format("Error Reading Base schedule File '{0}'", mBaseScheduleFileName));
        //    }
        //    return ret;
        //}

        //public void SaveSchedule(string fileName)
        //{
        //    try
        //    {
        //        FileStream s1 = new FileStream(fileName, FileMode.OpenOrCreate);
        //        s1.Write(mBinarySchedule, 0, mBinarySchedule.Length);
        //        s1.Close();
        //    }
        //    catch(Exception e)
        //    {
        //        StaticUtils.AddError( string.Format("Error Saving Schedule "+e.Message) );
        //    }
        //}
    }// end class

}