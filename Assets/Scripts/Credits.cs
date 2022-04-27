using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    [SerializeField] float sceneTime = 5f;
    [SerializeField] Animator anim;
    [SerializeField] int newScene;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeOut());
    }
    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(sceneTime);
        anim.SetTrigger("End Scene");
    }
    public void SceneChangeEnd()
    {
        SceneManager.LoadScene(newScene);
    }
}
