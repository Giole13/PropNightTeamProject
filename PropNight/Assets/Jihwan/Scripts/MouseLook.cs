using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    public GameObject Obj;
    public float ObjDistance;
    public PlayerMovement Player;
    public CinemachineVirtualCamera VirtualCamera;

    private float _maxDistance = 300f;
    private RaycastHit _hit;
    private float _xRotation = 0f;
    private PlayerInput _playerInput;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _playerInput = GetComponent<PlayerInput>();
        VirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Player.IsPlayerNotChange && Player.IsMovePossible)
        {
            VirtualCamera.Priority = 11;
            if (Physics.Raycast(transform.position, transform.forward, out _hit, _maxDistance))
            {
                Obj = _hit.transform.gameObject;
                ObjDistance = _hit.distance;
            }
            else
            {
                Obj = null;
            }


            float mouseX = _playerInput.RotateX * mouseSensitivity * Time.deltaTime;
            float mouseY = _playerInput.RotateY * mouseSensitivity * Time.deltaTime;

            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -70f, 40f);

            transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX * 5);

        }
        else
        {
            VirtualCamera.Priority = 9;
        }


    }
}
