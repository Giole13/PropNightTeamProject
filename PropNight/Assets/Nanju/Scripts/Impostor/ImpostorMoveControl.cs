using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ImpostorMoveControl : MonoBehaviourPun
{
    public float Speed;
    private float xMove;
    private float zMove;
    // 점프하는 힘
    public float JumpForce;
    // _KillerRigidbody라는 변수로 Rigidbody Component를 통제한다는 뜻. 즉, Inspector가 아니라 코드에서도 통제가 가능한것
    private Rigidbody _KillerRigidbody;
    public bool IsGround = true;

    // 현재 캐릭터가 가지고 있는 캐릭터 컨트롤러 콜라이더.
    // 킬러의 움직이는 방향
    private Vector3 MoveDir;

    // 애니메이션 가져오기
    private Animation Animation;
    // 공격 여부 확인
    public bool _attacking = false;

    public float SkillSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        // 2023.05.04 / HyungJun / 살인마 이동속도 수정
        // Speed = 10f;

        if (photonView.IsMine)
        {
            GetComponent<AudioSource>().Stop();
        }

        // rigidboidy 컴포넌트 받아오기
        _KillerRigidbody = GetComponent<Rigidbody>();
        IsGround = true;
        MoveDir = Vector3.zero;

        // 애니메이션 초기화
        Animation = gameObject.GetComponent<Animation>();

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

        xMove = Input.GetAxis("Horizontal");
        zMove = Input.GetAxis("Vertical");

        transform.Translate((new Vector3(xMove, 0, zMove) * Speed * SkillSpeed) * Time.deltaTime);

        if (IsGround && !_attacking)
        {
            // Animation = gameObject.GetComponent<Animation>();
            if (xMove == 0f && zMove == 0f)
            {
                Animation.Play("Idle");
            }
            else
            {
                Animation.Play("Walk");
            }
        }
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
                _KillerRigidbody.velocity = Vector3.up * JumpForce;
                if (!_attacking)
                {
                    Animation.Play("Run");
                }
                else
                {
                    /* Do nothing */
                }
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

        // IsGround를 true로 변경
        IsGround = true;

    }

}
