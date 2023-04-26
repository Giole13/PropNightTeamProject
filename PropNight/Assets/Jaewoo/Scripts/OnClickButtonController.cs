using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickButtonController : MonoBehaviour
{
    public GameObject topPlayObj = default;
    public GameObject topCustomObj = default;
    public GameObject topOptionObj = default;



    public void ClickTopPlay()
    {

        Debug.Log(topPlayObj.name);

        Debug.Log(gameObject.name);

        topPlayObj.SetActive(true);
        topCustomObj.SetActive(false);
        topOptionObj.SetActive(false);

    }
    public void ClickTopCustom()
    {

        topPlayObj.SetActive(false);
        topCustomObj.SetActive(true);
        topOptionObj.SetActive(false);
    }
    public void ClickTopOption()
    {
        topPlayObj.SetActive(false);
        topCustomObj.SetActive(false);
        topOptionObj.SetActive(true);
    }
    public void ClickCustomCharactor()
    {
    }
    public void ClickCustomAvatar()
    {

    }
    public void ClickCustomBanner()
    {

    }

}
