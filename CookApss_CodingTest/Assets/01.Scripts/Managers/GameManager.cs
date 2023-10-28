using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class GameManager : Singleton<GameManager>
{
    public GameState state;
    public BattleInfo battleInfo;

    public void Awake()
    {
        Managers.Resource.LoadAllAsync<UnityEngine.Object>("Preload", _completeCallback: () => 
        {
            StartGame(() => 
            {
                state = GameState.BattleBefore;
                battleInfo = new BattleInfo();
            });
        });
    }
    
    public void StartGame(Action _callback)
    {
        Managers.Data.LoadPreData(() => { Managers.Scene.LoadScene(Define.Scene.Main); _callback?.Invoke(); });
    }

    public void SaveGame()
    {
        Managers.Data.SavePlayerData(Managers.Data.playerData);
    }

    public void OnApplicationPause(bool pause)
    {
        if (pause)
            SaveGame();
    }
}

public class BattleInfo
{
    public BattleEntityData[] armyRear;
    public BattleEntityData[] armyCenter;
    public BattleEntityData[] armyFront;

    public BattleEntityData[] enemyRear;
    public BattleEntityData[] enemyCenter;
    public BattleEntityData[] enemyFront;

    public int isCanUseBattleEntityCount;
    public int nowUseBattleEntityCount;
    public int battleForce;

    public Dictionary<string, int> specialties;

    public bool UseBattleEntity(BattleEntityData _data)
    {
        if (nowUseBattleEntityCount == isCanUseBattleEntityCount) return false;
        nowUseBattleEntityCount++;

        int nullIndex = armyFront.FindEmptyArrayIndex();
        if (nullIndex != -1)
        {
            armyFront[nullIndex] = _data;
            SetInformationValue();
            return true;
        }

        nullIndex = armyCenter.FindEmptyArrayIndex();
        if(nullIndex != -1)
        {
            armyCenter[nullIndex] = _data;
            SetInformationValue();
            return true;
        }

        nullIndex = armyRear.FindEmptyArrayIndex();
        if (nullIndex != -1)
        {
            armyRear[nullIndex] = _data;
            SetInformationValue();
            return true;
        }

        return false;
    }

    public void UnUseBattleEntity(BattleEntityData _data)
    {
        nowUseBattleEntityCount--;
        for (int i = 0; i < armyFront.Length; i++)
        {
            if (armyFront[i] == _data)
            {
                armyFront[i] = null;
                SetInformationValue();
                return;
            }
        }

        for (int i = 0; i < armyCenter.Length; i++)
        {
            if (armyFront[i] == _data)
            {
                armyFront[i] = null;
                SetInformationValue();
                return;
            }
        }

        for (int i = 0; i < armyRear.Length; i++)
        {
            if (armyFront[i] == _data)
            {
                armyFront[i] = null;
                SetInformationValue();
                return;
            }
        }
    }

    private void SetInformationValue()
    {
        //개인 특성을 추가 한다면 여기에 기능 추가
        battleForce = 0;
        for (int i = 0; i < armyFront.Length; i++)
        {
            if (armyFront[i] != null)
            {
                battleForce += armyFront[i].attackForce;
                battleForce += armyFront[i].maxHP;
            }
        }

        for (int i = 0; i < armyCenter.Length; i++)
        {
            if (armyCenter[i] != null)
            {
                battleForce += armyCenter[i].attackForce;
                battleForce += armyCenter[i].maxHP;
            }
        }

        for (int i = 0; i < armyRear.Length; i++)
        {
            if (armyRear[i] != null)
            {
                battleForce += armyRear[i].attackForce;
                battleForce += armyRear[i].maxHP;
            }
        }
        Managers.Event.OnVoidEvent?.Invoke(VoidEventType.OnChangeBattleInfo);
    }

    public BattleInfo()
    {
        armyRear = new BattleEntityData[3];
        armyCenter = new BattleEntityData[3];
        armyFront = new BattleEntityData[3];

        enemyRear = new BattleEntityData[3];
        enemyCenter = new BattleEntityData[3];
        enemyFront = new BattleEntityData[3];

        isCanUseBattleEntityCount = 3;
        specialties = new Dictionary<string, int>();
    }
}


