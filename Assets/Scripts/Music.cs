using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    private static Music instance;

    public AudioSource TitleMusic;
    public AudioSource gameMusic;

    public bool playingGameMusic;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        if (!playingGameMusic)
        {
            if (TitleMusic.isPlaying == false)
                TitleMusic.Play();
            if (TitleMusic.volume < .2f)
                TitleMusic.volume += Time.deltaTime * .5f;
            else if(TitleMusic.volume > .2f)
                TitleMusic.volume = .2f;

            if (gameMusic.volume > 0)
                gameMusic.volume -= Time.deltaTime * .5f;
            else if (gameMusic.volume <= 0)
                gameMusic.Stop();
        }
        else
        {
            if (gameMusic.isPlaying == false)
                gameMusic.Play();
            if (gameMusic.volume < .2f)
                gameMusic.volume += Time.deltaTime * .5f;
            else if (gameMusic.volume > .2f)
                gameMusic.volume = .2f;

            if (TitleMusic.volume > 0)
                TitleMusic.volume -= Time.deltaTime * .5f;
            else if (TitleMusic.volume <= 0)
                TitleMusic.Stop();
        }
    }
}
