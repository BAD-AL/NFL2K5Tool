using System;
using System.Collections.Generic;
using System.Text;

namespace NFL2K5Tool
{
    /// <summary> Code to map player attributes to locations </summary>
    public enum PlayerOffsets
    {
        College=0,
        PBP = 4,
        Photo= 6,
        Helmet_LeftShoe_RightShoe = 0x0c, // LShoe is last 3 bits; helmet is 2nd bit; RShoe is bits 3,4,5 
        JerseyNumber = 0x20, // & 0x21

        Turtleneck_Body_EyeBlack_Hand_Dreads = 0x18, // Shared:  Turtleneck(bits6&7), Body(4&5), EyeBlack(3), Hand(2),  Dreads(1) (most sig  --> least sig )
        DOB = 0x19,
        MouthPiece_LeftGlove_Sleeves_NeckRoll= 0x1c,
        RightGlove_LeftWrist = 0x1d,
        RightWrist_LeftElbow = 0x1e,
        FaceMask = 0x21, // part if Wacko Visor is in this byte too (FaceMask = val & 0x1F >> 1)
        Face = 0x22, // part of Wacko Visor is in this byte too (Face = Val >>1 )
        Weight = 0x2A, // 150 + value
        Height = 0x2B, // (Inches)

        // Shared (Skin
        YearsPro = 0x25,
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
        RunRoute,
        Tackle,
        BreakTackle,
        PassAccuracy,
        PassReadCoverage,
        Catch,//0x44
        RunBlocking,
        PassBlocking,
        HoldOntoBall,
        PassRush,
        RunCoverage,
        KickAccuracy,
        Leadership = 0x4C,
        PowerRunStyle,
        Composure,
        Scramble,
        Consistency,
        Aggressiveness
    }

    public enum AppearanceAttributes
    {
        College, DOB, Hand, Weight, Height, BodyType, Skin, Face, Dreads, Helmet, FaceMask, Visor, EyeBlack, MouthPiece, LeftGlove, RightGlove, LeftWrist, RightWrist, LeftElbow, RightElbow, Sleeves, LeftShoe, RightShoe, NeckRoll, Turtleneck
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
        Neoprine,
        WhiteTurf,
        BlackTurf,
        Taped,
        HighWhite,
        HighBlack,
        HighTeam
    }
}
