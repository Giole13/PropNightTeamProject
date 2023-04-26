using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class HypnoticChair : MonoBehaviourPun, IInteraction
{
    public enum HypnoticChairState
    {
        IDLE, WORKING
    }
    public HypnoticChairState ChairState;
    //최면의자에 앉을 생존자 - Jihwan 2023.04.25
    private GameObject _player;
    private InGameManager GameManager;
    // 처형까지의 시간 ##################### - 중요함
    private float _maxExecutionTime = 100f;
    // 현재 처형까지의 시간
    private float _currentExecutionTime = 0f;

    private bool IsCountStart = true;

    // private GameObject PlayerObj = default;


    private void Awake()
    {
        ChairState = HypnoticChairState.IDLE;
    }
    private void Start()
    {
        GameManager = GameObject.Find("InGameManager").GetComponent<InGameManager>();
    }
    public void OnInteraction(string ViewID)
    {


        // { 생존자가 생존자가 앉은 의자에 접근
        if (ViewID == "Player" && ChairState == HypnoticChairState.WORKING)
        {
            // 탈출하는 함수
            IsCountStart = false;
            // 플레이어가 의자에서 일어나는 함수
            // _player.GetComponent<PlayerMovement>().

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

    [PunRPC]
    /// <summary>탈출하는 함수</summary>
    public void EscapeChair()
    {
        // 플레이어가 탈출 할 때 실행할 함수
        // _player.GetComponent<PlayerMovement>().

    }


    public void OffInteraction(string tagName)
    {

        // { 생존자가 생존자가 앉은 의자에 멀어짐
        if (tagName == "Player" && ChairState == HypnoticChairState.WORKING)
        {

        }
        // } 생존자가 생존자가 앉은 의자에 멀어짐

        ChairState = HypnoticChairState.IDLE;

        // PlayerObj = default;
        foreach (Transform _obj in transform) { _obj.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", Color.white); }
    }


    // 생존자가 의자에 앉혔을 때 처형까지의 시간을 카운트 하는 코루틴
    private IEnumerator PlayerExecutionCountStart()
    {
        while (IsCountStart)
        {
            yield return new WaitForSeconds(0.01f);
            _currentExecutionTime += 0.01f;

            if (_maxExecutionTime <= _currentExecutionTime)
            {
                // 최대처형시간까지 잡혀있다면 플레이어의 스크립트를 전부 켜주고 플레이어 오브젝트를 꺼버린다.
                // PlayerObj.SetActive(false);
                // OffInteraction(PlayerObj);
                yield break;
            }
        }
    }


    [PunRPC]
    public void SurvivorSitOnChair(string ViewID)
    {
        StartCoroutine(PlayerExecutionCountStart());
        GameObject player = GameManager.FindPlayerorKiller(ViewID);
        _player = player;
        _player.GetComponent<PlayerMovement>().SitOnChair();
        _player.transform.SetParent(null);
        _player.transform.localPosition = gameObject.transform.localPosition + new Vector3(0f, 1f, 0f);
        ChairState = HypnoticChairState.WORKING;
        foreach (Transform _obj in transform) { _obj.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", Color.black); }
        IsCountStart = true;

    }   // 생존자 의자에 앉히기


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
