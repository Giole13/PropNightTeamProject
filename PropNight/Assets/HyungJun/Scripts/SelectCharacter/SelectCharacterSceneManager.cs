using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class SelectCharacterSceneManager : MonoBehaviourPun, IPunObservable
{
    public TMP_Text CountDownTxt;

    public GameObject StartBtn;
    public GameObject ReadyBtn;

    public Image[] PlayerReadyImage;

    // 현재 준비완료 카운트
    private int _clientReadyCount;

    // 게임 시작 조건 준비완료 카운트
    private int _gameStartReadyCount;
    private float uiMaxTime = 60f;
    private int uiTimeMin = 0;
    private int uiTimeSec = 0;

    private bool _gameStarted = false;

    private void Awake()
    {
        _clientReadyCount = 0;
        _gameStartReadyCount = 0;
    }

    private void Start()
    {
        foreach (Image objImage in PlayerReadyImage)
        {
            objImage.color = Color.gray;
            objImage.gameObject.SetActive(false);
        }

        // 게임 시작 버튼을 활성화 한다.
        StartBtn.SetActive(true);

        // ReadyBtn.SetActive(false);
        photonView.RPC("GameStartCountUp", RpcTarget.All);
    }
    private void Update()
    {
        UiTime();
    }

    public void UiTime()
    {
        if (_gameStarted) { return; }
        uiMaxTime -= Time.deltaTime;
        // if (600f < uiMaxTime)
        // {
        //     uiMaxTime = 600f;
        // }
        if (60f <= uiMaxTime)
        {
            uiTimeMin = (int)uiMaxTime / 60;
            uiTimeSec = (int)uiMaxTime % 60;
            CountDownTxt.text = uiTimeMin.ToString("00") + " : " + uiTimeSec.ToString("00");
        }

        if (uiMaxTime < 60f)
        {
            CountDownTxt.text = "00 : " + (int)uiMaxTime;
        }
        if (uiMaxTime < 10f)
        {
            CountDownTxt.text = "00 : 0" + (int)uiMaxTime;
        }
        if (uiMaxTime <= 0f)
        {
            CountDownTxt.text = "00 : 00";
            MoveNextScene();
        }
    }   //UiTime()


    // 게임 시작 조건 변수 ++
    [PunRPC]
    public void GameStartCountUp()
    {
        if (PhotonNetwork.IsMasterClient) { _gameStartReadyCount++; }



    }

    // 준비완료 카운트 ++
    [PunRPC]
    public void ReadyCountUp()
    {
        PlayerReadyImage[_clientReadyCount].color = Color.red;
        _clientReadyCount++;
    }


    // 게임시작 버튼
    public void MoveNextScene()
    {
        // 선택한 캐릭터의 정보를 가져와서 캐싱해야한다.

        photonView.RPC("ReadyCountUp", RpcTarget.AllBuffered);
        photonView.RPC("MoveSceneProgress", RpcTarget.All);
        StartBtn.GetComponent<Button>().interactable = false;
    }

    [PunRPC]
    public void MoveSceneProgress()
    {
        // 게임 시작 버튼을 누르면 60초의 카운트 다운이 시작되고 준비 됨 버튼을 활성화 한다.
        // StartBtn.SetActive(false);
        // ReadyBtn.SetActive(true);


        // 게임 시작을 누르면 준비완료 카운트 +1
        if (_gameStartReadyCount <= _clientReadyCount)
        {
            _gameStarted = true;
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

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            stream.SendNext(_gameStartReadyCount);
        }
        else
        {
            _gameStartReadyCount = (int)stream.ReceiveNext();
        }


        for (int i = 0; i < _gameStartReadyCount; i++)
        {
            PlayerReadyImage[i].gameObject.SetActive(true);
        }
    }
}
