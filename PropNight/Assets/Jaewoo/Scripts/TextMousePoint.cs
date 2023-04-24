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
    private int Count = 0;

    Color32 colorRed = new Color32(200, 68, 75, 255);
    RectTransform rectTransform = default;

    public TMP_Text text = default;
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
        text.color = new Color32(255, 255, 255, 255);
    }   //OnPointerExit()

    public void OnPointerUp(PointerEventData data)
    {
        Count = 0;
        rectTransform.anchoredPosition = new Vector3(gameObject.GetComponent<RectTransform>().anchoredPosition.x, gameObject.GetComponent<RectTransform>().anchoredPosition.y + 5, 0);
    }   //OnPointerUp()

    public void OnPointerDown(PointerEventData data)
    {
        rectTransform.anchoredPosition = new Vector3(gameObject.GetComponent<RectTransform>().anchoredPosition.x, gameObject.GetComponent<RectTransform>().anchoredPosition.y - 5, 0);
    }   //OnPointerDown()

    public void OnPointerClick(PointerEventData data)
    {
        if (Count < 1)
        {
            text.color = new Color32(200, 68, 75, 255);
            text.text = "> " + gameObject.GetComponent<TMP_Text>().text;
        }
        Count++;
    }


}
