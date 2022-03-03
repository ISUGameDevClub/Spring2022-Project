using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponRotate : MonoBehaviour
{
    GameObject player;
    public GameObject weapon;
    bool weaponOnLeft = false;
    Vector3 flipValues = new Vector3(0, 180, 0);
    Quaternion flip = new Quaternion();
    Vector3 rightAngle = new Vector3(0, 0, 90);
    Quaternion offset = new Quaternion();
    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        flip.eulerAngles = flipValues;
        offset.eulerAngles = rightAngle;

    }
    // Update is called once per frame
    void Update()
    {
        if (weaponOnLeft)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position) * flip * offset;
        }
        else
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position) * offset;
        }
    }
    private void FixedUpdate()
    {
        if (!weaponOnLeft && weapon.transform.position.x - player.transform.position.x < -0.3)
            weaponOnLeft = true;
        else if (weaponOnLeft && weapon.transform.position.x - player.transform.position.x > 0.3)
            weaponOnLeft = false;
        else
            return;
    }
}
