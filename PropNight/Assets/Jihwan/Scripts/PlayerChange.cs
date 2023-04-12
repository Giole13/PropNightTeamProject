using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerChange : MonoBehaviour
{
    public MouseLook Look;
    public GameObject ChangeObj;
    public GameObject Player;

    private PlayerMovement _playerMovement;
    private PlayerInput _playerInput;

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerMovement = GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        if (_playerInput.LeftClick)
        {
            if (Look.Obj == null)
            {
                return;
            }
            if (Look.Obj.tag != "Change")
            {
                return;
            }
            if (ChangeObj != null)
            {
                Destroy(ChangeObj);
            }
            _playerMovement.IsPlayerNotChange = false;
            ChangeObj = Instantiate(Look.Obj);
            ChangeObj.transform.SetParent(transform, true);
            ChangeObj.transform.localPosition = Vector3.zero;
            Player.SetActive(false);
        }
        if (_playerInput.RightClick)
        {
            if (Player.activeSelf)
            {
                return;
            }
            _playerMovement.IsPlayerNotChange = true;
            Destroy(ChangeObj);
            ChangeObj = null;
            Player.SetActive(true);
        }
    }

}
