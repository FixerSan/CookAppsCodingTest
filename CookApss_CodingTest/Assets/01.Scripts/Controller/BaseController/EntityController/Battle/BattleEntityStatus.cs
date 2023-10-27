using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEntityStatus 
{
    public int maxHP;
    public int currentHP;
    public int attackForce;
    public float skillCooltime;
    public float currentSkillCooltime;

    public BattleEntityStatus(int _maxHP, int _currentHP, int _attackForce,  float _skillCooltime, float _currentSkillCooltime)
    {
        maxHP = _maxHP;
        currentHP = _currentHP; 
        attackForce = _attackForce;
        skillCooltime = _skillCooltime;
        currentSkillCooltime = _currentSkillCooltime;
    }
}
