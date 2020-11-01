using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

// NOTE: Using ARMaxDLL.dll will require you to target x86, not 'AnyCPU'
// Right click on Project -> Properties -> Build -> Platform target -> x86
namespace ARMaxWrapper
{
    /// <summary>
    /// Documentation at: 
    /// https://github.com/PMStanley/ARMax/wiki/Public-Functions 
    /// 
    /// Notes: 
    ///    Internal file '0' does not exist, when using functions that take file index, valid values are 1 +.
    ///    Not all functions exposed by ARMaxDLL.dll are wrapped by this class.
    /// </summary>
    public class ARMaxNativeMethods
    {
        // Return values for these functions; 0 ==> good; other == error

        [DllImport("ARMaxDLL.dll", CharSet = CharSet.Ansi)]
        public static extern int DLLVersion();

        /// <summary>
        /// Initialises the internal data structures used when accessing .max saves. Required before using 
        /// any other functions
        /// </summary>
        /// <returns>See standard return codes for ARMax at top of file</returns>
        [DllImport("ARMaxDLL.dll", CharSet = CharSet.Ansi)]
        public static extern int InitMaxSave();

        /// <summary>
        /// Loads an existing .max save into memory and allows direct access to the files contained.
        /// </summary>
        /// <example> theResult = LoadSave(pathToFile);</example>
        /// <param name="filename"></param>
        /// <returns>See standard return codes for ARMax at top of file</returns>
        [DllImport("ARMaxDLL.dll", CharSet = CharSet.Ansi)]
        public static extern int LoadSave(string filename);

        /// <summary>
        ///  Releases the internal data structures used by the DLL. Must be the last function called when no 
        ///  longer working with the DLL.
        /// </summary>
        [DllImport("ARMaxDLL.dll", CharSet = CharSet.Ansi)]
        public static extern int FreeMaxSave();

        /// <summary>
        /// Returns the amount of files within the .max save in memory.
        /// </summary>
        /// <returns>A successful result is a number greater than 0 but below 249. If the DLL is uninitialized 
        /// the result will be $F9 (constant NOT_INITIALISED)</returns>
        [DllImport("ARMaxDLL.dll", CharSet = CharSet.Ansi)]
        public static extern int NumberOfFiles();

        /// <summary>
        /// Adds a file to the .max save in memory
        /// </summary>
        /// <param name="pathToFile"></param>
        /// <example>theResult = AddFileToSave(pathtoFile);</example>
        /// <returns></returns>
        [DllImport("ARMaxDLL.dll", CharSet = CharSet.Ansi)]
        public static extern int AddFileToSave(string pathToFile);

        /// <summary>
        /// Deletes a file from the .max save in memory.
        /// file index starts at 1; file 0 does not exist.
        /// </summary>
        /// <param name="filename"></param>
        /// <example>theResult = DeleteFileInSave(1);</example>
        [DllImport("ARMaxDLL.dll", CharSet = CharSet.Ansi)]
        public static extern int DeleteFileInSave(int itemNum);

        /// <summary>
        /// Creates a .max file with containing all the files in memory with the name and location specified.
        /// </summary>
        ///<example>???</example>
        /// <param name="filename">the file to create/save</param>
        [DllImport("ARMaxDLL.dll", CharSet = CharSet.Ansi)]
        public static extern int SaveMaxFile(string filename);

        /// <summary>
        ///  Sets the Root Directory of the .max save. This is the directory the files will be extracted to on the 
        ///  PS2 Memory card.
        /// </summary>
        /// <example>theResult = SetRootDir(nameOfDirectory);</example>
        /// <param name="filename"></param>
        [DllImport("ARMaxDLL.dll", CharSet = CharSet.Ansi)]
        public static extern int SetRootDir(string filename);

        /// <summary>
        /// Gets the Root Directory of the .max save. This is the directory the files will be extracted to on the 
        /// PS2 Memory card.
        /// </summary>
        /// <example>
        /// StringBuilder builder = new StringBuilder(256);
        /// theResult = GetRootDir(builder, 256);
        /// string theRootDir = builder.Tostring();
        ///</example>
        [DllImport("ARMaxDLL.dll", CharSet = CharSet.Ansi)]
        public static extern int GetRootDir(StringBuilder buff, int bufsize);

        /// <summary>
        ///  Does a case sensitive search for a filename in the files in memory and returns it's position in the list.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns>Successful result is a value greater than 0, an unsuccessful result (filename not found) is -1. </returns>
        [DllImport("ARMaxDLL.dll", CharSet = CharSet.Ansi)]
        public static extern int FileExistsInSavePos(string filename);

        /// <summary>
        /// Replaces an existing file with the data of another file. The existing filename passed is case sensitive. 
        /// The filename of the data to replace with does not need to match the existing filename as this name will 
        /// not be used in the .max save.
        /// </summary>
        /// <example>theResult = ReplaceFileInSave(nameOfFile, locationAndNameOfAnotherFile);</example>
        /// <param name="existingFileName"></param>
        /// <param name="newFile"></param>
        /// <returns></returns>
        [DllImport("ARMaxDLL.dll", CharSet = CharSet.Ansi)]
        public static extern int ReplaceFileInSave(string existingFileName, string newFile);

        /// <summary>
        /// Gets the file name and size of a file stored in memory.
        /// The DLL must be initiated before use.
        /// </summary>
        /// <example>
        /// int fileSize = -1;
        /// StringBuilder buff = new StringBuilder(256);
        /// int result = ARMaxNativeMethods.FileDetails(i, buff, 256, ref fileSize);
        /// Console.WriteLine("Filename: "+buff.ToString());
        /// Console.WriteLine("size: " + fileSize);
        /// </example>
        /// <param name="itemNum"></param>
        /// <param name="name">buffer to store the data</param>
        /// <param name="nameLength">length of the name buffer</param>
        /// <param name="fileSize"></param>
        [DllImport("ARMaxDLL.dll", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern int FileDetails(int itemNum, StringBuilder name, int nameLength, ref int fileSize);

        /// <summary>
        /// <example>
        /// theResult = ExtractAFile(1, locationToSaveTo);
        /// </example>
        /// </summary>
        /// <param name="itemNum"></param>
        /// <param name="pathToSaveTo"></param>
        /// <returns>See standard return codes for ARMax at top of file</returns>
        [DllImport("ARMaxDLL.dll", CharSet = CharSet.Ansi)]
        public static extern int ExtractAFile(int itemNum, string pathToSaveTo);

    }
}
