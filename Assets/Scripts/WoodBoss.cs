using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodBoss : MonoBehaviour
{
    Attack currentAttack;
    EnemyMovement myMovement;
    public GameObject waveAttackPrefab;
    public GameObject lineAttackPrefab;

    public AudioClip bossTheme;

    public int lineAttacksPerWaveAttack = 3;
    private int attacksSpawned = 0;
    // Start is called before the first frame update
    void Start()
    {
        myMovement = GetComponent<EnemyMovement>();
        currentAttack = GetComponent<Attack>();
    }

    void FixedUpdate()
    {
        if(currentAttack.canAttack && myMovement.canAggro)
        {
            if(bossTheme != null)
            {
                if(FindObjectOfType<Music>())
                {
                    FindObjectOfType<Music>().ChangeSong(bossTheme, null);
                }
                bossTheme = null;
            }

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
