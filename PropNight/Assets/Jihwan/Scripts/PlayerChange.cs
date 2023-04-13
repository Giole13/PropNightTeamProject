using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerChange : MonoBehaviour
{
    public MouseLook Look;
    public GameObject ChangeObj;
    public GameObject Player;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Look.Obj == null)
            {
                return;
            }
            if (Look.Obj.tag != "Change")
            {
                return;
            }
            if (ChangeObj != null)
            {
                Destroy(ChangeObj);
            }

            ChangeObj = Instantiate(Look.Obj);
            ChangeObj.transform.SetParent(transform, true);
            ChangeObj.transform.localPosition = Vector3.zero;
            Player.SetActive(false);
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (Player.activeSelf)
            {
                return;
            }

            Destroy(ChangeObj);
            ChangeObj = null;
            Player.SetActive(true);
        }
    }

}
