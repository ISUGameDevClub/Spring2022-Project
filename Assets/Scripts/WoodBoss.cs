using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodBoss : MonoBehaviour
{
    Attack currentAttack;
    public GameObject waveAttackPrefab;
    public GameObject lineAttackPrefab;

    public int lineAttacksPerWaveAttack = 3;
    private int attacksSpawned = 0;
    // Start is called before the first frame update
    void Start()
    {
        currentAttack = GetComponent<Attack>();
    }

    void FixedUpdate()
    {
        if(currentAttack.canAttack)
        {
            attacksSpawned++;
            if(attacksSpawned % (lineAttacksPerWaveAttack + 1) == 0)
            {
                WaveAttack();
            }
            else
            {
                LineAttack();
            }
        }
    }

    void WaveAttack()
    {
        currentAttack.hurtboxPrefab = waveAttackPrefab;
        currentAttack.SpawnAttack();
    }

    void LineAttack()
    {
        currentAttack.hurtboxPrefab = lineAttackPrefab;
        currentAttack.SpawnAttack();
    }
}
