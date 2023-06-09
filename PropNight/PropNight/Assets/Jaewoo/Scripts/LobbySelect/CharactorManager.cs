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
        CharacterData character = characterDB.GetCharacter(selectedOption);
        characterName.text = character.characterName;
        abilityName.text = character.abilityName;
        abilityExpanation.text = character.abilityExpanation;
        abilityImage.sprite = sprites[selectedOption];
        Vector3 vec3 = new Vector3(1, -11, 20);
        Quaternion quater = Quaternion.Euler(0, 180, 0);
        GameObject parentInstance = Instantiate(prefabs[selectedOption], parent);
        parentInstance.transform.localPosition = vec3;
        parentInstance.transform.localRotation = quater;


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
