using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{

    [SerializeField] float maxHealth = 10f; //maximum possible health of entity
    [Tooltip("Mark this if this prefab is a Player")]
    [SerializeField] bool isPlayer;
    private float currentHealth = 10f; //current health of entity
    private bool isDead = false;
    [SerializeField] AudioSource characterAudioSource;
    [SerializeField] AudioClip death;
    [SerializeField] AudioClip hurt;
    [SerializeField] Animator playerHurtEffect;
    [Tooltip("IF PLAYER, drag in Health Bar from Scene! Will create errors if left empty for player. Leave empty for everything else")]
    [SerializeField] GameObject healthbar;
    [SerializeField] Animator hurtAnim;
    [SerializeField] GameObject persistentSoundPrefab;
    private HealthBar bar;

    // How enemies tell the room they are in that it is cleared
    [HideInInspector]
    public Room myRoom;

    private void Start()
    {
        currentHealth = maxHealth;
        if (isPlayer)
            bar = healthbar.GetComponent<HealthBar>();
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (isPlayer)
        {
            bar.ChangeHealth((int)currentHealth);
            if (playerHurtEffect != null)
                playerHurtEffect.SetTrigger("Hurt");
            else
                Debug.Log("Player is missing their hurt effect!");
        }

        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            if (!isPlayer)
                EnemyDeath();
            else
                PlayerDeath();
        }
        else
        {
            ChangeSound(hurt);
            characterAudioSource.Play();
            hurtAnim.SetTrigger("Hurt");
        }

        gameObject.GetComponent<EnemyMovement>().KnockBack();

    }

    //use this whenever you need to check if an entity has died
    public bool IsDead()
    {
        return isDead;
    }

    private void EnemyDeath()
    {
        if(myRoom != null)
            myRoom.EnemyDied();

        GameObject tempDeathSound = Instantiate(persistentSoundPrefab,transform.position,Quaternion.identity,transform);
        tempDeathSound.GetComponent<AudioSource>().clip = death;
        tempDeathSound.GetComponent<AudioSource>().Play();
        tempDeathSound.transform.SetParent(null);

        if (characterAudioSource != null)
        {
            ChangeSound(death);
            characterAudioSource.Play();
        }
        isDead = true;

        Destroy(gameObject);
    }

    private void PlayerDeath()
    {
        hurtAnim.SetTrigger("Hurt");
        playerHurtEffect.SetTrigger("Die");
        StartCoroutine(ResetScene());
    }

    private IEnumerator ResetScene()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Title");
    }
    public void ChangeSound(AudioClip clip)
    {
        characterAudioSource.clip = clip;
    }
}
