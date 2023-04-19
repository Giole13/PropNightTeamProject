using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class InGameManager : MonoBehaviourPunCallbacks /*, IPunObservable*/
{
    // public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    // {
    //     throw new System.NotImplementedException();
    // }

    public GameObject PlayerPrefab;
    public GameObject PlayerCameraPrefab;



    // Start is called before the first frame update
    void Start()
    {
        // PhotonNetwork.Instantiate(PlayerCameraPrefab.name, Vector3.zero, Quaternion.identity);
        PhotonNetwork.Instantiate(PlayerPrefab.name, Vector3.zero, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
