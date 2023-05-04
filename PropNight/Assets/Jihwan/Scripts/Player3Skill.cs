using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player3Skill : Skill
{
    // Start is called before the first frame update
    private PlayerMovement Player;
    void Start()
    {
        IsSkillActive = false;
        CoolTime = 10;
        Player = GetComponent<PlayerMovement>();
    }

    public override void ESkill()
    {
        if (Player.Stamina >= 100)
        {
            return;
        }
        IsSkillActive = true;
        Player.Stamina += 50;
        if (Player.Stamina > 100) { Player.Stamina = 100; }

    }
}
