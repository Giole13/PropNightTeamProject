using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PropFixBar : MonoBehaviour
{
    public Image circleBar;
    public RectTransform stick;
    public float timingValue = 0f;

    void Update()
    {
        TimingBar(timingValue);
    }

    private void TimingBar(float timingValue_)
    {
        float barAmount = (timingValue_ / 100f) * 180f / 360;
        circleBar.fillAmount = barAmount;
        float stickAngle = barAmount * 360;
        stick.localEulerAngles = new Vector3(0, 0, -stickAngle);

    }
}
