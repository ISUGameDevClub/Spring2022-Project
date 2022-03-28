using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject lightAttack;
    public GameObject strongAttack;
    Attack at;
    // Start is called before the first frame update
    void Start()
    {
        at = gameObject.GetComponent<Attack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && at.canAttack)
        {
            at.hurtboxPrefab = lightAttack;
            at.SpawnAttack();
        }
        else if (Input.GetButton("Fire2") && at.canAttack)
        {
            at.hurtboxPrefab = strongAttack;
            at.SpawnAttack();
        }
        else
            return;
    }
}
