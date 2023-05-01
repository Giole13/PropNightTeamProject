using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class SelectCharacterSceneManager : MonoBehaviourPun
{
    public TMP_Text CountDownTxt;

    public GameObject StartBtn;
    public GameObject ReadyBtn;


    private void Start()
    {
        // 게임 시작 버튼을 활성화 한다.
        StartBtn.SetActive(true);
        ReadyBtn.SetActive(false);
    }

    // 다음 씬으로 넘어가는 함수
    public void MoveNextScene()
    {
        photonView.RPC("MoveSceneProgress", RpcTarget.All);
    }

    [PunRPC]
    public void MoveSceneProgress()
    {
        // 게임 시작 버튼을 누르면 60초의 카운트 다운이 시작되고 준비 됨 버튼을 활성화 한다.
        StartBtn.SetActive(false);
        ReadyBtn.SetActive(true);
        StartCoroutine(StartGameCountDown());
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
