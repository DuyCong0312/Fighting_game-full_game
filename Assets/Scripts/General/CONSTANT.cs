using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CONSTANT 
{
    // Tag
    public static readonly string MainCamera = "MainCamera";
    public static readonly string MapBorder = "MapBorder";
    public static readonly string Player = "Player";
    public static readonly string Com = "Com";
    public static readonly string Ground = "Ground";

    // Layer
    public static readonly string Dashing = "Dashing";

    // Animator
        // Animator Trigger
    public static readonly string Iskill = "Iskill";
    public static readonly string Uskill = "Uskill";
    public static readonly string Attack = "Attack";
    public static readonly string FirstAttack = "0Attack";
    public static readonly string SecondAttack = "1Attack";
    public static readonly string ThirdAttack = "2Attack";
    public static readonly string getHurt = "getHurt";
        // Animator Name
    public static readonly string IKskill = "K+I";
    public static readonly string UKskill = "K+U";
    public static readonly string airAttack = "K+J";
    public static readonly string StartPose = "start_pose";
    public static readonly string WinPose = "win_pose";
    public static readonly string LosePose = "lose_pose";
        // Animator bool
    public static readonly string isDashing = "isDashing";
    public static readonly string Running = "Running";
    public static readonly string Jumping = "Jumping";
    public static readonly string Falling = "Falling";
    public static readonly string isAttack = "isAttack";
    public static readonly string isDefend = "isDefend";
        // Animator int
    public static readonly string CurrentState = "CurrentState";

    // Audio
    public static readonly string Audio = "Audio";
    public static readonly string SFX = "SFX";
    public static readonly string AudioVolume = "AudioVolume";
    public static readonly string SFXVolume = "SFXVolume";

    // PlayerPrefsKey
    public static readonly string SelectedFirstFighterIndex = "SelectedFirstFighterIndex";
    public static readonly string SelectedSecondFighterIndex = "SelectedSecondFighterIndex";
    public static readonly string SelectedMapIndex = "SelectedMapIndex";

    // GameModeName
    public static readonly string Player_vs_Computer = "Player_vs_Computer";
    public static readonly string Player_vs_Player = "Player_vs_Player";
    public static readonly string Training = "Training";

    // GameSetting
    public static readonly string PlayerHealth = "PlayerHealth";
    public static readonly string TimePerRound = "TimePerRound";
    // PlayerInput
        // P1
    public static readonly string P1_Up = "P1_Up";
    public static readonly string P1_Down = "P1_Down";
    public static readonly string P1_Left = "P1_Left";
    public static readonly string P1_Right = "P1_Right";
    public static readonly string P1_Attack = "P1_Attack";
    public static readonly string P1_Jump = "P1_Jump";
    public static readonly string P1_Dash = "P1_Dash";
    public static readonly string P1_Skill = "P1_Skill";
    public static readonly string P1_SpSkill = "P1_SpSkill";
        // P2
    public static readonly string P2_Up = "P2_Up";
    public static readonly string P2_Down = "P2_Down";
    public static readonly string P2_Left = "P2_Left";
    public static readonly string P2_Right = "P2_Right";
    public static readonly string P2_Attack = "P2_Attack";
    public static readonly string P2_Jump = "P2_Jump";
    public static readonly string P2_Dash = "P2_Dash";
    public static readonly string P2_Skill = "P2_Skill";
    public static readonly string P2_SpSkill = "P2_SpSkill";
}
