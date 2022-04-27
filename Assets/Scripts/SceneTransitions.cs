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

    public void ChangeScene(string scene, AudioClip newSong, AudioClip newAddOn)
    {
        if(scene == "Title")
        {
            PassiveBuffs.ResetStats();
        }

        if(FindObjectOfType<Music>())
        {
            FindObjectOfType<Music>().ChangeSong(newSong, newAddOn);
        }
        newScene = scene;
        sceneTransitionAnim.SetTrigger("End Scene");
    }

    public void ChangeScene2(string scene)
    {
        newScene = scene;
        sceneTransitionAnim.SetTrigger("End Scene 2");
    }

    public void SceneChangeEnd()
    {
        SceneManager.LoadScene(newScene);
    }
}
