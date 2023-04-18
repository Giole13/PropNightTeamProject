using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdViewCam : MonoBehaviour
{
    private float _xRotation;
    private float _yRotation;
    private PlayerInput _playerInput;


    public PlayerChange ChangeObj;
    public GameObject GameObj;
    public GameObject PlayerObj;
    public Transform FirstCam;
    public PlayerMovement Player;
    public float mouseSensitivity;
    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
    }


    void Update()
    {
        if (Player.Status != PlayerStatus.NORMAL)
        {
            return;
        }

        float mouseX = _playerInput.RotateX * mouseSensitivity * Time.deltaTime;
        float mouseY = _playerInput.RotateY * mouseSensitivity * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -70f, 40f);
        _yRotation -= mouseX;
        //{ 1인칭 시점
        if (Player.IsPlayerNotChange && Player.IsMovePossible)
        {

            float RotateX = _playerInput.RotateX * mouseSensitivity * Time.deltaTime;
            _yRotation -= RotateX;
            PlayerObj.transform.localRotation = Quaternion.Euler(PlayerObj.transform.localRotation.x, -_yRotation, 0f);

            // 2023-04-18 / HyungJun / Debug를 위한 주석처리 -> 주석 해제해도 무방
            FirstCam.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
            PlayerObj.transform.localRotation = Quaternion.Euler(0f, -_yRotation, 0f);
        }
        //} 1인칭 시점

        //{ 3인칭 시점
        else
        {

            FirstCam.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
            //{ 플레이어가 오브젝트로 변신한 경우
            if (!Player.IsPlayerNotChange)
            {
                ChangeObj.ChangeObj.transform.localRotation = Quaternion.Euler(0f, _yRotation, 0f);
                PlayerObj.transform.localRotation = Quaternion.Euler(0f, -_yRotation, 0f);
            }
            if (!Player.IsMovePossible)
            {
                Debug.Log("!!");
                FirstCam.localRotation = Quaternion.Euler(_xRotation, 180 - _yRotation, 0f);
            }
            //} 플레이어가 오브젝트로 변신한 경우
            //{ 플레이어가 작업중일 경우
            // if (!Player.IsMovePossible)
            // {
            //     PlayerObj.transform.localRotation = Quaternion.Euler(PlayerObj.transform.localRotation.x, -_yRotation, 0f);
            //     return;
            // }
            //{ 플레이어가 작업중일 경우





        }
        //} 3인칭 시점
    }
}
