using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerMoveControl : MonoBehaviour
{
    public float Speed = 5f;
    // _KillerRigidbody라는 변수로 Rigidbody Component를 통제한다는 뜻. 즉, Inspector가 아니라 코드에서도 통제가 가능한것
    private Rigidbody _KillerRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _KillerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        KillerMove();
    }


    public void KillerMove()
    {
        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");

        _KillerRigidbody.velocity = new Vector3(xMove * Speed, 0, zMove * Speed);
    }



}
