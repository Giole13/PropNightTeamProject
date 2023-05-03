using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameCharactorData : MonoBehaviour
{
    public Sprite[] killerSkill = new Sprite[2];
    public Sprite[] suviverSkill = new Sprite[4];
    //인게임에 캐릭터 나올시 프리팹을 비교해 스킬과 이름 뜨게 하기중
    public InGameCharactorData inGameCharactorData;
    public CharacterDataBase characterDB;
    public CharacterDataBase killerDB;

    public TMP_Text characterName;
    public TMP_Text abilityName;
    public Image abilityImage;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
