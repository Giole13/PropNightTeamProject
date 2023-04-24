using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    private static UIManager _instance = default;
    // private static GameManager _instance = default;

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
