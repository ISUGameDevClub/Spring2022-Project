using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private GameObject lightAttack;
    [SerializeField] private AudioClip lightAttackSound;
    [SerializeField] private GameObject heavyAttack;
    [SerializeField] private AudioClip heavyAttackSound;
    [SerializeField] private Sprite weaponSprite;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        PickupWeapon();
    }

    private void PickupWeapon()
    {
        PlayerAttack.lightAttack = lightAttack;
        PlayerAttack.lightSound = lightAttackSound;
        PlayerAttack.strongAttack = heavyAttack;
        PlayerAttack.heavySound = heavyAttackSound;
        PlayerAttack.weaponSprite = weaponSprite;
        FindObjectOfType<PlayerAttack>().weaponSpriteGameObject.sprite = PlayerAttack.weaponSprite;
        foreach(GameObject w in FindObjectOfType<IntroManager>().weapons)
        {
            if (w != gameObject)
                w.SetActive(true);
            else
                w.SetActive(false);
        }
        if (FindObjectOfType<IntroManager>().phase == 1)
        {
            FindObjectOfType<IntroManager>().phase = 2;
            FindObjectOfType<IntroManager>().ItemText.GetComponent<Animator>().SetTrigger("Hide");
            FindObjectOfType<IntroManager>().battle1Text.GetComponent<Animator>().SetTrigger("Show");
        }
    }
}
