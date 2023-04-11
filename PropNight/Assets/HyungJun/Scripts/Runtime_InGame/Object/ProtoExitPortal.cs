using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoExitPotal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // 플레이어와 충돌하면 결과화면을 보여준다.
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Player")
        {
            Gfunc.LoadScene("03.Result");
        }
    }
}
