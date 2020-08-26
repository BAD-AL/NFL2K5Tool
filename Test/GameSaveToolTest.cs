using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFL2K5Tool;
using System.IO;
using System.Reflection;

namespace Test
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class GameSaveToolTest
    {
        static object AssemblyObject = new GameSaveToolTest();

        /// <summary>
        /// Get a file that has been embedded as a resource in this test dll.
        /// You need to make sure the file you are trying to get is an embedded resource in the dll.
        ///    [Build Action -> Embedded Recource]
        /// </summary>
        /// <param name="file">The file name to get.</param>
        /// <returns></returns>
        public static String GetFile(string file)
        {
            string ret = null;
            System.IO.Stream s =
                AssemblyObject.GetType().Assembly.GetManifestResourceStream(file);
            using( StreamReader reader = new StreamReader(s, Encoding.UTF8))
            {
                ret = reader.ReadToEnd();
            }
            return ret;
        }

        public static string GetTextFileContents(string fileName)
        {
            string retVal = "";
            string path = GetFilePath(fileName);
            retVal = File.ReadAllText(path);
            return retVal;
        }

        public static string GetFilePath(string fileName)
        {
            fileName = Path.DirectorySeparatorChar + fileName;
            string path = Path.GetFullPath(".");
            string filePath = path + fileName;
            string dotDot = "";
            do
            {
                if (File.Exists(filePath))
                {
                    return filePath;
                }
                dotDot += (Path.DirectorySeparatorChar + "..");
                filePath = path + dotDot + fileName;
            } while (filePath.Length < 255);

            return null;
        }

        /// <summary>
        /// </summary>
        /// <param name="a1"></param>
        /// <param name="a2"></param>
        /// <returns>Returns -1 if they are equal, the index of inequality otherwise</returns>
        public static int CompareArrays(byte[] a1, byte[] a2)
        {
            if (a1.Length == a2.Length)
            {
                for (int i = 0; i < a1.Length; i++)
                {
                    if (a1[i] != a2[i])
                        return i;
                }
                return -1;
            }
            return a2.Length;
        }

        public GameSaveToolTest()
        {
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        /// <summary>
        /// Make sure 'GetLeaguePlayers()' function is returning correct results.
        /// </summary>
        [TestMethod]
        public void GetLeaguePlayersTest()
        {
            string compareAgainst = GameSaveToolTest.GetTextFileContents("AllBasePlayers.txt").Replace("\r\n","\n");
            GamesaveTool tool = new GamesaveTool();
            tool.LoadSaveFile(GetFilePath("Base_NFL2K5_SAVEGAME.DAT"));
            string leaguePlayers = tool.GetLeaguePlayers(true, true).Replace("\r\n", "\n");
            int index = compareAgainst.IndexOf(leaguePlayers);
            Assert.IsTrue(index > -1, "Error! there is a difference form the base to what we got.");
        }


        /// <summary>
        /// Make sure get Free agents functionality is returning correct results.
        /// </summary>
        [TestMethod]
        public void GetFreeAgentsTest()
        {
            string compareAgainst = GameSaveToolTest.GetTextFileContents("AllBasePlayers.txt").Replace("\r\n", "\n");
            GamesaveTool tool = new GamesaveTool();
            tool.LoadSaveFile(GetFilePath("Base_NFL2K5_SAVEGAME.DAT"));
            string leaguePlayers = tool.GetTeamPlayers("FreeAgents", true, true).Replace("\r\n", "\n");
            int index = compareAgainst.IndexOf(leaguePlayers);
            Assert.IsTrue(index > -1, "Error! there is a difference form the base to what we got.");
        }

        /// <summary>
        /// Make sure Get Draft class functionality is returning correct results.
        /// </summary>
        [TestMethod]
        public void GetDraftClassTest()
        {
            string compareAgainst = GameSaveToolTest.GetTextFileContents("AllBasePlayers.txt").Replace("\r\n", "\n");
            GamesaveTool tool = new GamesaveTool();
            tool.LoadSaveFile(GetFilePath("Base_NFL2K5_SAVEGAME.DAT"));
            string leaguePlayers = tool.GetTeamPlayers("DraftClass", true, true).Replace("\r\n", "\n");
            int index = compareAgainst.IndexOf(leaguePlayers);
            Assert.IsTrue(index > -1, "Error! there is a difference form the base to what we got.");
        }

        /// <summary>
        /// Make sure Get schedule functionality is returning correct results.
        /// </summary>
        [TestMethod]
        public void GetScheduleTest()
        {
            string compareAgainst = GameSaveToolTest.GetTextFileContents("BaseSchedule.txt").Replace("\r\n", "\n");
            GamesaveTool tool = new GamesaveTool();
            tool.LoadSaveFile(GetFilePath("Base_NFL2K5_SAVEGAME.DAT"));
            string schedule = tool.GetSchedule();
            int index = compareAgainst.IndexOf(schedule);
            Assert.IsTrue(index > -1, "Error! there is a difference form the base to what we got.");
        }

        /// <summary>
        /// Make sure setting default data results in no changes in the binary data.
        /// </summary>
        [TestMethod]
        public void SavePlayerDataTest()
        {
            GamesaveTool tool = new GamesaveTool();
            tool.LoadSaveFile(GetFilePath("Base_NFL2K5_SAVEGAME.DAT"));
            Byte[] baseData = new byte[tool.GameSaveData.Length];
            Array.Copy(tool.GameSaveData, baseData, baseData.Length);

            InputParser parser = new InputParser(tool);
            parser.ProcessText( GameSaveToolTest.GetTextFileContents("AllBasePlayers.txt"));

            Byte[] modifiedData = new byte[tool.GameSaveData.Length];
            Array.Copy(tool.GameSaveData, modifiedData, baseData.Length);

            int loc = CompareArrays(baseData, modifiedData);
            Assert.AreEqual(-1, loc, "Error! data differs at location "+ loc.ToString("X"));
        }


        /// <summary>
        /// Make sure setting default data results in no changes in the binary data.
        /// </summary>
        [TestMethod]
        public void SaveScheduleDataTest()
        {
            GamesaveTool tool = new GamesaveTool();
            tool.LoadSaveFile(GetFilePath("Base_NFL2K5_SAVEGAME.DAT"));
            Byte[] baseData = new byte[tool.GameSaveData.Length];
            Array.Copy(tool.GameSaveData, baseData, baseData.Length);

            InputParser parser = new InputParser(tool);
            parser.ProcessText(GameSaveToolTest.GetTextFileContents("BaseSchedule.txt"));

            Byte[] modifiedData = new byte[tool.GameSaveData.Length];
            Array.Copy(tool.GameSaveData, modifiedData, baseData.Length);

            int loc = CompareArrays(baseData, modifiedData);
            Assert.AreEqual(-1, loc, "Error! data differs at location " + loc.ToString("X"));
        }

        [TestMethod]
        public void CallMethods()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(GameSaveToolTest));
            var testMethods = //from assembly in Assembly.GetAssembly(typeof(GameSaveToolTest))
                              from type in assembly.GetTypes()
                              from method in type.GetMethods()
                              where method.IsDefined(typeof(TestMethodAttribute), false)
                              select method;

            string str = "";
            foreach (MethodInfo method in testMethods)
            {
                if (method.Name == "CallMethods")
                    continue;
                Object dude = Activator.CreateInstance(method.DeclaringType);
                method.Invoke(dude, null);
            }
        }
    }
}
