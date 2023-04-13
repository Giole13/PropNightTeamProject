using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdViewCam : MonoBehaviour
{
    private float _xRotation;
    private float _yRotation;
    private PlayerInput _playerInput;

    public GameObject GameObj;
    public PlayerMovement Player;
    public float mouseSensitivity;
    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();    
    }

    
    void Update()
    {
        if(Player.IsPlayerNotChange)
        {

        }
        else
        {
            float RotateX = _playerInput.RotateX * mouseSensitivity * Time.deltaTime;
            float RotateY = _playerInput.RotateY * mouseSensitivity * Time.deltaTime;
            _xRotation -= RotateY;
            _yRotation -= RotateX;

            _xRotation = Mathf.Clamp(_xRotation, -70f, 40f);

            GameObj.transform.localRotation = Quaternion.Euler(_xRotation, _yRotation, 0f);

        }
    }
}
