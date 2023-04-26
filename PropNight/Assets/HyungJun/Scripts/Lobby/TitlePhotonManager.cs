using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class TitlePhotonManager : MonoBehaviourPunCallbacks
{
    // 들어 갈 수 있는 서버의 종류 [서버] [로비] [룸]


    [SerializeField] private GameObject _lobbyList;
    [SerializeField] private GameObject _playButtonGroup;

    public GameObject RoomPrefab;
    public Transform GridTr;

    public TMP_InputField TxtRoomName;


    // Start is called before the first frame update
    private void Start()
    {
        _lobbyList.SetActive(false);
        // 접속하자 마자 서버에 접속함 -> 현재 상태 [서버]
        PhotonNetwork.ConnectUsingSettings();
    }

    // 빠른게임 찾기 버튼
    public void QuickPlayerSearchBtn()
    {
        // 방을 랜덤으로 참가하거나 없으면 방을 만든다.
        PhotonNetwork.JoinRandomOrCreateRoom();
    }

    // 사용자 설정 게임 버튼
    public void UserSettingGameBtn()
    {
        _lobbyList.SetActive(true);
        _playButtonGroup.SetActive(false);
        // 로비에 접속한다.
        PhotonNetwork.JoinLobby();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        // Debug.Log("업데이트 실행");
        // foreach (RoomInfo roomInfo in roomList)
        // {
        //     Debug.Log(roomInfo.Name);
        // }
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("ROOM"))
        {
            Destroy(obj);
        }
        foreach (RoomInfo roomInfo in roomList)
        {
            // 해당 룸 인포의 플레이어 수가 0 이하이면 만들지 않는다.
            if (roomInfo.PlayerCount <= 0) { continue; }
            GameObject _room = Instantiate(RoomPrefab, GridTr);
            _room.name = roomInfo.Name;
            RoomData roomData = _room.GetComponent<RoomData>();
            roomData.RoomName = roomInfo.Name;
            roomData.MaxPlayers = roomInfo.MaxPlayers;
            roomData.PlayerCount = roomInfo.PlayerCount;
            roomData.UpdateInfo();
            roomData.Btn.onClick.AddListener
            (
                delegate
                {
                    OnClickRoom(roomData.RoomName);
                }
            );
        }
    }

    // 방 참가 버튼
    void OnClickRoom(string roomName)
    {
        // PhotonNetwork.NickName = txtUserId.text;
        PhotonNetwork.JoinRoom(roomName, null);
        PlayerPrefs.SetString("USER_ID", PhotonNetwork.NickName);
    }

    // 방 만들기 버튼
    public void CreateRoomBtnClick()
    {
        PhotonNetwork.CreateRoom(TxtRoomName.text
                                , new RoomOptions { MaxPlayers = 5 });
    }


}
