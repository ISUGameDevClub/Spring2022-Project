using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveAttackDamage : MonoBehaviour
{
    public Collider2D damageZone;
    public Collider2D safeZone;

    [SerializeField, Tooltip("Damage to be dealt to entity that is hit by this")] float damage = 1f;
    [SerializeField, Tooltip("Mark this if the projectile goes through walls")] bool isPiercing = false;
    [SerializeField, Tooltip("Mark this if the hurtbox never gets destroyed (Melee Enemies)")] bool persisting = false;
    public float duration = 0.3f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (safeZone.IsTouching(collision) == false)
        {
            if (collision.gameObject.tag == "Player" && collision.gameObject.TryGetComponent(out Health health)) //checks that it is not colliding with its creator and what it is colliding with has a Health script
            {
                if (health.IsDead() == false) //only deals damage if the entity is not already dead
                {
                    health.TakeDamage(damage);
                    if (!isPiercing && !persisting)
                    {
                        Destroy(gameObject);
                    }
                    //Debug.Log(collision.gameObject.name + " took " + damage.ToString() + " damage!");
                    //Put any extra methods associated with taking/dealing damage here!!!
                }
            }
            else if (collision.gameObject.tag == "Player" && collision.gameObject.layer == LayerMask.NameToLayer("Walls"))
            {
                if (!persisting)
                    Destroy(gameObject);
            }
        }
    }

    private void Awake()
    {
        StartCoroutine(SelfDestruct(duration));
    }

    IEnumerator SelfDestruct(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
