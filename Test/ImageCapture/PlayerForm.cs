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
    public partial class PlayerForm : Form
    {
        public PlayerForm()
        {
            InitializeComponent();
        }

        private void mSourceTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                mPlayer.URL = mSourceTextBox.Text;
                //mPlayer.Ctlcontrols.play();
            }
        }

        private void mBrowseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                mSourceTextBox.Text = dlg.FileName;
            }
            dlg.Dispose();
        }
    }
}
