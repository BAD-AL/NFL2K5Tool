using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace NFL2K5Tool
{
    public partial class PhotoChooser : StringSelectionControl
    {
        public PhotoChooser()
        {
            InitializeComponent();
            this.ValueChanged += new EventHandler(PhotoChooser_ValueChanged);
            //ComboBox box =  this.Controls.Find("mComboBox", false) as ComboBox; 
        }

        protected override void OnEnter(EventArgs e)
        {
            this.Size = new System.Drawing.Size(126, 155);
            base.OnEnter(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            Size = new System.Drawing.Size(126, 48);
            base.OnLeave(e);
        }

        void PhotoChooser_ValueChanged(object sender, EventArgs e)
        {
            string dude = DataMap.PhotoMap[this.Value];
            string path = String.Format("PlayerData\\Thumbnails\\{0}.jpg", dude);
            string noPhoto = "PlayerData\\Thumbnails\\0004.jpg";
            if (System.IO.File.Exists(path))
                this.mPictureBox.ImageLocation = path;
            else if (System.IO.File.Exists(noPhoto))
                this.mPictureBox.ImageLocation = noPhoto;
        }
    }
}
