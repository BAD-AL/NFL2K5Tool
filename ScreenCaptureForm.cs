using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NFL2K5Tool
{
    public partial class ScreenCaptureForm : Form
    {

        public ScreenCaptureForm()
        {
            InitializeComponent();
            mLocationTextBox.Text = System.IO.Directory.GetCurrentDirectory() + "\\ImageCapture";
        }

        private Image TakeSnap(int x, int y, int width, int height)
        {
            Rectangle rect = new Rectangle(x, x, width, height);
            Bitmap bmp = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bmp);
            g.CopyFromScreen(x, y, 0, 0, bmp.Size, CopyPixelOperation.SourceCopy);
            //bmp.Save(fileName, ImageFormat.Jpeg);
            return bmp;
        }

        private Image TakeSnap()
        {
            return TakeSnap((int)mXUpDown.Value, (int)mYUpDown.Value, (int)mWidthUpDown.Value, (int)mHeightUpDown.Value);
        }

        int imageNumber = -1;

        private string GetFileName()
        {
            string ret = "Capture1.jpg";
            if (imageNumber == -1)
            {
                if (!System.IO.Directory.Exists(mLocationTextBox.Text))
                    System.IO.Directory.CreateDirectory(mLocationTextBox.Text);

                string[] files = System.IO.Directory.GetFiles(mLocationTextBox.Text);
                imageNumber = files.Length;
            }
            imageNumber++;
            ret = String.Concat(mLocationTextBox.Text,"\\Capture", imageNumber.ToString(), ".jpg");
            return ret;
        }

        private void mCaptureButton_Click(object sender, EventArgs e)
        {
            Image img = TakeSnap();
            mPictureBox.Image = img;
            string fileName = GetFileName();
            img.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        private void upDown_ValueChanged(object sender, EventArgs e)
        {
            if (mWidthUpDown.Value > mPictureBox.Width)
                mPictureBox.Width = (int)mWidthUpDown.Value;

            if (mHeightUpDown.Value > mPictureBox.Height)
                mPictureBox.Height = (int)mHeightUpDown.Value;

            mPictureBox.Image = TakeSnap();
        }

        private void mPictureBox_Click(object sender, EventArgs e)
        {
            mPictureBox.Image = TakeSnap();
            if( checkBox1.Checked)
                mPictureBox.Image.Save(GetFileName(), System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mPictureBox.Image.Save(GetFileName(), System.Drawing.Imaging.ImageFormat.Jpeg);
        }
    }
}
