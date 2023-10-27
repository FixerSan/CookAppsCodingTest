using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEntityStatus 
{
    public int maxHP;
    public int currentHP;
    public float skillCooltime;
    public float currentSkillCooltime;

    public BattleEntityStatus(int _maxHP, int _currentHP, float _skillCooltime, float _cerrentSkillCooltime)
    {
        maxHP = _maxHP;
        currentHP = _currentHP; 
        skillCooltime = _skillCooltime;
        currentSkillCooltime = _cerrentSkillCooltime;
    }
}
