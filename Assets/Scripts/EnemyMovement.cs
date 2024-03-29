using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    enum EnemyType { Zombie, Ghost, Lumberjack, Clown, Lion , DarkWoodsBoss, CircusBoss };

    [SerializeField] EnemyType typeOfEnemy;
    [SerializeField] Animator enemyMovingAnim;
    [SerializeField] SpriteRenderer enemySpriteRender;
    [SerializeField] float moveSpeed;
    [SerializeField] float maxDist;
    [SerializeField] float minDist;
    [SerializeField] float bufferDist;
    [SerializeField] bool charger;
    [SerializeField] AudioSource characterAudioSource;
    [SerializeField] AudioClip aggroClip;
    [HideInInspector] public bool aggro = false;
    public bool dontFlip = false;
    bool enemyMoving;
    bool charging;
    Rigidbody2D playerRB;
    Rigidbody2D enemyRB;
    Vector2 lastpos;
    Vector2 direction;
    public bool canAggro;
    [SerializeField] bool runAway;
    [SerializeField] float bufferRange = 0.25f;
    void Start()
    {
        canAggro = false;
        aggro = false;
        enemyRB = GetComponent<Rigidbody2D>();
        lastpos = enemyRB.position;
        playerRB = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        enemyMoving = false;
    }

    private void Update()
    {
        if (enemyMovingAnim != null && typeOfEnemy != EnemyType.CircusBoss)
        {
            if (!enemyMoving)
            {
                enemyMovingAnim.SetBool("Walking", false);
            }
            else if (enemyMoving)
            {
                enemyMovingAnim.SetBool("Walking", true);
            }
        }
    }

    void FixedUpdate()
    {
        if (canAggro && typeOfEnemy != EnemyType.CircusBoss)
        {
            enemyRB.velocity = Vector2.zero;
            bool seesPlayer = canSeePlayer();
            float distanceFromPlayer = Vector2.Distance(enemyRB.position, playerRB.position);

            if ((Vector2.Distance(enemyRB.position, playerRB.position) <= bufferDist + bufferRange && Vector2.Distance(enemyRB.position, playerRB.position) >= bufferDist - bufferRange) && !charger)
            {
                Debug.Log("not moving");
                enemyMoving = false;
            }
            else if (distanceFromPlayer > minDist && seesPlayer && !charging && !runAway)
            {
                direction = playerRB.position - enemyRB.position;
                lastpos = playerRB.position;
                if (direction.x > 0 && !dontFlip)
                {
                    enemySpriteRender.flipX = true;
                }
                else if (direction.x < 0)
                {
                    enemySpriteRender.flipX = false;
                }
                enemyMoving = true;
                if (charger)
                {
                    charging = true;
                    if (enemyMovingAnim != null)
                    {
                        enemyMovingAnim.SetBool("Charging", true);
                    }
                }
            }
            else if (distanceFromPlayer > minDist && !seesPlayer && !charging && !runAway)
            {
                direction = lastpos - enemyRB.position;
                if (direction.x > 0 && !dontFlip)
                {
                    enemySpriteRender.flipX = true;
                }
                else if (direction.x < 0)
                {
                    enemySpriteRender.flipX = false;
                }
                enemyMoving = true;
                if (charger)
                {
                    charging = true;
                    if (enemyMovingAnim != null)
                    {
                        enemyMovingAnim.SetBool("Charging", true);
                    }
                }
            }
            else if ((Vector2.Distance(enemyRB.position, playerRB.position) < minDist) && seesPlayer && runAway)
            {
                  direction = enemyRB.position - playerRB.position;
                  lastpos = playerRB.position;
                  enemyRB.MovePosition(enemyRB.position + (direction).normalized * moveSpeed * Time.fixedDeltaTime);
                  if (direction.x > 0 && !dontFlip)
                  {
                      enemySpriteRender.flipX = true;
                  }
                  else if (direction.x < 0)
                  {
                      enemySpriteRender.flipX = false;
                  }
                  enemyMoving = true;
            }
            else if (!charging)
            {
                enemyMoving = false;
            }

            if (enemyMoving)
            {
                enemyRB.MovePosition(enemyRB.position + (direction).normalized * moveSpeed * Time.fixedDeltaTime);
            }
        }
    }

    public void AllowAggroStart()
    {
        StartCoroutine(AllowAggro());
        if (GetComponent<CircusBoss>() != null)
        {
            StartCoroutine(GetComponent<CircusBoss>().EnterRoom(0.5f)); //PUT STARTING ANIMATION LENGTH FOR CIRCUS BOSS IN HERE
        }
    }

    public IEnumerator AllowAggro()
    {
        yield return new WaitForSeconds(.35f);
        canAggro = true;
    }

    public bool canSeePlayer()
    {
        int layerMask = LayerMask.GetMask("Player","Walls");
        RaycastHit2D hit = Physics2D.Raycast(enemyRB.position, playerRB.position - enemyRB.position, maxDist, layerMask);
        if(hit.collider != null && hit.collider.gameObject.tag == "Player")
        {
            if (!aggro)
            {
                if (aggroClip != null)
                {
                    characterAudioSource.clip = aggroClip;
                    characterAudioSource.Play();
                }
                aggro = true;
            }
            return true;
        }
        return false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int layerMask = LayerMask.GetMask("Walls");
        if (collision.gameObject.layer == 9)
        {
            if (charging)
            {
                StartCoroutine(StopCharging());
            }
        }
    }

    private IEnumerator StopCharging()
    {
        enemyMoving = false;
        if (enemyMovingAnim != null)
        {
            enemyMovingAnim.SetBool("Charging", false);
        }
        yield return new WaitForSeconds(1);
        charging = false;
    }

    public void KnockBack(Vector2 direction, float knockbackPower, float knockbackTime)
    {
        //First disable enemy movement
        //Set enemy velocity to 0
        //Move enemy backwards
        //Set enemy velocity back to 0
        //Enable movement
        canAggro = false;
        enemyRB.velocity = Vector2.zero;
        enemyRB.AddForce(direction * knockbackPower, ForceMode2D.Impulse);
        StartCoroutine(knockBackTime(knockbackTime));
        //Debug.Log("Knocked Back!");
    }

    private IEnumerator knockBackTime(float time)
    {
        yield return new WaitForSeconds(time);
        enemyRB.velocity = Vector2.zero;
        canAggro = true;
    }
}
