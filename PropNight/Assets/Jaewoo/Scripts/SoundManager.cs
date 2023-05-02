using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public AudioSource bgSound;
    public AudioClip[] bgList;
    private static SoundManager _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadScene)
    {
        for (int i = 0; i < bgList.Length; i++)
        {
            if (scene.name == bgList[i].name)
            {
                BgSoundPlay(bgList[i]);
            }
        }
    }

    public void BgSoundPlay(AudioClip clip)
    {
        bgSound.clip = clip;
        bgSound.loop = true;
        bgSound.volume = 0.1f;
        bgSound.Play();
    }
}
