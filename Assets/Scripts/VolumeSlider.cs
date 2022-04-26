using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeSlider : MonoBehaviour
{
    public AudioMixer mixer;
    public void ChangeVolume(float value)
    {
        mixer.SetFloat("volume", value);
    }

    public void ChangeMusicVolume(float value)
    {
        Music.maxVolume = value;
    }
}
