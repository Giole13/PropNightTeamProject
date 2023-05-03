using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPun, IDamage
{

    private PlayerInput _playerInput;       // 키보드 입력
    private Rigidbody _playerRigidBody;     // Rigidbody

    private UiPlayerSkill _uiPlayerSkill;       // 생존자 UI
    private bool IsJump;                    // 점프할 수 있는가
    private bool IsDoSomething = false;     //무언가를 하고 있는가
    private int JumpCount;                  // 점프가능 횟수
    private GameStatusManager StatusManager;
    private int _life;
    public bool IsFallDown = false;
    public float Stamina;
    public Animator Animator;
    public bool IsplayerCanChange = true;
    public bool IsMovePossible = true;
    public bool IsPlayerNotChange = true;
    public PlayerStatus Status = PlayerStatus.NORMAL;
    public Skill SurvivorSkill;
    public MouseLook Look;
    public PlayerChange Change;
    public GameObject Object;
    public GameObject Player;
    public float Speed;
    public float JumpForce;
    public float DashGauge;
    public float HP;
    public float Timer;
    public float CoolTime;

    public float SkillDistance = 0;
    public int SkillJumpCount = 0;
    private void Start()
    {
        _life = 2;
        Stamina = 100f;
        JumpCount = 0;
        if (photonView.IsMine)
        {
            _uiPlayerSkill = GameObject.Find("InGamePlayerUi").GetComponent<UiPlayerSkill>();
            _uiPlayerSkill.playerInput = _playerInput;
            _uiPlayerSkill.playerDashGage = this;
        }
        _playerRigidBody = GetComponent<Rigidbody>();
        Animator = GetComponent<Animator>();
        StatusManager = GameObject.Find("GameStatusManager").GetComponent<GameStatusManager>();

    }
    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }
    private void Update()
    {
        if (!photonView.IsMine) { return; }

        if (Status == PlayerStatus.DIE)
        {
            Die();
            return;
        }

        if (IsMovePossible)
        {
            Move();
            //Jump();
            photonView.RPC("Jump", RpcTarget.All);

        }
        LeftClick();
        RightClick();
        ESkill();

        if (!IsMovePossible)
        {
            if (Object == null)
            {
                return;
            }

            if (Object.tag == "PropMachine" && Object.GetComponent<PropMachine>().IsFixDone)
            {
                //Object.GetComponent<IInteraction>().OffInteraction(gameObject);
                IsMovePossible = true;
                IsDoSomething = false;
                Animator.SetTrigger("IsStop");
            }
            if (Object.tag == "HypnoticChair" && Object.GetComponent<HypnoticChair>().IsSurvivorOut)
            {
                IsMovePossible = true;
                IsDoSomething = false;
                Animator.SetTrigger("IsStop");
            }
        }
    }   // Update

    private void Move()
    {
        float horizontalMove = _playerInput.MoveX;
        float vertical = _playerInput.MoveZ;
        float AniSpeed;
        Vector3 moveDistance = _playerInput.MoveX * transform.right * Speed * Time.deltaTime + _playerInput.MoveZ * transform.forward * Speed * Time.deltaTime;
        if (_playerInput.Dash && Stamina > 0)
        {
            Stamina -= Time.deltaTime * 25;
            _playerRigidBody.MovePosition(_playerRigidBody.position + (2f * moveDistance));
            AniSpeed = 1.5f;
        }
        else
        {
            if (!_playerInput.Dash && Stamina < 100) { Stamina += Time.deltaTime * 17; }

            _playerRigidBody.MovePosition(_playerRigidBody.position + moveDistance);
            AniSpeed = 1;
        }
        //photonView.RPC("MoveAnimation", RpcTarget.All, vertical, horizontalMove, AniSpeed);
        MoveAnimation(vertical, horizontalMove, AniSpeed);

    }   // 이동
    /// <summary>
    /// 
    /// </summary>
    /// <param name="MoveAni">vertical ,horizon, speed</param>

    public void MoveAnimation(params object[] MoveAni)
    {
        if (MoveAni == null || MoveAni.Length < 3) { return; }

        float vertical = (float)MoveAni[0];
        float horizontalMove = (float)MoveAni[1];
        float AniSpeed = (float)MoveAni[2];
        Animator.SetFloat("Vertical", vertical * AniSpeed, 0.1f, Time.deltaTime);
        Animator.SetFloat("Horizontal", horizontalMove, 0.1f, Time.deltaTime);
        Animator.SetFloat("WalkSpeed", Speed);
    }
    [PunRPC]
    public void Jump()
    {
        if (IsPlayerNotChange)
        {
            if (_playerInput.Jump && !IsJump)
            {
                _playerRigidBody.AddForce(transform.up * JumpForce, ForceMode.Impulse);
                IsJump = true;
                //IsDoAnimation = true;
                Animator.SetTrigger("IsJump");
            }
        }
        else
        {
            if (_playerInput.Jump && !IsJump)
            {
                JumpCount++;
                _playerRigidBody.AddForce(transform.up * JumpForce, ForceMode.Impulse);
                if (JumpCount > 1 + SkillJumpCount)
                {
                    IsJump = true;
                }
            }
        }
    }   // 점프

    private void LeftClick()
    {
        if (!IsPlayerNotChange)
        {
            return;
        }

        if (_playerInput.LeftClick)
        {
            // { 무언가를 해야한다.
            if (!IsDoSomething)
            {
                if (Look.Obj == null)
                {
                    return;
                }

                // { 프롭머신을 고친다.
                if (Look.Obj.tag == "PropMachine" && Look.ObjDistance < 1 + SkillDistance)
                {
                    Object = Look.Obj;
                    Debug.Log(Object.name);
                    IsDoSomething = true;
                    IsMovePossible = false;
                    Object.GetComponent<IInteraction>().OnInteraction(gameObject.tag);
                    Animator.SetTrigger("IsFixMachine");
                }
                // } 프롭머신을 고친다.
                // { 최면의자에 앉은 생존자를 풀어준다
                // 2023.04.26 / HyungJun / 의자가 작동 중일 때 플레이어가 의자에 접근한다면 실행하는 로직
                if (Look.Obj.tag == "HypnoticChair" && Look.ObjDistance < 1 + SkillDistance)
                {
                    Object = Look.Obj;
                    if (Object.GetComponent<HypnoticChair>().ChairState == HypnoticChair.HypnoticChairState.WORKING)
                    {
                        IsDoSomething = true;
                        IsMovePossible = false;
                        Object.GetComponent<IInteraction>().OnInteraction(gameObject.tag);
                        Animator.SetTrigger("IsFixMachine");
                    }
                }
                // } 최면의자에 앉은 생존자를 풀어준다
            }
            // } 무언가를 해야한다.

            // { 무언가 하던거를 그만한다.
            else
            {
                IsDoSomething = false;
                IsMovePossible = true;
                // 2023-04-19 / HyungJun / 실험을 위한 주석 해제
                Object.GetComponent<IInteraction>().OffInteraction(gameObject.tag);
                Animator.SetTrigger("IsStop");
            }
            // } 무언가 하던거를 그만한다.
        }
    } // 마우스 왼쪽 클릭

    private void RightClick()
    {

    }   // 마우스 오른쪽 클릭
    private void ESkill()
    {
        if (_playerInput.Skill)
        {
            if (CoolTime <= 0 && !SurvivorSkill.IsSkillActive)
            {
                SurvivorSkill.ESkill();
                if (SurvivorSkill.IsSkillActive)
                {
                    CoolTime = SurvivorSkill.CoolTime;
                    photonView.RPC("ApplyUpdatedHealth", RpcTarget.Others, HP);
                }

            }

        }
        if (CoolTime > 0)
        {
            CoolTime -= Time.deltaTime;
        }
        else
        {
            SurvivorSkill.IsSkillActive = false;
        }
    }
    public void GetDamage()
    {
        if (IsFallDown) { return; }
        if (PhotonNetwork.IsMasterClient)
        {
            HP -= 1;
            photonView.RPC("ApplyUpdatedHealth", RpcTarget.Others, HP);
            // photonView.RPC("GetDamage", RpcTarget.Others);
        }

        if (HP < 0f)
        {
            Debug.LogFormat("폴 다운 실행 1 isMaster {0}, Hp: {1}", PhotonNetwork.IsMasterClient, HP);
            //FallDown();
            photonView.RPC("FallDown", RpcTarget.All);
        }
        // 2023.04.21 / Nanju / 다른 클라이언트 체력, 데미지, 상태 동기화로 수정
    }   // 생존자가 살인마한테 맞음

    // 2023.04.21 / Nanju / 다른 클라이언트들의 체력 동기화 함수
    [PunRPC]
    public void ApplyUpdatedHealth(float _hp)
    {
        HP = _hp;
    }

    // void OnCollisionEnter(Collision other)
    // {
    //     IsJump = false;
    //     IsDoAnimation = true;
    //     JumpCount = 0;
    // }   // 생존자가 땅에 닿음

    void OnCollisionStay(Collision other)
    {
        if (!photonView.IsMine)
        {
            if (IsJump)
            {
                Debug.Log(transform.position.y);
            }
            return;
        }


        if (IsJump)
        {
            Timer += Time.deltaTime;
            if (Timer > 0.1f)
            {
                photonView.RPC("Ground", RpcTarget.All);
                Timer = 0;
            }
        }

    }

    // 쓰러지는 함수
    [PunRPC]
    private void FallDown()
    {
        //Debug.LogFormat("폴 다운 실행 2 isMaster {0}, Hp: {1}", PhotonNetwork.IsMasterClient, HP);
        Animator.SetTrigger("IsFallDown");  // 애니메이션 실행
        // { 만약 생존자가 변신중 이라면
        if (!IsPlayerNotChange)
        {
            IsPlayerNotChange = true;
            Change.Player.SetActive(true);
            Destroy(Change.ChangeObj);
        }
        // } 만약 생존자가 변신중 이라면
        IsFallDown = true;
        IsMovePossible = false;
        Status = PlayerStatus.FALLDOWN;
        IsplayerCanChange = false;
        Player.transform.localRotation = Quaternion.Euler(90f, transform.localRotation.y, 0f);
        Player.transform.localPosition += new Vector3(0f, 0.5f, 0.5f);
        if (_life == 0)
        {
            // StatusManager.SurvivorDie();
            // 2023.05.02 / HyungJun / 플레이어가 죽으면 모든 클라이언트의 스테이터스매니저에 살아있는 생존자 카운트를 1 줄인다.
            StatusManager.GetComponent<PhotonView>().RPC("SurvivorDie", RpcTarget.All);
            Status = PlayerStatus.DIE;
            Player.GetComponent<CapsuleCollider>().enabled = false;
        }
        // photonView.RPC("FallDown", RpcTarget.All);
    }   // 생존자가 쓰러짐
    [PunRPC]
    public void SitOnChair()
    {
        StatusManager.photonView.RPC("SurvivorFallDown", RpcTarget.All, gameObject.GetPhotonView().ViewID);
        Animator.SetTrigger("IsSitOnChair");
        Status = PlayerStatus.CAUGHT;
        _life--;
    }   // 생존자가 최면의자에 앉혀짐
    [PunRPC]
    public void Hold()
    {
        _playerRigidBody.useGravity = false;
        // _playerRigidBody.isKinematic = true;
        Player.transform.localPosition = new Vector3(0f, 0f, 0f);
        Player.GetComponent<CapsuleCollider>().enabled = false;

    }   // 생존자가 쓰러지고, 살인마에게 들어올려짐
    [PunRPC]
    public void PutDown()
    {
        _playerRigidBody.useGravity = true;
        Player.GetComponent<CapsuleCollider>().enabled = true;
        Player.transform.localPosition += new Vector3(0f, 0.5f, 0.5f);
    }       // 생존자가 바닦에 다시 놓여짐
    [PunRPC]
    public void WakeUp()
    {
        IsMovePossible = true;
        Status = PlayerStatus.NORMAL;
        IsplayerCanChange = true;
        _playerRigidBody.useGravity = true;
        Player.GetComponent<CapsuleCollider>().enabled = true;
        Player.transform.localPosition += new Vector3(0f, 0f, 0f);
        Player.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        Animator.SetTrigger("IsRevival");
        IsFallDown = false;
        HP = 2;
    }       // 생존자가 일어남

    [PunRPC]
    public void Ground()
    {
        Animator.SetTrigger("IsGround");
        IsJump = false;
        JumpCount = 0;
    }       // 바닥에 착지했는가

    // 생존자가 완전히 사망
    public void Die()
    {

    }
}
