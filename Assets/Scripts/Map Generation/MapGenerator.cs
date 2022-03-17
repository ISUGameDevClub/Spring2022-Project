using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject startingRoom;

    public GameObject[] horizontalRooms;
    public GameObject[] verticalRooms;

    public GameObject[] innerRooms;

    void Start()
    {
        innerRooms = startingRoom.GetComponent<Room>().SpawnHallways(horizontalRooms[0], verticalRooms[0], 10);


    }
}
