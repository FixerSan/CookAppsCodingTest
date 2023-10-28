using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleEntity : IAttackable, IHittable
{
    protected BattleEntityController controller;
    public abstract void Attack();
    public abstract void Hit(int _damage);
    public abstract void GetDamage(int _damage);
    public virtual void Move()
    {
        if(controller.target != null)
        {
            Vector2 dir = (controller.target.position - controller.transform.position).normalized;
            controller.rb.velocity =  250 * dir * controller.status.moveSpeed * Time.deltaTime;
        }
    }
    public virtual bool CheckStop()
    {
        if (Vector2.Distance(controller.target.position, controller.transform.position) < 0.1f)
        {
            controller.ChangeState(Define.BattleEntityState.Idle);
            return true;
        }
        return false;
    }
}

namespace BattleEntites
{
    public class Zero : BattleEntity
    {
        public override void Attack()
        {

        }

        public override void GetDamage(int _damage)
        {

        }

        public override void Hit(int _damage)
        {

        }

 

        public Zero(BattleEntityController _controller)
        {
            controller = _controller;
        }
    }

    public class One : BattleEntity
    {
        public override void Attack()
        {

        }

        public override void GetDamage(int _damage)
        {

        }

        public override void Hit(int _damage)
        {

        }

        public One(BattleEntityController _controller)
        {
            controller = _controller;
        }
    }
}
