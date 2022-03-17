using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float currentHealth = 10f; //current health of entity
    public float maxHealth = 10f; //maximum possible health of entity
    public bool isDead = false; //use this whenever you need to check if an entity has died


    private void Update()
    {
        if(currentHealth <= 0 && !isDead) //Ensures that this code only runs once when the GameObject dies
        {
            Debug.Log(gameObject.name + " died!");
            isDead = true;
            //Put Death functionality here!!!
        }
    }
}
