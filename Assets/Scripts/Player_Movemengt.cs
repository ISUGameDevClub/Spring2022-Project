using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movemengt : MonoBehaviour
{
    Vector2 direction;
    [SerializeField] float speed;
    float Xinput;
    float Yinput;
    Rigidbody2D playerRB;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Xinput = Input.GetAxis("Horizontal");
        Yinput = Input.GetAxis("Vertical");

        direction = new Vector2(Xinput, Yinput);
        playerRB.MovePosition((Vector2)transform.position + (direction * speed * Time.fixedDeltaTime));
        if (direction.magnitude > 1)
        {
            direction.Normalize();
        }
    }
}
