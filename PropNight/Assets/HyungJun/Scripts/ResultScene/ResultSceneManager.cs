using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;


public class ResultSceneManager : MonoBehaviourPunCallbacks
{
    public GameObject WinWait = default;

    public GameObject Win = default;
    public GameObject Lose = default;

    private DataContainer _dc = default;

    [SerializeField] private GameObject _camera = default;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        WinWait.SetActive(false);
        Win.SetActive(false);
        Lose.SetActive(false);

        _dc = GameObject.Find("DataContainer").GetComponent<DataContainer>();

        StartCoroutine(ShowResult());
    }

    // 결과 창을 보여주는 코루틴
    private IEnumerator ShowResult()
    {
        if (_dc.IsGameVictory) Win.SetActive(true);
        else Lose.SetActive(true);

        yield return new WaitForSeconds(3f);


        Instantiate(_dc.ClientObject, new Vector3(0f, -1f, -8f), Quaternion.identity);


        Win.SetActive(false);
        Lose.SetActive(false);
        WinWait.SetActive(true);
    }

    // 로비로 돌아가는 함수
    public void BacktoLobby()
    {
        SceneManager.LoadSceneAsync(Define.TITLE_MAIN_SCENE_NAME);

        // 방을 나가는 함수
        PhotonNetwork.LeaveRoom();
    }


}
