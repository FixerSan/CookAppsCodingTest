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
    public List<BattleEntityController> Enemys { get; } = new List<BattleEntityController>();

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
    public List<BattleEntityController> Armys { get; } = new List<BattleEntityController>();

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
                states.Add(Define.BattleEntityState.Attack, new BattleEntityStates.Base.Attack());
                states.Add(Define.BattleEntityState.SkillCast, new BattleEntityStates.Base.SkillCast());
                states.Add(Define.BattleEntityState.Die, new BattleEntityStates.Base.Die());
                states.Add(Define.BattleEntityState.EndBattle, new BattleEntityStates.Base.EndBattle());

                data = Managers.Data.GetBattleEntityData((int)_entity, _level);
                status = new BattleEntityStatus(data.maxHP, data.maxHP, data.attackForce, data.skillCooltime, data.skillCooltime, data.moveSpeed);
                battleEntity = new BattleEntites.Zero(battleEntityController, data);
                break;

            case Define.BattleEntity.One:
                states.Add(Define.BattleEntityState.Idle, new BattleEntityStates.Base.Idle());
                states.Add(Define.BattleEntityState.Move, new BattleEntityStates.Base.Move());
                states.Add(Define.BattleEntityState.Follow, new BattleEntityStates.Base.Follow());
                states.Add(Define.BattleEntityState.Attack, new BattleEntityStates.Base.Attack());
                states.Add(Define.BattleEntityState.SkillCast, new BattleEntityStates.Base.SkillCast());
                states.Add(Define.BattleEntityState.Die, new BattleEntityStates.Base.Die());
                states.Add(Define.BattleEntityState.EndBattle, new BattleEntityStates.Base.EndBattle());

                data = Managers.Data.GetBattleEntityData((int)_entity, _level);
                status = new BattleEntityStatus(data.maxHP, data.maxHP, data.attackForce, data.skillCooltime, data.skillCooltime, data.moveSpeed);
                battleEntity = new BattleEntites.One(battleEntityController, data);
                break;

            case Define.BattleEntity.Two:
                states.Add(Define.BattleEntityState.Idle, new BattleEntityStates.Base.Idle());
                states.Add(Define.BattleEntityState.Move, new BattleEntityStates.Base.Move());
                states.Add(Define.BattleEntityState.Follow, new BattleEntityStates.Base.Follow());
                states.Add(Define.BattleEntityState.Attack, new BattleEntityStates.Base.Attack());
                states.Add(Define.BattleEntityState.SkillCast, new BattleEntityStates.Base.SkillCast());
                states.Add(Define.BattleEntityState.Die, new BattleEntityStates.Base.Die());
                states.Add(Define.BattleEntityState.EndBattle, new BattleEntityStates.Base.EndBattle());

                data = Managers.Data.GetBattleEntityData((int)_entity, _level);
                status = new BattleEntityStatus(data.maxHP, data.maxHP, data.attackForce, data.skillCooltime, data.skillCooltime, data.moveSpeed);
                battleEntity = new BattleEntites.Two(battleEntityController, data);
                break;

            case Define.BattleEntity.Three:
                states.Add(Define.BattleEntityState.Idle, new BattleEntityStates.Base.Idle());
                states.Add(Define.BattleEntityState.Move, new BattleEntityStates.Base.Move());
                states.Add(Define.BattleEntityState.Follow, new BattleEntityStates.Base.Follow());
                states.Add(Define.BattleEntityState.Attack, new BattleEntityStates.Base.Attack());
                states.Add(Define.BattleEntityState.SkillCast, new BattleEntityStates.Base.SkillCast());
                states.Add(Define.BattleEntityState.Die, new BattleEntityStates.Base.Die());
                states.Add(Define.BattleEntityState.EndBattle, new BattleEntityStates.Base.EndBattle());

                data = Managers.Data.GetBattleEntityData((int)_entity, _level);
                status = new BattleEntityStatus(data.maxHP, data.maxHP, data.attackForce, data.skillCooltime, data.skillCooltime, data.moveSpeed);
                battleEntity = new BattleEntites.Three(battleEntityController, data);
                break;

            case Define.BattleEntity.Four:
                states.Add(Define.BattleEntityState.Idle, new BattleEntityStates.Base.Idle());
                states.Add(Define.BattleEntityState.Move, new BattleEntityStates.Base.Move());
                states.Add(Define.BattleEntityState.Follow, new BattleEntityStates.Base.Follow());
                states.Add(Define.BattleEntityState.Attack, new BattleEntityStates.Base.Attack());
                states.Add(Define.BattleEntityState.SkillCast, new BattleEntityStates.Base.SkillCast());
                states.Add(Define.BattleEntityState.Die, new BattleEntityStates.Base.Die());
                states.Add(Define.BattleEntityState.EndBattle, new BattleEntityStates.Base.EndBattle());

                data = Managers.Data.GetBattleEntityData((int)_entity, _level);
                status = new BattleEntityStatus(data.maxHP, data.maxHP, data.attackForce, data.skillCooltime, data.skillCooltime, data.moveSpeed);
                battleEntity = new BattleEntites.Four(battleEntityController, data);
                break;
            case Define.BattleEntity.Five:
                states.Add(Define.BattleEntityState.Idle, new BattleEntityStates.Base.Idle());
                states.Add(Define.BattleEntityState.Move, new BattleEntityStates.Base.Move());
                states.Add(Define.BattleEntityState.Follow, new BattleEntityStates.Base.Follow());
                states.Add(Define.BattleEntityState.Attack, new BattleEntityStates.Base.Attack());
                states.Add(Define.BattleEntityState.SkillCast, new BattleEntityStates.Base.SkillCast());
                states.Add(Define.BattleEntityState.Die, new BattleEntityStates.Base.Die());
                states.Add(Define.BattleEntityState.EndBattle, new BattleEntityStates.Base.EndBattle());

                data = Managers.Data.GetBattleEntityData((int)_entity, _level);
                status = new BattleEntityStatus(data.maxHP, data.maxHP, data.attackForce, data.skillCooltime, data.skillCooltime, data.moveSpeed);
                battleEntity = new BattleEntites.Five(battleEntityController, data);
                break;
        }

        if (battleEntityController != null)
        {
            battleEntityController.Init(battleEntity, states, status, BattleEntityType.Army);
            Armys.Add(battleEntityController);
            return battleEntityController;        
        }

        Debug.Log("스폰을 실패하였습니다.");
        return null;
    }

    public BattleEntityController SpawnEnemyBattleEntity(Define.BattleEntity _entity, int _level, Vector2 _position = new Vector2())
    {
        BattleEntityController battleEntityController = Managers.Resource.Instantiate($"{_entity.ToString()}", _parent: EnemyBattleEntityTrnas).GetOrAddComponent<BattleEntityController>();
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
                states.Add(Define.BattleEntityState.Attack, new BattleEntityStates.Base.Attack());
                states.Add(Define.BattleEntityState.SkillCast, new BattleEntityStates.Base.SkillCast());
                states.Add(Define.BattleEntityState.Die, new BattleEntityStates.Base.Die());
                states.Add(Define.BattleEntityState.EndBattle, new BattleEntityStates.Base.EndBattle());

                data = Managers.Data.GetBattleEntityData((int)_entity, _level);
                status = new BattleEntityStatus(data.maxHP, data.maxHP, data.attackForce, data.skillCooltime, data.skillCooltime, data.moveSpeed);
                battleEntity = new BattleEntites.Zero(battleEntityController, data);
                break;

            case Define.BattleEntity.One:
                states.Add(Define.BattleEntityState.Idle, new BattleEntityStates.Base.Idle());
                states.Add(Define.BattleEntityState.Move, new BattleEntityStates.Base.Move());
                states.Add(Define.BattleEntityState.Follow, new BattleEntityStates.Base.Follow());
                states.Add(Define.BattleEntityState.Attack, new BattleEntityStates.Base.Attack());
                states.Add(Define.BattleEntityState.SkillCast, new BattleEntityStates.Base.SkillCast());
                states.Add(Define.BattleEntityState.Die, new BattleEntityStates.Base.Die());
                states.Add(Define.BattleEntityState.EndBattle, new BattleEntityStates.Base.EndBattle());

                data = Managers.Data.GetBattleEntityData((int)_entity, _level);
                status = new BattleEntityStatus(data.maxHP, data.maxHP, data.attackForce, data.skillCooltime, data.skillCooltime, data.moveSpeed);
                battleEntity = new BattleEntites.One(battleEntityController, data);
                break;

            case Define.BattleEntity.Two:
                states.Add(Define.BattleEntityState.Idle, new BattleEntityStates.Base.Idle());
                states.Add(Define.BattleEntityState.Move, new BattleEntityStates.Base.Move());
                states.Add(Define.BattleEntityState.Follow, new BattleEntityStates.Base.Follow());
                states.Add(Define.BattleEntityState.Attack, new BattleEntityStates.Base.Attack());
                states.Add(Define.BattleEntityState.SkillCast, new BattleEntityStates.Base.SkillCast());
                states.Add(Define.BattleEntityState.Die, new BattleEntityStates.Base.Die());
                states.Add(Define.BattleEntityState.EndBattle, new BattleEntityStates.Base.EndBattle());

                data = Managers.Data.GetBattleEntityData((int)_entity, _level);
                status = new BattleEntityStatus(data.maxHP, data.maxHP, data.attackForce, data.skillCooltime, data.skillCooltime, data.moveSpeed);
                battleEntity = new BattleEntites.Two(battleEntityController, data);
                break;

            case Define.BattleEntity.Three:
                states.Add(Define.BattleEntityState.Idle, new BattleEntityStates.Base.Idle());
                states.Add(Define.BattleEntityState.Move, new BattleEntityStates.Base.Move());
                states.Add(Define.BattleEntityState.Follow, new BattleEntityStates.Base.Follow());
                states.Add(Define.BattleEntityState.Attack, new BattleEntityStates.Base.Attack());
                states.Add(Define.BattleEntityState.SkillCast, new BattleEntityStates.Base.SkillCast());
                states.Add(Define.BattleEntityState.Die, new BattleEntityStates.Base.Die());
                states.Add(Define.BattleEntityState.EndBattle, new BattleEntityStates.Base.EndBattle());

                data = Managers.Data.GetBattleEntityData((int)_entity, _level);
                status = new BattleEntityStatus(data.maxHP, data.maxHP, data.attackForce, data.skillCooltime, data.skillCooltime, data.moveSpeed);
                battleEntity = new BattleEntites.Three(battleEntityController, data);
                break;

            case Define.BattleEntity.Four:
                states.Add(Define.BattleEntityState.Idle, new BattleEntityStates.Base.Idle());
                states.Add(Define.BattleEntityState.Move, new BattleEntityStates.Base.Move());
                states.Add(Define.BattleEntityState.Follow, new BattleEntityStates.Base.Follow());
                states.Add(Define.BattleEntityState.Attack, new BattleEntityStates.Base.Attack());
                states.Add(Define.BattleEntityState.SkillCast, new BattleEntityStates.Base.SkillCast());
                states.Add(Define.BattleEntityState.Die, new BattleEntityStates.Base.Die());
                states.Add(Define.BattleEntityState.EndBattle, new BattleEntityStates.Base.EndBattle());

                data = Managers.Data.GetBattleEntityData((int)_entity, _level);
                status = new BattleEntityStatus(data.maxHP, data.maxHP, data.attackForce, data.skillCooltime, data.skillCooltime, data.moveSpeed);
                battleEntity = new BattleEntites.Four(battleEntityController, data);
                break;
            case Define.BattleEntity.Five:
                states.Add(Define.BattleEntityState.Idle, new BattleEntityStates.Base.Idle());
                states.Add(Define.BattleEntityState.Move, new BattleEntityStates.Base.Move());
                states.Add(Define.BattleEntityState.Follow, new BattleEntityStates.Base.Follow());
                states.Add(Define.BattleEntityState.Attack, new BattleEntityStates.Base.Attack());
                states.Add(Define.BattleEntityState.SkillCast, new BattleEntityStates.Base.SkillCast());
                states.Add(Define.BattleEntityState.Die, new BattleEntityStates.Base.Die());
                states.Add(Define.BattleEntityState.EndBattle, new BattleEntityStates.Base.EndBattle());

                data = Managers.Data.GetBattleEntityData((int)_entity, _level);
                status = new BattleEntityStatus(data.maxHP, data.maxHP, data.attackForce, data.skillCooltime, data.skillCooltime, data.moveSpeed);
                battleEntity = new BattleEntites.Five(battleEntityController, data);
                break;
        }

        if (battleEntityController != null)
        {
            battleEntityController.Init(battleEntity, states, status, BattleEntityType.Enemy);
            Enemys.Add(battleEntityController);
            return battleEntityController;
        }

        Debug.Log("스폰을 실패하였습니다.");
        return null;
    }

}
