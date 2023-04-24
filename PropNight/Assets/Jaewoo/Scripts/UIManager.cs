using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    enum TopMenu
    {
        None = 0,
        Play,
        Customs,
        Option
    }

    enum Custom
    {
        None = 0,
        Charactor,
        Avatar,
        Banner
    }
    //마우스 클릭을 여부
    public bool isClick = false;
    public GameObject uiGameObject = default;
    private static UIManager _instance = default;

    public static UIManager Instance
    {
        get
        {
            if (_instance == default || _instance == null)
            {
                _instance = new UIManager();
                DontDestroyOnLoad(_instance.gameObject);
            }       // if: 인스턴스가 없다면 새로 생성한다.

            // 생성한 인스턴스를 리턴한다.
            return _instance;
        }
    }








}
