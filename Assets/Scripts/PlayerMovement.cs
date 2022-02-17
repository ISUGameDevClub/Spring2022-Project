using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  Vector2 direction;
  [SerializeField] float speed = 10f;
  float xInput;
  float yInput;
  Rigidbody2D whateverYouLike;
    // Start is called before the first frame update
    void Start()
    {
      whateverYouLike = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
      xInput = Input.GetAxis("Horizontal");
      yInput = Input.GetAxis("Vertical");
      direction = new Vector2(xInput, yInput);

      if(direction.magnitude > 1)
      {
          direction.Normalize();
      }
      
      whateverYouLike.MovePosition((Vector2)transform.position + (direction * speed * Time.fixedDeltaTime));

    }
}
