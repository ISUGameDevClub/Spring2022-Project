using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToMousePosition : MonoBehaviour
{
    private void Awake()
    {
        transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, transform.position.z); //sets position to current mouse position at the moment the object is instantiated
    }
}
