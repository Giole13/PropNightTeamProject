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
    public PlayerInput playerInput = default;
    public PlayerMovement Player = default;
    public InGameManager inGameManager = default;
    public bool isPlayerSkillUse = false;
    public bool isLeftShift = false;
    public bool isRun = false;

    private int howPlayer = 0;
    private int playerCool = 0;


    private void Start()
    {
        switch (DataContainer.PlayerSelectNumber)
        {
            case 0:
                //러너
                howPlayer = 0;
                playerCool = 10;
                break;
            case 1:
                //힐러
                howPlayer = 0;
                playerCool = 15;
                break;
            case 2:
                //과학자
                howPlayer = 1;
                break;
            case 3:
                //점퍼
                howPlayer = 1;

                break;
            default:
                break;
        }
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
            if (playerInput.Skill)
            {
                if (howPlayer == 0)
                {
                    if (Player.Stamina < 100)
                    {
                        StartCoroutine(playerSkill(playerCool));
                    }
                }
                else if (howPlayer == 1)
                {

                }

            }
        }
        else {/*Do nothing*/ }
    }
    public void AbilityRun()
    {
        if (playerInput.Dash)
        {
            if (1 < Player.Stamina)
            {
                UseStemina();
            }
        }
        if (!playerInput.Dash)
        {
            if (Player.Stamina < 100)
            {
                AddStemina();
            }
        }
    }   //AbilityRun()
    public void UseStemina()
    {
        RunUseStemina();
    }


    public void AddStemina()
    {
        RunAddStemina();
    }
    #endregion

    #region IEnumerator 모음
    public IEnumerator playerSkill(float cool)
    {
        float playerCool = cool;

        playerSkillAbility.SetActive(true);
        isPlayerSkillUse = true;
        playerSkillCoolImage.fillAmount = 1f;
        while (0 < playerSkillCoolImage.fillAmount)
        {
            yield return null;
            //yield return new WaitForSeconds(0.01f);
            playerSkillCoolImage.fillAmount = 1 - Player.CoolTime / playerCool;
            if (playerSkillCoolImage.fillAmount == 1) { break; }
        }
        playerSkillAbility.SetActive(false);
        isPlayerSkillUse = false;
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
        playerSteminaGageBar.fillAmount = (Player.Stamina / 100f);

    }
    public void RunAddStemina()
    {
        playerSkillRun.SetActive(false);

        float steminaHide = 0;

        steminaHide += Time.deltaTime;

        playerSteminaGageBar.fillAmount = (Player.Stamina / 100f);
        if (1f < steminaHide)
        {
            playerRunGagebar.SetActive(false);
        }
        //yield return new WaitForFixedUpdate();
    }



}
