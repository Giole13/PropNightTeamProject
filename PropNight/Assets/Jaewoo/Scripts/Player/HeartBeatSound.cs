using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBeatSound : MonoBehaviour
{
    public AudioClip heartBeatSound;
    public AudioSource mySoucre;
    private void Awake()
    {
        mySoucre = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Killer")
        {
            mySoucre.PlayOneShot(heartBeatSound);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Killer")
        {
            mySoucre.Stop();
        }
    }
}
