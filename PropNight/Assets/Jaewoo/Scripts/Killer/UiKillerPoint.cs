using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UiKillerPoint : MonoBehaviour
{
    public AkibanPlayerHoldSit akibanPlayerHoldSit;
    public AkibanAttack akibanAttack;
    public ImpostorPlayerHoldSit impostorPlayerHoldSit;
    public ImpostorAttack impostorAttack;
    public Image mouseImage = default;
    public GameObject killerPointer = default;
    public GameObject killerAttackPointer = default;
    public TMP_Text pointerText = default;
    Image attackImage = default;
    public Sprite[] mouseImageIcon = new Sprite[2];

    private float fadeIn = default;
    private float fadeOut = default;



    Vector3 fallDownVector3 = new Vector3(0, -350, 0);
    Vector3 caughtlVector3 = new Vector3(0, -200, 0);
    Vector3 chairVector3 = new Vector3(100, 20, 0);

    void Start()
    {
        //0 = 좌클릭, 1 = 우클릭
        mouseImageIcon[0] = Resources.Load<Sprite>("UiIcon/MouseIcon-removebg-preview");
        mouseImageIcon[1] = Resources.Load<Sprite>("UiIcon/Mious-removebg-preview");
        killerPointer.SetActive(false);
        attackImage = killerAttackPointer.GetComponent<Image>();

        attackImage.color = new Color(1, 1, 1, 0);

    }
    void Update()
    {
        WhatKiller();
    }
    public void WhatKiller()
    {
        if (impostorPlayerHoldSit == null)
        {
            AkibanPointer();
        }
        if (akibanPlayerHoldSit == null)
        {
            ImpostorPointer();
        }
    }

    public void AkibanPointer()
    {
        if (akibanPlayerHoldSit.IsAkibanPlayerDownCheck == true)//playerMovement.Status == PlayerStatus.FALLDOWN)
        {
            killerPointer.SetActive(true);
            pointerText.text = "들기";
            killerPointer.transform.localPosition = fallDownVector3;
            mouseImage.sprite = mouseImageIcon[1];
            return;
        }

        if (akibanPlayerHoldSit.IsAkibanPlayerSitCheck == true)//playerMovement.Status == PlayerStatus.CAUGHT && !testLook)
        {
            killerPointer.SetActive(true);
            pointerText.text = "최면의자에 놓기";
            killerPointer.transform.localPosition = chairVector3;
            mouseImage.sprite = mouseImageIcon[1];
            return;
        }

        if (akibanPlayerHoldSit.IsPlayerHoldDownCheck == true)//playerMovement.Status == PlayerStatus.CAUGHT)
        {
            killerPointer.SetActive(true);
            pointerText.text = "내려놓기";
            killerPointer.transform.localPosition = caughtlVector3;
            mouseImage.sprite = mouseImageIcon[1];
            return;
        }

        if (akibanAttack.IsPropmachineAttackCheck == true)
        {
            killerPointer.SetActive(true);
            pointerText.text = "부수기";
            killerPointer.transform.localPosition = caughtlVector3;
            mouseImage.sprite = mouseImageIcon[0];
            return;
        }
        killerPointer.SetActive(false);


        if (akibanAttack.IsPlayerAttackCheck == true)
        {
            StartCoroutine(KillerAttackPointer());
        }
    }

    public void ImpostorPointer()
    {
        if (impostorPlayerHoldSit.IsImpostorPlayerDownCheck == true)//playerMovement.Status == PlayerStatus.FALLDOWN)
        {
            killerPointer.SetActive(true);
            pointerText.text = "들기";
            killerPointer.transform.localPosition = fallDownVector3;
            mouseImage.sprite = mouseImageIcon[1];
            return;
        }
        if (impostorPlayerHoldSit.IsImpostorPlayerSitCheck == true)//playerMovement.Status == PlayerStatus.CAUGHT && !testLook)
        {
            killerPointer.SetActive(true);
            pointerText.text = "최면의자에 놓기";
            killerPointer.transform.localPosition = chairVector3;
            mouseImage.sprite = mouseImageIcon[1];
            return;
        }

        if (impostorPlayerHoldSit.IsPlayerHoldDownCheck == true)//playerMovement.Status == PlayerStatus.CAUGHT)
        {
            killerPointer.SetActive(true);
            pointerText.text = "내려놓기";
            killerPointer.transform.localPosition = caughtlVector3;
            mouseImage.sprite = mouseImageIcon[1];
            return;
        }





        if (impostorAttack.IsPropmachineAttackCheck == true)
        {
            killerPointer.SetActive(true);
            pointerText.text = "부수기";
            killerPointer.transform.localPosition = caughtlVector3;
            mouseImage.sprite = mouseImageIcon[0];
            return;
        }
        killerPointer.SetActive(false);

        if (impostorAttack.IsPlayerAttackCheck == true)
        {
            StartCoroutine(KillerAttackPointer());
        }
    }



    IEnumerator KillerAttackPointer()
    {
        attackImage.color = new Color(1, 1, 1, 1);
        fadeOut = 0f;

        while (fadeOut < 1f)
        {
            fadeOut += Time.deltaTime;
            attackImage.color = new Color(1, 1, 1, 1f - fadeOut);
            yield return null;
        }

    }
}
