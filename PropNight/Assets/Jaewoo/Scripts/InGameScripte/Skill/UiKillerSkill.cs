using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiKillerSkill : MonoBehaviour, IKillerSkill, IKillerEnumverator
{
    public AkibanAttack akibanAttack = default;
    public ImpostorAttack impostorAttack = default;
    public Image killerSkillShortCoolImage = default;
    public Image killerSkillLongCoolImage = default;
    private bool isKillerShortSkillUse = false;
    private bool isKillerLongSkillUse = false;
    private float currentCoolTime;
    private float howKiller;
    public float killerCool;

    void Start()
    {
        switch (DataContainer.KillerSelectNumber)
        {
            case 0:
                //임포스터
                howKiller = 0;
                killerCool = 8;
                break;
            case 1:
                //아키반
                howKiller = 1;
                killerCool = 15;
                break;
            default:
                break;
        }
        killerSkillShortCoolImage.fillAmount = 1f;
    }
    void Update()
    {
        KillerShortSkillCool(killerCool);
        //임포스터면

    }
    public void KillerShortSkillCool(float cool)
    {

        if (isKillerShortSkillUse == false)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (howKiller == 0)
                {
                    //임포
                    StartCoroutine(KillerSkillCool(cool));

                }
                else if (howKiller == 1)
                {
                    //킬러
                    StartCoroutine(KillerSkillShortCool(cool));
                }
            }
        }

    }   //KillerFirstSkillCool()

    //아키반
    public IEnumerator KillerSkillShortCool(float cool)
    {
        isKillerShortSkillUse = true;
        if (isKillerShortSkillUse == true)
        {
            float killerCool = cool;
            killerSkillShortCoolImage.fillAmount = 1f;

            while (0 < killerSkillShortCoolImage.fillAmount)
            {
                yield return null;
                //yield return new WaitForSeconds(0.01f);
                killerSkillShortCoolImage.fillAmount = 1 - akibanAttack._coolTime / killerCool;
            }
            isKillerShortSkillUse = false;

        }
    }   //KillerSkillShortCool()

    //임포
    public IEnumerator KillerSkillCool(float cool)
    {
        isKillerShortSkillUse = true;
        if (isKillerShortSkillUse == true)
        {
            float killerCool = cool;
            killerSkillShortCoolImage.fillAmount = 1f;

            while (0 < killerSkillShortCoolImage.fillAmount)
            {
                yield return null;
                //yield return new WaitForSeconds(0.01f);
                killerSkillShortCoolImage.fillAmount = 1 - impostorAttack._coolTime / killerCool;
            }
            isKillerShortSkillUse = false;

        }
    }   //KillerSkillShortCool()

}
