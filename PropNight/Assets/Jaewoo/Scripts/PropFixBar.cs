using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PropFixBar : MonoBehaviour
{
    public GameObject propMachineFixedCheck = default;

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
    public int successBarAngle = default;
    public int perfectBarAngle = default;

    void Start()
    {
        propMachineFixedCheck.SetActive(false);
        //밸류 초기화
        timingValue = 0f;
    }

    /// <summary>
    /// FillAmount 조절시 stick이 움직ㅇ
    /// </summary>
    /// <param name="timingValue_"></param>
    public void SkillCheck()
    {
        propMachineFixedCheck.SetActive(true);
        StartCoroutine(SkillCheckRoutine());
    }
    IEnumerator SkillCheckRoutine()
    {
        successBarAngle = Random.Range(-20, -80);
        perfectBarAngle = (successBarAngle);
        successStick.localEulerAngles = new Vector3(0, 0, successBarAngle);
        perfectStick.localEulerAngles = new Vector3(0, 0, perfectBarAngle);
        // perfectStick.localEulerAngles = new Vector3(0, 0, perfectBarAngle);

        while (circleBar.fillAmount < 0.4f && !Input.GetKeyDown(KeyCode.Space))
        {
            timingValue_ += Time.deltaTime * 70;
            barAmount = (timingValue_ * 0.01f) * 0.5f;
            stickAngle = barAmount * 360;
            circleBar.fillAmount = barAmount;
            stick.localEulerAngles = new Vector3(0, 0, -stickAngle);
            yield return null;
        }

        if (Mathf.Abs((successBarAngle)) <= stickAngle &&
            Mathf.Abs((successBarAngle)) + 54 >= stickAngle)
        {
            Debug.Log("난 성공했다.");
        }
        if (Mathf.Abs((perfectBarAngle)) <= stickAngle &&
            Mathf.Abs((perfectBarAngle)) + 18 >= stickAngle)
        {
            Debug.Log("난 대성공했다.");
        }
        propMachineFixedCheck.SetActive(false);
        timingValue_ = 0f;
        barAmount = 0f;
        stickAngle = 0f;
        circleBar.fillAmount = 0f;
        stick.localEulerAngles = new Vector3(0, 0, -stickAngle);
    }
}
