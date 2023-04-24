using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSkill : MonoBehaviour
{
    public Image skillCoolImage = default;
    public int fillAmount = 0;


    void Update()
    {
        AbilitySkill();
    }
    public void AbilitySkill()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(AbilityCoolTime(5f));
        }
    }
    public void AbilityRun()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {

        }
    }

    IEnumerator AbilityCoolTime(float cool)
    {
        Debug.Log("코루틴시작");
        while (1.0f < cool)
        {
            cool -= Time.deltaTime;
            skillCoolImage.fillAmount = (1f / cool);
            yield return new WaitForFixedUpdate();
        }
        Debug.Log("코루틴끝");
    }

    public void RunSkill()
    {

    }
}
