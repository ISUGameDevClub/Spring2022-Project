using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Rigidbody2D playerRB;
    public Rigidbody2D enemyRB;
    public float moveSpeed;
    public float maxDist;
    public float minDist;
    public float bufferDist;
    public Vector2 lastpos;
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        lastpos = enemyRB.position;
        playerRB = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        bool seesPlayer = canSeePlayer();
        if ((Vector2.Distance(enemyRB.position, playerRB.position) > minDist) && seesPlayer)
        {
           lastpos = playerRB.position;
           enemyRB.MovePosition(enemyRB.position + (playerRB.position - enemyRB.position ).normalized * moveSpeed * Time.fixedDeltaTime);
        }
        else if (!seesPlayer)
        {
            enemyRB.MovePosition(enemyRB.position + (lastpos-enemyRB.position).normalized * moveSpeed * Time.fixedDeltaTime);
        }
    }
    public bool canSeePlayer()
    {
        return Physics2D.Raycast(enemyRB.position, playerRB.position - enemyRB.position,maxDist,LayerMask.GetMask("Player"));
    }
}
