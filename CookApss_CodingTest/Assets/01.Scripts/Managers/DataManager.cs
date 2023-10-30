using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;


public class DataManager
{
    public PlayerData playerData;

    public Dictionary<int, Dictionary<int,BattleEntityData>> battleEntityStatusDatas;
    public Dictionary<int, KnightageData> knightageDatas;
    public Dictionary<int, DialogData> dialogDatas;
    public Dictionary<int, StageData> stageDatas;
    public readonly string PLAYERSAVEDATA_PATH;

    public PlayerData GetPlayerData(int _UID)
    {
        return LoadPlayerData(_UID);
    }

    public StageData GetStageData(int _UID)
    {
        if (stageDatas.TryGetValue(_UID, out StageData _data)) return _data;
        return null;
    }

    public DialogData GetDialogData(int _UID)
    {
        if (dialogDatas.TryGetValue(_UID, out DialogData _data)) return _data;
        return null;
    }

    public BattleEntityData GetBattleEntityData(int _UID, int _level)
    {
        if (battleEntityStatusDatas.TryGetValue(_UID, out Dictionary<int, BattleEntityData> datas)) if (datas.TryGetValue(_level, out BattleEntityData data)) return data;
        return null;

    }

    public void LoadPreData(Action _callback)
    {
        GetPlayerData(Define.userUID);
        LoadBattleEntityStatusData();
        LoadStageData();

        _callback?.Invoke();
    }

    private PlayerData LoadPlayerData(int _UID)
    {
        if(playerData == null)
        {
            TextAsset textAsset = Managers.Resource.Load<TextAsset>("PlayerSaveData");
            PlayerSaveData saveData = JsonUtility.FromJson<PlayerSaveData>(textAsset.text);

            string[] entityUIDs = saveData.hasEntityUID.Split(',');
            string[] entityLevels = saveData.hasEntityLevel.Split(',');

            List<BaseBattleEntityData> entityDatas = new List<BaseBattleEntityData>();

            for (int i = 0; i < entityUIDs.Length; i++)
            {
                BaseBattleEntityData entityData = new BaseBattleEntityData(Int32.Parse(entityUIDs[i]), Int32.Parse(entityLevels[i]));
                entityDatas.Add(entityData);
            }

            playerData = new PlayerData(saveData.UID, saveData.name, saveData.knightageLevel, entityDatas);
        }
        return playerData;
    }

    public void LoadBattleEntityStatusData()
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>("BattleEntityData");
        BattleEntityDatas _battleEntityStatusDatas = JsonUtility.FromJson<BattleEntityDatas>(textAsset.text);

        for (int i = 0; i < Define.currentBattleEntityCount; i++)
        {
            battleEntityStatusDatas.Add(i, new Dictionary<int, BattleEntityData>());
        }

