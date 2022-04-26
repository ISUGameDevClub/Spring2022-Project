using System.Collections;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    [SerializeField] float speed;
    Rigidbody2D projectileRB;
    public float cooldownTime = 0.2f; //Time until the Player can attack again after this

    public float activeTime = 1f; //Time that a projectile is active until it is destroyed
    public float spawnDistance = 0;

    // Start is called before the first frame update
    void Start()
    {
        projectileRB = GetComponent<Rigidbody2D>();
        transform.localPosition = (Vector2)transform.position + ((Vector2)transform.right * spawnDistance);
    }

    private void Awake()
    {
        StartCoroutine(SelfDestruct(activeTime));
    }

    private void Update()
    {
        
    }
    private void FixedUpdate()
    {
        projectileRB.MovePosition((Vector2)transform.position + ((Vector2)transform.right * Time.fixedDeltaTime * speed));
    }
    IEnumerator SelfDestruct(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}