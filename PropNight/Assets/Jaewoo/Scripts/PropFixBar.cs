using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PropFixBar : MonoBehaviour
{

    public GameObject propMachineFixedCheck;

    [Header("이미지")]
    //막대기 
    public Image circleBar;
    //성공 칸 이미지
    public Image circleSuccess;
    //완벽한 성공 칸 이미지
    public Image circlePerfect;


    public RectTransform stick;
    public RectTransform successStick;
    public RectTransform perfectStick;

    [Header("Value")]
    public float timingValue = default;
    public float timingValue_ = default;
    public float barAmount = default;
    public float stickAngle = default;
    public float minSuccess = default;
    public float maxSuccess = default;
    public float minPerfect = default;
    public float maxPerfect = default;
    public float successBarAngle = default;
    public float perfectBarAngle = default;
    public float textFadeOut = default;
    public float textFadeIn = default;
    public float returnGuage = default;
    void Start()
    {
        propMachineFixedCheck.SetActive(false);
    }

    public float SkillCheck(float guage)
    {
        propMachineFixedCheck.SetActive(true);
        circleBar.fillAmount = 0f;
        StartCoroutine(SkillCheckRoutine(guage));

        return returnGuage;
        // stick.localEulerAngles = new Vector3(0, 0, 0);
        // successStick.localEulerAngles = new Vector3(0, 0, 0);
        // perfectStick.localEulerAngles = new Vector3(0, 0, 0);
    }

    IEnumerator SkillCheckRoutine(float guage)
    {
        timingValue_ = 0;
        barAmount = 0;

        //성공 대성공 앵글 초기화
        successBarAngle = Random.Range(-20, -80);

        perfectBarAngle = (successBarAngle - 36f);

        successStick.localEulerAngles = new Vector3(0, 0, successBarAngle);
        perfectStick.localEulerAngles = new Vector3(0, 0, perfectBarAngle);

        while (circleBar.fillAmount < 0.4f && !Input.GetKeyDown(KeyCode.Space))
        {
            timingValue_ += Time.deltaTime * 70;
            barAmount = (timingValue_ * 0.01f) * 0.5f;
            circleBar.fillAmount = barAmount;
            stickAngle = barAmount * 360;
            stick.localEulerAngles = new Vector3(0, 0, -stickAngle);
            yield return null;
        }

        if (stickAngle < Mathf.Abs(successBarAngle) || Mathf.Abs(perfectBarAngle - 18) < stickAngle)
        {
            guage -= 5f;
            //실패 연결
        }

        if (Mathf.Abs(successBarAngle) <= stickAngle && stickAngle < Mathf.Abs(successBarAngle - 36))
        {
            guage += 5f;
            //성공 연결

        }
        if (Mathf.Abs(perfectBarAngle) <= stickAngle && stickAngle <= Mathf.Abs(perfectBarAngle - 18))
        {
            guage += 10f;
            //대성공 연결
        }
        returnGuage = guage;
        propMachineFixedCheck.SetActive(false);
    }

}
