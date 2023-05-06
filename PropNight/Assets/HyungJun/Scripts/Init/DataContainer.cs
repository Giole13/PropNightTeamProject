using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임의 정보를 담고있는 클래스
public class DataContainer : MonoBehaviour
{
    // 플레이어가 선택한 캐릭터 넘버
    public static int PlayerSelectNumber = 0;

    // 킬러가 선택한 캐릭터 넘버
    public static int KillerSelectNumber = 0;

    // 로컬의 승리와 패배를 구분하는 변수
    public bool IsGameVictory = false;

    // public static GameObject ClientObject = default;


    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // 마스터가 살인마를 골랐을 때 실행하는 함수
    public void SelectKiller(int _number) => KillerSelectNumber = _number;

    // 플레이어가 생존자를 골랐을 때 실행하는 함수
    public void SelectPlayer(int _number) => PlayerSelectNumber = _number;






}
