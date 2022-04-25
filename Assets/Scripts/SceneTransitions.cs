using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    private Animator sceneTransitionAnim;
    private string newScene;

    void Start()
    {
        sceneTransitionAnim = GetComponent<Animator>();
        newScene = "";
    }

    public void ChangeScene(string scene)
    {
        if(scene == "Final Forest")
        {

        }

        newScene = scene;
        sceneTransitionAnim.SetTrigger("End Scene");
    }

    public void SceneChangeEnd()
    {
        SceneManager.LoadScene(newScene);
    }
}
