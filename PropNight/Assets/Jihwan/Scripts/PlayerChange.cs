using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Photon.Pun;
public class PlayerChange : MonoBehaviourPun
{
    private GameObjectManager _gameObjManager;
    public MouseLook Look;

    public GameObject ChangeObj;
    public GameObject Player;


    private PlayerMovement _playerMovement;
    private PlayerInput _playerInput;

    private void Start()
    {
        _gameObjManager = GameObject.Find("ObjectManager").GetComponent<GameObjectManager>();
        _playerInput = GetComponent<PlayerInput>();
        _playerMovement = GetComponent<PlayerMovement>();
    }


    private void Update()
    {
        if (!photonView.IsMine) { return; }

        if (!_playerMovement.IsplayerCanChange)
        {
            return;
        }


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
                return;
            }
            Debug.Log("!!");
            //Transforming();
            photonView.RPC("Transforming", RpcTarget.All, _gameObjManager.GetIndex(Look.Obj));
        }
        if (_playerInput.RightClick)
        {
            //UnTransforming();
            photonView.RPC("UnTransforming", RpcTarget.All);
        }
    }
    [PunRPC]
    public void Transforming(int index)
    {

        if (ChangeObj != null)
        {
            Destroy(ChangeObj);
        }
        _playerMovement.IsPlayerNotChange = false;
        // ChangeObj = PhotonNetwork.InstantiateSceneObject("Look.Obj", transform.position, Quaternion.identity);
        ChangeObj = Instantiate(_gameObjManager.GetObj(index), transform.position, Quaternion.identity);
        ChangeObj.transform.SetParent(transform, true);
        ChangeObj.transform.localPosition = Vector3.zero;
        Player.SetActive(false);
        transform.position += new Vector3(0f, ChangeObj.transform.position.y + 1, 0f);
        Debug.Log(ChangeObj.name);
    }
    [PunRPC]
    public void UnTransforming()
    {
        //if (!photonView.IsMine) { return; }

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