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
        _playerInput= GetComponent<PlayerInput>();
        _playerRigidBody = GetComponent<Rigidbody>(); 
    }
    void FixedUpdate()
    {
        Move();
        Jump();
    }

<<<<<<< HEAD
        if (IsGrounded && velocity.y < 0)
=======
    private void Move()
    {
        Vector3 moveDistance = _playerInput.MoveX*transform.right * Time.deltaTime + _playerInput.MoveZ*transform.forward * Time.deltaTime;
        Debug.Log(moveDistance);
        _playerRigidBody.MovePosition(_playerRigidBody.position + moveDistance);
    }
    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
>>>>>>> a0327af4c4911765f703b183040c3049136c4c55
        {
            if(Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 2))
            {
                _playerRigidBody.AddForce(transform.up * jumpForce);
            }
        }
<<<<<<< HEAD
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            controller.Move(move * speed * 2 * Time.deltaTime);
        }
        else
        {
            controller.Move(move * speed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && IsGrounded)
        {
            velocity.y = Mathf.Sqrt(JumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
=======
>>>>>>> a0327af4c4911765f703b183040c3049136c4c55
    }
}
