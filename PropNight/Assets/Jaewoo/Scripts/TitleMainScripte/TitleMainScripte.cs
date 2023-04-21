using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TitleMainScripte : MonoBehaviour
{
    //=================GameObject===================
    //타이틀&메인의 최상위 부모
    public GameObject mainCanvasObject = default;
    //메인의 최상위 부모
    public GameObject mainObject = default;
    //타이틀 최상위 부모
    public GameObject titleObject = default;
    //Caution CanvasGroup Object
    public GameObject cautionGroup = default;
    //title CanvasGroup Object
    public GameObject titleGroup = default;

    //=================TMP_Text===================
    //타이틀에서 텍스트 효과의 TMP_Text
    public TMP_Text titleText = default;

    //=================정수 상수===================
    //주의사항 페이드아웃
    private float fadeOutCautionTime = 0f;
    //타이틀씬 anykey용 페이드인
    private float fadeInEffectTime = 0f;
    //타이틀씬 anykey용 페이드아웃
    private float fadeOutEffectTime = 0f;

    //타이틀씬 text전체용 페이드아웃
    private float fadeOutAllTitle = 0f;
    //메인씬 text전체용 페이드 인
    private float fadeInAllMain = 0f;

    //=================Bool===================
    //title 씬에서 키 입력 받았는지
    public bool isAnyKeyDown = false;
    //title 씬에서 키 입력 받고 ui의 역할이 끝났는지
    public bool isEndTitle = false;
    //title 씬에서 fadeout 이 끝났는지
    public bool isTitleFadeOut = false;
    //Caution 인지 아닌지
    public bool isCautionEnd = false;
    void Start()
    {
        mainObject.GetComponent<CanvasGroup>().alpha = 0f;
        titleGroup.GetComponent<CanvasGroup>().alpha = 0f;
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
            if (!isCautionEnd)
            {
                StartCoroutine(Delay());
            }
            else
            {
                titleText.alpha = 0f;
                if (fadeInEffectTime < 1f)
                {
                    titleText.text = "아무거나 입력하시오";
                    titleText.alpha = fadeInEffectTime / 1f;
                }
                else
                {
                    fadeOutEffectTime = 2f;
                }
                if (fadeInEffectTime < fadeOutEffectTime)
                {
                    titleText.alpha = 1f - fadeInEffectTime / fadeOutEffectTime;
                }
                if (2f < fadeInEffectTime)
                {
                    fadeInEffectTime = 0f;
                    fadeOutEffectTime = 0f;
                }
                fadeInEffectTime += Time.deltaTime;
            }
        }
        else
        {
            if (isEndTitle)
            {
                TitleAlpha();
            }
            else
            {

            }
        }
    }   //TitleAnyKeyText()

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
            StartCoroutine(OneFrame(1f));
        }
        if (1f < fadeInAllMain)
        {
            isEndTitle = true;
        }
        fadeOutAllTitle += Time.deltaTime;

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

    #region 코루틴
    IEnumerator CautionTitle()
    {
        yield return new WaitForSeconds(0f);
        TitleAnyKeyText();
        titleGroup.GetComponent<CanvasGroup>().alpha = 1f;
    }   //CautionTitle()
    IEnumerator OneFrame(float frame)
    {
        yield return new WaitForSeconds(frame);
        fadeInAllMain += Time.deltaTime;
        TextFadeInAlpha(fadeInAllMain / 1f);
    }   //OneFrame()
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(5f);
        if (fadeOutCautionTime < 1f)
        {
            cautionGroup.GetComponent<CanvasGroup>().alpha = 1 - fadeOutCautionTime / 1f;
            cautionGroup.GetComponent<CanvasGroup>().alpha = 0f;
            StartCoroutine(CautionTitle());
            isCautionEnd = true;
        }
        fadeOutCautionTime += Time.deltaTime;

    }  // Delay()
    #endregion
}
