using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    public PlayerData userData;

    public Dictionary<int, Dictionary<int,BattleEntityStatusData>> battleEntityStatusDatas;
    public Dictionary<int, DialogData> dialogDatas;
    public readonly string PLAYERSAVEDATA_PATH;

    public T Get<T>(int _UID) where T : Data
    {
        if (typeof(T) == typeof(PlayerData)) return LoadPlayerData(_UID) as T;
        if (typeof(T) == typeof(DialogData)) if (dialogDatas.TryGetValue(_UID, out DialogData _data)) return _data as T;

        Debug.LogError("데이터가 반환되지 않았습니다.");
        return null;
    }

    public void LoadPreData()
    {
        LoadPlayerData(Define.userUID);
        LoadBattleEntityStatusData();
    }

    private PlayerData LoadPlayerData(int _UID)
    {
        if(userData == null)
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

            userData = new PlayerData(saveData.UID, saveData.name, entityDatas);
        }
        return userData;
    }

    public void LoadBattleEntityStatusData()
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>("BattleEntityStatusData");
        BattleEntityStatusDatas _battleEntityStatusDatas = JsonUtility.FromJson<BattleEntityStatusDatas>(textAsset.text);

        for (int i = 0; i < Define.currentBattleEntityCount; i++)
        {
            battleEntityStatusDatas.Add(i, new Dictionary<int, BattleEntityStatusData>());
        }

        for (int i = 0; i < Define.currentBattleEntityMaxLevel; i++)
        {
            battleEntityStatusDatas[0].Add(_battleEntityStatusDatas.zero[i].level, _battleEntityStatusDatas.zero[i]);
            Debug.Log("데이터가 로드 되었습니다.");
            battleEntityStatusDatas[1].Add(_battleEntityStatusDatas.one[i].level, _battleEntityStatusDatas.one[i]);
            Debug.Log("데이터가 로드 되었습니다.");
            battleEntityStatusDatas[2].Add(_battleEntityStatusDatas.two[i].level, _battleEntityStatusDatas.two[i]);
            Debug.Log("데이터가 로드 되었습니다.");
            battleEntityStatusDatas[3].Add(_battleEntityStatusDatas.three[i].level, _battleEntityStatusDatas.three[i]);
            Debug.Log("데이터가 로드 되었습니다.");
        }
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

        PlayerSaveData saveData = new PlayerSaveData(_playerData.UID,_playerData.name, hasEntityUIDs, hasEntityLevel);
        string saveDataJson = JsonUtility.ToJson(saveData);

        File.WriteAllText(PLAYERSAVEDATA_PATH, saveDataJson);
    }

    public DataManager()
    {
        userData = null;
        dialogDatas = new Dictionary<int, DialogData> ();
        battleEntityStatusDatas = new Dictionary<int, Dictionary<int, BattleEntityStatusData>>();
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
    public List<BaseBattleEntityData> hasEntites;

    public PlayerData(int _UID, string _name, List<BaseBattleEntityData> _hasEntites)
    {
        UID = _UID;
        name = _name;
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

    public PlayerSaveData(int _UID, string _name, string _hasEntityUID, string _hasEntityLevel)
    {
        UID = _UID;
        name = _name;
        hasEntityUID = _hasEntityUID;
        hasEntityLevel = _hasEntityLevel;
    }
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
public class BaseBattleEntityData
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
public class BattleEntityStatusData
{
    public int UID;
    public int level;
    public int maxHP;
    public int attackForce;
    public float skillCooltime;
}

public class BattleEntityStatusDatas
{
    public BattleEntityStatusData[] zero;
    public BattleEntityStatusData[] one;
    public BattleEntityStatusData[] two;
    public BattleEntityStatusData[] three;
}