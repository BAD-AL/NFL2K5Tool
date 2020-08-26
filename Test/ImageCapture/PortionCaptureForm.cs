using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImageCapture
{
    public partial class PortionCaptureForm : Form, IComparer<string>
    {
        public PortionCaptureForm()
        {
            InitializeComponent();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string path = "Capture\\" + textBox1.Text + ".jpg";
                UpdatePictureBoxPortion();
                if (System.IO.File.Exists(path))
                    MessageBox.Show("File already exists!");
                else 
                    pictureBox2.Image.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        private void UpdatePictureBoxPortion()
        {
            pictureBox2.Image = GetImagePortion(pictureBox1.Image,
                (int)mOffsetXSpinner.Value, (int)mOffsetYSpinner.Value,
                (int)mWidthSpinner.Value, (int)mHeightSpinner.Value);
        }

        private Image GetImagePortion(Image bigImage, int x, int y, int width, int height)
        {
            if (bigImage != null)
            {
                Image img = new Bitmap(width, height);
                Graphics g = Graphics.FromImage(img);
                Rectangle destRect = new Rectangle(0, 0, width, height);
                g.DrawImage(bigImage, destRect, x, y, width, height, GraphicsUnit.Pixel);
                g.Dispose();
                return img;
            }
            return null;
        }

        private void mCaptureButton_Click(object sender, EventArgs e)
        {
            UpdatePictureBoxPortion();
        }

        private List<string> mFileNames = null;

        public List<string> FileNames
        {
            get 
            {
                if (mFileNames == null)
                {
                    mFileNames = new List<string>(System.IO.Directory.GetFiles("Capture\\", "*.png"));
                    mFileNames.Sort(this);
                }
                return mFileNames; 
            }
        }

        private void mPreviousButton_Click(object sender, EventArgs e)
        {
            int index = FileNames.IndexOf(pictureBox1.ImageLocation);
            if (index == FileNames.Count)
                index = 1;
            else if (index == 0)
                index = FileNames.Count;
            pictureBox1.ImageLocation = FileNames[--index];
            this.Text = pictureBox1.ImageLocation;
            UpdatePictureBoxTimer();
        }

        private void mNextButton_Click(object sender, EventArgs e)
        {
            int index = FileNames.IndexOf(pictureBox1.ImageLocation);
            if (index == 0) 
                index = FileNames.Count - 1;
            else if (index == -1) 
                index = 1;
            pictureBox1.ImageLocation = FileNames[++index];
            this.Text = pictureBox1.ImageLocation;
            UpdatePictureBoxTimer();
        }

        private void mWidthSpinner_ValueChanged(object sender, EventArgs e)
        {
            UpdatePictureBoxPortion();
        }

        System.Windows.Forms.Timer mTimer = null;

        private void UpdatePictureBoxTimer()
        {
            if (mTimer == null)
            {
                mTimer = new System.Windows.Forms.Timer();
                mTimer.Interval = 25;
                mTimer.Tick += new EventHandler(mTimer_Tick);
            }
            mTimer.Start();
        }

        void mTimer_Tick(object sender, EventArgs e)
        {
            mTimer.Stop();
            UpdatePictureBoxPortion();
        }

        #region IComparer<string> Members

        public int Compare(string x, string y)
        {
            int x1 = Int32.Parse(x.Replace(".png", "").Replace("Capture\\",""));
            int y1 = Int32.Parse(y.Replace(".png", "").Replace("Capture\\",""));

            return x1.CompareTo(y1);
        }


        #endregion
    }
}
