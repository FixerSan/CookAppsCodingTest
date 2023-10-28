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
    public HashSet<BattleEntityController> Armys { get; } = new HashSet<BattleEntityController>();

    public BattleEntityController SpawnArmyBattleEntity(Define.BattleEntity _entity, int _level,Vector2 _position = new Vector2())
    {
        BattleEntityController battleEntityController = Managers.Resource.Instantiate($"{_entity.ToString()}", _parent: ArmyBattleEntityTrnas).GetOrAddComponent<BattleEntityController>();
        battleEntityController.transform.position = _position;
        BattleEntity battleEntity = null;
        BattleEntityStatus status = null;
        BattleEntityData data = null;
        Dictionary<Define.BattleEntityState, State<BattleEntityController>> states = new Dictionary<BattleEntityState, State<BattleEntityController>>();
        switch (_entity)
        {
            case Define.BattleEntity.Zero:
                states.Add(Define.BattleEntityState.Idle, new BattleEntityStates.Base.Idle());
                states.Add(Define.BattleEntityState.Move, new BattleEntityStates.Base.Move());
                states.Add(Define.BattleEntityState.Follow, new BattleEntityStates.Base.Follow());
                states.Add(Define.BattleEntityState.Attack, new BattleEntityStates.Zero.Attack());
                states.Add(Define.BattleEntityState.SkillCast, new BattleEntityStates.Zero.SkillCast());
                states.Add(Define.BattleEntityState.Die, new BattleEntityStates.Zero.Die());
                states.Add(Define.BattleEntityState.EndBattle, new BattleEntityStates.Zero.EndBattle());

                data = Managers.Data.GetBattleEntityData((int)_entity, _level);
                status = new BattleEntityStatus(data.maxHP, data.maxHP, data.attackForce, data.skillCooltime, data.skillCooltime, data.moveSpeed);
                battleEntity = new BattleEntites.Zero(battleEntityController);
                break;

            case Define.BattleEntity.One:
                states.Add(Define.BattleEntityState.Idle, new BattleEntityStates.Base.Idle());
                states.Add(Define.BattleEntityState.Move, new BattleEntityStates.Base.Move());
                states.Add(Define.BattleEntityState.Follow, new BattleEntityStates.Base.Follow());
                states.Add(Define.BattleEntityState.Attack, new BattleEntityStates.One.Attack());
                states.Add(Define.BattleEntityState.SkillCast, new BattleEntityStates.One.SkillCast());
                states.Add(Define.BattleEntityState.Die, new BattleEntityStates.One.Die());
                states.Add(Define.BattleEntityState.EndBattle, new BattleEntityStates.One.EndBattle());

                data = Managers.Data.GetBattleEntityData((int)_entity, _level);
                status = new BattleEntityStatus(data.maxHP, data.maxHP, data.attackForce, data.skillCooltime, data.skillCooltime, data.moveSpeed);
                battleEntity = new BattleEntites.Zero(battleEntityController);
                break;

            case Define.BattleEntity.Two:
                states.Add(Define.BattleEntityState.Idle, new BattleEntityStates.Base.Idle());
                states.Add(Define.BattleEntityState.Move, new BattleEntityStates.Base.Move());
                states.Add(Define.BattleEntityState.Follow, new BattleEntityStates.Base.Follow());
                states.Add(Define.BattleEntityState.Attack, new BattleEntityStates.Two.Attack());
                states.Add(Define.BattleEntityState.SkillCast, new BattleEntityStates.Two.SkillCast());
                states.Add(Define.BattleEntityState.Die, new BattleEntityStates.Two.Die());
                states.Add(Define.BattleEntityState.EndBattle, new BattleEntityStates.Two.EndBattle());

                data = Managers.Data.GetBattleEntityData((int)_entity, _level);
                status = new BattleEntityStatus(data.maxHP, data.maxHP, data.attackForce, data.skillCooltime, data.skillCooltime, data.moveSpeed);
                battleEntity = new BattleEntites.Zero(battleEntityController);
                break;

            case Define.BattleEntity.Three:
                states.Add(Define.BattleEntityState.Idle, new BattleEntityStates.Base.Idle());
                states.Add(Define.BattleEntityState.Move, new BattleEntityStates.Base.Move());
                states.Add(Define.BattleEntityState.Follow, new BattleEntityStates.Base.Follow());
                states.Add(Define.BattleEntityState.Attack, new BattleEntityStates.Three.Attack());
                states.Add(Define.BattleEntityState.SkillCast, new BattleEntityStates.Three.SkillCast());
                states.Add(Define.BattleEntityState.Die, new BattleEntityStates.Three.Die());
                states.Add(Define.BattleEntityState.EndBattle, new BattleEntityStates.Three.EndBattle());

                data = Managers.Data.GetBattleEntityData((int)_entity, _level);
                status = new BattleEntityStatus(data.maxHP, data.maxHP, data.attackForce, data.skillCooltime, data.skillCooltime, data.moveSpeed);
                battleEntity = new BattleEntites.Zero(battleEntityController);
                break;

            case Define.BattleEntity.Four:
                states.Add(Define.BattleEntityState.Idle, new BattleEntityStates.Base.Idle());
                states.Add(Define.BattleEntityState.Move, new BattleEntityStates.Base.Move());
                states.Add(Define.BattleEntityState.Follow, new BattleEntityStates.Base.Follow());
                states.Add(Define.BattleEntityState.Attack, new BattleEntityStates.Four.Attack());
                states.Add(Define.BattleEntityState.SkillCast, new BattleEntityStates.Four.SkillCast());
                states.Add(Define.BattleEntityState.Die, new BattleEntityStates.Four.Die());
                states.Add(Define.BattleEntityState.EndBattle, new BattleEntityStates.Four.EndBattle());

                data = Managers.Data.GetBattleEntityData((int)_entity, _level);
                status = new BattleEntityStatus(data.maxHP, data.maxHP, data.attackForce, data.skillCooltime, data.skillCooltime, data.moveSpeed);
                battleEntity = new BattleEntites.Zero(battleEntityController);
                break;
            case Define.BattleEntity.Five:
                states.Add(Define.BattleEntityState.Idle, new BattleEntityStates.Base.Idle());
                states.Add(Define.BattleEntityState.Move, new BattleEntityStates.Base.Move());
                states.Add(Define.BattleEntityState.Follow, new BattleEntityStates.Base.Follow());
                states.Add(Define.BattleEntityState.Attack, new BattleEntityStates.Five.Attack());
                states.Add(Define.BattleEntityState.SkillCast, new BattleEntityStates.Five.SkillCast());
                states.Add(Define.BattleEntityState.Die, new BattleEntityStates.Five.Die());
                states.Add(Define.BattleEntityState.EndBattle, new BattleEntityStates.Five.EndBattle());

                data = Managers.Data.GetBattleEntityData((int)_entity, _level);
                status = new BattleEntityStatus(data.maxHP, data.maxHP, data.attackForce, data.skillCooltime, data.skillCooltime, data.moveSpeed);
                battleEntity = new BattleEntites.Zero(battleEntityController);
                break;
        }

        if (battleEntityController != null)
        {
            battleEntityController.entityType = BattleEntityType.Army;
            battleEntityController.Init(battleEntity, states, status);
            Armys.Add(battleEntityController);
            return battleEntityController;        
        }

        Debug.Log("������ �����Ͽ����ϴ�.");
        return null;
    }
}
