using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageScene : BaseScene
{
    public Dictionary<string, Transform> poses;
    public override void Init(Action _callback = null)
    {
        poses = new Dictionary<string, Transform>();
        string[] names = Enum.GetNames(typeof(Trans));
        Transform posesTrans = GameObject.Find("@Trans").transform;

        for (int i = 0; i < names.Length; i++)
            poses.Add(names[i], Util.FindChild<Transform>(posesTrans.gameObject, _name: names[i], true));

        Managers.Object.SpawnArmyBattleEntity(Define.BattleEntity.Zero, 1, poses[Trans.Trans_ArmyFront_OneOrThree_Spawn.ToString()].GetChild(0).position).SetTarget(poses[Trans.Trans_ArmyFront_OneOrThree_Stop.ToString()].GetChild(0));
        Managers.Object.SpawnArmyBattleEntity(Define.BattleEntity.One, 1, poses[Trans.Trans_ArmyFront_OneOrThree_Spawn.ToString()].GetChild(1).position).SetTarget(poses[Trans.Trans_ArmyFront_OneOrThree_Stop.ToString()].GetChild(1));
        Managers.Object.SpawnArmyBattleEntity(Define.BattleEntity.Two, 1, poses[Trans.Trans_ArmyFront_OneOrThree_Spawn.ToString()].GetChild(2).position).SetTarget(poses[Trans.Trans_ArmyFront_OneOrThree_Stop.ToString()].GetChild(2)); ;

        Managers.Object.SpawnArmyBattleEntity(Define.BattleEntity.Three, 1, poses[Trans.Trans_ArmyCenter_Two_Spawn.ToString()].GetChild(0).position).SetTarget(poses[Trans.Trans_ArmyCenter_Two_Stop.ToString()].GetChild(0));
        Managers.Object.SpawnArmyBattleEntity(Define.BattleEntity.Four, 1, poses[Trans.Trans_ArmyCenter_Two_Spawn.ToString()].GetChild(1).position).SetTarget(poses[Trans.Trans_ArmyCenter_Two_Stop.ToString()].GetChild(1));

        Managers.Object.SpawnArmyBattleEntity(Define.BattleEntity.Five, 1, poses[Trans.Trans_ArmyRear_OneOrThree_Spawn.ToString()].GetChild(1).position).SetTarget(poses[Trans.Trans_ArmyRear_OneOrThree_Stop.ToString()].GetChild(1));

        foreach (var item in Managers.Object.Armys)
        {
            item.ChangeState(Define.BattleEntityState.Move);
        }
    }

    public override void Clear()
    {

    }

    public override void SceneEvent(int _eventIndex, Action _callback = null)
    {

    }

    private enum Trans
    {
        Trans_ArmyFront_OneOrThree_Spawn,
        Trans_ArmyFront_Two_Spawn, 
        Trans_ArmyCenter_OneOrThree_Spawn,
        Trans_ArmyCenter_Two_Spawn,
        Trans_ArmyRear_OneOrThree_Spawn,
        Trans_ArmyRear_Two_Spawn,
        Trans_ArmyFront_OneOrThree_Stop,
        Trans_ArmyFront_Two_Stop,
        Trans_ArmyCenter_OneOrThree_Stop,
        Trans_ArmyCenter_Two_Stop, 
        Trans_ArmyRear_OneOrThree_Stop,
        Trans_ArmyRear_Two_Stop
    }
}
