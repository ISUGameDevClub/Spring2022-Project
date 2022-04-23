using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    Collider2D myCol;
    // Start is called before the first frame update
    void Start()
    {
        myCol = GetComponent<Collider2D>();
        myCol.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Stab()
    {
        myCol.enabled = true;
        GetComponent<AudioSource>().Play();
    }

    public void End()
    {
        Destroy(gameObject);
    }
}
