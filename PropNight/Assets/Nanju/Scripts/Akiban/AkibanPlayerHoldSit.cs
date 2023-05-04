using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class AkibanPlayerHoldSit : MonoBehaviourPun
{
    // 플레이어 들었을때 위치값
    public Transform HoldPlayerPosition;
    // 오른쪽 마우스를 클릭했는가
    private bool IsRightMouseClick = false;
    // 플레이어 알기
    public GameObject Player;
    public InGameManager GameManager;
    // 카메라 알기
    // 1인칭 카메라
    public GameObject FirstCamera;
    // 3인칭 카메라
    public GameObject ThirdCamera;

    [SerializeField] private CinemachineVirtualCamera VirtualFirstCamera;
    [SerializeField] private CinemachineVirtualCamera VirtualThirdCamera;

    [SerializeField] private KillerState _killerState = KillerState.IDLE;

    private PlayerMovement _playerMovementScript = default;

    // Laycast를 불러와서 사용하기
    [SerializeField] private AkibanCameraMove LookCamera;

    private UiKillerPoint _killerPoint;
    // 플레이어가 쓰러진것을 확인
    public bool IsAkibanPlayerDownCheck = false;
    // 최면의자에 앉히는 것을 확인
    public bool IsAkibanPlayerSitCheck = false;
    // 플레이어 놓기 확인
    public bool IsPlayerHoldDownCheck = false;


    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            _killerPoint = GameObject.Find("InGameKillerUi").GetComponent<UiKillerPoint>();
            _killerPoint.akibanPlayerHoldSit = this;
            VirtualFirstCamera.Priority = 20;
            VirtualThirdCamera.Priority = 20;
        }

        // LookCamera = GetComponent<>
        // GameManager = GameObject.Find("InGameManager").GetComponent<InGameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        HoldUiCheck();
        SitUiCheck();
        RightClick();
    }

    //  플레이어어 쓰러진 상태를 ui 한테 보내주기 위한 함수
    public void HoldUiCheck()
    {
        // 포톤에서 자기자신만 움직이게 하기 위해 
        if (!photonView.IsMine) { return; }
        if (LookCamera.Obj == null) { return; }

        if (LookCamera.Obj.tag == "Player" && LookCamera.ObjDistance < 3f)
        {
            _playerMovementScript = LookCamera.Obj.GetComponent<PlayerMovement>();
            if (_playerMovementScript.Status == PlayerStatus.FALLDOWN)
            {
                // Ui 오브젝트 가져오기 (활성화)
                IsAkibanPlayerDownCheck = true;
                return;
            }
        }

        IsAkibanPlayerDownCheck = false;

    }

    // 플레이어를 최면의자에 앉힌 것을 보내주기 위한 함수(UI)
    public void SitUiCheck()
    {
        // 포톤에서 자기자신만 움직이게 하기 위해 
        if (!photonView.IsMine) { return; }
        if (LookCamera.Obj == null) { return; }

        if (LookCamera.Obj.tag == "HypnoticChair" && _killerState == KillerState.PLAYERHOLD && LookCamera.ObjDistance < 3f)
        {
            // Ui 오브젝트 가져오기 (활성화)
            IsAkibanPlayerSitCheck = true;
            return;

        }
        IsAkibanPlayerSitCheck = false;
    }

    // 플레이어 놓기를 보내주기 위한 함수(ui)
    public void PlayerHoldDownCheck()
    {
        // 포톤에서 자기자신만 움직이게 하기 위해 
        if (!photonView.IsMine) { return; }

        if (_killerState == KillerState.PLAYERHOLD)
        {
            _killerState = KillerState.IDLE;
            IsPlayerHoldDownCheck = true;
            return;
        }
        IsPlayerHoldDownCheck = false;
    }
    // 오른쪽 마우스를 클릭시

    private void RightClick()
    {
        // 포톤에서 자기자신만 움직이게 하기 위해 
        if (!photonView.IsMine) { return; }

        if (Input.GetMouseButtonDown(1))
        {
            if (LookCamera.Obj == null) { return; }

            // 플레이어 들기
            if (LookCamera.Obj.tag == "Player" && LookCamera.ObjDistance < 3f)
            {
                // 플레이어 스크립트 가져오기
                _playerMovementScript = LookCamera.Obj.GetComponent<PlayerMovement>();

                // 플레이어의 상태가 쓰러진 상태이면
                if (_playerMovementScript.Status == PlayerStatus.FALLDOWN)
                {
                    photonView.RPC("PlayerHold", RpcTarget.All, LookCamera.Obj.GetPhotonView().ViewID.ToString());
                }
            }
            // 플레이어 최면의자에 앉히기
            else if (LookCamera.Obj.tag == "HypnoticChair" && _killerState == KillerState.PLAYERHOLD && LookCamera.ObjDistance < 3f)
            {
                // photonView.RPC("PlayerSeating", RpcTarget.All);
                //Player.GetComponent<PlayerMovement>().SitOnChair();   // 최면의자 스크립트에서 실행
                // 플레이어의 상태를 변환하는 것은 모든 클라이언트에 적용되야 함 그래서 나머지는 최면의자 스크립트에서 실행
                LookCamera.Obj.GetComponent<IInteraction>().OnInteraction(Player.GetPhotonView().ViewID.ToString());
                // 1인칭 카메라 켜기
                ThirdCamera.SetActive(false);
                // 3인칭 카메라 끄기
                FirstCamera.SetActive(true);
                // 플레이어 오브젝트 살인마 자식으로 빼기
                //Player.transform.SetParent(null);     // 최면의자 스크립트에서 실행

            }
            // 생존자 들고있는 상태에서 오른쪽 클릭시 생존자 내려놓기
            else if (_killerState == KillerState.PLAYERHOLD)
            {
                photonView.RPC("PlayerHoldDown", RpcTarget.All);
            }

        }

    }

    // ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
    // █░░░░░░░░▀█▄▀▄▀██████░▀█▄▀▄▀██████░
    // ░░░░░░░░░░░▀█▄█▄███▀░░░ ▀██▄█▄███▀░


    [PunRPC]
    // 플레이어 들기 함수
    public void PlayerHold(string ViewID)
    {
        // 플레이어 스크립트 가져오기
        GameObject player = GameManager.FindPlayerorKiller(ViewID);

        _playerMovementScript = player.GetComponent<PlayerMovement>();

        // 2023.04.25 / HyungJun / 클라이언트 리스트를 사용해보기위한 테스트
        // InGameManager.ClientDic[]

        // 플레이어의 상태가 쓰러진 상태이면
        if (_playerMovementScript.Status == PlayerStatus.FALLDOWN)
        {
            _killerState = KillerState.PLAYERHOLD;
            Player = player;
            // 플레이어 오브젝트가 살인마 자식으로 오게 하기
            Player.transform.SetParent(gameObject.transform);
            // 플레이어 상태 바꾸기
            Player.GetComponent<PlayerMovement>().Hold();
            // 플레이어 위치값 변경하기(들기)
            Player.transform.position = HoldPlayerPosition.position;
            // 카메라 3인칭 되게 하기
            ThirdCamera.SetActive(true);
            // 1인칭 카메라 끄기
            FirstCamera.SetActive(false);
        }
    }



    // 플레이어 최면의자에 앉히기 함수
    [PunRPC]
    public void PlayerSeating()
    {
        Player.GetComponent<PlayerMovement>().SitOnChair();
        // 플레이어 오브젝트 살인마 자식으로 빼기
        Player.transform.SetParent(null);
        LookCamera.Obj.GetComponent<IInteraction>().OnInteraction(Player.tag);

        // 1인칭 카메라 켜기
        ThirdCamera.SetActive(false);
        // 3인칭 카메라 끄기
        FirstCamera.SetActive(true);
    }

    // 들고 있는 플레이어 놓기 함수
    [PunRPC]
    public void PlayerHoldDown()
    {
        // 플레이어 오브젝트 살인마 자식으로 빼기
        Player.transform.SetParent(null);
        // 플레이어 놓기
        Player.GetComponent<PlayerMovement>().PutDown();
        _killerState = KillerState.IDLE;

        // 1인칭 카메라 켜기
        ThirdCamera.SetActive(false);
        // 3인칭 카메라 끄기
        FirstCamera.SetActive(true);
    }


    // // 마우스 오른쪽 클릭시 
    // public void MouseRightButton()
    // {
    //     if (Input.GetMouseButtonDown(1))
    //     {
    //         // 플레이어 들고 있는 시간 코루틴 실행
    //         StartCoroutine(HoldTime());

    //     }
    // }

    // // 플레이어 들고 있는 시간을 주어 마우스 오른쪽 true, false 만들기
    // private IEnumerator HoldTime()
    // {
    //     IsRightMouseClick = true;
    //     yield return null;
    //     IsRightMouseClick = false;
    // }

}
