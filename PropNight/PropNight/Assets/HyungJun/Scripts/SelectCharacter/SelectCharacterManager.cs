using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class SelectCharacterManager : MonoBehaviourPun
{
    public TMP_Text CountDownTxt;


    // 다음 씬으로 넘어가는 함수
    public void MoveNextScene()
    {
        photonView.RPC("MoveSceneProgress", RpcTarget.All);
    }

    [PunRPC]
    public void MoveSceneProgress()
    {
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
