using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropMachine : MonoBehaviour, IInteraction
{
    // 몇개를 수리했는지 알려주는 변수
    public static byte s_fixPropMachine = 0;
    private float _maxFixGauge = 1f;

    [SerializeField]
    private ProtoExitPortal _exitPortalScript = default;

    private float _currentFixGauge = 0f;
    private bool IsFixing = false;
    public bool IsFixDone = false;
    private void Awake()
    {
        _currentFixGauge = 0f;
    }

    #region 프로토타입 버전
    // 플레이어와 충돌하면 플레이어 상호작용 UI 팝업 & 상호작용 게이지 상승
    // private void OnCollisionEnter(Collision other)
    // {
    //     if (other.transform.tag == "Player" && !IsFixDone)
    //     {
    //         OnInteraction();
    //     }
    // }

    // private void OnCollisionExit(Collision other)
    // {
    //     if (other.transform.tag == "Player" && !IsFixDone)
    //     {
    //         OffInteraction();
    //     }
    // }


    // 플레이어 상호작용 UI 활성화 및 게이지 증가
    public void OnInteraction()
    {
        // 수치 초기화 후 켜주기
        PlayerUi.s_instance.FixingPropMachine(_currentFixGauge / _maxFixGauge);
        PlayerUi.s_instance.SetInterationTxt("프롭머신 수리하는 중");

        PlayerUi.s_instance.InteractionInfo.SetActive(true);


        IsFixing = true;
        StartCoroutine(RaiseFixGauge());
    }

    // 플레이어 상호작용 UI 비 활성화 및 게이지 증가 정지
    public void OffInteraction()
    {
        PlayerUi.s_instance.InteractionInfo.SetActive(false);
        IsFixing = false;
    }

    // 프롭머신을 수리하는 코루틴
    private IEnumerator RaiseFixGauge()
    {
        while (IsFixing && !IsFixDone)
        {
            yield return new WaitForSecondsRealtime(0.01f);
            _currentFixGauge += 0.01f;
            PlayerUi.s_instance.FixingPropMachine(_currentFixGauge / _maxFixGauge);
            if (_maxFixGauge <= _currentFixGauge)
            {
                OffInteraction();
                IsFixDone = true;
                ++s_fixPropMachine;
                GetComponent<MeshRenderer>().material.SetColor("_BaseColor", Color.black);
                if (5 == s_fixPropMachine) { _exitPortalScript.DoorOpen(); }


                yield break;
            }
        }
    }       // RaiseFixGauge()

    // private void ExitPortalOpen()
    // {

    // }

    #endregion 프로토타입 버전
}       // class PropMachine
