using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilScript : MonoBehaviour
{
    public float speed;
    private GameObject player;
    private Animator anim;
    public GameObject[] bulletSpawns;
    public GameObject bullet;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        anim = GetComponent<Animator>();
        StartCoroutine(BossPattern());
    }

    void Update()
    {
        if(player.transform.position.x > transform.position.x + 1)
        {
            transform.position = transform.position + new Vector3(speed * Time.deltaTime, 0, 0);
        }
        else if (player.transform.position.x < transform.position.x - 1)
        {
            transform.position = transform.position + new Vector3(-speed * Time.deltaTime, 0, 0);
        }

        if(bulletSpawns[0] == null)
        {
            Destroy(bulletSpawns[1]);
            Destroy(bulletSpawns[2]);
        }
    }

    private IEnumerator BossPattern()
    {
        yield return new WaitForSeconds(2.5f);

        while (true && bulletSpawns[0] != null)
        {
            anim.SetTrigger("Left Attack");
            yield return new WaitForSeconds(2.5f);
            StartCoroutine(SpawnBullets());
            yield return new WaitForSeconds(3.5f);
            anim.SetTrigger("Right Attack");
            yield return new WaitForSeconds(2.5f);
            StartCoroutine(SpawnBullets());
            yield return new WaitForSeconds(3.5f);
        }
    }

    private IEnumerator SpawnBullets()
    {
        for (int j = 0; j < 6; j++)
        {
            for (int i = 0; i < bulletSpawns.Length; i++)
            {
                if (bulletSpawns[i] != null && bulletSpawns[0] != null)
                    Instantiate(bullet, bulletSpawns[i].transform.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(.25f);
        }
    }
}
