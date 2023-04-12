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

    public GameObject PlayerObj = default;
    public void OnInteraction()
    {
        if (PlayerObj == default) { return; }
        _chairState = HypnoticChairState.WORKING;
        GetComponent<Collider>().isTrigger = true;
        Rigidbody playerRigid = PlayerObj.gameObject.GetComponent<Rigidbody>();
        playerRigid.useGravity = false;
        playerRigid.velocity = Vector3.zero;
        // PlayerObj.gameObject.GetComponent<PlayerInput>().enabled = false;
        PlayerObj.gameObject.GetComponent<PlayerInput>().enabled = false;
        PlayerObj.gameObject.GetComponent<PlayerMovement>().enabled = false;
        PlayerObj.gameObject.GetComponent<PlayerChange>().enabled = false;
        PlayerObj.transform.position = transform.position + new Vector3(0, 1f, 0);
        foreach (Transform obj in transform)
        { obj.GetComponent<Renderer>().material.SetColor("_BaseColor", Color.black); }
    }

    public void OffInteraction()
    {
        if (PlayerObj == default) { return; }
        _chairState = HypnoticChairState.IDLE;
        GetComponent<Collider>().isTrigger = false;
        Rigidbody playerRigid = PlayerObj.gameObject.GetComponent<Rigidbody>();
        playerRigid.useGravity = true;
        playerRigid.velocity = Vector3.zero;
        PlayerObj.gameObject.GetComponent<PlayerInput>().enabled = true;
        PlayerObj.gameObject.GetComponent<PlayerMovement>().enabled = true;
        PlayerObj.gameObject.GetComponent<PlayerChange>().enabled = true;
        PlayerObj.transform.position = transform.position + new Vector3(1.5f, 0f, 0);
        foreach (Transform obj in transform)
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
