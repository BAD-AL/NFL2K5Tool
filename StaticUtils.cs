using System;
using System.Collections.Generic;
using System.Text;

namespace NFL2K5Tool
{
    /// <summary>
    /// Static utility functions that I don't want to clutter up other files with.
    /// </summary>
    public static class StaticUtils
    {
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
    }
}
