using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public TMP_Text start = default;
    public GameObject button = default;


    public void Cancel()
    {

        if (this.GetComponent<Button>().interactable == true)
            this.GetComponent<Button>().interactable = false;
    }


}
