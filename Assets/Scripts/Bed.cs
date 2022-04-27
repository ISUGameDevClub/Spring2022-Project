using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    public string newLevel;
    bool touchingPlayer;
    public bool intro;
    public bool insta;

    public AudioClip nextSong;
    public AudioClip nextAddOn;

    // Start is called before the first frame update
    void Start()
    {
        if(insta)
        {
            GetComponent<Animator>().SetTrigger("Insta");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(touchingPlayer && Input.GetKeyDown(KeyCode.E) && (!intro || (FindObjectOfType<IntroManager>().phase == 5 || !FindObjectOfType<IntroManager>())))
        {
            FindObjectOfType<SceneTransitions>().ChangeScene(newLevel, nextSong, nextAddOn);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            touchingPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            touchingPlayer = false;
        }
    }
}
