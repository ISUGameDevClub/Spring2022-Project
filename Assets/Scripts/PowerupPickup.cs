using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupPickup : MonoBehaviour
{
    public float speedIncrease = 0;
    public float dashSpeedIncrease = 0;
    public float dashTimeIncrease = 0;
    public float dashCooldownDecrease = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PassiveBuffs.speedIncrease += speedIncrease;
            PassiveBuffs.dashSpeedIncrease += dashSpeedIncrease;
            PassiveBuffs.dashTimeIncrease += dashTimeIncrease;
            PassiveBuffs.dashCooldownDecrease += dashCooldownDecrease;
            other.gameObject.GetComponent<PlayerMovement>().UpdatePassives();
            Destroy(gameObject);
        }
    }
}
