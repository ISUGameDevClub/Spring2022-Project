using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject lightAttack;
    public GameObject strongAttack;
    public AudioClip lightSound;
    public AudioClip heavySound;
    public bool canAttack;
    Attack at;

    [SerializeField] Animator playerWeaponAnim;
    // Start is called before the first frame update
    void Start()
    {
        at = gameObject.GetComponent<Attack>();
        canAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canAttack)
        {
            if (Input.GetButton("Fire1") && at.canAttack)
            {
                at.ChangeClip(lightSound);
                at.hurtboxPrefab = lightAttack;
                at.SpawnAttack();
                playerWeaponAnim.SetTrigger("Light Attack");
            }
            else if (Input.GetButton("Fire2") && at.canAttack)
            {
                at.ChangeClip(heavySound);
                at.hurtboxPrefab = strongAttack;
                at.SpawnAttack();
                playerWeaponAnim.SetTrigger("Heavy Attack");

            }
        }
        Input.GetButtonDown()
    }
}
