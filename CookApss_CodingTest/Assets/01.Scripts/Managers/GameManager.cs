using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
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
    private void Update()
    {
        if(battleInfo != null)
            battleInfo.Update();
    }

    public void OnApplicationPause(bool pause)
    {
        if (pause)
            SaveGame();
    }
}

[System.Serializable]
public class BattleInfo
{
    public StageData currentStage;

    public BattleEntityData[] armyFront;
    public BattleEntityData[] armyCenter;
    public BattleEntityData[] armyRear;

    public int isCanUseBattleEntityCount;
    public int nowUseBattleEntityCount;
    public int armyCurrentHP;
    public int armyMaxHP;
    public int armyAttackForce;
    public int armybattleForce;

    public BattleEntityData[] enemyFront;
    public BattleEntityData[] enemyCenter;
    public BattleEntityData[] enemyRear;

    public int nowEnemyCount;
    public int enemyCurrentHP;
    public int enemyMaxHP;
    public int enemyAttackForce;
    public int enemybattleForce;

    public Dictionary<string, int> specialties;

    public bool isFastSpeed;
    public float time;

    public List<BattleEntityController> battleMVPPoints;
    public int allBattlePoint = 0;

    public bool UseBattleEntity(BattleEntityData _data)
    {
        if (nowUseBattleEntityCount == isCanUseBattleEntityCount) return false;
        nowUseBattleEntityCount++;

        int nullIndex = armyFront.FindEmptyArrayIndex();
        if (nullIndex != -1)
        {
            armyFront[nullIndex] = _data;
            SetArmyBattleForceValue();
            return true;
        }

        nullIndex = armyCenter.FindEmptyArrayIndex();
        if(nullIndex != -1)
        {
            armyCenter[nullIndex] = _data;
            SetArmyBattleForceValue();
            return true;
        }

        nullIndex = armyRear.FindEmptyArrayIndex();
        if (nullIndex != -1)
        {
            armyRear[nullIndex] = _data;
            SetArmyBattleForceValue();
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
                SetArmyBattleForceValue();
                return;
            }
        }

        for (int i = 0; i < armyCenter.Length; i++)
        {
            if (armyCenter[i] == _data)
            {
                armyCenter[i] = null;
                SetArmyBattleForceValue();
                return;
            }
        }

