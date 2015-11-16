using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

//https://github.com/icsharpcode/SharpZipLib
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;

namespace NFL2K5Tool
{
    /// <summary>
    /// Static utility functions that I don't want to clutter up other files with.
    /// </summary>
    public static class StaticUtils
    {
        /// <summary>
        /// a place to keep all the processing errors.
        /// </summary>
        public static List<string> Errors = new List<string>();

        /// <summary>
        /// Shows the errors (if any exist)
        /// </summary>
        /// <param name="showToConsole">true to print the errors to the console, 
        /// false to show them in a GUI dialog</param>
        public static void ShowErrors(bool showToConsole)
        {
            if (Errors.Count > 0)
            {
                StringBuilder b = new StringBuilder();
                foreach (string s in Errors)
                {
                    b.Append(s);
                    b.Append("\n");
                }
                if (!showToConsole)
                {
                    ErrorForm form = new ErrorForm();
                    form.ErrorText = b.ToString();
                    form.ShowDialog();
                }
                else
                {
                    Console.Error.WriteLine(b.ToString());
                }
                Errors = new List<string>();
            }
        }

        /// <summary>
        /// Find string 'str' (unicode string) in the data byte array.
        /// </summary>
        /// <param name="str">The string to look for</param>
        /// <param name="data">The data to search through.</param>
        /// <param name="start">where to start in 'data'</param>
        /// <param name="end">Where to end in 'data'</param>
        /// <returns>a list of addresses</returns>
        public static List<long> FindStringInFile(string str, byte[] data, int start, int end)
        {
            return FindStringInFile(str, data, start, end, false);
        }

        /// <summary>
        /// Find string 'str' (unicode string) in the data byte array.
        /// </summary>
        /// <param name="str">The string to look for</param>
        /// <param name="data">The data to search through.</param>
        /// <param name="start">where to start in 'data'</param>
        /// <param name="end">Where to end in 'data'</param>
        /// <param name="nullByte">True to append the null byte at the end.</param>
        /// <returns>a list of addresses</returns>
        public static List<long> FindStringInFile(string str, byte[] data, int start, int end, bool nullByte)
        {
            List<long> retVal = new List<long>();

            if (data != null && data.Length > 80)
            {
                if (start < 0)
                    start = 0;
                if (end > data.Length)
                    end = data.Length - 1;

                int i = 0;
                int length  = str.Length * 2;
                if (nullByte)
                    length += 2;

                byte[] target = new byte[length];
                
                Array.Clear(target, 0, target.Length); // fill with 0's
                foreach (char c in str)
                {
                    target[i] = (byte)c;
                    target[i + 1] = 0;
                    i += 2;
                }
                long num = (long)(end - target.Length);
                for (long num3 = start; num3 < num; num3 += 1L)
                {
                    if (Check(target, num3, data))
                    {
                        retVal.Add(num3);
                    }
                }
            }
            return retVal;
        }

        private static bool Check(byte[] target, long location, byte[] data)
        {
            int i;
            for (i = 0; i < target.Length; i++)
            {
                if (target[i] != data[(int)(checked((IntPtr)(unchecked(location + (long)i))))])
                {
                    break;
                }
            }
            return i == target.Length;
        }

        #region Zip file handling
        /// <summary>
        /// 
        /// </summary>
        /// <remarks> Taken from code sample at:
        /// https://github.com/icsharpcode/SharpZipLib/wiki/Zip-Samples#anchorUnpackFull
        /// </remarks>
        /// <param name="archiveFilenameIn"></param>
        /// <param name="password"></param>
        /// <param name="outFolder"></param>
        public static void ExtractZipFile(string archiveFilenameIn, string password, string outFolder)
        {
            ZipFile zf = null;
            try
            {
                FileStream fs = File.OpenRead(archiveFilenameIn);
                zf = new ZipFile(fs);
                if (!String.IsNullOrEmpty(password))
                {
                    zf.Password = password;     // AES encrypted entries are handled automatically
                }
                foreach (ZipEntry zipEntry in zf)
                {
                    if (!zipEntry.IsFile)
                    {
                        continue;           // Ignore directories
                    }
                    String entryFileName = zipEntry.Name;
                    // to remove the folder from the entry:- entryFileName = Path.GetFileName(entryFileName);
                    // Optionally match entrynames against a selection list here to skip as desired.
                    // The unpacked length is available in the zipEntry.Size property.

                    byte[] buffer = new byte[4096];     // 4K is optimum
                    Stream zipStream = zf.GetInputStream(zipEntry);

                    // Manipulate the output filename here as desired.
                    String fullZipToPath = Path.Combine(outFolder, entryFileName);
                    string directoryName = Path.GetDirectoryName(fullZipToPath);
                    if (directoryName.Length > 0)
                        Directory.CreateDirectory(directoryName);

                    // Unzip file in buffered chunks. This is just as fast as unpacking to a buffer the full size
                    // of the file, but does not waste memory.
                    // The "using" will close the stream even if an exception occurs.
                    using (FileStream streamWriter = File.Create(fullZipToPath))
                    {
                        StreamUtils.Copy(zipStream, streamWriter, buffer);
                    }
                }
            }
            finally
            {
                if (zf != null)
                {
                    zf.IsStreamOwner = true; // Makes close also shut the underlying stream
                    zf.Close(); // Ensure we release resources
                }
            }
        }
        
