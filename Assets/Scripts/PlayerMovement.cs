using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector2 direction;
    public float speed;
    float xInput;
    float yInput;
    Rigidbody2D playerRB;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

        direction = new Vector2(xInput, yInput);

        if (direction.magnitude > 1)
        {
            direction.Normalize();
        }

        playerRB.MovePosition((Vector2)(transform.position) + (direction * speed * Time.fixedDeltaTime));
    }
}
