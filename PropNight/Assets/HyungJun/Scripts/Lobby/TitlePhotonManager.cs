using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using Hashtable = ExitGames.Client.Photon.Hashtable;

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

        TxtRoomName.text = PlayerPrefs.GetString("ROOM_NAME", "ROOM_" + Random.Range(1, 999));
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
        // _lobbyList.SetActive(true);
        // _playButtonGroup.SetActive(false);
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

            // 여기에 이동할 씬 이름을 저장
            roomData.SceneName = (string)roomInfo.CustomProperties["RoomSceneName"];
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
        // 여기서 캐싱하기
        PlayerPrefs.SetString("USER_ID", PhotonNetwork.NickName);
    }


    // JoinRoom() 함수가 실행하면 같이 실행하는 함수
    public override void OnJoinedRoom()
    {
        // // 해당 방의 커스텀 프로퍼티를 설정한다.
        // PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { "RoomSceneName", "Waiting" } });

        // 해당 방의 커스텀프로퍼티를 가져온다.
        Hashtable ht = PhotonNetwork.CurrentRoom.CustomProperties;

        // _logTxt.text = "방 참가 성공!";
        // 씬 이름은 캐싱해서 가져오기
        PhotonNetwork.LoadLevel((string)ht["RoomSceneName"]);
        // PhotonNetwork.AutomaticallySyncScene = true;
        // PhotonNetwork.SetLevelInPropsIfSynced()
        // PhotonNetwork.Load
        // PhotonNetwork.AutomaticallySyncScene
    }

    // 방 만들기 버튼
    public void CreateRoomBtnClick()
    {
    }

    // 형준이 전용 디버그 방 생성
    public void CreateRoomHyungJunScene()
    {
        // 해당 방의 옵션을 설정한다.
        RoomOptions _roomOption = new RoomOptions();
        // 씬의 이름 설정
        _roomOption.CustomRoomProperties = new Hashtable() { { "RoomSceneName", Define.HYUNGJUN_DEBUG_SCENE_NAME } };
        // 최대 플레이어 설정
        _roomOption.MaxPlayers = 5;

        // PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { "RoomSceneName", Define.HYUNGJUN_DEBUG_SCENE_NAME } });
        PhotonNetwork.CreateRoom(TxtRoomName.text, _roomOption);
    }

    // 인게임 방 생성
    public void CreateRoomInGameScene()
    {
        // 해당 방의 옵션을 설정한다.
        RoomOptions _roomOption = new RoomOptions();
        // 씬의 이름 설정
        _roomOption.CustomRoomProperties = new Hashtable() { { "RoomSceneName", Define.INGAME_SCENE_NAME } };
        // 최대 플레이어 설정
        _roomOption.MaxPlayers = 5;

        // PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { "RoomSceneName", Define.HYUNGJUN_DEBUG_SCENE_NAME } });
        PhotonNetwork.CreateRoom(TxtRoomName.text, _roomOption);
    }

    public void CreateRoomSelectCharacterScene()
    {
        // 해당 방의 옵션을 설정한다.
        RoomOptions _roomOption = new RoomOptions();
        // 씬의 이름 설정
        _roomOption.CustomRoomProperties = new Hashtable() { { "RoomSceneName", Define.SELECT_CHARACTER_SCENE_NAME } };
        // 최대 플레이어 설정
        _roomOption.MaxPlayers = 5;

        // PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { "RoomSceneName", Define.HYUNGJUN_DEBUG_SCENE_NAME } });
        PhotonNetwork.CreateRoom(TxtRoomName.text, _roomOption);
    }

}
