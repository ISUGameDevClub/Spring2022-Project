using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon
{

  public string name;
  public float baseDamage;
  public float currDamage;
  public string description;

  void Start()
  {
    baseDamage = currDamage;
  }

}
