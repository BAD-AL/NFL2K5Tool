using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;

namespace NFL2K5Tool
{
    public partial class FaceForm : Form
    {
        // actual size of player face image = 94x107
        const int pbWidth = 94;
        const int pbHeight = 107;

        internal static string sCategoryFile = "PlayerData\\FaceFormCategories.json";
        internal static string sFaceDir = "PlayerData\\PlayerPhotos";
        private string[] mPhotoFiles = null;
        private string[] mAllPhotoFiles = null;
        private GenericArrayArray mCategories = null;

        private List<PictureBox> mPictureBoxList = new List<PictureBox>();

        private int mStartingPictureIndex = 0;

        protected int StartingPictureIndex
        {
            get { return mStartingPictureIndex; }
            set
            {
                mStartingPictureIndex = value;
                OnStartingPictureIndexChanged();
            }
        }

        public string SelectedFace
        {
            get;
            set;
        }

        /// <summary>
        /// Form used to Pick a face.
        /// </summary>
        public FaceForm()
        {
            InitializeComponent();
            if (Directory.Exists(sFaceDir))
            {
                mAllPhotoFiles = System.IO.Directory.GetFiles(sFaceDir, "*.jpg");
                mPhotoFiles = mAllPhotoFiles;
                PopulatePhotosBoxes();
                UpdateScrollbarLargeChange();
                mPhotoScrollBar.ValueChanged += new EventHandler(mPhotoScrollBar_ValueChanged);
                StartingPictureIndex = 0;
            }
            else
                MessageBox.Show("Expected Folder '" + sFaceDir + "\\' to exist!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (!File.Exists(sCategoryFile))
            {
                String txf = StaticUtils.GetEmbeddedTextFile("FaceFormCategories.json");
                if (!Directory.Exists(".\\PlayerData\\"))
                    Directory.CreateDirectory(".\\PlayerData\\");
                Console.WriteLine("Couldn't fine 'FaceFormCategories.json', retrieving embedded file...");
                File.WriteAllText(sCategoryFile, txf);
            }
            if (File.Exists(sCategoryFile))
            {
                mCategories = GenericArrayArray.FromFile(sCategoryFile);
                PopulateCategories();
            }
        }

        private void UpdateScrollbarLargeChange()
        {
            mPhotoScrollBar.LargeChange = mPhotoScrollBar.Height / pbHeight;
        }

        public int[] GetFacesFromCategory(string key)
        {
            if (mCategories != null)
            {
                for (int i = 0; i < mCategories.categories.Length; i++)
                {
                    if (mCategories.categories[i].key == key)
                        return mCategories.categories[i].values;
                }
            }
            return null;
        }

        public bool CheckFace(int faceNum, string category)
        {
            bool retVal = false;
            int[] faces = GetFacesFromCategory(category);
            if (faces != null && Array.IndexOf(faces,faceNum) > -1)
            {
                retVal = true;
            }
            return retVal;
        }

        void mPhotoScrollBar_ValueChanged(object sender, EventArgs e)
        {
            StartingPictureIndex = mPhotoScrollBar.Value * (mPictureBoxPanel.Width  / pbWidth) ;
        }

        ToolTip mTip = new ToolTip();

        private void OnStartingPictureIndexChanged()
        {
            string filename = "";
            mPictureBoxPanel.SuspendLayout();
            for (int i = 0; i < mPictureBoxList.Count; i++)
            {
                if (mPhotoFiles.Length > (i + StartingPictureIndex))
                {
                    filename = mPhotoFiles[i + StartingPictureIndex];
                    if (!File.Exists(filename))
                        Console.WriteLine("Image does not exist: {0}", filename);
                    mPictureBoxList[i].Image = StaticUtils.GetImageFromPath(filename);
                    mPictureBoxList[i].Tag = filename;
                    mTip.SetToolTip(mPictureBoxList[i], GetToolTip(filename));
                }
                else
                {
                    mPictureBoxList[i].Image = null;
                    mPictureBoxList[i].Tag = "";
                }
            }
            mPictureBoxPanel.ResumeLayout();
        }

        private string ENFPhotoIndex_txt = null;

        public string GetToolTip(string filename)
        {
            string retVal = "--NONE--";
            if (ENFPhotoIndex_txt == null)
            {
                ENFPhotoIndex_txt = File.ReadAllText("PlayerData\\ENFPhotoIndex.txt");
            }
            //special case nophoto
            if (filename == "PlayerData\\PlayerPhotos\\NoPhoto.jpg")
                return "NoPhoto=0004";
            string lookup = "=" + filename.Substring(filename.Length-8, 4);
            int index1 = ENFPhotoIndex_txt.IndexOf(lookup);
            if (index1 > -1)
            {
                int i;
                for (i = index1; i > 0; i--)
                {
                    if (ENFPhotoIndex_txt[i - 1] == '\n')
                        break;
                }
                retVal = ENFPhotoIndex_txt.Substring(i, index1 - i + 5);
            }
            return retVal;
        }

        private void PopulateCategories()
        {
            if (mCategories != null)
            {
                mCategoryComboBox.Items.Clear();
                mCategoryComboBox.Items.Add("All");
                foreach (GenericArrayObj obj in mCategories.categories)
                {
                    mCategoryComboBox.Items.Add(obj.key);
                }
                mCategoryComboBox.SelectedIndex = 0;
            }
        }

        private void PopulatePhotosBoxes( )
        {
            int currentY = 0;
            int currentX = 0;

            mPictureBoxPanel.SuspendLayout();
            int pbAcross = mPictureBoxPanel.Width  / pbWidth;
            int pbDown   = mPictureBoxPanel.Height / pbHeight;

            for (int i = 0; i <= pbDown; i++)
            {
                currentX = 0;
                for (int j = 0; j < pbAcross; j++)
                {
                    PictureBox box = new PictureBox();
                    box.Click += new EventHandler(box_Click);
                    box.SizeMode = PictureBoxSizeMode.StretchImage;
                    box.Bounds = new Rectangle(currentX, currentY, pbWidth, pbHeight);
                    box.Parent = mPictureBoxPanel;
                    mPictureBoxList.Add(box);
                    currentX += pbWidth;
                    box.Paint += new PaintEventHandler(box_Paint);
                }
                currentY += pbHeight;
            }
            mPhotoScrollBar.Minimum = 0;
            mPhotoScrollBar.Maximum = (mPhotoFiles.Length /pbAcross)+3; // should be Total number of rows possible
            mPictureBoxPanel.ResumeLayout();
        }

        private void ReleasePictureBoxes()
        {
            for (int i = mPictureBoxList.Count - 1; i > -1; i--)
            {
                mPictureBoxList[i].Parent = null;
                mPictureBoxList[i].Dispose();
            }
            mPictureBoxList.Clear();
        }

        void box_Paint(object sender, PaintEventArgs e)
        {
            if (mShowNumbersCheckbox.Checked)
            {
                PictureBox box = sender as PictureBox;
                string tag = box.Tag.ToString();
                if (tag != null && tag.Length > 25)
                {
                    string text = tag.Substring(24, 4);
                    SizeF sz = e.Graphics.MeasureString(text, this.Font);
                    e.Graphics.FillRectangle(Brushes.Beige, 0, 0, sz.Width, sz.Height);
                    e.Graphics.DrawString(text, this.Font, Brushes.Black, 0, 0);
                }
            }
        }

        List<string> leftClickPlayers = new List<string>();   // dark
        List<string> rightClickPlayers = new List<string>();  // light
        List<string> middleClickPlayers = new List<string>(); // medium

        void box_Click(object sender, EventArgs e)
        {
            PictureBox box = sender as PictureBox;
            if (classifyMode.Checked)
            {
                string dude = box.Tag.ToString().Replace("PlayerData\\PlayerPhotos\\", "").Replace(".jpg", "");
                //remove leading zeros
                while (dude.Length > 1 && dude[0] == '0')
                    dude = dude.Substring(1);
                if (dude.Contains("NoPhoto"))
                    dude = "4";
                // The below is used to classify photos; initially used for skin color, could be used for
                MouseEventArgs mea = e as MouseEventArgs;
                if (mea.Button == MouseButtons.Right)
                    rightClickPlayers.Add(dude);
                else if (mea.Button == MouseButtons.Left)
                    leftClickPlayers.Add(dude);
                else if (mea.Button == MouseButtons.Middle)
                    middleClickPlayers.Add(dude);
            }
            else
            {
                string path = box.Tag.ToString();
                int slashIndex = path.LastIndexOf(System.IO.Path.DirectorySeparatorChar);
                this.SelectedFace = path.Substring(slashIndex + 1).Replace(".jpg", "");
                this.DialogResult = DialogResult.OK;
            }
        }

        private string GetSpecialString()
        {
            StringBuilder sb = new StringBuilder(1000);
            sb.Append("{\n");
            sb.Append("   {\"key\": \"leftClickPlayers\",\"values\":[");
            sb.Append(String.Join(",", leftClickPlayers.ToArray()));
            sb.Append("]},\n");
            sb.Append("   {\"key\": \"rightClickPlayers\",\"values\":[");
            sb.Append(String.Join(",", rightClickPlayers.ToArray()));
            sb.Append("]},\n");
            sb.Append("   {\"key\": \"middleClickPlayers\",\"values\":[");
            sb.Append(String.Join(",", middleClickPlayers.ToArray()));
            sb.Append("]}\n}");
            return sb.ToString();
        }

        private int resizeAmount = 0;

        private void mPictureBoxPanel_Resize(object sender, EventArgs e)
        {
            if (DoResizePictureBoxPanel())
            {
                System.Diagnostics.Debugger.Log(1, "Resize", resizeAmount++ + "Resizing & re-populating...\n");
                ReleasePictureBoxes();
                PopulatePhotosBoxes();
                UpdateScrollbarLargeChange();
                OnStartingPictureIndexChanged();
            }
        }

        private bool DoResizePictureBoxPanel()
        {
            bool retVal = false;
            int pbAcross = mPictureBoxPanel.Width / pbWidth;
            int pbDown = mPictureBoxPanel.Height / pbHeight;
            int total = pbAcross * pbDown;
            if (mPictureBoxPanel.Controls.Count != total)
                retVal = true;
            return retVal;
        }

        private void mSkinComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string choice = mCategoryComboBox.SelectedItem.ToString();
            if (choice == "All")
                mPhotoFiles = mAllPhotoFiles;
            else
                mPhotoFiles = mCategories.GetPhotos(choice);

            if (mPhotoScrollBar.Value != 0)
                mPhotoScrollBar.Value = 0;
            else
                mPhotoScrollBar_ValueChanged(mPhotoScrollBar, EventArgs.Empty);
        }

