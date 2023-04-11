using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropMachine : MonoBehaviour
{
    private float _gauge = 100f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    // 플레이어와 충돌하면 플레이어 상호작용 UI 팝업 & 상호작용 게이지 상승
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("[PropMachine] OnCollisionEnter : 난 충돌했어요!");
    }
}
