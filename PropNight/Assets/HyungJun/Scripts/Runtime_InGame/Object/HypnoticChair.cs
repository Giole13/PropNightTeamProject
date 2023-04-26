using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HypnoticChair : MonoBehaviour, IInteraction
{
    private enum HypnoticChairState
    {
        IDLE, WORKING
    }
    private HypnoticChairState _chairState;

    // 처형까지의 시간 ##################### - 중요함
    private float _maxExecutionTime = 100f;
    // 현재 처형까지의 시간
    private float _currentExecutionTime = 0f;

    private bool IsCountStart = true;

    // private GameObject PlayerObj = default;


    private void Awake()
    {
        _chairState = HypnoticChairState.IDLE;
    }
    public void OnInteraction(string tagName)
    {
        foreach (Transform _obj in transform) { _obj.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", Color.black); }
        IsCountStart = true;

        // obj.transform.SetParent(transform);

        // PlayerObj = obj;
        _chairState = HypnoticChairState.WORKING;
        // GetComponent<Collider>().isTrigger = true;
        // Rigidbody playerRigid = PlayerObj.gameObject.GetComponent<Rigidbody>();
        // playerRigid.useGravity = false;
        // playerRigid.velocity = Vector3.zero;
        // // PlayerObj.gameObject.GetComponent<PlayerInput>().enabled = false;
        // PlayerObj.GetComponent<PlayerInput>().enabled = false;
        // PlayerObj.GetComponent<PlayerMovement>().SitOnChair();
        // PlayerObj.GetComponent<PlayerChange>().enabled = false;
        // PlayerObj.transform.position = transform.position + new Vector3(0, 2f, 0);

        StartCoroutine(PlayerExecutionCountStart());
    }

    public void OffInteraction(string tagName)
    {
        IsCountStart = false;

        _chairState = HypnoticChairState.IDLE;
        // PlayerObj.transform.SetParent(transform.parent);
        // // GetComponent<Collider>().isTrigger = false;
        // Rigidbody playerRigid = PlayerObj.gameObject.GetComponent<Rigidbody>();
        // playerRigid.useGravity = true;
        // playerRigid.velocity = Vector3.zero;
        // PlayerObj.GetComponent<PlayerInput>().enabled = true;
        // PlayerObj.GetComponent<PlayerMovement>().enabled = true;
        // PlayerObj.GetComponent<PlayerChange>().enabled = true;
        // PlayerObj.transform.position = transform.position + new Vector3(3f, 0, 0);

        // PlayerObj = default;
        foreach (Transform _obj in transform) { _obj.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", Color.white); }
    }


    // 생존자가 의자에 앉혔을 때 처형까지의 시간을 카운트 하는 코루틴
    private IEnumerator PlayerExecutionCountStart()
    {
        while (IsCountStart)
        {
            yield return new WaitForSeconds(0.01f);
            _currentExecutionTime += 0.01f;

            if (_maxExecutionTime <= _currentExecutionTime)
            {
                // 최대처형시간까지 잡혀있다면 플레이어의 스크립트를 전부 켜주고 플레이어 오브젝트를 꺼버린다.
                // PlayerObj.SetActive(false);
                // OffInteraction(PlayerObj);
                yield break;
            }
        }
    }

    // // Start is called before the first frame update
    // void Start()
    // {

    // }

    // // Update is call00000000ed once per frame
    // void Update()
    // {

    // }

    // private void OnCollisionEnter(Collision other)
    // {
    //     // Debug.Log($"{other.transform.name}여기는 콜리젼 땅!");
    //     if (other.transform.tag == "Player" && _chairState == HypnoticChairState.IDLE)
    //     {
    //         OnInteraction(other.gameObject);
    //     }
    //     else if (other.transform.tag == "Player" && _chairState == HypnoticChairState.WORKING)
    //     {
    //         OffInteraction(other.gameObject);
    //     }
    // }

    // private void OnTriggerEnter(Collider other)
    // {
    //     Debug.Log($"{other.name}여기는 트리거 땅!");
    //     if (other.transform.tag == "Player")
    //     {
    //         // && _chairState == HypnoticChairState.WORKING

    //         OffInteraction(other.gameObject);
    //     }
    // }
}
