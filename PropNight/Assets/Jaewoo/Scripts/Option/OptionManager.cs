using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class OptionManager : MonoBehaviour
{
    private static OptionManager _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    Resolution[] resolutions;
    public TMP_Dropdown resolutionDropdown;
    public TMP_Text windowText = default;
    public AudioMixer audioMixer;
    public Slider masterSlider;
    public Slider bgmSlider;
    public Slider sfxSlider;


    void Start()
    {

        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
            resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

    }


    public void MasterVolume()
    {
        audioMixer.SetFloat("volume", Mathf.Log10(bgmSlider.value) * 20);
    }
    public void BGMVolume()
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(bgmSlider.value) * 20);
    }
    public void SFXVolume()
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(sfxSlider.value) * 20);
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        windowText.text = "전체화면";
    }
    public void NotFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = !isFullscreen;
        windowText.text = "창 화면";
    }

    public void SizeChange(int resolutionIndex)
    {

        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

}
