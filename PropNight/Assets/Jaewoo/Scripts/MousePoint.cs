using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class MousePoint : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    Image image = default;
    Color32 colorDefaultBg = new Color32(36, 24, 24, 255);
    Color32 colorOnMouseBg = new Color32(170, 170, 170, 255);

    void Start()
    {
        image = this.gameObject.GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData data)
    {
        image.color = colorOnMouseBg;
    }

    public void OnPointerExit(PointerEventData data)
    {
        image.color = colorDefaultBg;
    }

    public void OnPointerClick(PointerEventData data)
    {

    }
}
