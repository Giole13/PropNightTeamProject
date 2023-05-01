using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UiKillerPoint : MonoBehaviour
{
    public GameObject killerPointer = default;
    public GameObject killerAttackPointer = default;
    public TMP_Text pointerText = default;
    private PlayerMovement _playerMovement = default;
    Image attackImage = default;

    private float fadeIn = default;
    private float fadeOut = default;

    bool testLook = false;
    bool test1 = false;
    bool test2 = false;
    bool test3 = false;
    bool test4 = false;
    //공격시
    bool test5 = false;

    Vector3 fallDownVector3 = new Vector3(0, -350, 0);
    Vector3 caughtlVector3 = new Vector3(0, -200, 0);
    Vector3 chairVector3 = new Vector3(100, 20, 0);

    void Start()
    {
        killerPointer.SetActive(false);
        attackImage = killerAttackPointer.GetComponent<Image>();

        _playerMovement = GetComponent<PlayerMovement>();
        attackImage.color = new Color(1, 1, 1, 0);

    }
    void Update()
    {
        KillerPointer();
    }
    public void KillerPointer()
    {
        if (test1 == true)//playerMovement.Status == PlayerStatus.FALLDOWN)
        {
            killerPointer.SetActive(true);
            pointerText.text = "들기";
            killerPointer.transform.localPosition = fallDownVector3;
        }
        if (test2 == true)//playerMovement.Status == PlayerStatus.CAUGHT)
        {
            killerPointer.SetActive(true);
            pointerText.text = "내려놓기";
            killerPointer.transform.localPosition = caughtlVector3;
        }
        if (test3 == true)//playerMovement.Status == PlayerStatus.CAUGHT && !testLook)
        {
            killerPointer.SetActive(true);
            pointerText.text = "최면의자에 놓기";
            killerPointer.transform.localPosition = chairVector3;
        }
        if (test4 == true)
        {
            killerPointer.SetActive(true);
            pointerText.text = "부수기";
            killerPointer.transform.localPosition = caughtlVector3;
        }

        if (test5 == true)
        {
            StartCoroutine(KillerAttackPointer());
        }
    }

    #region 테스트 확인
    //버튼 확인용
    public void fallDown()
    {
        test1 = true;
        //playerMovement.Status = PlayerStatus.FALLDOWN;
    }
    public void caught()
    {
        test2 = true;
        // playerMovement.Status = PlayerStatus.CAUGHT;
    }
    public void chair()
    {
        test3 = true;
        // playerMovement.Status = PlayerStatus.CAUGHT;
        //testLook = true;
    }
    public void propMachineBrocken()
    {
        test4 = true;
        // playerMovement.Status = PlayerStatus.CAUGHT;
        //testLook = true;
    }

    public void fallDownN()
    {
        test1 = false;
        killerPointer.SetActive(false);
        //killerPointer.SetActive(false);
        //playerMovement.Status = 0;
    }
    public void caughtN()
    {
        test2 = false;
        killerPointer.SetActive(false);
        // killerPointer.SetActive(false);
        //playerMovement.Status = 0;
    }
    public void chairN()
    {
        test3 = false;
        killerPointer.SetActive(false);
        //playerMovement.Status = 0;
        //testLook = false;
        //killerPointer.SetActive(false);
    }
    public void propMachineBrockenN()
    {
        test4 = false;
        killerPointer.SetActive(false);
        // playerMovement.Status = PlayerStatus.CAUGHT;
        //testLook = true;
    }
    #endregion

    public void AttackPointer()
    {
        test5 = true;
    }

    IEnumerator KillerAttackPointer()
    {
        test5 = false;
        attackImage.color = new Color(1, 1, 1, 1);
        fadeOut = 0f;

        while (fadeOut < 1f)
        {

            fadeOut += Time.deltaTime;
            Debug.Log(fadeOut);
            attackImage.color = new Color(1, 1, 1, 1f - fadeOut);
            yield return null;
        }





    }
}
