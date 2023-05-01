using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player4Skill : Skill
{
    private PlayerMovement Player;

    void Start()
    {
        Player = GetComponent<PlayerMovement>();
        Player.SkillJumpCount = 1;
    }
}
