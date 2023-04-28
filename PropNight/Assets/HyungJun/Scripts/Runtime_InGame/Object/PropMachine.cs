using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;


// ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣀⣀⣀⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
// ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⠤⠖⠚⠉⠉⠀⠀⠀⠀⠉⠉⠙⠒⠤⣄⡀⠀⠀⣀⣠⣤⣀⡀⠀⠀⠀
// ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⠖⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠛⢯⡀⠀⠀⠀⠉⠳⣄⠀
// ⠀⣀⠤⠔⠒⠒⠒⠦⢤⣀⢀⡴⠋⠀⠀⠀⠀⠀⠀⠀⠀⢠⣤⣄⠀⠀⠀⠀⠀⣴⢶⣄⠀⠀⠀⠉⢢⡀⠀⠀⠀⠘⡆
// ⢠⠞⠁⠀⠀⠀⠀⠀⠀⠀⠈⢻⡀⠀⠀⠀⠀⠀⠀⠀⠀⢠⡟⠀⢹⣧⠀⠀⠀⠀⣿⠀⢹⣇⠀⠀⠀⠀⠙⢦⠀⠀⠀⣧
// ⡏⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⡇⠀⠀⠀⠀⠀⠀⠀⠀⢸⣿⣦⣼⣿⡇⠀⠀⠀⢿⣿⣿⣿⡄⠀⠀⠀⠀⠈⢳⡀⢀⡟
// ⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡸⠁⠀⠀⠀⠀⠀⠀⠀⠀⠈⣿⡿⠿⠿⣿⠀⠀⠀⠘⣿⡛⣟⣧⠀⠀⠀⠀⠀⠀⢳⠞⠀
// ⢳⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⠇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⣷⡄⢴⡿⠀⠀⠀⠀⠘⣿⣷⡏⠀⢀⡠⠤⣄⠀⠀⣇⠀
// ⠀⢳⡀⠀⠀⠀⠀⠀⠀⢠⠏⠀⠀⠀⠀⠀⣠⠄⠀⠀⠀⠀⠀⠈⠛⠛⠁⣀⡤⠤⠤⠤⢌⣉⠀⠀⢠⡀⠀⠀⡱⠀⢸⡄
// ⠀⠀⠙⠦⣀⠀⠀⠀⣰⠋⠀⠀⠀⠀⠀⠸⣅⠀⠀⢀⡀⠀⠀⠀⢀⠴⠋⠀⠀⠀⠀⠀⠀⠈⠳⣄⠀⠈⠉⠉⠀⠀⢘⣧
// ⠀⠀⠀⠀⠈⠙⢲⠞⠁⠀⠀⠀⠀⠀⠀⠀⠀⠉⠉⠁⠀⠀⠀⣰⣋⣀⣀⣀⣀⠀⠀⠀⠀⠀⠀⠈⢧⠀⠀⠀⠀⠀⢐⣿
// ⠀⠀⠀⠀⠀⠀⢸⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡰⠁⠀⠀⠀⠀⠀⠉⠙⠒⢤⣀⠀⠀⠈⣇⠀⠀⠀⠀⠀⣿
// ⠀⠀⠀⠀⠀⠀⠘⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⠇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠳⣄⠀⢸⠀⠀⠀⠀⢠⡏
// ⠀⠀⠀⠀⠀⠀⠀⢳⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣾⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⡆⠘⣧⠀⠀⠀⣸⠀
// ⠀⠀⠀⠀⠀⠀⠀⡟⣆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡏⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢱⢰⠏⠀⠀⢠⠇⠀
// ⠀⠀⠀⠀⠀⠀⢸⠁⠘⣆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡼⣸⠀⠀⢀⠏⠀⠀
// ⠀⠀⠀⠀⠀⠀⣿⠀⠀⠘⢆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡴⣣⠃⠀⣠⠏⠀⠀⠀
// ⠀⠀⠀⠀⠀⠀⣿⠀⠀⠀⠈⠳⣄⠀⠀⠀⠀⠀⠀⠀⠀⠘⡆⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⡤⠞⡱⠋⢀⡴⠁⠀⠀⠀⠀
// ⠀⠀⠀⠀⠀⠀⣿⠀⠀⠀⠀⠀⠈⠣⣄⠀⠀⠀⠀⠀⠀⠀⠹⣄⠀⠀⠀⠀⢀⣀⡤⠖⢋⡠⠞⢁⡴⠋⡇⠀⠀⠀⠀⠀
// ⠀⠀⠀⠀⠀⠀⠸⡄⠀⠀⠀⠀⠀⠀⠈⠙⠢⣄⡀⠀⠀⠀⠀⠈⠙⠯⠭⢉⠡⠤⠴⠒⣉⠴⠚⠁⠀⢰⠃⠀⠀⠀⠀⠀
// ⠀⠀⠀⠀⠀⠀⠀⢳⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⢹⠖⠲⠤⠤⠤⠤⠤⠤⢶⡖⠚⠉⠀⠀⠀⠀⢀⡞⠀⠀⠀⠀⠀⠀
// ⠀⠀⠀⠀⠀⠀⠀⠀⢳⡀⠀⠀⠀⠀⠀⠀⠀⠀⡰⠋⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⠲⠤⠤⠤⠤⠔⠋⠀⠀⠀⠀⠀⠀⠀
// ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⢤⡀⠀⠀⠀⠀⣠⠞⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
// ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠛⠑⠒⠒⠋⠂⠐⠒⠀⠀⠒⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀


