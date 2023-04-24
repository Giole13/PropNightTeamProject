using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class MousePoint : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    Image image = default;
    Color colorDefaultBg = new Color32(36, 24, 24, 255);
    Color colorOnMouseBg = new Color32(170, 170, 170, 255);

    void start()
    {
        image = this.gameObject.GetComponent<Image>();
        //colorDefaultBg = new Color32(36, 24, 24, 255);
        //colorOnMouseBg = new Color32(170, 170, 170, 255);

    }


    public void OnPointerEnter(PointerEventData data)
    {
        image = this.gameObject.GetComponent<Image>();
        //Color color = new Color32(205, 205, 205, 255);
        image.color = colorOnMouseBg;
    }

    public void OnPointerExit(PointerEventData data)
    {
        image = this.gameObject.GetComponent<Image>();

        image.color = colorDefaultBg;
    }

    public void OnPointerClick(PointerEventData data)
    {

    }
}
