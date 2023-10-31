using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleBuff 
{
    public BattleEntityStatus status;
    public bool isPlusAttackSpeed;
    public float currentPlusAttackSpeedTime;
    public float plusAttackSpeed;
    public bool isCanMiss;
    public int isCanMissCount;
    public float checkTime;

    public void CheckBuff()
    {
        CheckPlusAttackSpeed();
    }

    public void CheckPlusAttackSpeed()
    {
        if (isPlusAttackSpeed) return;

        currentPlusAttackSpeedTime -= Time.deltaTime;
        if (currentPlusAttackSpeedTime <= 0)
        {
            EndPlusAttackSpeed();
        }
    }

    public void StartPlusAttackSpeed(float _time, float _plusAttackSpeed)
    {
        isPlusAttackSpeed = true;
        currentPlusAttackSpeedTime = _time;
        plusAttackSpeed = _plusAttackSpeed;
        status.currentAttackCycle += plusAttackSpeed;
    }

    public void EndPlusAttackSpeed()
    {
        isPlusAttackSpeed = false;
        currentPlusAttackSpeedTime = 0;

        status.currentAttackCycle -= plusAttackSpeed;
        plusAttackSpeed = 0;
    }

    public BattleBuff(BattleEntityStatus _status)
    {
        status = _status;
        isPlusAttackSpeed = false;
        currentPlusAttackSpeedTime = 0;
        plusAttackSpeed = 0;
        isCanMiss = false; 
        isCanMissCount = 0;
        checkTime = 0; ;
    }
}
