using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneController : MonoBehaviour
{
    private static string _nextSceneName;

    public static void LoadScene(string sceneName)
    {
        _nextSceneName = sceneName;
        SceneManager.LoadScene("04.LoadingScene");
    }

    void Start()
    {

    }

    private IEnumerator LoadSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(_nextSceneName);
        op.allowSceneActivation = false;

        float timer = 0f;
        while (op.isDone)
        {
            yield return null;
        }
    }
}
