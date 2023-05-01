using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PhotonInit : MonoBehaviourPunCallbacks
{
    public enum ActivePanel { LOGIN = 0, ROOMS = 1 }

    public ActivePanel ActivePanelState = ActivePanel.LOGIN;

    // private string _gameVersion = "1.0";
    public string UserId = "박형준";
    public byte MaxPlayers = 20;

    public TMP_InputField TxtUserId;
    public TMP_InputField TxtRoomName;

    public GameObject[] Panels;

    public GameObject Room;
    public Transform GridTr;



    // Start is called before the first frame update
    void Start()
    {
        // TxtUserId = PlayerPrefs.GetString("")
        if (PhotonNetwork.IsConnected)
        {
            // Chage
        }
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
