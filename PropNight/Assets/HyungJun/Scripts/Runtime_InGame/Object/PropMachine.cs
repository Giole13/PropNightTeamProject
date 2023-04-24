using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PropMachine : MonoBehaviourPun, IInteraction, IPunObservable
{
    // 몇개를 수리했는지 알려주는 변수
    private float _maxFixGauge = 1f;

    [SerializeField]
    private ProtoExitPortal _exitPortalScript = default;

    #region 각 클라이언트가 공유해야하는 자원
    // 현재 수리된 프롭머신의 합계
    public static byte s_fixPropMachine = 0;
    // 해당 프롭머신의 수리 진행도
    private float _currentFixGauge = 0f;
    // 수리 중인지 체크하는 변수
    private bool IsFixing = false;
    // 수리 완료 변수
    public bool IsFixDone = false;
    // 해당 프롭머신이 파괴 가능한지 체크하는 변수
    public bool IsBreakPossible { get; private set; } = false;
    #endregion 각 클라이언트가 공유해야하는 자원

    [SerializeField]
    private Image _fixGaugeImage;

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
    [PunRPC]
    public void OnInteraction(GameObject obj)
    {
        // if()
        // 플레이어가 프롭머신을 작동하면
        if (obj.tag == "Player" && !IsFixDone)
        {
            // UI 출력
            // 수치 초기화 후 켜주기
            PlayerUi.s_instance.FixingPropMachine(_currentFixGauge / _maxFixGauge);
            PlayerUi.s_instance.SetInterationTxt("프롭머신 수리하는 중");

            PlayerUi.s_instance.InteractionInfo.SetActive(true);

            IsFixing = true;
            StartCoroutine(RaiseFixGauge(obj));
        }
        // 킬러라면 프롭머신의 수리 진행도를 줄여주는 함수 작성
        else if (obj.tag == "Killer" && !IsFixDone)
        {
            StartCoroutine(FallDownFixGauge());
        }
    }

    // 플레이어 상호작용 UI 비 활성화 및 게이지 증가 정지
    [PunRPC]
    public void OffInteraction(GameObject obj)
    {
        if (obj.tag == "Player")
        {
            PlayerUi.s_instance.InteractionInfo.SetActive(false);
            IsFixing = false;
        }
        // else if (obj.tag == "")
    }

    // 프롭머신을 수리하는 코루틴
    private IEnumerator RaiseFixGauge(GameObject obj)
    {
        while (IsFixing && !IsFixDone)
        {
            IsBreakPossible = true;
            yield return new WaitForSecondsRealtime(0.01f);
            _currentFixGauge += 0.01f;
            PlayerUi.s_instance.FixingPropMachine(_currentFixGauge / _maxFixGauge);
            // 프롭머신의 위에 존재하는 게이지바를 업데이트 하는 로직
            _fixGaugeImage.fillAmount = _currentFixGauge / _maxFixGauge;
            if (_maxFixGauge <= _currentFixGauge)
            {
                // 수리 완료시 실행 하는 함수
                OffInteraction(obj);
                IsBreakPossible = false;
                IsFixDone = true;
                ++s_fixPropMachine;
                GetComponent<MeshRenderer>().material.SetColor("_BaseColor", Color.black);
                if (5 == s_fixPropMachine) { _exitPortalScript.DoorOpen(); }

                yield break;
            }
        }
    }       // RaiseFixGauge()



    private IEnumerator FallDownFixGauge()
    {
        while (IsBreakPossible)
        {
            yield return new WaitForSeconds(0.01f);
            _currentFixGauge -= 0.001f;
            _fixGaugeImage.fillAmount = _currentFixGauge / _maxFixGauge;
            if (_currentFixGauge <= 0)
            {
                yield break;
            }
        }
    }



    // 현재 프롭머신의 게이지 상태를 공유하는 함수
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // 현재 실행하는 스크립트가 로컬일 경우 쓰기
        if (stream.IsWriting)
        {
            stream.SendNext(s_fixPropMachine);
            stream.SendNext(_currentFixGauge);
            stream.SendNext(IsFixing);
            stream.SendNext(IsFixDone);
            stream.SendNext(IsBreakPossible);
        }
        // 다른 클라이언트라면 받기
        else
        {
            s_fixPropMachine = (byte)stream.ReceiveNext();
            _currentFixGauge = (float)stream.ReceiveNext();
            IsFixing = (bool)stream.ReceiveNext();
            IsFixDone = (bool)stream.ReceiveNext();
            IsBreakPossible = (bool)stream.ReceiveNext();
        }
    }

    // private void ExitPortalOpen()
    // {

    // }

    #endregion 프로토타입 버전
}       // class PropMachine
