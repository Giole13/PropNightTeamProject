using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharactorManager : MonoBehaviour
{
    [SerializeField]
    private GameObject noHairKiller = default;
    [SerializeField]
    private GameObject runnerPlayer = default;
    public GameObject ifPlayertrue;
    public GameObject[] playerPrefabs = new GameObject[4];
    public GameObject[] killerPrefabs = new GameObject[2];
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
    void Awake()
    {
        sprites[0] = Resources.Load<Sprite>("skills/icons_skills_machine");
        sprites[1] = Resources.Load<Sprite>("skills/icons_skills_alchemy");
        sprites[2] = Resources.Load<Sprite>("skills/icons_skills_revive");
        sprites[3] = Resources.Load<Sprite>("skills/icons_skills_acrobat");

        killerSprites[0] = Resources.Load<Sprite>("skills/icons_skills_machine");
        killerSprites[1] = Resources.Load<Sprite>("skills/icons_skills_alchemy");
        killerSprites[2] = Resources.Load<Sprite>("skills/icons_skills_revive");
        killerSprites[3] = Resources.Load<Sprite>("skills/icons_skills_acrobat");
    }
    void Start()
    {
        UpdateKiller(selectedKiller);
        UpdateCharacter(selectedPlayer);
    }
    private void UpdateCharacter(int selectedPlayer)
    {
        CharacterData character = characterDB.GetCharacter(selectedPlayer);
        characterName.text = character.characterName;
        abilityName.text = character.abilityName;
        abilityExpanation.text = character.abilityExpanation;
        abilityImage.sprite = sprites[selectedPlayer];
    }
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
        abilityImage.sprite = sprites[selectedKiller];
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
