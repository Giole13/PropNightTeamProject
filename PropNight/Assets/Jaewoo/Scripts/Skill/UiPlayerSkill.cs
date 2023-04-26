using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiPlayerSkill : MonoBehaviour, IPlayerSkill, IPlayerEnumerator

{
    public Image playerSkillCoolImage = default;
    public Image playerSteminaGageBar = default;
    public GameObject playerSkillRun = default;
    public GameObject playerSkillAbility = default;
    public GameObject playerRunGagebar = default;

    public bool isPlayerSkillUse = false;
    public bool isLeftShift = false;

    public int fillAmountAbility = 0;
    public int currentCool = 0;
    public float fillAmountStemina = 100;

    private void Start()
    {
        playerSkillRun.SetActive(false);
        playerSkillAbility.SetActive(false);
        playerRunGagebar.SetActive(false);
    }

    void Update()
    {
        AbilitySkill();
        AbilityRun();
        //AddStemina();
    }

    #region 플레이어스킬
    public void AbilitySkill()
    {
        if (isPlayerSkillUse == false)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(playerSkill(5f));
            }
        }
        else { }
    }
    public void AbilityRun()
    {
        if (0 < fillAmountStemina)
        {
            UseStemina();
        }
        if (fillAmountStemina < 0)
        {
            AddStemina();
        }
    }   //AbilityRun()
    public void UseStemina()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log(Input.GetKeyDown(KeyCode.LeftShift));

            StartCoroutine(RunUseStemina());
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Debug.Log(Input.GetKeyUp(KeyCode.LeftShift));
            AddStemina();
        }
    }
    public void AddStemina()
    {

        StartCoroutine(RunAddStemina());

    }
    #endregion

    #region IEnumerator 모음
    public IEnumerator playerSkill(float cool)
    {
        playerSkillAbility.SetActive(true);
        isPlayerSkillUse = true;
        while (1.0f < cool)
        {
            cool -= Time.deltaTime;
            playerSkillCoolImage.fillAmount = (1f / cool);
            yield return new WaitForFixedUpdate();
        }
        isPlayerSkillUse = false;
        Debug.Log(isPlayerSkillUse);
        playerSkillAbility.SetActive(false);
    }  // playerSkill()
    #endregion
    public void playerSkillCool()
    {
        isPlayerSkillUse = true;
    }   //playerSkillCool()

    IEnumerator RunUseStemina()
    {
        playerRunGagebar.SetActive(true);
        while (0 < fillAmountStemina)
        {
            fillAmountStemina -= Time.deltaTime * 5f;
            playerSteminaGageBar.fillAmount = (fillAmountStemina / 100f);
            yield return new WaitForFixedUpdate();
        }
    }
    IEnumerator RunAddStemina()
    {
        while (fillAmountStemina < 100)
        {
            fillAmountStemina += Time.deltaTime * 5f;
            playerSteminaGageBar.fillAmount = (fillAmountStemina / 100f);
            yield return new WaitForFixedUpdate();
        }
    }


}
