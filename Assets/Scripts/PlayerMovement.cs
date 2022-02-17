using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector2 direction2;
    public float speed;
    float xInput;
    float yInput;
    Rigidbody2D playerRB;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

        direction2 = new Vector2(xInput, yInput);

        if (direction2.magnitude > 1)
        {
            direction2.Normalize();
        }

        playerRB.MovePosition((Vector2)transform.position + (direction2 * speed * Time.fixedDeltaTime));
    }
}