        private void mShowNumbersCheckbox_Click(object sender, EventArgs e)
        {
            foreach (Control c in mPictureBoxPanel.Controls)
                c.Invalidate();
        }

        private void runSpecialActionItem_Click(object sender, EventArgs e)
        {
            if( classifyMode.Checked)
                MessageForm.ShowMessage("Player stuff", GetSpecialString());

            //WriteBigfaceImage();
            //MessageForm.ShowMessage("Skin map", mTriSkinMap.ToJson() );
            //MessageBox.Show("Modify 'runSpecialActionItem_Click' function to have this do something.");
        }

        private void showUnclassifiedPhotosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageForm.ShowMessage("Unclassified photos", GetUnclassifiedPlayers());
        }

        private string GetUnclassifiedPlayers()
        {
            List<int> classifiedPlayers = new List<int>();
            foreach( GenericArrayObj dude in mCategories.categories)
                classifiedPlayers.AddRange(dude.values);
            FaceForm.RemoveDups(classifiedPlayers);

            List<string> classifiedPlayerPics = new List<string>(FaceForm.GetPictures(classifiedPlayers));
            StringBuilder builder = new StringBuilder();

            foreach (string pfile in mAllPhotoFiles)
            {
                if (classifiedPlayerPics.IndexOf(pfile) == -1)
                {
                    builder.Append(pfile);
                    builder.Append("\r\n");
                }
            }
            return builder.ToString();
        }

