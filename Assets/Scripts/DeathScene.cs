using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class DeathScene : MonoBehaviour
{
    public void Setup()
    {

    }
    public void RetryButton()
    {
        SceneManager.LoadScene("Final Forrest");
    }
    public void QuitButton()
    {
        SceneManager.LoadScene("Title");
    }
}
