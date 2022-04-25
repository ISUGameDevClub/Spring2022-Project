using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupPickup : MonoBehaviour
{
    public AudioSource powerupSound;
    public float speedIncrease = 0;
    public float dashSpeedIncrease = 0;
    public float dashTimeIncrease = 0;
    public float dashCooldownDecrease = 0;
    public float attackSpeedIncrease = 0;
    public float attackSizeIncrease = 0;
    public float attackKnockbackIncrease = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            powerupSound.Play();
            PassiveBuffs.speedIncrease += speedIncrease;
            PassiveBuffs.dashSpeedIncrease += dashSpeedIncrease;
            PassiveBuffs.dashTimeIncrease += dashTimeIncrease;
            PassiveBuffs.dashCooldownDecrease += dashCooldownDecrease;
            PassiveBuffs.attackSpeedIncrease += attackSpeedIncrease;
            PassiveBuffs.attackSizeIncrease += attackSizeIncrease;
            PassiveBuffs.attackKnockbackIncrease += attackKnockbackIncrease;
            other.gameObject.GetComponent<PlayerMovement>().UpdatePassives();
            Destroy(gameObject);
        }
    }
}