        private void FaceForm_Click(object sender, EventArgs e)
        {
            MouseEventArgs mea = e as MouseEventArgs;
            if (mea != null && mea.Button == MouseButtons.Right) // show context menu
            {
                this.contextMenuStrip1.Show(this, mea.Location);
            }
        }

        private void WriteBigfaceImage()
        {
            // actual size of player face image = 94x107
            int picWidth  = 94;
            int picHeight = 107;

            int numberOfColums = 10;
            string[] files = System.IO.Directory.GetFiles(sFaceDir, "*.jpg");
            Image dude = Image.FromFile(files[0]);

            int numberOfRows = files.Length / numberOfColums;
            numberOfRows++;
            string toolTip = "";
            Bitmap bmp = new Bitmap(numberOfColums * picWidth, numberOfRows * picHeight);
            Graphics g = Graphics.FromImage(bmp);
            
            int index = 0;
            Rectangle srcRect  = new Rectangle(0, 0, dude.Width, dude.Height);
            Rectangle destRect = new Rectangle(0, 0, picWidth, picHeight);
            FaceFormToolTipObj tip = null;
            FaceFormToolTipList list = new FaceFormToolTipList();
            int slashIndex = 0;

            for (int row = 0; row < numberOfRows; row++)
            {
                for (int col = 0; col < numberOfColums; col++)
                {
                    if (index < files.Length)
                    {
                        destRect.X = col * picWidth;
                        destRect.Y = row * picHeight;
                        dude = Image.FromFile(files[index]);
                        g.DrawImage(dude, destRect, srcRect, GraphicsUnit.Pixel);

                        tip = new FaceFormToolTipObj();
                        slashIndex = files[index].LastIndexOf(System.IO.Path.DirectorySeparatorChar);
                        tip.Location = new Rectangle(destRect.X, destRect.Y, destRect.Width, destRect.Height);
                        toolTip = files[index].Substring(slashIndex + 1).Replace(".jpg", "");// image number
                        if (DataMap.ReversePhotoMap.ContainsKey(toolTip))
                            tip.Tip = string.Concat(toolTip, "\r\n", DataMap.ReversePhotoMap[toolTip]);
                        else
                            tip.Tip = toolTip;
                        list.List.Add(tip);
                        index++;
                    }
                }
            }

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(FaceFormToolTipList));
            using (System.IO.FileStream stream = new System.IO.FileStream(".\\FaceData.json", System.IO.FileMode.OpenOrCreate))
            {
                ser.WriteObject(stream, list);
            }
            bmp.Save(".\\BigFaces.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        private void classifyMode_Click(object sender, EventArgs e)
        {
            classifyMode.Checked = !classifyMode.Checked;
            if (!classifyMode.Checked)
            {
                if (rightClickPlayers.Count + leftClickPlayers.Count + middleClickPlayers.Count > 0 &&
                    MessageBox.Show("Clear clicked classifications?", "Clear?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    rightClickPlayers.Clear();
                    leftClickPlayers.Clear();
                    middleClickPlayers.Clear();
                }
            }
        }/**/

        public static string[] GetPictures(List<int> nums)
        {
            string[] retVal = new string[nums.Count];
            for (int i = 0; i < retVal.Length; i++)
            {
                retVal[i] = String.Format("{0}\\{1:D4}.jpg", FaceForm.sFaceDir, nums[i]);
            }
            return retVal;
        }

        public static void RemoveDups(List<int> dude)
        {
            if (dude != null)
            {
                dude.Sort();
                for (int i = dude.Count - 1; i > 1; i--)
                {
                    if (dude[i] == dude[i - 1])
                        dude.RemoveAt(i);
                }
            }
        }
    }

    [DataContract]
    public class GenericArrayArray
    {
        [DataMember]
        public string comment { get; set; }

        [DataMember]
        public GenericArrayObj[] categories { get; set; }

        public static GenericArrayArray FromFile(string file)
        {
            GenericArrayArray retVal = null;
            using (System.IO.FileStream fs = new System.IO.FileStream(file, System.IO.FileMode.Open))
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(GenericArrayArray));
                retVal = (GenericArrayArray)ser.ReadObject(fs);
            }
            retVal.RemoveDups();
            return retVal;
        }
        public void RemoveDups()
        {
            List<int> cur = null;
            for (int i = 0; i < this.categories.Length; i++)
            {
                cur = new List<int>(this.categories[i].values);
                FaceForm.RemoveDups(cur);
                this.categories[i].values = cur.ToArray();
            }
        }

