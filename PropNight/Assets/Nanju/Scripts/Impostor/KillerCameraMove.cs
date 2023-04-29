using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class KillerCameraMove : MonoBehaviourPun
{
    // 마우스 민감도 설정
    public float mouseSensitivity = 100f;

    // 킬러 몸통 오브젝트
    //전체 1인칭 개체에 대한 참조 필요, 공개 변환 생성
    public Transform killerBody;
    float xRotation = 0f;



    // [ Laycast ] 

    // 특정 사물만 맞추기
    public LayerMask ObjCheck;
    // Laycast 대상
    public GameObject Obj;
    // Laycast 범위
    public float ObjDistance;
    private float _maxDistance = 300f;
    private RaycastHit _hit;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastCheck();
        CameraMoveInput();

    }

    // Lay 레이져  확인 
    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, transform.forward * 300f, Color.red);
    }



    // Laycast 체크하기
    public void RaycastCheck()
    {
        if (Physics.Raycast(transform.position, transform.forward, out _hit,
                 _maxDistance))
        {
            // 충돌 대상
            Obj = _hit.transform.gameObject;
            // 충돌 범위
            ObjDistance = _hit.distance;

        }

    }

    // 카메라 360도 움직임
    public void CameraMoveInput()
    {
        // 포톤에서 자기자신만 움직이게 하기 위해 
        if (!photonView.IsMine) { return; }


        // mouseSensitivity : 마우스 움직임에 따라 변경될 unity 내부의 사전 프로그래밍된 축
        // Time.deltaTime :  현재 프레임 속도와 독립적으로 회전하는지 확인하기 위함, 
        //                   업데이트 함수가 마지막으로 호출된 이후 경과한 시간임
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        // 위, 아래 고정
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // 회전 축 지정   
        // Vector3.up : 중심으로 회전
        killerBody.Rotate(Vector3.up * mouseX);
    }
}