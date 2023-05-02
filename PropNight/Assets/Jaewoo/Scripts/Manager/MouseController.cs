using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MouseController : MonoBehaviour
{
    #region CustomGroup
    public GameObject charObj = default;
    public GameObject avaObj = default;
    public GameObject benObj = default;
    public TMP_Text characterText = default;
    public TMP_Text avatorText = default;
    public TMP_Text bennerText = default;

    public bool isUseChar = false;
    public bool isUseAva = false;
    public bool isUseBen = false;
    #endregion

    #region OptionGroup
    public GameObject screenObj = default;
    public GameObject keyBoardObj = default;
    public GameObject soundObj = default;
    public TMP_Text screenText = default;
    public TMP_Text keyBoardText = default;
    public TMP_Text soundText = default;

    public bool isUseScreen = false;
    public bool isUseKeyBoard = false;
    public bool isUseSound = false;
    #endregion

    Color32 inMouse = new Color32(200, 79, 80, 255);
    Color32 outMouse = new Color32(255, 255, 255, 255);

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
        if (isUseAva == false)
        {
            avatorText.color = outMouse;
        }
    }
    public void OnMouseExitBen()
    {
        if (isUseBen == false)
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
            isUseAva = false;
            isUseBen = false;
        }

    }
    public void OnMouseClickAva()
    {
        isUseAva = true;
        if (isUseAva == true)
        {
            charObj.SetActive(false);
            avaObj.SetActive(true);
            benObj.SetActive(false);
            characterText.color = outMouse;
            avatorText.color = inMouse;
            bennerText.color = outMouse;
            isUseChar = false;
            isUseBen = false;
        }
    }

    public void OnMouseClickBen()
    {
        isUseBen = true;
        if (isUseBen == true)
        {
            charObj.SetActive(false);
            avaObj.SetActive(false);
            benObj.SetActive(true);
            characterText.color = outMouse;
            avatorText.color = outMouse;
            bennerText.color = inMouse;
            isUseChar = false;
            isUseAva = false;
        }
    }

    public void OnMouseScreen()
    {
        screenText.color = inMouse;
    }
    public void OnMouseKeyBoard()
    {
        keyBoardText.color = inMouse;
    }
    public void OnMouseSound()
    {
        soundText.color = inMouse;
    }

    public void OnMouseScreenExit()
    {
        if (isUseScreen == false)
        {
            screenText.color = outMouse;
        }

    }
    public void OnMouseKeyBoardExit()
    {
        if (isUseKeyBoard == false)
        {
            keyBoardText.color = outMouse;
        }
    }
    public void OnMouseSoundExit()
    {
        if (isUseSound == false)
        {
            soundText.color = outMouse;
        }
    }

    public void OnMouseClickScreen()
    {
        isUseScreen = true;
        if (isUseScreen == true)
        {
            screenObj.SetActive(true);
            keyBoardObj.SetActive(false);
            soundObj.SetActive(false);
            screenText.color = inMouse;
            keyBoardText.color = outMouse;
            soundText.color = outMouse;
            isUseKeyBoard = false;
            isUseSound = false;
        }
    }
    public void OnMouseClickKeyBoard()
    {
        isUseKeyBoard = true;
        if (isUseKeyBoard == true)
        {
            screenObj.SetActive(false);
            keyBoardObj.SetActive(true);
            soundObj.SetActive(false);
            screenText.color = outMouse;
            keyBoardText.color = inMouse;
            soundText.color = outMouse;
            isUseScreen = false;
            isUseSound = false;
        }
    }
    public void OnMouseClickSound()
    {
        isUseSound = true;
        if (isUseSound == true)
        {
            screenObj.SetActive(false);
            keyBoardObj.SetActive(false);
            soundObj.SetActive(true);
            screenText.color = outMouse;
            keyBoardText.color = outMouse;
            soundText.color = inMouse;
            isUseScreen = false;
            isUseKeyBoard = false;
        }
    }
}
