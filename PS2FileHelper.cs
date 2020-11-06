using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ARMaxWrapper;

namespace NFL2K5Tool
{
    public class PS2FileHelper: IDisposable
    {
        public string Filename
        {
            get;
            private set;
        }
        
        public PS2FileHelper(string filename)
        {
            this.Filename = filename;

            int result = ARMaxNativeMethods.InitMaxSave();
            if (result == 0)
            {
                result = ARMaxNativeMethods.LoadSave(filename);

                Console.WriteLine("#ARMax version: {0}, Number of files:{1}; RootDir: {2}",
                    ARMaxNativeMethods.DLLVersion(),
                    ARMaxNativeMethods.NumberOfFiles()
                    ,this.RootDir
                    );
            }
            else
                Console.Error.WriteLine("#Error calling 'ARMaxNativeMethods.InitMaxSave()' result:{0}", result);
        }

        public string RootDir
        {
            get
            {
                string retVal = null;
                StringBuilder buff = new StringBuilder(256);
                int result = ARMaxNativeMethods.GetRootDir( buff, 256);
                if (result == 0)
                    retVal = buff.ToString();
                else
                    Console.Error.WriteLine("Error calling 'ARMaxNativeMethods.GetRootDir()' result:{0}", result);
                return retVal;
            }
        }

        List<string> mFilesInSave = null;

        public List<string> FilesInSave
        {
            get
            {
                if (mFilesInSave == null)
                {
                    mFilesInSave = new List<string>();
                    StringBuilder buff = new StringBuilder(256);
                    int numFiles = ARMaxNativeMethods.NumberOfFiles();
                    int fileSize = -1;
                    int result = 0;
                    for (int i = 1; i < numFiles+1; i++) // there is no 0th file
                    {
                        try
                        {
                            result = ARMaxNativeMethods.FileDetails(i, buff, 256, ref fileSize);
                            mFilesInSave.Add(buff.ToString());
                        }
                        catch (Exception)
                        {
                            Console.Error.WriteLine("Error calling 'ARMaxNativeMethods.GetRootDir()' LastError:{0}",
                                Marshal.GetLastWin32Error()
                            );
                            mFilesInSave.Add(i + ":Error!");
                        }
                        buff.Length = 0;
                    }
                }
                return mFilesInSave;
            }
        }

        public bool HasFile(string filename)
        {
            bool retVal = false;
            foreach (string file in this.FilesInSave)
            {
                if (file == filename)
                {
                    retVal = true;
                    break;
                }
            }
            return retVal;
        }

        public string MainSaveFileName
        {
            get
            {
                string retVal = null;
                foreach (string file in this.FilesInSave)
                {
                    if (file.StartsWith("BASLUS"))
                    {
                        retVal = file;
                        break;
                    }
                }
                return retVal;
            }
        }

        public bool ReplaceFile(string internalFileName, string pathToFile)
        {
            int result = -1;
            if (HasFile(internalFileName))
            {
                result = ARMaxNativeMethods.ReplaceFileInSave(internalFileName, pathToFile);
            }
            return (result == 0);
        }

        public bool ExtractFile(string internalFile, string pathToExtractTo)
        {
            bool retVal = false;
            int index = FilesInSave.IndexOf(internalFile);
            if (index > -1)
            {
                index++;
                int result = ARMaxNativeMethods.ExtractAFile(index, pathToExtractTo);
                retVal = (result == 0);
            }
            return retVal;
        }

        public void ExtractPS2SaveContents(string pathToExtractTo)
        {
            string dirName = pathToExtractTo;
            if (!dirName.EndsWith("\\"))
                dirName += "\\";

            if (Directory.Exists(dirName))
                Directory.Delete(dirName, true);
            Directory.CreateDirectory(dirName);
            int result = -1;
            int numFiles = ARMaxNativeMethods.NumberOfFiles();
            // show the files in the list box
            for (int i = 1; i <= numFiles; i++) // there is no '0'th file
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
        }

        public bool AddFileToSave(string pathToFileToAdd)
        {
            int result = -1;
            if (File.Exists(pathToFileToAdd))
                result = ARMaxNativeMethods.AddFileToSave(pathToFileToAdd);
            return (result == 0);
        }

        public bool DeleteFileFromSave(string filename)
        {
            int result = -1;
            int index = this.FilesInSave.IndexOf(filename);
            if (index > -1)
            {
                result = ARMaxNativeMethods.DeleteFileInSave(index + 1);
            }
            return (result == 0);
        }

        public bool SaveMaxFileAs(string filename)
        {
            int result = ARMaxNativeMethods.SaveMaxFile(filename);
            return result == 0;
        }

        #region IDisposable Members

        public void Dispose()
        {
            ARMaxNativeMethods.FreeMaxSave();
        }

        #endregion
    }
}
