using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFL2K5Tool
{
    public class DepthChart
    {
        public DepthChart() { }
        List < PlayerDepthData> mPlayers = new List<PlayerDepthData>();

        public void AddPlayer(string fname, string lname, string position, int depth)
        {
            mPlayers.Add(new PlayerDepthData()
            {
                fname = fname,
                lname = lname,
                position = position,
                depth = depth
            });
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            mPlayers.Sort();
            string currentPos = null;// mPlayers[0].position;
            for (int i = 0; i < mPlayers.Count; i++) 
            {
                if (currentPos != mPlayers[i].position)
                {
                    currentPos = mPlayers[i].position;
                    sb.Append("\n");
                    sb.Append(mPlayers[i].position);
                }
                sb.Append(",");
                sb.Append(mPlayers[i].fname);
                sb.Append(" ");
                sb.Append(mPlayers[i].lname);
            }
            return sb.ToString().TrimStart();
        }
    }

    internal class PlayerDepthData : IComparable
    {
        internal string fname; internal string lname; internal string position; internal int depth;

        static List<string> sPositionOrder = new List<string>( new string[] { 
                "QB", "RB", "FB",  "WR",  "TE", "C",  "G",  "T",
                "DE", "DT", "OLB", "ILB", "CB", "FS", "SS",
                "K",  "P"
            });
        public PlayerDepthData(){} //(string fname, string lname, string position, int depth) 
        
        #region IComparable Members

        public int CompareTo(object obj)
        {
            PlayerDepthData data = obj as PlayerDepthData;
            int myPositionIndex = sPositionOrder.IndexOf(this.position);
            int yourPositionIndex = sPositionOrder.IndexOf(data.position);
            if (myPositionIndex == yourPositionIndex)
                return this.depth.CompareTo(data.depth);
            else
                return myPositionIndex.CompareTo(yourPositionIndex);
        }

        #endregion
    }
}
