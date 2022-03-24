using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float currentHealth = 10f; //current health of entity
    public float maxHealth = 10f; //maximum possible health of entity
    public bool isDead = false; //use this whenever you need to check if an entity has died

    // How enemies tell the room they are in that it is cleared
    [HideInInspector]
    public Room myRoom;


    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    public void Death()
    {
        myRoom.EnemyDied();
        Debug.Log(gameObject.name + " died!");
        isDead = true;
        //Put Death functionality here!!!
        Destroy(gameObject);
    }
}
