using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public static float maxVolume = .4f;
    public float volumeChangeSpeed;
    public AudioSource currentSong;
    public AudioSource addOnSong;


    private static Music instance;
    public AudioClip nextSong;
    public AudioClip nextAddOn;

    public bool playingAddOn;

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
        if (currentSong.clip == nextSong)
            nextSong = null;
        if(addOnSong.clip == nextAddOn)
            nextAddOn = null;


        if (nextSong == null)
        {
            if (currentSong.volume < maxVolume)
            {
                currentSong.volume += volumeChangeSpeed * Time.deltaTime;
            }
            else
            {
                currentSong.volume = maxVolume;
            }
        }
        else
        {
            if(currentSong.volume > .01f)
            {
                currentSong.volume -= volumeChangeSpeed * Time.deltaTime;
            }
            else
            {
                currentSong.clip = nextSong;
                currentSong.Play();
                nextSong = null;

                addOnSong.clip = nextAddOn;
                addOnSong.Play();
                nextAddOn = null;
            }
        }

        if (nextAddOn == null && playingAddOn)
        {
            if (addOnSong.volume < maxVolume)
            {
                addOnSong.volume += volumeChangeSpeed * Time.deltaTime;
            }
            else
            {
                addOnSong.volume = maxVolume;
            }
        }
        else
        {
            addOnSong.volume -= volumeChangeSpeed * Time.deltaTime;
        }
    }

    public void ChangeSong(AudioClip newSong, AudioClip newAddOn)
    {
        nextSong = newSong;
        nextAddOn = newAddOn;
    }
}
