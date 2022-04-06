using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    enum EnemyType { Zombie, Ghost, Lumberjack, Clown, Lion , DarkWoodsBoss };

    [SerializeField] EnemyType typeOfEnemy;
    [SerializeField] Animator enemyMovingAnim;
    [SerializeField] SpriteRenderer enemySpriteRender;
    [SerializeField] float moveSpeed;
    [SerializeField] float maxDist;
    [SerializeField] float minDist;
    [SerializeField] float bufferDist;
    [SerializeField] bool charger;

    public bool dontFlip = false;
    bool enemyMoving;
    bool charging;
    Rigidbody2D playerRB;
    Rigidbody2D enemyRB;
    Vector2 lastpos;
    Vector2 direction;
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        lastpos = enemyRB.position;
        playerRB = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        enemyMoving = false;
    }

    private void Update()
    {
        if (!enemyMoving)
        {
            switch (typeOfEnemy)
            {
                case EnemyType.Zombie:
                    {
                        enemyMovingAnim.SetBool("ZombieWalking",false);
                        return; 
                    }
                case EnemyType.Ghost:
                    {
                        return;
                    }
                case EnemyType.Lumberjack:
                    {
                        return;
                    }
            }
        }
        else if(enemyMoving)
        {
            switch (typeOfEnemy)
            {
                case EnemyType.Zombie:
                    {
                        enemyMovingAnim.SetBool("ZombieWalking", true);
                        return;
                    }
                case EnemyType.Ghost:
                    {
                        return;
                    }
                case EnemyType.Lumberjack:
                    {
                        return;
                    }
            }
        }
    }

    void FixedUpdate()
    {
        enemyRB.velocity = Vector2.zero;
        bool seesPlayer = canSeePlayer();
        float distanceFromPlayer = Vector2.Distance(enemyRB.position, playerRB.position);

        if (distanceFromPlayer > minDist && seesPlayer && !charging)
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
                enemyMovingAnim.SetBool("Charging", true);
            }
        }
        else if (Vector2.Distance(enemyRB.position, lastpos) > bufferDist && !seesPlayer && !charger)
        {
            direction = (lastpos - enemyRB.position);
            if (direction.x > 0 && !dontFlip)
            {
                enemySpriteRender.flipX = true;
            } else if(direction.x < 0)
            {
                enemySpriteRender.flipX = false;
            }
            enemyMoving = true;
        }
        else if (!charging)
        {
            enemyMoving = false;
        }

        if(enemyMoving)
        {
            enemyRB.MovePosition(enemyRB.position + (direction).normalized * moveSpeed * Time.fixedDeltaTime);
        }
    }

    public bool canSeePlayer()
    {
        int layerMask = LayerMask.GetMask("Player","Walls");
        RaycastHit2D hit = Physics2D.Raycast(enemyRB.position, playerRB.position - enemyRB.position, maxDist, layerMask);
        if(hit.collider != null && hit.collider.gameObject.tag == "Player")
        {
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
        enemyMovingAnim.SetBool("Charging", false);
        yield return new WaitForSeconds(1);
        charging = false;
    }
}
