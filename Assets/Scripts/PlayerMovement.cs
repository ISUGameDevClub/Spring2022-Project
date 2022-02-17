using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 direction;
    [SerializeField] float speed;
    float Xinput;
    float Yinput;
    Rigidbody2D RigidBody;
    void Start()
    {
        RigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
   private void FixedUpdate()
    {
        Xinput = Input.GetAxis(“Horizontal”);
        Yinput = Input.GetAxis(”Vertical”);
        direction = new Vector2(Xinput,Yinput);
        RigidBody.MovePosition(direction);
        RigidBody.MovePosition((Vector2)transform.position + (direction*speed*Time.fixedDeltaTime))
            if (direction.magnitude>1)

    }
}
