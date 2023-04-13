using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{

    private PlayerInput _playerInput;
    private Rigidbody _playerRigidBody;

    public float jumpForce;

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerRigidBody = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        Vector3 moveDistance = _playerInput.MoveX * transform.right * Time.deltaTime + _playerInput.MoveZ * transform.forward * Time.deltaTime;
        // Debug.Log(moveDistance);
        _playerRigidBody.MovePosition(_playerRigidBody.position + moveDistance);
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 2))
            {
                _playerRigidBody.AddForce(transform.up * jumpForce);
            }
        }
    }
}
