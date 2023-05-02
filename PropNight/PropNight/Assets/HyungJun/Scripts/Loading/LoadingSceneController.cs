using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;


//! 로딩 씬을 컨트롤 하기위한 스크립트
public class LoadingSceneController : MonoBehaviourPun
{
    private static string _nextSceneName;
    private static AsyncOperation _mainOp;
    private static AsyncOperation _subOp;

    [SerializeField]
    private Image progressBar;

    public static void LoadScene(string sceneName)
    {
        _nextSceneName = sceneName;
        SceneManager.LoadScene(Define.LOADING_SCENE_NAME);
    }

    void Start()
    {
        StartCoroutine(LoadSceneProcess());
        // StartCoroutine(LoadSceneDelay());

    }

    // { 2023.04.29 / HyungJun / Lagacy
    // private IEnumerator LoadSceneDelay()
    // {
    //     yield return null;
    //     PhotonNetwork.LoadLevel(_nextSceneName);
    //     // PhotonNetwork.
    // }
    // } 2023.04.29 / HyungJun / Lagacy


    private IEnumerator LoadSceneProcess()
    {
        yield return null;

        // 캐릭터 선택 창 로딩
        _subOp = SceneManager.LoadSceneAsync(Define.SELECT_CHARACTER_SCENE_NAME, LoadSceneMode.Single);
        _subOp.allowSceneActivation = false;

        // AsyncOperation 에서 다음 씬을 불러올때 한번에 하나의 씬만 불러온다.
        // LoadSceneAsync 함수의 역할은 비동기 형식으로 씬을 불러온다.

        float timer = 0f;
        while (!_subOp.isDone)
        {
            yield return null;
            if (_subOp.progress < 0.9f) { progressBar.fillAmount = _subOp.progress; }
            else
            {
                timer += Time.unscaledDeltaTime;
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
                // 진행도 Bar 가 꽉 차거나 넘어갔을 때 다음 씬으로 넘어간다.
                // Debug.Log("매인 맵씬 로딩 진행도 : " + secondOp.progress);
                // Debug.Log("캐릭터 선택씬 로딩 진행도 : " + op.progress);
                // Debug.Log("현재 씬의 이름 : " + SceneManager.GetActiveScene().name);

                if (progressBar.fillAmount >= 1f /*&& secondOp.progress >= 0.9f*/)
                {
                    // Scene currentScene = SceneManager.GetActiveScene();
                    // currentScene.
                    // Application.LoadLevel()
                    // AsyncOperation loadSceneOp = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
                    // loadSceneOp.allowSceneActivation = false;
                    _subOp.allowSceneActivation = true;
                    break;
                }
            }
        }

        // 캐릭터 선택 씬으로 넘어간 뒤 메인 맵의 씬을 미리 불러온다.
        _mainOp = SceneManager.LoadSceneAsync(_nextSceneName, LoadSceneMode.Single);
        _mainOp.allowSceneActivation = false;
    }   // LoadSceneProcess()

    // 모든 플레이어가 준비가 완료된다면 다음 씬으로 넘어간다.
    public static void MoveScene() => _mainOp.allowSceneActivation = true;


}   // class LoadingSceneController
