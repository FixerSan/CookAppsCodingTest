using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Define;

public class ObjectManager
{
    private Transform entityTrans;
    public Transform EntityTrans
    {
        get
        {
            if (entityTrans == null)
            {
                GameObject go = GameObject.Find("@EntityTrans");
                if (go == null)
                    go = new GameObject(name: "@EntityTrans");
                entityTrans = go.transform;
            }
            return entityTrans;
        }
    }

    private Transform armyBattleEntityTrnas;
    public Transform ArmyBattleEntityTrnas
    {
        get
        {
            if (armyBattleEntityTrnas == null)
            {
                GameObject go = GameObject.Find("@ArmyBattleEntityTrans");
                if (go == null)
                {
                    go = new GameObject(name: "@ArmyBattleEntityTrans");
                    go.transform.SetParent(EntityTrans);
                }
                armyBattleEntityTrnas = go.transform;
            }
            return armyBattleEntityTrnas;
        }
    }
    public HashSet<BattleEntityController> Enemys { get; } = new HashSet<BattleEntityController>();

    private Transform enemyBattleEntityTrnas;
    public Transform EnemyBattleEntityTrnas
    {
        get
        {
            if (enemyBattleEntityTrnas == null)
            {
                GameObject go = GameObject.Find("@EnemyBattleEntityTrnas");
                if (go == null)
                {
                    go = new GameObject(name: "@EnemyBattleEntityTrnas");
                    go.transform.SetParent(EntityTrans);
                }
                enemyBattleEntityTrnas = go.transform;
            }
            return enemyBattleEntityTrnas;
        }
    }
    public HashSet<BattleEntityController> armys { get; } = new HashSet<BattleEntityController>();

    private BattleEntityController SpawnBattleEntity(Define.BattleEntity _entity, int _level, BattleEntityType _entityType)
    {
        BattleEntityController battleEntityController = Managers.Resource.Instantiate($"{_entity.ToString()}", _parent: ArmyBattleEntityTrnas).GetOrAddComponent<BattleEntityController>();
        BattleEntity battleEntity = null;
        Dictionary<Define.BattleEntityState, State<BattleEntityController>> states = new Dictionary<BattleEntityState, State<BattleEntityController>>();
        switch (_entity)
        {
            case Define.BattleEntity.Zero:
                states.Add(Define.BattleEntityState.Idle, new BattleEntityStates.Zero.Idle());
                states.Add(Define.BattleEntityState.Move, new BattleEntityStates.Zero.Move());
                states.Add(Define.BattleEntityState.Attack, new BattleEntityStates.Zero.Attack());
                states.Add(Define.BattleEntityState.SkillCast, new BattleEntityStates.Zero.SkillCast());
                states.Add(Define.BattleEntityState.Die, new BattleEntityStates.Zero.Die());
                states.Add(Define.BattleEntityState.EndBattle, new BattleEntityStates.Zero.EndBattle());
                break;

            case Define.BattleEntity.One:

                break;

            case Define.BattleEntity.Two:

                break;

            case Define.BattleEntity.Three:

                break;
        }

        if(battleEntityController != null)
        {
            battleEntityController.entityType = _entityType;
            battleEntityController.Init(battleEntity, states);
            return battleEntityController;        
        }

        Debug.Log("스폰을 실패하였습니다.");
        return null;
    }

}
