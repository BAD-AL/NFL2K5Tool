using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using AutoIt;
using System.IO;

namespace ImageCapture
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int mCaptureNumber = 0;

        private void mCaptureButton_Click(object sender, EventArgs e)
        {
            CaptureIt();
        }

        private void CaptureIt()
        {
            Image tmp = null;
            Bitmap bmp = new Bitmap((int)mWidthSpinner.Value, (int)mHeightSpinner.Value, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bmp);
            if (AutoItX.WinExists(mWindowNameTextBox.Text, "") != 0)
            {
                Rectangle r = AutoItX.WinGetPos(mWindowNameTextBox.Text, "");
                g.CopyFromScreen(r.Left + (int)mOffsetXSpinner.Value, r.Top + (int)mOffsetYSpinner.Value,
                    0, 0,
                    bmp.Size, CopyPixelOperation.SourceCopy);
                tmp = pictureBox1.Image;
                pictureBox1.Image = bmp;
                if (mSaveToFileCheckbox.Checked)
                {
                    string fileName = GetFileName();
                    bmp.Save(fileName, ImageFormat.Png);
                    this.Text = fileName.Substring(fileName.LastIndexOf('\\') + 1);
                }
            }
            else
                MessageBox.Show("Window + '" + mWindowNameTextBox.Text + "' does not exist.");
            g.Dispose();
            if (tmp != null)
                tmp.Dispose();
        }

        private string GetFileName()
        {
            string path = String.Concat(
                System.IO.Path.GetFullPath("."), "\\Capture\\" , (mCaptureNumber++) , ".png");
            return path;
        }

        private void mSizePlayerButton_Click(object sender, EventArgs e)
        {
            //AutoItX.WinMove(mWindowNameTextBox.Text,"", 0, 0, 568, 464);
            //PlayerForm form = new PlayerForm();
            //form.Show();
            //int faceWidth = 94;
            //int faceHeight = 107;
            //Image face = new Bitmap(faceWidth, faceHeight);
            //Graphics g = Graphics.FromImage(face);
            //Rectangle destRect = new Rectangle(0, 0, faceWidth, faceHeight);
            //g.DrawImage(pictureBox1.Image, destRect, 9, 6, faceWidth, faceHeight, GraphicsUnit.Pixel);
            //pictureBox2.Image = face;

            pictureBox2.Image = GetFace(pictureBox1.Image);
        }

        private Image GetFace(Image bigImage)
        {
            int faceWidth = 94;
            int faceHeight = 107;
            int startX = 9;
            int startY = 6;
            Image face = new Bitmap(faceWidth, faceHeight);
            Graphics g = Graphics.FromImage(face);
            Rectangle destRect = new Rectangle(0, 0, faceWidth, faceHeight);
            g.DrawImage(bigImage, destRect, startX, startY, faceWidth, faceHeight, GraphicsUnit.Pixel);
            g.Dispose();
            return face;
        }

        private Image GetNameImage(Image bigImage)
        {
            int faceWidth = 321;
            int faceHeight = 23;
            int startX = 134;
            int startY = 0;
            Image face = new Bitmap(faceWidth, faceHeight);
            Graphics g = Graphics.FromImage(face);
            Rectangle destRect = new Rectangle(0, 0, faceWidth, faceHeight);
            g.DrawImage(bigImage, destRect, startX, startY, faceWidth, faceHeight, GraphicsUnit.Pixel);
            g.Dispose();
            return face;
        }

        private void mOffsetYSpinner_ValueChanged(object sender, EventArgs e)
        {
            CaptureIt();
        }

        private void namesFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new NameHelperForm().Show();
        }

        System.Windows.Forms.Timer mTimer = null;

        private void mTimedCaptureButton_Click(object sender, EventArgs e)
        {
            if (mTimer == null)
            {
                mTimer = new Timer();
                mTimer.Interval = 250;
                mTimer.Tick += new EventHandler(mTimer_Tick);
                mTimer.Start();
            }
            else
            {
                mTimer.Stop();
                mTimer.Tick -= new EventHandler(mTimer_Tick);
                mTimer.Dispose();
                mTimer = null;
            }
        }

        void mTimer_Tick(object sender, EventArgs e)
        {
            CaptureIt();
        }


        private void mDeleteGarbageImages_Click(object sender, EventArgs e)
        {
            //DeleteGarbageImages();
            ThinFiles();
        }

        private void DeleteGarbageImages()
        {
            List<string> deleteThese = new List<string>(5000);
            string path = String.Concat(System.IO.Path.GetFullPath("."), "\\Capture\\");
            string[] files = Directory.GetFiles(path, "*.png");

            Bitmap prev = null;
            Bitmap current = null;
            string file = "";
            for(int i=0; i < files.Length; i++)
            {
                file = files[i];
                current = Bitmap.FromFile(file) as Bitmap;
                if (IsNoPhoto(current) || IsGrayPhoto(current))
                {
                    deleteThese.Add(file);
                    this.Text = deleteThese.Count+ " Deleting files..";
                }                
                if( prev != null) prev.Dispose();
                prev = current;
            }
            if (deleteThese.Count > 0)
            {
                StringBuilder newFile = new StringBuilder();
                Console.WriteLine("deleting files" + deleteThese.Count);
                foreach (string bad in deleteThese)
                {
                    newFile.Length = 0;
                    newFile.Append(bad);
                    newFile.Insert(bad.LastIndexOf('\\') + 1, "Fail\\");
                    File.Move(bad, newFile.ToString());
                }
            }
        }

        private Bitmap[] mBadImages = null;

        Rectangle mFaceRect = new Rectangle(66,55, 2, 13);

        private Point FacesAreEqual(Bitmap b1, Bitmap b2)
        {
            int stopX = mFaceRect.X + mFaceRect.Width;
            int stopY = mFaceRect.Y + mFaceRect.Height;
            for (int x = mFaceRect.X; x < stopX; x++)
            {
                for (int y = mFaceRect.Y; y < stopY; y++)
                {
                    if (b1.GetPixel(x, y).ToArgb() != b2.GetPixel(x, y).ToArgb())
                    {
                        return new Point(x, y);
                    }
                }
            }
            return new Point(-1,-1);
        }

        private bool IsBadImage(Bitmap img)
        {
            foreach (Bitmap bad in mBadImages)
                if (FacesAreEqual(bad, img).Y == -1)
                    return true;
            return false;
        }
        Rectangle mFacePortion2 = new Rectangle(34, 52, 42, 71);
        
        private bool IsNoPhoto(Bitmap img)
        {
            int targetHue = 0;
            float hue = 0;
            int xMax = mFacePortion2.X + mFacePortion2.Width;
            int yMax = mFacePortion2.Y + mFacePortion2.Height;
            for (int x = mFacePortion2.X; x < xMax; x++)
            {
                for (int y = mFacePortion2.Y; y < yMax; y++)
                {
                    if (x < img.Width && y < img.Height)
                    {
                        hue = img.GetPixel(x, y).GetHue();
                        if (hue == 0)
                            targetHue++;
                    }
                }
            }
            float area = mFacePortion2.Height * mFacePortion2.Width;
            float ratio = targetHue / area;
            debugLabel.Text = "0hue ratio = " + ratio;
            return (ratio > 0.09);
        }

        Rectangle mFacePortion = new Rectangle(30, 40, 30, 20);
        
        private bool IsGrayPhoto(Bitmap img)
        {
            int bottomGray = 62;
            int upperGray = 77;
            int grayCount = 0;
            float hue = 0;
            int xMax = mFacePortion.X + mFacePortion.Width;
            int yMax = mFacePortion.Y + mFacePortion.Height;
            for (int x = mFacePortion.X; x < xMax; x++)
            {
                for (int y = mFacePortion.Y; y < yMax; y++)
                {
                    hue = img.GetPixel(x, y).GetHue();
                    if (hue > bottomGray && hue < upperGray)
                        grayCount++;
                }
            }
            float area = mFacePortion.Height * mFacePortion.Width;
            float ratio = grayCount / area;
            debugLabel.Text = "Gray ratio = " + ratio;
            return ratio > 0.19;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Bitmap bmp = pictureBox1.Image as Bitmap;
            if (bmp != null)
            {
                Color c = bmp.GetPixel(e.X, e.Y);
                Text = "" + c.GetHue();//0-360
            }
        }

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                SetPictureBox1Image(dlg.FileName);
            }
        }

        private void SetPictureBox1Image(string fileName)
        {
            Image tmp = pictureBox1.Image;
            Bitmap b = Bitmap.FromFile(fileName) as Bitmap;
            Text = String.Concat("No Photo == ", IsNoPhoto(b), "Is Gray == " + IsGrayPhoto(b));
            
            pictureBox1.Image = b;
            pictureBox1.Tag = fileName;
            if (tmp != null)
                tmp.Dispose();
        }

        private void debugLabel_Click(object sender, EventArgs e)
        {
            string[] files = null;
            if (this.Tag == null && pictureBox1.Tag != null)
            {
                string dir = pictureBox1.Tag.ToString();
                dir = dir.Substring(0, dir.LastIndexOf('\\') + 1);
                files = Directory.GetFiles(dir, "*.png");
                Array.Sort(files);
                debugLabel.Tag = files;
            }
            files = debugLabel.Tag as string[];
            int index = Array.BinarySearch(files, pictureBox1.Tag);
            if (index > -1 && index < files.Length-1)
            {
                SetPictureBox1Image(files[index + 1]);
            }
        }

        private void ThinFiles()
        {
            string moveTo = @"C:\Users\Chris\Documents\Visual Studio 2008\Projects\NFL2K5Tool\Test\ImageCapture\bin\Debug\Capture\Fail\";
            string path = @"C:\Users\Chris\Documents\Visual Studio 2008\Projects\NFL2K5Tool\Test\ImageCapture\bin\Debug\Capture\Thumbnails\";
            String[] faceFiles = Directory.GetFiles(path, "*_1.jpg");
            string file = "";
            string f1 = "";
            string f2 = "";
            for (int i = 0; i < faceFiles.Length; i++)
            {
                file = faceFiles[i];
                f1 = file.Replace("_1", "");
                f2 = f1.Replace("jpg", "png");
                if (File.Exists(f1))
                {
                    File.Delete(f1);
                    File.Move(file, f1);
                }
                if (File.Exists(f2))
                    File.Delete(f2);
            }
        }

        private void portionFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PortionCaptureForm f = new PortionCaptureForm();
            f.Show();
        }

    }
}
