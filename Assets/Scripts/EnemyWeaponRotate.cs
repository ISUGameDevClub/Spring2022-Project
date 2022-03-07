using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponRotate : MonoBehaviour
{
    public GameObject enemy;
    public GameObject weapon;
    public GameObject target; //target to which the weapon will rotate towards
    bool weaponOnLeft = false;
    Vector3 flipValues = new Vector3(0, 180, 0);
    Quaternion flip = new Quaternion();
    Vector3 rightAngle = new Vector3(0, 0, 90);
    Quaternion offset = new Quaternion();

    public float gunRotationMaxSpeed; //max speed in degrees per second that the weapon can rotate
    private void Start()
    {
        flip.eulerAngles = flipValues;
        offset.eulerAngles = rightAngle;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, target.transform.position - weapon.transform.position) * offset; //snaps weapon rotation to player's location on Start
    }
    // Update is called once per frame
    void Update()
    {
        if (weaponOnLeft)
        {
            float degreesPerSecond = gunRotationMaxSpeed * Time.deltaTime;
            Vector3 direction = target.transform.position - weapon.transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction) * flip;
            transform.rotation = Quaternion.RotateTowards(weapon.transform.rotation, targetRotation * offset, degreesPerSecond);
        }
        else
        {
            float degreesPerSecond = gunRotationMaxSpeed * Time.deltaTime;
            Vector3 direction = target.transform.position - weapon.transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
            transform.rotation = Quaternion.RotateTowards(weapon.transform.rotation, targetRotation * offset, degreesPerSecond);
        }
    }
    private void FixedUpdate()
    {
        if (!weaponOnLeft && weapon.transform.position.x - enemy.transform.position.x < -0.3)
        {
            weaponOnLeft = true;
            transform.rotation *= flip;
            transform.rotation *= offset;
            transform.rotation *= offset;
        }
        else if (weaponOnLeft && weapon.transform.position.x - enemy.transform.position.x > 0.3)
        {
            weaponOnLeft = false;
            transform.rotation *= flip;
            transform.rotation *= offset;
            transform.rotation *= offset;
        }
        else
            return;
    }
}
