using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestLobbyStart : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadScene("04.LoadingScene");
    }
}
