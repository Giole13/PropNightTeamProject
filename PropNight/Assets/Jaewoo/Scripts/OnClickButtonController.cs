using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickButtonController : MonoBehaviour
{
    public GameObject topPlayObj = default;
    public GameObject topCustomObj = default;
    public GameObject topOptionObj = default;
    public GameObject LobbyListObj = default;

    private void Start()
    {
        foreach (Transform _tr in LobbyListObj.transform)
        {
            _tr.gameObject.SetActive(false);
        }
    }

    public void ClickTopPlay()
    {

        Debug.Log(topPlayObj.name);

        Debug.Log(gameObject.name);

        topPlayObj.SetActive(true);
        topCustomObj.SetActive(false);
        topOptionObj.SetActive(false);

    }
    public void ClickTopCustom()
    {

        topPlayObj.SetActive(false);
        topCustomObj.SetActive(true);
        topOptionObj.SetActive(false);
    }
    public void ClickTopOption()
    {
        topPlayObj.SetActive(false);
        topCustomObj.SetActive(false);
        topOptionObj.SetActive(true);
    }
    public void ClickCustomCharactor()
    {
    }
    public void ClickCustomAvatar()
    {

    }
    public void ClickCustomBanner()
    {

    }


    // 2023.04.28 / HyungJun / 사용자 설정 게임을 눌렀을 때 작동하는 함수
    public void ClickUserCustomGame(bool Isactive)
    {
        // 로비를 끄고 방 목록을 켜준다.
        topPlayObj.SetActive(!Isactive);
        LobbyListObj.SetActive(Isactive);
        LobbyListObj.transform.GetChild(0).gameObject.SetActive(Isactive);
    }

    // 2023.04.28 / HyungJun / 로비 리스트에서 방 생성을 눌렀을 때 작동하는 함수
    public void ClickCreateRoom(bool Isactive)
    {
        // 로비 리스트를 끄고 방 생성을 호출한다.
        LobbyListObj.transform.GetChild(0).gameObject.SetActive(!Isactive);
        LobbyListObj.transform.GetChild(1).gameObject.SetActive(Isactive);
    }


}
