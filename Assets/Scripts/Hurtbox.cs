using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//IMPORTANT: Make sure that any object with this script attatched has a Collider2D component with "Is Trigger" CHECKED!
public class Hurtbox : MonoBehaviour
{
    GameObject parent; //GameObject that spawned this Hurtbox (the entity that is attacking)
    [SerializeField, Tooltip("Decides if the hitbox can hurt the player")] bool playerHitbox = false;
    [SerializeField, Tooltip("Damage to be dealt to entity that is hit by this")] float damage = 1f;
    [SerializeField, Tooltip("Mark this if the projectile goes through walls")] bool isPiercing = false;
    [SerializeField, Tooltip("Mark this if the hurtbox never gets destroyed (Melee Enemies)")] bool persisting = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject != parent && ((collision.gameObject.tag != "Player" && playerHitbox) || (collision.gameObject.tag == "Player" && !playerHitbox)) && collision.gameObject.TryGetComponent(out Health health)) //checks that it is not colliding with its creator and what it is colliding with has a Health script
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
        else if (collision.gameObject != parent && collision.gameObject.layer == LayerMask.NameToLayer("Walls"))
        {
            if (!persisting)
                Destroy(gameObject);
        }
    }
    public void SetParent(GameObject parent)
    {
        if(parent != null)
            this.parent = parent;
    }
    public GameObject GetParent()
    {
        return parent;
    }
}
