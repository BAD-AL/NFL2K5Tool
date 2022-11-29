using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;

namespace NFL2K5Tool
{
    public partial class PCSX2TextureForm : Form
    {
        GamesaveTool mTool = null;

        public PCSX2TextureForm(GamesaveTool tool )
        {
            mTool = tool;
            InitializeComponent();
        }

        private void showYamlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowYaml();
        }

        public void ShowYaml()
        {
            if (mTool != null)
            {
                mTextBox.Text = mTool.GetPlayerPhotoPCSX2Yaml();
            }
        }

        public void ShowBatchFile()
        {
            if (mTool != null)
            {
                mTextBox.Text = mTool.GenPlayerPhotoPCSX2_data_2022_BatchFile();
            }
        }

        private void mClearButton_Click(object sender, EventArgs e)
        {
            mTextBox.Clear();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mTextBox.Clear();
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void checkTexturesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String path = null;
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.Description = "Choose PCSX2 'textures' folder";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                path = dlg.SelectedPath;
            }
            dlg.Dispose();
            if (path != null)
            {
                string filePath = "";
                Regex fileRegex = new Regex("\"(.*)\"");
                MatchCollection mc = fileRegex.Matches(mTextBox.Text);
                StringBuilder sb = new StringBuilder();
                foreach (Match m in mc)
                {
                    filePath = path + "\\" + m.Groups[1].ToString().Replace("/", "\\");
                    if (!File.Exists(filePath))
                    {
                        sb.Append("file '");
                        sb.Append(filePath);

                        //sb.Append(m.Groups[0].ToString());
                        sb.Append("' does not exist\n");
                    }
                }
                MessageForm.ShowMessage("Check these", sb.ToString());
            }
        }

    }
}
