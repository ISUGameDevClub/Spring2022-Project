using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject hurtboxPrefab; //prefab of the Object that will be spawned when the attack happens
    public GameObject attackSpawn; //source object from which the attack should spawn
    public float cooldownTime = 0.2f; //modifying this value in the inspector won't usually change anything, change the cooldown on the projectile prefab
    public bool canAttack = true;
    [SerializeField] AudioSource AtkSnd;
    
    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Cooldown(float duration)
    {
        yield return new WaitForSeconds(duration);
        canAttack = true;
    }

    public void SpawnAttack()
    {
        GameObject newHurtbox = Instantiate(hurtboxPrefab, attackSpawn.transform.position, attackSpawn.transform.rotation);
        newHurtbox.GetComponent<Hurtbox>().SetParent(gameObject);
        if (AtkSnd != null)
        {
            Debug.Log(AtkSnd);
            AtkSnd.Play();
        }
        canAttack = false;
        StartCoroutine(Cooldown(newHurtbox.GetComponent<ProjectileBehavior>().cooldownTime));
    }
    public void AttackSound(bool light)
    {
        if (light)
        {
            AtkSnd.pitch = 1;
        } else if (!light){
            AtkSnd.pitch = -1;
        }
    }
    public void ChangeClip(AudioClip newClip)
    {
        AtkSnd.clip = newClip;
    }
}
