using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwap : MonoBehaviour
{
  private bool swapped;
  private bool pressed;
  private float timer;
    // Start is called before the first frame update
    void Start()
    {
      timer = 0f;
      swapped = false;
      if(this.gameObject.name == "Box1")
      {
        transform.localPosition = new Vector3(350, 85, 0);
      }
      if(this.gameObject.name == "Box2")
      {
        transform.localPosition = new Vector3(350, -15, 0);
      }
    }

    // Update is called once per frame
    void Update()
    {
      if(timer >= 0)
      {
        timer = timer - Time.deltaTime;
      }
        if(this.gameObject.name == "Box1" && Input.GetKeyDown(KeyCode.RightShift) && timer <= 0 && swapped == false)
        {
          transform.localPosition = new Vector3(350, -15, 0);
          swapped = true;
          timer = 1;
        }
        if(this.gameObject.name == "Box1" && Input.GetKeyDown(KeyCode.RightShift) && timer <= 0 && swapped == true)
        {
          transform.localPosition = new Vector3(350, 85, 0);
          swapped = false;
          timer = 1;
        }
        if(this.gameObject.name == "Box2" && Input.GetKeyDown(KeyCode.RightShift) && timer <= 0 && swapped == false)
        {
          transform.localPosition = new Vector3(350, 85, 0);
          swapped = true;
          timer = 1;
        }
        if(this.gameObject.name == "Box2" && Input.GetKeyDown(KeyCode.RightShift) && timer <= 0 && swapped == true)
        {
          transform.localPosition = new Vector3(350, -15, 0);
          swapped = false;
          timer = 1;
        }

    }
}
