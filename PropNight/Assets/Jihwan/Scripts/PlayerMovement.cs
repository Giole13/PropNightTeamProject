using System.Collections;
using System.Collections.Generic;
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

    Vector3 velocity;
    bool IsGrounded;
    // Update is called once per frame
    void Update()
    {
        IsGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(IsGrounded && velocity.y <0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        Vector3 move = transform.right* x + transform.forward *z;
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
