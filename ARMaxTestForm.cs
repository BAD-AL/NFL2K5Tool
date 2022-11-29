using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using ARMaxWrapper;

namespace NFL2K5Tool
{
    /// <summary>
    /// Test form.
    /// Tests for functions:
    ///          DLLVersion * 
    ///          InitMaxSave *
    ///          FreeMaxSave *
    ///          LoadSave *
    ///          NumberOfFiles *
    ///          AddFileToSave
    ///          DeleteFileInSave
    ///          DeleteFileInSaveByName
    ///          SaveMaxFile
    ///          SetRootDir
    ///          GetRootDir
    ///          FileExists * 
    ///          FileExistsInSavePos -
    ///          ReplaceFileInSave
    ///          AddDataAsFile
    ///          FileDetails *
    ///          CopyFileToBuffer 
    ///          ExtractAFile
    ///          ExtractAFileAs
    ///          GetFileSize
    ///          AsciiToSJis
    ///          SJisToAscii
    /// </summary>
    public partial class ARMaxTestForm : Form
    {
        public ARMaxTestForm()
        {
            InitializeComponent();
        }

        private void mSaveFileNameTextBox_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void TextBox_DragDrop(object sender, DragEventArgs e)
        {
            TextBox tb = sender as TextBox;
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files != null && files.Length == 1 && tb != null)
            {
                tb.Text = files[0];
                if (tb == mSaveFileNameTextBox)
                {
                    ShowContants(tb.Text);
                }
            }
        }

        private void mExtractButton_Click(object sender, EventArgs e)
        {
            ExtractFileContents();
        }

        private void mFileExistsButton_Click(object sender, EventArgs e)
        {
            CheckFileExists();
        }

        private void ShowContants(string filename)
        {
            if (filename.EndsWith(".max", StringComparison.InvariantCultureIgnoreCase))
            {
                mInternalFilesListBox.Items.Clear();
                int result = LoadARMaxFile(filename);

                int numFiles = ARMaxNativeMethods.NumberOfFiles();
                StringBuilder buff = new StringBuilder(256);
                int fileSize = -1;
                // show the files in the list box
                for (int i = 1; i < numFiles + 1; i++) // there is no '0'th file
                {
                    try
                    {
                        result = ARMaxNativeMethods.FileDetails(i, buff, 256, ref fileSize);
                        if (result == 0)
                            mInternalFilesListBox.Items.Add(buff.ToString()); // add to the list box items
                        else
                            Console.Error.WriteLine("Error Code = {0}", result);
                    }
                    catch (Exception exc)
                    {
                        Console.Error.WriteLine("Error calling 'ARMaxNativeMethods.GetRootDir()' LastError:{0}\n{1}",
                            System.Runtime.InteropServices.Marshal.GetLastWin32Error()
                            , exc.Message
                            );
                    }
                    buff.Length = 0; // clear out the chars in buff
                }
                ARMaxNativeMethods.FreeMaxSave();
                mExtractButton.Enabled = true;
            }
            else
            {
                MessageBox.Show("This is not a a .max file: "+ filename);
            }
        }

        private static int LoadARMaxFile(string filename)
        {

            int result = ARMaxNativeMethods.InitMaxSave();
            Console.WriteLine("#DLL version: ", ARMaxNativeMethods.DLLVersion());

            if (result != 0)
                throw new Exception("Could not load initialize ARMax DLL!");
            result = ARMaxNativeMethods.LoadSave(filename);
            if (result != 0)
                throw new Exception("Could not load file:" + filename);
            return result;
        }

        private void PackContents(string resultFileName, string folderName)
        {
            string filename = mSaveFileNameTextBox.Text;
            int result = ARMaxNativeMethods.InitMaxSave();
            if (result != 0)
                throw new Exception("Could not load initialize ARMax DLL!");
            result = ARMaxNativeMethods.LoadSave(filename);
            if (result != 0)
                throw new Exception("Could not load file:" + filename);
        }

