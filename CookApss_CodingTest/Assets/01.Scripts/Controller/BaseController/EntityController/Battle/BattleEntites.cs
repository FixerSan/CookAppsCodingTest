using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class BattleEntity : IAttackable, IHittable
{
    protected BattleEntityController controller;
    public BattleEntityData data;
    public virtual void Attack()
    {
        controller.routines.Add("attack", controller.StartCoroutine(AttackRoutine()));
    }

    public virtual IEnumerator AttackRoutine()
    {
        Managers.Battle.AttackCalculation(controller, controller.attackTarget);
        yield return new WaitForSeconds(1);
        controller.routines.Remove("attack");
        controller.ChangeState(Define.BattleEntityState.Follow);
    }

    public virtual void Hit(int _damage)
    {
        controller.GetDamage(_damage);
    }
    public virtual void GetDamage(int _damage)
    {
        controller.status.CurrentHP -= _damage;
        if(controller.status.CurrentHP <= 0)
        {
            controller.status.CurrentHP = 0;
            controller.ChangeState(Define.BattleEntityState.Die);
        }
    }

    public virtual void Move()
    {
        if(controller.moveTarget != null)
        {
            Vector2 dir = (controller.moveTarget.position - controller.transform.position).normalized;
            controller.rb.velocity =  250 * dir * controller.status.moveSpeed * Time.deltaTime;
        }
    }

    public virtual void Follow()
    {
        if (controller.attackTarget != null)
        {
            Vector2 dir = (controller.attackTarget.transform.position - controller.transform.position).normalized;
            controller.rb.velocity = 250 * dir * controller.status.moveSpeed * Time.deltaTime;
        }
    }

    public virtual bool CheckStop()
    {
        if (Vector2.Distance(controller.moveTarget.position, controller.transform.position) < 0.1f)
        {
            controller.ChangeState(Define.BattleEntityState.Idle);
            return true;
        }
        return false;
    }

    public virtual bool CheckAttack()
    {
        if (controller.attackTarget == null) return false;
        if (Vector2.Distance(controller.attackTarget.transform.position, controller.transform.position) < data.canAttackDistance)
        {
            controller.ChangeState(Define.BattleEntityState.Attack);
            return true;
        }
        return false;
    }

}

namespace BattleEntites
{
    public class Zero : BattleEntity
    {
        public Zero(BattleEntityController _controller, BattleEntityData _data)
        {
            controller = _controller;
            data = _data;
        }
    }

    public class One : BattleEntity
    {
        public One(BattleEntityController _controller, BattleEntityData _data)
        {
            controller = _controller;
            data = _data;
        }
    }

    public class Two : BattleEntity
    {
        public Two(BattleEntityController _controller, BattleEntityData _data)
        {
            controller = _controller;
            data = _data;
        }
    }

    public class Three : BattleEntity
    {
        public Three(BattleEntityController _controller, BattleEntityData _data)
        {
            controller = _controller;
            data = _data;
        }
    }

    public class Four : BattleEntity
    {
        public Four (BattleEntityController _controller, BattleEntityData _data)
        {
            controller = _controller;
            data = _data;
        }
    }

    public class Five : BattleEntity
    {
        public Five(BattleEntityController _controller, BattleEntityData _data)
        {
            controller = _controller;
            data = _data;
        }
    }
}
