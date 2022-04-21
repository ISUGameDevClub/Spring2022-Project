using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawnManager : MonoBehaviour
{
    public GameObject[] powerups;

    public void SpawnPowerups()
    {
        PowerupSpawn[] spawns = FindObjectsOfType<PowerupSpawn>();
        int[] powerupNumbers = UniqueRandomNumbers(spawns.Length, 0, powerups.Length);

        for(int i = 0; i < spawns.Length; i++)
        {
            spawns[i].SpawnPowerup(powerups[powerupNumbers[i]]);
        }
    }

    public int[] UniqueRandomNumbers(int numbersGenerated, int min, int max)
    {
        int[] result = new int[numbersGenerated];
        for (int i = 0; i < result.Length; i++)
        {
            bool unique = false;
            while (!unique)
            {
                unique = true;
                result[i] = Random.Range(min, max);
                for (int k = i - 1; k >= 0; k--)
                {
                    if (result[i] == result[k])
                    {
                        unique = false;
                        break;
                    }
                }
            }
        }

        return result;
    }
}
