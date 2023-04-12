using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerMoveControl : MonoBehaviour
{
    public float Speed;
    // 점프하는 힘
    public float JumpForce ;
    // _KillerRigidbody라는 변수로 Rigidbody Component를 통제한다는 뜻. 즉, Inspector가 아니라 코드에서도 통제가 가능한것
    private Rigidbody _KillerRigidbody;

    // 현재 캐릭터가 가지고 있는 캐릭터 컨트롤러 콜라이더.
    private CharacterController _Controller;
    // 킬러의 움직이는 방향
    private Vector3 MoveDir;
    public bool IsGround = true ;

    // Start is called before the first frame update
    void Start()
    {
        // 초기화
        _KillerRigidbody = GetComponent<Rigidbody>();
        MoveDir = Vector3.zero;
        _Controller = GetComponent<CharacterController>();
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
        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");
        MoveDir = new Vector3(xMove, _KillerRigidbody.velocity.y, zMove);
        
        // 벡터를 로컬 좌표계 기준에서 월드 좌표계 기준으로 변환한다.
        MoveDir = transform.TransformDirection(MoveDir);
        // _KillerRigidbody.velocity = new Vector3(xMove * Speed, _KillerRigidbody.velocity.y, zMove * Speed);

        // 살인마 움직임
        _Controller.Move(MoveDir * Time.deltaTime);
    }

    // 살인마 점프
    public void KillerJump()
    {
        // 여러번 점프하지 않도록
        // 스페이스를 누르고 땅이면 점프
        if(Input.GetKeyDown(KeyCode.Space) && IsGround)
        {
             // body에 힘을 가한다
             // AddForce(방향, 힘을 어떻게 가할 것인가)
             _KillerRigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);

             // 땅에서 떨어졌으므로 IsGround를 false로 바꿈
             IsGround = false;
        } 
    }




    // 충돌 처리
    private void OnCollisionEnter(Collision other)
    {
        // 땅 충돌 처리(Layer에 Ground 가 있으면)
        if(other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            // IsGround를 true로 변경
            IsGround = true;
        }
        
        // 오브젝트 충돌 처리
        // if (other.gameObject.CompareTag("Object"))
        // {
        //     //  공격하기
        // }
        // 맵 충돌 처리


    }

}
