using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiSkill : MonoBehaviour, IPlayerSkill, IPlayerEnumerator

{
    public Image playerSkillCoolImage = default;
    public Image killerSkillShortCoolImage = default;
    public Image killerSkillLongCoolImage = default;

    public GameObject playerSkillRun = default;
    public GameObject playerSkillAbility = default;
    private bool isPlayerSkillUse = false;
    private bool isKillerShortSkillUse = false;
    private bool isKillerLongSkillUse = false;
    public int fillAmount = 0;
    public int currentCool = 0;
    private void Start()
    {
        playerSkillRun.SetActive(false);
        playerSkillAbility.SetActive(false);
    }

    void Update()
    {
        AbilitySkill();
        AbilityRun();
    }

    #region 플레이어스킬
    public void AbilitySkill()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isPlayerSkillUse = true;

            StartCoroutine(playerSkill(5f));
        }
    }
    public void AbilityRun()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerSkillRun.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            playerSkillRun.SetActive(false);
        }
    }   //AbilityRun()
    #endregion

    #region IEnumerator 모음
    public IEnumerator playerSkill(float cool)
    {
        playerSkillAbility.SetActive(true);
        if (isPlayerSkillUse == true)
        {
            playerSkillCool();

            while (1.0f < cool)
            {
                cool -= Time.deltaTime;
                playerSkillCoolImage.fillAmount = (1f / cool);
                yield return new WaitForFixedUpdate();
            }
        }
        playerSkillAbility.SetActive(false);
    }  // playerSkill()

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
    #endregion
    public void playerSkillCool()
    {
        isPlayerSkillUse = false;
    }   //playerSkillCool()
    public void KillerFirstSkillCool()
    {

    }   //KillerFirstSkillCool()
    public void KillerSecondSkillLongCool()
    {

    }   //KillerSecondSkillLongCool()

    public void RunSkill()
    {

    }
}
