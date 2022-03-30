using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] float maxHealth = 10f; //maximum possible health of entity
    [Tooltip("Mark this if this prefab is a Player")]
    [SerializeField] bool isPlayer;
    private float currentHealth = 10f; //current health of entity
    private bool isDead = false;
    [SerializeField] AudioSource dth;

    // How enemies tell the room they are in that it is cleared
    [HideInInspector]
    public Room myRoom;

    private void Start()
    {
        currentHealth = maxHealth;
    }
    private void Update()
    {
        while(currentHealth <= 0 && !isDead) //Ensures that this code only runs once and fully completes when the GameObject dies
        {
            isDead = true;
            Death();
        }
    }
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }
    //use this whenever you need to check if an entity has died
    public bool IsDead()
    {
        return isDead;
    }
    private void Death()
    {
        if (!isPlayer)
        {
            if (!isPlayer)
            {
                //myRoom.EnemyDied();
                Debug.Log(gameObject.name + " died!");
                isDead = true;
                dth.Play();
            }
            dth.Play();
            //Put Death functionality here!!!
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}
