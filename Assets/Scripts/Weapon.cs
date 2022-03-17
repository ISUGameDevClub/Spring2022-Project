using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon
{
  public Weapon(string name, float baseDamage,float currDamage, string description)
  {
    this.name = name;
    this.baseDamage = baseDamage;
    this.currDamage = currDamage;
    this.description = description;
  }

  public string name;
  public float baseDamage;
  public float currDamage;
  public string description;

  void Start()
  {
    baseDamage = currDamage;
  }

}
