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
    private bool init = false;


    public float currentSkillCooltime;
    public int currentAttackForce;
    public float currentAttackCycle;
    public int mvpPoint;

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

        currentSkillCooltime = _entity.data.skillCooltime;
        currentAttackForce = _entity.data.attackForce;
        currentAttackCycle = 0;
        mvpPoint = 0;

        UIHPBar hpBar =  Managers.Resource.Instantiate("UIHPbar", _pooling:true).GetOrAddComponent<UIHPBar>();

        hpBar.Init(this);

        isDead = false;
        init = true;
    }

    public void Attack()
    {
        if (isDead) return;
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
    public void Skill()
    {
        if (isDead) return;
        entity.Skill();
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
        routines.Clear();
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
        List<BattleEntityController> tempHashSet = null; ;

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
    public void SetTarget(Transform _target)
    {
        moveTarget = _target;
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

    public void ChangeStateWithDelay(BattleEntityState _nextState, float _delay,bool _isChangeSameState = false)
    {
        if (!init) return;
        routines.Add("ChangeStateWithDelay", StartCoroutine(ChangeStateWithDelayRoutine(_nextState, _delay, _isChangeSameState)));
    }

    private IEnumerator ChangeStateWithDelayRoutine(BattleEntityState _nextState, float _delay, bool _isChangeSameState = false)
    {
        yield return new WaitForSeconds(_delay);
        ChangeState(_nextState, _isChangeSameState);
        routines.Remove("ChangeStateWithDelay");
    }

    public void Update()
    {
        if (!init) return;
        stateMachine.UpdateState();
    }

    public void OnDisable()
    {
        StopAllRoutine();
    }
}


