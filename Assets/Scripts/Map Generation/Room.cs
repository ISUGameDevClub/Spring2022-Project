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

    private void Start()
    {
        GetDoors();
        enemyCount = 0;
        foreach (Transform child in transform)
        {
            if (child.tag == "Enemy")
            {
                child.GetComponent<Health>().myRoom = this;
                enemyCount++;
            }
        }
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
            Debug.Log(enemyCount);
            if (enemyCount > 0)
            {
                roomActive = true;
                foreach (Door door in doors)
                {
                    door.CloseDoor();
                }
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
