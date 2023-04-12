using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HypnoticChair : MonoBehaviour, IInteraction
{
    enum HypnoticChairState
    {
        IDLE,

    }
    public void OffInteraction()
    {
        throw new System.NotImplementedException();
    }

    public void OnInteraction()
    {
        throw new System.NotImplementedException();
    }

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
            GetComponent<Collider>().isTrigger = true;
            Debug.Log("메롱");
            Rigidbody playerRigid = other.gameObject.GetComponent<Rigidbody>();
            playerRigid.useGravity = false;
            playerRigid.velocity = Vector3.zero;
            other.gameObject.GetComponent<PlayerInput>().enabled = false;
            other.gameObject.GetComponent<PlayerMovement>().enabled = false;
            other.transform.position = transform.position + new Vector3(0, 1f, 0);

            foreach (Transform obj in transform)
            {
                obj.GetComponent<Renderer>().material.SetColor("_BaseColor", Color.black);
            }
        }
    }
}
