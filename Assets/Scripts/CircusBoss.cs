using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircusBoss : MonoBehaviour
{
    Attack currentAttack;
    public GameObject whipAttackPrefab;

    public int whipAttacksPerWarp = 3;
    private int attacksSpawned = 0; //counts both whip and warp calls

    public bool onRight = false; //false if on left, true if on right

    public float roomDistanceFromCenter = 30f; //how far should the boss move to the left or right from the center of the room?

    Transform tf;

    public float minTimeBetweenAttack = 0.8f;
    public float maxTimeBetweenAttack = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        currentAttack = GetComponent<Attack>();
        currentAttack.hurtboxPrefab = whipAttackPrefab;
        tf = GetComponent<Transform>();
        StartCoroutine(EnterRoom(0f));
    }

    void FixedUpdate()
    {
        if(currentAttack.canAttack)
        {
            attacksSpawned++;
            if(attacksSpawned % (whipAttacksPerWarp + 1) == 0)
            {
                Warp();
            }
            else
            {
                WhipAttack();
            }
        }
    }

    void WhipAttack()
    {
        currentAttack.cooldownTime = Random.Range(minTimeBetweenAttack, maxTimeBetweenAttack);
        currentAttack.SpawnAttack();
        Debug.Log("I just simultaneously whipped and nae-nae'd! " + attacksSpawned);
    }

    void Warp()
    {
        if(onRight)
        {
            Vector2 newDistance = new Vector3(tf.position.x - roomDistanceFromCenter * 2, tf.position.y);
            tf.position = newDistance;
            onRight = false;
        }
        else
        {
            Vector2 newDistance = new Vector3(tf.position.x + roomDistanceFromCenter * 2, tf.position.y);
            tf.position = newDistance;
            onRight = true;
        }
    }

    public IEnumerator EnterRoom(float animationLength)
    {
        yield return new WaitForSeconds(animationLength);
        StartFight();
    }

    void StartFight()
    {
        if(FindObjectOfType<PlayerMovement>().gameObject.transform.position.x < transform.position.x)
        {
            Vector2 newDistance = new Vector3(tf.position.x + roomDistanceFromCenter, tf.position.y);
            tf.position = newDistance;
            onRight = true;
        }
        else
        {
            Vector2 newDistance = new Vector3(tf.position.x - roomDistanceFromCenter, tf.position.y);
            tf.position = newDistance;
            onRight = false;
        }
    }
}
