using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject hurtboxPrefab; //prefab of the Object that will be spawned when the attack happens
    public GameObject attackSpawn; //source object from which the attack should spawn
    public float cooldownTime = 0.2f;
    bool canAttack = true;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire1") && canAttack)
        {
            GameObject newHurtbox = Instantiate(hurtboxPrefab, attackSpawn.transform.position, attackSpawn.transform.rotation);
            newHurtbox.GetComponent<Hurtbox>().parent = gameObject;
            canAttack = false;
            StartCoroutine(Cooldown(cooldownTime));
        }
    }

    IEnumerator Cooldown(float duration)
    {
        yield return new WaitForSeconds(duration);
        canAttack = true;
    }
}
