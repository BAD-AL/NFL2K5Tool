using System;
using System.Collections.Generic;

using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace NFL2K5Tool
{
    public class GamesaveTool
    {
        private const int cPlayerStart = 0xB288;
        private const int cPlayerDataLength = 0x54;

        // Duane starks is the first player in the original roster 
        const int cDuaneStarksFnamePointerLoc = 0xB298;

        public byte[] GameSaveData = null;

        public GamesaveTool()
        {
        }

        public void LoadSaveFile(string fileName)
        {
            GameSaveData = File.ReadAllBytes(fileName);
        }

        /// <summary>
        /// Can be useful for debugging when you want to check why you're writing at a specific point in the file.
        /// </summary>
        /// <param name="location"></param>
        /// <param name="b"></param>
        protected void SetByte(int location, byte b)
        {
            GameSaveData[location] = b;
        }

        /// <summary>
        /// The attributes key
        /// </summary>
        public string Key
        {
            get
            {
                StringBuilder builder = new StringBuilder(350);
                StringBuilder dummy = new StringBuilder(200);
                StringBuilder stillNeedToDo = new StringBuilder(50);
                int prevLength = 0;
                builder.Append("#fname,lname,Number,Pos,");
                foreach (PlayerOffsets attr in mAttributeOrder)
                {
                    builder.Append(attr.ToString());
                    builder.Append(",");
                }
                foreach (AppearanceAttributes app in mApperanceOrder)
                {
                    prevLength = dummy.Length;
                    GetPlayerApperanceAttribute(0, app, dummy);
                    if (dummy.Length > prevLength)
                    {
                        builder.Append(app.ToString());
                        builder.Append(",");
                    }
                    else
                    {
                        stillNeedToDo.Append(app.ToString());
                        stillNeedToDo.Append(",");
                    }
                }
                if (stillNeedToDo.Length > 0)
                {
                    builder.Append("\n#Still need to do:");
                    builder.Append(stillNeedToDo.ToString());
                }
                return builder.ToString();
            }
        }

        /// <summary>
        /// Attribute order: 
        /// #fname,lname,position,number,Speed,Agility,Strength,Jumping,Coverage,PassRush,RunCoverage,PassBlocking,RunBlocking,Catch,RunRoute,
        /// BreakTackle,HoldOnToBall,PowerRunStyle,PassAccuracy,PassArmStrength,PassReadCoverage,Tackle,KickPower,KickAccuracy,Stamana,Durability,
        /// Leadership,Scramble,Composure,Consistency,Aggressiveness,
        /// 
        /// Apperance order:
        /// College,DOB,Hand,Weight,Height,BodyType,Skin,Face,Dreads,Helmet,FaceMask,FaceShield,EyeBlack,MouthPiece,LeftGlove,RightGlove,
        /// LeftWrist,RightWrist,LeftElbow,RightElbow,Sleeves,LeftShoe,RightShoe,NeckRoll,Turtleneck
        /// </summary>
        /// <param name="player"></param>
        /// <param name="attributes"></param>
        /// <param name="apperance"></param>
        /// <returns></returns>
        public string GetPlayerData(int player, bool attributes, bool apperance)
        {
            StringBuilder builder = new StringBuilder(300);
            builder.Append(GetPlayerName(player, ','));
            builder.Append(',');
            builder.Append(GetAttribute(player, PlayerOffsets.JerseyNumber));
            builder.Append(',');
            builder.Append(GetPlayerPosition(player));
            builder.Append(',');
            if (attributes)
                GetPlayerAttributes(player,builder);
            if (apperance)
                GetPlayerApperance(player, builder);

            return builder.ToString();
        }

        private int GetPlayerDataStart(int player)
        {
            int ret = cPlayerStart + player * cPlayerDataLength;
            return ret;
        }

        private void GetPlayerApperance(int player, StringBuilder builder)
        {
            foreach (AppearanceAttributes attr in this.mApperanceOrder)
            {
                GetPlayerApperanceAttribute(player, attr, builder);
            }
        }

        private void GetPlayerApperanceAttribute(int player, AppearanceAttributes attr, StringBuilder builder)
        {
            switch (attr)
            {
                case AppearanceAttributes.BodyType:
                    GetBody(player, builder);break;
                case AppearanceAttributes.Dreads:
                    GetDreads(player, builder);break;
                case AppearanceAttributes.EyeBlack:
                    GetEyeblack(player, builder);break;
                case AppearanceAttributes.Hand:
                    GetHand(player, builder);break;
                case AppearanceAttributes.Turtleneck:
                    GetTurtleneck(player, builder); break;
                case AppearanceAttributes.Face:
                    GetFace(player, builder); break;
                case AppearanceAttributes.FaceMask:
                    GetFaceMask(player, builder); break;
                case AppearanceAttributes.Visor:
                    GetVisor(player, builder); break;
                case AppearanceAttributes.Skin:
                    GetSkin(player, builder); break;
                case AppearanceAttributes.DOB:
                    builder.Append(GetAttribute(player, PlayerOffsets.DOB)); 
                    builder.Append(","); 
                    break;
                case AppearanceAttributes.Helmet:
                    GetHelmet(player, builder); break;
                case AppearanceAttributes.RightShoe:
                    GetRightShoe(player, builder); break;
                case AppearanceAttributes.LeftShoe:
                    GetLeftShoe(player, builder); break;
                case AppearanceAttributes.LeftGlove:
                    GetLeftGlove(player, builder); break;
                case AppearanceAttributes.MouthPiece:
                    GetMouthPiece(player, builder); break;
                case AppearanceAttributes.Sleeves:
                    GetSleeves(player, builder); break;
                case AppearanceAttributes.NeckRoll:
                    GetNeckRoll(player, builder); break;
                case AppearanceAttributes.RightGlove:
                    GetRightGlove(player, builder); break;
                case AppearanceAttributes.LeftWrist:
                    GetLeftWrist(player, builder); break;
                case AppearanceAttributes.RightWrist:
                    GetRightWrist(player, builder); break;
                case AppearanceAttributes.LeftElbow:
                    GetLeftElbow(player, builder); break;
                case AppearanceAttributes.Weight:
                    builder.Append(GetAttribute(player, PlayerOffsets.Weight));
                    builder.Append(","); 
                    break;
                case AppearanceAttributes.Height:
                    builder.Append(GetAttribute(player, PlayerOffsets.Height));
                    builder.Append(",");
                    break;
                case AppearanceAttributes.RightElbow:
                    GetRightElbow(player, builder); break;
                case AppearanceAttributes.College:
                    builder.Append(GetAttribute(player, PlayerOffsets.College));
                    builder.Append(",");
                    break;
            }
        }

        private AppearanceAttributes[] mApperanceOrder = new AppearanceAttributes[]{
             AppearanceAttributes.College, AppearanceAttributes.DOB, AppearanceAttributes.Hand, 
             AppearanceAttributes.Weight, AppearanceAttributes.Height, AppearanceAttributes.BodyType, 
             AppearanceAttributes.Skin, AppearanceAttributes.Face, AppearanceAttributes.Dreads, 
             AppearanceAttributes.Helmet, AppearanceAttributes.FaceMask, AppearanceAttributes.Visor, 
             AppearanceAttributes.EyeBlack, AppearanceAttributes.MouthPiece, AppearanceAttributes.LeftGlove, 
             AppearanceAttributes.RightGlove, AppearanceAttributes.LeftWrist, AppearanceAttributes.RightWrist,
             AppearanceAttributes.LeftElbow, AppearanceAttributes.RightElbow, AppearanceAttributes.Sleeves,
             AppearanceAttributes.LeftShoe, AppearanceAttributes.RightShoe, AppearanceAttributes.NeckRoll, 
             AppearanceAttributes.Turtleneck
        };

        private PlayerOffsets[] mAttributeOrder = new PlayerOffsets[] { 
                        PlayerOffsets.Speed,           PlayerOffsets.Agility,          PlayerOffsets.Strength,     PlayerOffsets.Jumping,       PlayerOffsets.Coverage,
                        PlayerOffsets.PassRush,        PlayerOffsets.RunCoverage,      PlayerOffsets.PassBlocking, PlayerOffsets.RunBlocking,   PlayerOffsets.Catch,
                        PlayerOffsets.RunRoute,        PlayerOffsets.BreakTackle,      PlayerOffsets.HoldOntoBall, PlayerOffsets.PowerRunStyle, PlayerOffsets.PassAccuracy,
                        PlayerOffsets.PassArmStrength, PlayerOffsets.PassReadCoverage, PlayerOffsets.Tackle,       PlayerOffsets.KickPower,     PlayerOffsets.KickAccuracy,
                        PlayerOffsets.Stamina,         PlayerOffsets.Durability,       PlayerOffsets.Leadership,   PlayerOffsets.Scramble,      PlayerOffsets.Composure,
                        PlayerOffsets.Consistency,     PlayerOffsets.Aggressiveness};

        private void GetPlayerAttributes(int player, StringBuilder builder)
        {
            int loc = GetPlayerDataStart(player);
            
            for (int i = 0; i < mAttributeOrder.Length; i++)
            {
                builder.Append(GetAttribute(player, mAttributeOrder[i]));
                builder.Append(',');
            }
        }

        private string GetAttribute(int player, PlayerOffsets attr)
        {
            string retVal = "";
            int loc = GetPlayerDataStart(player) + (int)attr;
            int val = GameSaveData[loc];
            switch (attr)
            {
                case PlayerOffsets.PowerRunStyle:
                    PowerRunStyle style = (PowerRunStyle)val;
                    retVal = style.ToString();
                    break;
                case PlayerOffsets.Face:
                    Face f = (Face)val;
                    retVal = f.ToString();
                    break;
                case PlayerOffsets.JerseyNumber:
                    val = GameSaveData[loc+1] << 5 & 0x60;
                    val += GameSaveData[loc] >> 3 & 0x1f;
                    retVal = val.ToString();
                    break;
                case PlayerOffsets.DOB:
                    // The year still isn't correct :( [month and Day are good though]; moving on...
                    int lsd_year = ((GameSaveData[loc + 2] & 0x0001) << 3) + (GameSaveData[loc + 1] >> 5);
                    int msd_year = (GameSaveData[loc + 2] & 0x0e)  ;
                    int day = GameSaveData[loc+1] & 0x1f;
                    int month = (int)(GameSaveData[loc]  >> 4);
                    //if (month != 0 && day != 0 && year != 0)
                        retVal = string.Concat(new object[] { month, "/", day, "/", msd_year, lsd_year });
                    //else
                    //    retVal = "1/1/1954";
                    break;
                case PlayerOffsets.Weight:
                    val += 150;
                    retVal = val.ToString();
                    break;
                case PlayerOffsets.Height:
                    int feet = val / 12;
                    int inches = val % 12;
                    retVal = string.Concat(feet, "\'", inches, "\"");
                    break;
                case PlayerOffsets.College:
                    val = val << 8;
                    val += GameSaveData[loc + 1];
                    retVal = "COLEGE:" + val.ToString("X4");
                    break;
                default:
                    retVal += val;
                    break;
            }
            return retVal;
        }

        private char[] slash = { '/' };

        private void SetAttribute(int player, PlayerOffsets attr, string stringVal)
        {
            int loc = GetPlayerDataStart(player) + (int)attr;
            int val = 0;
            int v1, v2;
            switch (attr)
            {
                case PlayerOffsets.PowerRunStyle:
                    PowerRunStyle style = (PowerRunStyle)Enum.Parse(typeof(PowerRunStyle), stringVal);
                    SetByte(loc, (byte)style);
                    break;
                case PlayerOffsets.Face:
                    Face f = (Face)Enum.Parse(typeof(Face), stringVal);
                    SetByte(loc, (byte)f);
                    break;
                case PlayerOffsets.JerseyNumber:
                    val = Int32.Parse(stringVal);
                    v1 = (val >> 5 & 3);
				    v2 = (val  << 3 & 0xf8);
                    SetByte(loc + 1, (byte)v1);
                    SetByte(loc, (byte)v2);
                    break;
                case PlayerOffsets.DOB:

                    break;
                case PlayerOffsets.Weight:
                    val = Int32.Parse(stringVal);
                    val -= 150;
                    SetByte(loc, (byte)val);
                    break;
                case PlayerOffsets.Height:
                    val = GetInches(stringVal);
                    SetByte(loc, (byte)val);
                    break;
                case PlayerOffsets.College:
                    break;
                default:
                    val = Int32.Parse(stringVal);
                    SetByte(loc, (byte)val);
                    break;
            }
        }

        // input like 6'3"
        private int GetInches(string stringVal)
        {
            int feet = stringVal[0] - 30;
            stringVal = stringVal.Replace("\"", "");
            int inches = Int32.Parse(stringVal.Substring(2));
            inches += feet * 12;
            return inches;
        }

        
        private string GetPlayerPosition(int player)
        {
            int loc = GetPlayerDataStart(player);
            loc += (int)PlayerOffsets.Position;
            Positions p = (Positions)GameSaveData[loc];
            return p.ToString();
        }

        /// <summary>
        /// Get's the player's name 
        /// </summary>
        /// <param name="player">an int from 0 to 2317</param>
        /// <returns></returns>
        public string GetPlayerName(int player, char sepChar)
        {
            string retVal = "!!!!!!!!INVALID!!!!!!!!!!!!";
            if (player > -1 && player < 2318)
            {
                int ptrLoc = player * cPlayerDataLength + cDuaneStarksFnamePointerLoc;
                retVal = GetName(ptrLoc) + sepChar + GetName(ptrLoc + 4);
            }
            return retVal;
        }

        /// <summary>
        /// a pointer is 4 bytes:
        /// goes from left to right (least to most significant)
        /// </summary>
        /// <param name="namePointerLoc">The location of the pointer</param>
        /// <returns>the string the pointer points to.</returns>
        public string GetName(int namePointerLoc)
        {
            string retVal = "!!!!!INVALID!!!!!!!!";
            int pointer = 0;
            pointer = GameSaveData[namePointerLoc+2] << 16;
            pointer += GameSaveData[namePointerLoc+1] << 8;
            pointer += GameSaveData[namePointerLoc ];
            int dataLocation = namePointerLoc + pointer - 1;
            StringBuilder builder = new StringBuilder();
            for (int i = dataLocation; i < dataLocation + 99; i += 2)
            {
                if( GameSaveData[i] == 0)
                    break;
                builder.Append((char)GameSaveData[i]);
            }

            if (builder.Length > 0)
                retVal = builder.ToString();
            return retVal;
        }


        #region Code that I would like to re-factor into something more common, but can't think of a good way to
        //Face is stored in all but last bit 
        private void GetFaceMask(int player, StringBuilder builder)
        {
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.FaceMask;
            int b = GameSaveData[loc];
            b = (b & 0x7f) >> 2;
            FaceMask ret = (FaceMask)b;
            builder.Append(ret.ToString());
            builder.Append(",");
        }

        /// <summary>
        /// set face for a dude
        /// </summary>
        /// <param name="player">the player (0-2100+)</param>
        /// <param name="val">string representation of the face enum</param>
        private void SetFaceMask(int player, String val)
        {
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.FaceMask;
            FaceMask ret = (FaceMask)Enum.Parse(typeof(FaceMask), val);
            int dude = (int)ret;
            dude = dude << 2;
            int b = GameSaveData[loc];
            b &= 0x80;// clear all but first bit
            b += dude;
            SetByte(loc, (byte)b);
        }

        //Face is stored in all but last bit 
        private void GetFace(int player, StringBuilder builder)
        {
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.Face;
            int b = GameSaveData[loc];
            b = b >> 1;
            Face ret = (Face)b;
            builder.Append(ret.ToString());
            builder.Append(",");
        }

        /// <summary>
        /// set face for a dude
        /// </summary>
        /// <param name="player">the player (0-2100+)</param>
        /// <param name="val">string representation of the face enum</param>
        private void SetFace(int player, String val)
        {
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.Face;
            Face ret = (Face)Enum.Parse(typeof(Face), val);
            int dude = (int)ret;
            int b = GameSaveData[loc];
            b &= 0x01; // clear all but last bit
            b += dude;
            SetByte(loc, (byte)b);
        }

        // Turtleneck is stored in bits 6&7 (0-3)
        private void GetTurtleneck(int player, StringBuilder builder)
        {
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.Turtleneck_Body_EyeBlack_Hand_Dreads;
            int b = GameSaveData[loc];
            b &= 0x60;
            b = b >> 5;
            Turtleneck ret = (Turtleneck)b;
            builder.Append(ret.ToString());
            builder.Append(",");
        }

        /// <summary>
        /// set turtleneck for a dude
        /// </summary>
        /// <param name="player">the player (0-2100+)</param>
        /// <param name="val">string repereentation of the Turtleneck enum</param>
        private void SetTurtleneck(int player, String val)
        {
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.Turtleneck_Body_EyeBlack_Hand_Dreads;
            Turtleneck ret = (Turtleneck)Enum.Parse(typeof(Turtleneck), val);
            int dude = (int)ret;
            dude = dude << 5;
            int b = GameSaveData[loc];
            b &= 0x1F; // clear bits 6&7
            b += dude;
            SetByte(loc, (byte)b);
        }

        // Body is stored in bits 4&5 (0-3)
        private void GetBody(int player, StringBuilder builder)
        {
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.Turtleneck_Body_EyeBlack_Hand_Dreads;
            int b = GameSaveData[loc];
            b &= 0x18;
            b = b >> 3;
            Body ret = (Body)b;
            builder.Append(ret.ToString());
            builder.Append(",");
        }

        /// <summary>
        /// set body type for a dude
        /// </summary>
        /// <param name="player">the player (0-2100+)</param>
        /// <param name="val">string repersentation of the body enum</param>
        private void SetBody(int player, String val)
        {
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.Turtleneck_Body_EyeBlack_Hand_Dreads;
            Body ret = (Body)Enum.Parse(typeof(Body), val);
            int dude = (int)ret;
            dude = dude << 3;
            int b = GameSaveData[loc];
            b &= 0x67; // clear bits 4&5
            b += dude;
            SetByte(loc, (byte)b);
        }

        // eyeblack is stored in bit 3
        private void GetEyeblack(int player, StringBuilder builder)
        {
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.Turtleneck_Body_EyeBlack_Hand_Dreads;
            int b = GameSaveData[loc];
            b &= 0x4;
            b = b >> 2;
            YesNo ret = (YesNo)b;
            builder.Append(ret.ToString());
            builder.Append(",");
        }

        /// <summary>
        /// set eyeblack for a dude
        /// </summary>
        /// <param name="player">the player (0-2100+)</param>
        /// <param name="val">string repersentation of the YesNo enum</param>
        private void SetEyeblack(int player, String val)
        {
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.Turtleneck_Body_EyeBlack_Hand_Dreads;
            YesNo ret = (YesNo)Enum.Parse(typeof(YesNo), val);
            int dude = (int)ret;
            dude = dude << 2;
            int b = GameSaveData[loc];
            b &= 0x7b; // clear bit 3
            b += dude;
            SetByte(loc, (byte)b);
        }

        // Hand is stored in bit 2
        private void GetHand(int player, StringBuilder builder)
        {
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.Turtleneck_Body_EyeBlack_Hand_Dreads;
            int b = GameSaveData[loc];
            b &= 0x2;
            b = b >> 1;
            Hand ret = (Hand)b;
            builder.Append(ret.ToString());
            builder.Append(",");
        }

        /// <summary>
        /// set Hand for a dude
        /// </summary>
        /// <param name="player">the player (0-2100+)</param>
        /// <param name="val">string repersentation of the hand enum</param>
        private void SetHand(int player, String val)
        {
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.Turtleneck_Body_EyeBlack_Hand_Dreads;
            YesNo ret = (YesNo)Enum.Parse(typeof(YesNo), val);
            int dude = (int)ret;
            dude = dude << 1;
            int b = GameSaveData[loc];
            b &= 0x7d; // clear bit 2
            b += dude;
            SetByte(loc, (byte)b);
        }

        // Dreads is stored in bit 1
        private void GetDreads(int player, StringBuilder builder)
        {
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.Turtleneck_Body_EyeBlack_Hand_Dreads;
            int b = GameSaveData[loc];
            b &= 0x1;
            YesNo ret = (YesNo)b;
            builder.Append(ret.ToString());
            builder.Append(",");
        }

        /// <summary>
        /// set Dreads for a dude
        /// </summary>
        /// <param name="player">the player (0-2100+)</param>
        /// <param name="val">string repersentation of the YesNo enum</param>
        private void SetDreads(int player, String val)
        {
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.Turtleneck_Body_EyeBlack_Hand_Dreads;
            YesNo ret = (YesNo)Enum.Parse(typeof(YesNo), val);
            int dude = (int)ret;
            int b = GameSaveData[loc];
            b &= 0x7e; // clear bit 1
            b += dude;
            SetByte(loc, (byte)b);
        }
        #endregion

        /// <summary>
        /// Visor is really weird, it has 3 states (Clear, Dark, None) but is stored across 2 adjacent bytes
        /// </summary>
        /// <param name="player">the player (0-2100+)</param>
        /// <param name="builder"></param>
        private void GetVisor(int player, StringBuilder builder)
        {
            int loc1 = GetPlayerDataStart(player) + (int)PlayerOffsets.FaceMask;
            int loc2 = GetPlayerDataStart(player) + (int)PlayerOffsets.Face;
            int fm = GameSaveData[loc1] & 0x80; // need 1st bit
            int f = GameSaveData[loc2] & 0x01; //need last bit
            Visor ret = Visor.None;
            if (fm > 0)
                ret = Visor.Clear;
            else if (f > 0)
                ret = Visor.Dark;
            builder.Append(ret.ToString());
            builder.Append(",");
        }

        /// <summary>
        /// set visor for a dude; visor is weird
        /// </summary>
        /// <param name="player">the player (0-2100+)</param>
        /// <param name="val">string representation of the face enum</param>
        private void SetVisor(int player, String val)
        {
            int loc1 = GetPlayerDataStart(player) + (int)PlayerOffsets.FaceMask;
            int loc2 = GetPlayerDataStart(player) + (int)PlayerOffsets.Face;
            Visor ret = (Visor)Enum.Parse(typeof(Visor), val);

            int dude = (int)ret;
            int b1 = GameSaveData[loc1];
            int b2 = GameSaveData[loc2];

            switch (ret)
            {
                case Visor.None:
                    b1 &= 0x7f;
                    b2 &= 0xfe;
                    break;
                case Visor.Clear:
                    b1 += 0x80;
                    b2 &= 0xfe;
                    break;
                case Visor.Dark:
                    b1 &= 0x7f;
                    b2 += 1;
                    break;
            }
            SetByte(loc1, (byte)b1);
            SetByte(loc2, (byte)b2);
        }

        //YaY! More crazy bit splitting
        private void GetSkin(int player, StringBuilder builder)
        {
            int loc1 = GetPlayerDataStart(player) + (int)PlayerOffsets.DOB; //skin is in the same bytes as DOB
            int loc2 = GetPlayerDataStart(player) + 1 + (int)PlayerOffsets.DOB; //skin is in the same bytes as DOB
            int b = GameSaveData[loc2] << 8;
            b += GameSaveData[loc1]; // Skin is in bits 5-9 of this int
            b &= 0xfff;
            b = b >> 7;
            Skin ret = (Skin)b;
            builder.Append(ret.ToString());
            builder.Append(",");
        }

        /// <summary>
        /// set skin for a dude
        /// I hope I don't have to look at this code in a year...
        /// Why on earth couldn't they just use simple bytes for each to represent everything????!!!!
        /// </summary>
        /// <param name="player">the player (0-2100+)</param>
        /// <param name="val">string representation of the face enum</param>
        private void SetSkin(int player, String val)
        {
            int loc1 = GetPlayerDataStart(player) + (int)PlayerOffsets.DOB; //skin is in the same bytes as DOB
            int loc2 = GetPlayerDataStart(player) + 1 + (int)PlayerOffsets.DOB; //skin is in the same bytes as DOB
            Skin ret = (Skin)Enum.Parse(typeof(Skin), val);
            int dude = (int)ret;
            dude = dude << 7;
            int b = GameSaveData[loc2] << 8;
            b += GameSaveData[loc1];
            b &= 0xf07f; // clear out bits 5-9
            b += dude;
            // now put hi byte in DOB+1; low byte in DOB
            int b1 = b & 0xff;
            int b2 = b >> 8;
            SetByte(loc1, (byte)b1);
            SetByte(loc2, (byte)b2);
        }

        private void GetHelmet(int player, StringBuilder builder)
        {
            Helmet retVal = Helmet.Standard;
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.Helmet_LeftShoe_RightShoe;
            // helmet is 2nd bit
            int val = GameSaveData[loc] & 0x40;
            if (val > 0)
                retVal = Helmet.Revolution;
            builder.Append(retVal.ToString());
            builder.Append(",");
        }

        private void SetHelmet(int player, String helmet)
        {
            Helmet h = (Helmet)Enum.Parse(typeof(Helmet), helmet);
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.Helmet_LeftShoe_RightShoe;
            // helmet is 2nd bit
            int val = GameSaveData[loc];
            if (h == Helmet.Standard)
                val = val & 0xBF;
            else
                val = val | 0x40;
            SetByte(loc, (byte)val);
        }

        private void GetLeftShoe(int player, StringBuilder builder)
        {
            Shoe retVal = Shoe.Shoe1;
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.Helmet_LeftShoe_RightShoe;
            // LShoe is last 3 bits; helmet is 2nd bit; RShoe is bits 3,4,5 
            int val = GameSaveData[loc] & 0x7;
            retVal = (Shoe)val;
            builder.Append(retVal.ToString());
            builder.Append(",");
        }

        private void SetLShoe(int player, String shoe)
        {
            Shoe h = (Shoe)Enum.Parse(typeof(Shoe), shoe);
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.Helmet_LeftShoe_RightShoe;
            //LShoe is last 3 bits; 
            int val = GameSaveData[loc] & 0xf8;
            val |= (int)val;
            SetByte(loc, (byte)val);
        }

        private void GetRightShoe(int player, StringBuilder builder)
        {
            Shoe retVal = Shoe.Shoe1;
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.Helmet_LeftShoe_RightShoe;
            // RShoe is bits 3,4,5 
            int val = GameSaveData[loc] & 0x38;
            val = val >> 3;
            retVal = (Shoe)val;
            builder.Append(retVal.ToString());
            builder.Append(",");
        }

        private void SetRShoe(int player, String shoe)
        {
            Shoe h = (Shoe)Enum.Parse(typeof(Shoe), shoe);
            int loc = GetPlayerDataStart(player) + (int) PlayerOffsets.Helmet_LeftShoe_RightShoe;
            // RShoe is bits 3,4,5 
            int val = GameSaveData[loc] & 0xc7;
            val |= (val << 3 ) ;
            SetByte(loc, (byte)val);
        }

        private void GetMouthPiece(int player, StringBuilder builder)
        {
            YesNo retVal = YesNo.No; // 3rd bit
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.MouthPiece_LeftGlove_Sleeves_NeckRoll;
            int val = (GameSaveData[loc] & 0x20) >> 5;
            retVal = (YesNo)val;
            builder.Append(retVal.ToString());
            builder.Append(",");
        }

        private void SetMouthPiece(int player, String piece)
        {
            YesNo h = (YesNo)Enum.Parse(typeof(YesNo), piece);
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.MouthPiece_LeftGlove_Sleeves_NeckRoll;
            int val = GameSaveData[loc] & 0xdf;
            val |= ((int)h << 5);
            SetByte(loc, (byte)val);
        }

        private void GetLeftGlove(int player, StringBuilder builder)
        {
            Glove retVal = Glove.None; 
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.MouthPiece_LeftGlove_Sleeves_NeckRoll;
            int val = (GameSaveData[loc] & 0xC0) >> 6;
            val += ((GameSaveData[loc+1] & 0x03) << 2);
            retVal = (Glove)val;
            builder.Append(retVal.ToString());
            builder.Append(",");
        }

        private void SetLeftGlove(int player, String glove)
        {
            Glove g = (Glove)Enum.Parse(typeof(Glove), glove);
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.MouthPiece_LeftGlove_Sleeves_NeckRoll;
            int val1 = GameSaveData[loc]; // 
            int val2 = GameSaveData[loc+1]; // most sig 2 bits of glove go to least sig bits in this value
            val1 = (val1 & 0x3f) + ( ((int)g & 3) << 6);
            val2 = (val2 & 0xfc) + ((int)g >> 2);
            SetByte(loc, (byte)val1);
            SetByte(loc+1, (byte)val2);
        }

        private void GetRightGlove(int player, StringBuilder builder)
        {
            Glove retVal = Glove.None;
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.RightGlove_LeftWrist;
            int val = (GameSaveData[loc] & 0x3c) >> 2;
            retVal = (Glove)val;
            builder.Append(retVal.ToString());
            builder.Append(",");
        }

        private void SetRightGlove(int player, String glove)
        {
            Glove g = (Glove)Enum.Parse(typeof(Glove), glove);
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.RightGlove_LeftWrist;
            int val = GameSaveData[loc];
            val = (val & 0xc3) + ((int)g << 2);
            SetByte(loc, (byte)val);
        }

        private void GetSleeves(int player, StringBuilder builder)
        {
            Sleeves retVal = Sleeves.None;
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.MouthPiece_LeftGlove_Sleeves_NeckRoll;
            int val = (GameSaveData[loc] & 3);
            retVal = (Sleeves)val;
            builder.Append(retVal.ToString());
            builder.Append(",");
        }

        private void SetSleeves(int player, String sleeve)
        {
            Sleeves s = (Sleeves)Enum.Parse(typeof(Sleeves), sleeve);
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.MouthPiece_LeftGlove_Sleeves_NeckRoll;
            int val = GameSaveData[loc] & 0xfc;
            val += (int)s;
            SetByte(loc, (byte)val);
        }

        private void GetNeckRoll(int player, StringBuilder builder)
        {
            NeckRoll retVal = NeckRoll.None;
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.MouthPiece_LeftGlove_Sleeves_NeckRoll;
            int val = ((GameSaveData[loc] & 0x1c) >> 2);
            retVal = (NeckRoll)val;
            builder.Append(retVal.ToString());
            builder.Append(",");
        }

        private void SetNeckRoll(int player, String roll)
        {
            NeckRoll s = (NeckRoll)Enum.Parse(typeof(NeckRoll), roll);
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.MouthPiece_LeftGlove_Sleeves_NeckRoll;
            int val = GameSaveData[loc] & 0xe3;
            val += ((int)s << 2);
            SetByte(loc, (byte)val);
        }

        private void GetLeftWrist(int player, StringBuilder builder)
        {
            Wrist retVal = Wrist.None;
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.RightGlove_LeftWrist;
            int val = ((GameSaveData[loc + 1] << 8) + GameSaveData[loc] )  >> 6;
            val &= 0xf;
            retVal = (Wrist)val;
            builder.Append(retVal.ToString());
            builder.Append(",");
        }

        private void SetLeftWrist(int player, String w)
        {
            Wrist s = (Wrist)Enum.Parse(typeof(Wrist), w);
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.RightGlove_LeftWrist;
            int val = ((GameSaveData[loc + 1] << 8) + GameSaveData[loc]) & 0xfcf3;
            val += ((int)s << 6);
            SetByte(loc, (byte)val);
            SetByte(loc + 1, (byte)(val >> 8));
        }

        private void GetRightWrist(int player, StringBuilder builder)
        {
            Wrist retVal = Wrist.None;
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.RightWrist_LeftElbow;
            int val = (GameSaveData[loc] & 0x3c) >> 2;
            retVal = (Wrist)val;
            builder.Append(retVal.ToString());
            builder.Append(",");
        }

        private void SetRightWrist(int player, String w)
        {
            Wrist s = (Wrist)Enum.Parse(typeof(Wrist), w);
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.RightWrist_LeftElbow;
            int val = GameSaveData[loc] & 0xc3;
            val += ((int)s << 2);
            SetByte(loc, (byte)val);
        }

        private void GetLeftElbow(int player, StringBuilder builder)
        {
            Elbow retVal = Elbow.None;
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.RightWrist_LeftElbow;
            int val = ((GameSaveData[loc + 1] << 8) + GameSaveData[loc]) & 0x3c0;
            val = val >> 6;
            retVal = (Elbow)val;
            builder.Append(retVal.ToString());
            builder.Append(",");
        }

        private void SetLeftElbow(int player, String w)
        {
            Elbow s = (Elbow)Enum.Parse(typeof(Elbow), w);
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.RightWrist_LeftElbow;
            int val1 = (GameSaveData[loc] & 0x3f) + (((int)s & 3 ) << 6) ;
            int val2 = (GameSaveData[loc+1] & 0xfc) + ((int)s & 0xfc );

            SetByte(loc, (byte)val1);
            SetByte(loc + 1, (byte)val2);
        }

        private void GetRightElbow(int player, StringBuilder builder)
        {
            Elbow retVal = Elbow.None;
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.RightElbow;
            int val = (GameSaveData[loc] & 0x3c) >> 2;
            retVal = (Elbow)val;
            builder.Append(retVal.ToString());
            builder.Append(",");
        }

        private void SetRightElbow(int player, String w)
        {
            Elbow s = (Elbow)Enum.Parse(typeof(Elbow), w);
            int loc = GetPlayerDataStart(player) + (int)PlayerOffsets.RightElbow;
            int val = (GameSaveData[loc] & 0xc3 ) + ((int)s << 2);
            SetByte(loc, (byte)val);
        }
    }
}
