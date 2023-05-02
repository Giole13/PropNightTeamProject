using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class AkibanAttack : MonoBehaviourPun
{

    // 프롭머신 망치기
    // Laycast를 불러와서 사용하기
    [SerializeField] private AkibanCameraMove _lookCamera;
    // 프롭머신을 공격할 수 있는지 여부를 알기
    private PropMachine _attackPropMachineCheck;
    // 프롭머신 게이지 닳는 함수 가져오기
    private PropMachine _propMachineGauge;

    // public GameObject PropMachineUI;
    public GameObject Akiban;
    // public GameObject KillerRightHand;

    // 애니메이션 가져오기
    private Animator _animator;
    // 스킬 사용 가능
    private bool _isSkillActive = true;
    // 돌진 스킬 쓰기 위한 쿨타임
    [SerializeField]
    private float _coolTime;

    public float Speed;

    public bool IsStop = false;

    public AkibanMoveControl AkibanControl;
    public Rigidbody Rigid;
    public GameObject KillerRightHand;

    // Start is called before the first frame update
    void Start()
    {
        // 초기화
        this.gameObject.SetActive(true);
        // 프롭머신 ui 초기화
        // 2023.04.27 / HyungJun / 오류로 인한 주석 처리
        // PropMachineUI.SetActive(false);

        // 애니메이션 초기화
        _animator = gameObject.GetComponent<Animator>();
        Rigid = gameObject.GetComponent<Rigidbody>();
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
        AkibanActiveSkill();
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
    private IEnumerator AkibanAttackMotion()
    {
        if (AkibanControl.Attacking) { yield break; }
        KillerRightHand.GetComponent<BoxCollider>().enabled = true;

        AkibanControl.Attacking = true;

        _animator.SetTrigger("IsAttack");
        yield return new WaitForSeconds(1.7f);
        AkibanControl.Attacking = false;
        // BoxCollider 끄기
        KillerRightHand.GetComponent<BoxCollider>().enabled = false;

    }


    // 마우스 왼쪽 클릭시 
    [PunRPC]
    public void MouseLeftButton()
    {
        // PropMachineAttack();
        // OnTriggerEnter(Player);
        StartCoroutine(AkibanAttackMotion());


    }



    // 아키반 스킬 쓰기(E 키보드 누른경우)
    // E 키보드를 누르면 재빠른 돌진 공격 스킬(단, 방향 전환 X)
    public void AkibanActiveSkill()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // 스킬 사용 가능
            if (_isSkillActive)
            {
                AkibanControl.IsCanControl = false;
                // { 스킬
                float DashTime = 0f;
                while (DashTime < 2f)
                {

                    Rigid.velocity += transform.forward * Time.deltaTime * Speed;
                    DashTime += Time.deltaTime;
                    if (IsStop)
                    {
                        break;
                    }
                }
                StartCoroutine(AkibanAttackMotion());
                // } 스킬
                IsStop = false;
                AkibanControl.IsCanControl = true;
                _isSkillActive = false;
                _coolTime = 5;
            }

        }
        if (!_isSkillActive)
        {
            _coolTime -= Time.deltaTime;
            if (_coolTime <= 0)
            {
                _isSkillActive = true;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Player")
        {
            IsStop = true;
        }

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
