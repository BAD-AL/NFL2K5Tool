using System;
using System.Collections.Generic;
using System.Text;

namespace NFL2K5Tool
{
    public enum SaveType
    {
        Roster,
        Franchise
    }

    // Addresses are based on a franchise file, not a roster file.
    /// <summary> Code to map player attributes to locations </summary>
    public enum PlayerOffsets
    {
        /**
         * College is tricky to calculate.
         * College strings start at 0x7a23c with Clemson, Duke, Florida State, Georgia Tech...
         * The college location looks like 4 byte ints which are all negative numbers ( like b1f7ffff; which == -2127).
         * Player 0 having a clemson pointer would be: fffff7b1 @location 0xb288.
         * Player 1 having a clemson pointer looks like: fffff75d @location 0xb2dc; this is 0x54 greater than what player 0's clemson pointer is.
         * The second college is Duke.
         * Player 0 having a Duke pointer would be: fffff7b9 @location 0xb288.
         *    The value difference from Clemson to Duke is 8; in fact, it looks like to increase a player's 
         *    college by 1 college you would add 8.
         * So the following formula is one we can use to calcualte college:
         * // p = integer player  (duane starks is the 0th player in the base save file)
         *  CollegeIndex(p) = (((collegePointerVal - (0xfffff7b1)) + player * 0x54)) / 8;
         */
        College=0,
        PBP = 4,
        Photo= 6,
        Helmet_LeftShoe_RightShoe = 0x0c, // LShoe is last 3 bits; helmet is 7th bit; RShoe is bits 4,5,6 
        Turtleneck_Body_EyeBlack_Hand_Dreads = 0x18, // Shared: Skin(8), Turtleneck(bits6&7), Body(4&5), EyeBlack(3), Hand(2),  Dreads(1) (most sig  --> least sig )
        DOB = 0x19, // Skin is shared in the second nibble at this location.
        MouthPiece_LeftGlove_Sleeves_NeckRoll= 0x1c,
        RightGlove_LeftWrist = 0x1d,
        RightWrist_LeftElbow = 0x1e,
        RightElbow = 0x1f,
        JerseyNumber = 0x20, // & 0x21
        FaceMask = 0x21, // part if Wacko Visor is in this byte too (FaceMask = val & 0x1F >> 1)
        Face = 0x22, // part of Wacko Visor is in this byte too (Face = Val >>1 )
        YearsPro = 0x25,
        Depth = 0x29,
        Weight = 0x2A, // 150 + value
        Height = 0x2B, // (Inches)


        Position = 0x35,
        Speed,
        Agility,
        PassArmStrength,
        Stamina,
        KickPower,
        Durability,
        Strength,
        Jumping,
        Coverage,
        RunRoute,//0x40
        Tackle,
        BreakTackle,
        PassAccuracy,
        PassReadCoverage,
        Catch,
        RunBlocking,
        PassBlocking,
        HoldOntoBall,
        PassRush,
        RunCoverage,
        KickAccuracy,
        Leadership = 0x4C,
        PowerRunStyle,
        Composure,
        Scramble,//4f
        Consistency,
        Aggressiveness
    }

    public enum AppearanceAttributes
    {
        College = 200, // starting here so that we have no collisions with the PlayerOffsets enum
        DOB, YearsPro, PBP, Photo, Hand, Weight, Height, BodyType, Skin, Face, Dreads, Helmet, FaceMask, Visor, 
        EyeBlack,  MouthPiece, LeftGlove, RightGlove, LeftWrist, RightWrist, LeftElbow, 
        RightElbow, Sleeves, LeftShoe, RightShoe, NeckRoll, Turtleneck
    }
    /// <summary> enum for positions </summary>
    public enum Positions
    {
        QB = 0,
        K,
        P,
        WR,
        CB,
        FS,
        SS,
        RB,
        FB,
        TE,
        OLB,
        ILB,
        C,
        G,
        T,
        DT,
        DE
    }

    /// <summary> power run style enum </summary>
    public enum PowerRunStyle
    {
        Finesse = 1,
        Balanced = 0x32,
        Power = 0x63
    }

    public enum Turtleneck
    {
        None = 0,
        White,
        Black,
        Team
    }

    public enum Body
    {
        Skinny = 0,
        Normal,
        Large,
        ExtraLarge
    }

    public enum YesNo
    {
        No =0,
        Yes
    }

    public enum Hand
    {
        Left=0,
        Right
    }

    public enum Face
    {
        Face1=0,
        Face2,
        Face3,
        Face4,
        Face5,
        Face6,
        Face7,
        Face8,
        Face9,
        Face10,
        Face11,
        Face12,
        Face13,
        Face14,
        Face15
    }

    public enum FaceMask
    {
        FaceMask1 = 0,
        FaceMask2,
        FaceMask3,
        FaceMask4,
        FaceMask5,
        FaceMask6,
        FaceMask7,
        FaceMask8,
        FaceMask9,
        FaceMask10,
        FaceMask11,
        FaceMask12,
        FaceMask13,
        FaceMask14,
        FaceMask15,
        FaceMask16,
        FaceMask17,
        FaceMask18,
        FaceMask19,
        FaceMask20,
        FaceMask21,
        FaceMask22,
        FaceMask23,
        FaceMask24,
        FaceMask25,
        FaceMask26,
        FaceMask27
    }

    public enum Visor
    {
        None,
        Dark,
        Clear
    }

    public enum Skin
    {
        Skin1,
        Skin2,
        Skin3,
        Skin4,
        Skin5,
        Skin6,
        Skin7,
        Skin8,
        Skin9,
        Skin10,
        Skin11,
        Skin12,
        Skin13,
        Skin14,
        Skin15,
        Skin16,
        Skin17,
        Skin18,
        Skin19,
        Skin20,
        Skin21,
        Skin22
    }

    public enum Helmet
    {
        Standard =0,
        Revolution
    }

    public enum Shoe
    {
        Shoe1,
        Shoe2,
        Shoe3,
        Shoe4,
        Shoe5,
        Shoe6,
        Taped
    }

    public enum Glove
    {
        None,
        Type1,
        Type2,
        Type3,
        Type4,
        Team1,
        Team2,
        Team3,
        Team4,
        Taped
    }

    public enum Sleeves
    {
        None,
        White,
        Black,
        Team
    }

    public enum NeckRoll
    {
        None,
        Collar,
        Roll,
        Washboard,
        Bulging
    }

    public enum Wrist
    {
        None,
        SingleWhite,
        DoubleWhite,
        SingleBlack,
        DoubleBlack,
        NeopreneSmall,
        NeopreneLarge,
        ElasticSmall,
        ElasticLarge,
        SingleTeam,
        DoubleTeam,
        TapedSmall,
        TapedLarge,
        Quarterback
    }

    public enum Elbow
    {
        None,
        White,
        Black,
        WhiteBlackStripe,
        BlackWhiteStripe,
        BlackTeamStripe,
        Team,
        WhiteTeamStripe,
        Elastic,
        Neoprene,
        WhiteTurf,
        BlackTurf,
        Taped,
        HighWhite,
        HighBlack,
        HighTeam
    }


    public enum Game
    {
        HomeTeam,
        AwayTeam,
        Month,
        Day,
        YearTwoDigit,
        HourOfDay,
        MinuteOfHour,
        NullByte
    }

    public enum SpecialTeamer
    {
        KR1 = 0x195,
        KR2 = 0x196,
        LS  = 0x198,
        PR  = 0x199
    }
}