public class PropMachine : MonoBehaviourPun, IInteraction
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

    private Transform _lookTransform;

    private bool _gaugeBarLookPlayer = false;
    // private GameObject _playerObj;

    private void Awake()
    {
        _currentFixGauge = 0f;
    }

    private void Start()
    {
        StartCoroutine(ClientCashing());
    }

    private IEnumerator ClientCashing()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (InGameManager.PlayerObject != default)
            {
                _lookTransform = InGameManager.PlayerObject.transform;
                _gaugeBarLookPlayer = true;
                yield break;
            }
        }
    }

    private void Update()
    {
        // 프롭머신의 위에 게이지바 오브젝트가 플레이어를 바라봐야한다.
        if (_gaugeBarLookPlayer)
        {
            // Debug.Log("업데이트 실행중");
            _fixGaugeImage.transform.parent.LookAt(_lookTransform);
        }

    }
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
    public void OnInteraction(string tagName)
    {
        // if()
        // 플레이어가 프롭머신을 작동하면
        if (tagName == "Player" && !IsFixDone)
        {
            // UI 출력
            // 수치 초기화 후 켜주기
            // PlayerUi.s_instance.FixingPropMachine(_currentFixGauge / _maxFixGauge);
            // PlayerUi.s_instance.SetInterationTxt("프롭머신 수리하는 중");

            // PlayerUi.s_instance.InteractionInfo.SetActive(true);

            // 플레이어정보를 가져와서 자신이 맞는지 확인
            // if (_playerObj.GetComponent<PlayerMovement>().photonView.IsMine) { return; }
            photonView.RPC("PropMachineFixGaugeUpdate", RpcTarget.All, _currentFixGauge, true);
        }
        else if (tagName == "Killer" && !IsFixDone)
        {
            photonView.RPC("FallDownFixGauge", RpcTarget.All);
        }

        // 킬러라면 프롭머신의 수리 진행도를 줄여주는 함수 작성
    }



    /// <summary> 프롭머신 위쪽의 게이지바를 업데이트 하는 로직</summary>
    [PunRPC]
    public void PropMachineFixGaugeUpdate(float currentValue, bool IsFixingValue)
    {
        IsFixing = IsFixingValue;
        _currentFixGauge = currentValue;
        StartCoroutine(RaiseFixGauge());
    }

    [PunRPC]
    public void StopFixPropMachine(bool IsFixingValue)
    {
        IsFixing = IsFixingValue;
    }

    // 플레이어 상호작용 UI 비 활성화 및 게이지 증가 정지
    public void OffInteraction(string tagName)
    {
        if (tagName == "Player")
        {
            // PlayerUi.s_instance.InteractionInfo.SetActive(false);
            // IsFixing = false;
            photonView.RPC("StopFixPropMachine", RpcTarget.All, false);
        }
    }

    // 프롭머신을 수리하는 코루틴
    private IEnumerator RaiseFixGauge()
    {
        while (IsFixing && !IsFixDone)
        {
            IsBreakPossible = true;
            yield return new WaitForSecondsRealtime(0.01f);
            _currentFixGauge += 0.01f;
            // PlayerUi.s_instance.FixingPropMachine(_currentFixGauge / _maxFixGauge);
            // 프롭머신의 위에 존재하는 게이지바를 업데이트 하는 로직
            _fixGaugeImage.fillAmount = _currentFixGauge / _maxFixGauge;
            // _fixGaugeImage.fillAmount = _currentFixGauge / _maxFixGauge;
            if (_maxFixGauge <= _currentFixGauge)
            {
                // 수리 완료시 실행 하는 함수
                IsBreakPossible = false;
                IsFixDone = true;
                ++s_fixPropMachine;
                GetComponent<MeshRenderer>().material.SetColor("_BaseColor", Color.black);
                if (5 == s_fixPropMachine) { _exitPortalScript.DoorOpen(); }

                yield break;
            }
        }
    }       // RaiseFixGauge()


    [PunRPC]
    public void FallDownFixGauge()
    {
        if (IsBreakPossible) StartCoroutine(FallDownFixGaugeCoroutine());
    }




    private IEnumerator FallDownFixGaugeCoroutine()
    {
        while (IsBreakPossible)
        {
            yield return new WaitForSeconds(0.01f);
            _currentFixGauge -= 0.01f;
            _fixGaugeImage.fillAmount = _currentFixGauge / _maxFixGauge;
            if (_currentFixGauge <= 0)
            {
                yield break;
            }
        }
    }


    // { Lagacy
    // 현재 프롭머신의 게이지 상태를 공유하는 함수
    // public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    // {
    //     Debug.Log("여기는 주기적으로 실행하는 부분");
    //     // 현재 실행하는 스크립트가 로컬일 경우 쓰기
    //     if (PhotonNetwork.IsMasterClient)
    //     {
    //         Debug.Log("여기는 로컬일 때");
    //         stream.SendNext(s_fixPropMachine);
    //         stream.SendNext(_currentFixGauge);
    //         stream.SendNext(IsFixing);
    //         stream.SendNext(IsFixDone);
    //         stream.SendNext(IsBreakPossible);
    //     }
    //     // 다른 클라이언트라면 받기
    //     else
    //     {
    //         Debug.Log("여기는 리모트일 때");
    //         s_fixPropMachine = (byte)stream.ReceiveNext();
    //         _currentFixGauge = (float)stream.ReceiveNext();
    //         IsFixing = (bool)stream.ReceiveNext();
    //         IsFixDone = (bool)stream.ReceiveNext();
    //         IsBreakPossible = (bool)stream.ReceiveNext();

    //     }
    // }
    // } Lagacy

    // private void ExitPortalOpen()
    // {

    // }

}       // class PropMachine
