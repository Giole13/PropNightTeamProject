using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 상호작용을 할 수 있는 것에 달리는 인터페이스
public interface IInteraction
{
    // 상호작용을 켰을 때
    public void OnInteraction(string tagName);

    // 상호작용을 껐을 때
    public void OffInteraction(string tagName);
}

public interface IDamage
{

    public void GetDamage();

}

