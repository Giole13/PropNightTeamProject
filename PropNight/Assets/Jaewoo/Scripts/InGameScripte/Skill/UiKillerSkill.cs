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

    void Start()
    {
        killerSkillShortCoolImage.fillAmount = 1f;
    }
    void Update()
    {
        KillerShortSkillCool();
    }
    public void KillerShortSkillCool()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(KillerSkillShortCool(8f));
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
                isKillerShortSkillUse = false;
            }

        }
    }   //KillerSkillShortCool()

}
