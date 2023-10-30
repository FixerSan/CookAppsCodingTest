using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define 
{
    public static readonly int currentBattleEntityCount = 6;
    public static readonly int currentBattleEntityMaxLevel = 5;
    public static readonly int userUID = 0;

    public enum GameState
    {
        BattleBefore,
        BattleProgress,
        BattleAfter
    }

    public enum GameResult
    {
        Victory, Lose
    }
    
    public enum BattleEntity
    {
        Zero = 0,
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5
    }

    public enum BattleEntityType
    {
        Army, Enemy
    }

    public enum BattleEntityState
    {
        Idle, Move, Follow, Attack, SkillCast, Die, EndBattle
    }

    public enum MainSpecialty
    {

    }
    public enum SubSpecialty
    {

    }

    public enum StageState
    {
        ZeroStar, OneStar, TwoStar, ThreeStar
    }

    public enum VoidEventType
    {
        OnChangeBattleInfo, OnChangeControllerStatus
    }

    public enum IntEventType
    {

    }

    public enum UIEventType
    {
        Click,
        Pressed,
        PointerDown,
        PointerUp,
        BeginDrag,
        Drag,
        EndDrag,
    }

    public enum SpeakerType
    {
        OneButton, 
        TwoButton, 
        ThreeButton
    }

    public enum Scene
    {
        Main, Stage
    }
}
