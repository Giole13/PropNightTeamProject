using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerPlayerHold : MonoBehaviour
{
    private bool IsRightMouseClick = false;
    public GameObject Player;

    // 플레이어 위치값 변경해주기위함
    // 플레이어가 원래 있던 위치
    Vector3 PlayerStartPosition = new Vector3(0, 0.5f, 5);
    Vector3 PlayerHoldPosition = new Vector3(0, 3, 5);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MouseRightButton();
        // PlayerHold();
    }


    // 플레이어인 확인하기
    private void OnCollisionStay(Collision other)
    {
        // tag가 Player 인것을 찾고 대입하기
        Player = GameObject.FindWithTag("Player");
        // 플레이어 충돌처리
        if (other.gameObject.tag == "Player" && IsRightMouseClick)
        {
            // Debug.Log("플레이어야");

            Debug.Log("들었어");
            // 플레이어 위치값 변경하기(들기)
            Player.transform.position = Vector3.MoveTowards(PlayerStartPosition, PlayerHoldPosition, 1);

            // 플레이어 오브젝트가 살인마 자식으로 오게 하기


        }
    }

    // 마우스 오른쪽 클릭시 
    public void MouseRightButton()
    {
        if (Input.GetMouseButtonDown(1))
        {
            // 플레이어 들고 있는 시간 코루틴 실행
            StartCoroutine(HoldTime());
            // 마우스 오른쪽 2번 클릭시 플레이어 원래 위치로 가기(나중에)
        }
    }

    // 플레이어 들고 있는 시간을 주어 마우스 오른쪽 true, false 만들기
    private IEnumerator HoldTime()
    {
        IsRightMouseClick = true;
        yield return new WaitForSeconds(0.5f);
        IsRightMouseClick = false;
    }


    // // 플레이어 들기
    // public void PlayerHold()
    // {
    //     // 플레이어 위치값 변경하기(들기)
    //     transform.position = Vector3.MoveTowards(PlayerStartPosition, PlayerHoldPosition, 1);

    //     // 플레이어 오브젝트가 살인마 자식으로 오게 하기

    // }
}
