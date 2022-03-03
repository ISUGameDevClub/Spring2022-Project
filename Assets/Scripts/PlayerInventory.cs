using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
  [SerializeField] private Weapon wp1;
  [SerializeField] private Weapon wp2;
  [SerializeField] private int activeSlot;

    // Start is called before the first frame update
    void Start()
    {
      activeSlot = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ChangeActiveSlot(int s)
    {
        activeSlot = s;
    }
    public void WeaponSwap(Weapon nW)
    {
      if(activeSlot == 1)
      {
        wp1 = nW;
      }
      else
      {
        wp2 = nW;
      }

    }
    public int ReturnActiveSlot()
    {
      return activeSlot;
    }
}
