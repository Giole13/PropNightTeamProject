using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpController : MonoBehaviour
{

    public int health = 100;
    public int playerHp = 100;
    public int numOfHeart;
    public int maxHealth;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    void Awake()
    {
        health = 100;
        maxHealth = 100;
        //하트 ui 개수
        numOfHeart = (health / 20);
    }
    // Update is called once per frame
    void Update()
    {
        if (numOfHeart < health)
        {
            health = numOfHeart;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numOfHeart)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

        if (80 <= playerHp)
        {
            hearts[4].fillAmount = (playerHp - 80) / 20f;
        }
        if (60 <= playerHp && playerHp < 80)
        {
            hearts[3].fillAmount = (playerHp - 60) / 20f;
        }
        if (40 <= playerHp && playerHp < 60)
        {
            hearts[2].fillAmount = (playerHp - 40) / 20f;
        }
        if (20 <= playerHp && playerHp < 40)
        {
            hearts[1].fillAmount = (playerHp - 20) / 20f;
        }
        if (0 <= playerHp && playerHp < 20)
        {
            hearts[0].fillAmount = (playerHp / 20f);
        }

        //hp 구간 당 없애기
        if (playerHp < 80)
        {
            hearts[4].fillAmount = 0f;
        }
        if (playerHp < 60)
        {
            hearts[3].fillAmount = 0f;
        }
        if (playerHp < 40)
        {
            hearts[2].fillAmount = 0f;
        }
        if (playerHp < 20)
        {
            hearts[1].fillAmount = 0f;
        }



    }
}
