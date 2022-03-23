using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool hasHallway;
    public GameObject verticalHallway;
    public GameObject horizontalHallway;
    public GameObject outwardsIndicator;
    public Collider2D doorCollider;

    private Animator anim;

    private Vector2 direction;

    private void Start()
    {
        anim = GetComponent<Animator>();
        outwardsIndicator.SetActive(false);
    }

    public GameObject SpawnHallway(GameObject endHorizontalRoom, GameObject endVerticalRoom, float hallwayLength, int cornerCheck)
    {
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
                    break;
                }
            }
        }
        else if (cornerCheck == 2)
        {
            makeHallway = false;
            Vector2 myCorner = GetCorner();

            foreach (Vector2 corn in MapGenerator.generatedDirections)
            {
                if(corn == myCorner)
                {
                    makeHallway = true;
                    break;
                }
            }
        }
        else if(cornerCheck == 3)
        {
            makeHallway = false;
            Vector2 myCorner = GetCorner2();

            for (int i = 0; i < MapGenerator.generatedDirections.Length; i++)
            {
                if (MapGenerator.generatedDirections[i] == myCorner)
                {
                    if (MapGenerator.cornerDoorPositions[i] == Vector2.zero)
                    {
                        MapGenerator.cornerDoorPositions[i] = transform.position;
                        endHorizontalRoom = null;
                        endVerticalRoom = null;
                        hallwayLength = 0;
                        OpenDoor();
                    }
                    else
                    {
                        if (Mathf.Abs(direction.x) > .5f)
                        {
                            hallwayLength = Mathf.Abs(transform.position.x - MapGenerator.cornerDoorPositions[i].x) - 1.5f;
                        }
                        else if (Mathf.Abs(direction.y) > .5f)
                        {
                            hallwayLength = Mathf.Abs(transform.position.y - MapGenerator.cornerDoorPositions[i].y) - 1.5f;
                        }
                    }
                    makeHallway = true;
                }
            }
        }
        else if (cornerCheck == 4)
        {
            makeHallway = false;
            bool pointingIn = PointingIn();

            if (pointingIn)
            {
                Vector2 myCorner = GetOuterCorner();
                for (int i = 0; i < MapGenerator.generatedDirections.Length; i++)
                {
                    if (MapGenerator.generatedDirections[i] == myCorner)
                    {
                        endHorizontalRoom = null;
                        endVerticalRoom = null;

                        if (Mathf.Abs(direction.x) > .5f)
                        {
                            hallwayLength = Mathf.Abs(transform.position.x - MapGenerator.cornerDoorPositions[i].x);
                        }
                        else if (Mathf.Abs(direction.y) > .5f)
                        {
                            hallwayLength = Mathf.Abs(transform.position.y - MapGenerator.cornerDoorPositions[i].y);
                        }
                        makeHallway = true;
                    }
                }
            }
        }

        GameObject newRoom = null;
        if (makeHallway && hallwayLength != 0)
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

                if(endHorizontalRoom != null)
                    newRoom = Instantiate(endHorizontalRoom, (Vector2)transform.position + direction * hallwayLength, Quaternion.identity).gameObject;
            }
            else if (Mathf.Abs(direction.y) > .5f)
            {
                GameObject hallWay = Instantiate(verticalHallway, (Vector2)transform.position + direction * hallwayLength / 2, Quaternion.identity).gameObject;
                foreach (SpriteRenderer sr in hallWay.GetComponentsInChildren<SpriteRenderer>())
                {
                    sr.size = new Vector2(1, hallwayLength - 1);
                }

                if (endVerticalRoom != null)
                    newRoom = Instantiate(endVerticalRoom, (Vector2)transform.position + direction * hallwayLength, Quaternion.identity).gameObject;
            }

            if (newRoom != null)
            {
                Door[] newDoors = newRoom.GetComponentsInChildren<Door>();
                foreach (Door d in newDoors)
                {
                    d.SetDirection();
                    if (direction + d.direction == Vector2.zero)
                    {
                        d.OpenDoor();
                        newRoom.transform.position -= d.transform.localPosition;
                        break;
                    }
                }
            }

            OpenDoor();
        }
        else if (!makeHallway)
        {
            CloseDoor();
        }

        return newRoom;
    }

    public void SetDirection()
    {
        direction = transform.right;
    }

    private Vector2 GetCorner()
    {
        Vector2 myCorner = direction;

        if (Mathf.Abs(transform.position.x) > Mathf.Abs(transform.position.y))
        {
            if (transform.position.x >= 0)
                myCorner = direction + new Vector2(1, 0);
            else
                myCorner = direction + new Vector2(-1, 0);
        }
        else
        {
            if (transform.position.y >= 0)
                myCorner = direction + new Vector2(0, 1);
            else
                myCorner = direction + new Vector2(0, -1);
        }
        return myCorner;
    }

    private Vector2 GetCorner2()
    {
        Vector2 myCorner = direction;

        if (Mathf.Abs(direction.x) < .5f)
        {
            if (transform.position.x >= 0)
                myCorner = direction + new Vector2(1, 0);
            else
                myCorner = direction + new Vector2(-1, 0);
        }
        else
        {
            if (transform.position.y >= 0)
                myCorner = direction + new Vector2(0, 1);
            else
                myCorner = direction + new Vector2(0, -1);
        }
        return myCorner;
    }

    private Vector2 GetOuterCorner()
    {
        int x = -1;
        int y = -1;

        if (transform.position.x >= 0)
            x = 1;

        if (transform.position.y >= 0)
            y = 1;

        Vector2 myCorner = new Vector2(x, y);
        return myCorner;
    }

    private bool PointingIn()
    {
        if ((transform.position.x > 0 && direction.x < -0.5f) || (transform.position.x < 0 && direction.x > 0.5f))
            return true;
        if ((transform.position.y > 0 && direction.y < -0.5f) || (transform.position.y < 0 && direction.y > 0.5f))
            return true;

        return false;
    }

    public void OpenDoor()
    {
        hasHallway = true;
        if(anim == null)
            anim = GetComponent<Animator>();
        anim.SetBool("Open", true);
        doorCollider.enabled = false;
    }

    public void CloseDoor()
    {
        hasHallway = false;
        if (anim == null)
            anim = GetComponent<Animator>();
        anim.SetBool("Open", false);
        doorCollider.enabled = true;
    }
}
