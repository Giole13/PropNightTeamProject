using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Skill : Skill
{
    private PlayerMovement Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GetComponent<PlayerMovement>();
        Player.SkillDistance = 5;
    }


}
