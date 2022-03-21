using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool hasHallway;
    public GameObject verticalHallway;
    public GameObject horizontalHallway;

    private Vector2 direction;

    public GameObject SpawnHallway(GameObject endHorizontalRoom, GameObject endVerticalRoom, float hallwayLength, int cornerCheck)
    {
        hasHallway = true;
        SetDirection();

        bool makeHallway = true;

        if(cornerCheck == 1)
        {
            makeHallway = false;
            foreach (Vector2 corn in MapGenerator.generatedDirections)
            {
                if((corn.x == direction.x && Mathf.Abs(direction.x) > .5f) || (corn.y == direction.y && Mathf.Abs(direction.y) > .5f))
                {
                    makeHallway = true;
                }
            }
        }
        else if (cornerCheck == 2)
        {
            makeHallway = false;
            foreach (Vector2 corn in MapGenerator.generatedDirections)
            {
                if(((corn.x == 1 && transform.position.x > 10 || corn.x == -1 && transform.position.x < -10) && corn.y == direction.y) || ((corn.y == 1 && transform.position.y > 10 || corn.y == -1 && transform.position.y < -10) && corn.x == direction.x))
                {
                    makeHallway = true;
                }
            }
        }
        else if(cornerCheck == 3)
        {
            makeHallway = false;
        }

        GameObject newRoom = null;
        if (makeHallway)
        {
            if (hallwayLength < 0)
            {
                float goal = Mathf.Abs(hallwayLength);
                Vector2 goalPosition = -direction * hallwayLength;
                hallwayLength = Vector2.Distance(goalPosition, (Vector2)transform.position);
            }

            if (Mathf.Abs(direction.x) > .5f)
            {
                GameObject hallWay = Instantiate(horizontalHallway, (Vector2)transform.position + direction * hallwayLength / 2, Quaternion.identity).gameObject;
                foreach (SpriteRenderer sr in hallWay.GetComponentsInChildren<SpriteRenderer>())
                {
                    sr.size = new Vector2(hallwayLength - 1, 1);
                }

                newRoom = Instantiate(endHorizontalRoom, (Vector2)transform.position + direction * hallwayLength, Quaternion.identity).gameObject;
            }
            else if (Mathf.Abs(direction.y) > .5f)
            {
                GameObject hallWay = Instantiate(verticalHallway, (Vector2)transform.position + direction * hallwayLength / 2, Quaternion.identity).gameObject;
                foreach (SpriteRenderer sr in hallWay.GetComponentsInChildren<SpriteRenderer>())
                {
                    sr.size = new Vector2(1, hallwayLength - 1);
                }

                newRoom = Instantiate(endVerticalRoom, (Vector2)transform.position + direction * hallwayLength, Quaternion.identity).gameObject;
            }

            Door[] newDoors = newRoom.GetComponentsInChildren<Door>();
            foreach (Door d in newDoors)
            {
                d.SetDirection();
                if (direction + d.direction == Vector2.zero)
                {
                    d.hasHallway = true;
                    newRoom.transform.position -= d.transform.localPosition;
                    break;
                }
            }
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);
        }

        return newRoom;
    }

    public void SetDirection()
    {
        direction = transform.right;
    }
}
