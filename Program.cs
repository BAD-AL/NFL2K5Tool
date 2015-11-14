using System;
using System.Collections.Generic;

using System.Windows.Forms;
using System.IO;
using System.Text;

namespace NFL2K5Tool
{
    static class Program
    {
        public static bool GUI_MODE = false;
        public static string Version
        {
            get
            {
                return System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString();
            }
        }

        /// <summary>
        /// The main entrypoint for the application.
        /// </summary>
        /// <param name="args">The arguments the user passes in.</param>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 0) //GUI mode
            {
                GUI_MODE = true;
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            else
            {
                GamesaveTool tool = null;
                // arguments 
                string saveFileName, outputFileName, dataToApplyTextFile;
                saveFileName = outputFileName = dataToApplyTextFile = null;

                bool showAppearance, showAbilities, showPlaybooks, showSchedule, readFromStdIn, autoUpdateDepthChart, autoUpdatePbp, autoUpdatePhoto;
                showAppearance = showAbilities = showPlaybooks = showSchedule = readFromStdIn = autoUpdateDepthChart = autoUpdatePbp = autoUpdatePhoto= false;

                #region Argument processing
                string arg = "";
                for (int i = 0; i < args.Length; i++)
                {
                    arg = args[i].ToLower();
                    switch (arg)
                    {
                        case "-app":	showAppearance = true; break;
                        case "-ab":     showAbilities = true; break;
                        case "-sch":    showSchedule = true;  break;
                        case "-stdin":  readFromStdIn = true; break;
                        case "-pb":     showPlaybooks = true; break;
                        case "-audc":   autoUpdateDepthChart = true; break;
                        case "-aupbp":  autoUpdatePbp = true; break;
                        case "-auph":   autoUpdatePhoto = true; break;
                        case "-help":   // common help message arguments
                        case "--help":
                        case "/help":
                        case "/?":
                            PrintUsage();
                            return;
                    }
                    if (args[i].StartsWith("-out:"))
                        outputFileName = args[i].Substring(5);
                    else if (args[i].EndsWith(".txt"))
                        dataToApplyTextFile = args[i];
                    else if (args[i].EndsWith(".dat", StringComparison.InvariantCultureIgnoreCase) || args[i].EndsWith(".zip", StringComparison.InvariantCultureIgnoreCase))
                        saveFileName = args[i];
                }
                #endregion
                
                #region load save file
                if (saveFileName != null ) 
                {
                    try
                    {
                        tool = new GamesaveTool();
                        if (!tool.LoadSaveFile(saveFileName))
                        {
                            Console.Error.WriteLine("File '{0}' does not exist. Make sure you have the correct path to the file specified. ", saveFileName);
                            return;
                        }
                    }
                    catch
                    {
                        Console.Error.WriteLine("Error loading file '{0}'. Make sure it is an actual NFL2K5 roster or franchise file. ", saveFileName);
                        return;
                    }
                }
                #endregion

                #region apply text data
                if ((dataToApplyTextFile != null || readFromStdIn) && outputFileName != null)  // apply data to save file
                {
                    if (tool == null)
                    {
                        Console.Error.WriteLine("You must specify a valid save file name in order to apply data.");
                        PrintUsage();
                        return;
                    }
                    InputParser parser = new InputParser();
                    parser.Tool = tool;
                    if (readFromStdIn)
                        parser.ReadFromStdin();
                    else
                        parser.ProcessFile(dataToApplyTextFile);
                    if (autoUpdateDepthChart)
                        tool.AutoUpdateDepthChart();
                    if( autoUpdatePbp)
                        tool.AutoUpdatePBP();
                    if (autoUpdatePhoto)
                        tool.AutoUpdatePhoto();
                    try // write to the file specified.
                    {
                        tool.SaveFile(outputFileName);
                        return;
                    }
                    catch (Exception e)
                    {
                        Console.Error.WriteLine("Error writing to file: {0}. {1}", outputFileName, e.Message);
                    }
                }
                #endregion

                #region printing stuff out
                //TODO: add support for showing schedule, playbooks
                if (tool != null)
                {
                    StringBuilder builder = new StringBuilder(5000);
                    builder.Append(tool.GetKey(showAbilities, showAppearance));
                    builder.Append(tool.GetLeaguePlayers(showAbilities, showAppearance));
                    builder.Append(tool.GetTeamPlayers("FreeAgents", showAbilities, showAppearance));
                    builder.Append(tool.GetTeamPlayers("DraftClass", showAbilities, showAppearance));
                    Console.Write(builder.ToString());
                }
                else
                {
                    Console.Error.WriteLine("Error! you need to specify a valid save file.");
                    PrintUsage();
                    return;
                }
                #endregion

                StaticUtils.ShowErrors(true);
            }
        }

        private static void PrintUsage()
        {
            Console.WriteLine(string.Format(
@"NFL2K5Tool.exe Version {0}

Modifies and extracts info from NFL2K5 game save files.

Usage: NFL2K5Tool <filename.dat>|<filename.zip> <data_to_apply.txt> [options]
This program can extract data from and import data into a NFL2K5 Save game files.

The default behavior when called with a .dat or .zip filename and no options is to print player inormation from the given NFL2K5 save file.
To launch the GUI, simply call the program with no arguments or double click on it. 

When called with a NFL2K5 save file and a <data_to_apply.txt> file, the behavior is that it will modify the NFL2K5 save file
with the data contained in the data file.

The following are the available options.

-app    Print appearance data.
-ab	    Print player abilities (speed, agility, ...).
-audc   Auto update the depth chart.
-aupbp  Auto update the play by play info for each player.
-auph   Auto update the photo for each player.
-sch    Print schedule.
-stdin  Read data from standard in.
-pb		Show Playbooks
-out:filename	Save modified Save file  to <filename>.
-help   Show this help message.
", Program.Version));
        }
    }
}
