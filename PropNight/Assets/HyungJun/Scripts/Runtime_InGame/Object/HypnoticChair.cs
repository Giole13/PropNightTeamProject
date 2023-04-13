using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HypnoticChair : MonoBehaviour, IInteraction
{
    private HypnoticChairState _chairState = HypnoticChairState.IDLE;
    private enum HypnoticChairState
    {
        IDLE, WORKING
    }

    private GameObject PlayerObj = default;
    public void OnInteraction(GameObject obj)
    {
        PlayerObj = obj;
        _chairState = HypnoticChairState.WORKING;
        GetComponent<Collider>().isTrigger = true;
        Rigidbody playerRigid = obj.gameObject.GetComponent<Rigidbody>();
        playerRigid.useGravity = false;
        playerRigid.velocity = Vector3.zero;
        // PlayerObj.gameObject.GetComponent<PlayerInput>().enabled = false;
        obj.GetComponent<PlayerInput>().enabled = false;
        obj.GetComponent<PlayerMovement>().enabled = false;
        obj.GetComponent<PlayerChange>().enabled = false;
        obj.transform.position = transform.position + new Vector3(0, 1f, 0);
        foreach (Transform _obj in transform)
        { obj.GetComponent<Renderer>().material.SetColor("_BaseColor", Color.black); }
    }

    public void OffInteraction(GameObject obj)
    {
        _chairState = HypnoticChairState.IDLE;
        GetComponent<Collider>().isTrigger = false;
        Rigidbody playerRigid = obj.gameObject.GetComponent<Rigidbody>();
        playerRigid.useGravity = true;
        playerRigid.velocity = Vector3.zero;
        obj.GetComponent<PlayerInput>().enabled = true;
        obj.GetComponent<PlayerMovement>().enabled = true;
        obj.GetComponent<PlayerChange>().enabled = true;
        obj.transform.position = transform.position + new Vector3(1.5f, 0, 0);
        foreach (Transform _obj in transform)
        { obj.GetComponent<Renderer>().material.SetColor("_BaseColor", Color.white); }
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is call00000000ed once per frame
    void Update()
    {

    }

    // private void OnCollisionEnter(Collision other)
    // {
    //     if (other.transform.tag == "Player" && _chairState == HypnoticChairState.IDLE)
    //     {
    //     }
    //     else if (other.transform.tag == "Player" && _chairState == HypnoticChairState.WORKING)
    //     {
    //     }
    // }
}
