using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

//https://github.com/icsharpcode/SharpZipLib
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using System.Security.Cryptography;
using System.Drawing;

namespace NFL2K5Tool
{
    /// <summary>
    /// Static utility functions that I don't want to clutter up other files with.
    /// </summary>
    public static class StaticUtils
    {
        private static Dictionary<string,Image> sImageMap = null;
        private static Dictionary<string, Image> ImageMap 
        {
            get
            {
                if (sImageMap == null)
                    sImageMap = new Dictionary<string, Image>();
                return sImageMap;
            }
        }
        private static PlayerParser pp = null;
        /// <summary>
        /// Get an image from the assembly; caches the image.
        /// </summary>
        public static Image GetEmbeddedImage(string file)
        {
            Image ret = null;
            try
            {
                if (pp == null)
                    pp = new PlayerParser("");
                if (ImageMap.ContainsKey(file))
                    ret = ImageMap[file];
                else
                {
                    System.IO.Stream s =
                        pp.GetType().Assembly.GetManifestResourceStream(file);
                    if (s != null)
                        ret = Image.FromStream(s);
                    sImageMap.Add(file, ret);
                }
            }
            catch (Exception e)
            {
                AddError("Error getting image "+ file);
            }
            return ret;
        }

        /// <summary>
        /// Gets an image from the path; caches the image.
        /// </summary>
        public static Image GetImageFromPath(string path)
        {
            Image ret = null;

            if (ImageMap.ContainsKey(path))
                ret = ImageMap[path];
            else
            {
                ret = Image.FromFile(path);
                ImageMap.Add(path, ret);
            }
            return ret;
        }

        #region Error functionality 
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
                    MessageForm form = new MessageForm(System.Drawing.SystemIcons.Error);
                    form.Text = "Error!";
                    form.MessageText = b.ToString();
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
        /// Add an error to the session
        /// </summary>
        /// <param name="error"></param>
        public static void AddError(string error)
        {
            Errors.Add(error);
        }

        #endregion

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
            int length = str.Length * 2;
            if (nullByte)
                length += 2;

            byte[] target = new byte[length];
            int i = 0;
            Array.Clear(target, 0, target.Length); // fill with 0's
            foreach (char c in str)
            {
                target[i] = (byte)c;
                target[i + 1] = 0;
                i += 2;
            }
            return FindByesInFile(target, data, start, end);
        }

        public static List<long> FindPointersToString( string searchString, byte[] saveFile, int start, int end)
        {
            List<long> locs = StaticUtils.FindStringInFile(searchString, saveFile, 0, saveFile.Length);
            List<int> pointers;
            List<long> retVal = new List<long>();

            for (int i = 0; i < locs.Count; i++)
            {
                pointers = FindPointersForLocation(locs[i], saveFile);
                foreach (int dude in pointers)
                {
                    if( dude > start && dude < end)
                        retVal.Add(dude);
                }
            }
            return retVal;
        }

        public static List<int> FindPointersForLocation(long location, byte[] saveFile)
        {
            List<int> pointerLocations = new List<int>();
            int pointer = 0;
            long dataLocation = 0;
            int limit = saveFile.Length - 4;
            for (long i = 0; i < limit; i++)
            {
                pointer = saveFile[i + 3] << 24;
                pointer += saveFile[i + 2] << 16;
                pointer += saveFile[i + 1] << 8;
                pointer += saveFile[i];
                dataLocation = i + pointer - 1;

                if (dataLocation == location)
                {
                    pointerLocations.Add((int)i);
                }
            }
            return pointerLocations;
        }

        /// <summary>
        /// Find an array of bytes in the data byte array.
        /// </summary>
        /// <param name="str">The bytes to look for</param>
        /// <param name="data">The data to search through.</param>
        /// <param name="start">where to start in 'data'</param>
        /// <param name="end">Where to end in 'data'</param>
        /// <returns>a list of addresses</returns>
        public static List<long> FindByesInFile(byte[] target, byte[] data, int start, int end)
        {
            List<long> retVal = new List<long>();

            if (data != null && data.Length > 80)
            {
                if (start < 0)
                    start = 0;
                if (end > data.Length)
                    end = data.Length - 1;

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
        
        private static string UnzipToTempFolder(string archiveFilenameIn, string password)
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

        #region save signing
        // http://www.gothi.co.uk/2010/06/xbox-save-resigning-a-technical-overview/

        //"722E7565FB841B09E938DA756393FF80"
        private static byte[] mNFL2K5Key = new byte[] { 
            0x72, 0x2E, 0x75, 0x65, 0xFB, 0x84, 0x1B, 0x09, 
            0xE9, 0x38, 0xDA, 0x75, 0x63, 0x93, 0xFF, 0x80
        };

        /// <summary>
        /// Signs the NFL2K5 xbox file.
        /// The actual SAVEGAME.DAT file is not signed. The file "EXTRA" inside the dave is.
        /// The "EXTRA" file is signed hashed with the SAVEGAME.DAT data and the 2K5 key.
        /// </summary>
        /// <param name="sourceFile">The file to sign</param>
        public static void SignNfl2K5Save(string fileToSign, byte[] dataToHash)
        {
            SignFile(mNFL2K5Key, fileToSign, dataToHash);
        }
        
        private static void SignFile(byte[] key, string fileToSign, byte[] dataToHash)
        {
            try
            {
                using (HMACSHA1 hMACSHA = new HMACSHA1(key))
                {
                    byte[] array = hMACSHA.ComputeHash(dataToHash);
                    File.WriteAllBytes(fileToSign, array);
                }
            }
            catch (Exception )
            {
                Errors.Add("Error signing file! " + fileToSign);
            }
        }
        #endregion
    }
}
