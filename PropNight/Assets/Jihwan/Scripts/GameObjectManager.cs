using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectManager : MonoBehaviour
{
    [SerializeField]
    Dictionary<int, GameObject> ObjDir = new Dictionary<int, GameObject>();
    // Start is called before the first frame update
    public void Awake()
    {
        GeneratedData();
    }

    public void GeneratedData()
    {
        GameObject ObjGroup = GameObject.Find("Objects");
        int Count = 1;
        for (int i = 0; i < ObjGroup.transform.childCount; i++)
        {
            if (ObjGroup.transform.GetChild(i).gameObject.tag == "Change")
            {
                ObjDir.Add(Count, ObjGroup.transform.GetChild(i).gameObject);
                Debug.Log($"{Count},{ObjGroup.transform.GetChild(i).gameObject.name}");
                Count++;
            }
        }
    }

    public int GetIndex(GameObject obj)
    {
        for (int i = 1; i < ObjDir.Count + 1; i++)
        {
            if (obj == ObjDir[i])
            {
                return i;
            }

        }
        return 0;
    }
    public GameObject GetObj(int index)
    {
        return ObjDir[index];
    }
}
