using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
    Slider slide;
    private void Start()
    {
        slide = GetComponent<Slider>();
        slide.gameObject.SetActive(false);
    }
    public void ChangeHealth(float percent)
    {
        if (!slide.gameObject.activeSelf)
        {
            slide.gameObject.SetActive(true);
        }
        slide.value = percent;
    }
}
