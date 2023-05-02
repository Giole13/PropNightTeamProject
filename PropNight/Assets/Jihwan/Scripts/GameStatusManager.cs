using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class GameStatusManager : MonoBehaviourPun
{
    // 생존자 각각의 남은 목숨,수리해야 되는 프롭머신 갯수
    // 생존자의 목숨이 줄어드는 것을 모든 클라이언트에서 적용
    // 남은 프롭머신 갯수를 모든 클라이언트에서 적용

    public int[] SurvivorID = new int[4];
    public int[] SurvivorLife = new int[4] { 2, 2, 2, 2 };
    public bool[] IsSurvivorCanDie = new bool[4] { false, false, false, false };
    public int SurvivorMemberNumber;
    public int PropMachineCount = 5;

    public bool IsCanEscape = false;
    // Start is called before the first frame update
    void Start()
    {
        int Count = 0;
        foreach (var obj in GameObject.FindGameObjectsWithTag("Player"))
        {
            SurvivorID[Count] = obj.GetPhotonView().ViewID;
            Count++;
        }
        SurvivorMemberNumber = Count;
    }

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
    [PunRPC]
    public void SurvivorDie()
    {
        SurvivorMemberNumber--;
    }

}
