using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class KillerAttack : MonoBehaviourPun
{

    // 프롭머신 망치기
    // Laycast를 불러와서 사용하기
    [SerializeField] private KillerCameraMove LookCamera;
    // 프롭머신을 공격할 수 있는지 여부를 알기
    private PropMachine AttackPropMachineCheck;
    // 프롭머신 게이지 닳는 함수 가져오기
    private PropMachine PropMachineGauge;

    // public GameObject PropMachineUI;
    public GameObject Killer;
    public GameObject KillerRightHand;


    // 애니메이션 가져오기
    private Animation Animation;


    // Start is called before the first frame update
    void Start()
    {
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
        if (!photonView.IsMine)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            photonView.RPC("MouseLeftButton", RpcTarget.All);

        }

    }

    // 킬러 박스 콜라이더와 플레이어 충돌 함수
    private void OnTriggerEnter(Collider other)
    {
        if (!photonView.IsMine || !PhotonNetwork.IsMasterClient) { return; }

        if (other.transform.parent.tag == "Player")
        {
            other.transform.parent.GetComponent<IDamage>().GetDamage();
        }
    }

    // 공격 애니메이션 랜덤으로 나오게 하기 위한 코루틴 함수
    private IEnumerator AttackTime()
    {
        // BoxCollider 켜기
        KillerRightHand.GetComponent<BoxCollider>().enabled = true;

        int random = Random.Range(0, 2);
        if (random == 1)
        {
            Animation.Play("Attack1");
        }
        else if (random == 2)
        {
            Animation.Play("Attack2");
        }
        yield return new WaitForSeconds(2f);

        Animation.Stop();

        // BoxCollider 끄기
        KillerRightHand.GetComponent<BoxCollider>().enabled = false;
    }

    // 마우스 왼쪽 클릭시 
    [PunRPC]
    public void MouseLeftButton()
    {
        // PropMachineAttack();
        // OnTriggerEnter(Player);
        StartCoroutine(AttackTime());


    }


    //프롭머신 파괴하기 위한 함수
    // [PunRPC]
    // public void PropMachineAttack()
    // {
    //     // 프롭머신 파괴 가능
    //     if (LookCamera.Obj.tag == "PropMachine" && LookCamera.ObjDistance < 3f)
    //     {
    //         // 프롭머신 게이지 닳는 함수 실행
    //         LookCamera.Obj.GetComponent<IInteraction>().OnInteraction(Killer.tag);
    //     }
    //     // 프롭머신이 파괴 불가능 
    //     else
    //     {
    //         /*Do nothing*/
    //     }
    // }






}
