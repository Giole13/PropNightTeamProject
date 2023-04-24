using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TextMousePoint : MonoBehaviour
, IPointerEnterHandler, IPointerExitHandler
, IPointerUpHandler, IPointerDownHandler
, IPointerClickHandler
{

    public GameObject gameObject = default;
    //Color32 캐싱
    Color32 colorRed = new Color32(200, 68, 75, 255);
    Color32 colorWhite = new Color32(200, 200, 200, 200);
    RectTransform rectTransform = default;
    TMP_Text text = default;
    void Start()
    {
        text = gameObject.GetComponent<TMP_Text>();
        rectTransform = gameObject.GetComponent<RectTransform>();

    }

    public void OnPointerEnter(PointerEventData data)
    {
        text.color = colorRed;
    }   //OnPointerEnter()

    public void OnPointerExit(PointerEventData data)
    {
        text.color = colorWhite;
    }   //OnPointerExit()

    public void OnPointerUp(PointerEventData data)
    {
        rectTransform.anchoredPosition = new Vector3(gameObject.GetComponent<RectTransform>().anchoredPosition.x, gameObject.GetComponent<RectTransform>().anchoredPosition.y + 5, 0);
    }   //OnPointerUp()

    public void OnPointerDown(PointerEventData data)
    {
        rectTransform.anchoredPosition = new Vector3(gameObject.GetComponent<RectTransform>().anchoredPosition.x, gameObject.GetComponent<RectTransform>().anchoredPosition.y - 5, 0);
    }   //OnPointerDown()

    public void OnPointerClick(PointerEventData data)
    {
        if (UIManager.Instance.isClick == false)
        {
            text.color = colorRed;
            text.text = "> " + gameObject.GetComponent<TMP_Text>().text;
        }
        else
        {

        }

    }


}
