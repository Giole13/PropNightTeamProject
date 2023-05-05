using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiKillerSkill : MonoBehaviour, IKillerSkill, IKillerEnumverator
{
    public Image killerSkillShortCoolImage = default;
    public Image killerSkillLongCoolImage = default;
    private bool isKillerShortSkillUse = false;
    private bool isKillerLongSkillUse = false;
    private float currentCoolTime;
    private float howKiller;
    private float killerCool;

    void Start()
    {
        switch (DataContainer.KillerSelectNumber)
        {
            case 0:
                // 임포스터
                howKiller = 0f;
                killerCool = 8f;
                break;
            case 1:
                // 아키반
                howKiller = 1f;
                killerCool = 15f;
                break;
            default:
                break;
        }
        killerSkillShortCoolImage.fillAmount = 1f;
    }
    void Update()
    {
        //임포스터면
        if (howKiller == 0)
        {
            KillerShortSkillCool(killerCool);
        }
        else if (howKiller == 1)
        {
            KillerShortSkillCool(killerCool);
        }

    }
    public void KillerShortSkillCool(float cool)
    {
        if (isKillerShortSkillUse == false)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(KillerSkillShortCool(cool));
            }
        }

    }   //KillerFirstSkillCool()

    // public IEnumerator KillerSkillShortCool(float cool)
    // {
    //     isKillerShortSkillUse = true;
    //     if (isKillerShortSkillUse == true)
    //     {
    //         killerSkillShortCoolImage.fillAmount = 0f;
    //         while (killerSkillShortCoolImage.fillAmount < 1f)
    //         {
    //             cool -= 0.01f;
    //             yield return new WaitForSeconds(0.01f);
    //             killerSkillShortCoolImage.fillAmount += 0.01f / cool;
    //         }
    //         isKillerShortSkillUse = false;

    //     }
    // }   //KillerSkillShortCool()

    public IEnumerator KillerSkillShortCool(float cool)
    {
        float CoolTime = cool;
        float Timer = 0;
        isKillerShortSkillUse = true;
        if (isKillerShortSkillUse == true)
        {
            killerSkillShortCoolImage.fillAmount = 0f;
            while (Timer <= CoolTime)
            {
                Timer += 0.01f;
                yield return new WaitForSeconds(0.01f);
                killerSkillShortCoolImage.fillAmount = Timer / CoolTime;
            }
            isKillerShortSkillUse = false;

        }
    }   //KillerSkillShortCool() 
}
