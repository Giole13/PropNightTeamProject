using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoExitPortal : MonoBehaviour
{
    private BoxCollider _portalCollider;

    public bool _IsOpened = false;
    private GameStatusManager StatusManager;
    void Start()
    {
        StatusManager = GameObject.Find("GameStatusManager").GetComponent<GameStatusManager>();
        _portalCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (StatusManager.IsCanEscape)
        {
            _portalCollider.enabled = true;
        }
        else
        {
            _portalCollider.enabled = false;
        }
    }

    // 플레이어와 충돌하면 결과화면을 보여준다.
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Player")
        {
            // 승리조건에 추가


            // Gfunc.LoadScene(Define.RESULT_SCENE_NAME);
        }
    }

    public void DoorOpen()
    {
        _IsOpened = true;
        GetComponent<MeshRenderer>().material.SetColor("_BaseColor", Color.blue);
    }

}
