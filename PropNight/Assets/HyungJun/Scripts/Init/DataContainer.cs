using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataContainer : MonoBehaviour
{
    // 플레이어가 선택한 캐릭터 넘버
    public static int PlayerSelectNumber = 0;

    // 킬러가 선택한 캐릭터 넘버
    public static int KillerSelectNumber = 0;


    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // 마스터가 살인마를 골랐을 때 실행하는 함수
    public void SelectKiller(int _number) => KillerSelectNumber = _number;

    // 플레이어가 생존자를 골랐을 때 실행하는 함수
    public void SelectPlayer(int _number) => PlayerSelectNumber = _number;


    // Update is called once per frame
    void Update()
    {

    }


}
