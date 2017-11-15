using System;
using System.Collections.Generic;

using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;

namespace NFL2K5Tool
{
    static class Program
    {
        /*[DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, WindowShowStyle nCmdShow);*/

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
                //ShowWindow(GetConsoleWindow(), WindowShowStyle.Hide);

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
                
                //ShowWindow(GetConsoleWindow(), WindowShowStyle.Show);
            }
            else
            {
                GamesaveTool tool = null;
                // arguments 
                string saveFileName, outputFileName, dataToApplyTextFile;
                saveFileName = outputFileName = dataToApplyTextFile = null;

                bool showAppearance, showSpecialteams, showAbilities, showPlaybooks, showSchedule, readFromStdIn,
                    autoUpdateDepthChart, autoUpdatePbp, autoUpdatePhoto, showFreeAgents, showDraftClass;
                showAppearance = showSpecialteams = showAbilities = showPlaybooks = showSchedule = readFromStdIn = 
                    autoUpdateDepthChart = autoUpdatePbp = autoUpdatePhoto= showFreeAgents = showDraftClass = false;

                #region Argument processing
                string arg = "";
                for (int i = 0; i < args.Length; i++)
                {
                    arg = args[i].ToLower();
                    switch (arg)
                    {
                        case "-app":	showAppearance = true; break;
                        case "-st":     showSpecialteams = true; break;
                        case "-ab":     showAbilities = true; break;
                        case "-sch":    showSchedule = true;  break;
                        case "-stdin":  readFromStdIn = true; break;
                        case "-pb":     showPlaybooks = true; break;
                        case "-audc":   autoUpdateDepthChart = true; break;
                        case "-aupbp":  autoUpdatePbp = true; break;
                        case "-auph":   autoUpdatePhoto = true; break;
                        case "-fa"  :   showFreeAgents = true; break;
                        case "-dc":     showDraftClass = true; break;
                        case "-help":   // common help message arguments
                        case "--help":
                        case "/help":
                        case "/?":
                            PrintUsage();
                            return;
                        default:
                            if ( arg.StartsWith("-") )
                            {
                                Console.Error.WriteLine("Invalid argument: " + arg);
                            }
                            break;
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
                    InputParser parser = new InputParser(tool);
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
                //TODO: add support for showing playbooks
                if (tool != null)
                {
                    StringBuilder builder = new StringBuilder(5000);
                    if (showAbilities || showAppearance)
                    {
                        builder.Append(tool.GetKey(showAbilities, showAppearance));
                        builder.Append(tool.GetLeaguePlayers(showAbilities, showAppearance, showSpecialteams));
                        if (showFreeAgents)
                            builder.Append(tool.GetTeamPlayers("FreeAgents", showAbilities, showAppearance, false));
                        if (showDraftClass)
                            builder.Append(tool.GetTeamPlayers("DraftClass", showAbilities, showAppearance, false));
                    }
                    if (showSchedule)
                        builder.Append(tool.GetSchedule());
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
-st     Print Special teams players
-ab	    Print player abilities (speed, agility, ...).
-audc   Auto update the depth chart.
-aupbp  Auto update the play by play info for each player.
-auph   Auto update the photo for each player.
-sch    Print schedule.
-fa     Print Free Agents
-dc     Print draft class
-stdin  Read data from standard in.
-pb		Show Playbooks
-out:filename	Save modified Save file  to <filename>.
-help   Show this help message.
", Program.Version));
        }
    }

    /// <summary>Enumeration of the different ways of showing a window using
    /// ShowWindow</summary>
    public enum WindowShowStyle : uint
    {
        /// <summary>Hides the window and activates another window.</summary>
        /// <remarks>See SW_HIDE</remarks>
        Hide = 0,
        /// <summary>Activates and displays a window. If the window is minimized
        /// or maximized, the system restores it to its original size and
        /// position. An application should specify this flag when displaying
        /// the window for the first time.</summary>
        /// <remarks>See SW_SHOWNORMAL</remarks>
        ShowNormal = 1,
        /// <summary>Activates the window and displays it as a minimized window.</summary>
        /// <remarks>See SW_SHOWMINIMIZED</remarks>
        ShowMinimized = 2,
        /// <summary>Activates the window and displays it as a maximized window.</summary>
        /// <remarks>See SW_SHOWMAXIMIZED</remarks>
        ShowMaximized = 3,
        /// <summary>Maximizes the specified window.</summary>
        /// <remarks>See SW_MAXIMIZE</remarks>
        Maximize = 3,
        /// <summary>Displays a window in its most recent size and position.
        /// This value is similar to "ShowNormal", except the window is not
        /// actived.</summary>
        /// <remarks>See SW_SHOWNOACTIVATE</remarks>
        ShowNormalNoActivate = 4,
        /// <summary>Activates the window and displays it in its current size
        /// and position.</summary>
        /// <remarks>See SW_SHOW</remarks>
        Show = 5,
        /// <summary>Minimizes the specified window and activates the next
        /// top-level window in the Z order.</summary>
        /// <remarks>See SW_MINIMIZE</remarks>
        Minimize = 6,
        /// <summary>Displays the window as a minimized window. This value is
        /// similar to "ShowMinimized", except the window is not activated.</summary>
        /// <remarks>See SW_SHOWMINNOACTIVE</remarks>
        ShowMinNoActivate = 7,
        /// <summary>Displays the window in its current size and position. This
        /// value is similar to "Show", except the window is not activated.</summary>
        /// <remarks>See SW_SHOWNA</remarks>
        ShowNoActivate = 8,
        /// <summary>Activates and displays the window. If the window is
        /// minimized or maximized, the system restores it to its original size
        /// and position. An application should specify this flag when restoring
        /// a minimized window.</summary>
        /// <remarks>See SW_RESTORE</remarks>
        Restore = 9,
        /// <summary>Sets the show state based on the SW_ value specified in the
        /// STARTUPINFO structure passed to the CreateProcess function by the
        /// program that started the application.</summary>
        /// <remarks>See SW_SHOWDEFAULT</remarks>
        ShowDefault = 10,
        /// <summary>Windows 2000/XP: Minimizes a window, even if the thread
        /// that owns the window is hung. This flag should only be used when
        /// minimizing windows from a different thread.</summary>
        /// <remarks>See SW_FORCEMINIMIZE</remarks>
        ForceMinimized = 11
    }
}
