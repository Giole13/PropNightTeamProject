using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TitleMainScripte : MonoBehaviour
{
    public GameObject mainCanvasObject = default;
    public GameObject mainObject = default;
    public GameObject titleObject = default;
    public TMP_Text titleText = default;

    //타이틀씬 anykey용 페이드인
    private float fadeInEffectTime = 0f;
    //타이틀씬 anykey용 페이드아웃
    private float fadeOutEffectTime = 0f;

    //타이틀씬 text전체용 페이드아웃
    private float fadeOutAllTitle = 0f;
    //메인씬 text전체용 페이드 인
    private float fadeInAllMain = 0f;
    //title 씬에서 키 입력 받았는지
    public bool isAnyKeyDown = false;
    //title 씬에서 키 입력 받고 ui의 역할이 끝났는지
    public bool isEndTitle = false;
    //title 씬에서 fadeout 이 끝났는지
    public bool isTitleFadeOut = false;
    void Start()
    {
        mainObject.GetComponent<CanvasGroup>().alpha = 0f;

    }
    void Update()
    {

        //키입력 없을때
        if (!Input.anyKeyDown)
        {
            TitleAnyKeyText();
        }
        //키입력 생기면
        else
        {
            //TitleText()가 더이상 실행이 안되게하는 true
            isAnyKeyDown = true;
            isEndTitle = true;
        }

    }   //Update()

    #region 타이틀 텍스트 효과 함수

    public void TitleAnyKeyText()
    {
        if (!isAnyKeyDown)
        {
            if (fadeInEffectTime < 1.5f)
            {
                titleText.alpha = 0f;
                titleText.text = "아무거나 입력하시오";
                titleText.alpha = fadeInEffectTime / 1.5f;
            }
            else
            {
                fadeOutEffectTime = 2.5f;
            }
            if (fadeInEffectTime < fadeOutEffectTime)
            {
                titleText.alpha = 1f - fadeInEffectTime / fadeOutEffectTime;
            }
            if (2.5f < fadeInEffectTime)
            {
                fadeInEffectTime = 0f;
                fadeOutEffectTime = 0f;
            }
            fadeInEffectTime += Time.deltaTime;
        }
        else
        {
            if (isEndTitle)
            {
                TitleAlpha();
            }
            else { };
        }
    }

    //타이틀씬 ui Object 알파값 조절 함수
    public void TitleAlpha()
    {
        if (fadeOutAllTitle < 1f)
        {
            TextFadeOutAlpha(1f - fadeOutAllTitle / 1f);
        }
        if (1f < fadeOutAllTitle)
        {
            titleObject.SetActive(false);
            fadeOutAllTitle = 0f;
            isTitleFadeOut = true;
        }
        if (!isTitleFadeOut && fadeInAllMain < 1f)
        {
            StartCoroutine(OneFrame());
            TextFadeInAlpha(fadeInAllMain / 1f);
        }
        if (1f < fadeInAllMain)
        {
            isEndTitle = true;
        }
        fadeOutAllTitle += Time.deltaTime;
        fadeInAllMain += Time.deltaTime;

    }   //TitleAlpha()

    //타이틀씬 ui Object 전체 알파값 대입 함수
    public void TextFadeOutAlpha(float alpha_)
    {
        titleObject.GetComponent<CanvasGroup>().alpha = alpha_;
    }   //TextFadeOutAlpha()

    //main씬 ui Object 전체 알파값 대입 함수
    public void TextFadeInAlpha(float alpha_)
    {
        mainObject.GetComponent<CanvasGroup>().alpha = alpha_;
    }   //TextFadeInAlpha
    #endregion

    IEnumerator OneFrame()
    {
        yield return new WaitForSeconds(1f);
    }
}
