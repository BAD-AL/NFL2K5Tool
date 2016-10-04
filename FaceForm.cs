using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NFL2K5Tool
{
    public partial class FaceForm : Form
    {
        public FaceForm()
        {
            InitializeComponent();
            PopulatePhotos();
        }
        // pbsize = 50,50

        private void PopulatePhotos()
        {
            string faceDir = "PlayerData\\PlayerPhotos";
            mPictureBoxPanel.SuspendLayout();

            string[] files = System.IO.Directory.GetFiles(faceDir);
            foreach (string file in files)
            {
                if (!file.StartsWith("_"))
                {
                    //AddPictureBox(file); Waaaaaay slow
                }
            }
            mPictureBoxPanel.ResumeLayout();
        }

        int currentX = 0;
        int currentY = 0;
        const int pbHeight = 50;
        const int pbWidth  = 50;

        private PictureBox AddPictureBox(string fileName)
        {
            PictureBox box = new PictureBox();
            box.SizeMode = PictureBoxSizeMode.StretchImage;
            box.ImageLocation = fileName;
            int index = fileName.LastIndexOf(System.IO.Path.PathSeparator);
            box.Tag = fileName.Substring(index + 1);
            if (currentX > mPictureBoxPanel.Width - pbWidth)
            {
                currentX = 0;
                currentY += pbHeight;
            }
            box.Bounds = new Rectangle(currentX, currentY, pbWidth, pbHeight);
            box.Parent = mPictureBoxPanel;

            currentX += pbWidth;
            return box;
        }

    }
}
