using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpController : MonoBehaviour
{

    public PlayerMovement playerMovement = default;


    public float numOfHeart;
    public float maxHealth;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    void Start()
    {

        maxHealth = 100;
        //하트 ui 개수
        numOfHeart = (maxHealth / 20);
    }
    // Update is called once per frame
    void Update()
    {
        if (numOfHeart < maxHealth)
        {
            maxHealth = numOfHeart;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < maxHealth)
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

        if (80 <= playerMovement.HP)
        {
            hearts[4].fillAmount = (playerMovement.HP - 80) / 20f;
            hearts[3].fillAmount = 1;
            hearts[2].fillAmount = 1;
            hearts[1].fillAmount = 1;
            hearts[0].fillAmount = 1;
        }
        if (60 <= playerMovement.HP && playerMovement.HP < 80)
        {
            hearts[3].fillAmount = (playerMovement.HP - 60) / 20f;
            hearts[2].fillAmount = 1;
            hearts[1].fillAmount = 1;
            hearts[0].fillAmount = 1;
        }
        if (40 <= playerMovement.HP && playerMovement.HP < 60)
        {
            hearts[2].fillAmount = (playerMovement.HP - 40) / 20f;
            hearts[1].fillAmount = 1;
            hearts[0].fillAmount = 1;
        }
        if (20 <= playerMovement.HP && playerMovement.HP < 40)
        {
            hearts[1].fillAmount = (playerMovement.HP - 20) / 20f;

            hearts[0].fillAmount = 1;
        }
        if (0 <= playerMovement.HP && playerMovement.HP < 20)
        {
            hearts[0].fillAmount = (playerMovement.HP / 20f);
        }
        if (playerMovement.HP < 0)
        {
            hearts[0].fillAmount = 0;
        }

        //hp 구간 당 없애기
        if (playerMovement.HP < 80)
        {
            hearts[4].fillAmount = 0f;
        }
        if (playerMovement.HP < 60)
        {
            hearts[3].fillAmount = 0f;
        }
        if (playerMovement.HP < 40)
        {
            hearts[2].fillAmount = 0f;
        }
        if (playerMovement.HP < 20)
        {
            hearts[1].fillAmount = 0f;
        }



    }
}
