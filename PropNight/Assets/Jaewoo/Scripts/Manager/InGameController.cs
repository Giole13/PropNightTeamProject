using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameController : MonoBehaviour
{
    private static InGameController s_inGameController = null;
    public static InGameController s_intance
    {
        get
        {
            return s_inGameController;
        }
    }

    public float uiMaxTime = 600f;
    public int uiTimeMin = 0;
    public int uiTimeSec = 0;
    public int uiPropMachineCount = 0;
    public int uiLivePlayerCount = 0;





    public TMP_Text timeText = default;
    public TMP_Text propMachineText = default;
    public TMP_Text manHoleText = default;
    public TMP_Text coinText = default;


    void Awake()
    {
        s_inGameController = this;

        propMachineText.text = uiPropMachineCount + "/5";
        uiMaxTime = 600f;
        uiPropMachineCount = 0;
        uiLivePlayerCount = 0;
    }


    void Update()
    {
        UiTime();
    }

    #region 게임 시간
    public void UiTime()
    {
        uiMaxTime -= Time.deltaTime;
        if (600f < uiMaxTime)
        {
            uiMaxTime = 600f;
        }
        if (60f <= uiMaxTime)
        {
            uiTimeMin = (int)uiMaxTime / 60;
            uiTimeSec = (int)uiMaxTime % 60;
            timeText.text = uiTimeMin.ToString("00") + " : " + uiTimeSec.ToString("00");
        }

        if (uiMaxTime < 60f)
        {
            timeText.text = "00 : " + (int)uiMaxTime;
        }
        if (uiMaxTime < 10f)
        {
            timeText.text = "00 : 0" + (int)uiMaxTime;
        }
        if (uiMaxTime <= 0f)
        {
            timeText.text = "00 : 00";
        }
    }   //UiTime()
    #endregion

    #region 프롭머쉰카운트
    // 2023.05.04 / HyungJun / 이 친구를 다른 PhotonView가 붙어있는 친구에서 불러주면 된다.
    public void UiPropMachineCount()
    {
        uiPropMachineCount++;
        //카운트 올라갈시 시간 10분으로 초기화
        uiMaxTime += 120f;

        if (uiPropMachineCount < 5)
        {
            propMachineText.text = uiPropMachineCount + "/5";

        }
        if (uiPropMachineCount == 5)
        {
            propMachineText.text = "출구를 찾아라";
        }
    }   //UiPropMachineCount
    #endregion 

    #region 하수구
    public void UiManhole()
    {
        //uiLivePlayerCount 대신 살아남은 플레이어 카운트를 받아와야함
        if (3 < uiPropMachineCount && uiLivePlayerCount == 1)
        {
            manHoleText.text = "하수구가 열렸습니다";
        }
    }
    #endregion



}
