using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class HypnoticChair : MonoBehaviourPun, IInteraction
{
    // 의자의 상태를 구분하기 위한 Enum
    public enum HypnoticChairState { IDLE, WORKING }
    public HypnoticChairState ChairState;
    public float Timer;
    //최면의자에 앉을 생존자 - Jihwan 2023.04.25 
    private GameObject _player;
    private InGameManager GameManager;
    private GameStatusManager _gsm;

    [SerializeField] private Transform _sitPosition;

    // 처형까지의 시간 ##################### - 중요함
    private float _maxExecutionTime = 3f;
    // 현재 처형까지의 시간
    private float _currentExecutionTime = 0f;




    private bool IsCountStart = true;
    public bool IsSurvivorOut = false;
    // private GameObject PlayerObj = default;


    private void Awake()
    {
        ChairState = HypnoticChairState.IDLE;
    }
    private void Start()
    {
        _gsm = GameObject.Find("GameStatusManager").GetComponent<GameStatusManager>();
        Timer = 0;
        GameManager = GameObject.Find("InGameManager").GetComponent<InGameManager>();
    }
    public void OnInteraction(string ViewID)
    {


        // { 생존자가 생존자가 앉은 의자에 접근
        if (ViewID == "Player" && ChairState == HypnoticChairState.WORKING)
        {
            photonView.RPC("ReleaseSurvivor", RpcTarget.All);
        }
        // } 생존자가 생존자가 앉은 의자에 접근

        // { 살인자가 빈의자에 접근
        else if (ViewID != "Player" && ChairState == HypnoticChairState.IDLE)
        {
            Debug.Log(ViewID);
            photonView.RPC("SurvivorSitOnChair", RpcTarget.All, ViewID);

        }
        // } 살인자가 빈의자에 접근
    }

    public void OffInteraction(string tagName)
    {

        // { 생존자가 생존자가 앉은 의자에 멀어짐
        if (tagName == "Player" && ChairState == HypnoticChairState.WORKING)
        {
            Timer = 0;
        }
        // } 생존자가 생존자가 앉은 의자에 멀어짐

        //ChairState = HypnoticChairState.IDLE;

        // PlayerObj = default;
        //foreach (Transform _obj in transform) { _obj.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", Color.white); }
    }   // 생존자가 의자에 앉은 생존자 구출 포기


    // 생존자가 의자에 앉혔을 때 처형까지의 시간을 카운트 하는 코루틴
    private IEnumerator PlayerExecutionCountStart()
    {
        while (IsCountStart)
        {
            yield return new WaitForSeconds(0.01f);
            _currentExecutionTime += 0.01f;

            if (_maxExecutionTime <= _currentExecutionTime)
            {
                Debug.Log("처형");
                // 최대처형시간까지 잡혀있다면 생존자 카운트를 하나 줄인다.
                // PlayerObj.SetActive(false);
                // OffInteraction(PlayerObj);
                // _player.
                _gsm.GetComponent<PhotonView>().RPC("SurvivorDie", RpcTarget.All);
                yield break;
            }
        }
    }


    // 살인마가 플레이어를 의자에 앉히는 함수
    [PunRPC]
    public void SurvivorSitOnChair(string ViewID)
    {
        _player = GameManager.FindPlayerorKiller(ViewID);
        _player.GetComponent<PlayerMovement>().SitOnChair();
        _player.transform.SetParent(null);
        // 2023.04.30 / HyungJun / 앉는 위치의 포지션값을 받아와서 적용함
        _player.transform.position = _sitPosition.position + new Vector3(0f, 0f, 0f);
        // _player.transform.rotation = gameObject.transform.rotation;

        ChairState = HypnoticChairState.WORKING;
        // 2023.05.03 / HyungJun / 버그로 인한 비활성화
        // foreach (Transform _obj in transform) { _obj.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", Color.black); }
        IsCountStart = true;
        IsSurvivorOut = false;
        StartCoroutine(PlayerExecutionCountStart());
    }   // 생존자 의자에 앉히기

    [PunRPC]
    public void ReleaseSurvivor()
    {
        Timer += Time.deltaTime;
        if (Timer > 3)
        {
            _player.GetComponent<PlayerMovement>().WakeUp();
            ChairState = HypnoticChairState.IDLE;
            // 2023.05.03 / HyungJun / 버그로 인한 비활성화
            // foreach (Transform _obj in transform) { _obj.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", Color.gray); }
            IsCountStart = false;
            IsSurvivorOut = true;
        }
    }

    // private void OnCollisionEnter(Collision other)
    // {
    //     // Debug.Log($"{other.transform.name}여기는 콜리젼 땅!");
    //     if (other.transform.tag == "Player" && _chairState == HypnoticChairState.IDLE)
    //     {
    //         OnInteraction(other.gameObject);
    //     }
    //     else if (other.transform.tag == "Player" && _chairState == HypnoticChairState.WORKING)
    //     {
    //         OffInteraction(other.gameObject);
    //     }
    // }

    // private void OnTriggerEnter(Collider other)
    // {
    //     Debug.Log($"{other.name}여기는 트리거 땅!");
    //     if (other.transform.tag == "Player")
    //     {
    //         // && _chairState == HypnoticChairState.WORKING

    //         OffInteraction(other.gameObject);
    //     }
    // }
}
