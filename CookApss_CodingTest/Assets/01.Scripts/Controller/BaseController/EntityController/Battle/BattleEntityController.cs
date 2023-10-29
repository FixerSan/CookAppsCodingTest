using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Rendering;
using static Define;

public class BattleEntityController : EntityController, IAttackable, IHittable
{
    public BattleEntity entity;
    public StateMachine<BattleEntityController> stateMachine;
    public BattleEntityStatus status;
    public Dictionary<BattleEntityState, State<BattleEntityController>> states;
    public BattleEntityType entityType;
    public BattleEntityState state;
    public Rigidbody2D rb;
    public Dictionary<string , Coroutine> routines;

    public Transform moveTarget;
    public BattleEntityController attackTarget;

    private bool isDead;
    private bool isAutoSkillCasting;
    private bool init = false;


    public void Init(BattleEntity _entity, Dictionary<BattleEntityState, State<BattleEntityController>> _states, BattleEntityStatus _status, BattleEntityType _entityType)
    {
        if (init) return;
        entity = _entity;
        states = _states;
        status = _status;
        entityType = _entityType;
        state = BattleEntityState.Idle;
        rb = gameObject.GetOrAddComponent<Rigidbody2D>();
        routines = new Dictionary<string, Coroutine>();
        stateMachine = new StateMachine<BattleEntityController>(this, states[BattleEntityState.Idle]);
        UIHPBar hpBar =  Managers.Resource.Instantiate("UIHPbar", _pooling:true).GetOrAddComponent<UIHPBar>();
        hpBar.Init(this);

        isDead = false;
        if (entityType == BattleEntityType.Enemy)
            isAutoSkillCasting = true;
        else
        { //여기에서 현재 플레이어 상태에 따라 정의
        }
          //여기에서 현재 플레이어 상태에 따라 정의
        init = true;
    }

    public void Attack()
    {
        entity.Attack();
    }

    public void GetDamage(int _damage)
    {
        if (isDead) return;
        entity.GetDamage(_damage);
    }

    public void Hit(int _damage)
    {
        if (isDead) return;
        entity.Hit(_damage);
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;
        StopAllRoutine();
    }

    public void StopAllRoutine()
    {
        foreach (var routine in routines)
            StopCoroutine(routine.Value);
    }

    public void Stop()
    {
        rb.velocity = Vector3.zero;
    }

    public void FindTarget()
    {
        BattleEntityController tempTrans = null;
        float minDistace = 10000000;
        float currentDistance = 0;
        HashSet<BattleEntityController> tempHashSet = null; ;

        if (entityType == BattleEntityType.Army) tempHashSet = Managers.Object.Enemys;
        else tempHashSet = Managers.Object.Armys;
        foreach (var item in tempHashSet)
        {
            if(item.isDead) continue;
            currentDistance = Vector2.Distance(transform.position, item.transform.position);
            if (currentDistance < minDistace)
            {
                minDistace = currentDistance;
                tempTrans = item;
            }
        }

        attackTarget = tempTrans;
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
        moveTarget = _target;
    }

    public void Update()
    {
        if (!init) return;
        stateMachine.UpdateState();
    }
}


