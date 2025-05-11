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

    // GameModeName
    public static readonly string Player_vs_Computer = "Player_vs_Computer";
    public static readonly string Player_vs_Player = "Player_vs_Player";
}
