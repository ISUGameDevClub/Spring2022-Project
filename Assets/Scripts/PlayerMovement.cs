using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector2 direction;
    [SerializeField] float speed;
    float xinput;
    float yinput;
    Rigidbody2D playerRB;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        xinput = Input.GetAxis("Horizontal");
        yinput = Input.GetAxis("Vertical");

        direction = new Vector2(xinput, yinput);
        
        if (direction.magnitude > 1)
        {
            direction.Normalize();
        }
        playerRB.MovePosition((Vector2)transform.position + (direction * speed * Time.fixedDeltaTime));
    }
}
