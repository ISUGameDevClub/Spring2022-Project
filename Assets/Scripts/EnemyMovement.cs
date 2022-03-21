using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    enum EnemyType { Zombie, Ghost, Lumberjack, Clown, Lion };

    [SerializeField] EnemyType typeOfEnemy;
    [SerializeField] Animator enemyMovingAnim;
    [SerializeField] SpriteRenderer enemySpriteRender;
    [SerializeField] float moveSpeed;
    [SerializeField] float maxDist;
    [SerializeField] float minDist;
    [SerializeField] float bufferDist;

    bool enemyMoving;
    bool seesPlayer;
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
        seesPlayer = canSeePlayer();
        if (!enemyMoving)
        {
            switch (typeOfEnemy)
            {
                case EnemyType.Zombie:
                    {
                        enemyMovingAnim.SetTrigger("ZombieIdle");
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
                        enemyMovingAnim.SetTrigger("ZombieWalking");
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
        if ((Vector2.Distance(enemyRB.position, playerRB.position) > minDist) && seesPlayer)
        {
            direction = playerRB.position - enemyRB.position;
            lastpos = playerRB.position;
            enemyRB.MovePosition(enemyRB.position + (direction).normalized * moveSpeed * Time.fixedDeltaTime);
            if (direction.x > 0)
            {
                enemySpriteRender.flipX = true;
            }
            else if (direction.x < 0)
            {
                enemySpriteRender.flipX = false;
            }
            enemyMoving = true;
        }
        else if (!seesPlayer && Vector2.Distance(lastpos,enemyRB.position) >bufferDist)
        {
            direction = (lastpos - enemyRB.position);
            enemyRB.MovePosition(enemyRB.position + (direction).normalized * moveSpeed * Time.fixedDeltaTime);
            if (direction.x > 0)
            {
                enemySpriteRender.flipX = true;
            } else if(direction.x < 0)
            {
                enemySpriteRender.flipX = false;
            }
            enemyMoving = true;
        }
        else
        {
            enemyMoving = false;
        }
    }
    public bool canSeePlayer()
    {
        return Physics2D.Raycast(enemyRB.position, playerRB.position - enemyRB.position,maxDist,LayerMask.GetMask("Player"));
    }
}
