using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class EventTrigger : MonoBehaviour

//, IPointerClickHandler
{
    public string uiName = string.Empty;
    public GameObject gameObject_ = default;

    //Color32 캐싱
    Color32 colorRed = new Color32(230, 79, 88, 255);
    Color32 colorWhite = new Color32(255, 255, 255, 255);
    RectTransform rectTransform = default;
    TMP_Text text = default;

    void Start()
    {
        uiName = gameObject.name;
        text = gameObject.GetComponent<TMP_Text>();
        rectTransform = gameObject.GetComponent<RectTransform>();

    }   //Start()

    public void OnPointerEnter()
    {
        //Debug.Log(UIManager.Instance.selectUiName);
        text.color = colorRed;

    }   //OnPointerEnter()

    public void OnPointerExit()
    {
        text.color = colorWhite;
    }   //OnPointerExit()

    public void OnPointerUp()
    {
        rectTransform.anchoredPosition = new Vector3(gameObject.GetComponent<RectTransform>().anchoredPosition.x, gameObject.GetComponent<RectTransform>().anchoredPosition.y + 1, 0);
    }   //OnPointerUp()

    public void OnPointerDown()
    {
        rectTransform.anchoredPosition = new Vector3(gameObject.GetComponent<RectTransform>().anchoredPosition.x, gameObject.GetComponent<RectTransform>().anchoredPosition.y - 1, 0);
    }   //OnPointerDown()

    // public void OnPointerClick(PointerEventData data)
    // {
    //     if (gameObject.transform.Find(""))
    //         text.color = colorRed;
    //     text.text = "> " + gameObject.GetComponent<TMP_Text>().text;
    //     // if (UIManager.Instance.isClick == false)
    //     // {
    //     //    
    //     // }
    //     // else
    //     // {

    //     // }

    // }  // OnPointerClick()


}

