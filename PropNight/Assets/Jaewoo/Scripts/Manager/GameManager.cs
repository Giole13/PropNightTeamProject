using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int time = 0;
    public int propMachineCount = 0;
    public int DownCount = 0;
    public int killCount = 0;
    private static GameManager _GameManager;
    public static GameManager s_GameManager
    {
        get
        {
            return s_GameManager;
        }
    }

    void Awake()
    {
        if (_GameManager)
        {
            Destroy(gameObject);
            return;
        }
        _GameManager = this;

    }


}
