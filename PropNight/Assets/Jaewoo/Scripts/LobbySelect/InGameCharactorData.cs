using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class InGameCharactorData : MonoBehaviour
{
    public Sprite[] killerSkill = new Sprite[2];
    public Sprite[] suviverSkill = new Sprite[4];
    public Sprite[] killerIcon = new Sprite[2];
    public Sprite[] suviverIcon = new Sprite[4];
    //인게임에 캐릭터 나올시 프리팹을 비교해 스킬과 이름 뜨게 하기중

    public InGameManager inGameManager;
    public CharacterDataBase characterDB;
    public CharacterDataBase killerDB;

    public TMP_Text characterName;
    public TMP_Text characterAbilityName;
    public TMP_Text killerName;
    public TMP_Text killerAbilityName;

    public Image charatorImage;
    public Image abilityImage;
    public Image killerImage;
    public Image killerAibilityImage;

    private int selectedPlayer = 0;
    private int selectedKiller = 0;
    // Start is called before the first frame update
    void Awake()
    {

        //생존자와 살인마 아이콘 리소스
        suviverIcon[0] = Resources.Load<Sprite>("UiIcon/Icon/Runner-removebg-preview");
        suviverIcon[1] = Resources.Load<Sprite>("UiIcon/Icon/Healer-removebg-preview");
        suviverIcon[2] = Resources.Load<Sprite>("UiIcon/Icon/Phocho-removebg-preview");
        suviverIcon[3] = Resources.Load<Sprite>("UiIcon/Icon/superjumper-removebg-preview");

        killerIcon[0] = Resources.Load<Sprite>("UiIcon/Icon/NoHairKiller-removebg-preview");
        killerIcon[1] = Resources.Load<Sprite>("UiIcon/Icon/HairKiller-removebg-preview");
        //생존자와 살인마 스킬 리소스 
        suviverSkill[0] = Resources.Load<Sprite>("skills/icons_skills_machine");
        suviverSkill[1] = Resources.Load<Sprite>("skills/icons_skills_alchemy");
        suviverSkill[2] = Resources.Load<Sprite>("skills/icons_skills_revive");
        suviverSkill[3] = Resources.Load<Sprite>("skills/icons_skills_acrobat");

        killerSkill[0] = Resources.Load<Sprite>("skills/Thief_Assassination");
        killerSkill[1] = Resources.Load<Sprite>("skills/Samurai_Ilseom");
    }
    void Start()
    {
        switch (DataContainer.PlayerSelectNumber)
        {
            case 0:
                selectedPlayer = 0;
                break;
            case 1:
                selectedPlayer = 1;
                break;
            case 2:
                selectedPlayer = 2;
                break;
            case 3:
                selectedPlayer = 3;
                break;
            default:
                break;
        }
        switch (DataContainer.KillerSelectNumber)
        {
            case 0:
                selectedKiller = 0;
                break;
            case 1:
                selectedKiller = 1;
                break;
            default:
                break;
        }


        //마스터일때
        if (PhotonNetwork.IsMasterClient)
        {
            UpdateKillerUi(selectedKiller);
        }
        //그외 플레이어들
        else
        {
            UpdateCharacterUi(selectedPlayer);
        }
    }

    //인게임 플레이어들의 ui 
    private void UpdateCharacterUi(int selectedPlayer)
    {
        CharacterData character = characterDB.GetCharacter(selectedPlayer);
        characterName.text = character.characterName;
        characterAbilityName.text = character.abilityName;
        charatorImage.sprite = suviverIcon[selectedPlayer];
        abilityImage.sprite = suviverSkill[selectedPlayer];
    }
    private void UpdateKillerUi(int selectedKiller)
    {
        CharacterData character = killerDB.GetCharacter(selectedKiller);
        killerName.text = character.characterName;
        killerAbilityName.text = character.abilityName;
        killerImage.sprite = killerIcon[selectedKiller];
        killerAibilityImage.sprite = killerSkill[selectedKiller];
    }
    // Update is called once per frame
}
