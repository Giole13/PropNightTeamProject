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
    //public TMP_Dropdown resolutionDropdown;
    public TMP_Text windowText = default;
    public AudioMixer audioMixer;
    public Slider masterSlider;
    public Slider bgmSlider;
    public Slider sfxSlider;
    public TMP_Dropdown resolutiondropdown;
    private int resolutionNum;

    FullScreenMode screenMode;
    List<Resolution> resolutions = new List<Resolution>();
    public void InitUi()
    {
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            resolutions.Add(Screen.resolutions[i]);
        }
        resolutiondropdown.options.Clear();

        int optionNum = 0;
        foreach (Resolution size in resolutions)
        {
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
            option.text = size.width + " x " + size.height;
            resolutiondropdown.options.Add(option);

            if (size.width == Screen.width && size.height == Screen.height)
            {
                resolutiondropdown.value = optionNum;
                optionNum++;
            }
        }
        resolutiondropdown.RefreshShownValue();
    }
    public void DropboxOptionChange(int change)
    {
        resolutionNum = change;
    }

    public void OkBtnClick()
    {
        Screen.SetResolution(resolutions[resolutionNum].width,
        resolutions[resolutionNum].height, screenMode);
    }
    void Start()
    {

        InitUi();
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



}
