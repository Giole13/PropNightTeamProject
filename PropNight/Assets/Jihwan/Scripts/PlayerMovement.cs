using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{

    private PlayerInput _playerInput;
    private Rigidbody _playerRigidBody;
    private bool IsJump;
    private bool IsDoSomething = false;
    public bool IsMovePossible = true;
    public bool IsPlayerNotChange = true;
    public MouseLook Look;
    public float Speed;
    public float JumpForce;

    public float DashGauge;
    public GameObject Object;

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerRigidBody = GetComponent<Rigidbody>();
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
            if (Object.GetComponent<PropMachine>().IsFixDone)
            {
                IsMovePossible = true;
            }
        }
    }

    private void Move()
    {
        Vector3 moveDistance = _playerInput.MoveX * transform.right * Speed * Time.deltaTime + _playerInput.MoveZ * transform.forward * Speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _playerRigidBody.MovePosition(_playerRigidBody.position + (2 * moveDistance));
        }
        else
        {
            _playerRigidBody.MovePosition(_playerRigidBody.position + moveDistance);
        }


    }
    private void Jump()
    {
        if (_playerInput.Jump && !IsJump)
        {
            _playerRigidBody.AddForce(transform.up * JumpForce, ForceMode.Impulse);
            IsJump = true;
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

    void OnCollisionEnter(Collision collision)
    {
        IsJump = false;
    }
}
