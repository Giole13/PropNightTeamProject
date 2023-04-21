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

    [Tooltip("생존자 오브젝트")]
    public GameObject PlayerPrefab;
    public GameObject PlayerCameraPrefab;


    [Tooltip("킬러 오브젝트")]
    public GameObject KillerPrefab;



    // Start is called before the first frame update
    void Start()
    {
        // PhotonNetwork.Instantiate(PlayerCameraPrefab.name, Vector3.zero, Quaternion.identity);

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate(KillerPrefab.name, Vector3.zero, Quaternion.identity);
        }
        else
        {
            PhotonNetwork.Instantiate(PlayerPrefab.name, Vector3.zero, Quaternion.identity);
        }


    }

    // Update is called once per frame
    void Update()
    {

    }
}
