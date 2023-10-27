using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class BattleEntityController : EntityController, IAttackable, IHittable
{
    public BattleEntity entity;
    public StateMachine<BattleEntityController> stateMachine;
    public BattleEntityStatus status;
    public Dictionary<BattleEntityState, State<BattleEntityController>> states;
    public BattleEntityType entityType;
    public BattleEntityState state = BattleEntityState.Idle;

    private bool init = false;
    private bool isDead;

    public void Init(BattleEntity _entity, Dictionary<BattleEntityState, State<BattleEntityController>> _states, BattleEntityStatus _status)
    {
        if (init) return;
        entity = _entity;
        states = _states;
        status = _status;
        stateMachine = new StateMachine<BattleEntityController>(this, states[BattleEntityState.Idle]);
        init = true;
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

    public void ChangeState(BattleEntityState _nextState , bool _isChangeSameState = false)
    {
        if (!init) return;
        if (state == _nextState)
        {
            if (_isChangeSameState)
                stateMachine.ChangeState(states[_nextState]);
            return;
        }
        state = _nextState;
        stateMachine.ChangeState(states[_nextState]);
    }

    public void Update()
    {
        if (!init) return;
        stateMachine.UpdateState();
    }
}


