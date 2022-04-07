using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private int health = 10;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    private void Start()
    {
        ChangeHealth(health);
    }

    public void ChangeHealth(int newHealth)
    {
        health = newHealth;
        for (int i=0; i<hearts.Length; i++)
        {
            if (i < (int)health / 2)
                hearts[i].sprite = fullHeart;
            else if (i * 2 == health - 1 && health != 0)
                hearts[i].sprite = halfHeart;
            else
                hearts[i].sprite = emptyHeart;
        }
    }
}
