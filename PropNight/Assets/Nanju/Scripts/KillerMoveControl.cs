using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class KillerMoveControl : MonoBehaviourPun
{
    public float Speed;
    // 점프하는 힘
    public float JumpForce;
    // _KillerRigidbody라는 변수로 Rigidbody Component를 통제한다는 뜻. 즉, Inspector가 아니라 코드에서도 통제가 가능한것
    private Rigidbody _KillerRigidbody;
    public bool IsGround = true;

    // 현재 캐릭터가 가지고 있는 캐릭터 컨트롤러 콜라이더.
    // 킬러의 움직이는 방향
    private Vector3 MoveDir;

    // Start is called before the first frame update
    void Start()
    {
        // rigidboidy 컴포넌트 받아오기
        _KillerRigidbody = GetComponent<Rigidbody>();
        IsGround = true;
        MoveDir = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        KillerMove();
        KillerJump();

    }

    // 살인마 이동
    public void KillerMove()
    {
        if (!photonView.IsMine) { return; }

        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");
        MoveDir = new Vector3(xMove, _KillerRigidbody.velocity.y, zMove);

        // 벡터를 로컬 좌표계 기준에서 월드 좌표계 기준으로 변환한다.
        MoveDir = transform.TransformDirection(MoveDir);
        // _KillerRigidbody.velocity = new Vector3(xMove * Speed, _KillerRigidbody.velocity.y, zMove * Speed);

        // 살인마 움직임
        _KillerRigidbody.velocity = MoveDir;
    }

    // 살인마 점프
    public void KillerJump()
    {
        if (!photonView.IsMine) { return; }

        // 스페이드 키를 누르면 점프
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 바닥에 있으면 점프 실행
            if (IsGround)
            {
                IsGround = false;
                _KillerRigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            }
            // 공중에 떠 있는 상태이면 점프하지 못하도록 리턴
            else
            {
                return;
            }
        }
    }

    // 충돌 처리
    private void OnCollisionEnter(Collision other)
    {
        // 땅 충돌 처리(Layer에 Ground 가 있으면)
        if (other.gameObject.CompareTag("Ground"))
        {
            // IsGround를 true로 변경
            IsGround = true;

        }



    }

}