        private void ExtractFileContents()
        {
            string filename = mSaveFileNameTextBox.Text;
            int result = ARMaxNativeMethods.InitMaxSave();
            if (result != 0)
                throw new Exception("Could not load initialize ARMax DLL!");
            result = ARMaxNativeMethods.LoadSave(filename);
            if (result != 0)
                throw new Exception("Could not load file:" + filename);

            StringBuilder rootDirName= new StringBuilder(256);
            ARMaxNativeMethods.GetRootDir(rootDirName, 256);
            rootDirName.Append("\\");
            string dirName = ".\\" + rootDirName.ToString();

            if (Directory.Exists(dirName))
                Directory.Delete(dirName, true);
            Directory.CreateDirectory(dirName);

            int numFiles = ARMaxNativeMethods.NumberOfFiles();
            // show the files in the list box
            for (int i = 1; i < numFiles + 1; i++) // there is no '0'th file
            {
                try
                {
                    result = ARMaxNativeMethods.ExtractAFile(i, dirName);
                    if (result != 0)
                    {
                        Console.Error.Write("'ARMaxNativeMethods.ExtractAFile' Failed; code = {0}", result);
                    }
                }
                catch (Exception exc)
                {
                    Console.Error.WriteLine("Error calling 'ARMaxNativeMethods.GetRootDir()' LastError:{0}\n{1}",
                        System.Runtime.InteropServices.Marshal.GetLastWin32Error()
                        , exc.Message
                        );
                }
            }
            ARMaxNativeMethods.FreeMaxSave();
        }


        private void CheckFileExists()
        {
            string filename = mSaveFileNameTextBox.Text;
            int result = ARMaxNativeMethods.InitMaxSave();
            if (result != 0)
                throw new Exception("Could not load initialize ARMax DLL!");
            result = ARMaxNativeMethods.LoadSave(filename);
            if (result != 0)
                throw new Exception("Could not load file:" + filename);
            result = ARMaxNativeMethods.FileExistsInSavePos(mFileExistsTextBox.Text);
            if (result > 0)
                mFileExistsLabel.Text = "File Exists";
            else
                mFileExistsLabel.Text = "File Does Not Exist";

            ARMaxNativeMethods.FreeMaxSave();
        }

        private void mAddFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string targetFile = "mySave.max";
                PS2FileHelper helper = new PS2FileHelper(mSaveFileNameTextBox.Text);
                helper.AddFileToSave(dlg.FileName);
                helper.SaveMaxFileAs(targetFile);
                mSaveFileNameTextBox.Text = targetFile;
                ShowContants(mSaveFileNameTextBox.Text);
                helper.Dispose();
            }
            dlg.Dispose();
        }

        private void mDeleteFileButton_Click(object sender, EventArgs e)
        {
            if (this.mInternalFilesListBox.SelectedIndex > -1)
            {
                String fileToRemove = mInternalFilesListBox.Items[mInternalFilesListBox.SelectedIndex].ToString();
                PS2FileHelper helper = new PS2FileHelper(mSaveFileNameTextBox.Text);
                string targetFile = "mySave.max";
                if (helper.DeleteFileFromSave(fileToRemove) && helper.SaveMaxFileAs(targetFile))
                {
                    mSaveFileNameTextBox.Text = targetFile;
                    ShowContants(mSaveFileNameTextBox.Text);
                }

                helper.Dispose();
            }
        }

        private void mInternalFilesListBox_DragDrop(object sender, DragEventArgs e)
        {
            Control tb = sender as Control;
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files != null && files.Length == 1 && tb != null)
            {
                string targetFile = ".\\mySave.max";
                PS2FileHelper helper = new PS2FileHelper(mSaveFileNameTextBox.Text);
                helper.AddFileToSave(files[0]);
                helper.SaveMaxFileAs(targetFile);
                mSaveFileNameTextBox.Text = targetFile;
                ShowContants(mSaveFileNameTextBox.Text);
                helper.Dispose();
            }
        }

        private void mConvertButton_Click(object sender, EventArgs e)
        {
            if (mXboxFileTextBox.Text.Length > 0)
            {
                PS2FileHelper.ConvertXboxSaveToPS2Max(mXboxFileTextBox.Text);
            }
            else
            {
                MessageBox.Show("Drag file into xbox file text box");
            }
        }


    }
}
