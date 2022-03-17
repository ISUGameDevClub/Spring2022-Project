using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool hasHallway;
    public GameObject verticalHallway;
    public GameObject horizontalHallway;

    private Vector2 direction;

    public GameObject SpawnHallway(GameObject endHorizontalRoom, GameObject endVerticalRoom, float hallwayLength)
    {
        hasHallway = true;
        SetDirection();
        GameObject newRoom = null;
        Debug.Log(gameObject);
        if (Mathf.Abs(direction.x) > .5f)
        {
            GameObject hallWay = Instantiate(horizontalHallway, (Vector2)transform.position + direction * hallwayLength / 2, Quaternion.identity).gameObject;
            foreach(SpriteRenderer sr in hallWay.GetComponentsInChildren<SpriteRenderer>())
            {
                sr.size = new Vector2(hallwayLength - 1, 1);
            }

            newRoom = Instantiate(endHorizontalRoom, (Vector2)transform.position + direction * hallwayLength, Quaternion.identity).gameObject;
        }
        else if (Mathf.Abs(direction.y) > .5f)
        {
            Debug.Log("HIT");
            GameObject hallWay = Instantiate(verticalHallway, (Vector2)transform.position + direction * hallwayLength / 2, Quaternion.identity).gameObject;
            foreach (SpriteRenderer sr in hallWay.GetComponentsInChildren<SpriteRenderer>())
            {
                sr.size = new Vector2(1, hallwayLength - 1);
            }

            newRoom = Instantiate(endVerticalRoom, (Vector2)transform.position + direction * hallwayLength, Quaternion.identity).gameObject;
        }

        Debug.Log(newRoom);

        Door[] newDoors = newRoom.GetComponentsInChildren<Door>();
        foreach (Door d in newDoors)
        {
            d.SetDirection();
            if (direction + d.direction == Vector2.zero)
            {
                newRoom.transform.position -= d.transform.localPosition;
                break;
            }
        }

        return newRoom;
    }

    public void SetDirection()
    {
        direction = transform.right;
    }
}
