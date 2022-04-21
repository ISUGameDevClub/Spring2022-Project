using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToMousePosition : MonoBehaviour
{
    private void Awake()
    {
        Vector3 mousePos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);

        int layerMask = LayerMask.GetMask("Walls");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, mousePos - transform.position, (mousePos - transform.position).magnitude, layerMask);
        if (hit.collider != null)
        {
            transform.position = hit.point;
        }
        else
        {
            transform.position = mousePos;
        }

        transform.rotation = Quaternion.identity;
    }
}
