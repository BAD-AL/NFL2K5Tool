using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace NFL2K5Tool
{
    public partial class FaceForm : Form
    {

        const int pbHeight = 50;
        const int pbWidth = 50;

        internal static string sFaceDir = "PlayerData\\PlayerPhotos";
        private string[] mPhotoFiles = null;
        private string[] mAllPhotoFiles = null;
        private SkinObj Skin_Photo_map = null;
        private FaceFormToolTipList Tool_Tip_List = null;

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
            mAllPhotoFiles = System.IO.Directory.GetFiles(sFaceDir, "*.jpg");
            mPhotoFiles = mAllPhotoFiles;
            PopulatePhotosBoxes();
            StartingPictureIndex = 0;
            mPhotoScrollBar.ValueChanged += new EventHandler(mPhotoScrollBar_ValueChanged);

            using (System.IO.FileStream fs = new System.IO.FileStream("PlayerData\\Skin_Photo_map.json", System.IO.FileMode.Open))
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(SkinObj));
                Skin_Photo_map = (SkinObj)ser.ReadObject(fs);
            }
            using (System.IO.FileStream fs = new System.IO.FileStream("PlayerData\\FaceData.json", System.IO.FileMode.Open))
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(FaceFormToolTipList));
                Tool_Tip_List = (FaceFormToolTipList)ser.ReadObject(fs);
            }
        }

        void mPhotoScrollBar_ValueChanged(object sender, EventArgs e)
        {
            StartingPictureIndex = mPhotoScrollBar.Value * (mPictureBoxPanel.Width  / pbWidth) ;
        }
        // pbsize = 50,50

        private void OnStartingPictureIndexChanged()
        {
            mPictureBoxPanel.SuspendLayout();
            for (int i = 0; i < mPictureBoxList.Count; i++)
            {
                if (mPhotoFiles.Length > (i + StartingPictureIndex))
                    mPictureBoxList[i].Image = StaticUtils.GetImageFromPath( mPhotoFiles[i + StartingPictureIndex]);
                else
                    mPictureBoxList[i].ImageLocation = null;
            }
            mPictureBoxPanel.ResumeLayout();
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
                }
                currentY += pbHeight;
            }
            mPhotoScrollBar.Minimum = 0;
            mPhotoScrollBar.Maximum = (mPhotoFiles.Length /pbAcross)+3; // should be Total number of rows possible
            mPictureBoxPanel.ResumeLayout();
        }

        void box_Click(object sender, EventArgs e)
        {
            PictureBox box = sender as PictureBox;
            int slashIndex = box.ImageLocation.LastIndexOf(System.IO.Path.DirectorySeparatorChar);
            this.SelectedFace = box.ImageLocation.Substring(slashIndex+1).Replace(".jpg", "");
            this.DialogResult = DialogResult.OK;

        }


        private void mPictureBoxPanel_Resize(object sender, EventArgs e)
        {

        }

        private void mLastNameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void mSkinComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string choice = mSkinComboBox.SelectedItem.ToString();
            switch (choice)
            {
                case "All": mPhotoFiles = mAllPhotoFiles; break;
                case "Black":
                    mPhotoFiles = Skin_Photo_map.GetBlackPhotos();
                    break;
                case "Hispanic":
                case "Pacific Islander":
                case "White":
                    mPhotoFiles = Skin_Photo_map.GetWhitePhotos();
                    break;
            }
            if (mPhotoScrollBar.Value != 0)
                mPhotoScrollBar.Value = 0;
            else
                mPhotoScrollBar_ValueChanged(mPhotoScrollBar, EventArgs.Empty);
        }

        /*private void WriteBigfaceImage()
        {
            string[] files = System.IO.Directory.GetFiles(sFaceDir, "*.jpg");
            Image dude = Image.FromFile(files[0]);
            // 50,50
            int numberOfRows = files.Length / 10;
            numberOfRows++;
            string toolTip = "";
            Bitmap bmp = new Bitmap(500, numberOfRows * 50);
            Graphics g = Graphics.FromImage(bmp);
            
            int index = 0;
            Rectangle srcRect = new Rectangle(0, 0, dude.Width, dude.Height);
            Rectangle destRect = new Rectangle(0, 0, 50, 50);
            FaceFormToolTipObj tip = null;
            FaceFormToolTipList list = new FaceFormToolTipList();
            int slashIndex = 0;

            for (int row = 0; row < numberOfRows; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    if (index < files.Length)
                    {
                        destRect.X = col * 50;
                        destRect.Y = row * 50;
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
        }/**/

    }

    [DataContract]
    public class SkinObj
    {
        [DataMember]
        public int[] Skin1 { get; set; }
        [DataMember]
        public int[] Skin2 { get; set; }
        [DataMember]
        public int[] Skin3 { get; set; }
        [DataMember]
        public int[] Skin4 { get; set; }
        [DataMember]
        public int[] Skin5 { get; set; }
        [DataMember]
        public int[] Skin6 { get; set; }
        [DataMember]
        public int[] Skin7 { get; set; }
        [DataMember]
        public int[] Skin8 { get; set; }
        [DataMember]
        public int[] Skin9 { get; set; }
        [DataMember]
        public int[] Skin10 { get; set; }
        [DataMember]
        public int[] Skin11 { get; set; }
        [DataMember]
        public int[] Skin12 { get; set; }
        [DataMember]
        public int[] Skin13 { get; set; }
        [DataMember]
        public int[] Skin14 { get; set; }
        [DataMember]
        public int[] Skin15 { get; set; }
        [DataMember]
        public int[] Skin16 { get; set; }
        [DataMember]
        public int[] Skin17 { get; set; }
        [DataMember]
        public int[] Skin18 { get; set; }
        [DataMember]
        public int[] Skin19 { get; set; }
        [DataMember]
        public int[] Skin20 { get; set; }
        [DataMember]
        public int[] Skin21 { get; set; }
        [DataMember]
        public int[] Skin22 { get; set; }

        //White guys: 1,9,10,17
        //Black guys: 2,3,4,5,6,11,12,13,14,18,19,20,21,22,
        public string[] GetWhitePhotos()
        {
            List<int> pictures = new List<int>();
            pictures.AddRange(Skin1);
            pictures.AddRange(Skin9);
            pictures.AddRange(Skin10);
            pictures.AddRange(Skin17);
            return GetPictures(pictures);
        }

        public string[] GetBlackPhotos()
        {
            List<int> pictures = new List<int>();
            pictures.AddRange(Skin2);
            pictures.AddRange(Skin3);
            pictures.AddRange(Skin4 );
            pictures.AddRange(Skin5 );
            pictures.AddRange(Skin6);
            pictures.AddRange(Skin11);
            pictures.AddRange(Skin12);
            pictures.AddRange(Skin13);
            pictures.AddRange(Skin14);
            pictures.AddRange(Skin18);
            pictures.AddRange(Skin19);
            pictures.AddRange(Skin20);
            pictures.AddRange(Skin21);
            pictures.AddRange(Skin22);
            return GetPictures(pictures);
        }

        private string[] GetPictures(List<int> nums)
        {
            string[] retVal = new string[nums.Count];
            for (int i=0; i < retVal.Length; i++)
            {
                //retVal[i] = String.Concat(FaceForm.sFaceDir,"\\", nums[i].ToString(), ".jpg");
                retVal[i] = String.Format("{0}\\{1:D4}.jpg", FaceForm.sFaceDir, nums[i]);
            }
            return retVal;
        }
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
