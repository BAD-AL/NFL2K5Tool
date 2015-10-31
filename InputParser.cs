using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace NFL2K5Tool
{
    public class InputParser
    {
        public GamesaveTool Tool { get; set; }

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
                StringInputDlg.ShowError(e.Message);
            }
        }

        public void ProcessText(string text)
        {
            char[] chars = "\n\r".ToCharArray();
            string[] lines = text.Split(chars);
            ProcessLines(lines);
        }
        int player = 0;

        private ParsingStates mCurrentState = ParsingStates.PlayerModification;

        public void ProcessLines(string[] lines)
        {
            player = 0;
            int i = 0;
            try
            {
                for (i = 0; i < lines.Length; i++)
                {
                    ProcessLine(lines[i]);
                    //Console.WriteLine(i);
                }
                //ShowErrors();
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

        protected virtual void ProcessLine(string line)
        {
            line = line.Trim();
            if (line.StartsWith("#") || line.Length < 1)
            {
            }
            else 
            {
                switch (mCurrentState)
                {
                    case ParsingStates.PlayerModification:
                        Tool.SetPlayerData(player, line);
                        player++;
                        break;
                    case ParsingStates.Schedule:
                        break;
                }
            }
        }
    }

    public enum ParsingStates
    {
        PlayerModification,
        Schedule
    }
}
