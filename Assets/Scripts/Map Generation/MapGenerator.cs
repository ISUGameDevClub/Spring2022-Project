using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public static Vector2[] generatedDirections;
    public static Vector2[] cornerDoorPositions;

    public GameObject startingRoom;
    public GameObject connector;

    public GameObject[] horizontalRooms;
    public GameObject[] verticalRooms;

    [Range(0, 4)]
    public int numberOfCornersGenerated;

    public GameObject bossRoom;

    public int treasureRoomCount;
    public GameObject treasureRoom;

    public GameObject bossRoomIcon;
    public GameObject treasureRoomIcon;

    private GameObject[] innerRooms;
    private GameObject[] firstConnectors;
    private GameObject[] outerRooms;
    private GameObject[] secondConnectors;

    void Start()
    {
        // Picks which 2 corners will be generated
        // Top Right: 0
        // Bottom Right: 1
        // Bottom Left: 2
        // Top Left: 3

        // Decide which corners will be used in generation
        int[] generatedCorners = UniqueRandomNumbers(numberOfCornersGenerated);

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
            
        }
        cornerDoorPositions = new Vector2[generatedDirections.Length];

        // spawn paths ending with inner rooms around the starting room
        innerRooms = Instantiate(startingRoom, Vector2.zero, Quaternion.identity).GetComponent<Room>().SpawnHallways(horizontalRooms, verticalRooms, 10, 0);

        // spawn paths from inner rooms reaching 47.5 in each direction that end with a 4 way
        GameObject[] tempConnectors = new GameObject[4];
        int connectorCount = 0;
        for (int i = 0; i < innerRooms.Length; i++)
        {
            GameObject[] conectors = new GameObject[1];
            conectors[0] = connector;
            tempConnectors[i] = innerRooms[i].GetComponent<Room>().SpawnHallways(conectors, conectors, -43.25f, 1)[0];
            if(tempConnectors[i] != null)
                connectorCount++;
        }

        firstConnectors = new GameObject[connectorCount];
        for (int i = 0; i < tempConnectors.Length; i++)
        {
            if(tempConnectors[i] != null)
            {
                for (int k = 0; k < firstConnectors.Length; k++)
                {
                    if (firstConnectors[k] == null)
                    {
                        firstConnectors[k] = tempConnectors[i];
                        break;
                    }
                }
            }
        }

        // have the 4 ways pick a direction to spawn their next room
        GameObject[] tempOuterRooms = new GameObject[4];
        connectorCount = 0;
        for (int i = 0; i < firstConnectors.Length; i++)
        {
            GameObject[] newOuterRooms = firstConnectors[i].GetComponent<Room>().SpawnHallways(horizontalRooms, verticalRooms, 17, 2);
            for(int k = 0; k < newOuterRooms.Length; k++)
            {
                if (newOuterRooms[k] != null)
                {
                    for (int g = 0; g < tempOuterRooms.Length; g++)
                    {
                        if (tempOuterRooms[g] == null)
                        {
                            tempOuterRooms[g] = newOuterRooms[k];
                            break;
                        }

                    }
                    connectorCount++;
                }
            }
        }

        outerRooms = new GameObject[connectorCount];
        for (int i = 0; i < tempOuterRooms.Length; i++)
        {
            if (tempOuterRooms[i] != null)
            {
                for (int k = 0; k < outerRooms.Length; k++)
                {
                    if (outerRooms[k] == null)
                    {
                        outerRooms[k] = tempOuterRooms[i];
                        break;
                    }
                }
            }
        }

        // Spawn final corner connectors
        tempConnectors = new GameObject[4];
        connectorCount = 0;
        for (int i = 0; i < outerRooms.Length; i++)
        {
            GameObject[] conectors = new GameObject[1];
            conectors[0] = connector;
            tempConnectors[i] = outerRooms[i].GetComponent<Room>().SpawnHallways(conectors, conectors, 0, 3)[0];
            if (tempConnectors[i] != null)
                connectorCount++;
        }

        secondConnectors = new GameObject[connectorCount];
        for (int i = 0; i < tempConnectors.Length; i++)
        {
            if (tempConnectors[i] != null)
            {
                for (int k = 0; k < secondConnectors.Length; k++)
                {
                    if (secondConnectors[k] == null)
                    {
                        secondConnectors[k] = tempConnectors[i];
                        break;
                    }
                }
            }
        }

        // Spawn hallway to connect final corner back to room
        for (int i = 0; i < secondConnectors.Length; i++)
        {
            secondConnectors[i].GetComponent<Room>().SpawnHallways(null, null, 0, 4);
        }

        // find all closed doors and add in boss and treasure rooms
        Door[] allDoors = FindObjectsOfType<Door>();
        int closedDoorNum = 0;
        for(int i = 0; i < allDoors.Length; i++)
        {
            if (!allDoors[i].hasHallway)
                closedDoorNum++;
        }

        Door[] closedDoors = new Door[closedDoorNum];
        for (int i = 0; i < allDoors.Length; i++)
        {
            if (!allDoors[i].hasHallway)
            {
                for(int k = 0; k < closedDoors.Length; k++)
                {
                    if(closedDoors[k] == null)
                    {
                        closedDoors[k] = allDoors[i];
                        break;
                    }
                }
            }
        }

        int[] extraRoomLocations = UniqueRandomNumbers(1 + treasureRoomCount);

        // spawn boss room
        Instantiate(bossRoomIcon, closedDoors[extraRoomLocations[0]].transform.position, Quaternion.identity);
        closedDoors[extraRoomLocations[0]].SpawnHallway(bossRoom, bossRoom, 15, 0);

        // spawn treasure rooms
        for (int i = 1; i <= treasureRoomCount; i++)
        {
            Instantiate(treasureRoomIcon, closedDoors[extraRoomLocations[i]].transform.position, Quaternion.identity);
            closedDoors[extraRoomLocations[i]].SpawnHallway(treasureRoom, treasureRoom, 10, 0);
        }

        // Spawn Powerups
        GetComponent<PowerupSpawnManager>().SpawnPowerups();

        //Block unused paths
        Door[] allDoors2 = FindObjectsOfType<Door>();
        int closedDoorNum2 = 0;
        for (int i = 0; i < allDoors2.Length; i++)
        {
            if (!allDoors2[i].hasHallway)
                closedDoorNum2++;
        }

        Door[] closedDoors2 = new Door[closedDoorNum2];
        for (int i = 0; i < allDoors2.Length; i++)
        {
            if (!allDoors2[i].hasHallway)
            {
                for (int k = 0; k < closedDoors2.Length; k++)
                {
                    if (closedDoors2[k] == null)
                    {
                        closedDoors2[k] = allDoors2[i];
                        break;
                    }
                }
            }
        }

        for (int i = 0; i < closedDoors2.Length; i++)
        {
            closedDoors2[i].EnablePathBlocker();
        }
    }

    public int[] UniqueRandomNumbers(int numbersGenerated)
    {
        int[] result = new int[numbersGenerated];
        for (int i = 0; i < result.Length; i++)
        {
            bool unique = false;
            while (!unique)
            {
                unique = true;
                result[i] = Random.Range(0, 4);
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
