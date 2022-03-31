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
    [SerializeField] AudioSource deathSound;
    [SerializeField] Animator playerHurtEffect;

    // How enemies tell the room they are in that it is cleared
    [HideInInspector]
    public Room myRoom;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0 && !isDead)
        {
            isDead = true;
            Death();
        }
        else if(isPlayer)
        {
            if (playerHurtEffect != null)
                playerHurtEffect.SetTrigger("Hurt");
            else
                Debug.Log("Player is missing their hurt effect!");
        }
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

                myRoom.EnemyDied();
                Debug.Log(gameObject.name + " died!");
                isDead = true;
        }

        if(deathSound != null)
            deathSound.Play();

        Destroy(gameObject);
    }
}
