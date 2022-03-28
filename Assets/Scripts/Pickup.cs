using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
  [SerializeField] private Weapon me = new Weapon("Basic", 0f, 0f, "This is a filler");
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerStay(Collider other)
    {
      if(other.gameObject.CompareTag("Player"))
      {
        if(Input.GetKeyDown(KeyCode.E))
        {
          FindObjectOfType<PlayerInventory>().WeaponSwap(me);
          Destroy(this.gameObject);
        }
      }
    }
}
