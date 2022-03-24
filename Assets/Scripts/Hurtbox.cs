using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//IMPORTANT: Make sure that any object with this script attatched has a Collider2D component with "Is Trigger" CHECKED!
public class Hurtbox : MonoBehaviour
{
    public GameObject parent; //GameObject that spawned this Hurtbox (the entity that is attacking)
    public float damage = 1f; //damage to be dealt to entity that is hit by this
    public bool isPiercing = false; //Mark this if the projectile is a piercing projectile

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject != parent && collision.gameObject.TryGetComponent(out Health health)) //checks that it is not colliding with its creator and what it is colliding with has a Health script
        {
            if (health.isDead == false) //only deals damage if the entity is not already dead
            {
                health.TakeDamage(damage);
                if(!isPiercing)
                {
                    Destroy(gameObject);
                }
                //Debug.Log(collision.gameObject.name + " took " + damage.ToString() + " damage!");
                //Put any extra methods associated with taking/dealing damage here!!!
            }
        }
        else if(collision.gameObject != parent && collision.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
