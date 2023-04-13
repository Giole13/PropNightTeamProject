using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 변신 가능한 사물의 HP를 결정하는 스크립트
public class TransformableObject : MonoBehaviour
{
    public ObjectSize SelectObjectSize;
    public int ObjectHp { get; private set; }
    private void Awake()
    {
        switch (SelectObjectSize)
        {
            case ObjectSize.SMALL:
                ObjectHp = 75;
                break;
            case ObjectSize.MIDDLE:
                ObjectHp = 100;
                break;
            case ObjectSize.BIG:
                ObjectHp = 125;
                break;
        }
    }
}
