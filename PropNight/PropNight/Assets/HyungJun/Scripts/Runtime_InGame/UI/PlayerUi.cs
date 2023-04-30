using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUi : MonoBehaviour
{
    private static PlayerUi s_playerUi = null;
    public static PlayerUi s_instance
    {
        get { return s_playerUi; }
        private set { s_playerUi = value; }
    }


    public GameObject InteractionInfo = default;
    private Image _interactionGaugeBarImage = default;
    private TMP_Text _infomationTxt = default;

    private void Awake()
    {
        s_instance = this;

        _interactionGaugeBarImage = InteractionInfo.transform.GetChild(1).GetComponent<Image>();
        _infomationTxt = _interactionGaugeBarImage.transform.GetChild(0).GetComponent<TMP_Text>();
    }

    private void Start()
    {
        InteractionInfo.SetActive(false);
    }




    /// <summary>상호작용 UI의 게이지바의 수치를 변경하는 함수</summary>
    /// <param name="fValue">0 ~ 1의 float 값</param>
    public void FixingPropMachine(float fValue) => _interactionGaugeBarImage.fillAmount = fValue;

    /// <summary>상호작용 UI의 텍스트를 변경하는 함수</summary>
    /// <param name="text">변경할 텍스트</param>
    public void SetInterationTxt(string text) => _infomationTxt.text = text;



}
