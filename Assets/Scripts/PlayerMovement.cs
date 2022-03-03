using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector2 direction;
    [SerializeField] float speed;
    float Xinput;
    float Yinput;
    [SerializeField] float dash = 10;
    bool dashing = false;
    [SerializeField] float sec = 0.5f;
    bool canmove = true;
    bool dashcooling;
    [SerializeField] float dashcoolsec = 1.5f;
    bool isslowed = false;
    [SerializeField] float tiredtime = 0.2f;
    [SerializeField] float tiredspeed = 2.5f;
    Rigidbody2D playerRB;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
         if (Input.GetKeyDown(KeyCode.LeftShift) && !dashing && !dashcooling)
        {
            StartCoroutine(DashTime(sec));
            
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
