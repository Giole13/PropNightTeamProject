using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoExitDoor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Player")
        {
            Gfunc.LoadScene("03.Result");
        }
    }
}
