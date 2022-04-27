using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] GameObject optionsScreen;
    [SerializeField] Scene playScene;
    public AudioSource buttonSound;
    SceneTransitions sceneTrans;
    public AudioClip BedroomTheme;
    public AudioClip TitleTheme;
    
    // Start is called before the first frame update
    void Start()
    {
        sceneTrans = FindObjectOfType<SceneTransitions>();
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
        sceneTrans.ChangeScene("Bedroom", BedroomTheme, null);
    }

    public void Credits()
    {
        buttonSound.Play();
        sceneTrans.ChangeScene("Credits", TitleTheme, null);
    }

    public void ReturnToTitle()
    {
        buttonSound.Play();
        sceneTrans.ChangeScene("Title", TitleTheme, null);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
