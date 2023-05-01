using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MouseController : MonoBehaviour
{
    public GameObject charObj = default;
    public GameObject avaObj = default;
    public GameObject benObj = default;
    public TMP_Text characterText = default;
    public TMP_Text avatorText = default;
    public TMP_Text bennerText = default;
    Color32 inMouse = new Color32(200, 79, 80, 255);
    Color32 outMouse = new Color32(255, 255, 255, 255);
    public bool isUseChar = false;
    public bool isUseAva = false;
    public bool isUseBen = false;
    void Start()
    {
        charObj.SetActive(true);
        avaObj.SetActive(false);
        benObj.SetActive(false);
        characterText.color = inMouse;
    }
    public void OnMouseEnterChar()
    {
        characterText.color = inMouse;
    }
    public void OnMouseEnterAva()
    {
        avatorText.color = inMouse;
    }
    public void OnMouseEnterBen()
    {
        bennerText.color = inMouse;
    }

    //Exit mouse
    public void OnMouseExit()
    {
        if (isUseChar == false)
        {
            characterText.color = outMouse;
        }
    }
    public void OnMouseExitAva()
    {
        if (isUseChar == false)
        {
            avatorText.color = outMouse;
        }
    }
    public void OnMouseExitBen()
    {
        if (isUseChar == false)
        {
            bennerText.color = outMouse;
        }
    }

    public void OnMouseClickChar()
    {
        isUseChar = true;
        if (isUseChar == true)
        {
            charObj.SetActive(true);
            avaObj.SetActive(false);
            benObj.SetActive(false);
            characterText.color = inMouse;
            avatorText.color = outMouse;
            bennerText.color = outMouse;
        }

    }
    public void OnMouseClickAva()
    {
        isUseChar = true;
        if (isUseChar == true)
        {
            charObj.SetActive(false);
            avaObj.SetActive(true);
            benObj.SetActive(false);
            characterText.color = outMouse;
            avatorText.color = inMouse;
            bennerText.color = outMouse;
        }
    }

    public void OnMouseClickBen()
    {
        isUseChar = true;
        if (isUseChar == true)
        {
            charObj.SetActive(false);
            avaObj.SetActive(false);
            benObj.SetActive(true);
            characterText.color = outMouse;
            avatorText.color = outMouse;
            bennerText.color = inMouse;
        }

    }
}
