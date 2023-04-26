using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{


    //마우스 클릭을 여부
    public static bool isButton = false;
    // public bool isTopCustomButton = false;

    // public bool isTopOptionButton = false;

    // public bool isCustomCharactorButton = false;

    // public bool isCustomAvatarButton = false;

    // public bool isCustomBannerButton = false;
    //public string selectUiName = default;

    public int[] topPanel = new int[3];

    public GameObject uiGameObject = default;
    private static UIManager _instance = default;

    public static UIManager Instance
    {
        get
        {
            if (_instance == default || _instance == null)
            {
                UIManager _instance = new UIManager();
                // DontDestroyOnLoad(_instance.gameObject);
            }       // if: 인스턴스가 없다면 새로 생성한다.

            // 생성한 인스턴스를 리턴한다.
            return _instance;
        }
    }












}
