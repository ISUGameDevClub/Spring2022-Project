using System.Collections;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    [SerializeField] float speed;
    Rigidbody2D projectileRB;
    // Start is called before the first frame update
    void Start()
    {
        projectileRB = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        
    }
    private void FixedUpdate()
    {
        projectileRB.MovePosition((Vector2)transform.position + ((Vector2)transform.right * Time.fixedDeltaTime * speed));
    }
}