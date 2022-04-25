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

    bool touchingWepaon;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && touchingWepaon)
        {
            Debug.Log("FOUND");
            PickupWeapon();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            touchingWepaon = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            touchingWepaon = false;
        }
    }

    private void PickupWeapon()
    {
        PlayerAttack.lightAttack = lightAttack;
        PlayerAttack.lightSound = lightAttackSound;
        PlayerAttack.strongAttack = heavyAttack;
        PlayerAttack.heavySound = heavyAttackSound;
        PlayerAttack.weaponSprite = weaponSprite;
        FindObjectOfType<PlayerAttack>().weaponSpriteGameObject.sprite = PlayerAttack.weaponSprite;
        Destroy(gameObject);
    }
}