        for (int i = 0; i < Define.currentBattleEntityMaxLevel; i++)
        {
            battleEntityStatusDatas[0].Add(_battleEntityStatusDatas.zero[i].level, _battleEntityStatusDatas.zero[i]);
            battleEntityStatusDatas[1].Add(_battleEntityStatusDatas.one[i].level, _battleEntityStatusDatas.one[i]);
            battleEntityStatusDatas[2].Add(_battleEntityStatusDatas.two[i].level, _battleEntityStatusDatas.two[i]);
            battleEntityStatusDatas[3].Add(_battleEntityStatusDatas.three[i].level, _battleEntityStatusDatas.three[i]);
            battleEntityStatusDatas[4].Add(_battleEntityStatusDatas.four[i].level, _battleEntityStatusDatas.four[i]);
            battleEntityStatusDatas[5].Add(_battleEntityStatusDatas.five[i].level, _battleEntityStatusDatas.five[i]);
        }
    }

    public void LoadStageData()
    {
        StageDatas datas = Managers.Resource.Load<StageDatas>("OneChapterStageData.Data");
        for (int i = 0; i < datas.normal.Length; i++)
            stageDatas.Add(datas.normal[i].UID, datas.normal[i]);

        for (int i = 0; i < datas.hard.Length; i++)
            stageDatas.Add(datas.hard[i].UID, datas.hard[i]);
    }

    public void SavePlayerData(PlayerData _playerData)
    {
        string hasEntityUIDs = string.Empty;
        string hasEntityLevel = string.Empty;

        for (int i = 0; i < _playerData.hasEntites.Count; i++)
        {
            if(i == _playerData.hasEntites.Count-1)
            {
                hasEntityUIDs += (_playerData.hasEntites[i].UID);
                hasEntityLevel += (_playerData.hasEntites[i].level);
                continue;
            }
            hasEntityUIDs += (_playerData.hasEntites[i].UID + ",");
            hasEntityLevel += (_playerData.hasEntites[i].level + ",");
        }

        PlayerSaveData saveData = new PlayerSaveData(_playerData.UID, _playerData.name, _playerData.knightageLevel, hasEntityUIDs, hasEntityLevel);
        string saveDataJson = JsonUtility.ToJson(saveData);

        File.WriteAllText(PLAYERSAVEDATA_PATH, saveDataJson);
    }

    public DataManager()
    {
        playerData = null;
        dialogDatas = new Dictionary<int, DialogData> ();
        battleEntityStatusDatas = new Dictionary<int, Dictionary<int, BattleEntityData>>();
        knightageDatas = new Dictionary<int,  KnightageData>();
        stageDatas = new Dictionary<int, StageData>();
        PLAYERSAVEDATA_PATH = Path.Combine(Application.dataPath + "/04.Datas/", "PlayerSaveData.txt");
    }
}



public class Data
{

}

[System.Serializable]
public class PlayerData : Data
{
    public int UID;
    public string name;
    public int knightageLevel;
    public List<BaseBattleEntityData> hasEntites;

    public PlayerData(int _UID, string _name, int _knightageLevel, List<BaseBattleEntityData> _hasEntites)
    {
        UID = _UID;
        name = _name;
        knightageLevel = _knightageLevel;
        hasEntites = _hasEntites;
    }
}

[System.Serializable]
public class PlayerSaveData : Data
{
    public int UID;
    public string name;
    public string hasEntityUID;
    public string hasEntityLevel;
    public int knightageLevel;

    public PlayerSaveData(int _UID, string _name, int _knightageLevel, string _hasEntityUID, string _hasEntityLevel)
    {
        UID = _UID;
        name = _name;
        knightageLevel = _knightageLevel;
        hasEntityUID = _hasEntityUID;
        hasEntityLevel = _hasEntityLevel;
    }
}

[System.Serializable]
public class KnightageData
{
    public int level;
    public int maxBattleEntityCount;
}

[System.Serializable]
public class KnightageDatas
{
    public KnightageData[] knightageDatas;
}


[System.Serializable]
public class DialogData : Data
{
    public int dialogUID;
    public string speakerName;
    public string speakerImageKey;
    public string speakerType;
    public string sentence;
    public string buttonOneContent;
    public string buttonTwoContent;
    public string buttonThreeContent;
    public int nextDialogUID;
}

[System.Serializable]
public class BaseBattleEntityData : Data
{
    public int UID;
    public int level;

    public BaseBattleEntityData(int _UID, int _level)
    {
        UID = _UID;
        level = _level;
    }
}

[System.Serializable]
public class BattleEntityData : Data
{
    public int UID;
    public string name;
    public int level;
    public int maxHP;
    public int attackForce;
    public float skillCooltime;
    public int moveSpeed;
    public float canAttackDistance;
    public float attackCycle;
}

public class BattleEntityDatas : Data
{
    public BattleEntityData[] zero;
    public BattleEntityData[] one;
    public BattleEntityData[] two;
    public BattleEntityData[] three;
    public BattleEntityData[] four;
    public BattleEntityData[] five;
}

