using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerAttack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // 살인마 충돌 체크
    private void OnCollisionEnter(Collision other)
    {
        // 플레이어 충돌처리
        if (other.gameObject.CompareTag("Player"))
        {
            //  공격하기
        }


    }


    // 일정 범위에 들면 플레이어인지 확인 후 마우스로 클릭해 공격
}
