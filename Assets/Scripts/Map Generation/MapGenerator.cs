using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject startingRoom;

    public GameObject[] horizontalRooms;
    public GameObject[] verticalRooms;
    public GameObject connector;

    [Range(0, 4)]
    public int numberOfCornersGenerated;

    [HideInInspector]
    public GameObject[] innerRooms;
    [HideInInspector]
    public GameObject[] firstConnectors;

    public static Vector2[] generatedDirections;

    void Start()
    {
        // Picks which 2 corners will be generated
        // Top Right: 0
        // Bottom Right: 1
        // Bottom Left: 2
        // Top Left: 3

        int[] generatedCorners = new int[numberOfCornersGenerated];
        for (int i = 0; i < generatedCorners.Length; i++)
        {
            bool unique = false;
            while (!unique)
            {
                unique = true;
                generatedCorners[i] = Random.Range(0, 4);
                for (int k = i - 1; k >= 0; k--)
                {
                    if (generatedCorners[i] == generatedCorners[k])
                    {
                        unique = false;
                        break;
                    }
                }
            }
        }

        generatedDirections = new Vector2[generatedCorners.Length];
        for (int i = 0; i < generatedDirections.Length; i++)
        {
            if (generatedCorners[i] == 0)
                generatedDirections[i] = new Vector2(1, 1);
            else if (generatedCorners[i] == 1)
                generatedDirections[i] = new Vector2(1, -1);
            else if (generatedCorners[i] == 2)
                generatedDirections[i] = new Vector2(-1, -1);
            else if (generatedCorners[i] == 3)
                generatedDirections[i] = new Vector2(-1, 1);

            Debug.Log(generatedDirections[i]);
        }

        // spawn paths ending with inner rooms around the starting room
        innerRooms = startingRoom.GetComponent<Room>().SpawnHallways(horizontalRooms[0], verticalRooms[0], 10, 0);

        // spawn paths from inner rooms reaching 47.5 in each direction that end with a 4 way
        GameObject[] tempConnectors = new GameObject[4];
        for (int i = 0; i < innerRooms.Length; i++)
        {
            tempConnectors[i] = innerRooms[i].GetComponent<Room>().SpawnHallways(connector, connector, -47.5f, 1)[0];
        }
        int connectorCount = 0;
        foreach(GameObject g in tempConnectors)
        {
            if (g != null)
                connectorCount++;
        }

        // have the 4 ways pick a direction to spawn their next room

        // add in additional boss and treasure rooms
    }
}
