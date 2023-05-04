using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class SelectCharacterSceneManager : MonoBehaviourPun
{
    public TMP_Text CountDownTxt;

    public GameObject StartBtn;
    public GameObject ReadyBtn;

    // 현재 준비완료 카운트
    private int _clientReadyCount;

    // 게임 시작 조건 준비완료 카운트
    private int _gameStartReadyCount;

    private void Awake()
    {
        _clientReadyCount = 0;
        _gameStartReadyCount = 0;
    }

    private void Start()
    {
        // 게임 시작 버튼을 활성화 한다.
        StartBtn.SetActive(true);

        // ReadyBtn.SetActive(false);
        photonView.RPC("GameStartCountUp", RpcTarget.AllBuffered);

    }

    // 게임 시작 조건 변수 ++
    [PunRPC] public void GameStartCountUp() => _gameStartReadyCount++;

    // 준비완료 카운트 ++
    [PunRPC] public void ReadyCountUp() => _clientReadyCount++;


    // 게임시작 버튼
    public void MoveNextScene()
    {
        // 선택한 캐릭터의 정보를 가져와서 캐싱해야한다.

        photonView.RPC("ReadyCountUp", RpcTarget.All);
        photonView.RPC("MoveSceneProgress", RpcTarget.All);
    }

    [PunRPC]
    public void MoveSceneProgress()
    {
        // 게임 시작 버튼을 누르면 60초의 카운트 다운이 시작되고 준비 됨 버튼을 활성화 한다.
        // StartBtn.SetActive(false);
        // ReadyBtn.SetActive(true);

        StartBtn.GetComponent<Button>().interactable = false;

        // 게임 시작을 누르면 준비완료 카운트 +1
        if (_gameStartReadyCount <= _clientReadyCount)
        {
            StartCoroutine(StartGameCountDown());
        }


        // // 마스터 클라이언트가 시작을 누르면 게임 시작
        // if (PhotonNetwork.IsMasterClient)
        // {
        //     StartCoroutine(StartGameCountDown());
        // }
        // // 생존자가 시작을 누르면 준비완료 카운트 +1
        // else
        // {

        // }


    }


    // 카운트 다운이 실행되는 코루틴
    private IEnumerator StartGameCountDown()
    {
        for (int i = 5; 0 < i; i--)
        {
            CountDownTxt.text = string.Format("00:0" + i);
            yield return new WaitForSecondsRealtime(1f);
        }
        LoadingSceneController.MoveScene();
    }
}
