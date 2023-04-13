using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float MoveX { get; private set; }
    public float MoveZ { get; private set; }
    public float RotateX { get; private set; }
    public float RotateY { get; private set; }
    public bool Jump { get; private set; }
    public bool LeftClick { get; private set; }

    public bool RightClick {get; private set; }

    // Update is called once per frame
    void Update()
    {
        MoveX = Input.GetAxis("Horizontal");
        MoveZ = Input.GetAxis("Vertical");  

        RotateX = Input.GetAxis("Mouse X");
        RotateY = Input.GetAxis("Mouse Y");

        Jump = Input.GetButtonDown("Jump");
        LeftClick = Input.GetButtonDown("Fire1");
        RightClick = Input.GetButtonDown("Fire2");
    }
}
