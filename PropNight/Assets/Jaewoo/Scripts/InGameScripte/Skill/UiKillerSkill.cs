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
                //러너
                howKiller = 0;
                killerCool = 8;
                break;
            case 1:
                //힐러
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

    public IEnumerator KillerSkillShortCool(float cool)
    {
        isKillerShortSkillUse = true;
        if (isKillerShortSkillUse == true)
        {
            killerSkillShortCoolImage.fillAmount = 0f;
            while (killerSkillShortCoolImage.fillAmount < 1f)
            {

                cool -= Time.smoothDeltaTime;
                killerSkillShortCoolImage.fillAmount += 1 * Time.smoothDeltaTime / cool;
                yield return null;

            }
            isKillerShortSkillUse = false;

        }
    }   //KillerSkillShortCool()

}
