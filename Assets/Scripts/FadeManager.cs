using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
  public Animator fades;
  private GameObject FadeBlock;

    // Start is called before the first frame update
    void Start()
    {
      fades.SetBool("FadeOut", false);
    }

    // Update is called once per frame
    void Update()
    {
      if(Input.GetKeyDown(KeyCode.F))
      {
        FadeOut();
      }
    }
    public void FadeOut()
    {
      fades.SetBool("FadeOut", true);
    }
}
