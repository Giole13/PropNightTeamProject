using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour, IDamage
{

    private PlayerInput _playerInput;
    private Rigidbody _playerRigidBody;
    private bool IsJump;
    private bool IsDoSomething = false;
    private int JumpCount;

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

    private void Start()
    {
        JumpCount = 0;
        _playerInput = GetComponent<PlayerInput>();
        _playerRigidBody = GetComponent<Rigidbody>();
        Animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (IsMovePossible)
        {
            Move();
            Jump();
        }
        LeftClick();
        RightClick();

        if (!IsMovePossible)
        {
            if (Object == null)
            {
                return;
            }

            if (Object.GetComponent<PropMachine>().IsFixDone)
            {
                Object.GetComponent<IInteraction>().OffInteraction(gameObject);
                IsMovePossible = true;
                IsDoSomething = false;
            }
        }
    }

    private void Move()
    {
        float horizontalMove = _playerInput.MoveX;
        float vertical = _playerInput.MoveZ;
        Vector3 moveDistance = _playerInput.MoveX * transform.right * Speed * Time.deltaTime + _playerInput.MoveZ * transform.forward * Speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _playerRigidBody.MovePosition(_playerRigidBody.position + (2 * moveDistance));
        }
        else
        {
            _playerRigidBody.MovePosition(_playerRigidBody.position + moveDistance);
        }
        Animator.SetFloat("Vertical", vertical, 0.1f, Time.deltaTime);
        Animator.SetFloat("Horizontal", horizontalMove, 0.1f, Time.deltaTime);
        Animator.SetFloat("WalkSpeed", Speed);

    }
    private void Jump()
    {
        if (IsPlayerNotChange)
        {
            if (_playerInput.Jump && !IsJump)
            {
                _playerRigidBody.AddForce(transform.up * JumpForce, ForceMode.Impulse);
                IsJump = true;
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
    }

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
                    IsDoSomething = true;
                    IsMovePossible = false;
                    Object.GetComponent<IInteraction>().OnInteraction(gameObject);
                }
                // } 프롭머신을 고친다.
            }
            // } 무언가를 해야한다.
            // { 무언가 하던거를 그만한다.
            else
            {
                IsDoSomething = false;
                IsMovePossible = true;
                Object.GetComponent<IInteraction>().OffInteraction(gameObject);
            }
            // } 무언가 하던거를 그만한다.
        }
    }

    private void RightClick()
    {

    }
    public void GetDamage(GameObject obj)
    {
        HP--;
        if (HP < 0)
        {
            FallDown();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        IsJump = false;
        JumpCount = 0;
    }

    private void FallDown()
    {
        if (!IsPlayerNotChange)
        {
            Destroy(Change.ChangeObj);
            Change.Player.SetActive(true);
        }
        Status = PlayerStatus.FALLDOWN;
        IsplayerCanChange = false;
        transform.localRotation = Quaternion.Euler(90f, transform.localRotation.y, 0f);
    }

    public void SitOnChair()
    {
        Status = PlayerStatus.CAUGHT;
        IsMovePossible = false;
        IsplayerCanChange = false;
        transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
    }
    public void Hold()
    {
        _playerRigidBody.useGravity = false;
        Player.GetComponent<CapsuleCollider>().enabled = false;
    }


}
