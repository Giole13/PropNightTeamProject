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
        MouseLeftButton();

    }


    // 무기 충돌 체크
    private void OnTriggerStay(Collider other)
    {


        // 플레이어 충돌처리
        if (other.transform.parent.tag == "Player" && IsLeftMouseClick)
        {
            Debug.Log(other.gameObject.tag);
            Debug.Log(IsLeftMouseClick);
            other.transform.parent.GetComponent<IDamage>().GetDamage(gameObject);
        }
    }


    // 마우스 왼쪽 클릭시 
    public void MouseLeftButton()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(AttackTime());


        }
    }


    private IEnumerator AttackTime()
    {
        IsLeftMouseClick = true;
        yield return null;
        IsLeftMouseClick = false;
    }


    // 일정 범위에 들면 플레이어인지 확인 후 마우스로 클릭해 공격
}
