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
    private bool IsFixPropMachine = false;
    private bool IsMovePossible = true;
    public MouseLook Look;
    public float Speed;
    public float jumpForce;
    public GameObject Object;

    private void Start()
    {
        _playerInput= GetComponent<PlayerInput>();
        _playerRigidBody = GetComponent<Rigidbody>(); 
    }
    private void Update()
    {
        if(IsMovePossible)
        {
            Move();
            Jump(); 
        }
        LeftClick();
        RightClick();
    }

    private void Move()
    {
        Vector3 moveDistance = _playerInput.MoveX*transform.right * Speed * Time.deltaTime + _playerInput.MoveZ*transform.forward * Speed * Time.deltaTime;
        _playerRigidBody.MovePosition(_playerRigidBody.position + moveDistance);
    }
    private void Jump()
    {
        if(_playerInput.Jump && !IsJump) 
        {
            _playerRigidBody.AddForce(transform.up * jumpForce,ForceMode.Impulse);
            IsJump = true;
        }
    }

    private void LeftClick()
    {
        if(_playerInput.LeftClick)
        {
            if(Look.Obj.tag == "PropMachine" && Look.ObjDistance <5)
            {
                Object = Look.Obj;
                IsFixPropMachine = true;
                IsMovePossible = false;
                Object.GetComponent<IInteraction>().OnInteraction();
            }
        }
    }

    private void RightClick()
    {
        if(_playerInput.RightClick)
        {
            if(IsFixPropMachine)
            {
                IsFixPropMachine = false;
                IsMovePossible = true;
                Object.GetComponent<IInteraction>().OffInteraction();
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        IsJump = false;
    }
}
