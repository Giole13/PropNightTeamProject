using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPun, IDamage
{

    private PlayerInput _playerInput;
    private Rigidbody _playerRigidBody;
    private bool IsJump;
    private bool IsDoSomething = false;
    private int JumpCount;
    private bool IsDoAnimation = false;
    public Animator Animator;
    public bool IsplayerCanChange = true;
    public bool IsMovePossible = true;
    public bool IsPlayerNotChange = true;
    public PlayerStatus Status = PlayerStatus.NORMAL;
    public MouseLook Look;
    public PlayerChange Change;
    public GameObject Player;
    public float Speed;
    public float JumpForce;
    public float DashGauge;
    public float HP;
    public GameObject Object;

    public float Timer;

    private void Start()
    {
        JumpCount = 0;
        _playerRigidBody = GetComponent<Rigidbody>();
        Animator = GetComponent<Animator>();
    }
    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {

        }
        else
        {

        }
    }
    private void Update()
    {
        if (!photonView.IsMine) { return; }

        if (IsMovePossible)
        {
            Move();
            //Jump();
            photonView.RPC("Jump", RpcTarget.All);
        }
        LeftClick();
        RightClick();
        // if (IsDoAnimation)
        // {
        //     if (!IsJump)
        //     {
        //         Animator.SetTrigger("IsGround");
        //     }
        //     else
        //     {
        //         Animator.SetTrigger("IsJump");
        //     }
        //     IsDoAnimation = false;
        // }


        if (!IsMovePossible)
        {
            if (Object == null)
            {
                return;
            }

            if (Object.GetComponent<PropMachine>().IsFixDone)
            {
                //Object.GetComponent<IInteraction>().OffInteraction(gameObject);
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
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _playerRigidBody.MovePosition(_playerRigidBody.position + (2f * moveDistance));
            AniSpeed = 1.5f;
        }
        else
        {
            _playerRigidBody.MovePosition(_playerRigidBody.position + moveDistance);
            AniSpeed = 1;
        }
        photonView.RPC("MoveAnimation", RpcTarget.All, vertical, horizontalMove, AniSpeed);

    }   // 이동
    /// <summary>
    /// 
    /// </summary>
    /// <param name="MoveAni">vertical ,horizon, speed</param>
    [PunRPC]
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
                if (JumpCount > 1)
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
                if (Look.Obj.tag == "PropMachine" && Look.ObjDistance < 1)
                {
                    Object = Look.Obj;
                    Debug.Log(Object.name);
                    IsDoSomething = true;
                    IsMovePossible = false;
                    // 2023-04-19 / HyungJun / 실험을 위한 주석 해제
                    Object.GetComponent<IInteraction>().OnInteraction(gameObject);
                    Animator.SetTrigger("IsFixMachine");
                }
                // } 프롭머신을 고친다.
            }
            // } 무언가를 해야한다.
            // { 무언가 하던거를 그만한다.
            else
            {
                IsDoSomething = false;
                IsMovePossible = true;
                // 2023-04-19 / HyungJun / 실험을 위한 주석 해제
                Object.GetComponent<IInteraction>().OffInteraction(gameObject);
                Animator.SetTrigger("IsStop");
            }
            // } 무언가 하던거를 그만한다.
        }
    }   // 마우스 왼쪽 클릭
        // [PunRPC]
        // public void LeftClicking()
        // {

    // }
    private void RightClick()
    {

    }   // 마우스 오른쪽 클릭

    public void GetDamage()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            HP -= 1;
            Debug.Log("00000000");
            photonView.RPC("ApplyUpdatedHealth", RpcTarget.Others, HP);
            // photonView.RPC("GetDamage", RpcTarget.Others);
        }
        else
        {

        }

        Debug.Log("!!!!!!!!");

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
        Debug.Log("체력을 깍는 함수");
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
        Debug.LogFormat("폴 다운 실행 2 isMaster {0}, Hp: {1}", PhotonNetwork.IsMasterClient, HP);


        Animator.SetTrigger("IsFallDown");

        if (!IsPlayerNotChange)
        {
            Destroy(Change.ChangeObj);
            Change.Player.SetActive(true);
            IsPlayerNotChange = true;
        }
        Status = PlayerStatus.FALLDOWN;
        IsplayerCanChange = false;
        Player.transform.localRotation = Quaternion.Euler(90f, transform.localRotation.y, 0f);
        Player.transform.localPosition += new Vector3(0f, 0.5f, 0f);
        // photonView.RPC("FallDown", RpcTarget.All);


    }   // 생존자가 쓰러짐

    public void SitOnChair()
    {
        Status = PlayerStatus.CAUGHT;
        IsMovePossible = false;
        IsplayerCanChange = false;
        Player.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
    }   // 생존자가 최면의자에 앉혀짐
    public void Hold()
    {
        _playerRigidBody.useGravity = false;
        // _playerRigidBody.isKinematic = true;
        Player.GetComponent<CapsuleCollider>().enabled = false;
    }   // 생존자가 쓰러지고, 살인마에게 들어올려짐
    public void PutDown()
    {
        _playerRigidBody.useGravity = true;
        Player.GetComponent<CapsuleCollider>().enabled = true;
    }
    private void WakeUp()
    {

    }

    [PunRPC]
    public void Ground()
    {


        Animator.SetTrigger("IsGround");
        IsJump = false;
        JumpCount = 0;

    }
}
