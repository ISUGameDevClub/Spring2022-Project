using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnsSpikes : MonoBehaviour
{
    public GameObject[] spikes;
    public float timeBetweenSpawns;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpikeSpawner());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator SpikeSpawner()
    {
        while (true)
        {
            for(int i = 0; i < spikes.Length; i++)
            {
                yield return new WaitForSeconds(timeBetweenSpawns);
                Instantiate(spikes[i], transform.position, Quaternion.identity);
            }
        }
    }
}
