using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameSceneManager : MonoBehaviour
{


    void Start()
    {

    }


    // 게임이 끝났고 결과창으로 넘어가는 함수
    public static void SceneMoveForGameEnd()
    {
        SceneManager.LoadScene(Define.RESULT_SCENE_NAME);
    }

}
