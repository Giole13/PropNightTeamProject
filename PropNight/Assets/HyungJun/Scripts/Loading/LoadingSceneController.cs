using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneController : MonoBehaviour
{
    private static string _nextSceneName;

    [SerializeField]
    private Image progressBar;

    public static void LoadScene(string sceneName)
    {
        _nextSceneName = sceneName;
        SceneManager.LoadScene(Define.LOADING_SCENE_NAME);

    }

    void Start()
    {
        // StartCoroutine(LoadSceneProcess());

    }



    private IEnumerator LoadSceneProcess()
    {
        yield return null;

        AsyncOperation op = SceneManager.LoadSceneAsync(_nextSceneName);
        op.allowSceneActivation = false;

        float timer = 0f;
        while (!op.isDone)
        {
            yield return null;
            if (op.progress < 0.9f) { progressBar.fillAmount = op.progress; }
            else
            {
                timer += Time.unscaledDeltaTime;
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
                if (progressBar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;
                    break;
                }
            }

        }
    }
}
