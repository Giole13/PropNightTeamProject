using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class KillerAttack : MonoBehaviourPun
{
    private BoxCollider AxeCollier;
    private bool IsLeftMouseClick = false;



    // 프롭머신 망치기
    // Laycast를 불러와서 사용하기
    public KillerCameraMove LookCamera;
    // 프롭머신을 공격할 수 있는지 여부를 알기
    public PropMachine AttackPropMachineCheck;
    // 프롭머신 게이지 닳는 함수 가져오기
    public PropMachine PropMachineGauge;

    public GameObject PropMachineUI;
    public GameObject Killer;


    // Start is called before the first frame update
    void Start()
    {
        AxeCollier = GetComponent<BoxCollider>();
        // 초기화
        this.gameObject.SetActive(true);
        // 프롭머신 ui 초기화
        PropMachineUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }


    // 무기 충돌 체크
    private void OnTriggerStay(Collider other)
    {
        if (!photonView.IsMine || !PhotonNetwork.IsMasterClient) { return; }

        // 플레이어 충돌처리
        // 2023.04.25 / HyungJun / 마우스 클릭을 하기 전에 부딪힌 물체를 찾아보기 때문에 NullReferenceException 오류가 발생함
        // Input.GetMouseButtonDown()함수를 첫번째 if문으로 올려주길 바람
        if (other.transform.parent.tag == "Player" && Input.GetMouseButtonDown(0))
        {
            other.transform.parent.GetComponent<IDamage>().GetDamage();

        }
    }



    // 프롭머신 파괴하기 위한 함수
    [PunRPC]
    public void PropMachineAttack()
    {
        // 프롭머신 파괴 가능
        if (LookCamera.Obj.tag == "PropMachine" && LookCamera.ObjDistance < 3f && AttackPropMachineCheck.IsBreakPossible == true)
        {
            // ui 활성화 시키기
            PropMachineUI.SetActive(true);
            // 파괴한다.
            // 프롭머신 게이지 닳는 함수 실행
            PropMachineGauge.OnInteraction(Killer.tag);
        }
        // 프롭머신이 파괴 불가능 
        else if (LookCamera.Obj.tag == "PropMachine" && LookCamera.ObjDistance < 3f && AttackPropMachineCheck.IsBreakPossible == false)
        {
            /*Do nothing*/
        }
    }





    // // 마우스 왼쪽 클릭시 
    // public void MouseLeftButton()
    // {
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         StartCoroutine(AttackTime());


    //     }
    // }


    // private IEnumerator AttackTime()
    // {
    //     IsLeftMouseClick = true;
    //     yield return null;
    //     IsLeftMouseClick = false;
    // }


}
