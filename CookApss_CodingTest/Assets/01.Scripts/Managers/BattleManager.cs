using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager
{
    public readonly float criticalMultiplier = 2f;
    public void AttackCalculation(BattleEntityController _attacker, BattleEntityController _hiter)
    {
        int tempInt = Random.Range(0, 101);
        int currentDamage = _attacker.status.attackForce;

        if(tempInt > 50)
            currentDamage = (int)(currentDamage * criticalMultiplier);

        _hiter.Hit(currentDamage);
    }
}
