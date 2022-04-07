using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWaveAttack : MonoBehaviour
{
    public GameObject[] stages;

    public float timeBetweenStages = 0.5f;
    private int currentStage;

    void Awake()
    {
        currentStage = 1;
        StartCoroutine(NextStage(timeBetweenStages));
    }

    IEnumerator NextStage(float time)
    {
        yield return new WaitForSeconds(time);
        GameObject newStage = Instantiate(stages[currentStage - 1], transform.position, transform.rotation);
        currentStage++;
        if (currentStage - 1 < stages.Length)
            StartCoroutine(NextStage(timeBetweenStages));
        else
            Destroy(gameObject);
    }
}
