using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float dash = 10;
    [SerializeField] float timeDashing = 0.5f;
    [SerializeField] float dashcoolsec = 1.5f;
    [SerializeField] float tiredtime = 0.2f;
    [SerializeField] float tiredspeed = 2.5f;
    [SerializeField] Animator playerWalking;
    [SerializeField] Sprite idleSprite;
    [SerializeField] SpriteRenderer playerSprite;
    Rigidbody2D playerRB;
    Vector2 direction;
    float Xinput;
    float Yinput;
    bool dashing = false;
    bool canmove = true;
    bool dashcooling;
    bool isslowed = false;



    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !dashing && !dashcooling)
        {
            StartCoroutine(DashTime(timeDashing));
        }
        if(direction != Vector2.zero) {
            playerWalking.SetBool("Moving", true);
        }
        else
        {
            playerWalking.SetBool("Moving",false);
        }
        if (Xinput < 0 && !dashing)
        {
            playerSprite.flipX = true;
        }
        else if(Xinput > 0 && !dashing)
        {
            playerSprite.flipX = false;
        }
    }
    private void FixedUpdate()
    {
        Xinput = Input.GetAxis("Horizontal");
        Yinput = Input.GetAxis("Vertical");
        if(canmove)
        {
        direction = new Vector2(Xinput, Yinput);

        }
        if (direction.magnitude > 1)
        {
            direction.Normalize();
        }


        if (dashing)
        {
            playerRB.MovePosition((Vector2)transform.position + (direction * dash * Time.fixedDeltaTime));
        }

        else
        {
            playerRB.MovePosition((Vector2)transform.position + (direction * speed * Time.fixedDeltaTime));
        }
        if (isslowed)
        {
            playerRB.MovePosition((Vector2)transform.position + (direction * tiredspeed * Time.fixedDeltaTime));
        }

    }

    IEnumerator DashTime (float sec)
    {
        dashing = true;
        canmove = false;
        yield return new WaitForSeconds(sec);
        canmove = true;
        dashing = false;
        dashcooling = true;
        StartCoroutine(DashCoolDown(dashcoolsec));
    }
    IEnumerator DashCoolDown (float dashcoolsec)
    {
        isslowed = true;
            yield return new WaitForSeconds(tiredtime);
        isslowed = false;
            yield return new WaitForSeconds(dashcoolsec);
            dashcooling = false;

    }
}
