using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace NFL2K5Tool
{
    public class DataMap
    {
        private static Dictionary<string, string> sPhotoMap;
        private static Dictionary<string, string> sPBPMap;

        private const string cPhotoMapPath = "PlayerData\\ENFPhotoIndex.txt";
        private const string cPBPMapPath = "PlayerData\\ENFNameIndex.txt";

        public static Dictionary<string, string> PhotoMap
        {
            get
            {
                if (sPhotoMap == null )
                    sPhotoMap = ReadIntoMap(cPhotoMapPath, false);
                return sPhotoMap;
            }
        }

        public static Dictionary<string, string> PBPMap
        {
            get
            {
                if (sPBPMap == null)
                    sPBPMap = ReadIntoMap(cPBPMapPath, false);
                return sPBPMap;
            }
        }

        /// <summary>
        /// returns a Dictionary of the file contants.
        /// </summary>
        /// <param name="fileName">the file to read</param>
        /// <param name="lookupByNumber"> true to lookup by number, false to lookup by name</param>
        private static Dictionary<string, string> ReadIntoMap(string fileName, bool lookupByNumber)
        {
            Dictionary<string, string> retVal = null;
            char[] sep = new char[] { '=' };
            int key = 0;
            int value = 1;
            if (lookupByNumber)
            {
                key = 1;
                value = 0;
            }
            if (File.Exists(cPhotoMapPath))
            {
                string[] contents = File.ReadAllLines(fileName);
                string line;
                //StringBuilder newFileContents = new StringBuilder();
                string[] parts;
                retVal = new Dictionary<string, string>(contents.Length);
                for (int i = 0; i < contents.Length; i++)
                {
                    line = contents[i];
                    if (line.Length > 0 && line[0] != ';')
                    {
                        parts = line.Split(sep);
                        if (!retVal.ContainsKey(parts[key]))
                        {
                            retVal.Add(parts[key], parts[value]);
                            //newFileContents.Append(line);
                            //newFileContents.Append("\r\n");
                        }
                        //else
                        //{
                        //    newFileContents.Append(";");
                        //    newFileContents.Append(line);
                        //    newFileContents.Append("  ");
                        //    newFileContents.Append(retVal[parts[1]]);
                        //    newFileContents.Append("\r\n");
                        //}
                    }
                }
                //if (newFileContents.Length > 0)
                //{
                //    ErrorForm form = new ErrorForm();
                //    form.Text = fileName;
                //    form.ErrorText = newFileContents.ToString();
                //    form.Show();
                //}
            }
            else
            {
                retVal = new Dictionary<string, string>(); // create an empty one so we don't crash
            }
            return retVal;
        }

        private static Dictionary<string, string> sReversePhotoMap;
        /// <summary>
        /// Returns the player's name the number maps to.
        /// </summary>
        /// <param name="number">the Photo number</param>
        public static string GetPlayerNameForPhoto(string number)
        {
            switch (number.Length)
            {
                case 1: number = "000" + number; break;
                case 2: number = "00" + number; break;
                case 3: number = "0" + number; break;
            }
            if (sReversePhotoMap == null)
                sReversePhotoMap = ReadIntoMap(cPhotoMapPath, true);
            if (sReversePhotoMap.ContainsKey(number))
                return sReversePhotoMap[number];
            return "UNKNOWN!";
        }

        private static Dictionary<string, string> sReversePBPMap;
        /// <summary>
        /// Returns the player's PBP the number maps to.
        /// </summary>
        /// <param name="number">the PBP number</param>
        public static string GetPlayerNameForPBP(string number)
        {
            switch (number.Length)
            {
                case 1: number = "000" + number; break;
                case 2: number = "00" + number; break;
                case 3: number = "0" + number; break;
            }
            if (sReversePBPMap == null)
                sReversePBPMap = ReadIntoMap(cPBPMapPath, true);
            if (sReversePBPMap.ContainsKey(number))
                return sReversePBPMap[number];
            return "UNKNOWN!";
        }
    }
}
