using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector2 direction;
    public float speed = 1;
    float Xinput;
    float Yinput;
    Rigidbody2D playerRB;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Xinput = Input.GetAxis("Horizontal");
        Yinput = Input.GetAxis("Vertical");
        direction = new Vector2(Xinput, Yinput);
        if (direction.magnitude >= 1)
            direction.Normalize();
        playerRB.MovePosition((Vector2)transform.position + (direction * speed * Time.fixedDeltaTime));
    }
}
