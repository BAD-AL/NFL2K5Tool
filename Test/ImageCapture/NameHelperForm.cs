using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ImageCapture
{
    public partial class NameHelperForm : Form
    {
        public NameHelperForm()
        {
            InitializeComponent();
        }
        private Dictionary<string, string> mNameToIdHash = new Dictionary<string, string>();
        private Dictionary<string, string> mConcatedNameToNameHash = new Dictionary<string, string>();
        private Dictionary<string, string> mPhotoIdToNameHash = new Dictionary<string, string>();

        private void button1_Click(object sender, EventArgs e)
        {
            if(Errors.Length > 3)
                Errors.Remove(0, Errors.Length - 1);// clear it out
            PopulateHashes();
            ProcessFiles();
        }

        private StringBuilder Errors = new StringBuilder(10000);

        private void ProcessFiles()
        {
            string thumbnailLocation = @"C:\Users\Chris\Documents\Visual Studio 2008\Projects\NFL2K5Tool\Experimentation\FaceVideo\Thumbnails\";
            string destDir = @"C:\Users\Chris\Documents\Visual Studio 2008\Projects\NFL2K5Tool\Test\ImageCapture\bin\Debug\Capture\name_ids\";

            string[] files = Directory.GetFiles(thumbnailLocation);
            string id = null;
            string newName = "";
            string shortName = "";
            string goodName = "";
            int index =0;
            foreach (string file in files)
            {
                index = file.LastIndexOf('\\') + 1;
                shortName = file.Substring(index).Replace(".jpg", "");
                if ((id = FindId(shortName)) != null)
                {
                    if (mPhotoIdToNameHash.ContainsKey(id))
                    {
                        goodName = mPhotoIdToNameHash[id];
                        newName = string.Concat(destDir, id, "_", goodName, ".jpg");
                        if( !File.Exists(newName))
                            File.Copy(file, newName);
                    }
                    else
                    {
                        Errors.Append("Error! did not find ID:");
                        Errors.Append(id);
                        Errors.Append(" ShortName:");
                        Errors.Append(shortName);
                        Errors.Append("\n");
                    }
                }
                else
                {
                    Errors.Append("Error! did not find  ShortName:");
                    Errors.Append(shortName);
                    Errors.Append("\n");
                }
            }
            mErrorTextBox.Text = Errors.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="playerName">The player name portion of the file (may have OCR errors in it)</param>
        /// <returns></returns>
        public string FindId(string playerName)
        {
            string search1 = playerName.ToUpper();
            if (mNameToIdHash.ContainsKey(search1))
                return mNameToIdHash[search1];

            string search2 = search1.Replace('V', 'Y');
            if (mNameToIdHash.ContainsKey(search2))
                return mNameToIdHash[search2];
            
            string search3 = search1.Replace('F', 'P');
            if (mNameToIdHash.ContainsKey(search3))
                return mNameToIdHash[search3];

            string search4 = search2.Replace('F', 'P');
            if (mNameToIdHash.ContainsKey(search4))
                return mNameToIdHash[search4];
           
            return null;
        }

        private void PopulateHashes()
        {
            if (mNameToIdHash.Count > 0)
                return;

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "select name files"; // PictureGrab1 & 2
            dlg.Multiselect = true;
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                StringBuilder builder = new StringBuilder(10000);
                foreach (string file in dlg.FileNames) builder.Append(System.IO.File.ReadAllText(file));
                builder.Replace("\r\n", "\n");
                string[] lines = builder.ToString().Split(new char[] { '\n' });
                char[] chars = "=#".ToCharArray();
                string[] parts = null;
                char[] comma = ",".ToCharArray();
                foreach (string line in lines)
                {
                    if (line.IndexOfAny(chars) == -1 && line.IndexOf(',') > -1)
                    {
                        parts = line.Split(comma);// last name, first name, photoId
                        if (parts.Length == 3)
                        {
                            try
                            {
                                mPhotoIdToNameHash.Add(parts[2], string.Concat(parts[1], " ", parts[0])); // 11111 -> "Steve Young"
                                mNameToIdHash.Add(string.Concat(parts[1], parts[0]).ToUpper(), parts[2]); // "STEVEYOUNG" -> 11111
                                mConcatedNameToNameHash.Add(string.Concat(parts[1], parts[0]).ToUpper(), string.Concat(parts[1], " ", parts[0])); // "STEVEYOUNG" -> "Steve Young"
                            }
                            catch 
                            {
                                Errors.Append("Could not add data for: ");
                                Errors.Append(line);
                                Errors.Append("\n");
                            }
                        }
                    }
                }
            }
        }

        private string[] mTeams = {	"49ers", "Bears", "Bengals", "Bills", "Broncos", "Browns", "Buccaneers",
	        "Cardinals", "Chargers", "Chiefs", "Colts", "Cowboys", "Dolphins", "Eagles", "Falcons", "Giants",
	        "Jaguars", "Jets", "Lions", "Packers", "Panthers", "Patroits", "Raiders", "Rams", "Ravens", "Redskins",
	        "Saints", "Seahawks", "Steelers", "Texans", "Titans", "Vikings", "FreeAgents"};

        private void mGeneratePhotoDataButton_Click(object sender, EventArgs e)
        {
            ShowPlayers();
        }

        private void ShowPlayers()
        {
            StringBuilder builder = new StringBuilder(10000);
            builder.Append("Key=fname,lname,Photo\r\n");
            int numPlayers = 53;
            int startingValue = (int)numericUpDown1.Value;
            for (int i = 0; i < mTeams.Length; i++)
            {
                if (i == mTeams.Length - 1)
                    numPlayers = 241;
                AddPlayers(builder, mTeams[i], numPlayers, startingValue);
                startingValue += numPlayers;
            }
            mErrorTextBox.Text = builder.ToString();
        }

        private void AddPlayers(StringBuilder builder, string team, int numPlayers, int startingValue)
        {
            int endValue = startingValue + numPlayers;
            builder.Append(String.Format("Team = {0}    Players:{1}\r\n",team,numPlayers));
            for (int i = startingValue; i < endValue; i++)
            {
                builder.Append(String.Format("{0:D4},-,{0:D4}\r\n", i));
            }
        }
    }
}
