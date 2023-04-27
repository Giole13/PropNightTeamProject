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
    public bool isRun = false;

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
        else {/*Do nothing*/ }
    }
    public void AbilityRun()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (1 < fillAmountStemina)
            {
                isRun = true;
                UseStemina();
            }
            else { }
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRun = false;
            if (fillAmountStemina < 100)
            {
                AddStemina();
            }

        }

    }   //AbilityRun()
    public void UseStemina()
    {
        if (isRun == true)
        {
            RunUseStemina();
        }
    }


    public void AddStemina()
    {
        if (isRun)
        {
            RunAddStemina();
        }
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
        playerSkillAbility.SetActive(false);
    }  // playerSkill()
    #endregion
    public void playerSkillCool()
    {
        isPlayerSkillUse = true;
    }   //playerSkillCool()

    public void RunUseStemina()
    {
        playerSkillRun.SetActive(true);
        playerRunGagebar.SetActive(true);
        while (0 < fillAmountStemina && isRun == true)
        {
            playerSteminaGageBar.fillAmount = (fillAmountStemina / 100f);
            // yield return new WaitForFixedUpdate();
        }
    }
    public void RunAddStemina()
    {
        playerSkillRun.SetActive(false);
        float steminaHide = 0;
        while (fillAmountStemina < 100)
        {
            steminaHide += Time.deltaTime;
            if (1f < steminaHide)
            {
                playerRunGagebar.SetActive(false);
            }
            fillAmountStemina += Time.deltaTime * 5f;
            playerSteminaGageBar.fillAmount = (fillAmountStemina / 100f);
            //yield return new WaitForFixedUpdate();
        }
    }



}
