using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class CharactorManager : MonoBehaviourPun
{
    [SerializeField]
    private GameObject noHairKiller = default;
    [SerializeField]
    private GameObject runnerPlayer = default;
    public GameObject ifPlayertrue;
    public GameObject[] playerPrefabs = new GameObject[4];
    public GameObject[] killerPrefabs = new GameObject[2];

    // 플레이어 선택 창
    public GameObject PlayerGridGroup = default;
    // 살인마 선택 창
    public GameObject KillerGridGroup = default;

    public Transform parent;
    public GameObject destoryObj;
    public CharacterDataBase characterDB;
    public CharacterDataBase killierDB;
    public TMP_Text characterName;
    public TMP_Text abilityName;
    public TMP_Text abilityExpanation;
    public Image abilityImage;
    private Sprite[] sprites = new Sprite[4];
    private Sprite[] killerSprites = new Sprite[4];
    private int selectedPlayer = 0;
    private int selectedKiller = 0;
    // private int selectedOption = 0;

    // private GameObject _parentInstance;
    void Awake()
    {
        sprites[0] = Resources.Load<Sprite>("skills/icons_skills_machine");
        sprites[1] = Resources.Load<Sprite>("skills/icons_skills_alchemy");
        sprites[2] = Resources.Load<Sprite>("skills/icons_skills_revive");
        sprites[3] = Resources.Load<Sprite>("skills/icons_skills_acrobat");

        killerSprites[0] = Resources.Load<Sprite>("skills/Thief_Assassination");
        killerSprites[1] = Resources.Load<Sprite>("skills/Samurai_Ilseom");

        KillerGridGroup.SetActive(false);
        PlayerGridGroup.SetActive(false);
    }
    void Start()
    {
        // 2023.05.02 / HyungJun / 여기에 PhotonNetwork.IsMasterClient 구문 작성 요함
        if (PhotonNetwork.IsMasterClient)
        {
            // 만약 마스터 클라이언트가 들어왔다면 킬러 선택창을 보여줌
            KillerGridGroup.SetActive(true);
            noHairKiller.SetActive(true);
            UpdateKiller(selectedKiller);
        }
        else
        {
            // 마스터 클라이언트가 아니면 플레이어 선택창을 보여줌
            PlayerGridGroup.SetActive(true);
            runnerPlayer.SetActive(true);
            UpdateCharacter(selectedPlayer);
        }
    }
    private void UpdateCharacter(int selectedPlayer)
    {
        CharacterData character = characterDB.GetCharacter(selectedPlayer);
        characterName.text = character.characterName;
        abilityName.text = character.abilityName;
        abilityExpanation.text = character.abilityExpanation;
        abilityImage.sprite = sprites[selectedPlayer];
    }
    //     UpdateCharacter(selectedOption);
    // }

    // private void UpdateCharacter(int selectedOption)
    // {
    //     if (_parentInstance != null) { Destroy(_parentInstance); }
    //     // { 2023.05.01 / HyungJun / Debug를 위한 주석처리
    //     CharacterData character = characterDB.GetCharacter(selectedOption);
    //     characterName.text = character.characterName;
    //     abilityName.text = character.abilityName;
    //     abilityExpanation.text = character.abilityExpanation;
    //     abilityImage.sprite = sprites[selectedOption];
    //     // } 2023.05.01 / HyungJun / Debug를 위한 주석처리
    //     // Vector3 vec3 = new Vector3(1, -11, 20);
    //     // Quaternion quater = Quaternion.Euler(0, 180, 0);
    //     _parentInstance = Instantiate(prefabs[selectedOption], new Vector3(1, -11, 20), Quaternion.Euler(0, 180, 0), parent);
    //     // _parentInstance.transform.localPosition = new Vector3(1, -11, 20);
    //     // _parentInstance.transform.localRotation = Quaternion.Euler(0, 180, 0);
    //     // Debug.Log("캐릭터 생성" + _parentInstance.transform.localPosition + " 메롱 " + _parentInstance.transform.localRotation);

    public void RunnerSelect()
    {
        UpdateCharacter(0);
        runnerPlayer.SetActive(true);
    }
    public void HealerSelect()
    {
        UpdateCharacter(1);
    }
    public void PhochoSelect()
    {
        UpdateCharacter(2);
    }
    public void JumperSelect()
    {
        UpdateCharacter(3);
    }

    private void UpdateKiller(int selectedKiller)
    {
        CharacterData character = killierDB.GetCharacter(selectedKiller);
        characterName.text = character.characterName;
        abilityName.text = character.abilityName;
        abilityExpanation.text = character.abilityExpanation;
        abilityImage.sprite = killerSprites[selectedKiller];
    }

    public void NoHairKiller()
    {
        UpdateKiller(0);
        noHairKiller.SetActive(true);
    }
    public void HairKiller()
    {
        UpdateKiller(1);
    }

}
