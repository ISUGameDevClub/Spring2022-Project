using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]

public class EnemyAttack : MonoBehaviour
{
    public GameObject attack;
    Attack at;
    EnemyMovement em;

    // Start is called before the first frame update
    void Start()
    {
        em = gameObject.GetComponent<EnemyMovement>();
        at = gameObject.GetComponent<Attack>();
    }
    void Update()
    {
        if (at.canAttack && em.canSeePlayer())
        {
            at.hurtboxPrefab = attack;
            at.SpawnAttack();
        }
    }
}
