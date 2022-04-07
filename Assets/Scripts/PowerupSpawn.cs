using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawn : MonoBehaviour
{
    public void SpawnPowerup(GameObject powerup)
    {
        Instantiate(powerup, transform.position, Quaternion.identity);
    }
}
