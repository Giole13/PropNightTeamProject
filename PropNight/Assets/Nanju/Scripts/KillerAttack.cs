using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerAttack : MonoBehaviour
{
    private BoxCollider AxeCollier;
    private bool IsLeftMouseClick = false;
    // Start is called before the first frame update
    void Start()
    {
        AxeCollier = GetComponent<BoxCollider>();
        // 초기화
        this.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        MouseButton();

    }


    // 무기 충돌 체크
    private void OnCollisionStay(Collision other)
    {
        // 플레이어 충돌처리
        if (other.gameObject.tag == "Player" && IsLeftMouseClick)
        {
            Debug.Log("충돌했어");
            Debug.Log("사라졌다");
            //  Axe 가 사라진다.
            this.gameObject.SetActive(false);



        }
    }


    // 마우스 왼쪽 클릭시 Axe 사라진다.
    public void MouseButton()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(AttackTime());


        }
    }


    private IEnumerator AttackTime()
    {
        IsLeftMouseClick = true;
        yield return new WaitForSeconds(0.5f);
        IsLeftMouseClick = false;
    }


    // 일정 범위에 들면 플레이어인지 확인 후 마우스로 클릭해 공격
}
