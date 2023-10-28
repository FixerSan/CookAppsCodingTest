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
    public Rigidbody2D rb;
    public Transform target;

    private Transform hpBarTrans;
    private bool init = false;
    private bool isDead;

    public void Init(BattleEntity _entity, Dictionary<BattleEntityState, State<BattleEntityController>> _states, BattleEntityStatus _status)
    {
        if (init) return;
        entity = _entity;
        states = _states;
        status = _status;
        rb = gameObject.GetOrAddComponent<Rigidbody2D>();
        isDead = false;
        hpBarTrans = Util.FindChild<Transform>(gameObject, "HpBarTrans", true);
        UIHPBar hpBar =  Managers.Resource.Instantiate("UIHPbar", _pooling:true).GetOrAddComponent<UIHPBar>();

        hpBar.Init(this);
        stateMachine = new StateMachine<BattleEntityController>(this, states[BattleEntityState.Idle]);
        init = true;
    }

    public void Attack()
    {
        entity.Attack();
    }

    public void GetDamage(int _damage)
    {
        entity.GetDamage(_damage);
    }

    public void Hit(int _damage)
    {
        entity.Hit(_damage);
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

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    public void Update()
    {
        if (!init) return;
        stateMachine.UpdateState();
    }
}


