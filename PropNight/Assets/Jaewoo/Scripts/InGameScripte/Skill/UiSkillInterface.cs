using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerSkill
{
    void playerSkillCool();
}
public interface IKillerSkill
{
    void KillerShortSkillCool();
    void KillerLongSkillLongCool();
}
public interface IPlayerEnumerator
{
    IEnumerator playerSkill(float cool);

}
public interface IKillerEnumverator
{
    IEnumerator KillerSkillShortCool(float cool);
    IEnumerator KillerSkillLongCool(float cool);
}