        public string ToJson()
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(GenericArrayArray));
            StringBuilder builder = new StringBuilder();
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, this);
            String retVal = System.Text.Encoding.Default.GetString(ms.ToArray());
            return retVal;
        }

        public string[] GetPhotos(string key)
        {
            string[] retVal = null;
            GenericArrayObj target = null;
            foreach (GenericArrayObj current in this.categories)
            {
                if (key == current.key)
                {
                    target = current;
                    break;
                }
            }
            if (target != null)
            {
                List<int> pictures = new List<int>(target.values);
                FaceForm.RemoveDups(pictures);
                retVal = FaceForm.GetPictures(pictures);
            }
            return retVal;
        }
    }

    [DataContract]
    public class GenericArrayObj
    {
        [DataMember]
        public int[] values { get; set; }

        [DataMember]
        public string key { get; set; }
    }


    [DataContract]
    public class FaceFormToolTipList
    {
        public FaceFormToolTipList()
        {
            List = new List<FaceFormToolTipObj>();
        }

        [DataMember]
        public List<FaceFormToolTipObj> List { get; set; }
    }

    [DataContract]
    public class FaceFormToolTipObj
    {
        [DataMember]
        public string Tip { get; set; }

        [DataMember]
        public Rectangle Location { get; set; }
    }
}
