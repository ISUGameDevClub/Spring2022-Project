using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private Door[] doors;

    public GameObject[] SpawnHallways(GameObject[] endHorizontalRooms, GameObject[] endVerticalRooms, float hallwayLength, int cornerCheck)
    {
        doors = GetComponentsInChildren<Door>();
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
}
