using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public Transform player;
    public Rigidbody2D enemy;
    public float moveSpeed = .5f;
    public float maxDist = 10.0f;
    public float minDist = 1.5f;
    public Vector2 lastpos;


    // Start is called before the first frame update
    void Start()
    {
        // player = GetComponent<Transform>();
        enemy = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // transform.LookAt(player);

        if (Vector2.Distance(transform.position, player.position) > minDist && 
            Vector2.Distance(transform.position, player.position) <= maxDist)
        {
            lastpos = player.position;
            // transform.position += transform.forward * moveSpeed * Time.deltaTime;
            enemy.MovePosition(player.position += player.position * moveSpeed * Time.fixedDeltaTime);
        }

        else if (Vector2.Distance(transform.position, player.position) > maxDist)
        {
            enemy.MovePosition(lastpos += lastpos * moveSpeed * Time.fixedDeltaTime);
        }

        

    }
}
