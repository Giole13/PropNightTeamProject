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

    void Update()
    {
        KillerShortSkillCool();
        KillerLongSkillLongCool();
    }
    public void KillerShortSkillCool()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

        }
    }   //KillerFirstSkillCool()
    public void KillerLongSkillLongCool()
    {
        if (Input.GetMouseButtonDown(1))
        {

        }
    }
    public IEnumerator KillerSkillShortCool(float cool)
    {
        if (isKillerShortSkillUse == true)
        {
            while (1.0f < cool)
            {
                cool -= Time.deltaTime;
                killerSkillShortCoolImage.fillAmount = (1f / cool);
                yield return new WaitForFixedUpdate();
            }
        }
    }   //KillerSkillShortCool()
    public IEnumerator KillerSkillLongCool(float cool)
    {
        if (isKillerLongSkillUse == true)
        {
            while (1.0f < cool)
            {
                cool -= Time.deltaTime;
                killerSkillLongCoolImage.fillAmount = (1f / cool);
                yield return new WaitForFixedUpdate();
            }
        }
    }   //KillerSkillLongCool()
}
