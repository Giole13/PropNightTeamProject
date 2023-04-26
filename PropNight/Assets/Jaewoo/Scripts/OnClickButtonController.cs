using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickButtonController : MonoBehaviour
{
    public GameObject topPlayObj = default;
    public GameObject topCustomObj = default;
    public GameObject topAvatoarObj = default;

    public void ClickTopPlay()
    {

    }
    public void ClickTopCustom()
    {
        if (UIManager.isButton == false)
        {
            UIManager.isButton = true;
        }

    }
    public void ClickTopOption()
    {
        if (UIManager.isButton == false)
        {
            UIManager.isButton = true;
        }
    }
    public void ClickCustomCharactor()
    {
        if (UIManager.isButton == false)
        {
            UIManager.isButton = true;
        }
    }
    public void ClickCustomAvatar()
    {
        if (UIManager.isButton == false)
        {
            UIManager.isButton = true;
        }
    }
    public void ClickCustomBanner()
    {
        if (UIManager.isButton == false)
        {
            UIManager.isButton = true;
        }
    }

}
