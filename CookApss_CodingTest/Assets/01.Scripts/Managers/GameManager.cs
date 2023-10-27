using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class GameManager : Singleton<GameManager>
{
    public GameState state;
    public PlayerData data;

    public void Awake()
    {
        Managers.Resource.LoadAllAsync<UnityEngine.Object>("Preload", _completeCallback: () => 
        { 
            StartGame();
        });
    }
    
    public void StartGame()
    {
        Managers.Data.LoadPreData();
    }

    public void SaveGame()
    {
        Managers.Data.SavePlayerData(Managers.Data.userData);
    }

    public void OnApplicationPause(bool pause)
    {
        if (pause)
            SaveGame();
    }
}


