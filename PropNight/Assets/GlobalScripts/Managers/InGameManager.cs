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



    [SerializeField] private GameObject _playerUI;
    [SerializeField] private GameObject _KillerUI;


    [Tooltip("생존자 오브젝트")] public GameObject PlayerPrefab;
    [Tooltip("킬러 오브젝트")] public GameObject KillerPrefab;



    // Start is called before the first frame update
    void Start()
    {
        _playerUI.SetActive(false);
        _KillerUI.SetActive(false);
        // PhotonNetwork.Instantiate(PlayerCameraPrefab.name, Vector3.zero, Quaternion.identity);

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate(KillerPrefab.name, Vector3.zero, Quaternion.identity);
            _KillerUI.SetActive(true);
        }
        else
        {
            PhotonNetwork.Instantiate(PlayerPrefab.name, Vector3.zero, Quaternion.identity);
            _playerUI.SetActive(true);
        }


    }

    // Update is called once per frame
    void Update()
    {

    }
}
