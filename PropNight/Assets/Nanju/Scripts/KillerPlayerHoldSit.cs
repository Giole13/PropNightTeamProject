using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerPlayerHoldSit : MonoBehaviour
{
    // 플레이어 들었을때 위치값
    public Transform HoldPlayerPosition;
    // 오른쪽 마우스를 클릭했는가
    private bool IsRightMouseClick = false;
    // 플레이어 알기
    public GameObject Player;
    // 카메라 알기
    // 1인칭 카메라
    public GameObject FirstCamera;
    // 3인칭 카메라
    public GameObject ThirdCamera;

    [SerializeField]
    private KillerState _killerState = KillerState.IDLE;

    private PlayerMovement _playerMovementScript = default;

    // Laycast를 불러와서 사용하기
    public KillerCameraMove LookCamera;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MouseRightButton();
        RightClick();
    }




    // 오른쪽 마우스를 클릭시
    private void RightClick()
    {
        if (Input.GetMouseButtonDown(1))
        {

            if (LookCamera.Obj.tag == "Player" && LookCamera.ObjDistance < 3f)
            {

                // 플레이어 스크립트 가져오기
                _playerMovementScript = LookCamera.Obj.GetComponent<PlayerMovement>();

                // 플레이어의 상태가 쓰러진 상태이면
                if (_playerMovementScript.Status == PlayerStatus.FALLDOWN)
                {
                    _killerState = KillerState.PLAYERHOLD;
                    Player = LookCamera.Obj;
                    // 플레이어 오브젝트가 살인마 자식으로 오게 하기
                    Player.transform.SetParent(gameObject.transform);
                    // 플레이어 상태 바꾸기
                    Player.GetComponent<PlayerMovement>().Hold();
                    // 플레이어 위치값 변경하기(들기)
                    Player.transform.position = HoldPlayerPosition.position;
                    // 카메라 3인칭 되게 하기
                    ThirdCamera.SetActive(true);
                    // 1인칭 카메라 끄기
                    FirstCamera.SetActive(false);
                }
            }
            // 플레이어 최면의자에 앉히기
            else if (LookCamera.Obj.tag == "HypnoticChair" && _killerState == KillerState.PLAYERHOLD && LookCamera.ObjDistance < 3f)
            {
                Player.GetComponent<PlayerMovement>().SitOnChair();
                LookCamera.Obj.GetComponent<IInteraction>().OnInteraction(Player);
                // 플레이어 오브젝트 살인마 자식으로 빼기
                Player.transform.SetParent(null);

                // 1인칭 카메라 켜기
                ThirdCamera.SetActive(false);
                // 3인칭 카메라 끄기
                FirstCamera.SetActive(true);
            }
            // 마우스 오른쪽 2번 클릭시 플레이어 원래 위치로 가기
            else if (_killerState == KillerState.PLAYERHOLD)
            {
                // 플레이어 오브젝트 살인마 자식으로 빼기
                Player.transform.SetParent(null);
                Debug.Log("자식으로 갔니?");

                // 1인칭 카메라 켜기
                ThirdCamera.SetActive(false);
                // 3인칭 카메라 끄기
                FirstCamera.SetActive(true);
            }

        }

    }
    // 마우스 오른쪽 클릭시 
    public void MouseRightButton()
    {
        if (Input.GetMouseButtonDown(1))
        {
            // 플레이어 들고 있는 시간 코루틴 실행
            StartCoroutine(HoldTime());

        }
    }

    // 플레이어 들고 있는 시간을 주어 마우스 오른쪽 true, false 만들기
    private IEnumerator HoldTime()
    {
        IsRightMouseClick = true;
        yield return null;
        IsRightMouseClick = false;
    }

}
