using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 10f;
    public float gravity = -9.18f;
    public float JumpHeight = 3f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    
    private Vector3 velocity;
    private bool IsGrounded;
    private PlayerInput _playerInput;

    private void Start()
    {
        _playerInput= GetComponent<PlayerInput>();
    }
    void Update()
    {
        
        IsGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(IsGrounded && velocity.y <0)
        {
            velocity.y = -2f;
        }
        float x = _playerInput.MoveX;
        float z = _playerInput.MoveZ;
        
        Vector3 move = transform.right * x + transform.forward * z;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            controller.Move(move * speed * 2 * Time.deltaTime);
        }
        else
        {
            controller.Move(move * speed * Time.deltaTime);
        }
        
        if(Input.GetButtonDown("Jump")&& IsGrounded)
        {
            velocity.y = Mathf.Sqrt(JumpHeight * -2f* gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