        for (int i = 0; i < armyRear.Length; i++)
        {
            if (armyRear[i] == _data)
            {
                armyRear[i] = null;
                SetArmyBattleForceValue();
                return;
            }
        }
    }
    public void UnUseAllBattleEntity()
    {
        armyFront = new BattleEntityData[3];
        armyCenter = new BattleEntityData[3];
        armyRear = new BattleEntityData[3];

        nowUseBattleEntityCount = 0;
        armybattleForce = 0;

        UpdateUI();
    }
    private void SetArmyBattleForceValue()
    {
        //개인 특성을 추가 한다면 여기에 기능 추가
        armyAttackForce = 0;
        armyMaxHP = 0;
        armybattleForce = 0;
        for (int i = 0; i < armyFront.Length; i++)
        {
            if (armyFront[i] != null)
            {
                armyAttackForce += armyFront[i].attackForce;
                armyMaxHP += armyFront[i].maxHP;
            }
        }

        for (int i = 0; i < armyCenter.Length; i++)
        {
            if (armyCenter[i] != null)
            {
                armyAttackForce += armyCenter[i].attackForce;
                armyMaxHP += armyCenter[i].maxHP;
            }
        }

        for (int i = 0; i < armyRear.Length; i++)
        {
            if (armyRear[i] != null)
            {
                armyAttackForce += armyRear[i].attackForce;
                armyMaxHP += armyRear[i].maxHP;
            }
        }

        armybattleForce = armyAttackForce + armyMaxHP;
        UpdateUI();
    }
    public void SetStageData(int _UID)
    {
        currentStage = Managers.Data.GetStageData(_UID);

        nowEnemyCount = 0;
        enemyAttackForce = 0;
        enemyMaxHP = 0;
        enemybattleForce = 0;
        time = 60;

        enemyFront = new BattleEntityData[3];
        for (int i = 0; i < currentStage.frontEnemyUIDs.Length; i++)
        {
            BattleEntityData data = Managers.Data.GetBattleEntityData(currentStage.frontEnemyUIDs[i], currentStage.frontEnemyLevels[i]);
            enemyFront[i] = data;
            nowEnemyCount++;
            enemyAttackForce += data.attackForce;
            enemyMaxHP += data.maxHP;
        }
        enemyCenter = new BattleEntityData[3];
        for (int i = 0; i < currentStage.centerEnemyUIDs.Length; i++)
        {
            BattleEntityData data = Managers.Data.GetBattleEntityData(currentStage.centerEnemyUIDs[i], currentStage.centerEnemyLevels[i]);
            enemyFront[i] = data;
            nowEnemyCount++;
            enemyAttackForce += data.attackForce;
            enemyMaxHP += data.maxHP;
        }

        enemyRear = new BattleEntityData[3];
        for (int i = 0; i < currentStage.rearEnemyUIDs.Length; i++)
        {
            BattleEntityData data = Managers.Data.GetBattleEntityData(currentStage.rearEnemyUIDs[i], currentStage.rearEnemyLevels[i]);
            enemyFront[i] = data;
            nowEnemyCount++;
            enemyAttackForce += data.attackForce;
            enemyMaxHP += data.maxHP;
        }

        enemybattleForce = enemyMaxHP + enemyAttackForce;
    }
    public void StartStage()
    {
        armyCurrentHP = armyMaxHP;
        enemyCurrentHP = enemyMaxHP;

        battleMVPPoints.Clear();
        battleMVPPoints = Managers.Object.Armys;
        UpdateUI();
    }   

    public void UpdateMVPPoints()
    {
        List<BattleEntityController> tempList = new List<BattleEntityController>();
        bool[] isChecked = new bool[Managers.Object.Armys.Count];
        for (int i = 0; i < isChecked.Length; i++)
            isChecked[i] = false;
        int bestPoint = 0;
        int bestIndex = -1;

        for (int i = 0; i < Managers.Object.Armys.Count; i++)
        {
            bestPoint = 0;
            for (int j = 0; j < Managers.Object.Armys.Count; j++)
            {
                if (isChecked[j]) continue;
                if(bestPoint < Managers.Object.Armys[j].mvpPoint)
                {
                    bestPoint = Managers.Object.Armys[j].mvpPoint;
                    bestIndex = j;
                }
            }
            isChecked[bestIndex] = true;
            tempList.Add(Managers.Object.Armys[bestIndex]);
        }

        battleMVPPoints = tempList;
        allBattlePoint = 0;

        for (int i = 0; i < battleMVPPoints.Count; i++)
            allBattlePoint += battleMVPPoints[i].mvpPoint;

        UpdateUI();
    }
    public void UpdateTeamHP(VoidEventType _type)
    {
        if (_type != VoidEventType.OnChangeControllerStatus) return;
        armyCurrentHP = 0;
        enemyCurrentHP = 0;
        foreach (var item in Managers.Object.Armys)
            armyCurrentHP += item.status.CurrentHP;
        foreach (var item in Managers.Object.Enemys)
            enemyCurrentHP += item.status.CurrentHP;
        UpdateUI();
    }
    public void ChangeFastSpeed()
    {
        isFastSpeed = !isFastSpeed;
        if (isFastSpeed) Time.timeScale = 1.5f;
        else Time.timeScale = 1.0f;
    }
    public void UpdateUI()
    {
        Managers.Event.OnVoidEvent?.Invoke(VoidEventType.OnChangeBattleInfo);
    }
    public void CheckTime()
    {
        if (Managers.Game.state == GameState.BattleProgress)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                time = 0;
                TimeOver();
            }
        }
    }
    public void TimeOver()
    {

    }
    public void Update()
    {
        CheckTime();
    }

    public BattleInfo()
    {
        armyRear = new BattleEntityData[3];
        armyCenter = new BattleEntityData[3];
        armyFront = new BattleEntityData[3];

        enemyRear = new BattleEntityData[3];
        enemyCenter = new BattleEntityData[3];
        enemyFront = new BattleEntityData[3];

        isCanUseBattleEntityCount = 4;
        specialties = new Dictionary<string, int>();
        battleMVPPoints = new List<BattleEntityController>();

        Managers.Event.OnVoidEvent -= UpdateTeamHP;
        Managers.Event.OnVoidEvent += UpdateTeamHP;
    }
}