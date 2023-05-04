using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Skill : Skill
{
    private PlayerMovement Player;

    private void Start()
    {
        IsSkillActive = false;
        CoolTime = 15;
        Player = GetComponent<PlayerMovement>();
    }
    override public void ESkill()
    {
        if (Player.HP == 4)
        {
            return;
        } // 체력이 풀이라 스킬 실행 불가능
        IsSkillActive = true;
        Player.HP++; // 체력 1 회복
    }
}
