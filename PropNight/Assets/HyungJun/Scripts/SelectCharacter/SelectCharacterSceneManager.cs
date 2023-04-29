using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacterSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // 다음 씬으로 넘어가는 함수
    public void MoveNextScene()
    {
        LoadingSceneController.MoveScene();
    }
}
