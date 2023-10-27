using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            Managers.Object.SpawnArmyBattleEntity((Define.BattleEntity)Managers.Data.playerData.hasEntites[0].UID, Managers.Data.playerData.hasEntites[0].level);
    }
}
