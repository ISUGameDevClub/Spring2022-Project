using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject attack;
    Attack at;
    public bool canSee = true; //sets whether the enemy sees the player and can  attack
    // Start is called before the first frame update
    void Start()
    {
        at = gameObject.GetComponent<Attack>();
    }
    void Update()
    {
        if (at.canAttack && canSee)
        {
            at.hurtboxPrefab = attack;
            at.SpawnAttack();
        }
    }
}
