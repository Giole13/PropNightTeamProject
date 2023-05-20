using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class InGameManager : MonoBehaviourPunCallbacks /*, IPunObservable*/
{
    // public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    // {
    //     throw new System.NotImplementedException();
    // }





    [SerializeField] private GameObject _playerUI;
    [SerializeField] private GameObject _KillerUI;
    [SerializeField] private ExitDoorPortal _exitDoorPortal;


    [Tooltip("생존자 오브젝트")] public GameObject[] PlayerPrefab;
    [Tooltip("킬러 오브젝트")] public GameObject[] KillerPrefab;

    // 현재 클라이언트의 플레이어블 캐릭터 오브젝트
    public static GameObject PlayerObject = default;
    public GameStatusManager StatusManager;
    // 모든 클라이언트의 오브젝트가 담겨져 있는 딕셔너리
    public static Dictionary<int, GameObject> ClientDic = new Dictionary<int, GameObject>();
    public SpwanPoint SpwanPoints;
    public int Count = 0;
    private DataContainer _dc = default;

    void Start()
    {
        _dc = GameObject.Find("DataContainer").GetComponent<DataContainer>();


        _playerUI.SetActive(false);
        _KillerUI.SetActive(false);
        // PhotonNetwork.Instantiate(PlayerCameraPrefab.name, Vector3.zero, Quaternion.identity);

        if (PhotonNetwork.IsMasterClient)
        {
            // 마스터 클라이언트라면 살인마로 결정
            PlayerObject = PhotonNetwork.Instantiate(KillerPrefab[DataContainer.KillerSelectNumber].name, Vector3.zero, Quaternion.identity);
            _KillerUI.SetActive(true);
        }
        else
        {
            // 게스트 클라이언트라면 생존자로 결정
            PlayerObject = PhotonNetwork.Instantiate(PlayerPrefab[DataContainer.PlayerSelectNumber].name, Vector3.zero, Quaternion.identity);
            _playerUI.SetActive(true);
            // PlayerObject = PhotonNetwork.Instantiate(PlayerPrefab.name, Vector3.zero, Quaternion.identity);
        }
        // 클라이언트 딕셔너리에 자기 자신의 오브젝트 추가
        photonView.RPC("ClientDicUpdate", RpcTarget.All);
        PlayerObject.transform.position = SpwanPoints.points[(PlayerObject.GetPhotonView().ViewID / 1000) - 1].position;

        // DataContainer.ClientObject = PlayerObject;

        Cursor.visible = false;
    }

    /// <summary>모든 클라이언트에서 리스트를 업데이트 하는 함수</summary>
    [PunRPC]
    public void ClientDicUpdate()
    {
        StartCoroutine(CashingClientList());
        // if (PhotonNetwork.IsMasterClient)
        // {
        //     // 플레이어를 찾을 수 있는 딕셔너리 생성 -> 모든 클라이언트가 같은 정보를 담고 있어야한다.
        //     // PhotonNetwork.PlayerList[0].
        // }
        // else
        // {
        //     // 게스트라면 자신을 딕셔너리에 넣는다.
        //     // 하이어라키에서 플레이어 태그를 전부 가져와서
        // ClientDic.Add(_player.GetPhotonView().ViewID, _player);
        // if (!PhotonNetwork.IsMasterClient)
        // {
        // }
    }

    private IEnumerator CashingClientList()
    {
        yield return new WaitForSeconds(1f);
        ClientDic.Clear();
        foreach (var obj in GameObject.FindGameObjectsWithTag("Killer"))
        {
            // 킬러 넣고
            // Debug.Log("여기서 킬러를 딕셔너리에 넣는다/.");
            ClientDic.Add(obj.GetPhotonView().ViewID, obj);
            // if (_player.GetPhotonView().ViewID == obj.GetPhotonView().ViewID)
            // {
            // }
        }
        // }
        int Count = 0;
        foreach (var obj in GameObject.FindGameObjectsWithTag("Player"))
        {
            ClientDic.Add(obj.GetPhotonView().ViewID, obj);
            StatusManager.SurvivorID[Count] = obj.GetPhotonView().ViewID;
            Count++;
            // if (_player.GetPhotonView().ViewID == obj.GetPhotonView().ViewID)
            // {

            // }
        }
        StatusManager.SurvivorMemberNumber = Count;
        StatusManager.SurvivorMaxNumber = Count;
        Debug.Log("총 생존자 수" + StatusManager.SurvivorMemberNumber);
    }



    // Debug - 딕셔너리에 저장된 정보 확인용
    public void DictionaryCheckBtn()
    {
        foreach (var obj in ClientDic)
        {
            Debug.Log("키값 : " + obj.Key + " 벨류 네임 : " + obj.Value.name);
        }
        // for (int i = 0; i < ClientDic.Count; i++)
        // {
        //     Debug.Log(i + "번째 오브젝트 이름" + ClientDic[i].name);
        // }
    }

    // Update is called once per frame
    void Update()
    {
        // 승리 패배 판정을 해야함
    }


    public GameObject FindPlayerorKiller(string ViewID)
    {
        int IDNumber = Int32.Parse(ViewID);

        return ClientDic[IDNumber];

    }
}
