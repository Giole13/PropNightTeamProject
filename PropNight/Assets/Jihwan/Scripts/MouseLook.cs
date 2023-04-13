using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    public GameObject Obj;
    private float _maxDistance = 300f;
    private RaycastHit _hit;
    private float _xRotation = 0f;
    private PlayerInput _playerInput;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _playerInput= GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out _hit, _maxDistance))
        {
            Obj = _hit.transform.gameObject;
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
        playerBody.Rotate(Vector3.up * mouseX*5);
    }
}
