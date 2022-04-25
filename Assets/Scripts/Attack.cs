using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject hurtboxPrefab; //prefab of the Object that will be spawned when the attack happens
    public GameObject attackSpawn; //source object from which the attack should spawn
    public float cooldownTime = 0.2f; //modifying this value in the inspector won't usually change anything, change the cooldown on the projectile prefab
    public bool canAttack = true;
    public bool isPlayer;
    [SerializeField] AudioSource AtkSnd;
    
    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Cooldown(float duration)
    {
        float der = duration;
        if (isPlayer)
            der = der * PassiveBuffs.attackSpeedIncrease;
        yield return new WaitForSeconds(der);
        canAttack = true;
    }

    public void SpawnAttack()
    {
        GameObject newHurtbox = Instantiate(hurtboxPrefab, attackSpawn.transform.position, attackSpawn.transform.rotation);
        newHurtbox.GetComponent<Hurtbox>().SetParent(gameObject);
        if (isPlayer)
        {
            newHurtbox.transform.localScale = new Vector3(PassiveBuffs.attackSizeIncrease, PassiveBuffs.attackSizeIncrease, 1);
        }
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
