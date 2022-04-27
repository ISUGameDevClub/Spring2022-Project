using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSound : MonoBehaviour
{

    public void PlayKnock()
    {
        GetComponent<AudioSource>().Play();
    }
}
