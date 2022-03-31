using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] GameObject optionsScreen;
    [SerializeField] Scene playScene;
    [SerializeField] AudioSource menuSound;
    // Start is called before the first frame update
    void Start()
    {
        menuSound = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void enableOptions()
    {
        menuSound.Play();
        optionsScreen.SetActive(true);
    }

    public void disableOptions()
    {
        menuSound.Play();
        optionsScreen.SetActive(false);
    }

    public void playGame()
    {
        menuSound.Play();
        SceneManager.LoadScene(2);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
