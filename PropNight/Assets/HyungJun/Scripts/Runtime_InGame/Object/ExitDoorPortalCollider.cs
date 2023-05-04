using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoorPortalCollider : MonoBehaviour
{
    public ExitDoorPortal EDP;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.tag == "Player") EDP.PlayerEscape();
    }
}
