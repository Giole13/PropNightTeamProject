using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using Photon.Pun;

public class MouseLook : MonoBehaviourPun
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    public GameObject Obj;
    public float ObjDistance;
    public PlayerMovement Player;
    public CinemachineVirtualCamera FirstVirtualCamera;

    private float _maxDistance = 100f;
    private RaycastHit _hit = default;
    private float _xRotation = 0f;
    private PlayerInput _playerInput;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _playerInput = GetComponent<PlayerInput>();
        FirstVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine) { return; }

        if (Player.IsPlayerNotChange && Player.IsMovePossible)
        {

            FirstVirtualCamera.Priority = 12;

            photonView.RPC("Search", RpcTarget.All);

        }
        else
        {
            FirstVirtualCamera.Priority = 11;

        }


    }
    [PunRPC]
    public void Search()
    {
        if (Physics.Raycast(transform.position, transform.forward, out _hit, _maxDistance))
        {
            Obj = _hit.collider.gameObject;
            ObjDistance = _hit.distance;

        }
        else
        {
            Obj = null;
        }
    }
}
