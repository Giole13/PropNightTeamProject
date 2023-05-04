using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class ImpostorAttack : MonoBehaviourPun
{

    // 프롭머신 망치기
    // Laycast를 불러와서 사용하기
    [SerializeField] private ImpostorCameraMove LookCamera;
    // 프롭머신을 공격할 수 있는지 여부를 알기
    private PropMachine AttackPropMachineCheck;
    // 프롭머신 게이지 닳는 함수 가져오기
    private PropMachine PropMachineGauge;
    public UiKillerPoint uiKillerPoint;

    // public GameObject PropMachineUI;
    public GameObject Killer;
    public GameObject KillerRightHand;



    // 애니메이션 가져오기
    private Animation Animation;

    private ImpostorMoveControl ImpostorControl;
    private bool _isSkillActive = true;

    private float _coolTime = 0;
    private bool _isCanAttack = true;


    // 프롭머신 망치는 상태 ui에게 보내주기
    public bool IsPropmachineAttackCheck = false;

    // 플레이어 공격하는 상태 ui에게 보내주기
    public bool IsPlayerAttackCheck = false;



    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            uiKillerPoint = GameObject.Find("InGameKillerUi").GetComponent<UiKillerPoint>();
            uiKillerPoint.impostorAttack = this;

        }
        // 초기화
        this.gameObject.SetActive(true);
        // 프롭머신 ui 초기화
        // 2023.04.27 / HyungJun / 오류로 인한 주석 처리
        // PropMachineUI.SetActive(false);
        ImpostorControl = gameObject.GetComponent<ImpostorMoveControl>();
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
            if (_isCanAttack)
            {
                PlayerAttackCheck();
                photonView.RPC("MouseLeftButton", RpcTarget.All);
            }


        }
        ImpostorAtiveSkill();
        PropmachinAttacCheck();
    }

    // 프롭머신 망치는 상태 ui에게 보내주기 함수
    public void PropmachinAttacCheck()
    {
        // 포톤에서 자기자신만 움직이게 하기 위해 
        if (!photonView.IsMine) { return; }
        if (LookCamera.Obj == null) { return; }

        if (LookCamera.Obj.tag == "PropMachine" && LookCamera.ObjDistance < 3f)
        {
            IsPropmachineAttackCheck = true;
            return;

        }
        IsPropmachineAttackCheck = false;
    }


    // 플레이어를 공격하는 상태 ui에게 보내주는 함수
    public void PlayerAttackCheck()
    {
        // 포톤에서 자기자신만 움직이게 하기 위해 
        if (!photonView.IsMine) { return; }
        if (LookCamera.Obj == null) { return; }

        if (LookCamera.Obj.tag == "Player" && LookCamera.ObjDistance < 3f && Input.GetMouseButtonDown(0))
        {
            IsPlayerAttackCheck = true;
            return;

        }
        IsPlayerAttackCheck = false;

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
        // 2023.05.04 / HyungJun / Random.Range(0, 2) 함수의 시작값은 이상 값이고 최대 값은 미만 값이다.
        // 0은 포함하고 2는 포함하지 않는다. -> 0, 1 의 랜덤 값 -> 로직 수정
        if (random == 0)
        {
            Animation.Play("Attack1");
        }
        else if (random == 1)
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
        PropMachineAttack();
        StartCoroutine(AttackTime());


    }


    // E 를 누르면 그림자가 되어 스피드가 빨라지고 무적이 되는 스킬(이동만 된다.)
    public void ImpostorAtiveSkill()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_isSkillActive)
            {
                ImpostorControl.SkillSpeed = 2;
                _isCanAttack = false;
                float DashTime = 0f;
                while (DashTime < 3f)
                {
                    if (Input.GetKeyUp(KeyCode.E)) { break; }
                    DashTime += Time.deltaTime;
                }
                ImpostorControl.SkillSpeed = 1;
                _isSkillActive = false;
                _coolTime = 8;
                _isCanAttack = true;
            }
            if (!_isSkillActive)
            {
                _coolTime -= Time.deltaTime;
                if (_coolTime <= 0)
                {
                    _isSkillActive = true;
                }
            }
            // 그림자가 된다.
            // 스피드가 빨라진다.

            // 이동만 된다.

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
        }
        // 프롭머신이 파괴 불가능 
        else
        {
            /*Do nothing*/
        }
    }






}
