using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using Photon.Pun;
using EPOOutline;

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
    private Transform _highLightTr;

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
        if (Player.Status == PlayerStatus.DIE)
        {
            FirstVirtualCamera.Priority = 10;
            return;
        }
        if (!Player.IsPlayerNotChange || !Player.IsMovePossible || Player.IsFallDown)
        {
            FirstVirtualCamera.Priority = 11;
        }
        else
        {
            FirstVirtualCamera.Priority = 12;
            // photonView.RPC("Search", RpcTarget.All);
            Search();
        }

        // { 2023.05.01 / HyungJun / 아웃라인을 위한 로직
        // if (Obj == null) { /* Do nothing */ }
        // else if (Obj.tag == "Change")
        // {
        //     // 레이를 맞은 오브젝트의 태그가 Change라면 아웃라인 활성화

        // }
        // 레이를 쐈을 때 아웃라인을 보기 위한 로직

        Debug.DrawRay(transform.position, transform.forward * 100, Color.red);
        if (Physics.Raycast(transform.position, transform.forward, out _hit, _maxDistance))
        {

            if (_hit.transform.tag == "Change")
            {
                if (_highLightTr != null)
                {
                    _highLightTr.GetComponent<Outlinable>().enabled = false;
                    _highLightTr = null;
                }

                _highLightTr = _hit.transform;

                if (_highLightTr.GetComponent<Outlinable>() != null)
                {
                    _highLightTr.GetComponent<Outlinable>().enabled = true;
                }
            }
            else
            {
                if (_highLightTr != null)
                {
                    _highLightTr.GetComponent<Outlinable>().enabled = false;
                    _highLightTr = null;
                }
            }
        }









        // } 2023.05.01 / HyungJun / 아웃라인을 위한 로직

    }
    [PunRPC]
    public void Search()
    {
        if (Physics.Raycast(transform.position, transform.forward, out _hit, _maxDistance))
        {
            Obj = _hit.collider.gameObject;
            ObjDistance = _hit.distance;

            //! 맞은 오브젝트의 태그가 Change 라면 아웃라인 활성화
            // if (Obj.tag == "Change")
            // {

            // }
            // else
            // {

            // }
        }
        else
        {

            Obj = null;
        }
    }
}

