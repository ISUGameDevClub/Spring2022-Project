using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public static GameObject lightAttack;
    public static GameObject strongAttack;
    public static AudioClip lightSound;
    public static AudioClip heavySound;
    public static Sprite weaponSprite;
    public SpriteRenderer weaponSpriteGameObject;
    public bool canAttack;
    Attack at;

    [SerializeField] Animator playerWeaponAnim;
    // Start is called before the first frame update
    void Start()
    {
        at = gameObject.GetComponent<Attack>();
        canAttack = true;
        weaponSpriteGameObject.sprite = weaponSprite;   
    }

    // Update is called once per frame
    void Update()
    {
        if (canAttack)
        {
            if (Input.GetButton("Fire1") && at.canAttack && lightAttack != null)
            {
                at.ChangeClip(lightSound);
                at.hurtboxPrefab = lightAttack;
                at.SpawnAttack();
                playerWeaponAnim.SetTrigger("Light Attack");
            }
            else if (Input.GetButton("Fire2") && at.canAttack && strongAttack != null)
            {
                at.ChangeClip(heavySound);
                at.hurtboxPrefab = strongAttack;
                at.SpawnAttack();
                playerWeaponAnim.SetTrigger("Heavy Attack");

            }
        }
    }
}
