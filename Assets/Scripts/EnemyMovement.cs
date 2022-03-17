using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] EnemyType typeOfEnemy;
    enum EnemyType { Zombie,Ghost,Lumberjack};
    [SerializeField] Animator enemyMovingAnim;
    [SerializeField] SpriteRenderer enemySpriteRender;
    bool enemyMoving;
    public Rigidbody2D playerRB;
    public Rigidbody2D enemyRB;
    public float moveSpeed;
    public float maxDist;
    public float minDist;
    public float bufferDist;
    public Vector2 lastpos;
    public Vector2 direction;
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
        bool seesPlayer = canSeePlayer();
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
