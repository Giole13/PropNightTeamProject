using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;
// using Photon.Pun;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text _inputTxt;
    [SerializeField] private TMP_Text _logTxt;
    [SerializeField] private Button _loginButton;
    [SerializeField] private GameObject _lobbyListWindow;
    [SerializeField] private GameObject _loginWindow;

    private string _gameVersion = "1";


    public void CreateRoomBtnClick()
    {

    }

    // 게임 시작과 함께 실행하는 함수
    void Start()
    {
        // 게임 버전 맞추기
        PhotonNetwork.GameVersion = _gameVersion;
        // 설정한 정보로 서버 접속 시도하기!
        PhotonNetwork.ConnectUsingSettings();

        // 서버 접속중인 동안에는 버튼 비 활성화, 서버 상태 표시
        _loginButton.interactable = false;
        _logTxt.text = "서버에 접속중입니다!";
    }

    // 서버 접속 성공 시 실행하는 함수
    public override void OnConnectedToMaster()
    {
        _loginButton.interactable = true;
        _logTxt.text = "서버 연결 성공!!";
    }

    // 서버에 접속했을 때 로비 블럭으로 이동
    private IEnumerator JoinLobby()
    {
        _logTxt.text = "로비로 이동..";
        yield return new WaitForSeconds(0.5f);
        _loginWindow.SetActive(false);
        _lobbyListWindow.SetActive(true);

    }

    // 서버 연결에 실패 했을 때 실행하는 함수
    public override void OnDisconnected(DisconnectCause cause)
    {
        _loginButton.interactable = false;
        _logTxt.text = "서버 연결 실패...\n재접속 시도중...";

        PhotonNetwork.ConnectUsingSettings();
    }

    // 로그인 버튼을 클릭했을 때 방을 생성 또는 접속
    public void LoginBtnClick()
    {
        // StartCoroutine(JoinLobby());

        _loginButton.interactable = false;
        // 서버에 접속 중인 상태
        if (PhotonNetwork.IsConnected)
        {
            _logTxt.text = "방에 접속 중...";
            // 랜덤한 방에 접속하는 함수
            PhotonNetwork.JoinRandomRoom();
        }
        // 그렇지 않은 상태
        else
        {
            // 재 접속
            _logTxt.text = "방 참가 실패...\n재접속 시도중...";
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    // 랜덤한 방에 접속하는게 실패 했을 때 나오는 함수
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        _logTxt.text = "빈 방이 없어요!\n방 생성중!";
        // 방을 생성하는 함수 (최대 플레이어는 5명)
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 5 });
    }

    // 방에 들어오면 실행하는 함수
    public override void OnJoinedRoom()
    {
        _logTxt.text = "방 참가 성공!";
        PhotonNetwork.LoadLevel(Define.INGAME_SCENE_NAME);
    }
    // OnDisConnected
    // // Update is called once per frame
    // void Update()
    // {

    // }



    // public void ClickLoginBtn()
    // {
    //     // GioleFunc.LoadScene("02.InGame");
    //     Gfunc.LoadScene("02.InGame");
    // }


}
