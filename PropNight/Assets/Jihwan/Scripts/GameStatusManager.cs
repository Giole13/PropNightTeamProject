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

    // 현재 게임의 최대 생존자 수
    public int SurvivorMaxNumber;

    // 고쳐야할 프롭머신 개수
    public int PropMachineCount = 5;
    public int PropMachineCurrentFixCount = 0;

    public bool IsCanEscape = false;

    private DataContainer _dc = default;



    // Start is called before the first frame update
    void Start()
    {
        _dc = GameObject.Find("DataContainer").GetComponent<DataContainer>();
        // Debug.Log(_dc);
        // 작동 안함 -> 코루틴으로 설정해야함.
        // StartCoroutine(CashingSurviorID());
    }

    private IEnumerator CashingSurviorID()
    {
        yield return new WaitForSeconds(1f);
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


    public void PropMachineFix()
    {
        PropMachineCount--;
        if (PropMachineCount == 0)
        {
            // 5개를 다 고친다면 문을 열어버린다.
            ExitDoorPortal.s__instance.OpenPortal();
            IsCanEscape = true;
        }
    }

    // 생존자가 죽었을 때 실행하는 함수
    [PunRPC]
    public void SurvivorDie()
    {
        SurvivorMemberNumber--;
        Debug.Log("여기서 생존자 수 감소");
        Debug.Log("여기서 씬 이동");
        Debug.Log(PhotonNetwork.IsMasterClient);
        Debug.Log(SurvivorMemberNumber);
        // 2023.05.02 / HyungJun / 마스터 클라이언트이고 생존한 플레이어가 0명이면 살인마인 경우만 승리
        if (PhotonNetwork.IsMasterClient && SurvivorMemberNumber == 0)
        {
            _dc.IsGameVictory = true;
            photonView.RPC("SceneMove", RpcTarget.All);
        }
    }

    // 플레이어가 탈출할 때 실행하는 함수
    public void SurvivorExit()
    {
        ExitMemberNumber++;
        // 만약 탈풀한 플레이어가 최대 플레이어와 같거나 많을 때
        // if (!PhotonNetwork.IsMasterClient)
        // {
        // 승리결과창 반영해주는 변수
        _dc.IsGameVictory = true;
        SceneMove();
        // photonView.RPC("SceneMove", RpcTarget.All);
        // }
    }


    // 씬을 이동시키는 함수
    [PunRPC]
    public void SceneMove()
    {
        Debug.Log("씬을 이동합니다.");
        PhotonNetwork.LoadLevel(Define.RESULT_SCENE_NAME);
    }

    // public void

}
