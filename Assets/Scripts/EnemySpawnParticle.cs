using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnParticle : MonoBehaviour
{
    [SerializeField] private float dur;
    

    // Start is called before the first frame update
    void Start()
    {
        ParticleSystem sys = GetComponent<ParticleSystem>();
        var main = sys.main;

        main.duration = dur;

        
        // Instantiate(sys, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
