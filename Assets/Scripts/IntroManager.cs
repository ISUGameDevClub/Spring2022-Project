using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    public GameObject[] weapons;
    public GameObject wasdText;
    public GameObject ItemText;
    public GameObject battle1Text;
    public GameObject battle2Text;
    public GameObject dashText;
    public GameObject sleepText;

    [HideInInspector]
    public int phase;

    // Start is called before the first frame update
    void Start()
    {
        phase = 0;
        wasdText.GetComponent<Animator>().SetTrigger("Show");
    }

    // Update is called once per frame
    void Update()
    {
        switch(phase)
        {
            case 0:
                if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                {
                    wasdText.GetComponent<Animator>().SetTrigger("Hide");
                    ItemText.GetComponent<Animator>().SetTrigger("Show");
                    phase = 1;
                }
                break;
            case 1:
                break;
            case 2:
                if (Input.GetButton("Fire1"))
                {
                    battle1Text.GetComponent<Animator>().SetTrigger("Hide");
                    battle2Text.GetComponent<Animator>().SetTrigger("Show");
                    phase = 3;
                }
                break;
            case 3:
                if (Input.GetButton("Fire2"))
                {
                    battle2Text.GetComponent<Animator>().SetTrigger("Hide");
                    dashText.GetComponent<Animator>().SetTrigger("Show");
                    phase = 4;
                }
                break;
            case 4:
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    dashText.GetComponent<Animator>().SetTrigger("Hide");
                    sleepText.GetComponent<Animator>().SetTrigger("Show");
                    phase = 5;
                }
                break;
            default:
                break;
        }
    }
}
