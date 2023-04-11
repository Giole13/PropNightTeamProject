using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropMachine : MonoBehaviour
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
        Debug.Log("[PropMachine] OnCollisionEnter : 난 충돌했어요!");
    }
}
