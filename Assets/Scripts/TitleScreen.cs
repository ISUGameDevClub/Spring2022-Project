using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] GameObject optionsScreen;
    [SerializeField] Scene playScene;
    AudioSource buttonSound;
    public Music music;
    
    // Start is called before the first frame update
    void Start()
    {
        buttonSound = gameObject.GetComponent<AudioSource>();
        music.playingGameMusic = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void enableOptions()
    {
        buttonSound.Play();
        optionsScreen.SetActive(true);
    }

    public void disableOptions()
    {
        buttonSound.Play();
        optionsScreen.SetActive(false);
    }

    public void playGame()
    {
        buttonSound.Play();
        music.playingGameMusic = true;
        SceneManager.LoadScene("Final Forest");
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
