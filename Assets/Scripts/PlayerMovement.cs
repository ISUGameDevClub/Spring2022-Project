using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float dash = 10;
    public float timeDashing = 0.5f;
    public float dashcoolsec = 1.5f;
    [SerializeField] float tiredtime = 0.2f;
    [SerializeField] float tiredspeed = 2.5f;
    [SerializeField] Animator playerAnim;
    [SerializeField] Sprite idleSprite;
    [SerializeField] SpriteRenderer playerSprite;
    [SerializeField] PlayerWeaponRotate weaponRotate;
    PlayerAttack playerAtk;
    Rigidbody2D playerRB;
    Vector2 direction;
    float Xinput;
    float Yinput;
    bool dashing = false;
    bool canmove = true;
    bool dashcooling;
    bool isslowed = false;

    bool stun = false;
    
    

    float finalSpeed;
    float finalDash;
    float finalTimeDashing;
    float finalDashcoolsec;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAtk = GetComponent<PlayerAttack>();
        UpdatePassives();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && !dashing && !dashcooling)
        {
            StartCoroutine(DashTime(finalTimeDashing));
        }

        if(direction != Vector2.zero) {
            playerAnim.SetBool("Moving", true);
        }
        else
        {
            playerAnim.SetBool("Moving",false);
        }

        if (weaponRotate.weaponOnLeft && !dashing)
        {
            playerSprite.flipX = true;
        }
        else if(!weaponRotate.weaponOnLeft && !dashing)
        {
            playerSprite.flipX = false;
        }
    }

    private void FixedUpdate()
    {
        Xinput = Input.GetAxis("Horizontal");
        Yinput = Input.GetAxis("Vertical");

        if(canmove && !stun)
        {
            direction = new Vector2(Xinput, Yinput);
        }

        if (direction.magnitude > 1)
        {
            direction.Normalize();
        }


        if (dashing && !stun)
        {
            playerRB.MovePosition((Vector2)transform.position + (direction * finalDash * Time.fixedDeltaTime));
        }

        else if (!stun)
        {
            playerRB.MovePosition((Vector2)transform.position + (direction * finalSpeed * Time.fixedDeltaTime));
        }
        if (isslowed && !stun)
        {
            playerRB.MovePosition((Vector2)transform.position + (direction * tiredspeed * Time.fixedDeltaTime));
        }

 
    }

    IEnumerator DashTime (float sec)
    {
        gameObject.layer = 11; //Set Layer to PlayerInvincible
        float XInput = Input.GetAxisRaw("Horizontal");
        float YInput = Input.GetAxisRaw("Vertical");

        if (XInput != 0 || YInput != 0)
        {

            playerAnim.SetBool("Dashing", true);
            playerAtk.canAttack = false;
            dashing = true;
            canmove = false;

            direction = new Vector2(XInput, YInput);

            if (XInput > 0)
                playerSprite.flipX = false;
            else if (XInput < 0)
                playerSprite.flipX = true;

            weaponRotate.weaponSprite.color = new Color(1, 1, 1, 0);

            yield return new WaitForSeconds(sec);

            weaponRotate.weaponSprite.color = new Color(1, 1, 1, 1);
            canmove = true;
            playerAnim.SetBool("Dashing", false);
            playerAtk.canAttack = true;
            dashing = false;
            dashcooling = true;
            StartCoroutine(DashCoolDown(finalDashcoolsec));
        }
        gameObject.layer = 10; //Set Layer to Player
    }

    IEnumerator DashCoolDown (float dashcoolsec)
    {
        isslowed = true;
        yield return new WaitForSeconds(tiredtime);
        isslowed = false;
        yield return new WaitForSeconds(dashcoolsec);
        dashcooling = false;

    }

    public void UpdatePassives()
    {
        finalSpeed = speed + PassiveBuffs.speedIncrease;
        finalDash = dash + PassiveBuffs.dashSpeedIncrease;
        finalTimeDashing = timeDashing + PassiveBuffs.dashTimeIncrease;
        finalDashcoolsec = dashcoolsec - PassiveBuffs.dashCooldownDecrease;
    }
    public void GetStunned(float stuntimer)
    {
        stun = true;
        StartCoroutine(stuntime(stuntimer));
    }
    public IEnumerator stuntime(float stuntimer)
    {
        yield return new WaitForSeconds(stuntimer);
        stun = false;
    }

}
