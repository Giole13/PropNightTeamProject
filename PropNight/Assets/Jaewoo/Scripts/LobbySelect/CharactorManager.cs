using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharactorManager : MonoBehaviour
{
    public GameObject[] prefabs = new GameObject[4];
    public Transform parent;
    public CharacterDataBase characterDB;
    public TMP_Text characterName;
    public TMP_Text abilityName;
    public TMP_Text abilityExpanation;
    public Image abilityImage;
    private Sprite[] sprites = new Sprite[4];
    private int selectedOption = 0;

    private GameObject _parentInstance;
    void Awake()
    {
        sprites[0] = Resources.Load<Sprite>("skills/icons_skills_machine");
        sprites[1] = Resources.Load<Sprite>("skills/icons_skills_alchemy");
        sprites[2] = Resources.Load<Sprite>("skills/icons_skills_revive");
        sprites[3] = Resources.Load<Sprite>("skills/icons_skills_acrobat");
    }
    void Start()
    {
        UpdateCharacter(selectedOption);
    }

    private void UpdateCharacter(int selectedOption)
    {
        if (_parentInstance != null) { Destroy(_parentInstance); }
        // { 2023.05.01 / HyungJun / Debug를 위한 주석처리
        CharacterData character = characterDB.GetCharacter(selectedOption);
        characterName.text = character.characterName;
        abilityName.text = character.abilityName;
        abilityExpanation.text = character.abilityExpanation;
        abilityImage.sprite = sprites[selectedOption];
        // } 2023.05.01 / HyungJun / Debug를 위한 주석처리
        // Vector3 vec3 = new Vector3(1, -11, 20);
        // Quaternion quater = Quaternion.Euler(0, 180, 0);
        _parentInstance = Instantiate(prefabs[selectedOption], new Vector3(1, -11, 20), Quaternion.Euler(0, 180, 0), parent);
        // _parentInstance.transform.localPosition = new Vector3(1, -11, 20);
        // _parentInstance.transform.localRotation = Quaternion.Euler(0, 180, 0);
        // Debug.Log("캐릭터 생성" + _parentInstance.transform.localPosition + " 메롱 " + _parentInstance.transform.localRotation);

    }
    public void RunnerSelect()
    {
        UpdateCharacter(0);

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
}
