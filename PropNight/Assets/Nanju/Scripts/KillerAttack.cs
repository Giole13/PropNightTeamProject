using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class KillerAttack : MonoBehaviourPun
{
    // private BoxCollider AxeCollier;
    private bool IsLeftMouseClick = false;



    // 프롭머신 망치기
    // Laycast를 불러와서 사용하기
    [SerializeField] private KillerCameraMove LookCamera;
    // 프롭머신을 공격할 수 있는지 여부를 알기
    private PropMachine AttackPropMachineCheck;
    // 프롭머신 게이지 닳는 함수 가져오기
    private PropMachine PropMachineGauge;

    public GameObject PropMachineUI;
    public GameObject Killer;


    // 애니메이션 가져오기
    private Animation Animation;


    // Start is called before the first frame update
    void Start()
    {
        // AxeCollier = GetComponent<BoxCollider>();
        // 초기화
        this.gameObject.SetActive(true);
        // 프롭머신 ui 초기화
        // 2023.04.27 / HyungJun / 오류로 인한 주석 처리
        // PropMachineUI.SetActive(false);

        // 애니메이션 초기화
        Animation = gameObject.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        MouseLeftButton();
    }


    // 무기 충돌 체크
    private void OnTriggerEnter(Collider other)
    {
        if (!photonView.IsMine || !PhotonNetwork.IsMasterClient) { return; }

        // 플레이어 충돌처리
        // 2023.04.25 / HyungJun / 마우스 클릭을 하기 전에 부딪힌 물체를 찾아보기 때문에 NullReferenceException 오류가 발생함
        // Input.GetMouseButtonDown()함수를 첫번째 if문으로 올려주길 바람  [Nanju]:  완료

        if (other.transform.parent.tag == "Player")
        {
            other.transform.parent.GetComponent<IDamage>().GetDamage();

        }

    }



    //프롭머신 파괴하기 위한 함수
    [PunRPC]
    public void PropMachineAttack()
    {
        // 프롭머신 파괴 가능
        if (LookCamera.Obj.tag == "PropMachine" && LookCamera.ObjDistance < 3f)
        {
            // 프롭머신 게이지 닳는 함수 실행
            LookCamera.Obj.GetComponent<IInteraction>().OnInteraction(Killer.tag);

            // // ui 활성화 시키기
            // PropMachineUI.SetActive(true);
            // // 파괴한다.
            // // 프롭머신 게이지 닳는 함수 실행
            // PropMachineGauge.OnInteraction(Killer.tag);
        }
        // 프롭머신이 파괴 불가능 
        else
        {
            /*Do nothing*/
        }
    }





    // 마우스 왼쪽 클릭시 
    public void MouseLeftButton()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PropMachineAttack();

            // BoXCollider에 닿기만 하연 플레이어의 HP 가 닳기 때문에
            // boxcollider 켜기
            gameObject.GetComponent<BoxCollider>().enabled = true;

            // 랜덤으로 Attack1, Attack2 공격하기
            int random = Random.Range(0, 2);
            if (random == 1)
            {
                Animation.Play("Attack1");
            }
            else if (random == 2)
            {
                Animation.Play("Attack2");
            }

            // boxcollider 끄기
            gameObject.GetComponent<BoxCollider>().enabled = false;

        }
    }


    // private IEnumerator AttackTime()
    // {
    //     IsLeftMouseClick = true;
    //     yield return null;
    //     IsLeftMouseClick = false;
    // }


}
