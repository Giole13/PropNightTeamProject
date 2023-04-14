using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextCount : MonoBehaviour
{
    public GameObject inGameController = default;

    public void PlusCount()
    {
        inGameController = transform.parent.parent.parent.gameObject;

        inGameController.GetComponent<InGameController>().UiPropMachineCount();
    }

    public void LivePlayer()
    {
        inGameController = transform.parent.parent.parent.gameObject;
        inGameController.GetComponent<InGameController>().uiLivePlayerCount = 1;


        inGameController.GetComponent<InGameController>().UiManhole();
    }
}
