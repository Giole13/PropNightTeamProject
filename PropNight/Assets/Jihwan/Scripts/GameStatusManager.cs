using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
public class GameStatusManager : MonoBehaviourPun
{
    // 생존자 각각의 남은 목숨,수리해야 되는 프롭머신 갯수
    // 생존자의 목숨이 줄어드는 것을 모든 클라이언트에서 적용
    // 남은 프롭머신 갯수를 모든 클라이언트에서 적용

    // 생존자 아이디
    public int[] SurvivorID = new int[4];
    // 생존자 목숨
    public int[] SurvivorLife = new int[4] { 2, 2, 2, 2 };
    // 생존자 생존 여부
    public bool[] IsSurvivorCanDie = new bool[4] { false, false, false, false };
    // 생존하고 있는 캐릭터 수
    public int SurvivorMemberNumber;
    // 탈출한 캐릭터 수
    public int ExitMemberNumber;
    public int PropMachineCount = 5;

    public bool IsCanEscape = false;

    private DataContainer _dc = default;



    // Start is called before the first frame update
    void Start()
    {
        _dc = GameObject.Find("DataContainer").GetComponent<DataContainer>();
        // Debug.Log(_dc);
        int Count = 0;
        foreach (var obj in GameObject.FindGameObjectsWithTag("Player"))
        {
            SurvivorID[Count] = obj.GetPhotonView().ViewID;
            Count++;
        }
        SurvivorMemberNumber = Count;
    }


    // 생존자가 쓰러졌을 때 실행하는 함수
    [PunRPC]
    public void SurvivorFallDown(int ViewID)
    {

        for (int i = 0; i < SurvivorID.Length; i++)
        {
            if (SurvivorID[i] == ViewID)
            {
                SurvivorLife[i]--;
                if (SurvivorLife[i] == 0)
                {
                    IsSurvivorCanDie[i] = true;
                }
                break;
            }
        }
    }


    [PunRPC]
    public void PropMachineFix()
    {
        PropMachineCount--;
        if (PropMachineCount == 0)
        {
            IsCanEscape = true;
        }
    }

    // 생존자가 죽었을 때 실행하는 함수
    [PunRPC]
    public void SurvivorDie()
    {
        SurvivorMemberNumber--;

        // 2023.05.02 / HyungJun / 마스터 클라이언트이고 생존한 플레이어가 0명이면 살인마인 경우만 승리
        if (PhotonNetwork.IsMasterClient && SurvivorMemberNumber == 0)
        {
            _dc.IsGameVictory = true;
            photonView.RPC("SceneMove", RpcTarget.All);
        }
    }

    // 씬을 이동시키는 함수
    [PunRPC]
    public void SceneMove()
    {
        SceneManager.LoadScene(Define.RESULT_SCENE_NAME);
    }

    // public void

}
