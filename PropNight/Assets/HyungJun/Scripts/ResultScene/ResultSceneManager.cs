using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultSceneManager : MonoBehaviour
{
    public GameObject WinWait = default;

    public GameObject Win = default;
    public GameObject Lose = default;

    private DataContainer _dc = default;


    // Start is called before the first frame update
    void Start()
    {
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

        Win.SetActive(false);
        Lose.SetActive(false);
        WinWait.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
