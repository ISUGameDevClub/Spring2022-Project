using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CircusBoss : MonoBehaviour
{
    Attack currentAttack;
    public GameObject whipAttackPrefab;
    public GameObject warpPrefab;
    public GameObject lion;
    public GameObject smoke;


    public int whipAttacksPerWarp = 3; //how many times the boss will whip before warping
    private int attacksSpawned = 0; //counts both whip and warp calls

    private bool onRight = false; //false if on left, true if on right
    private bool stopped = false; //whether the boss is moving
    private bool attacking = false; //whether the boss is in the process of doing something
    public float movementSpeed = 2f;

    public float roomDistanceFromCenter = 10f; //how far should the boss move to the left or right from the center of the room?
    public float roomHeightFromCenter = 3f; //how far should the boss move up or down from the center of the room?
    public float whipDistFromBoss = 2f; //how far away from the boss does the whip attack spawn?

    public Animator anim;
    public SpriteRenderer sR;

    Transform tf;
    Rigidbody2D rb;

    public float minTimeBetweenAttack = 0.8f;
    public float maxTimeBetweenAttack = 1.5f;
    public float whipPauseTime = 0.1f;
    public float warpTime = 1f;

    private float movementTime = 0f; //time that only increases while the boss is not attacking, used for movement

    // Start is called before the first frame update
    void Start()
    {
        currentAttack = GetComponent<Attack>();
        tf = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (onRight)
            sR.flipX = true;
        else
            sR.flipX = false;

        if (GetComponent<EnemyMovement>().canAggro){
            if (currentAttack.canAttack && !attacking)
            {
                attacksSpawned++;
                if (attacksSpawned % (whipAttacksPerWarp + 1) == 0)
                {
                    StartCoroutine(StartWarp());
                }
                else
                {
                    StartCoroutine(StartWhip());
                }

            }
            else if (!stopped)
            {
                anim.SetBool("Walking", true);
                movementTime += Time.fixedDeltaTime;
                Vector3 pos = tf.localPosition;
                float movementY = Mathf.Sin(movementTime * movementSpeed);
                tf.localPosition = new Vector3(pos.x, 8.5f+ movementY * roomHeightFromCenter, pos.z);
            }
            else
            {
                return;
            }
        }
    }

    void WhipAttack()
    {
        currentAttack.hurtboxPrefab = whipAttackPrefab;
        whipAttackPrefab.GetComponent<ProjectileBehavior>().cooldownTime = Random.Range(minTimeBetweenAttack, maxTimeBetweenAttack);
        if (onRight)
        {
            currentAttack.attackSpawn = new GameObject();
            currentAttack.attackSpawn.transform.localPosition = transform.localPosition;
            currentAttack.attackSpawn.transform.localPosition -= new Vector3(whipDistFromBoss, 0, 0);
        }
        else
        {
            currentAttack.attackSpawn = new GameObject();
            currentAttack.attackSpawn.transform.localPosition = transform.localPosition;
            currentAttack.attackSpawn.transform.localPosition += new Vector3(whipDistFromBoss, 0, 0);
        }
        currentAttack.SpawnAttack();
        Destroy(currentAttack.attackSpawn);
        currentAttack.attackSpawn = gameObject;
        //Debug.Log("I just simultaneously whipped and nae-nae'd! " + attacksSpawned);
    }

    void Warp()
    {
        currentAttack.hurtboxPrefab = warpPrefab;
        warpPrefab.GetComponent<ProjectileBehavior>().cooldownTime = Random.Range(warpTime + minTimeBetweenAttack, warpTime + maxTimeBetweenAttack);
        currentAttack.SpawnAttack();
        if (onRight)
        {
            Vector2 newDistance = new Vector3(tf.localPosition.x - roomDistanceFromCenter * 2, tf.localPosition.y);
            tf.localPosition = newDistance;
            onRight = false;
        }
        else
        {
            Vector2 newDistance = new Vector3(tf.localPosition.x + roomDistanceFromCenter * 2, tf.localPosition.y);
            tf.localPosition = newDistance;
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
            tf.position = new Vector3(tf.position.x + roomDistanceFromCenter, tf.position.y);
            onRight = true;
        }
        else
        {
            tf.position = new Vector3(tf.position.x - roomDistanceFromCenter, tf.position.y);
            onRight = false;
        }
    }

    IEnumerator Pause(float duration)
    {
        yield return new WaitForSeconds(duration);
        stopped = false;
    }

    IEnumerator StartWhip()
    {
        anim.SetBool("Walking", false);
        anim.SetBool("Attacking", true);
        attacking = true;
        stopped = true;
        yield return new WaitForSeconds(whipPauseTime);
        WhipAttack();
        StartCoroutine(Pause(whipPauseTime));
        attacking = false;
        anim.SetBool("Attacking", false);
    }
    IEnumerator StartWarp()
    {
        anim.SetBool("Walking", false);
        anim.SetBool("Warping", true);
        attacking = true;
        stopped = true;
        yield return new WaitForSeconds(warpTime);
        Vector3 pos = tf.position;
        GameObject liono = Instantiate(lion, pos, new Quaternion());
        liono.GetComponent<EnemyMovement>().AllowAggroStart();
        Instantiate(smoke, pos, new Quaternion());
        Warp();
        StartCoroutine(Pause(warpTime));
        attacking = false;
        anim.SetBool("Warping", false);
    }
}