        public static string UnzipToTempFolder(string archiveFilenameIn, string password)
        {
            string dirName = Path.GetTempPath() + "NFL2K5ToolTmpZipUnpack";

            if (Directory.Exists(dirName))
                Directory.Delete(dirName, true);

            ExtractZipFile(archiveFilenameIn, password, dirName);

            return dirName;
        }

        /// <summary>
        /// Extract a specific file from a zip file.
        /// </summary>
        /// <param name="archiveFilenameIn">the zip file to search.</param>
        /// <param name="password">the password for the file</param>
        /// <param name="fileToExtract">the name of the fuile to extract.</param>
        /// <returns>null if file was not found, byte array if it was successfully extracted.</returns>
        public static byte[] ExtractFileFromZip(string archiveFilenameIn, string password, string fileToExtract)
        {
            byte[] retVal = null;
            string dirName = UnzipToTempFolder(archiveFilenameIn, password);
            string[] files = Directory.GetFiles(dirName, fileToExtract, SearchOption.AllDirectories);
            if (files.Length > 0)
                retVal = File.ReadAllBytes(files[0]);
            
            if (Directory.Exists(dirName))
                Directory.Delete(dirName, true);
            
            return retVal;
        }

        public static void ReplaceFileInArchive(string archiveFilenameIn, string password, string fileToReplace, string newFilePath)
        {
            string dirName = UnzipToTempFolder(archiveFilenameIn, password);
            string[] files = Directory.GetFiles(dirName, fileToReplace, SearchOption.AllDirectories);
            if (files.Length > 0)
                File.Copy(newFilePath, files[0], true);
            
            string outPathname = Path.GetTempFileName();

            FileStream fsOut = File.Create(outPathname);
            ZipOutputStream zipStream = new ZipOutputStream(fsOut);

            zipStream.SetLevel(5); //0-9, 9 being the highest level of compression

            zipStream.Password = password;  // optional. Null is the same as not setting. Required if using AES.

            // This setting will strip the leading part of the folder path in the entries, to
            // make the entries relative to the starting folder.
            // To include the full path for each entry up to the drive root, assign folderOffset = 0.
            int folderOffset = dirName.Length + (dirName.EndsWith("\\") ? 0 : 1);

            CompressFolder(dirName, zipStream, folderOffset);

            zipStream.IsStreamOwner = true; // Makes the Close also Close the underlying stream
            zipStream.Close();
            File.Copy(outPathname, archiveFilenameIn, true);
            File.Delete(outPathname);
        }

        //Taken from https://github.com/icsharpcode/SharpZipLib/wiki/Zip-Samples#anchorUnpackFull
        private static void CompressFolder(string path, ZipOutputStream zipStream, int folderOffset)
        {
            string[] files = Directory.GetFiles(path);

            foreach (string filename in files)
            {
                FileInfo fi = new FileInfo(filename);

                string entryName = filename.Substring(folderOffset); // Makes the name in zip based on the folder
                entryName = ZipEntry.CleanName(entryName); // Removes drive from name and fixes slash direction
                ZipEntry newEntry = new ZipEntry(entryName);
                newEntry.DateTime = fi.LastWriteTime; // Note the zip format stores 2 second granularity

                // Specifying the AESKeySize triggers AES encryption. Allowable values are 0 (off), 128 or 256.
                // A password on the ZipOutputStream is required if using AES.
                //   newEntry.AESKeySize = 256;

                // To permit the zip to be unpacked by built-in extractor in WinXP and Server2003, WinZip 8, Java, and other older code,
                // you need to do one of the following: Specify UseZip64.Off, or set the Size.
                // If the file may be bigger than 4GB, or you do not need WinXP built-in compatibility, you do not need either,
                // but the zip will be in Zip64 format which not all utilities can understand.
                //   zipStream.UseZip64 = UseZip64.Off;
                newEntry.Size = fi.Length;

                zipStream.PutNextEntry(newEntry);

                // Zip the file in buffered chunks
                // the "using" will close the stream even if an exception occurs
                byte[] buffer = new byte[4096];
                using (FileStream streamReader = File.OpenRead(filename))
                {
                    StreamUtils.Copy(streamReader, zipStream, buffer);
                }
                zipStream.CloseEntry();
            }
            string[] folders = Directory.GetDirectories(path);
            foreach (string folder in folders)
            {
                CompressFolder(folder, zipStream, folderOffset);
            }
        }
        #endregion
    }
}
