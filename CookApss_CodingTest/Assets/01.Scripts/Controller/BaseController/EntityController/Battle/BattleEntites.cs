using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleEntity : IAttackable, IHittable
{
    protected BattleEntityController controller;
    //데이터는 여기에

    public abstract void Attack();
    public abstract void Hit();
    public abstract void GetDamage();
}

namespace BattleEntites
{
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
