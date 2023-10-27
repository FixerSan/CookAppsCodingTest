using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleEntity : IAttackable, IHittable
{
    protected BattleEntityController controller;
    public abstract void Attack();
    public abstract void Hit();
    public abstract void GetDamage();
}

namespace BattleEntites
{
    public class Zero : BattleEntity
    {
        public override void Attack()
        {

        }

        public override void GetDamage()
        {

        }

        public override void Hit()
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

        public override void GetDamage()
        {

        }

        public override void Hit()
        {

        }

        public One(BattleEntityController _controller)
        {
            controller = _controller;
        }
    }
}
