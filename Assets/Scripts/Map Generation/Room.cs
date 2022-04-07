using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [HideInInspector]
    public bool roomActive;
    [HideInInspector]
    public bool roomCleared;
    [HideInInspector]
    public int enemyCount;

    private Door[] doors;
    private GameObject[] enemies;

    private GameObject[] preSpawnEffects;

    private void Start()
    {
        GetDoors();
        GetEnemies();
        DisableEnemies();
    }

    private void Update()
    {

    }

    public GameObject[] SpawnHallways(GameObject[] endHorizontalRooms, GameObject[] endVerticalRooms, float hallwayLength, int cornerCheck)
    {
        GetDoors();
        int numNewRooms = 0;
        foreach (Door d in doors)
        {
            if (!d.hasHallway)
                numNewRooms++;
        }

        GameObject[] newRooms = new GameObject[numNewRooms];
        for (int i = 0; i < doors.Length; i++)
        {
            if (!doors[i].hasHallway)
            {
                GameObject endHorizontalRoom = null;
                GameObject endVerticalRoom = null;
                if (endHorizontalRooms != null)
                    endHorizontalRoom = endHorizontalRooms[Random.Range(0, endHorizontalRooms.Length)];
                if (endVerticalRooms != null)
                    endVerticalRoom = endVerticalRooms[Random.Range(0, endVerticalRooms.Length)];
                GameObject temp = doors[i].SpawnHallway(endHorizontalRoom, endVerticalRoom, hallwayLength, cornerCheck);

                for(int k = 0; k < newRooms.Length; k++)
                {
                    if (newRooms[k] == null)
                    {
                        newRooms[k] = temp;
                        break;
                    }
                }
            }
        }

        return newRooms;
    }

    private void GetDoors()
    {
        if(doors == null)
            doors = GetComponentsInChildren<Door>();
        foreach (Door door in doors)
        {
            door.myRoom = this;
        }
    }

    private void GetEnemies()
    {
        enemyCount = 0;
        foreach (Transform child in transform)
        {
            if (child.tag == "Enemy")
            {
                child.GetComponent<Health>().myRoom = this;
                enemyCount++;
            }
        }
        enemies = new GameObject[enemyCount];
        foreach (Transform child in transform)
        {
            if (child.tag == "Enemy")
            {
                for(int i = 0; i < enemyCount; i++)
                {
                    if (enemies[i] == null)
                    {
                        enemies[i] = child.gameObject;
                        break;
                    }
                }
            }
        }

        for (int t = 0; t < enemies.Length; t++)
        {
            GameObject tmp = enemies[t];
            int r = Random.Range(t, enemies.Length);
            enemies[t] = enemies[r];
            enemies[r] = tmp;
        }
    }

    private void DisableEnemies()
    {
        preSpawnEffects = new GameObject[enemies.Length];
        for(int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].GetComponent<EnemySpawnParticle>() != null)
                preSpawnEffects[i] = Instantiate(enemies[i].GetComponent<EnemySpawnParticle>().preSpawnEffect, enemies[i].transform.position, Quaternion.identity);
            enemies[i].SetActive(false);
        }
    }

    private void EnableEnemies()
    {
        StartCoroutine(EnableEnemiesWithDelay());
    }

    private IEnumerator EnableEnemiesWithDelay()
    {
        yield return new WaitForSeconds(.2f);

        foreach (GameObject en in enemies)
        {
            if (en.GetComponent<EnemySpawnParticle>() != null)
                Instantiate(en.GetComponent<EnemySpawnParticle>().spawnEffect, en.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(.2f);
            for (int i = 0; i < preSpawnEffects.Length; i++)
            {
                Destroy(preSpawnEffects[i]);
            }
            en.SetActive(true);
            if (en.GetComponent<EnemyMovement>() != null)
                en.GetComponent<EnemyMovement>().AllowAggroStart();
        }
    }

    public void EnemyDied()
    {
        enemyCount--;
        if (enemyCount <= 0)
            RoomCleared();
    }

    public void PlayerEnter()
    {
        if (!roomActive && !roomCleared)
        {
            if (enemyCount > 0)
            {
                roomActive = true;
                foreach (Door door in doors)
                {
                    door.CloseDoor();
                }
                EnableEnemies();
                // Programming team can add code below here

            }
            else
                RoomCleared();
        }
    }

    public void PlayerExit()
    {
        // Programming team can add code below here

    }

    public void RoomCleared()
    {
        roomActive = false;
        roomCleared = true;
        foreach (Door door in doors)
        {
            if(door.hasHallway)
                door.OpenDoor();
        }
        // Programming team can add code below here

    }
}
