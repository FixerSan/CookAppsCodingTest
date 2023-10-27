using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class BattleEntityController : EntityController, IAttackable, IHittable
{
    public BattleEntity entity;
    public BattleEntityStatus status;
    public StateMachine<BattleEntityController> stateMachine;
    public Dictionary<BattleEntityState, State<BattleEntityController>> states;

    public BattleEntityType entityType;

    private bool isInit = false;
    private bool isDead;

    public void Init(BattleEntity _entity, Dictionary<BattleEntityState, State<BattleEntityController>> _states)
    {
        if (isInit) return;
        entity = _entity;
        states = _states;
        stateMachine = new StateMachine<BattleEntityController>(this, states[BattleEntityState.Idle]);
        isDead = false;
    }

    public void Attack()
    {
        entity.Attack();
    }

    public void GetDamage()
    {
        entity.GetDamage();
    }

    public void Hit()
    {
        entity.Hit();
    }

    public void Die()
    {
        if (isDead) return;
        Managers.Resource.Destroy(gameObject);
    }

    public void Update()
    {
        
    }
}


