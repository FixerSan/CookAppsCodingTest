using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class BattleEntity : IAttackable, IHittable
{
    protected BattleEntityController controller;
    public BattleEntityData data;

    public virtual bool CheckAttack()
    {
        if (controller.status.currentAttackCycle > 0)
        {
            controller.status.currentAttackCycle -= Time.deltaTime;
            if (controller.status.currentAttackCycle <= 0)
                controller.status.currentAttackCycle = 0;
        }
        if (controller.attackTarget == null) return false;
        if (controller.status.currentAttackCycle > 0) return false;
        if (controller.state != Define.BattleEntityState.Follow) return false;
        if (Vector2.Distance(controller.attackTarget.transform.position, controller.transform.position) < data.canAttackDistance)
        {
            controller.ChangeState(Define.BattleEntityState.Attack);
            return true;
        }
        return false;
    }

    public virtual void Attack()
    {
        controller.routines.Add("attack", controller.StartCoroutine(AttackRoutine()));
    }

    public virtual IEnumerator AttackRoutine()
    {
        controller.status.currentAttackCycle = data.attackCycle;
        Managers.Battle.AttackCalculation(controller, controller.attackTarget, (_damage)=> { controller.mvpPoint += _damage; });
        Managers.Game.battleInfo.UpdateMVPPoints();
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
        Vector2 damageTextPos = Camera.main.WorldToScreenPoint(controller.transform.position) + (Vector3)controller.damageTextOffset;
        Managers.Resource.Instantiate("UIPopup_DamageText").GetOrAddComponent<UIPopup_DamageText>().Init(_damage, damageTextPos);
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
            if (Vector2.Distance(controller.attackTarget.transform.position, controller.transform.position) < data.canAttackDistance)
            {
                controller.Stop();
                return;
            }
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


    public virtual bool CheckCanUseSkill()
    {
        if(controller.status.currentSkillCooltime > 0)
        {
            controller.status.currentSkillCooltime -= Time.deltaTime;
            if (controller.status.currentSkillCooltime <= 0)
                controller.status.currentSkillCooltime = 0;
        }
        if (Managers.Screen.isSkillCasting) return false;
        if (controller.entityType == Define.BattleEntityType.Enemy) 
        {
            if (controller.state != Define.BattleEntityState.Follow) return false;
            if (controller.status.currentSkillCooltime <= 0)
            {
                controller.ChangeState(Define.BattleEntityState.SkillCast);
                return true;
            }
            else return false;
        }

        if (!Managers.Game.battleInfo.isAutoSkill) return false;
        if (controller.state != Define.BattleEntityState.Follow) return false;
        if (controller.status.currentSkillCooltime <= 0)
        {
            controller.ChangeState(Define.BattleEntityState.SkillCast);
            return true;
        }
        else return false;
    }

    public abstract void Skill();
    public void BaseSkill()
    {
        controller.StopAllRoutine();
        Managers.Screen.SkillScreen();
        controller.ChangeStateWithDelay(Define.BattleEntityState.Follow, 2);
        controller.status.currentSkillCooltime = data.skillCooltime;
    }
}

namespace BattleEntites
{
    public class Warrior : BattleEntity
    {
        public override void Skill()
        {
            BaseSkill();
            for (int i = 0; i < Managers.Object.Armys.Count; i++)
            {
                Managers.Object.Armys[i].SetBuff_PlusSpeed(3f,Managers.Object.Armys[i].status.currentAttackCycle * 0.25f);
            }
        }

        public Warrior(BattleEntityController _controller, BattleEntityData _data)
        {
            controller = _controller;
            data = _data;
        }
    }

    public class Tank : BattleEntity
    {
        public override void Skill()
        {
            BaseSkill();
        }

        public Tank(BattleEntityController _controller, BattleEntityData _data)
        {
            controller = _controller;
            data = _data;
        }

    }

    public class Wizard : BattleEntity
    {
        public override void Skill()
        {
            BaseSkill();
        }

        public Wizard(BattleEntityController _controller, BattleEntityData _data)
        {
            controller = _controller;
            data = _data;
        }
    }

    public class Three : BattleEntity
    {

        public override void Skill()
        {
            BaseSkill();
            Managers.Battle.AttackCalculation(controller.status.attackForce * 3, controller.attackTarget, (_damage) => { controller.mvpPoint += _damage; });
        }

        public Three(BattleEntityController _controller, BattleEntityData _data)
        {
            controller = _controller;
            data = _data;
        }
    }

    public class Four : BattleEntity
    {

        public override void Skill()
        {
            BaseSkill();
            Managers.Battle.AttackCalculation(controller.status.attackForce * 3, controller.attackTarget, (_damage) => { controller.mvpPoint += _damage; });
        }

        public Four (BattleEntityController _controller, BattleEntityData _data)
        {
            controller = _controller;
            data = _data;
        }
    }

    public class Five : BattleEntity
    {

        public override void Skill()
        {
            BaseSkill();
            Managers.Battle.AttackCalculation(controller.status.attackForce * 3, controller.attackTarget, (_damage) => { controller.mvpPoint += _damage; });
        }

        public Five(BattleEntityController _controller, BattleEntityData _data)
        {
            controller = _controller;
            data = _data;
        }
    }
}
