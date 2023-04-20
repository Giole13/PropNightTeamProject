using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomData : MonoBehaviour
{
    public string RoomName = "";
    public int PlayerCount = 0;
    public int MaxPlayers = 0;

    public Button Btn;

    public TMPro.TMP_Text RoomDataTxt;

    public void UpdateInfo()
    {
        RoomDataTxt.text = string.Format("{0} [{1}/{2}]"
                                        , RoomName
                                        , PlayerCount.ToString("00")
                                        , MaxPlayers);
    }
}
