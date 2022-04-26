using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    public string newLevel;
    bool touchingPlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(touchingPlayer && Input.GetKeyDown(KeyCode.E) && (FindObjectOfType<IntroManager>().phase == 5 || !FindObjectOfType<IntroManager>()))
        {
            FindObjectOfType<SceneTransitions>().ChangeScene(newLevel);
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
