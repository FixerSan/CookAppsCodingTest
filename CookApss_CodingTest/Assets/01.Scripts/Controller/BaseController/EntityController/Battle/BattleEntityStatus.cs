using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleEntityStatus 
{
    public int maxHP;
    private int currentHP;
    public int CurrentHP 
    {
        get { return currentHP; }
        set 
        { 
            currentHP = value;
            Managers.Event.OnVoidEvent?.Invoke(Define.VoidEventType.OnChangeControllerStatus);
        }
    }
    public int attackForce;
    public float skillCooltime;
    public float currentSkillCooltime;
    public int moveSpeed;

    public BattleEntityStatus(int _maxHP, int _currentHP, int _attackForce,  float _skillCooltime, float _currentSkillCooltime, int _moveSpeed)
    {
        maxHP = _maxHP;
        currentHP = _currentHP; 
        attackForce = _attackForce;
        skillCooltime = _skillCooltime;
        currentSkillCooltime = _currentSkillCooltime;
        moveSpeed = _moveSpeed;
    }
}
