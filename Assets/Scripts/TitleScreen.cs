using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] GameObject optionsScreen;
    [SerializeField] Scene playScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void enableOptions()
    {
        optionsScreen.SetActive(true);
    }

    public void disableOptions()
    {
        optionsScreen.SetActive(false);
    }

    public void playGame()
    {
        SceneManager.LoadScene(2);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
