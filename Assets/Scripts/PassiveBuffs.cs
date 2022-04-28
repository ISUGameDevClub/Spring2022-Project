using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveBuffs : MonoBehaviour
{
    // Add a static variable here for player buffs, then add them to the base stat (if applicable)
    static public float speedIncrease = 0;
    static public float dashSpeedIncrease = 0;
    static public float dashTimeIncrease = 0;
    static public float dashCooldownDecrease = 0;
    static public float attackSpeedIncrease = 1;
    static public float attackSizeIncrease = 1;
    static public float attackKnockbackIncrease = 1;

    static public void ResetStats()
    {
        speedIncrease = 0;
        dashSpeedIncrease = 0;
        dashTimeIncrease = 0;
        dashCooldownDecrease = 0;
        attackSpeedIncrease = 1;
        attackSizeIncrease = 1;
        attackKnockbackIncrease = 1;
        PlayerAttack.weaponSprite = null;
        PlayerAttack.lightAttack = null;
        PlayerAttack.strongAttack = null;
    }
}
