using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EPOOutline;

// 변신 가능한 사물의 HP를 결정하는 스크립트
public class TransformableObject : MonoBehaviour, IInteraction
{
    private Outlinable _outLinableScript;

    public ObjectSize SelectObjectSize;
    public int ObjectHp { get; private set; }

    public void OffInteraction(string tagName)
    {
        _outLinableScript.enabled = true;
    }

    public void OnInteraction(string tagName)
    {
        throw new System.NotImplementedException();
    }

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

    private void Start()
    {
        _outLinableScript = GetComponent<Outlinable>();

        _outLinableScript.enabled = false;
    }
}
