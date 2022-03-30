using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] private float dur;

    void Start()
    {
        Destroy(gameObject, dur);
    }
}
