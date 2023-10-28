using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleEntity : IAttackable, IHittable
{
    protected BattleEntityController controller;
    public abstract void Attack();
    public abstract void Hit(int _damage);
    public abstract void GetDamage(int _damage);
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
